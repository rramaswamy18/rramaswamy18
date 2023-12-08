using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryEnumerations
{
    public enum WeightUnitEnum : int
    {
        [Description("Gram(s)")]
        Grams = 100,
        [Description("Kilogram(s)")]
        Kilograms = 200,
    }
}
