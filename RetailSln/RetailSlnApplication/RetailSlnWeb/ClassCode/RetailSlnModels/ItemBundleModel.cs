using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ItemBundleModel : AuditInfoModel
    {
        public long ItemBundleId { set; get; }
        public long ClientId { set; get; }
        public long ItemId { set; get; }
        public long ParentItemId { set; get; }
        public float SeqNum { set; get; }
        public ItemModel ItemModel { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
