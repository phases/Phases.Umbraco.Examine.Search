using Examine;
using Phases.Umb.Examine.Search.Extension.Composing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Umbraco.Cms.Core.Mapping;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Phases.Umb.Examine.Search.Extension.Extension
{
    /// <summary>
    /// Provides extension methods for Search Results
    /// </summary>
    public static class SearchResultExtensions
    {
        /// <summary>
        /// Extension method that gets the search results converted to the given type.
        /// </summary>
        /// <typeparam name="T">The type to convert the search results to.</typeparam>
        /// <param name="results">The search results to convert.</param>
        /// <returns>An enumerable collection of converted search results.</returns>
        public static IEnumerable<T> GetResults<T>(this IEnumerable<ISearchResult> results)
        {
            return results
                .Select(x => ConvertValue<T>(x))
                .Where(x => x != null);
        }

        /// <summary>
        /// Extension method that gets the value for a particular field in the search results.
        /// </summary>
        /// <param name="result">The search result to get the value from.</param>
        /// <param name="field">The field to get the value for.</param>
        /// <returns>The value of the field.</returns>
        public static string Value(this ISearchResult result, string field)
        {
            result.Values.TryGetValue(field, out string value);

            return value;
        }

        /// <summary>
        /// Extension method that gets the value for a particular field in the search results.
        /// </summary>
        /// <typeparam name="T">The type to convert the value to.</typeparam>
        /// <param name="result">The search result to get the value from.</param>
        /// <param name="field">The field to get the value for.</param>
        /// <returns>The value of the field converted to the given type.</returns>
        public static T Value<T>(this ISearchResult result, string field)
        {
            result.Values.TryGetValue(field, out string value);

            return ConvertValue<T>(value);
        }

        /// <summary>
        /// Extension method that gets multiple values for a particular field in the search results.
        /// </summary>
        /// <typeparam name="T">The type to convert the values to.</typeparam>
        /// <param name="result">The search result to get the values from.</param>
        /// <param name="field">The field to get the values for.</param>
        /// <returns>An enumerable collection of values for the given field.</returns>
        public static IEnumerable<T> Values<T>(this ISearchResult result, string field)
        {
            var values = result.GetValues(field);

            return values
                .Select(x => ConvertValue<T>(x))
                .Where(x => x != null);
        }


        /// <summary>
        /// Converts the given value to the specified type T using the appropriate converter.
        /// </summary>
        /// <typeparam name="T">The type to which the value should be converted.</typeparam>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        private static T ConvertValue<T>(object value)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));

            if (converter.CanConvertFrom(value.GetType()) == true)
            {
                return (T)converter.ConvertFrom(value);
            }

            var mapper = ServiceLocator.GetInstance<IUmbracoMapper>();

            if (typeof(IPublishedContent).IsAssignableFrom(typeof(T)) == true)
            {
                return (T)mapper.Map<IPublishedContent>(value);
            }

            return mapper.Map<T>(value);
        }

    }
}