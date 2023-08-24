using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryEnumerations
{
    public enum FluidVolumeUnitEnum : int
    {
        [Description("Liter")]
        Liter = 100,
        [Description("Milliliter")]
        Milliliter = 200,
        [Description("Fluid Ounce")]
        FluidOunce = 300,
    }
}
