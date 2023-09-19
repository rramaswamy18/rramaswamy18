using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPrdEnumerations
{
    public enum PaymentMethodEnum : int
    {
        [Description("Check")]
        Check = 100,
        [Description("Credit Card")]
        CreditCard = 200,
    }
}
