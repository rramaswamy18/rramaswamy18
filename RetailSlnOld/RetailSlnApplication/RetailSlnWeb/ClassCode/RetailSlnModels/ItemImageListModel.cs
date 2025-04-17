using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ItemImageListModel
    {
        public string Dummy { set; get; } = "Dummy Data";

        public ItemModel ItemModel { set; get; }

        public List<ItemImageModel> ItemImageModels { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
