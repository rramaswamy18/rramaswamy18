using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class PaymentDataModel
    {
        public long CouponId { set; get; }
        public long CreditCardDataId { set; get; }
        public long GiftCertId { set; get; }
        public long OrderHeaderId { set; get; }
        public long OrderPaymentId { set; get; }
        public long PaymentModeId { set; get; }
        public Dictionary<string, Dictionary<string, string>> PaymentRefOptions { set; get; }
        //public string PaymentRefNumCaption1 { set; get; }
        //public string PaymentRefNumData1 { set; get; }
        //public string PaymentRefNumCaption2 { set; get; }
        //public string PaymentRefNumData2 { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
