using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ItemListModel
    {
        public List<ItemModel> ItemModels { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
