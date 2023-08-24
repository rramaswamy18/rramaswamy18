using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryEnumerations
{
    public enum AddressTypeEnum : int
    {
        [Description("")]
        _ = 0,
        [Description("Home Address")]
        Home = 100,
        [Description("Work Address")]
        Work = 200,
        [Description("Business Address")]
        Business = 300,
        [Description("Mailing Address")]
        Mailing = 400,
        [Description("P.O Box Address")]
        POBox = 500,
    }
}
