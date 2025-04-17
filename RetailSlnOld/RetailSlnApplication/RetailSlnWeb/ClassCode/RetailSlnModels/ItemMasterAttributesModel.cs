using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ItemMasterAttributesModel
    {
        public long ItemId { set; get; }
        public long TabId { set; get; }
        public ItemMasterModel ItemMasterModel { set; get; }
        public List<string> ItemMasterAttributesTabs { set; get; }
        public List<string> ItemMasterAttributesViews { set; get; }
        public List<object> ItemMasterAttributesDatas { set; get; }
        //public ItemInfoListModel ItemInfoListModel { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
