using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phases.Umb.Examine.Search.Extension.Constants
{
    internal static partial class Constants
    {
        internal static partial class Internals
        {
            internal const string FieldName = "word";
        }

        internal static partial class Configuration
        {
            internal const string DefaultIndexName = "ExternalIndex";
            internal const string ConfigurationSection = "SearchSpellCheck";
            internal const string IndexName = "Our.Umbraco.SearchSpellCheck.IndexName";
            internal const string IndexedFields = "Our.Umbraco.SearchSpellCheck.IndexedFields";
            internal const string AutoRebuildIndex = "Our.Umbraco.SearchSpellCheck.AutoRebuildIndex";
            internal const string AutoRebuildDelay = "Our.Umbraco.SearchSpellCheck.AutoRebuildDelay";
            internal const string AutoRebuildRepeat = "Our.Umbraco.SearchSpellCheck.AutoRebuildRepeat";
            internal const string EnableLogging = "Our.Umbraco.SearchSpellCheck.EnableLogging";
        }
    }
}
