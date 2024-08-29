using ArchitectureLibraryEnumerations;
using RetailSlnEnumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ShoppingCartItemBundleModel
    {
        public long ItemBundleItemId { set; get; }

        public long ItemId { set; get; }

        public ItemTypeEnum ItemTypeId { set; get; }

        public float OrderAmount { set; get; }

        public long OrderQty { set; get; }

        public int Quantity { set; get; }
    }
}
