using ArchitectureLibraryModels;
using RetailSlnEnumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class OrderHeader : AuditInfoModel
    {
        public long OrderHeaderId { set; get; }
        public long ClientId { set; get; }
        public long CreatedForPersonId { set; get; }
        public InvoiceTypeEnum InvoiceTypeId { set; get; }
        public string OrderDateTime { set; get; }
        //public string OrderNumber { set; get; }
        public long OrderStatusId { set; get; }
        public long PersonId { set; get; }
        public bool SaveThisAddress { set; get; }
        public PersonModel PersonModel { set; get; }
        public PersonModel CreatedForPersonModel { set; get; }
        public List<OrderDetail> OrderDetails { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
