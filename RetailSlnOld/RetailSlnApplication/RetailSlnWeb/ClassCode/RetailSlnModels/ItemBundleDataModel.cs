using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ItemBundleDataModel
    {
        public long ItemMasterId { set; get; }
        public ItemMasterModel ItemMasterModel { set; get; }
        public ItemBundleModel ItemBundleModel { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
