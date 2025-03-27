using Lucene.Net.Analysis;
using Lucene.Net.Util;
using System.IO;

namespace Phases.Umb.Examine.Search.Extension.Analysis
{
    public class WhitespaceSeparatorAnalyzer : Analyzer
    {
        public WhitespaceSeparatorAnalyzer(LuceneVersion matchVersion, char separator)
        {
            MatchVersion = matchVersion;
            Separator = separator;
        }

        public LuceneVersion MatchVersion { get; }

        public char Separator { get; }

        protected override TokenStreamComponents CreateComponents(string fieldName, TextReader reader)
        {
            return new TokenStreamComponents(new WhitespaceSeparatorTokenizer(MatchVersion, reader, Separator));
        }
    }
}