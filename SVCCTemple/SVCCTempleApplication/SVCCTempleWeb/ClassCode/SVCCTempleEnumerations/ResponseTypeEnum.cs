using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SVCCTempleEnumerations
{
    public enum ResponseTypeEnum : int
    {
        [Description("Info")]
        Info = 100,
        [Description("Success")]
        Success = 200,
        [Description("Error")]
        Error = 300
    }
}
