﻿using Examine;
using Examine.Search;
using Phases.Umb.Examine.Search.Extension.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Phases.Umb.Examine.Search.Extension.Helpers
{
    public class SearchHelper
    {
        /// <summary>
        /// Get the results for the given query
        /// </summary>
        public ISearchResults Search(IBooleanOperation query, out int totalResults)
        {
            var searchResults = query.Execute();

            totalResults = (int)searchResults.TotalItemCount;

            return searchResults;
        }

        /// <summary>
        /// Get the results for the given query
        /// </summary>
        public IEnumerable<T> Search<T>(IBooleanOperation query, out int totalResults)
            where T : class, IPublishedContent
        {
            var searchResults = query.Execute();

            totalResults = (int)searchResults.TotalItemCount;

            return searchResults.GetResults<T>();
        }

        /// <summary>
        /// Get paged results for the given query
        /// </summary>
        public IEnumerable<ISearchResult> Page(IBooleanOperation query, int page, int perPage, out int totalResults)
        {
            var searchResults = Search(query, out totalResults);

            return searchResults
                .Skip((page - 1) * perPage)
                .Take(perPage);
        }

        /// <summary>
        /// Get paged results for the given query
        /// </summary>
        public IEnumerable<ISearchResult> Page(IBooleanOperation query, int page, int perPage, out int totalPages, out int totalResults)
        {
            var results = Page(query, page, perPage, out totalResults);

            totalPages = (int)Math.Ceiling((decimal)totalResults / perPage);

            return results;
        }

        /// <summary>
        /// Get paged results for the given query
        /// </summary>
        public IEnumerable<T> Page<T>(IBooleanOperation query, int page, int perPage, out int totalResults)
            where T : class, IPublishedContent
        {
            var searchResults = Search(query, out totalResults);

            return searchResults
                .Skip((page - 1) * perPage)
                .GetResults<T>()
                .Take(perPage);
        }

        /// <summary>
        /// Get paged results for the given query
        /// </summary>
        public IEnumerable<T> Page<T>(IBooleanOperation query, int page, int perPage, out int totalPages, out int totalResults)
            where T : class, IPublishedContent
        {
            var results = Page<T>(query, page, perPage, out totalResults);

            totalPages = (int)Math.Ceiling((decimal)totalResults / perPage);

            return results;
        }
    }
}