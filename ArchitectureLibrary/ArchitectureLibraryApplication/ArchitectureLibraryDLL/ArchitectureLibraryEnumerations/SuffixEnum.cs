using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryEnumerations
{
    public enum SuffixEnum : int
    {
        [Description("")]
        _ = 0,
        [Description("Jr")]
        Jr = 100,
        [Description("Sr")]
        Sr = 200,
    }
}
