using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPrdEnumerations
{
    public enum PaymentStatusEnum : int
    {
        [Description("Open")]
        Open = 100,
        [Description("Partial Payment")]
        PartialPayment = 200,
        [Description("Parid")]
        Paid = 300,
    }
}
