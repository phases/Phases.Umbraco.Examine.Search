using HtmlAgilityPack;
using Phases.Umb.Examine.Search.Extension.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Phases.Umb.Examine.Search.Extension.Helpers
{

    /// <summary>
    /// Provides string format extension
    /// </summary>
    public class StringFormatHelper : IStringFormatHelper
    {

        public StringFormatHelper()
        {
        }
        /// <summary>
        /// Extension method that gets the text from the htmlcontent.
        /// </summary>
        /// <param name="htmlContent">The html content to convert.</param>
        /// <returns>string.</returns>
        public string ExtractTextFromHtml(string htmlContent)
        {
            string extractedText = "";
            if (!string.IsNullOrWhiteSpace(htmlContent))
            {
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(htmlContent);

                // Find the div element with the specified class
                extractedText = !string.IsNullOrWhiteSpace(htmlDoc.DocumentNode.InnerText) ? Regex.Replace(htmlDoc.DocumentNode.InnerText, @"<[^>]+>|(\s)\s+|\x1B\[\d+m", "$1").Replace("\r", " ").Replace("\\n", " ") : "";
            }
            return extractedText;
        }

        /// <summary>
        /// Highlights all occurances of the search terms in a body of text.
        /// </summary>
        /// <param name="input">The content .</param>
        /// <param name="searchTerms">The searchTerms to add highlight class.</param>
        /// <returns>string.</returns>

        public string HighlightString(string input, IEnumerable<string> searchTerms)
        {

            foreach (var searchTerm in searchTerms)
            {
                input = Regex.Replace(input, Regex.Escape(searchTerm), @"<span class=""search-highlight"">$0</span>", RegexOptions.IgnoreCase);
            }

            return input;
        }
    }
}
