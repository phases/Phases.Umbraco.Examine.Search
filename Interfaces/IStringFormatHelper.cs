﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phases.Umb.Examine.Search.Extension.Interfaces
{
    public interface IStringFormatHelper
    {
        public string ExtractTextFromHtml(string htmlContent);
        public string HighlightString(string input, IEnumerable<string> searchTerms);
    }
}
