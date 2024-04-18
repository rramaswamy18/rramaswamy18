using RetailSlnEnumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class PaymentSummaryDataModel
    {
        public string AspNetRoleName { set; get; }
        public float? BalanceDue { set; get; }
        public string CardHolderName { set; get; }
        public long CreditCardDataId { set; get; }
        public string CreditCardNumberLast4 { set; get; }
        public float? CreditCardPaymentAmount { set; get; }
        public string CreditCardProcessMessage { set; get; }
        public string CreditCardProcessStatus { set; get; }
        public string DeliveryMethodName { set; get; }
        public string EmailAddress { set; get; }
        public string GiftCertNumberLast4 { set; get; }
        public long? GiftCertId { set; get; }
        public float? GiftCertPaymentAmount { set; get; }
        public long? OrderHeaderId { set; get; }
        public string PaymentModeDesc { set; get; }
        public string PaymentModeName { set; get; }
        public string UserFullName { set; get; }
    }
}
