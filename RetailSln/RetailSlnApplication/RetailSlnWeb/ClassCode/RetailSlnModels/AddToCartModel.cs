using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class AddToCartModel
    {
        public long ItemId { set; get; }

        public float ItemRate { set; get; }

        //public float DiscountPercent { set; get; }

        public long OrderQty { set; get; }

        public List<ShoppingCartItemBundleModel> ShoppingCartItemBundleModels { set; get; }
    }
}
