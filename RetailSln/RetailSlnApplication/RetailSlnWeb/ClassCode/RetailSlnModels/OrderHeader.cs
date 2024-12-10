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
        public long OrderCreatedForPersonId { set; get; }
        public string OrderDateTime { set; get; }
        public string OrderNumber { set; get; }
        public long OrderStatusId { set; get; }
        public long PersonId { set; get; }
        public long TelephoneCountryId { set; get; }
        public string TelephoneNumber { set; get; }
    }
}
