using ArchitectureLibraryDocumentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class OrderDetailWIPModel : AuditInfoModel
    {
        public long? OrderDetailWIPId { set; get; }
        public long ClientId { set; get; }
        public long ItemId { set; get; }
        public float ItemRate { set; get; }
        public long OrderHeaderWIPId { set; get; }
        public long OrderQty { set; get; }
        public long ParentItemId { set; get; }
        public float SeqNum { set; get; }
        public List<OrderDetailWIPModel> ShoppingCartItemBundleModels { set; get; }
    }
}
