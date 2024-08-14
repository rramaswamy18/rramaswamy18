using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class OrderSummaryModel
    {
        public string AspNetUserId { set; get; }
        //public long? DeliveryAddressId { set; get; }
        //public float BalanceDue { set; get; }
        //public string BalanceDueFormatted { set; get; }
        public long? OrderHeaderId { set; get; }
        public CorpAcctModel CorpAcctModel { set; get; }
        public string EmailAddress { set; get; }
        public string FirstName { set; get; }
        //public float InvoiceAmount { set; get; }
        //public string InvoiceAmountFormatted { set; get; }
        public string LastName { set; get; }
        public long? PersonId { set; get; }
        public string TelephoneCode { set; get; }
        public string TelephoneCountryId { set; get; }
        public string TelephoneNumber { set; get; }
        public string UserFullName { set; get; }
    }
}
