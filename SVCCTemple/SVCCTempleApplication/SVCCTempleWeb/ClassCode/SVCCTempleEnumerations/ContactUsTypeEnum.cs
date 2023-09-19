using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SVCCTempleEnumerations
{
    public enum ContactUsTypeEnum : int
    {
        [Description("Request")]
        Request = 100,
        [Description("Suggestion")]
        Suggestion = 200,
        [Description("Question")]
        Question = 300,
    }
}
