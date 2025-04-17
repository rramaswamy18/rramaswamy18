using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ItemBundleDiscountModel : AuditInfoModel
    {
        public long ItemBundleDiscountId { set; get; }

        public long ClientId { set; get; }

        public long ItemBundleId { set; get; }

        public float SeqNum { set; get; }

        public long ItemId { set; get; }
    }
}