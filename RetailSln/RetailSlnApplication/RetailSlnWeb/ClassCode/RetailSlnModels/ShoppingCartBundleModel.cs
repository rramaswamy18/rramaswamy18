using ArchitectureLibraryEnumerations;
using RetailSlnEnumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ShoppingCartBundleModel
    {
        public long ItemBundleId { set; get; }

        public long ItemId { set; get; }

        public float DiscountPercent { set; get; }

        public long OrderQty { set; get; }

        public string OrderComments { set; get; }

        public List<ShoppingCartItemBundleModel> ShoppingCartItemBundleModels { set; get; }
    }
}
