using ArchitectureLibraryDocumentModels;
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
        public string EmailAddress { set; get; }
        public long OrderCreatedByPersonId { set; get; }
        public string OrderDateTime { set; get; }
        public string OrderNumber { set; get; }
        public long OrderStatusId { set; get; }
        public long PersonId { set; get; }
    }
}
