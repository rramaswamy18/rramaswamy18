using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryEnumerations
{
    public enum ContactUsTypeEnum : int
    {
        [Description("")]
        _ = 0,
        [Description("Request")]
        Request = 100,
        [Description("Question")]
        Question = 200,
        [Description("Comments")]
        Comments = 300,
        [Description("Other")]
        Other = 9900,
    }
}
