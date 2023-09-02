using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class OrderListModel
    {
        public long OrderHeaderId { set; get; }

        public float BalanceDue { set; get; }

        public string OrderDate { set; get; }

        public string OrderNumber { set; get; }

        public float ShippingHandlingCharges { set; get; }

        public float TotalAmountPaid { set; get; }

        public float TotalInvoiceAmount { set; get; }
    }
}
