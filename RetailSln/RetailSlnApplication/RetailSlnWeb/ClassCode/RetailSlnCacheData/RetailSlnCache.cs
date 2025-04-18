using ArchitectureLibraryCacheData;
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
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace RetailSlnCacheData
{
    public class RetailSlnCache
    {
        #region Properties
        public static List<CategoryModel> CategoryModels { set; get; }
        public static List<CategoryItemMasterHierModel> CategoryItemMasterHierModels { set; get; }
        public static List<CorpAcctModel> CorpAcctModels { set; get; }
        public static List<CorpAcctLocationModel> CorpAcctLocationModels { set; get; }
        public static List<DeliveryMethodFilterModel> DeliveryMethodFilterModels { set; get; }
        public static List<ItemModel> ItemModels { set; get; }
        public static List<ItemMasterModel> ItemMasterModels { set; get; }
        public static List<ItemSpecMasterModel> ItemSpecMasterModels { set; get; }
        public static List<ItemSpecModel> ItemSpecModels { set; get; }
        public static List<PaymentModeFilterModel> PaymentModeFilterModels { set; get; }
        public static List<PickupLocationModel> PickupLocationModels { set; get; }
        #endregion
        #region
        public static Dictionary<string, Dictionary<long, List<CategoryItemMasterHierModel>>> AspNetRoleParentCategoryCategoryModels { set; get; }
        public static Dictionary<string, Dictionary<long, List<CategoryItemMasterHierModel>>> AspNetRoleParentCategoryItemMasterModels { set; get; }
        public static CultureInfo CurrencyCultureInfo { set; get; }
        public static string CurrencyDecimalPlaces { set; get; }
        public static string CurrencySymbol { set; get; }
        public static long DefaultDeliveryDemogInfoCountryId { set; get; }
        public static int RoundingDigitCount { set; get; }
        public static List<DemogInfoCountryModel> DeliveryDemogInfoCountryModels { set; get; }
        public static List<SelectListItem> DeliveryDemogInfoCountrySelectListItems { set; get; }
        public static Dictionary<long, List<SelectListItem>> DeliveryDemogInfoCountrySubDivisionSelectListItems { set; get; }
        public static Dictionary<YesNoEnum, List<CodeDataModel>> DeliveryMethodsList { set; get; }
        public static Dictionary<YesNoEnum, List<CodeDataModel>> PaymentModesList { set; get; }
        public static List<SelectListItem> DeliveryMethodSelectListItems { set; get; }
        public static List<DemogInfoAddressModel> PickupLocationDemogInfoAddressModels1 { set; get; }
        public static List<DemogInfoAddressModel> PickupLocationDemogInfoAddressModels2 { set; get; }
        #endregion
        public static void Initialize(long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            RetailSlnCacheBL retailSlnCacheBL = new RetailSlnCacheBL();
            retailSlnCacheBL.Initialize(out RetailSlnInitModel retailSlnInitModel, clientId, ipAddress, execUniqueId, loggedInUserId);
            CategoryModels = retailSlnInitModel.CategoryModels;
            CategoryItemMasterHierModels = retailSlnInitModel.CategoryItemMasterHierModels;
            CorpAcctModels = retailSlnInitModel.CorpAcctModels;
            CorpAcctLocationModels = retailSlnInitModel.CorpAcctLocationModels;
            DeliveryMethodFilterModels = retailSlnInitModel.DeliveryMethodFilterModels;
            ItemModels = retailSlnInitModel.ItemModels;
            ItemMasterModels = retailSlnInitModel.ItemMasterModels;
            ItemSpecMasterModels = retailSlnInitModel.ItemSpecMasterModels;
            ItemSpecModels = retailSlnInitModel.ItemSpecModels;
            PaymentModeFilterModels = retailSlnInitModel.PaymentModeFilterModels;
            PickupLocationModels = retailSlnInitModel.PickupLocationModels;

            CurrencyCultureInfo = new CultureInfo(ArchLibCache.GetApplicationDefault(clientId, "Currency", "CultureInfo"));
            CurrencyDecimalPlaces = ArchLibCache.GetApplicationDefault(clientId, "Currency", "CurrencyDecimalPlaces");
            var regionInfo = new RegionInfo(ArchLibCache.GetApplicationDefault(clientId, "Currency", "CultureInfo"));
            CurrencySymbol = regionInfo.CurrencySymbol;
            DefaultDeliveryDemogInfoCountryId = long.Parse(ArchLibCache.GetApplicationDefault(clientId, "DeliveryInfo", "DefaultDemogInfoCountry"));
            RoundingDigitCount = int.Parse(ArchLibCache.GetApplicationDefault(clientId, "OrderProcess", "RoundingDigitCount"));

            BuildCacheModels(retailSlnInitModel, clientId, ipAddress, execUniqueId, loggedInUserId);
        }
        private static void BuildCacheModels(RetailSlnInitModel retailSlnInitModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            BuildCacheModels0(retailSlnInitModel, clientId, ipAddress, execUniqueId, loggedInUserId);
            BuildCacheModels1(retailSlnInitModel, clientId, ipAddress, execUniqueId, loggedInUserId);
            BuildCacheModels2(retailSlnInitModel, clientId, ipAddress, execUniqueId, loggedInUserId);
        }
        private static void BuildCacheModels0(RetailSlnInitModel retailSlnInitModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            foreach (var itemSpecModel in retailSlnInitModel.ItemSpecModels)
            {
                itemSpecModel.ItemSpecMasterModel = retailSlnInitModel.ItemSpecMasterModels.First(x => x.ItemSpecMasterId == itemSpecModel.ItemSpecMasterId);
            }
            int itemSeqNum = 0;
            string prefixString;
            foreach (var itemModel in retailSlnInitModel.ItemModels)
            {
                itemSeqNum++;
                itemModel.ItemMasterModel = retailSlnInitModel.ItemMasterModels.First(x => x.ItemMasterId == itemModel.ItemMasterId);
                itemModel.ItemRateFormatted = itemModel.ItemRate.Value.ToString(CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                itemModel.ImageTitle = itemModel.ItemShortDesc + " Id" + itemModel.ItemId + " #" + itemSeqNum + "/" + retailSlnInitModel.ItemModels.Count;
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
                itemModel.ItemSpecModelsForItem = retailSlnInitModel.ItemSpecModels.FindAll(x => x.ItemId == itemModel.ItemId && x.SeqNumItem != null).OrderBy(y => y.SeqNumItem).ToList();
                itemModel.ItemSpecModelsForDisplay = new Dictionary<string, ItemSpecModel>();
                List<CodeDataModel> abc1;
                CodeDataModel abc2;
                foreach (var itemSpecModel in itemModel.ItemSpecModels)
                {
                    itemModel.ItemSpecModelsForDisplay[itemSpecModel.ItemSpecMasterModel.SpecName] = itemSpecModel;
                    itemSpecModel.ItemSpecValueForDisplay = itemSpecModel.ItemSpecValue;
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
                prefixString = string.Empty;
                itemModel.ItemItemSpecsForDisplay = "";
                foreach (var itemItemSpecModel in itemModel.ItemSpecModelsForItem)
                {
                    itemModel.ItemItemSpecsForDisplay += prefixString + itemItemSpecModel.ItemSpecValueForDisplay;
                    prefixString = " | ";
                }
            }
            int itemMasterSeqNum = 0;
            int itemModelCount;
            foreach (var itemMasterModel in retailSlnInitModel.ItemMasterModels)
            {
                itemMasterSeqNum++;
                itemMasterModel.ImageTitle = itemMasterModel.ItemMasterDesc + " Id" + itemMasterModel.ItemMasterId + " #" + itemMasterSeqNum + "/" + retailSlnInitModel.ItemMasterModels.Count;
                itemMasterModel.ItemModels = retailSlnInitModel.ItemModels.FindAll(x => x.ItemMasterId == itemMasterModel.ItemMasterId);
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
        }
        private static void BuildCacheModels1(RetailSlnInitModel retailSlnInitModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            int i;
            long parentCategoryId;
            string aspNetRoleName;
            List<CategoryItemMasterHierModel> categoryItemMasterHierModels;
            foreach (var categoryItemMasterHierModel in retailSlnInitModel.CategoryItemMasterHierModels)
            {
                categoryItemMasterHierModel.ParentCategoryModel = retailSlnInitModel.CategoryModels.First(x => x.CategoryId == categoryItemMasterHierModel.ParentCategoryId);
                if (categoryItemMasterHierModel.CategoryId != null)
                {
                    categoryItemMasterHierModel.CategoryModel = retailSlnInitModel.CategoryModels.First(x => x.CategoryId == categoryItemMasterHierModel.CategoryId);
                }
                if (categoryItemMasterHierModel.ItemMasterId != null)
                {
                    categoryItemMasterHierModel.ItemMasterModel = retailSlnInitModel.ItemMasterModels.First(x => x.ItemMasterId == categoryItemMasterHierModel.ItemMasterId);
                }
            }
            //Get all with Item Master Id Null - All categories (hier)
            AspNetRoleParentCategoryCategoryModels = new Dictionary<string, Dictionary<long, List<CategoryItemMasterHierModel>>>();
            categoryItemMasterHierModels = retailSlnInitModel.CategoryItemMasterHierModels.FindAll(x => x.ItemMasterId == null).OrderBy(y => y.AspNetRoleName).ThenBy(y => y.ParentCategoryId).ThenBy(y => y.SeqNum).ToList();
            i = 0;
            while (i < categoryItemMasterHierModels.Count)
            {
                aspNetRoleName = categoryItemMasterHierModels[i].AspNetRoleName;
                AspNetRoleParentCategoryCategoryModels[aspNetRoleName] = new Dictionary<long, List<CategoryItemMasterHierModel>>();
                while (i < categoryItemMasterHierModels.Count &&
                       categoryItemMasterHierModels[i].AspNetRoleName == aspNetRoleName
                      )
                {
                    parentCategoryId = categoryItemMasterHierModels[i].ParentCategoryId;
                    AspNetRoleParentCategoryCategoryModels[aspNetRoleName][parentCategoryId] = new List<CategoryItemMasterHierModel>();
                    while (i < categoryItemMasterHierModels.Count &&
                           categoryItemMasterHierModels[i].AspNetRoleName == aspNetRoleName &&
                           categoryItemMasterHierModels[i].ParentCategoryId == parentCategoryId
                          )
                    {
                        AspNetRoleParentCategoryCategoryModels[aspNetRoleName][parentCategoryId].Add(categoryItemMasterHierModels[i]);
                        i++;
                    }
                }
            }
            //Get all with Item Master Id Not Null - All item masters (hier)
            AspNetRoleParentCategoryItemMasterModels = new Dictionary<string, Dictionary<long, List<CategoryItemMasterHierModel>>>();
            categoryItemMasterHierModels = retailSlnInitModel.CategoryItemMasterHierModels.FindAll(x => x.CategoryId == null).OrderBy(y => y.AspNetRoleName).ThenBy(y => y.ParentCategoryId).ThenBy(y => y.SeqNum).ToList();
            i = 0;
            while (i < categoryItemMasterHierModels.Count)
            {
                aspNetRoleName = categoryItemMasterHierModels[i].AspNetRoleName;
                AspNetRoleParentCategoryItemMasterModels[aspNetRoleName] = new Dictionary<long, List<CategoryItemMasterHierModel>>();
                while (i < categoryItemMasterHierModels.Count &&
                       categoryItemMasterHierModels[i].AspNetRoleName == aspNetRoleName
                      )
                {
                    parentCategoryId = categoryItemMasterHierModels[i].ParentCategoryId;
                    AspNetRoleParentCategoryItemMasterModels[aspNetRoleName][parentCategoryId] = new List<CategoryItemMasterHierModel>();
                    while (i < categoryItemMasterHierModels.Count &&
                           categoryItemMasterHierModels[i].AspNetRoleName == aspNetRoleName &&
                           categoryItemMasterHierModels[i].ParentCategoryId == parentCategoryId
                          )
                    {
                        AspNetRoleParentCategoryItemMasterModels[aspNetRoleName][parentCategoryId].Add(categoryItemMasterHierModels[i]);
                        i++;
                    }
                }
            }
        }
        private static void BuildCacheModels2(RetailSlnInitModel retailSlnInitModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            YesNoEnum creditSale, shippingAndHandlingCharges;
            int i;
            foreach (var corpAcctModel in retailSlnInitModel.CorpAcctModels)
            {
                //corpAcctModel.CorpAcctLocationModels = new List<CorpAcctLocationModel>();
                corpAcctModel.CorpAcctLocationModels.AddRange(retailSlnInitModel.CorpAcctLocationModels.FindAll(x => x.CorpAcctId == corpAcctModel.CorpAcctId));
            }
            List<SelectListItem> deliveryDemogInfoSubDivisionSelectListItemsTemp;
            DemogInfoCountryModel demogInfoCountryModel;
            DeliveryDemogInfoCountryModels = new List<DemogInfoCountryModel>();
            DeliveryDemogInfoCountrySelectListItems = new List<SelectListItem>();
            DeliveryDemogInfoCountrySubDivisionSelectListItems = new Dictionary<long, List<SelectListItem>>();
            string deliveryInfoDemogInfoCountryIds = ArchLibCache.GetApplicationDefault(clientId, "DeliveryInfo", "DemogInfoCountryIds");
            foreach (var deliveryInfoDemogInfoCountryId in deliveryInfoDemogInfoCountryIds.Split(';'))
            {
                DeliveryDemogInfoCountryModels.Add(demogInfoCountryModel = DemogInfoCache.DemogInfoCountryModels.First(x => x.DemogInfoCountryId == long.Parse(deliveryInfoDemogInfoCountryId)));
                DeliveryDemogInfoCountrySubDivisionSelectListItems[demogInfoCountryModel.DemogInfoCountryId] = (deliveryDemogInfoSubDivisionSelectListItemsTemp = new List<SelectListItem>());
                DeliveryDemogInfoCountrySelectListItems.Add
                (
                    new SelectListItem
                    {
                        Text = demogInfoCountryModel.CountryDesc + " " + demogInfoCountryModel.CountryAbbrev,
                        Value = demogInfoCountryModel.DemogInfoCountryId.ToString(),
                    }
                );
            }
            DeliveryMethodsList = new Dictionary<YesNoEnum, List<CodeDataModel>>();
            i = 0;
            while (i < DeliveryMethodFilterModels.Count)
            {
                shippingAndHandlingCharges = DeliveryMethodFilterModels[i].ShippingAndHandlingCharges;
                DeliveryMethodsList[shippingAndHandlingCharges] = new List<CodeDataModel>();
                while (i < DeliveryMethodFilterModels.Count && shippingAndHandlingCharges == DeliveryMethodFilterModels[i].ShippingAndHandlingCharges)
                {
                    DeliveryMethodsList[shippingAndHandlingCharges].Add(LookupCache.CodeDataModels.First(x => x.CodeTypeId == 205 && x.CodeDataNameId == DeliveryMethodFilterModels[i].DeliveryMethodId));
                    i++;
                }
            }
            i = 0;
            PaymentModesList = new Dictionary<YesNoEnum, List<CodeDataModel>>();
            while (i < PaymentModeFilterModels.Count)
            {
                creditSale = PaymentModeFilterModels[i].CreditSale;
                PaymentModesList[creditSale] = new List<CodeDataModel>();
                while (i < PaymentModeFilterModels.Count && creditSale == PaymentModeFilterModels[i].CreditSale)
                {
                    PaymentModesList[creditSale].Add(LookupCache.CodeDataModels.First(x => x.CodeTypeId == 212 && x.CodeDataNameId == PaymentModeFilterModels[i].PaymentModeId));
                    i++;
                }
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
                    foreach (var pickupLocationModel in retailSlnInitModel.PickupLocationModels.Skip(1))
                    {
                        PickupLocationDemogInfoAddressModels1.Add(pickupLocationModel.DemogInfoAddressModel);
                        DeliveryMethodSelectListItems.Add
                        (
                            new SelectListItem
                            {
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
        }
    }
}
