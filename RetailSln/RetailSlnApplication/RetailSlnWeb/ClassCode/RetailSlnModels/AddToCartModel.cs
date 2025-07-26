using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class AddToCartModel
    {
        public string DoNotBreakBundleParm { set; get; }

        public bool DoNotBreakBundle { set; get; }

        public long ItemId { set; get; }

        public string ItemIdParm { set; get; }

        public ItemModel ItemModel { set; get; }

        //public float ItemRate { set; get; }

        public long OrderQty { set; get; }

        public string OrderQtyParm { set; get; }

        public long ParentItemId { set; get; }

        public List<ShoppingCartItemModel> ShoppingCartItemBundleModels { set; get; }
    }
}
