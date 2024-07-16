using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ItemBundleItemModel : AuditInfoModel
    {
        public long ItemBundleItemId { set; get; }

        public long ClientId { set; get; }

        public long BundleItemId { set; get; }

        public ItemModel BundleItemModel { set; get; }

        public float SeqNum { set; get; }

        public long ItemId { set; get; }

        public short Quantity { set; get; }

        public ItemModel ItemModel { set; get; }
    }
}
