using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace RetailSlnEnumerations
{
    public enum PaymentStatusEnum : int
    {
        [Description("Not Paid")]
        NotPaid = 100,
        [Description("Partially Paid")]
        PartiallyPaid = 200,
        [Description("Paid in full")]
        PaidInFull = 1800,
    }
}
