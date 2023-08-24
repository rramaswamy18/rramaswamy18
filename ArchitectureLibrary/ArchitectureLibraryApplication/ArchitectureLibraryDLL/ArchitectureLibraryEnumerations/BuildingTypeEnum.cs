using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryEnumerations
{
    public enum BuildingTypeEnum : int
    {
        [Description("")]
        _ = 0,
        [Description("Apartment")]
        APT = 100,
        [Description("Suite")]
        STE = 200,
        [Description("Unit")]
        UNIT = 300,
        [Description("Floor")]
        FLR = 400,
    }
}
