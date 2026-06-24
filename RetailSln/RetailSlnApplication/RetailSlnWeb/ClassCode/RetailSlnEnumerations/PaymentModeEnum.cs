using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace RetailSlnEnumerations
{
    public enum PaymentModeEnum : int
    {
        [Description("Credit Sale")]
        CreditSale = 100,
        [Description("Payment Gateway")]
        PaymentGateway = 200,
        [Description("Cash on Delivery (COD)")]
        COD = 300,
        [Description("Process by credit card")]
        ProcessCreditCard = 400,
    }
}
