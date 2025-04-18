using ArchitectureLibraryDocumentModels;
using RetailSlnEnumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class OrderHeaderWIPModel : AuditInfoModel
    {
        public long? OrderHeaderWIPId { set; get; }
        public long ClientId { set; get; }
        public long CorpAcctLocationId { set; get; }
        public long CreatedForPersonId {set; get; }
        public long InvoiceTypeId { set; get; }
        public float MaxSeqNum { set; get; }
        public string OrderDateTime { set; get; }
        public long? OrderStatusId { set; get; }
        public long PersonId { set; get; }
        public List<OrderDetailWIPModel> OrderDetailWIPModels { set; get; }
    }
}
