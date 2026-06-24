using ArchitectureLibraryModels;
using RetailSlnEnumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class OrderEditModel
    {
        public int Index { set; get; }
        public InvoiceTypeEnum InvoiceTypeId { set; get; }
        public long OrderDeliveryId { get; set; }
        public long OrderHeaderId { set; get; }
        public long OrderHeaderSummaryId { set; get; }
        public long OrderPaymentId { get; set; }
        public OrderStatusEnum OrderStatusId { set; get; }
        public PaymentModeEnum PaymentModeId { set; get; }
        public PaymentStatusEnum PaymentStatusId { set; get; }
        public string TrackingRefNumber { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
