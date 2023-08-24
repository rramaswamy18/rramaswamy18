using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryEnumerations
{
    public enum MaritalStatusEnum : int
    {
        [Description("")]
        _ = 0,
        [Description("Married")]
        Married = 100,
        [Description("Separated")]
        Separated = 200,
        [Description("Unmarried")]
        Unmarried = 300,
    }
}
