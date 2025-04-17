using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ItemBundleItemListModel
    {
        public ItemModel ItemModel { set; get; }

        public List<ItemBundleItemModel> ItemBundleItemModels { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
