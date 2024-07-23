using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ItemAttributesModel
    {
        public long ItemId { set; get; }
        public long TabId { set; get; }
        public ItemModel ItemModel { set; get; }
        public List<string> ItemAttributesTabs { set; get; }
        public List<string> ItemAttributesViews { set; get; }
        public List<object> ItemAttributesDatas { set; get; }
        //public ItemInfoListModel ItemInfoListModel { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
