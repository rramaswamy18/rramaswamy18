using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class PaymentData1Model
    {
        public long CouponId { set; get; }
        public long CreditCardDataId { set; get; }
        public long GiftCertId { set; get; }
        public long OrderHeaderId { set; get; }
        public string PaymentReferenceNumber { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
