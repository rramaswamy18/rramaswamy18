using RetailSlnEnumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class OrderPayment
    {
        public long OrderPaymentId { get; set; }
        public long ClientId { set; get; }
        public PaymentModeEnum PaymentModeId { set; get; }
        public PaymentStatusEnum PaymentStatusId { set; get; }
    }
}
