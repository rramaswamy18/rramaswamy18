using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RetailSlnModels
{
    public class RetailSlnInitModel
    {
        public List<CategoryModel> CategoryModels { set; get; }
        public List<CategoryItemMasterHierModel> CategoryItemMasterHierModels { set; get; }
        public List<CorpAcctModel> CorpAcctModels { set; get; }
        public List<CorpAcctLocationModel> CorpAcctLocationModels { set; get; }
        public List<DeliveryMethodFilterModel> DeliveryMethodFilterModels { set; get; }
        public List<ItemBundleModel> ItemBundleModels { set; get; }
        public List<ItemDiscountModel> ItemDiscountModels { set; get; }
        public List<ItemMasterModel> ItemMasterModels { set; get; }
        public List<ItemModel> ItemModels { set; get; }
        public List<ItemMasterItemSpecModel> ItemMasterItemSpecModels { set; get; }
        public List<ItemItemSpecModel> ItemItemSpecModels { set; get; }
        public List<ItemSpecMasterModel> ItemSpecMasterModels { set; get; }
        public List<PaymentModeFilterModel> PaymentModeFilterModels { set; get; }
        public List<PickupLocationModel> PickupLocationModels { set; get; }
    }
}
