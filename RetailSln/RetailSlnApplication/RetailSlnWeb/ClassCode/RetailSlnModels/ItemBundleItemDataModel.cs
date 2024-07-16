using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ItemBundleItemDataModel
    {
        public long BundleItemId { set; get; }
        public string PrefixSeqNum { set; get; }
        public int PaddingLeft { set; get; }
        public ItemModel BundleItemModel { set; get; }
        public List<ItemBundleItemModel> ItemBundleItemModels { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
