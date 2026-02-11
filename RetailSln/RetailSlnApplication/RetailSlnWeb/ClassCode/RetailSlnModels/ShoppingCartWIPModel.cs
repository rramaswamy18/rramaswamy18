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
        public bool DoNotBreakBundle { set; get; }
        public long ItemId { set; get; }
        public float ItemSeqNum { set; get; }
        public string OrderComments { set; get; }
        public long OrderQty { set; get; }
        public long ParentItemId { set; get; }
        public long ShoppingCartWIPHdrId { set; get; }
    }
}
