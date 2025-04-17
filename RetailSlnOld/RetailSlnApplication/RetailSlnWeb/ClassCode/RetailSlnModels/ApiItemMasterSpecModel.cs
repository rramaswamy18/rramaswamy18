using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ApiItemMasterSpecModel
    {
        public ItemMasterModel ItemMasterModel { set; get; }
        public List<ItemSpecModel> ItemSpecModels { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
