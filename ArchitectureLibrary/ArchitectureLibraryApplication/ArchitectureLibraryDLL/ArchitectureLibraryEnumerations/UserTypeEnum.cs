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
        [Description("System Administrator")]
        SystemAdmin = 100,
        [Description("Application Administrator")]
        ApplicationAdmin = 200,
        [Description("Regular User")]
        RegularUser = 300,
        [Description("Guest User")]
        GuestUser = 400,
    }
}
