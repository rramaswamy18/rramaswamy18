using ArchitectureLibraryDocumentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class OrderDetailItemBundle : AuditInfoModel
    {
        public long? OrderDetailItemBundleId { set; get; }

        public long ClientId { set; get; }

        public float DiscountPercent { set; get; }

        public long ItemBundleId { set; get; }

        public long ItemBundleItemId { set; get; }

        public long ItemId { set; get; }

        public string ItemMasterDesc { set; get; }

        public float ItemRate { set; get; }

        public float ItemRateBeforeDiscount { set; get; }

        public float OrderAmount { set; get; }

        public float OrderAmountBeforeDiscount { set; get; }

        public long OrderDetailId { set; get; }

        public long OrderDetailTypeId { set; get; }

        public long OrderQty { set; get; }

        public float SeqNum { set; get; }
    }
}