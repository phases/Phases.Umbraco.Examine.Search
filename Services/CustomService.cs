using Lucene.Net.Analysis.Standard;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Search.Highlight;
using Lucene.Net.Util;
using Microsoft.AspNetCore.Hosting.Server;
using NTextCat;
using Phases.Umb.Examine.Search.Extension.Interfaces;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Umbraco.Cms.Core.Constants;
using System.Reflection;
using System;

namespace Phases.Umb.Examine.Search.Extension.Services
{
    public class CustomService : ICustomService
    {
        public CustomService()
        {

        }
        /// <summary>
		/// Get the fragment for the given matching text
		/// </summary>
		/// <param name="SearchTerm">The search terms</param>
		/// <param name="type">The content type</param>
		/// <param name="fragmentSize">The fragment charater count</param>
		/// <returns>The object of highlighter class </returns>
		public Highlighter Highlighter(string SearchTerm, string type = "content", int fragmentSize = 400)
        {
            var queryParser = new QueryParser(LuceneVersion.LUCENE_48, type, new StandardAnalyzer(LuceneVersion.LUCENE_48));
            queryParser.AllowLeadingWildcard = true;
            var query = queryParser.Parse(SearchTerm);
            var highlighter = new Highlighter(new QueryScorer(query));
            var fragmenter = new SimpleFragmenter(fragmentSize);
            highlighter.TextFragmenter = fragmenter;
            return highlighter;
        }

        /// <summary>
        /// Get the fragment for the given matching text
        /// </summary>
        /// <param name="SearchTerm">The search terms</param>
        /// <param name="type">The content type</param>
        /// <returns>The object of highlighter class </returns>
        public Highlighter Highlighter(string SearchTerm, string type)
        {
            var queryParser = new QueryParser(LuceneVersion.LUCENE_48, type, new StandardAnalyzer(LuceneVersion.LUCENE_48));
            queryParser.AllowLeadingWildcard = true;
            var query = queryParser.Parse(SearchTerm);
            var highlighter = new Highlighter(new QueryScorer(query));
            var fragmenter = new SimpleFragmenter();
            highlighter.TextFragmenter = fragmenter;
            return highlighter;
        }

        /// <summary>
        /// Get the language ISO code for the given matching text
        /// </summary>
        /// <param name="content">The language content</param>
        /// <returns>The object of Language Info </returns>
        public Tuple<LanguageInfo, double> DetectLanguage(string content)
        {

            // Don't forget to deploy a language profile (e.g. Core14.profile.xml) with your application.
            // (take a look at "content" folder inside of NTextCat nupkg and here: https://github.com/ivanakcheurov/ntextcat/tree/master/src/LanguageModels).
            var factory = new RankedLanguageIdentifierFactory();

            var identifier = factory.Load("Core14.xml"); // can be an absolute or relative path. Beware of 260 chars limitation of the path length in Windows. Linux allows 4096 chars.
            var languages = identifier.Identify(content);
            var mostCertainLanguage = languages.FirstOrDefault();
            return mostCertainLanguage;
        }

    }
}
