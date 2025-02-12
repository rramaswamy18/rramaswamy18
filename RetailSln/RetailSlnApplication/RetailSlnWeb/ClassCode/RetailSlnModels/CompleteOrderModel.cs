using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class CompleteOrderModel
    {
        public long? PaymentAmount { set; get; }
        public long? ApproverSignatureTextId { set; get; }
        public string ApproverSignatureTextValue { set; get; }
    }
}
