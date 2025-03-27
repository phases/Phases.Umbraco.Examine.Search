using Examine;
using Examine.Search;

namespace Phases.Umb.Examine.Search.Extension.Extension
{
    /// <summary>
    /// Provides extension methods for Searcher
    /// </summary>
    public static class SearcherExtensions
    {
        /// <summary>
        /// Creates an instance of <see cref="IQuery"/> for the specified <paramref name="searcher"/>.
        /// </summary>
        /// <param name="searcher">The <see cref="ISearcher"/> instance to create the query for.</param>
        /// <param name="category">The category to search within. Default is <c>null</c>.</param>
        /// <param name="defaultOperation">The default operation for boolean queries. Default is <see cref="BooleanOperation.And"/>.</param>
        /// <returns>An instance of <see cref="IQuery"/> for the specified <paramref name="searcher"/>.</returns>
        public static IQuery CreateQueryWithCategory(this ISearcher searcher, string category = null, BooleanOperation defaultOperation = BooleanOperation.And)
        {
            var query = searcher.CreateQuery(category, defaultOperation);
            return query;
        }

    }
}