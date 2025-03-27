using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Phases.Umb.Examine.Search.Extension.Constants.Constants;

namespace Phases.Umb.Examine.Search.Extension.Indexing
{
    public partial class SpellCheckOptions
    {
        public string IndexName { get; set; } = Configuration.DefaultIndexName;
        public List<string> IndexedFields { get; set; } = new List<string>(new string[] { "nodeName" });
        public bool AutoRebuildIndex { get; set; } = false;
        public int AutoRebuildDelay { get; set; } = 5;
        public int AutoRebuildRepeat { get; set; } = 30;
        public bool EnableLogging { get; set; } = false;
    }
}
