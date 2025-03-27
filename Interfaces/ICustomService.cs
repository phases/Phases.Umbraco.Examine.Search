using Lucene.Net.Search.Highlight;
using NTextCat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phases.Umb.Examine.Search.Extension.Interfaces
{
    public interface ICustomService
    {
        Highlighter Highlighter(string SearchTerm, string type = "content", int fragmentSize = 400);
        Highlighter Highlighter(string SearchTerm, string type);
        public Tuple<LanguageInfo, double> DetectLanguage(string content);
    }
}
