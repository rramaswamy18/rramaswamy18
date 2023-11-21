using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryEnumerations
{
    public enum UserTypeEnum : int
    {
        [Description("Default Role")]
        DefaultRole = 100,
        [Description("System Administrator")]
        SystAdmin = 200,
        [Description("Application Administrator")]
        ApplAdmin = 300,
        [Description("Guest Role")]
        GuestRole = 400,
        [Description("Wholesale Role")]
        WholesaleRole = 500,
    }
}
