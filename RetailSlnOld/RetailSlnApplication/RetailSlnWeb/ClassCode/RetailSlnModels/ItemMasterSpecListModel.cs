using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ItemMasterSpecListModel
    {
        public string Dummy { set; get; } = "Dummy Data";

        public ItemMasterModel ItemMasterModel { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
