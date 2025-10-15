using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ShoppingCartBundleModel
    {
        public bool Checkout { set; get; }

        public int Index { set; get; }

        public ShoppingCartItemModel ShoppingCartItemModel { set; get; }
    }
}
