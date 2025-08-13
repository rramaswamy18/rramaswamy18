using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ItemBundleDataModel
    {
        public string CurrencySymbol { set; get; }
        public ParentItemBundleModel ParentItemBundleModel { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
