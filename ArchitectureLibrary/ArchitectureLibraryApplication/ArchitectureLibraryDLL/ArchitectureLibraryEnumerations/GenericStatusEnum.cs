using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryEnumerations
{
    public enum GenericStatusEnum : int
    {
        [Description("Active")]
        Active = 100,
        [Description("Inactive")]
        Inactive = 200,
        [Description("Soft Delete")]
        Deleted = 300,
    }
}
