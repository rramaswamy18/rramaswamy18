using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryEnumerations
{
    public enum LoginTypeEnum : int
    {
        [Description("Unique Login Id")]
        LoginId = 100,
        [Description("Telephone Number with extension")]
        TelephoneNumberWithExtension = 200,
        [Description("Email Address")]
        EmailAddress = 300,
        [Description("Facebook")]
        Facebook = 400,
        [Description("Twitter")]
        Twitter = 500,
        [Description("Google")]
        Google = 600,
        [Description("Linkedin")]
        Linkedin = 700,
    }
}
