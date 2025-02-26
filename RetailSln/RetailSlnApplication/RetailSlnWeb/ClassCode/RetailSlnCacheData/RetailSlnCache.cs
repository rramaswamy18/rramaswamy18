using ArchitectureLibraryCacheData;
using ArchitectureLibraryDataLayer;
using ArchitectureLibraryEnumerations;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using RetailSlnCacheBusinessLayer;
using RetailSlnEnumerations;
using RetailSlnModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace RetailSlnCacheData
{
    public static class RetailSlnCache
    {
        #region Properties
        public static ApiBusinessInfoModel ApiBusinessInfoModel { set; get; }
        public static List<DemogInfoAddressModel> BusinessDemogInfoAddressModels { set; get; }
        public static List<CategoryModel> CategoryModels { set; get; }
        public static List<CategoryHierModel> CategoryHierModels { set; get; }
        public static List<CategoryItemHierModel> CategoryItemHierModels { set; get; }
        public static List<CategoryItemMasterHierModel> CategoryItemMasterHierModels { set; get; }
        public static CultureInfo CurrencyCultureInfo { set; get; }
        public static string CurrencyDecimalPlaces { set; get; }
        public static string CurrencySymbol { set; get; }
        //public static Dictionary<long, CategoryLayoutModel> CategoryLayoutModels { set; get; }
        public static Dictionary<string, Dictionary<long, List<CategoryModel>>> AspNetRoleParentCategoryCategoryModels { set; get; }
        public static Dictionary<string, Dictionary<long, List<CategoryItemMasterHierModel>>> AspNetRoleParentCategoryCategoryItemMasterHierModels { set; get; }
        public static Dictionary<string, Dictionary<long, List<CategoryItemLayoutModel>>> AspNetRoleCategoryItemLayoutModels { set; get; }
        //public static Dictionary<string, Dictionary<long, CategoryLayoutModel>> AspNetRoleCategoryLayoutModels { set; get; }
        public static List<CorpAcctModel> CorpAcctModels { set; get; }
        public static List<CorpAcctLocationModel> CorpAcctLocationModels { set; get; }
        //public static List<DiscountDtlModel> DiscountDtlModels { set; get; }
        public static List<FestivalListModel> FestivalListModels { set; get; }
        public static List<FestivalListImageModel> FestivalListImageModels { set; get; }
        //public static List<ItemDiscountModel> ItemDiscountModels { set; get; }
        public static List<ItemMasterModel> ItemMasterModels { set; get; }
        public static List<ItemModel> ItemModels { set; get; }
        public static List<ItemSpecMasterModel> ItemSpecMasterModels { set; get; }
        public static List<ItemSpecModel> ItemSpecModels { set; get; }
        //public static List<ItemItemSpecModel> ItemItemSpecModels { set; get; }
        public static List<ItemMasterItemSpecModel> ItemMasterItemSpecModels { set; get; }
        public static List<ItemInfoModel> ItemInfoModels { set; get; }
        public static List<ItemImageModel> ItemImageModels { set; get; }
        public static List<ItemBundleModel> ItemBundleModels { set; get; }
        public static List<ItemBundleItemModel> ItemBundleItemModels { set; get; }
        public static List<ItemBundleDiscountModel> ItemBundleDiscountModels { set; get; }
        public static List<DemogInfoCountryModel> DeliveryDemogInfoCountryModels { set; get; }
        public static List<SelectListItem> DeliveryDemogInfoCountrySelectListItems { set; get; }
        public static Dictionary<long, List<SelectListItem>> DeliveryDemogInfoCountrySubDivisionSelectListItems { set; get; }
        public static List<ApiCodeDataModel> DeliveryMethods { set; get; }
        public static List<ApiCodeDataModel> PaymentMethodsCreditSale { set; get; }
        public static List<ApiCodeDataModel> PaymentMethods { set; get; }
        public static long DefaultDeliveryDemogInfoCountryId { set; get; }
        public static List<KeyValuePair<long, string>> DeliveryDemogInfoCountrys { set; get; }
        public static List<KeyValuePair<long, List<KeyValuePair<long, string>>>> DeliveryDemogInfoCountryStates { set; get; }
        public static List<PickupLocationModel> PickupLocationModels { set; get; }
        //public static List<SelectListItem> PickupLocationModelSelectListItems { set; get; }
        public static List<SelectListItem> DeliveryMethodSelectListItems { set; get; }
        public static List<DemogInfoAddressModel> PickupLocationDemogInfoAddressModels1 { set; get; }
        public static List<DemogInfoAddressModel> PickupLocationDemogInfoAddressModels2 { set; get; }
        #endregion
        public static void Initialize(long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            RetailSlnCacheBL retailSlnCacheBL = new RetailSlnCacheBL();
            retailSlnCacheBL.Initialize(out RetailSlnInitModel retailSlnInitModel, clientId, ipAddress, execUniqueId, loggedInUserId);
            CategoryModels = retailSlnInitModel.CategoryModels;
            CategoryHierModels = retailSlnInitModel.CategoryHierModels;
            ItemBundleModels = retailSlnInitModel.ItemBundleModels;
            ItemBundleItemModels = retailSlnInitModel.ItemBundleItemModels;
            //ItemDiscountModels = retailSlnInitModel.ItemDiscountModels;
            ItemMasterModels = retailSlnInitModel.ItemMasterModels;
            ItemModels = retailSlnInitModel.ItemModels;
            ItemSpecModels = retailSlnInitModel.ItemSpecModels;
            ItemMasterItemSpecModels = retailSlnInitModel.ItemMasterItemSpecModels;
            ItemInfoModels = retailSlnInitModel.ItemInfoModels;
            ItemImageModels = retailSlnInitModel.ItemImageModels;
            ItemSpecMasterModels = retailSlnInitModel.ItemSpecMasterModels;
            CategoryItemHierModels = retailSlnInitModel.CategoryItemHierModels;
            CategoryItemMasterHierModels = retailSlnInitModel.CategoryItemMasterHierModels;
            //DeliveryListModels = deliveryListModels;
            //DeliveryChargeModels = deliveryChargeModels;
            CorpAcctModels = retailSlnInitModel.CorpAcctModels;
            CorpAcctLocationModels = retailSlnInitModel.CorpAcctLocationModels;
            //DiscountDtlModels = retailSlnInitModel.DiscountDtlModels;
            FestivalListModels = retailSlnInitModel.FestivalListModels;
            FestivalListImageModels = retailSlnInitModel.FestivalListImageModels;
            CurrencyCultureInfo = new CultureInfo(ArchLibCache.GetApplicationDefault(clientId, "Currency", "CultureInfo"));
            CurrencyDecimalPlaces = ArchLibCache.GetApplicationDefault(clientId, "Currency", "CurrencyDecimalPlaces");
            var regionInfo = new RegionInfo(ArchLibCache.GetApplicationDefault(clientId, "Currency", "CultureInfo"));
            CurrencySymbol = regionInfo.CurrencySymbol;
            BuildCacheModels(retailSlnInitModel, clientId, ipAddress, execUniqueId, loggedInUserId);
            DeliveryDemogInfoCountryModels = retailSlnInitModel.DeliveryDemogInfoCountryModels;
            DeliveryDemogInfoCountrySelectListItems = retailSlnInitModel.DeliveryDemogInfoCountrySelectListItems;
            DeliveryDemogInfoCountrySubDivisionSelectListItems = retailSlnInitModel.DeliveryDemogInfoSubDivisionSelectListItems;
            DefaultDeliveryDemogInfoCountryId = long.Parse(ArchLibCache.GetApplicationDefault(clientId, "DeliveryInfo", "DefaultDemogInfoCountry"));
            //CategoryLayoutModels = retailSlnInitModel.CategoryLayoutModels;
            AspNetRoleParentCategoryCategoryModels = retailSlnInitModel.AspNetRoleParentCategoryCategoryModels;
            AspNetRoleParentCategoryCategoryItemMasterHierModels = retailSlnInitModel.AspNetRoleParentCategoryCategoryItemMasterHierModels;
            AspNetRoleCategoryItemLayoutModels = retailSlnInitModel.AspNetRoleCategoryItemLayoutModels;
            DeliveryMethods = retailSlnInitModel.DeliveryMethods;
            PaymentMethodsCreditSale = retailSlnInitModel.PaymentMethodsCreditSale;
            PaymentMethods = retailSlnInitModel.PaymentMethods;
            DeliveryDemogInfoCountrys = retailSlnInitModel.DeliveryCountrys;
            DeliveryDemogInfoCountryStates = retailSlnInitModel.DeliveryCountryStates;
            ApiBusinessInfoModel = retailSlnInitModel.BusinessInfoModel;
            PickupLocationModels = retailSlnInitModel.PickupLocationModels;
        }
        private static void BuildCacheModels(RetailSlnInitModel retailSlnInitModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            BuildCacheModels1(retailSlnInitModel, clientId, ipAddress, execUniqueId, loggedInUserId);
            BuildCacheModels2(retailSlnInitModel, clientId, ipAddress, execUniqueId, loggedInUserId);
            BuildCacheModels3(retailSlnInitModel, clientId, ipAddress, execUniqueId, loggedInUserId);
            BuildCacheModels4(retailSlnInitModel, clientId, ipAddress, execUniqueId, loggedInUserId);
            BuildCacheModels5(retailSlnInitModel, clientId, ipAddress, execUniqueId, loggedInUserId);
        }
        private static void BuildCacheModels1(RetailSlnInitModel retailSlnInitModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            int i = 0;
            string aspNetRoleName;
            long parentCategoryId;
            foreach (var categoryHierModel in retailSlnInitModel.CategoryHierModels)
            {
                categoryHierModel.CategoryModel = retailSlnInitModel.CategoryModels.First(x => x.CategoryId == categoryHierModel.CategoryId);
                categoryHierModel.ParentCategoryModel = retailSlnInitModel.CategoryModels.First(x => x.CategoryId == categoryHierModel.ParentCategoryId);
            }
            foreach (var categoryItemHierModel in retailSlnInitModel.CategoryItemHierModels)
            {
                categoryItemHierModel.ItemModel = retailSlnInitModel.ItemModels.First(x => x.ItemId == categoryItemHierModel.ItemId);
            }
            foreach (var categoryItemMasterHierModel in retailSlnInitModel.CategoryItemMasterHierModels)
            {
                categoryItemMasterHierModel.ItemMasterModel = retailSlnInitModel.ItemMasterModels.First(x => x.ItemMasterId == categoryItemMasterHierModel.ItemMasterId);
                categoryItemMasterHierModel.ParentCategoryModel = retailSlnInitModel.CategoryModels.First(x => x.CategoryId == categoryItemMasterHierModel.ParentCategoryId);
                categoryItemMasterHierModel.CategoryItemHierModels = retailSlnInitModel.CategoryItemHierModels.FindAll(x => x.CategoryItemMasterHierId == categoryItemMasterHierModel.CategoryItemMasterHierId);
            }
            retailSlnInitModel.AspNetRoleParentCategoryCategoryModels = new Dictionary<string, Dictionary<long, List<CategoryModel>>>();
            while (i < retailSlnInitModel.CategoryHierModels.Count)
            {
                aspNetRoleName = retailSlnInitModel.CategoryHierModels[i].AspNetRoleName;
                retailSlnInitModel.AspNetRoleParentCategoryCategoryModels[aspNetRoleName] = new Dictionary<long, List<CategoryModel>>();
                while (i < retailSlnInitModel.CategoryHierModels.Count &&
                       retailSlnInitModel.CategoryHierModels[i].AspNetRoleName == aspNetRoleName
                      )
                {
                    parentCategoryId = retailSlnInitModel.CategoryHierModels[i].ParentCategoryId;
                    retailSlnInitModel.AspNetRoleParentCategoryCategoryModels[aspNetRoleName][parentCategoryId] = new List<CategoryModel>();
                    while (i < retailSlnInitModel.CategoryHierModels.Count &&
                           retailSlnInitModel.CategoryHierModels[i].AspNetRoleName == aspNetRoleName &&
                           retailSlnInitModel.CategoryHierModels[i].ParentCategoryId == parentCategoryId
                          )
                    {
                        retailSlnInitModel.AspNetRoleParentCategoryCategoryModels[aspNetRoleName][parentCategoryId].Add(CategoryModels.First(x => x.CategoryId == retailSlnInitModel.CategoryHierModels[i].CategoryId));
                        i++;
                    }
                }
            }
            i = 0;
            retailSlnInitModel.AspNetRoleParentCategoryCategoryItemMasterHierModels = new Dictionary<string, Dictionary<long, List<CategoryItemMasterHierModel>>>();
            while (i < retailSlnInitModel.CategoryItemMasterHierModels.Count)
            {
                aspNetRoleName = retailSlnInitModel.CategoryItemMasterHierModels[i].AspNetRoleName;
                retailSlnInitModel.AspNetRoleParentCategoryCategoryItemMasterHierModels[aspNetRoleName] = new Dictionary<long, List<CategoryItemMasterHierModel>>();
                while (i < retailSlnInitModel.CategoryItemMasterHierModels.Count &&
                       retailSlnInitModel.CategoryItemMasterHierModels[i].AspNetRoleName == aspNetRoleName
                      )
                {
                    parentCategoryId = retailSlnInitModel.CategoryItemMasterHierModels[i].ParentCategoryId;
                    retailSlnInitModel.AspNetRoleParentCategoryCategoryItemMasterHierModels[aspNetRoleName][parentCategoryId] = new List<CategoryItemMasterHierModel>();
                    while (i < retailSlnInitModel.CategoryItemMasterHierModels.Count &&
                           retailSlnInitModel.CategoryItemMasterHierModels[i].AspNetRoleName == aspNetRoleName &&
                           retailSlnInitModel.CategoryItemMasterHierModels[i].ParentCategoryId == parentCategoryId
                          )
                    {
                        retailSlnInitModel.AspNetRoleParentCategoryCategoryItemMasterHierModels[aspNetRoleName][parentCategoryId].Add(retailSlnInitModel.CategoryItemMasterHierModels[i]);
                        i++;
                    }
                }
            }
        }
        private static void BuildCacheModels2(RetailSlnInitModel retailSlnInitModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<CodeDataModel> abc1;
            CodeDataModel abc2;
            foreach (var itemSpecModel in retailSlnInitModel.ItemSpecModels)
            {
                itemSpecModel.ItemSpecMasterModel = retailSlnInitModel.ItemSpecMasterModels.First(x => x.ItemSpecMasterId == itemSpecModel.ItemSpecMasterId);
                itemSpecModel.ItemSpecValueForDisplay = /*itemSpecModel.ItemSpecMasterModel.SpecDesc + ": " +*/ itemSpecModel.ItemSpecValue;
                if (itemSpecModel.ItemSpecValue != "")
                {
                    if (itemSpecModel.ItemSpecMasterModel.CodeTypeId != null)
                    {
                        try
                        {
                            abc1 = LookupCache.GetCodeDatasForCodeTypeIdByCodeDataNameId(itemSpecModel.ItemSpecMasterModel.CodeTypeId.Value, execUniqueId);
                            abc2 = abc1.First(x => x.CodeDataNameId == long.Parse(itemSpecModel.ItemSpecUnitValue));
                            itemSpecModel.ItemSpecValueForDisplay += " " + abc2.CodeDataDesc0;
                        }
                        catch (Exception exception)
                        {
                            exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception, "ItemId", itemSpecModel.ItemId.ToString(), "ItemSpecId", itemSpecModel.ItemSpecId.ToString());
                        }
                    }
                }
            }
            int i = 0, itemSeqNum;
            long itemMasterIdTemp;
            ItemMasterModel itemMasterModelTemp;
            while (i < retailSlnInitModel.ItemMasterItemSpecModels.Count)
            {
                itemMasterIdTemp = retailSlnInitModel.ItemMasterItemSpecModels[i].ItemMasterId;
                retailSlnInitModel.ItemMasterItemSpecModels[i].ItemMasterModel = retailSlnInitModel.ItemMasterModels.First(x => x.ItemMasterId == itemMasterIdTemp);
                itemMasterModelTemp = retailSlnInitModel.ItemMasterItemSpecModels[i].ItemMasterModel;
                itemMasterModelTemp.ItemMasterItemSpecModels = new List<ItemSpecModel>();
                while (i < retailSlnInitModel.ItemMasterItemSpecModels.Count && itemMasterIdTemp == retailSlnInitModel.ItemMasterItemSpecModels[i].ItemMasterId)
                {
                    if (retailSlnInitModel.ItemMasterItemSpecModels[i].ItemSpecModels == null)
                    {
                        retailSlnInitModel.ItemMasterItemSpecModels[i].ItemSpecModels = new List<ItemSpecModel>();
                    }
                    if (retailSlnInitModel.ItemMasterItemSpecModels[i].ItemSpecId != 0)
                    {
                        retailSlnInitModel.ItemMasterItemSpecModels[i].ItemSpecModels.Add(retailSlnInitModel.ItemSpecModels.First(x => x.ItemSpecId == retailSlnInitModel.ItemMasterItemSpecModels[i].ItemSpecId));
                    }
                    itemMasterModelTemp.ItemMasterItemSpecModels.Add
                    (
                        retailSlnInitModel.ItemSpecModels.First
                        (
                            x => x.ItemSpecId == retailSlnInitModel.ItemMasterItemSpecModels[i].ItemSpecId
                        )
                    );
                    i++;
                }
            }
            itemSeqNum = 0;
            foreach (var itemMasterModel in retailSlnInitModel.ItemMasterModels)
            {
                itemSeqNum++;
                itemMasterModel.ItemModels = retailSlnInitModel.ItemModels.FindAll(x => x.ItemMasterId == itemMasterModel.ItemMasterId);
                itemMasterModel.ImageTitle = itemMasterModel.ItemMasterDesc + " #" + itemMasterModel.ItemMasterId + " " + itemSeqNum + "/" + retailSlnInitModel.ItemMasterModels.Count;
                if (itemMasterModel.ItemMasterItemSpecModels == null)
                {
                    itemMasterModel.ItemMasterItemSpecModels = new List<ItemSpecModel>();
                }
                if (itemMasterModel.ItemMasterItemSpecModelsForDisplay == null)
                {
                    itemMasterModel.ItemMasterItemSpecModelsForDisplay = new Dictionary<string, ItemSpecModel>();
                }
            }
            itemSeqNum = 0;
            foreach (var itemModel in retailSlnInitModel.ItemModels)
            {
                itemSeqNum++;
                itemModel.ItemMasterModel = retailSlnInitModel.ItemMasterModels.First(x => x.ItemMasterId == itemModel.ItemMasterId);
                itemModel.ItemRateFormatted = itemModel.ItemRate.Value.ToString(CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                itemModel.ImageTitle = itemModel.ItemShortDesc + " #" + itemModel.ItemId + " " + itemSeqNum + "/" + retailSlnInitModel.ItemModels.Count;
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
                itemModel.ItemSpecModels = retailSlnInitModel.ItemSpecModels.FindAll(x => x.ItemId == itemModel.ItemId);
                itemModel.ItemItemSpecModels = retailSlnInitModel.ItemSpecModels.FindAll(x => x.ItemId == itemModel.ItemId && x.SeqNumItem != null).OrderBy(y => y.SeqNumItem).ToList();
                itemModel.ItemInfoModels = retailSlnInitModel.ItemInfoModels.FindAll(x => x.ItemId == itemModel.ItemId);
                itemModel.ItemImageModels = retailSlnInitModel.ItemImageModels.FindAll(x => x.ItemId == itemModel.ItemId);
                itemModel.ItemSpecModelsForDisplay = new Dictionary<string, ItemSpecModel>();
                foreach (var itemSpecModel in itemModel.ItemSpecModels)
                {
                    itemModel.ItemSpecModelsForDisplay[itemSpecModel.ItemSpecMasterModel.SpecName] = itemSpecModel;
                }
                itemModel.ItemDiscountModels = new List<ItemDiscountModel>();
                //itemModel.ItemDiscountModels.AddRange(ItemDiscountModels.FindAll(x => x.ItemId == itemModel.ItemId));
            }
            int itemModelCount;
            string prefixString;
            foreach (var itemMasterModel in retailSlnInitModel.ItemMasterModels)
            {
                itemMasterModel.ItemRatesForDisplay = "";
                itemMasterModel.ItemRatesForDisplayAll = "";
                if (itemMasterModel.ItemModels.Count > 1)
                {
                    itemMasterModel.ItemRatesForDisplay = itemMasterModel.ItemModels[0].ItemRateFormatted + "...";
                    prefixString = "";
                    itemModelCount = 0;
                    foreach (var itemModel in itemMasterModel.ItemModels)
                    {
                        itemMasterModel.ItemRatesForDisplayAll += prefixString + itemModel.ItemRateFormatted;
                        itemModelCount++;
                        prefixString = "; ";
                    }
                    itemMasterModel.ItemRatesForDisplay += "...";
                    itemMasterModel.ItemRatesForDisplayAll += "...";
                }
            }
            foreach (var itemMasterModel in retailSlnInitModel.ItemMasterModels)
            {
                itemMasterModel.ItemMasterSpecsForDisplay = "";
                prefixString = "";
                foreach (var itemMasterItemSpecModel in itemMasterModel.ItemMasterItemSpecModels)
                {
                    //itemMasterModel.ItemMasterSpecsForDisplay += prefixString + ++index1 + ". " + itemMasterItemSpecModel.ItemSpecValueForDisplay;
                    itemMasterModel.ItemMasterSpecsForDisplay += prefixString + itemMasterItemSpecModel.ItemSpecValueForDisplay;
                    prefixString = " | ";
                }
                foreach (var itemModel in itemMasterModel.ItemModels)
                {
                    itemModel.ItemItemSpecsForDisplay = itemMasterModel.ItemMasterSpecsForDisplay;
                    foreach (var itemItemSpecModel in itemModel.ItemItemSpecModels)
                    {
                        itemModel.ItemItemSpecsForDisplay += prefixString + itemItemSpecModel.ItemSpecValueForDisplay;
                        prefixString = " | ";
                    }
                }
            }
            return;
        }
        private static void BuildCacheModels3(RetailSlnInitModel retailSlnInitModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            List<SelectListItem> deliveryDemogInfoSubDivisionSelectListItemsTemp;
            foreach (var corpAcctModel in retailSlnInitModel.CorpAcctModels)
            {
                corpAcctModel.CorpAcctLocationModels = new List<CorpAcctLocationModel>();
                corpAcctModel.CorpAcctLocationModels.AddRange(retailSlnInitModel.CorpAcctLocationModels.FindAll(x => x.CorpAcctId == corpAcctModel.CorpAcctId));
                corpAcctModel.DiscountDtlModels = new List<DiscountDtlModel>();
                corpAcctModel.DiscountDtlModels.AddRange(retailSlnInitModel.DiscountDtlModels.FindAll(x => x.CorpAcctId == corpAcctModel.CorpAcctId));
            }
            DemogInfoCountryModel demogInfoCountryModel;
            retailSlnInitModel.DeliveryDemogInfoCountryModels = new List<DemogInfoCountryModel>();
            retailSlnInitModel.DeliveryDemogInfoCountrySelectListItems = new List<SelectListItem>();
            retailSlnInitModel.DeliveryDemogInfoCountrySubDivisionSelectListItems = new Dictionary<long, List<SelectListItem>>();
            string deliveryInfoDemogInfoCountryIds = ArchLibCache.GetApplicationDefault(clientId, "DeliveryInfo", "DemogInfoCountryIds");
            foreach (var deliveryInfoDemogInfoCountryId in deliveryInfoDemogInfoCountryIds.Split(';'))
            {
                retailSlnInitModel.DeliveryDemogInfoCountryModels.Add(demogInfoCountryModel = DemogInfoCache.DemogInfoCountryModels.First(x => x.DemogInfoCountryId == long.Parse(deliveryInfoDemogInfoCountryId)));
                retailSlnInitModel.DeliveryDemogInfoCountrySubDivisionSelectListItems[demogInfoCountryModel.DemogInfoCountryId] = (deliveryDemogInfoSubDivisionSelectListItemsTemp = new List<SelectListItem>());
                retailSlnInitModel.DeliveryDemogInfoCountrySelectListItems.Add
                (
                    new SelectListItem
                    {
                        Text = demogInfoCountryModel.CountryDesc + " " + demogInfoCountryModel.CountryAbbrev,
                        Value = demogInfoCountryModel.DemogInfoCountryId.ToString(),
                    }
                );
            }
            //TempFunction(retailSlnInitModel.CorpAcctModels, retailSlnInitModel.CorpAcctLocationModels);
            return;
        }
        private static void BuildCacheModels4(RetailSlnInitModel retailSlnInitModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            retailSlnInitModel.DeliveryMethods = new List<ApiCodeDataModel>();
            var codeDataModels = LookupCache.CodeTypeModels.First(x => x.CodeTypeNameDesc == "DeliveryMethod").CodeDataModelsCodeDataNameId;
            foreach (var codeDataModel in codeDataModels)
            {
                retailSlnInitModel.DeliveryMethods.Add
                (
                    new ApiCodeDataModel
                    {
                        CodeDataId = codeDataModel.CodeDataId,
                        CodeTypeId = codeDataModel.CodeTypeId,
                        CodeTypeNameId = codeDataModel.CodeTypeNameId,
                        CodeDataNameId = codeDataModel.CodeDataNameId,
                        CodeDataNameDesc = codeDataModel.CodeDataNameDesc,
                        CodeDataDesc0 = codeDataModel.CodeDataDesc0,
                        CodeDataDesc1 = codeDataModel.CodeDataDesc1,
                        CodeDataDesc2 = codeDataModel.CodeDataDesc2,
                        CodeDataDesc3 = codeDataModel.CodeDataDesc3,
                        CodeDataDesc4 = codeDataModel.CodeDataDesc4,
                        CodeTypeModel = null,
                    }
                );
            }
            codeDataModels = LookupCache.CodeTypeModels.First(x => x.CodeTypeNameDesc == "PaymentMode").CodeDataModelsCodeDataNameId;
            retailSlnInitModel.PaymentMethodsCreditSale = new List<ApiCodeDataModel>();
            int i;
            for (i = 0; i < 1; i++)
            {
                retailSlnInitModel.PaymentMethodsCreditSale.Add
                (
                    new ApiCodeDataModel
                    {
                        CodeDataId = codeDataModels[i].CodeDataId,
                        CodeTypeId = codeDataModels[i].CodeTypeId,
                        CodeTypeNameId = codeDataModels[i].CodeTypeNameId,
                        CodeDataNameId = codeDataModels[i].CodeDataNameId,
                        CodeDataNameDesc = codeDataModels[i].CodeDataNameDesc,
                        CodeDataDesc0 = codeDataModels[i].CodeDataDesc0,
                        CodeDataDesc1 = codeDataModels[i].CodeDataDesc1,
                        CodeDataDesc2 = codeDataModels[i].CodeDataDesc2,
                        CodeDataDesc3 = codeDataModels[i].CodeDataDesc3,
                        CodeDataDesc4 = codeDataModels[i].CodeDataDesc4,
                        CodeTypeModel = null,
                    }
                );
            }
            retailSlnInitModel.PaymentMethods = new List<ApiCodeDataModel>();
            for (i = 1; i < codeDataModels.Count; i++)
            {
                retailSlnInitModel.PaymentMethods.Add
                (
                    new ApiCodeDataModel
                    {
                        CodeDataId = codeDataModels[i].CodeDataId,
                        CodeTypeId = codeDataModels[i].CodeTypeId,
                        CodeTypeNameId = codeDataModels[i].CodeTypeNameId,
                        CodeDataNameId = codeDataModels[i].CodeDataNameId,
                        CodeDataNameDesc = codeDataModels[i].CodeDataNameDesc,
                        CodeDataDesc0 = codeDataModels[i].CodeDataDesc0,
                        CodeDataDesc1 = codeDataModels[i].CodeDataDesc1,
                        CodeDataDesc2 = codeDataModels[i].CodeDataDesc2,
                        CodeDataDesc3 = codeDataModels[i].CodeDataDesc3,
                        CodeDataDesc4 = codeDataModels[i].CodeDataDesc4,
                        CodeTypeModel = null,
                    }
                );
            }
            retailSlnInitModel.DeliveryCountrys = new List<KeyValuePair<long, string>>();
            retailSlnInitModel.DeliveryCountryStates = new List<KeyValuePair<long, List<KeyValuePair<long, string>>>>();
            List<KeyValuePair<long, string>> deliveryStates;
            foreach (var deliveryDemogInfoCountryModel in retailSlnInitModel.DeliveryDemogInfoCountryModels)
            {
                retailSlnInitModel.DeliveryCountrys.Add(new KeyValuePair<long, string>(deliveryDemogInfoCountryModel.DemogInfoCountryId, deliveryDemogInfoCountryModel.CountryDesc));
                deliveryStates = new List<KeyValuePair<long, string>>();
                retailSlnInitModel.DeliveryCountryStates.Add(new KeyValuePair<long, List<KeyValuePair<long, string>>>(deliveryDemogInfoCountryModel.DemogInfoCountryId, deliveryStates));
                var demogInfoSubDivisionModels = DemogInfoCache.DemogInfoSubDivisionModels.FindAll(x => x.DemogInfoCountryId == deliveryDemogInfoCountryModel.DemogInfoCountryId);
                foreach (var demogInfoSubDivisionModel in demogInfoSubDivisionModels)
                {
                    deliveryStates.Add(new KeyValuePair<long, string>(deliveryDemogInfoCountryModel.DemogInfoCountryId, demogInfoSubDivisionModel.SubDivisionDesc));
                }
            }
            retailSlnInitModel.BusinessInfoModel = new ApiBusinessInfoModel
            {
                ClientId = clientId,
                BaseUrl = ArchLibCache.GetApplicationDefault(clientId, "BaseUrl", ""),
                BusinessName1 = ArchLibCache.GetApplicationDefault(clientId, "BusinessName1", ""),
                BusinessName2 = ArchLibCache.GetApplicationDefault(clientId, "BusinessName2", ""),
                BusinessType = ArchLibCache.GetApplicationDefault(clientId, "BusinessType", ""),
                ContactPhoneFormatted = ArchLibCache.GetApplicationDefault(clientId, "ContactPhoneFormatted", ""),
                ContactPhoneHref = ArchLibCache.GetApplicationDefault(clientId, "ContactPhoneHref", ""),
                ContactTextPhoneFormatted = ArchLibCache.GetApplicationDefault(clientId, "ContactTextPhoneFormatted", ""),
                ContactTextPhoneHref = ArchLibCache.GetApplicationDefault(clientId, "ContactTextPhoneHref", ""),
                ContactWhatsAppPhone = ArchLibCache.GetApplicationDefault(clientId, "ContactWhatsAppPhone", ""),
                ContactWhatsAppPhoneFormatted = ArchLibCache.GetApplicationDefault(clientId, "ContactWhatsAppPhoneFormatted", ""),
                DemogInfoAddressModels = new List<DemogInfoAddressModel>
                {
                    new DemogInfoAddressModel
                    {
                        AddressLine1 = ArchLibCache.GetApplicationDefault(clientId, "AddressLine1", ""),
                        AddressLine2 = ArchLibCache.GetApplicationDefault(clientId, "AddressLine1A", ""),
                        AddressLine3 = ArchLibCache.GetApplicationDefault(clientId, "AddressLine2", ""),
                        CityName = ArchLibCache.GetApplicationDefault(clientId, "AddressCityName", ""),
                        ZipCode = ArchLibCache.GetApplicationDefault(clientId, "AddressZipCode", ""),
                        StateAbbrev = ArchLibCache.GetApplicationDefault(clientId, "AddressStateAbbrev", ""),
                        CountryDesc = ArchLibCache.GetApplicationDefault(clientId, "AddressCountryName", ""),
                    },
                },
                LogoImageName = "Image_000.webp",
                LogoRelativeUrl = "/ClientSpecific/" + clientId + "_" + ArchLibCache.ClientName + "/Documents/Images/",
                WhatsAppUrl = "https://api.whatsapp.com/send?phone=",
            };
            retailSlnInitModel.BusinessInfoModel.LogoImageFullUrl = retailSlnInitModel.BusinessInfoModel.BaseUrl + retailSlnInitModel.BusinessInfoModel.LogoRelativeUrl + retailSlnInitModel.BusinessInfoModel.LogoImageName;
            retailSlnInitModel.BusinessInfoModel.PhoneImageFullUrl = retailSlnInitModel.BusinessInfoModel.BaseUrl + "Images/Phone1_Small.png";
            retailSlnInitModel.BusinessInfoModel.SMSImageFullUrl = retailSlnInitModel.BusinessInfoModel.BaseUrl + "Images/SMSIcon3_Small.png";
            retailSlnInitModel.BusinessInfoModel.WhatsAppImageFullUrl = retailSlnInitModel.BusinessInfoModel.BaseUrl + "Images/WhatsApp1_Small.png";
            return;
        }
        private static void BuildCacheModels5(RetailSlnInitModel retailSlnInitModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            foreach (ItemBundleItemModel itemBundleItemModel in retailSlnInitModel.ItemBundleItemModels)
            {
                itemBundleItemModel.ItemModel = ItemModels.First(x => x.ItemId == itemBundleItemModel.ItemId);
                itemBundleItemModel.ItemBundleModel = ItemBundleModels.First(x => x.ItemBundleId == itemBundleItemModel.ItemBundleId);
            }
            //var results = retailSlnInitModel.ItemModels.Where(a => retailSlnInitModel.ItemBundleItemModels.Where(b => b.ItemBundleId == 6 && b.ItemId == a.ItemId).Any()).ToList();
            foreach (ItemBundleModel itemBundleModel in retailSlnInitModel.ItemBundleModels)
            {
                itemBundleModel.ItemModel = retailSlnInitModel.ItemModels.First(x => x.ItemId == itemBundleModel.ItemId);
                itemBundleModel.DiscountPercentFormatted = (itemBundleModel.DiscountPercent / 100).ToString("0.00%");
                itemBundleModel.ItemRateAfterDiscount = itemBundleModel.ItemModel.ItemRate.Value * (100 - itemBundleModel.DiscountPercent) / 100;
                itemBundleModel.ItemRateAfterDiscountFormatted = itemBundleModel.ItemRateAfterDiscount.ToString(CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                itemBundleModel.ItemBundleItemModels = retailSlnInitModel.ItemBundleItemModels.FindAll(x => x.ItemBundleId == itemBundleModel.ItemBundleId);
            }
            foreach (var festivalListModel in retailSlnInitModel.FestivalListModels)
            {
                festivalListModel.FestivalListImageModels = retailSlnInitModel.FestivalListImageModels.FindAll(x => x.FestivalListId == festivalListModel.FestivalListId);
            }
            DeliveryMethodSelectListItems = new List<SelectListItem>();
            PickupLocationDemogInfoAddressModels1 = new List<DemogInfoAddressModel>();
            //For Credit Sale and custom location
            PickupLocationDemogInfoAddressModels2 = new List<DemogInfoAddressModel>
            {
                retailSlnInitModel.PickupLocationModels[0].DemogInfoAddressModel
            };
            foreach (var codeDataModel in LookupCache.CodeDataModels.FindAll(x => x.CodeTypeId == 205))
            {
                if (codeDataModel.CodeDataNameDesc == "PickupFromStore")
                {
                    var selectListGroup = new SelectListGroup
                    {
                        Name = "Pick up from",
                    };
                    foreach (var pickupLocationModel in retailSlnInitModel.PickupLocationModels.Skip(1))
                    {
                        PickupLocationDemogInfoAddressModels1.Add(pickupLocationModel.DemogInfoAddressModel);
                        DeliveryMethodSelectListItems.Add
                        (
                            new SelectListItem
                            {
                                Group = selectListGroup,
                                Text = pickupLocationModel.LocationDesc,
                                Value = codeDataModel.CodeDataNameId.ToString() + ";" + pickupLocationModel.PickupLocationId.Value.ToString() + ";",
                            }
                        );
                    }
                }
                else
                {
                    DeliveryMethodSelectListItems.Add
                    (
                        new SelectListItem
                        {
                            Text = codeDataModel.CodeDataDesc0,
                            Value = codeDataModel.CodeDataNameId.ToString() + ";;",
                        }
                    );
                    PickupLocationDemogInfoAddressModels1.Add(new DemogInfoAddressModel { DemogInfoAddressId = -1 });
                }
            }
            return;
        }
        /*
        private static void TempFunction(List<CorpAcctModel> corpAcctModels, List<CorpAcctLocationModel> corpAcctLocationModels)
        {
            int rowNum = 0;
            var fileName = Utilities.GetServerMapPath("~") + @"\CorpAcctLocationModel.txt";
            StreamWriter streamWriter = new StreamWriter(fileName);

            TempFunction1(corpAcctLocationModels, streamWriter, ref rowNum);

            streamWriter.Write(Environment.NewLine);
            streamWriter.Write(Environment.NewLine);

            var corpAcctLocationModelsTemp = corpAcctModels.First(x => x.CorpAcctId == 41).CorpAcctLocationModels;
            rowNum = 900;
            TempFunction1(corpAcctLocationModelsTemp, streamWriter, ref rowNum);

            streamWriter.Close();
        }
        private static void TempFunction1(List<CorpAcctLocationModel> corpAcctLocationModels, StreamWriter streamWriter, ref int rowNum)
        {
            foreach (var corpAcctLocationModel in corpAcctLocationModels)
            {
                rowNum++;
                streamWriter.Write(rowNum);
                streamWriter.Write("\t" + corpAcctLocationModel.CorpAcctLocationId);
                streamWriter.Write("\t" + corpAcctLocationModel.ClientId);
                streamWriter.Write("\t" + corpAcctLocationModel.AlternateTelephoneCountryId);
                streamWriter.Write("\t" + corpAcctLocationModel.AlternateTelephoneNumber);
                streamWriter.Write("\t" + corpAcctLocationModel.CorpAcctId);
                streamWriter.Write("\t" + corpAcctLocationModel.DemogInfoAddressId);
                streamWriter.Write("\t" + corpAcctLocationModel.LocationName);
                streamWriter.Write("\t" + corpAcctLocationModel.PrimaryTelephoneCountryId);
                streamWriter.Write("\t" + corpAcctLocationModel.PrimaryTelephoneNumber);
                streamWriter.Write("\t" + corpAcctLocationModel.SeqNum);
                streamWriter.Write("\t" + corpAcctLocationModel.DemogInfoAddressModel.AddressLine1);
                streamWriter.Write("\t" + corpAcctLocationModel.DemogInfoAddressModel.AddressLine2);
                streamWriter.Write("\t" + corpAcctLocationModel.DemogInfoAddressModel.AddressLine3);
                streamWriter.Write("\t" + corpAcctLocationModel.DemogInfoAddressModel.AddressLine4);
                streamWriter.Write("\t" + corpAcctLocationModel.DemogInfoAddressModel.AddressName);
                streamWriter.Write("\t" + corpAcctLocationModel.DemogInfoAddressModel.AddressTypeId);
                streamWriter.Write("\t" + corpAcctLocationModel.DemogInfoAddressModel.HouseNumber);
                streamWriter.Write("\t" + corpAcctLocationModel.DemogInfoAddressModel.BuildingTypeId);
                //streamWriter.Write("\t" + corpAcctLocationModel.DemogInfoAddressModel.);
                //streamWriter.Write("\t" + corpAcctLocationModel.DemogInfoAddressModel.);
                //streamWriter.Write("\t" + corpAcctLocationModel.DemogInfoAddressModel.);
                //streamWriter.Write("\t" + corpAcctLocationModel.DemogInfoAddressModel.);
                streamWriter.Write("\t" + corpAcctLocationModel.DemogInfoAddressModel.CityName);
                streamWriter.Write("\t" + corpAcctLocationModel.DemogInfoAddressModel.StateAbbrev);
                streamWriter.Write("\t" + corpAcctLocationModel.DemogInfoAddressModel.ZipCode);
                streamWriter.Write("\t" + corpAcctLocationModel.DemogInfoAddressModel.CountryAbbrev);
                streamWriter.Write("\t" + corpAcctLocationModel.DemogInfoAddressModel.CountryDesc);

                streamWriter.Write(Environment.NewLine);
            }
        }
        */
    }
}
