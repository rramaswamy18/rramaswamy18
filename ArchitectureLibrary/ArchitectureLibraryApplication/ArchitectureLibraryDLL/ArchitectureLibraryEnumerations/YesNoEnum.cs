using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryEnumerations
{
    public enum YesNoEnum : int
    {
        [Description("")]
        _ = 0,
        [Description("Yes")]
        Yes = 100,
        [Description("No")]
        No = 200,
    }
}
