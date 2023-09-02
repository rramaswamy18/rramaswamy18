using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ItemAttribListModel
    {
        public string Dummy { set; get; } = "Dummy Data";

        public ItemModel ItemModel { set; get; }

        public List<ItemAttribModel> ItemAttribModels { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
