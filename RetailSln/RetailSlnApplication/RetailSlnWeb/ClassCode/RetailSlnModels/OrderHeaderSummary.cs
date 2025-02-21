using ArchitectureLibraryDocumentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class OrderHeaderSummary : AuditInfoModel
    {
        public long OrderHeaderSummaryId { set; get; }
        public long ClientId { set; get; }
        public float BalanceDue { set; get; }
        public long OrderHeaderId { set; get; }
        public float ShippingAndHandlingCharges { set; get; }
        public float TotalAmountPaid { set; get; }
        public float TotalDiscountAmount { set; get; }
        public float TotalInvoiceAmount { set; get; }
        public float TotalOrderAmount { set; get; }
        public float TotalTaxAmount { set; get; }
    }
}
