using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ParentItemBundleModel
    {
        public List<ItemModel> ItemModels { set; get; }
        public long ParentItemId { set; get; }
        public ItemModel ParentItemModel { set; get; }
        public List<ShoppingCartItemModel> ShoppingCartItemBundleModels { set; get; }
        public float TotalOrderAmount { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
