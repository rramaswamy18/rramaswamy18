using RetailSlnEnumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ApiPaymentMethodModel
    {
        public string PaymentModeDesc { set; get; }
        public PaymentModeEnum? PaymentModeId { set; get; }
        public string PaymentModeName { set; get; }
    }
}
