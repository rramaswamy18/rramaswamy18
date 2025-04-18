using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetailSlnEnumerations
{
    public enum PaymentModeEnum : int
    {
        [Display(Name = "Credit Sale")]
        CreditSale = 100,
        [Display(Name = "Payment Gateway")]
        PaymentGateway = 200,
        [Display(Name = "Cash on Delivery (COD)")]
        COD = 300,
        [Display(Name = "Process by credit card")]
        ProcessCreditCard = 400,
    }
}
