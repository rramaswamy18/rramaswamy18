using ArchitectureLibraryModels;
using RetailSlnEnumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class OrderListDataModel
    {
        public long OrderHeaderId { set; get; }
        public long CreatedForPersonId { set; get; }
        public string CreatedForFirstName { set; get; }
        public string CreatedForLastName { set; get; }
        public InvoiceTypeEnum InvoiceTypeId { set; get; }
        public string OrderDateTime { set; get; }
        public OrderStatusEnum OrderStatusId { set; get; }
        public long PersonId { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }

        public float BalanceDue { set; get; }
        public float ShippingAndHandlingCharges { set; get; }
        public float TotalAmountPaid { set; get; }
        public float TotalDiscountAmount { set; get; }
        public float TotalInvoiceAmount { set; get; }
        public float TotalOrderAmount { set; get; }
        public float TotalTaxAmount { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
