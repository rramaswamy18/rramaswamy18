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
        public ApiBusinessInfoModel BusinessInfoModel { set; get; }
        public List<DemogInfoAddressModel> BusinessDemogInfoAddressModels { set; get; }
        public List<CategoryModel> CategoryModels { set; get; }
        public CultureInfo CurrencyCultureInfo { set; get; }
        public string CurrencyDecimalPlaces { set; get; }
        public string CurrencySymbol { set; get; }
        //public Dictionary<long, CategoryLayoutModel> CategoryLayoutModels { set; get; }
        //public Dictionary<string, Dictionary<long, CategoryLayoutModel>> AspNetRoleCategoryLayoutModels { set; get; }
        public Dictionary<string, Dictionary<long, List<CategoryModel>>> AspNetRoleParentCategoryCategoryModels { set; get; }
        public Dictionary<string, Dictionary<long, List<CategoryItemMasterHierModel>>> AspNetRoleParentCategoryCategoryItemMasterHierModels { set; get; }
        public Dictionary<string, Dictionary<long, List<CategoryItemLayoutModel>>> AspNetRoleCategoryItemLayoutModels { set; get; }
        public List<CorpAcctModel> CorpAcctModels { set; get; }
        public List<CorpAcctLocationModel> CorpAcctLocationModels { set; get; }
        public List<DiscountDtlModel> DiscountDtlModels { set; get; }
        public List<FestivalListModel> FestivalListModels { set; get; }
        public List<FestivalListImageModel> FestivalListImageModels { set; get; }
        public List<ItemDiscountModel> ItemDiscountModels { set; get; }
        public List<ItemMasterModel> ItemMasterModels { set; get; }
        public List<ItemModel> ItemModels { set; get; }
        public List<ItemMasterItemSpecModel> ItemMasterItemSpecModels { set; get; }
        public List<ItemSpecModel> ItemSpecModels { set; get; }
        public List<ItemSpecMasterModel> ItemSpecMasterModels { set; get; }
        public List<ItemInfoModel> ItemInfoModels { set; get; }
        public List<ItemImageModel> ItemImageModels { set; get; }
        public List<ItemBundleModel> ItemBundleModels { set; get; }
        public List<ItemBundleItemModel> ItemBundleItemModels { set; get; }
        public List<ItemBundleDiscountModel> ItemBundleDiscountModels { set; get; }
        public List<CategoryItemHierModel> CategoryItemHierModels { set; get; }
        public List<CategoryHierModel> CategoryHierModels { set; get; }
        //public List<CategoryItemHierModel> CategoryItemHierModelsNew { set; get; }
        public List<CategoryItemMasterHierModel> CategoryItemMasterHierModels { set; get; }
        public List<DemogInfoCountryModel> DeliveryDemogInfoCountryModels { set; get; }
        public List<SelectListItem> DeliveryDemogInfoCountrySelectListItems { set; get; }
        public Dictionary<long, List<SelectListItem>> DeliveryDemogInfoCountrySubDivisionSelectListItems { set; get; }
        public List<ApiCodeDataModel> DeliveryMethods { set; get; }
        public List<ApiCodeDataModel> PaymentMethodsCreditSale { set; get; }
        public List<ApiCodeDataModel> PaymentMethods { set; get; }
        public long DefaultDeliveryDemogInfoCountryId { set; get; }
        public List<KeyValuePair<long, string>> DeliveryDemogInfoCountrys { set; get; }
        public List<KeyValuePair<long, List<KeyValuePair<long, string>>>> DeliveryDemogInfoCountryStates { set; get; }
        public List<KeyValuePair<long, string>> DeliveryCountrys { set; get; }
        public List<KeyValuePair<long, List<KeyValuePair<long, string>>>> DeliveryCountryStates { set; get; }
        public Dictionary<long, List<SelectListItem>> DeliveryDemogInfoSubDivisionSelectListItems { set; get; }
        public List<PickupLocationModel> PickupLocationModels { set; get; }
    }
}
