using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ItemInfoListModel
    {
        public long ItemId { set; get; }
        public ItemModel ItemModel { set; get; }
        public List<ItemInfoModel> ItemInfoModels { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
