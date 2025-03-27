using Examine;
using Examine.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Phases.Umb.Examine.Search.Extension.Extension
{
    public static class SearchExtension
    {
        /// <summary>
        /// Get the results for the given query
        /// </summary>
        private static ISearchResults PerformSearch(this IBooleanOperation query, out int totalResults)
        {
            var searchResults = query.Execute();

            totalResults = (int)searchResults.TotalItemCount;

            return searchResults;
        }

        /// <summary>
        /// Get the results for the given query
        /// </summary>
        public static IEnumerable<T> PerformSearch<T>(this IBooleanOperation query)
            where T : class, IPublishedContent
        {
            var searchResults = query.Execute();
            return searchResults.GetResults<T>();
        }

        /// <summary>
        /// Get paged results for the given query
        /// </summary>
        public static IEnumerable<ISearchResult> PerformSearchByPage(this IBooleanOperation query, int page, int perPage, out int totalResults)
        {
            var searchResults = PerformSearch(query, out totalResults);

            return searchResults
                .Skip((page - 1) * perPage)
                .Take(perPage);
        }

        /// <summary>
        /// Get paged results for the given query
        /// </summary>
        public static IEnumerable<ISearchResult> PerformSearchByPage(this IBooleanOperation query, int page, int perPage, out int totalPages, out int totalResults)
        {
            var results = PerformSearchByPage(query, page, perPage, out totalResults);

            totalPages = (int)Math.Ceiling((decimal)totalResults / perPage);

            return results;
        }

        /// <summary>
        /// Get paged results for the given query
        /// </summary>
        public static IEnumerable<T> PerformSearchByPage<T>(this IBooleanOperation query, int page, int perPage, out int totalResults)
            where T : class, IPublishedContent
        {
            var searchResults = PerformSearch(query, out totalResults);

            return searchResults
                .Skip((page - 1) * perPage)
                .GetResults<T>()
                .Take(perPage);
        }

        /// <summary>
        /// Get paged results for the given query
        /// </summary>
        public static IEnumerable<T> PerformSearchByPage<T>(IBooleanOperation query, int page, int perPage, out int totalPages, out int totalResults)
            where T : class, IPublishedContent
        {
            var results = PerformSearchByPage<T>(query, page, perPage, out totalResults);

            totalPages = (int)Math.Ceiling((decimal)totalResults / perPage);

            return results;
        }


    }
}
