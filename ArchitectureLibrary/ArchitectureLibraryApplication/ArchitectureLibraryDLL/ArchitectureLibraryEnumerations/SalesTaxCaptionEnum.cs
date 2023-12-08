using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryEnumerations
{
    public enum SalesTaxCaptionEnum : int
    {
        [Description("State Rate")]
        StateRate = 100,
        [Description("Est County Rate")]
        EstCountyRate = 200,
        [Description("Est City Rate")]
        EstCityRate = 300,
        [Description("Est Special Rate")]
        EstSpecialRate = 400,
        [Description("Sales Tax")]
        EstCombinedRate = 500,
        [Description("CGST")]
        CGST = 600,
        [Description("SGST")]
        SGST = 700,
        [Description("IGST")]
        IGST = 800,
        [Description("UTGST")]
        UTGST = 900,
    }
}
