using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryEnumerations
{
    public enum UserStatusEnum : int
    {
        [Description("Active")]
        Active = 100,
        [Description("Inactive")]
        Inactive = 200,
        [Description("Account Locked")]
        Locked = 300,
        [Description("Login Denied")]
        LoginDenied = 400,
    }
}
