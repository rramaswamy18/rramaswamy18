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
        public List<string> ItemAttribsTabs { set; get; }
        public ItemSpecListModel ItemSpecListModel { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
