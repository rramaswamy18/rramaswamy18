using ArchitectureLibraryBusinessLayer;
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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace RetailSlnCacheData
{
    public class RetailSlnCache
    {
        #region Properties
        public static List<AspNetRoleModel> AspNetRoleModelsReferral { set; get; }
        public static List<CategoryModel> CategoryModels { set; get; }
        public static List<CategoryItemMasterHierModel> CategoryItemMasterHierModels { set; get; }
        public static Dictionary<long, Dictionary<long, ItemDiscountModel>> CorpAcctItemDiscountModels { set; get; }
        public static List<CorpAcctModel> CorpAcctModels { set; get; }
        public static List<CorpAcctLocationModel> CorpAcctLocationModels { set; get; }
        public static List<DeliveryMethodFilterModel> DeliveryMethodFilterModels { set; get; }
        public static List<ItemBundleModel> ItemBundleModels { set; get; }
        public static List<ItemDiscountModel> ItemDiscountModels { set; get; }
        public static List<ItemItemSpecModel> ItemItemSpecModels { set; get; }
        public static List<ItemMasterItemSpecModel> ItemMasterItemSpecModels { set; get; }
        public static List<ItemMasterModel> ItemMasterModels { set; get; }
        public static List<ItemModel> ItemModels { set; get; }
        public static List<ItemSpecMasterModel> ItemSpecMasterModels { set; get; }
        public static List<PaymentModeFilterModel> PaymentModeFilterModels { set; get; }
        public static List<PickupLocationModel> PickupLocationModels { set; get; }
        #endregion
        #region
        public static Dictionary<string, Dictionary<long, List<CategoryItemMasterHierModel>>> AspNetRoleParentCategoryCategoryModels { set; get; }
        public static Dictionary<string, Dictionary<long, List<CategoryItemMasterHierModel>>> AspNetRoleParentCategoryItemMasterModels { set; get; }
        public static string BuildDateTime { set; get; }
        public static CultureInfo CurrencyCultureInfo { set; get; }
        public static string CurrencyDecimalPlaces { set; get; }
        public static string CurrencySymbol { set; get; }
        public static long DefaultDeliveryDemogInfoCountryId { set; get; }
        public static int RoundingDigitCount { set; get; }
        public static List<DemogInfoCountryModel> DeliveryDemogInfoCountryModels { set; get; }
        public static List<SelectListItem> DeliveryDemogInfoCountrySelectListItems { set; get; }
        public static Dictionary<long, List<SelectListItem>> DeliveryDemogInfoCountrySubDivisionSelectListItems { set; get; }
        public static Dictionary<YesNoEnum, List<CodeDataModel>> DeliveryMethodsList { set; get; }
        public static List<SelectListItem> DeliveryMethodSelectListItems { set; get; }
        public static Dictionary<YesNoEnum, List<CodeDataModel>> PaymentModesList { set; get; }
        public static Dictionary<long, ParentItemBundleModel> ParentItemBundleModels { set; get; }
        public static List<DemogInfoAddressModel> PickupLocationDemogInfoAddressModels1 { set; get; }
        public static List<DemogInfoAddressModel> PickupLocationDemogInfoAddressModels2 { set; get; }
        #endregion
        public static void Initialize(long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            BuildDateTime = GetBuildDateTime();
            RetailSlnCacheBL retailSlnCacheBL = new RetailSlnCacheBL();
            retailSlnCacheBL.Initialize(out RetailSlnInitModel retailSlnInitModel, clientId, ipAddress, execUniqueId, loggedInUserId);
            CategoryModels = retailSlnInitModel.CategoryModels;
            CategoryItemMasterHierModels = retailSlnInitModel.CategoryItemMasterHierModels;
            CorpAcctModels = retailSlnInitModel.CorpAcctModels;
            CorpAcctLocationModels = retailSlnInitModel.CorpAcctLocationModels;
            DeliveryMethodFilterModels = retailSlnInitModel.DeliveryMethodFilterModels;
            ItemBundleModels = retailSlnInitModel.ItemBundleModels;
            ItemDiscountModels = retailSlnInitModel.ItemDiscountModels;
            ItemModels = retailSlnInitModel.ItemModels;
            ItemItemSpecModels = retailSlnInitModel.ItemItemSpecModels;
            ItemMasterItemSpecModels = retailSlnInitModel.ItemMasterItemSpecModels;
            ItemMasterModels = retailSlnInitModel.ItemMasterModels;
            ItemSpecMasterModels = retailSlnInitModel.ItemSpecMasterModels;
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
        private static string GetBuildDateTime()
        {
            string projectDirectory = Utilities.GetServerMapPath("");
            StreamReader streamReader = new StreamReader(projectDirectory + @"\BuildDateTime.txt");
            string buildDateTime = streamReader.ReadToEnd();
            streamReader.Close();
            buildDateTime = buildDateTime.Replace("  ", " ");
            var buildDateTimes = buildDateTime.Split(' ');
            buildDateTime = buildDateTimes[0] + " " + DateTime.Parse(buildDateTimes[1]).ToString("MMM-dd-yyyy") + " " + buildDateTimes[2].Trim() + " " + buildDateTimes[3].Trim();
            return buildDateTime;
        }
        private static void BuildCacheModels(RetailSlnInitModel retailSlnInitModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            BuildCacheModels0(retailSlnInitModel, clientId, ipAddress, execUniqueId, loggedInUserId);
            BuildCacheModels1(retailSlnInitModel, clientId, ipAddress, execUniqueId, loggedInUserId);
            BuildCacheModels2(retailSlnInitModel, clientId, ipAddress, execUniqueId, loggedInUserId);
            BuildCacheModels3(retailSlnInitModel, clientId, ipAddress, execUniqueId, loggedInUserId);
        }
        private static void BuildCacheModels0(RetailSlnInitModel retailSlnInitModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<CodeDataModel> codeDataModels;
            CodeDataModel codeDataModel;
            foreach (var itemMasterItemSpecModel in retailSlnInitModel.ItemMasterItemSpecModels)
            {
                itemMasterItemSpecModel.ItemSpecMasterModel = retailSlnInitModel.ItemSpecMasterModels.First(x => x.ItemSpecMasterId == itemMasterItemSpecModel.ItemSpecMasterId);
                itemMasterItemSpecModel.ItemSpecValueForDisplay = itemMasterItemSpecModel.ItemSpecValue;
                if (itemMasterItemSpecModel.ItemSpecValue != "")
                {
                    if (itemMasterItemSpecModel.ItemSpecMasterModel.CodeTypeId != null)
                    {
                        try
                        {
                            codeDataModels = LookupCache.GetCodeDatasForCodeTypeIdByCodeDataNameId(itemMasterItemSpecModel.ItemSpecMasterModel.CodeTypeId.Value, execUniqueId);
                            codeDataModel = codeDataModels.First(x => x.CodeDataNameId == long.Parse(itemMasterItemSpecModel.ItemSpecUnitValue));
                            itemMasterItemSpecModel.ItemSpecValueForDisplay += " " + codeDataModel.CodeDataDesc0;
                        }
                        catch (Exception exception)
                        {
                            exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception, "ItemMasterItemSpecId", itemMasterItemSpecModel.ItemMasterItemSpecId.ToString());
                        }
                    }
                }
            }
            foreach (var itemItemSpecModel in retailSlnInitModel.ItemItemSpecModels)
            {
                itemItemSpecModel.ItemSpecMasterModel = retailSlnInitModel.ItemSpecMasterModels.First(x => x.ItemSpecMasterId == itemItemSpecModel.ItemSpecMasterId);
                itemItemSpecModel.ItemSpecValueForDisplay = itemItemSpecModel.ItemSpecValue;
                if (itemItemSpecModel.ItemSpecValue != "")
                {
                    if (itemItemSpecModel.ItemSpecMasterModel.CodeTypeId != null)
                    {
                        try
                        {
                            codeDataModels = LookupCache.GetCodeDatasForCodeTypeIdByCodeDataNameId(itemItemSpecModel.ItemSpecMasterModel.CodeTypeId.Value, execUniqueId);
                            codeDataModel = codeDataModels.First(x => x.CodeDataNameId == long.Parse(itemItemSpecModel.ItemSpecUnitValue));
                            itemItemSpecModel.ItemSpecValueForDisplay += " " + codeDataModel.CodeDataDesc0;
                        }
                        catch (Exception exception)
                        {
                            exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception, "ItemId", itemItemSpecModel.ItemId.ToString(), "ItemItemSpecId", itemItemSpecModel.ItemItemSpecId.ToString());
                        }
                    }
                }
            }
            int itemSeqNum = 0, itemMasterSeqNum = 0, itemModelCount;
            string prefixString, prefixString2;
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
                itemModel.ItemItemSpecModels = new Dictionary<string, ItemItemSpecModel>();
                itemModel.ItemItemSpecsForDisplayAll = "";
                itemModel.ItemItemSpecsForDisplay = "";
                prefixString = "";
                prefixString2 = "";
                foreach (var itemItemSpecModel in retailSlnInitModel.ItemItemSpecModels.FindAll(x => x.ItemId == itemModel.ItemId))
                {
                    itemModel.ItemItemSpecModels[itemItemSpecModel.ItemSpecMasterModel.SpecName] = itemItemSpecModel;
                    itemModel.ItemItemSpecsForDisplayAll += prefixString + itemItemSpecModel.ItemSpecValueForDisplay;
                    if (itemItemSpecModel.SeqNumItem != null)
                    {
                        itemModel.ItemItemSpecsForDisplay += prefixString2 + itemItemSpecModel.ItemSpecValueForDisplay;
                        prefixString2 = " | ";
                    }
                    prefixString = " | ";
                }
            }
            foreach (var itemMasterModel in retailSlnInitModel.ItemMasterModels)
            {
                itemMasterSeqNum++;
                itemMasterModel.ImageTitle = itemMasterModel.ItemMasterDesc + " Id" + itemMasterModel.ItemMasterId + " #" + itemMasterSeqNum + "/" + retailSlnInitModel.ItemMasterModels.Count;
                itemMasterModel.ItemModels = retailSlnInitModel.ItemModels.FindAll(x => x.ItemMasterId == itemMasterModel.ItemMasterId);
                itemMasterModel.ItemRatesForDisplay = "";
                itemMasterModel.ItemRatesForDisplayAll = "";
                prefixString = "";
                prefixString2 = "";
                itemMasterModel.ItemMasterItemSpecModels = new Dictionary<string, ItemMasterItemSpecModel>();
                foreach (var itemMasterItemSpecModel in retailSlnInitModel.ItemMasterItemSpecModels.FindAll(x => x.ItemMasterId == itemMasterModel.ItemMasterId))
                {
                    itemMasterModel.ItemMasterItemSpecModels[itemMasterItemSpecModel.ItemSpecMasterModel.SpecName] = itemMasterItemSpecModel;
                    itemMasterModel.ItemMasterItemSpecsForDisplayAll += prefixString + itemMasterItemSpecModel.ItemSpecValueForDisplay;
                    if (itemMasterItemSpecModel.SeqNumItemMaster != null)
                    {
                        itemMasterModel.ItemMasterItemSpecsForDisplay += prefixString2 + itemMasterItemSpecModel.ItemSpecValueForDisplay;
                        prefixString2 = " | ";
                    }
                    prefixString = " | ";
                }
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
            AspNetRoleModelsReferral = ArchLibCache.AspNetRoleModels.FindAll(x => x.UserTypeId >= 500 && x.UserTypeId <= 700);
        }
        private static void BuildCacheModels1(RetailSlnInitModel retailSlnInitModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            int i;
            long parentCategoryId;
            string aspNetRoleName;
            long corpAcctId;
            CorpAcctItemDiscountModels = new Dictionary<long, Dictionary<long, ItemDiscountModel>>();
            i = 0;
            while (i < retailSlnInitModel.ItemDiscountModels.Count)
            {
                corpAcctId = retailSlnInitModel.ItemDiscountModels[i].CorpAcctId;
                CorpAcctItemDiscountModels[retailSlnInitModel.ItemDiscountModels[i].CorpAcctId] = new Dictionary<long, ItemDiscountModel>();
                while (i < retailSlnInitModel.ItemDiscountModels.Count && retailSlnInitModel.ItemDiscountModels[i].CorpAcctId == corpAcctId)
                {
                    CorpAcctItemDiscountModels[retailSlnInitModel.ItemDiscountModels[i].CorpAcctId][retailSlnInitModel.ItemDiscountModels[i].ItemId] = retailSlnInitModel.ItemDiscountModels[i];
                    i++;
                }
            }
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
            foreach (var itemMasterModel in retailSlnInitModel.ItemMasterModels)
            {
                List<CategoryItemMasterHierModel> aspNetRoleNameCategoryModels = retailSlnInitModel.CategoryItemMasterHierModels.FindAll(x => x.ItemMasterId == itemMasterModel.ItemMasterId).OrderBy(x => x.AspNetRoleName).ToList();//.ThenBy(x => x.CategoryModel.CategoryDesc).ToList();
                i = 0;
                itemMasterModel.AspNetRoleNameCategoryModels = new Dictionary<string, List<CategoryModel>>();
                while (i < aspNetRoleNameCategoryModels.Count)
                {
                    aspNetRoleName = aspNetRoleNameCategoryModels[i].AspNetRoleName;
                    itemMasterModel.AspNetRoleNameCategoryModels[aspNetRoleName] = new List<CategoryModel>();
                    while (i < aspNetRoleNameCategoryModels.Count &&
                           aspNetRoleName == aspNetRoleNameCategoryModels[i].AspNetRoleName
                          )
                    {
                        itemMasterModel.AspNetRoleNameCategoryModels[aspNetRoleName].Add(aspNetRoleNameCategoryModels[i].ParentCategoryModel);
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
                                Text = "Pickup from " + pickupLocationModel.LocationDesc,
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
        private static void BuildCacheModels3(RetailSlnInitModel retailSlnInitModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            foreach (ItemBundleModel itemBundleModel in retailSlnInitModel.ItemBundleModels)
            {
                itemBundleModel.ItemModel = retailSlnInitModel.ItemModels.First(x => x.ItemId == itemBundleModel.ItemId);
            }
            int i = 0;
            long parentItemId;
            ItemModel itemModel;
            ShoppingCartItemModel shoppingCartItemModel;
            ParentItemBundleModels = new Dictionary<long, ParentItemBundleModel>();
            while (i < retailSlnInitModel.ItemBundleModels.Count)
            {
                parentItemId = retailSlnInitModel.ItemBundleModels[i].ParentItemId;
                ParentItemBundleModels[parentItemId] = new ParentItemBundleModel
                {
                    ItemModels = new List<ItemModel>(),
                    ParentItemId = parentItemId,
                    ParentItemModel = retailSlnInitModel.ItemModels.First(x => x.ItemId == parentItemId),
                    ShoppingCartItemBundleModels = new List<ShoppingCartItemModel>(),
                    TotalOrderAmount = 0
                };
                while (i < retailSlnInitModel.ItemBundleModels.Count && parentItemId == retailSlnInitModel.ItemBundleModels[i].ParentItemId)
                {
                    itemModel = retailSlnInitModel.ItemBundleModels[i].ItemModel;
                    ParentItemBundleModels[parentItemId].ItemModels.Add(itemModel);
                    ParentItemBundleModels[parentItemId].TotalOrderAmount += itemModel.ItemRate.Value;
                    if (itemModel.ItemId == 0)
                    {
                        shoppingCartItemModel = new ShoppingCartItemModel
                        {
                            HSNCode = "", //itemModel.ItemItemSpecModels["HSNCode"].ItemSpecValueForDisplay,
                            ImageName = "",
                            ItemDiscountAmount = 0,
                            ItemDiscountPercent = 0,
                            ItemDiscountPercentFormatted = "",
                            ItemId = itemModel.ItemId,
                            ItemItemSpecsForDisplay = itemModel.ItemItemSpecsForDisplay,
                            ItemMasterDesc = itemModel.ItemMasterModel.ItemMasterDesc,
                            ItemMasterDesc0 = itemModel.ItemMasterModel.ItemMasterDesc0,
                            ItemMasterDesc1 = itemModel.ItemMasterModel.ItemMasterDesc1,
                            ItemMasterDesc2 = itemModel.ItemMasterModel.ItemMasterDesc2,
                            ItemMasterDesc3 = itemModel.ItemMasterModel.ItemMasterDesc3,
                            ItemRate = itemModel.ItemRate,
                            ItemRateBeforeDiscount = itemModel.ItemRate,
                            ItemRateBeforeDiscountFormatted = itemModel.ItemRate.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
                            ItemRateFormatted = itemModel.ItemRate.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
                            ItemShortDesc = null,//itemModel.ItemShortDesc,
                            OrderAmount = 0,
                            OrderAmountBeforeDiscount = 0,
                            OrderAmountFormatted = "",
                            OrderQty = 1,
                            OrderDetailTypeId = OrderDetailTypeEnum.Item,
                            ProductCode = "",
                            ProductOrVolumetricWeightUnitId = WeightUnitEnum.Grams,
                            WeightCalcUnitId = WeightUnitEnum.Grams,
                            WeightUnitId = WeightUnitEnum.Grams,
                            ItemModel = null,
                            ShoppingCartItemSummarys = new List<ShoppingCartItemModel>(),
                        };
                    }
                    else
                    {
                        shoppingCartItemModel = new ShoppingCartItemModel
                        {
                            HSNCode = itemModel.ItemItemSpecModels["HSNCode"].ItemSpecValueForDisplay,
                            ImageName = itemModel.ItemMasterModel.ImageName,
                            ItemDiscountAmount = 0,
                            ItemDiscountPercent = 0,
                            ItemDiscountPercentFormatted = "",
                            ItemId = itemModel.ItemId,
                            ItemItemSpecsForDisplay = itemModel.ItemItemSpecsForDisplay,
                            ItemMasterDesc = itemModel.ItemMasterModel.ItemMasterDesc,
                            ItemMasterDesc0 = itemModel.ItemMasterModel.ItemMasterDesc0,
                            ItemMasterDesc1 = itemModel.ItemMasterModel.ItemMasterDesc1,
                            ItemMasterDesc2 = itemModel.ItemMasterModel.ItemMasterDesc2,
                            ItemMasterDesc3 = itemModel.ItemMasterModel.ItemMasterDesc3,
                            ItemRate = itemModel.ItemRate,
                            ItemRateBeforeDiscount = itemModel.ItemRate,
                            ItemRateBeforeDiscountFormatted = itemModel.ItemRate.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
                            ItemRateFormatted = itemModel.ItemRate.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
                            ItemShortDesc = null,//itemModel.ItemShortDesc,
                            OrderAmount = itemModel.ItemRate,
                            OrderAmountBeforeDiscount = itemModel.ItemRate,
                            OrderAmountFormatted = itemModel.ItemRate.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", ""),
                            OrderQty = 1,
                            OrderDetailTypeId = OrderDetailTypeEnum.Item,
                            ProductCode = itemModel.ItemItemSpecModels["ProductCode"].ItemSpecValueForDisplay,
                            ProductOrVolumetricWeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemItemSpecModels["ProductOrVolumetricWeight"].ItemSpecUnitValue),
                            WeightCalcUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemItemSpecModels["ProductWeight"].ItemSpecUnitValue),
                            WeightUnitId = (WeightUnitEnum)int.Parse(itemModel.ItemItemSpecModels["ProductWeight"].ItemSpecUnitValue),
                            ItemModel = itemModel,
                            ShoppingCartItemSummarys = new List<ShoppingCartItemModel>(),
                        };
                    }
                    ParentItemBundleModels[parentItemId].ShoppingCartItemBundleModels.Add(shoppingCartItemModel);
                    i++;
                }
            }
        }
    }
}
