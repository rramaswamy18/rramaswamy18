using ArchitectureLibraryCacheData;
using ArchitectureLibraryDataLayer;
using ArchitectureLibraryDLL.ArchitectureLibraryEnumerations;
using ArchitectureLibraryEnumerations;
using ArchitectureLibraryModels;
using RetailSlnCacheBusinessLayer;
using RetailSlnEnumerations;
using RetailSlnModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RetailSlnCacheData
{
    public static class RetailSlnCache
    {
        public static List<DemogInfoAddressModel> BusinessDemogInfoAddressModels { set; get; }
        public static List<CategoryModel> CategoryModels { set; get; }
        public static CultureInfo CurrencyCultureInfo { set; get; }
        public static string CurrencyDecimalPlaces { set; get; }
        public static string CurrencySymbol { set; get; }
        public static Dictionary<long, CategoryLayoutModel> CategoryLayoutModels { set; get; }
        public static List<CorpAcctModel> CorpAcctModels { set; get; }
        public static List<DeliveryChargeModel> DeliveryChargeModels { set; get; }
        public static List<DeliveryListModel> DeliveryListModels { set; get; }
        public static List<DiscountDtlModel> DiscountDtlModels { set; get; }
        public static List<ItemModel> ItemModels { set; get; }
        public static List<ItemAttribMasterModel> ItemAttribMasterModels { set; get; }
        public static List<ItemAttribModel> ItemAttribModels { set; get; }
        public static List<ItemBundleItemModel> ItemBundleItemModels { set; get; }
        public static List<ItemBundleDiscountModel> ItemBundleDiscountModels { set; get; }
        public static List<CategoryItemHierModel> CategoryItemHierModels { set; get; }
        public static List<DemogInfoCountryModel> DeliveryDemogInfoCountryModels { set; get; }
        public static List<SelectListItem> DeliveryDemogInfoCountrySelectListItems { set; get; }
        public static long DefaultDeliveryDemogInfoCountryId { set; get; }
        public static void Initialize(long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            RetailSlnCacheBL retailSlnCacheBL = new RetailSlnCacheBL();
            retailSlnCacheBL.Initialize(out List<CategoryModel> categoryModels, out List<ItemModel> itemModels, out List<ItemAttribModel> itemAttribModels, out List<ItemAttribMasterModel> itemAttribMasterModels, out List<ItemBundleItemModel> itemBundleItemModels, out List<ItemBundleDiscountModel> itemBundleDiscountModels, out List<CategoryItemHierModel> categoryItemHierModels, out List<DeliveryListModel> deliveryListModels, out List<DeliveryChargeModel> deliveryChargeModels, out List<CorpAcctModel> corpAcctModels, out List<DiscountDtlModel> discountDtlModels, clientId, ipAddress, execUniqueId, loggedInUserId);
            CategoryModels = categoryModels;
            ItemBundleItemModels = ItemBundleItemModels;
            ItemModels = itemModels;
            ItemAttribModels = itemAttribModels;
            ItemAttribMasterModels = itemAttribMasterModels;
            CategoryItemHierModels = categoryItemHierModels;
            DeliveryListModels = deliveryListModels;
            DeliveryChargeModels = deliveryChargeModels;
            CorpAcctModels = corpAcctModels;
            DiscountDtlModels = discountDtlModels;
            CurrencyCultureInfo = new CultureInfo(ArchLibCache.GetApplicationDefault(clientId, "Currency", "CultureInfo"));
            CurrencyDecimalPlaces = ArchLibCache.GetApplicationDefault(clientId, "Currency", "CurrencyDecimalPlaces");
            var regionInfo = new RegionInfo(ArchLibCache.GetApplicationDefault(clientId, "Currency", "CultureInfo"));
            CurrencySymbol = regionInfo.CurrencySymbol;
            BuildCacheModels(categoryModels, out Dictionary<long, CategoryLayoutModel> categoryLayoutModels, itemModels, itemAttribModels, itemAttribMasterModels, categoryItemHierModels, deliveryListModels, deliveryChargeModels, corpAcctModels, discountDtlModels, out List<DemogInfoCountryModel> deliveryDemogInfoCountryModels, out List<SelectListItem> deliveryDemogInfoCountrySelectListItems, clientId, ipAddress, execUniqueId, loggedInUserId);
            DeliveryDemogInfoCountryModels = deliveryDemogInfoCountryModels;
            DeliveryDemogInfoCountrySelectListItems = deliveryDemogInfoCountrySelectListItems;
            DefaultDeliveryDemogInfoCountryId = long.Parse(ArchLibCache.GetApplicationDefault(clientId, "DeliveryInfo", "DefaultDemogInfoCountry"));
            CategoryLayoutModels = categoryLayoutModels;
        }
        private static void BuildCacheModels(List<CategoryModel> categoryModels, out Dictionary<long, CategoryLayoutModel> categoryLayoutModels, List<ItemModel> itemModels, List<ItemAttribModel> itemAttribModels, List<ItemAttribMasterModel> itemAttribMasterModels, List<CategoryItemHierModel> categoryItemHierModels, List<DeliveryListModel> deliveryListModels, List<DeliveryChargeModel> deliveryChargeModels, List<CorpAcctModel> corpAcctModels, List<DiscountDtlModel> discountDtlModels, out List<DemogInfoCountryModel> deliveryDemogInfoCountryModels, out List<SelectListItem> deliveryDemogInfoCountrySelectListItems, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            foreach (var categoryItemHierModel in categoryItemHierModels)
            {
                categoryItemHierModel.CategoryModel = categoryModels.FirstOrDefault(x => x.CategoryId == categoryItemHierModel.CategoryId);
                categoryItemHierModel.ItemModel = itemModels.FirstOrDefault(x => x.ItemId == categoryItemHierModel.ItemId);
                categoryItemHierModel.ParentCategoryModel = categoryModels.FirstOrDefault(x => x.CategoryId == categoryItemHierModel.ParentCategoryId);
            }
            categoryLayoutModels = new Dictionary<long, CategoryLayoutModel>();
            foreach (var categoryModel in categoryModels)
            {
                categoryLayoutModels[(long)categoryModel.CategoryId] = new CategoryLayoutModel();
            }
            foreach (var categoryLayoutModel in categoryLayoutModels)
            {
                categoryLayoutModel.Value.CategoryItemHierModels = new List<CategoryItemHierModel>();
                categoryLayoutModel.Value.CategoryItemHierModels.AddRange(categoryItemHierModels.FindAll(x => x.ParentCategoryId == categoryLayoutModel.Key).OrderBy(y => y.SeqNum).ToList());
            }
            foreach (var itemAttribModel in itemAttribModels)
            {
                itemAttribModel.ItemAttribMasterModel = itemAttribMasterModels.First(x => x.ItemAttribMasterId == itemAttribModel.ItemAttribMasterId);
            }
            //
            ItemAttribModel itemAttribModel1;
            foreach (var itemModel in itemModels)
            {
                itemModel.ItemRateFormatted = itemModel.ItemRate.Value.ToString(CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                itemModel.ImageTitle = itemModel.ItemShortDesc0 + " " + itemModel.ItemShortDesc1;
                itemModel.ImageTitle += string.IsNullOrWhiteSpace(itemModel.ItemShortDesc2) ? "" : " " + itemModel.ItemShortDesc2;
                itemModel.ImageTitle += string.IsNullOrWhiteSpace(itemModel.ItemShortDesc3) ? "" : " " + itemModel.ItemShortDesc3;
                itemModel.ItemShortDesc = itemModel.ImageTitle;
                if (itemModel.ItemStatusId == ItemStatusEnum.OutOfStock)
                {
                    itemModel.ImageTitle += " Sold Out";
                    if (string.IsNullOrEmpty(itemModel.ExpectedAvailability))
                    {
                        itemModel.ExpectedAvailability = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");
                    }
                    itemModel.ExpectedAvailabilityFormatted = DateTime.Parse(itemModel.ExpectedAvailability).ToString("MMM-dd");
                    itemModel.ImageTitle += " Expected to be available on " + itemModel.ExpectedAvailabilityFormatted;
                }
                itemModel.ItemAttributesForDisplay = new Dictionary<string, string>();
                itemModel.ItemAttribModels = itemAttribModels.FindAll(x => x.ItemId == itemModel.ItemId);

                itemAttribModel1 = itemAttribModels.FirstOrDefault(x => x.ItemId == itemModel.ItemId && x.ItemAttribMasterModel.AttribName == "HSNCode");
                itemModel.ItemAttributesForDisplay["HSNCode"] = itemAttribModel1.ItemAttribValue;

                itemAttribModel1 = itemAttribModels.FirstOrDefault(x => x.ItemId == itemModel.ItemId && x.ItemAttribMasterModel.AttribName == "ProductCode");
                itemModel.ItemAttributesForDisplay["ProductCode"] = itemAttribModel1.ItemAttribValue;

                itemAttribModel1 = itemAttribModels.FirstOrDefault(x => x.ItemId == itemModel.ItemId && x.ItemAttribMasterId == 16); //Show Weight
                if (itemAttribModel1 != null && itemAttribModel1.ItemAttribValue == "Yes")
                {
                    itemAttribModel1 = itemAttribModels.FirstOrDefault(x => x.ItemId == itemModel.ItemId && x.ItemAttribMasterId == 4); //Weight
                    itemModel.ItemAttributesForDisplay["Weight"] = "Weight " + itemAttribModel1.ItemAttribValue + " " + LookupCache.GetCodeDatasForCodeTypeNameDescByCodeDataNameId("WeightUnit", execUniqueId).First(a => a.CodeDataNameId == int.Parse(itemAttribModel1.ItemAttribUnitValue)).CodeDataDesc3;//(WeightUnitEnum)int.Parse(itemAttribModel1.ItemAttribUnitValue);
                }
                itemAttribModel1 = itemAttribModels.FirstOrDefault(x => x.ItemId == itemModel.ItemId && x.ItemAttribMasterId == 17); //Show Volume
                if (itemAttribModel1 != null && itemAttribModel1.ItemAttribValue == "Yes")
                {
                    itemAttribModel1 = itemAttribModels.FirstOrDefault(x => x.ItemId == itemModel.ItemId && x.ItemAttribMasterId == 7); //Fluid Volume
                    itemModel.ItemAttributesForDisplay["Volume"] = "Volume " + itemAttribModel1.ItemAttribValue + " " + LookupCache.GetCodeDatasForCodeTypeNameDescByCodeDataNameId("FluidVolumeUnit", execUniqueId).First(a => a.CodeDataNameId == int.Parse(itemAttribModel1.ItemAttribUnitValue)).CodeDataDesc3;//(WeightUnitEnum)int.Parse(itemAttribModel1.ItemAttribUnitValue);
                }
                itemAttribModel1 = itemAttribModels.FirstOrDefault(x => x.ItemId == itemModel.ItemId && x.ItemAttribMasterId == 18); //Show Count
                if (itemAttribModel1 != null && itemAttribModel1.ItemAttribValue == "Yes")
                {
                    itemAttribModel1 = itemAttribModels.FirstOrDefault(x => x.ItemId == itemModel.ItemId && x.ItemAttribMasterId == 12); //Count
                    itemModel.ItemAttributesForDisplay["Count"] = "Count " + itemAttribModel1.ItemAttribValue + " " + (CountEnum)int.Parse(itemAttribModel1.ItemAttribUnitValue);
                }
                itemAttribModel1 = itemAttribModels.FirstOrDefault(x => x.ItemId == itemModel.ItemId && x.ItemAttribMasterId == 22); //Show Color
                if (itemAttribModel1 != null && itemAttribModel1.ItemAttribValue == "Yes")
                {
                    itemAttribModel1 = itemAttribModels.FirstOrDefault(x => x.ItemId == itemModel.ItemId && x.ItemAttribMasterId == 8); //Color
                    itemModel.ItemAttributesForDisplay["Color"] = "Color " + itemAttribModel1.ItemAttribValue;
                }
                itemAttribModel1 = itemAttribModels.FirstOrDefault(x => x.ItemId == itemModel.ItemId && x.ItemAttribMasterId == 23); //Show Size
                if (itemAttribModel1 != null && itemAttribModel1.ItemAttribValue == "Yes")
                {
                    itemAttribModel1 = itemAttribModels.FirstOrDefault(x => x.ItemId == itemModel.ItemId && x.ItemAttribMasterId == 10); //Size
                    itemModel.ItemAttributesForDisplay["Size"] = "Size " + itemAttribModel1.ItemAttribValue;
                }
                itemAttribModel1 = itemAttribModels.FirstOrDefault(x => x.ItemId == itemModel.ItemId && x.ItemAttribMasterId == 25); //Show Weight Attribute
                if (itemAttribModel1 != null && itemAttribModel1.ItemAttribValue == "Yes")
                {
                    itemAttribModel1 = itemAttribModels.FirstOrDefault(x => x.ItemId == itemModel.ItemId && x.ItemAttribMasterId == 24); //Weight Attribute
                    itemModel.ItemAttributesForDisplay["WeightAttribute"] = "Weight " + itemAttribModel1.ItemAttribValue;
                }
            }
            //
            //ItemAttribModel itemAttribModel1;
            //foreach (var itemModel in itemModels)
            //{
            //    itemModel.ItemAttribModels = itemAttribModels.FindAll(x => x.ItemId == itemModel.ItemId);
            //    itemModel.ItemDescAttrib = "";
            //    itemAttribModel1 = itemAttribModels.FirstOrDefault(x => x.ItemId == itemModel.ItemId && x.ItemAttribMasterId == 16); //Show Weight
            //    if (itemAttribModel1 != null)
            //    {
            //        itemModel.ItemDescAttrib = itemAttribModel1.ItemAttribValue + " " + itemAttribModel1.ItemAttribUnitValue;
            //    }
            //    itemAttribModel1 = itemAttribModels.FirstOrDefault(x => x.ItemId == itemModel.ItemId && x.ItemAttribMasterId == 17); //Show Volume
            //    if (itemAttribModel1 != null)
            //    {
            //        itemModel.ItemDescAttrib = itemAttribModel1.ItemAttribValue + " " + itemAttribModel1.ItemAttribUnitValue;
            //    }
            //    itemAttribModel1 = itemAttribModels.FirstOrDefault(x => x.ItemId == itemModel.ItemId && x.ItemAttribMasterId == 18); //Show Count
            //    if (itemAttribModel1 != null)
            //    {
            //        itemModel.ItemDescAttrib = itemAttribModel1.ItemAttribValue + " " + itemAttribModel1.ItemAttribUnitValue;
            //    }
            //}
            //
            foreach (var deliveryListModel in deliveryListModels)
            {
                deliveryListModel.DeliveryChargeModels = new List<DeliveryChargeModel>();
                deliveryListModel.DeliveryChargeModels.AddRange(deliveryChargeModels.FindAll(x => x.DeliveryListId == deliveryListModel.DeliveryListId));
            }
            foreach (var corpAcctModel in corpAcctModels)
            {
                corpAcctModel.DiscountDtlModels = new List<DiscountDtlModel>();
                corpAcctModel.DiscountDtlModels.AddRange(discountDtlModels.FindAll(x => x.CorpAcctId == corpAcctModel.CorpAcctId));
            }
            DemogInfoCountryModel demogInfoCountryModel;
            deliveryDemogInfoCountryModels = new List<DemogInfoCountryModel>();
            deliveryDemogInfoCountrySelectListItems = new List<SelectListItem>();
            string deliveryInfoDemogInfoCountryIds = ArchLibCache.GetApplicationDefault(clientId, "DeliveryInfo", "DemogInfoCountryIds");
            foreach (var deliveryInfoDemogInfoCountryId in deliveryInfoDemogInfoCountryIds.Split(';'))
            {
                deliveryDemogInfoCountryModels.Add(demogInfoCountryModel = DemogInfoCache.DemogInfoCountryModels.First(x => x.DemogInfoCountryId == long.Parse(deliveryInfoDemogInfoCountryId)));
                deliveryDemogInfoCountrySelectListItems.Add
                (
                    new SelectListItem
                    {
                        Text = demogInfoCountryModel.CountryDesc + " " + demogInfoCountryModel.CountryAbbrev,
                        Value = demogInfoCountryModel.DemogInfoCountryId.ToString(),
                    }
                );
            }
            //Business Addresses
            //ArchLibDataContext.OpenSqlConnection();
            //DemogInfoAddressModel demogInfoAddressModel;
            //BusinessDemogInfoAddressModels = new List<DemogInfoAddressModel>();
            //demogInfoAddressModel = ArchLibDataContext.GetDemogInfoAddress(1, ArchLibDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
            //BusinessDemogInfoAddressModels.Add(demogInfoAddressModel);
            //demogInfoAddressModel = ArchLibDataContext.GetDemogInfoAddress(2, ArchLibDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
            //BusinessDemogInfoAddressModels.Add(demogInfoAddressModel);
        }
    }
}
