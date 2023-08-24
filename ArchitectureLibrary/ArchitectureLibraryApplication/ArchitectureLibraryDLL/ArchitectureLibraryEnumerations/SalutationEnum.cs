using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryEnumerations
{
    public enum SalutationEnum : int
    {
        [Description("")]
        _ = 0,
        [Description("Mr.")]
        Mr = 100,
        [Description("Mrs.")]
        Mrs = 200,
        [Description("M/s")]
        Ms = 300,
        [Description("Dr.")]
        Dr = 400,
    }
}
