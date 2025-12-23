using ArchitectureLibraryDocumentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ShoppingCartWIPModel : AuditInfoModel
    {
        public long? ShoppingCartWIPId { set; get; }
        public long ClientId { set; get; }
        //public float BundleItemSeqNum { set; get; }
        public long CorpAcctLocationId { set; get; }
        public long CreatedForPersonId { set; get; }
        public bool DoNotBreakBundle { set; get; }
        public long ItemId { set; get; }
        public long OrderQty { set; get; }
        public long ParentItemId { set; get; }
        public long PersonId { set; get; }
        public float ItemSeqNum { set; get; }
    }
}
