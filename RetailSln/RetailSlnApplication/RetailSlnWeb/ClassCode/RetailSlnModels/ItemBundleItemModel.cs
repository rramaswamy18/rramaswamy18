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

        public long ItemBundleId { set; get; }

        public long ItemId { set; get; }

        public float ItemRateAfterDiscount { set; get; }

        public string ItemRateAfterDiscountFormatted { set; get; }

        public float ItemRateBeforeDiscount { set; get; }

        public float OrderQtyIndex { set; get; }

        public float? OrderQtyIndexFinish { set; get; }

        public float? OrderQtyIndexStart { set; get; }

        public short Quantity { set; get; }

        public float SeqNum { set; get; }

        public ItemBundleModel ItemBundleModel { set; get; }

        public ItemModel ItemModel { set; get; }
    }
}
