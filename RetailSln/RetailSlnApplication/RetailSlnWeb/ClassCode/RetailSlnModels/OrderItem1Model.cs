using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class OrderItem1Model
    {
        public Dictionary<long, ItemDiscountModel> ItemDiscountModels { set; get; }
        public ItemMasterModel ItemMasterModel { set; get; }
        public ItemBundleDataModel ItemBundleDataModel { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
