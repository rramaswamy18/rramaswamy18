using ArchitectureLibraryDocumentModels;
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
        public float DiscountPercent { set; get; }
        public string DiscountPercentFormatted { set; get; }
        public long ItemId { set; get; }
        public float ItemRateAfterDiscount { set; get; }
        public string ItemRateAfterDiscountFormatted { set; get; }
        public ItemModel ItemModel { set; get; }
        public List<ItemBundleItemModel> ItemBundleItemModels { set; get; }
    }
}
