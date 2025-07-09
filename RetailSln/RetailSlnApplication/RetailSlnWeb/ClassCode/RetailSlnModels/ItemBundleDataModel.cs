using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ItemBundleDataModel
    {
        public string CurrencySymbol { set; get; }
        //public ItemMasterModel ItemMasterModel { set; get; }
        public ItemModel ItemModel { set; get; }
        //public ParentItemBundleModel ParentItemBundleModel { set; get; }
        public long ParentItemId { set; get; }
        public List<ShoppingCartItemModel> ShoppingCartItemBundleModels { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
