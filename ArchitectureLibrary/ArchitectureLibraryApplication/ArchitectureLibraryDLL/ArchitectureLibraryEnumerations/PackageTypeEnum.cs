using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryEnumerations
{
    public enum PackageTypeEnum : int
    {
        [Description("Box")]
        Box = 100,
        [Description("Container")]
        Container = 200,
    }
}
