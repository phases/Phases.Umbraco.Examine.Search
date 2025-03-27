using Examine;
using Examine.Search;
using Phases.Umb.Examine.Search.Extension.Extension;
using System;
using System.Linq;

namespace Phases.Umb.Examine.Search.Extension.Extension
{
    /// <summary>
    /// Provides extension methods for Enhancing the search behaviour
    /// </summary>
    public static class SearchTermExtensions
    {
        /// <summary>
        /// Boosts the relevance of terms in search
        /// </summary>
        /// <param name="input">The array of search terms</param>
        /// <param name="boost">The boost factor to apply</param>
        /// <returns>The array of boosted search terms</returns>
        public static IExamineValue[] Boost(this string[] input, float boost)
        {
            return input
            .Select(x => x.Boost(boost))
            .ToArray();
        }

        /// <summary>
        /// Escapes the terms so they are used literally in search
        /// </summary>
        /// <param name="input">The array of search terms</param>
        /// <returns>The array of escaped search terms</returns>
        public static IExamineValue[] Escape(this string[] input)
        {
            return input
                .Select(x => x.Escape())
                .ToArray();
        }

        /// <summary>
        /// Fuzzy matches the terms in search
        /// </summary>
        /// <param name="input">The array of search terms</param>
        /// <param name="fuzziness">The degree of fuzziness for the match</param>
        /// <returns>The array of fuzzy-matched search terms</returns>
        public static IExamineValue[] Fuzzy(this string[] input, float fuzziness = 0.5f)
        {
            return input
                .Select(x => x.Fuzzy(fuzziness))
                .ToArray();
        }

        /// <summary>
        /// Matches terms with a single wildcard character in search
        /// </summary>
        /// <param name="input">The array of search terms</param>
        /// <returns>The array of wildcard-matched search terms</returns>
        public static IExamineValue[] SingleCharacterWildcard(this string[] input)
        {
            return input
                .Select(x => x.SingleCharacterWildcard())
                .ToArray();
        }

        /// <summary>
        /// Matches terms with a multiple wildcard characters in search
        /// </summary>
        /// <param name="input">The array of search terms</param>
        /// <returns>The array of wildcard-matched search terms</returns>
        public static IExamineValue[] MultipleCharacterWildcard(this string[] input)
        {
            return input
                .Select(x => x.MultipleCharacterWildcard())
                .ToArray();
        }

        /// <summary>
        /// Matches terms within the defined proximity of each other in search
        /// </summary>
        /// <param name="input">The array of search terms</param>
        /// <param name="proximity">The maximum distance between the matched terms</param>
        /// <returns>The array of proximity-matched search terms</returns>
        public static IExamineValue[] Proximity(this string[] input, int proximity)
        {
            return input
                .Select(x => x.Proximity(proximity))
                .ToArray();
        }

        /// <summary>
        /// Splits a string, removing any stop words and empty parts
        /// </summary>
        /// <param name="input">The string to split</param>
        /// <param name="separator">The separator character to use</param>
        /// <returns>The array of sanitized search terms</returns>
        public static string[] ToSafeArray(this string input, string separator = " ")
        {
            return input
                .RemoveStopWords()
                .Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Splits a string, removing empty parts
        /// </summary>
        /// <param name="input">The string to split</param>
        /// <param name="separator">The separator character to use</param>
        /// <returns>The array of sanitized search terms</returns>
        public static string[] ToDefaultArray(this string input, string separator = " ")
        {
            return input
                .Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}