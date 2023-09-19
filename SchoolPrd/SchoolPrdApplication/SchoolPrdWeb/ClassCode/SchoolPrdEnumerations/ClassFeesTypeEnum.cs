using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SchoolPrdEnumerations
{
    public enum ClassFeesTypeEnum : int
    {
        [Description("Fees")]
        Fees = 100,
        [Description("Discount")]
        Discount = 200,
    }
}
