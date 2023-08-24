using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryEnumerations
{
    public enum StatusEnum : int
    {
        [Description("Active")]
        Active = 100,
        [Description("Inactive")]
        Inactive = 200,
    }
}
