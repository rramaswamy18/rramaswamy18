using ArchitectureLibraryCacheData;
using ArchitectureLibraryDataLayer;
using ArchitectureLibraryEnumerations;
using ArchitectureLibraryModels;
using RetailSlnCacheBusinessLayer;
using RetailSlnEnumerations;
using RetailSlnModels;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        public static BusinessInfoModel BusinessInfoModel { set; get; }
        public static List<DemogInfoAddressModel> BusinessDemogInfoAddressModels { set; get; }
        public static List<CategoryModel> CategoryModels { set; get; }
        public static CultureInfo CurrencyCultureInfo { set; get; }
        public static string CurrencyDecimalPlaces { set; get; }
        public static string CurrencySymbol { set; get; }
        public static Dictionary<long, CategoryLayoutModel> CategoryLayoutModels { set; get; }
        public static List<CorpAcctModel> CorpAcctModels { set; get; }
        public static List<DiscountDtlModel> DiscountDtlModels { set; get; }
        public static List<FestivalListModel> FestivalListModels { set; get; }
        public static List<FestivalListImageModel> FestivalListImageModels { set; get; }
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
        public static List<CategoryItemMasterHierModel> CategoryItemMasterHierModels { set; get; }
        public static List<DemogInfoCountryModel> DeliveryDemogInfoCountryModels { set; get; }
        public static List<SelectListItem> DeliveryDemogInfoCountrySelectListItems { set; get; }
        public static Dictionary<long, List<SelectListItem>> DeliveryDemogInfoCountrySubDivisionSelectListItems { set; get; }
        public static List<ApiCodeDataModel> DeliveryMethods { set; get; }
        public static List<ApiCodeDataModel> PaymentMethodsCreditSale { set; get; }
        public static List<ApiCodeDataModel> PaymentMethods { set; get; }
        public static long DefaultDeliveryDemogInfoCountryId { set; get; }
        public static List<KeyValuePair<long, string>> DeliveryDemogInfoCountrys { set; get; }
        public static List<KeyValuePair<long, List<KeyValuePair<long, string>>>> DeliveryDemogInfoCountryStates { set; get; }
        public static void Initialize(long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            RetailSlnCacheBL retailSlnCacheBL = new RetailSlnCacheBL();
            retailSlnCacheBL.Initialize(out RetailSlnInitModel retailSlnInitModel, clientId, ipAddress, execUniqueId, loggedInUserId);
            CategoryModels = retailSlnInitModel.CategoryModels;
            ItemBundleModels = retailSlnInitModel.ItemBundleModels;
            ItemBundleItemModels = retailSlnInitModel.ItemBundleItemModels;
            ItemMasterModels = retailSlnInitModel.ItemMasterModels;
            ItemModels = retailSlnInitModel.ItemModels;
            ItemSpecModels = retailSlnInitModel.ItemSpecModels;
            ItemMasterItemSpecModels = retailSlnInitModel.ItemMasterItemSpecModels;
            ItemInfoModels = retailSlnInitModel.ItemInfoModels;
            ItemImageModels = retailSlnInitModel.ItemImageModels;
            ItemSpecMasterModels = retailSlnInitModel.ItemSpecMasterModels;
            CategoryItemMasterHierModels = retailSlnInitModel.CategoryItemMasterHierModels;
            //DeliveryListModels = deliveryListModels;
            //DeliveryChargeModels = deliveryChargeModels;
            CorpAcctModels = retailSlnInitModel.CorpAcctModels;
            DiscountDtlModels = retailSlnInitModel.DiscountDtlModels;
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
            CategoryLayoutModels = retailSlnInitModel.CategoryLayoutModels;
            DeliveryMethods = retailSlnInitModel.DeliveryMethods;
            PaymentMethodsCreditSale = retailSlnInitModel.PaymentMethodsCreditSale;
            PaymentMethods = retailSlnInitModel.PaymentMethods;
            DeliveryDemogInfoCountrys = retailSlnInitModel.DeliveryCountrys;
            DeliveryDemogInfoCountryStates = retailSlnInitModel.DeliveryCountryStates;
            BusinessInfoModel = retailSlnInitModel.BusinessInfoModel;
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
            foreach (var categoryItemMasterHierModel in retailSlnInitModel.CategoryItemMasterHierModels)
            {
                categoryItemMasterHierModel.CategoryModel = retailSlnInitModel.CategoryModels.FirstOrDefault(x => x.CategoryId == categoryItemMasterHierModel.CategoryId);
                categoryItemMasterHierModel.ItemMasterModel = retailSlnInitModel.ItemMasterModels.FirstOrDefault(x => x.ItemMasterId == categoryItemMasterHierModel.ItemMasterId);
                categoryItemMasterHierModel.ParentCategoryModel = retailSlnInitModel.CategoryModels.FirstOrDefault(x => x.CategoryId == categoryItemMasterHierModel.ParentCategoryId);
            }
            retailSlnInitModel.CategoryLayoutModels = new Dictionary<long, CategoryLayoutModel>();
            foreach (var categoryModel in retailSlnInitModel.CategoryModels)
            {
                retailSlnInitModel.CategoryLayoutModels[(long)categoryModel.CategoryId] = new CategoryLayoutModel();
            }
            foreach (var categoryLayoutModel in retailSlnInitModel.CategoryLayoutModels)
            {
                categoryLayoutModel.Value.CategoryItemMasterHierModels = new List<CategoryItemMasterHierModel>();
                categoryLayoutModel.Value.CategoryItemMasterHierModels.AddRange(retailSlnInitModel.CategoryItemMasterHierModels.FindAll(x => x.ParentCategoryId == categoryLayoutModel.Key).OrderBy(y => y.SeqNum).ToList());
            }
            return;
        }
        private static void BuildCacheModels2(RetailSlnInitModel retailSlnInitModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            foreach (var itemSpecModel in retailSlnInitModel.ItemSpecModels)
            {
                itemSpecModel.ItemSpecMasterModel = retailSlnInitModel.ItemSpecMasterModels.First(x => x.ItemSpecMasterId == itemSpecModel.ItemSpecMasterId);
                itemSpecModel.ItemSpecValueForDisplay = /*itemSpecModel.ItemSpecMasterModel.SpecDesc + ": " +*/ itemSpecModel.ItemSpecValue;
                if (itemSpecModel.ItemSpecValue != "")
                {
                    if (itemSpecModel.ItemSpecMasterModel.CodeTypeId != null)
                    {
                        var abc1 = LookupCache.GetCodeDatasForCodeTypeIdByCodeDataNameId(itemSpecModel.ItemSpecMasterModel.CodeTypeId.Value, execUniqueId);
                        var abc2 = abc1.First(x => x.CodeDataNameId == long.Parse(itemSpecModel.ItemSpecUnitValue));
                        itemSpecModel.ItemSpecValueForDisplay += " " + abc2.CodeDataDesc0;
                    }
                }
            }
            int i = 0;
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
            foreach (var itemMasterModel in retailSlnInitModel.ItemMasterModels)
            {
                itemMasterModel.ItemModels = retailSlnInitModel.ItemModels.FindAll(x => x.ItemMasterId == itemMasterModel.ItemMasterId);
                itemMasterModel.ImageTitle = itemMasterModel.ItemMasterDesc + " #" + itemMasterModel.ItemMasterId;
                if (itemMasterModel.ItemMasterItemSpecModels == null)
                {
                    itemMasterModel.ItemMasterItemSpecModels = new List<ItemSpecModel>();
                }
                if (itemMasterModel.ItemMasterItemSpecModelsForDisplay == null)
                {
                    itemMasterModel.ItemMasterItemSpecModelsForDisplay = new Dictionary<string, ItemSpecModel>();
                }
            }
            foreach (var itemModel in retailSlnInitModel.ItemModels)
            {
                itemModel.ItemMasterModel = retailSlnInitModel.ItemMasterModels.First(x => x.ItemMasterId == itemModel.ItemMasterId);
                itemModel.ItemRateFormatted = itemModel.ItemRate.Value.ToString(CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                itemModel.ImageTitle = itemModel.ItemShortDesc + " #" + itemModel.ItemId;
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
                //foreach (var itemSpecModel in itemModel.ItemSpecModels)
                //{
                //    switch (itemSpecModel.ItemSpecMasterModel.SpecName)
                //    {
                //        case "HSNCode":
                //        case "ProductCode":
                //            itemModel.ItemSpecModelsForDisplay[itemSpecModel.ItemSpecMasterModel.SpecName] = itemSpecModel;
                //            itemModel.ImageTitle += " " + itemSpecModel.ItemSpecValueForDisplay;
                //            break;
                //        case "WeightCalc":
                //            itemModel.ImageTitle += " Wt: " + itemSpecModel.ItemSpecValue + " G";
                //            break;
                //        case "ProductOrVolumetricWeight":
                //            itemModel.ImageTitle += " Vol Wt: " + itemSpecModel.ItemSpecValue + " G";
                //            break;
                //    }
                //    //if (itemSpecModel.ShowValue)
                //    //{
                //    //    itemModel.ItemSpecModelsForDisplay[itemSpecModel.ItemSpecMasterModel.SpecName] = itemSpecModel;
                //    //}
                //}
            }
            return;
            //foreach (var joinOutputTemp in joinOutput)
            //{

            //}
            //var itemMasterModels = new List<ItemMasterModel>
            //{
            //    new ItemMasterModel
            //    {
            //        ItemMasterId = 1,
            //        ItemModels = retailSlnInitModel.ItemModels.Where(im => im.ItemMasterId == 1).ToList(),
            //        ItemMasterItemSpecModels = retailSlnInitModel.ItemSpecModels.Where(ism => retailSlnInitModel.ItemModels.Any(im => im.ItemId == ism.ItemId && im.ItemMasterId == 1)).ToList()
            //    },
            //    new ItemMasterModel
            //    {
            //        ItemMasterId = 2,
            //        ItemModels = retailSlnInitModel.ItemModels.Where(im => im.ItemMasterId == 2).ToList(),
            //        ItemMasterItemSpecModels = retailSlnInitModel.ItemSpecModels.Where(ism => retailSlnInitModel.ItemModels.Any(im => im.ItemId == ism.ItemId && im.ItemMasterId == 2)).ToList()
            //    }
            //};
            //// Create a dictionary to map ItemMasterId to the corresponding ItemSpecModels
            //var itemMasterSpecMapping = retailSlnInitModel.ItemModels
            //    .GroupBy(im => im.ItemMasterId)
            //    .ToDictionary(
            //        g => g.Key,
            //        g => retailSlnInitModel.ItemSpecModels
            //            .Where(ism => g.Any(im => im.ItemId == ism.ItemId))
            //            .ToList()
            //    );
            //var joinOutput =
            //    (
            //        from a1 in retailSlnInitModel.ItemSpecModels
            //        join a2 in retailSlnInitModel.ItemModels
            //        on a1.ItemId equals a2.ItemId
            //        where a1.SeqNumItemMaster != null
            //        select new
            //        {
            //            c1 = a2.ItemMasterId,
            //            c2 = a1.SeqNumItemMaster,
            //            c3 = a1.ItemSpecId,
            //            c4 = itemMasterSpecMapping[a2.ItemMasterId]
            //        }
            //    ).OrderBy(x => x.c1).ThenBy(x => x.c2).ToList();

            //var joinOutput =
            //(
            //    from a1 in retailSlnInitModel.ItemSpecModels
            //    join a2 in retailSlnInitModel.ItemModels
            //    on a1.ItemId equals a2.ItemId
            //    where a1.SeqNumItemMaster != null
            //    group a1 by new { a2.ItemMasterId, a1.SeqNumItemMaster } into g
            //    let allSpecs = g.ToList()
            //    from spec in allSpecs
            //    select new
            //    {
            //        c1 = g.Key.ItemMasterId,
            //        c2 = g.Key.SeqNumItemMaster,
            //        c3 = spec.ItemSpecId,
            //        c4 = allSpecs
            //    }
            //).OrderBy(x => x.c1).ThenBy(x => x.c2).ToList();
            //
            //var joinOutput1 =
            //(
            //    from a1 in retailSlnInitModel.ItemSpecModels
            //    join a2 in retailSlnInitModel.ItemModels
            //    on a1.ItemId equals a2.ItemId
            //    join a3 in retailSlnInitModel.ItemMasterModels
            //    on a2.ItemMasterId equals a3.ItemMasterId
            //    where a1.SeqNumItemMaster != null
            //    group new { a2.ItemMasterId, a1.SeqNumItemMaster, a1.ItemSpecId }
            //    by new { a2.ItemMasterId, a1.SeqNumItemMaster }
            //    into g
            //    select new
            //    {
            //        c1 = g.Key.ItemMasterId,
            //        c2 = g.Key.SeqNumItemMaster,
            //        c3 = g.Min(x => x.ItemSpecId),
            //        c4 = g.ToList(),
            //        //c4 = a3.ItemMasterItemSpecModels = AddRange List<ItemSpecModel>()
            //    }
            //).OrderBy(x => x.c1).ThenBy(x => x.c2).ToList();
            //var joinOutput2 =
            //    (
            //        from a1 in retailSlnInitModel.ItemSpecModels
            //        join a2 in retailSlnInitModel.ItemModels
            //        on a1.ItemId equals a2.ItemId
            //        where a1.SeqNumItemMaster != null
            //        group a1 by new { a2.ItemMasterId, a1.SeqNumItemMaster } into g
            //        select new
            //        {
            //            c1 = g.Key.ItemMasterId,
            //            c2 = g.Key.SeqNumItemMaster,
            //            c3 = g.Min(x => x.ItemSpecId),
            //            c4 = g.ToList()
            //        }
            //    ).OrderBy(x => x.c1).ThenBy(x => x.c2).ToList();
        }
        private static void BuildCacheModels3(RetailSlnInitModel retailSlnInitModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            List<SelectListItem> deliveryDemogInfoSubDivisionSelectListItemsTemp;
            foreach (var corpAcctModel in retailSlnInitModel.CorpAcctModels)
            {
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
            retailSlnInitModel.BusinessInfoModel = new BusinessInfoModel
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
                itemBundleModel.ItemBundleItemModels = retailSlnInitModel.ItemBundleItemModels.FindAll(x => x.ItemBundleId == itemBundleModel.ItemBundleId);
                //itemBundleModel.ItemBundleItemModels.ForEach(x => x.DiscountPercent = itemBundleModel.DiscountPercent);
                //itemBundleModel.ItemModels = retailSlnInitModel.ItemModels.Where(a => retailSlnInitModel.ItemBundleItemModels.Where(b => b.ItemBundleId == itemBundleModel.ItemBundleId && b.ItemId == a.ItemId).Any()).ToList();
            }
            foreach (ItemBundleItemModel itemBundleItemModel in retailSlnInitModel.ItemBundleItemModels)
            {
                itemBundleItemModel.ItemRateBeforeDiscount = itemBundleItemModel.ItemModel.ItemRate.Value;
                itemBundleItemModel.ItemRateAfterDiscount = itemBundleItemModel.ItemRateBeforeDiscount * (100 - itemBundleItemModel.ItemBundleModel.DiscountPercent) / 100;
                itemBundleItemModel.ItemRateAfterDiscountFormatted = itemBundleItemModel.ItemRateAfterDiscount.ToString(CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
            }
            //SeqNum
            foreach (var festivalListModel in retailSlnInitModel.FestivalListModels)
            {
                festivalListModel.FestivalListImageModels = retailSlnInitModel.FestivalListImageModels.FindAll(x => x.FestivalListId == festivalListModel.FestivalListId);
            }
            return;
        }
        #region
        //private static void BuildCacheModels(List<CategoryModel> categoryModels, out Dictionary<long, CategoryLayoutModel> categoryLayoutModels, List<ItemMasterModel> itemMasterModels, List<ItemModel> itemModels, List<ItemBundleItemModel> itemBundleItemModels, List<ItemSpecModel> itemSpecModels, List<ItemInfoModel> itemInfoModels, List<ItemImageModel> itemImageModels, List<ItemSpecMasterModel> itemSpecMasterModels, List<ItemItemSpecModel> itemItemSpecModels, List<ItemMasterItemSpecModel> itemMasterItemSpecModels, List<CategoryItemMasterHierModel> categoryItemMasterHierModels, List<CorpAcctModel> corpAcctModels, List<DiscountDtlModel> discountDtlModels, List<FestivalListModel> festivalListModels, List<FestivalListImageModel> festivalListImageModels, out List<DemogInfoCountryModel> deliveryDemogInfoCountryModels, out List<SelectListItem> deliveryDemogInfoCountrySelectListItems, out Dictionary<long, List<SelectListItem>> deliveryDemogInfoCountrySubDivisionSelectListItems, out List<ApiCodeDataModel> deliveryMethods, out List<ApiCodeDataModel> paymentMethodsCreditSale, out List<ApiCodeDataModel> paymentMethods, out List<KeyValuePair<long, string>> deliveryCountrys, out List<KeyValuePair<long, List<KeyValuePair<long, string>>>> deliveryCountryStates, out BusinessInfoModel businessInfoModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    BuildCacheModels1(categoryModels, out categoryLayoutModels, categoryItemMasterHierModels, itemMasterModels, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    BuildCacheModels2(itemMasterModels, itemModels, itemSpecModels, itemInfoModels, itemImageModels, itemSpecMasterModels, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    BuildCacheModels3(corpAcctModels, discountDtlModels, out deliveryDemogInfoCountryModels, out deliveryDemogInfoCountrySelectListItems, out deliveryDemogInfoCountrySubDivisionSelectListItems, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    BuildCacheModels4(out deliveryMethods, out paymentMethodsCreditSale, out paymentMethods, out deliveryCountrys, out deliveryCountryStates, out businessInfoModel, deliveryDemogInfoCountryModels, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    BuildCacheModels5(itemBundleItemModels, festivalListModels, festivalListImageModels, clientId, ipAddress, execUniqueId, loggedInUserId);
        //}
        //private static void BuildCacheModels1(List<CategoryModel> categoryModels, out Dictionary<long, CategoryLayoutModel> categoryLayoutModels, List<CategoryItemMasterHierModel> categoryItemMasterHierModels, List<ItemMasterModel> itemMasterModels, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    foreach (var categoryItemMasterHierModel in categoryItemMasterHierModels)
        //    {
        //        categoryItemMasterHierModel.CategoryModel = categoryModels.FirstOrDefault(x => x.CategoryId == categoryItemMasterHierModel.CategoryId);
        //        categoryItemMasterHierModel.ItemMasterModel = itemMasterModels.FirstOrDefault(x => x.ItemMasterId == categoryItemMasterHierModel.ItemMasterId);
        //        categoryItemMasterHierModel.ParentCategoryModel = categoryModels.FirstOrDefault(x => x.CategoryId == categoryItemMasterHierModel.ParentCategoryId);
        //    }
        //    categoryLayoutModels = new Dictionary<long, CategoryLayoutModel>();
        //    foreach (var categoryModel in categoryModels)
        //    {
        //        categoryLayoutModels[(long)categoryModel.CategoryId] = new CategoryLayoutModel();
        //    }
        //    foreach (var categoryLayoutModel in categoryLayoutModels)
        //    {
        //        categoryLayoutModel.Value.CategoryItemMasterHierModels = new List<CategoryItemMasterHierModel>();
        //        categoryLayoutModel.Value.CategoryItemMasterHierModels.AddRange(categoryItemMasterHierModels.FindAll(x => x.ParentCategoryId == categoryLayoutModel.Key).OrderBy(y => y.SeqNum).ToList());
        //    }
        //    return;
        //}
        //private static void BuildCacheModels2(List<ItemMasterModel> itemMasterModels, List<ItemModel> itemModels, List<ItemSpecModel> itemSpecModels, List<ItemInfoModel> itemInfoModels, List<ItemImageModel> itemImageModels, List<ItemSpecMasterModel> itemSpecMasterModels, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    foreach (var itemSpecModel in itemSpecModels)
        //    {
        //        itemSpecModel.ItemSpecMasterModel = itemSpecMasterModels.First(x => x.ItemSpecMasterId == itemSpecModel.ItemSpecMasterId);
        //        itemSpecModel.ItemSpecValueForDisplay = itemSpecModel.ItemSpecMasterModel.SpecDesc + " " + itemSpecModel.ItemSpecValue;
        //        if (itemSpecModel.ItemSpecValue != "")
        //        {
        //            if (itemSpecModel.ItemSpecMasterModel.CodeTypeId != null)
        //            {
        //                var abc1 = LookupCache.GetCodeDatasForCodeTypeIdByCodeDataNameId(itemSpecModel.ItemSpecMasterModel.CodeTypeId.Value, execUniqueId);
        //                var abc2 = abc1.First(x => x.CodeDataNameId == long.Parse(itemSpecModel.ItemSpecUnitValue));
        //                itemSpecModel.ItemSpecValueForDisplay += " " + abc2.CodeDataDesc0;
        //            }
        //        }
        //    }
        //    foreach (var itemMasterModel in itemMasterModels)
        //    {
        //        itemMasterModel.ItemModels = itemModels.FindAll(x => x.ItemMasterId == itemMasterModel.ItemMasterId);
        //        itemMasterModel.ImageTitle = itemMasterModel.ItemMasterDesc + " #" + itemMasterModel.ItemMasterId;
        //    }
        //    foreach (var itemModel in itemModels)
        //    {
        //        itemModel.ItemRateFormatted = itemModel.ItemRate.Value.ToString(CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
        //        itemModel.ImageTitle = itemModel.ItemShortDesc + " #" + itemModel.ItemId;
        //        if (itemModel.ItemStatusId == ItemStatusEnum.OutOfStock)
        //        {
        //            itemModel.ImageTitle += " Sold Out";
        //            if (string.IsNullOrEmpty(itemModel.ExpectedAvailability))
        //            {
        //                itemModel.ExpectedAvailability = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");
        //            }
        //            itemModel.ExpectedAvailabilityFormatted = DateTime.Parse(itemModel.ExpectedAvailability).ToString("MMM-dd");
        //            itemModel.ImageTitle += " Expected to be available on " + itemModel.ExpectedAvailabilityFormatted;
        //        }
        //        itemModel.ItemSpecModels = itemSpecModels.FindAll(x => x.ItemId == itemModel.ItemId);
        //        itemModel.ItemInfoModels = itemInfoModels.FindAll(x => x.ItemId == itemModel.ItemId);
        //        itemModel.ItemImageModels = itemImageModels.FindAll(x => x.ItemId == itemModel.ItemId);
        //        itemModel.ItemSpecModelsForDisplay = new Dictionary<string, ItemSpecModel>();
        //        foreach (var itemSpecModel in itemModel.ItemSpecModels)
        //        {
        //            switch (itemSpecModel.ItemSpecMasterModel.SpecName)
        //            {
        //                case "HSNCode":
        //                case "ProductCode":
        //                    itemModel.ItemSpecModelsForDisplay[itemSpecModel.ItemSpecMasterModel.SpecName] = itemSpecModel;
        //                    itemModel.ImageTitle += " " + itemSpecModel.ItemSpecValueForDisplay;
        //                    break;
        //                case "WeightCalc":
        //                    itemModel.ImageTitle += " Wt: " + itemSpecModel.ItemSpecValue + " G";
        //                    break;
        //                case "ProductOrVolumetricWeight":
        //                    itemModel.ImageTitle += " Vol Wt: " + itemSpecModel.ItemSpecValue + " G";
        //                    break;
        //            }
        //            //if (itemSpecModel.ShowValue)
        //            //{
        //            //    itemModel.ItemSpecModelsForDisplay[itemSpecModel.ItemSpecMasterModel.SpecName] = itemSpecModel;
        //            //}
        //        }
        //    }
        //    return;
        //}
        //private static void BuildCacheModels2Backup(RetailSlnInitModel retailSlnInitModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    //Precompute the mapping of ItemMasterId to the corresponding ItemSpecModels
        //    var itemMasterSpecMapping = retailSlnInitModel.ItemModels
        //        .GroupBy(im => im.ItemMasterId)
        //        .ToDictionary(
        //            g => g.Key,
        //            g => retailSlnInitModel.ItemSpecModels
        //                .Where(ism => g.Any(im => im.ItemId == ism.ItemId))
        //                .ToList()
        //        );
        //    //Main query to get grouped results with minimum ItemSpecId
        //    var groupedResults =
        //        from a1 in retailSlnInitModel.ItemSpecModels
        //        join a2 in retailSlnInitModel.ItemModels
        //        on a1.ItemId equals a2.ItemId
        //        where a1.SeqNumItemMaster != null
        //        group new { a2.ItemMasterId, a1.SeqNumItemMaster, a1.ItemSpecId }
        //        by new { a2.ItemMasterId, a1.SeqNumItemMaster } into g
        //        select new
        //        {
        //            c1 = g.Key.ItemMasterId,
        //            c2 = g.Key.SeqNumItemMaster,
        //            c3 = g.Min(x => x.ItemSpecId)
        //        };
        //    //Create the final output with all related ItemSpecModels
        //    var joinOutput =
        //        from result in groupedResults
        //        let allSpecs = itemMasterSpecMapping[result.c1]
        //        select new
        //        {
        //            result.c1,
        //            result.c2,
        //            result.c3,
        //            c4 = allSpecs
        //        };
        //    foreach (var itemSpecModel in retailSlnInitModel.ItemSpecModels)
        //    {
        //        itemSpecModel.ItemSpecMasterModel = retailSlnInitModel.ItemSpecMasterModels.First(x => x.ItemSpecMasterId == itemSpecModel.ItemSpecMasterId);
        //        itemSpecModel.ItemSpecValueForDisplay = itemSpecModel.ItemSpecMasterModel.SpecDesc + " " + itemSpecModel.ItemSpecValue;
        //        if (itemSpecModel.ItemSpecValue != "")
        //        {
        //            if (itemSpecModel.ItemSpecMasterModel.CodeTypeId != null)
        //            {
        //                var abc1 = LookupCache.GetCodeDatasForCodeTypeIdByCodeDataNameId(itemSpecModel.ItemSpecMasterModel.CodeTypeId.Value, execUniqueId);
        //                var abc2 = abc1.First(x => x.CodeDataNameId == long.Parse(itemSpecModel.ItemSpecUnitValue));
        //                itemSpecModel.ItemSpecValueForDisplay += " " + abc2.CodeDataDesc0;
        //            }
        //        }
        //    }
        //    foreach (var itemMasterModel in retailSlnInitModel.ItemMasterModels)
        //    {
        //        itemMasterModel.ItemModels = retailSlnInitModel.ItemModels.FindAll(x => x.ItemMasterId == itemMasterModel.ItemMasterId);
        //        itemMasterModel.ImageTitle = itemMasterModel.ItemMasterDesc + " #" + itemMasterModel.ItemMasterId;
        //        //itemMasterModel.ItemMasterItemSpecModels = joinOutput.First(x => x.c1 == itemMasterModel.ItemMasterId).c4;
        //    }
        //    foreach (var joinOutputTemp in joinOutput)
        //    {
        //        var itemMasterModelTemp = retailSlnInitModel.ItemMasterModels.First(x => x.ItemMasterId == joinOutputTemp.c1);
        //        if (itemMasterModelTemp.ItemMasterItemSpecModels == null)
        //        {
        //            itemMasterModelTemp.ItemMasterItemSpecModels = joinOutputTemp.c4.FindAll(x => x.SeqNumItemMaster != null);
        //        }
        //    }
        //    foreach (var itemModel in retailSlnInitModel.ItemModels)
        //    {
        //        itemModel.ItemRateFormatted = itemModel.ItemRate.Value.ToString(CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
        //        itemModel.ImageTitle = itemModel.ItemShortDesc + " #" + itemModel.ItemId;
        //        if (itemModel.ItemStatusId == ItemStatusEnum.OutOfStock)
        //        {
        //            itemModel.ImageTitle += " Sold Out";
        //            if (string.IsNullOrEmpty(itemModel.ExpectedAvailability))
        //            {
        //                itemModel.ExpectedAvailability = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");
        //            }
        //            itemModel.ExpectedAvailabilityFormatted = DateTime.Parse(itemModel.ExpectedAvailability).ToString("MMM-dd");
        //            itemModel.ImageTitle += " Expected to be available on " + itemModel.ExpectedAvailabilityFormatted;
        //        }
        //        itemModel.ItemSpecModels = retailSlnInitModel.ItemSpecModels.FindAll(x => x.ItemId == itemModel.ItemId);
        //        itemModel.ItemItemSpecModels = retailSlnInitModel.ItemSpecModels.FindAll(x => x.ItemId == itemModel.ItemId && x.SeqNumItem != null).OrderBy(y => y.SeqNumItem).ToList();
        //        itemModel.ItemInfoModels = retailSlnInitModel.ItemInfoModels.FindAll(x => x.ItemId == itemModel.ItemId);
        //        itemModel.ItemImageModels = retailSlnInitModel.ItemImageModels.FindAll(x => x.ItemId == itemModel.ItemId);
        //        itemModel.ItemSpecModelsForDisplay = new Dictionary<string, ItemSpecModel>();
        //        foreach (var itemSpecModel in itemModel.ItemSpecModels)
        //        {
        //            switch (itemSpecModel.ItemSpecMasterModel.SpecName)
        //            {
        //                case "HSNCode":
        //                case "ProductCode":
        //                    itemModel.ItemSpecModelsForDisplay[itemSpecModel.ItemSpecMasterModel.SpecName] = itemSpecModel;
        //                    itemModel.ImageTitle += " " + itemSpecModel.ItemSpecValueForDisplay;
        //                    break;
        //                case "WeightCalc":
        //                    itemModel.ImageTitle += " Wt: " + itemSpecModel.ItemSpecValue + " G";
        //                    break;
        //                case "ProductOrVolumetricWeight":
        //                    itemModel.ImageTitle += " Vol Wt: " + itemSpecModel.ItemSpecValue + " G";
        //                    break;
        //            }
        //            //if (itemSpecModel.ShowValue)
        //            //{
        //            //    itemModel.ItemSpecModelsForDisplay[itemSpecModel.ItemSpecMasterModel.SpecName] = itemSpecModel;
        //            //}
        //        }
        //    }
        //    return;
        //    //foreach (var joinOutputTemp in joinOutput)
        //    //{

        //    //}
        //    //var itemMasterModels = new List<ItemMasterModel>
        //    //{
        //    //    new ItemMasterModel
        //    //    {
        //    //        ItemMasterId = 1,
        //    //        ItemModels = retailSlnInitModel.ItemModels.Where(im => im.ItemMasterId == 1).ToList(),
        //    //        ItemMasterItemSpecModels = retailSlnInitModel.ItemSpecModels.Where(ism => retailSlnInitModel.ItemModels.Any(im => im.ItemId == ism.ItemId && im.ItemMasterId == 1)).ToList()
        //    //    },
        //    //    new ItemMasterModel
        //    //    {
        //    //        ItemMasterId = 2,
        //    //        ItemModels = retailSlnInitModel.ItemModels.Where(im => im.ItemMasterId == 2).ToList(),
        //    //        ItemMasterItemSpecModels = retailSlnInitModel.ItemSpecModels.Where(ism => retailSlnInitModel.ItemModels.Any(im => im.ItemId == ism.ItemId && im.ItemMasterId == 2)).ToList()
        //    //    }
        //    //};
        //    //// Create a dictionary to map ItemMasterId to the corresponding ItemSpecModels
        //    //var itemMasterSpecMapping = retailSlnInitModel.ItemModels
        //    //    .GroupBy(im => im.ItemMasterId)
        //    //    .ToDictionary(
        //    //        g => g.Key,
        //    //        g => retailSlnInitModel.ItemSpecModels
        //    //            .Where(ism => g.Any(im => im.ItemId == ism.ItemId))
        //    //            .ToList()
        //    //    );
        //    //var joinOutput =
        //    //    (
        //    //        from a1 in retailSlnInitModel.ItemSpecModels
        //    //        join a2 in retailSlnInitModel.ItemModels
        //    //        on a1.ItemId equals a2.ItemId
        //    //        where a1.SeqNumItemMaster != null
        //    //        select new
        //    //        {
        //    //            c1 = a2.ItemMasterId,
        //    //            c2 = a1.SeqNumItemMaster,
        //    //            c3 = a1.ItemSpecId,
        //    //            c4 = itemMasterSpecMapping[a2.ItemMasterId]
        //    //        }
        //    //    ).OrderBy(x => x.c1).ThenBy(x => x.c2).ToList();

        //    //var joinOutput =
        //    //(
        //    //    from a1 in retailSlnInitModel.ItemSpecModels
        //    //    join a2 in retailSlnInitModel.ItemModels
        //    //    on a1.ItemId equals a2.ItemId
        //    //    where a1.SeqNumItemMaster != null
        //    //    group a1 by new { a2.ItemMasterId, a1.SeqNumItemMaster } into g
        //    //    let allSpecs = g.ToList()
        //    //    from spec in allSpecs
        //    //    select new
        //    //    {
        //    //        c1 = g.Key.ItemMasterId,
        //    //        c2 = g.Key.SeqNumItemMaster,
        //    //        c3 = spec.ItemSpecId,
        //    //        c4 = allSpecs
        //    //    }
        //    //).OrderBy(x => x.c1).ThenBy(x => x.c2).ToList();
        //    //
        //    //var joinOutput1 =
        //    //(
        //    //    from a1 in retailSlnInitModel.ItemSpecModels
        //    //    join a2 in retailSlnInitModel.ItemModels
        //    //    on a1.ItemId equals a2.ItemId
        //    //    join a3 in retailSlnInitModel.ItemMasterModels
        //    //    on a2.ItemMasterId equals a3.ItemMasterId
        //    //    where a1.SeqNumItemMaster != null
        //    //    group new { a2.ItemMasterId, a1.SeqNumItemMaster, a1.ItemSpecId }
        //    //    by new { a2.ItemMasterId, a1.SeqNumItemMaster }
        //    //    into g
        //    //    select new
        //    //    {
        //    //        c1 = g.Key.ItemMasterId,
        //    //        c2 = g.Key.SeqNumItemMaster,
        //    //        c3 = g.Min(x => x.ItemSpecId),
        //    //        c4 = g.ToList(),
        //    //        //c4 = a3.ItemMasterItemSpecModels = AddRange List<ItemSpecModel>()
        //    //    }
        //    //).OrderBy(x => x.c1).ThenBy(x => x.c2).ToList();
        //    //var joinOutput2 =
        //    //    (
        //    //        from a1 in retailSlnInitModel.ItemSpecModels
        //    //        join a2 in retailSlnInitModel.ItemModels
        //    //        on a1.ItemId equals a2.ItemId
        //    //        where a1.SeqNumItemMaster != null
        //    //        group a1 by new { a2.ItemMasterId, a1.SeqNumItemMaster } into g
        //    //        select new
        //    //        {
        //    //            c1 = g.Key.ItemMasterId,
        //    //            c2 = g.Key.SeqNumItemMaster,
        //    //            c3 = g.Min(x => x.ItemSpecId),
        //    //            c4 = g.ToList()
        //    //        }
        //    //    ).OrderBy(x => x.c1).ThenBy(x => x.c2).ToList();
        //}        //private static void BuildCacheModels3(List<CorpAcctModel> corpAcctModels, List<DiscountDtlModel> discountDtlModels, out List<DemogInfoCountryModel> deliveryDemogInfoCountryModels, out List<SelectListItem> deliveryDemogInfoCountrySelectListItems, out Dictionary<long, List<SelectListItem>> deliveryDemogInfoCountrySubDivisionSelectListItems, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    List<SelectListItem> deliveryDemogInfoSubDivisionSelectListItemsTemp;
        //    foreach (var corpAcctModel in corpAcctModels)
        //    {
        //        corpAcctModel.DiscountDtlModels = new List<DiscountDtlModel>();
        //        corpAcctModel.DiscountDtlModels.AddRange(discountDtlModels.FindAll(x => x.CorpAcctId == corpAcctModel.CorpAcctId));
        //    }
        //    DemogInfoCountryModel demogInfoCountryModel;
        //    deliveryDemogInfoCountryModels = new List<DemogInfoCountryModel>();
        //    deliveryDemogInfoCountrySelectListItems = new List<SelectListItem>();
        //    deliveryDemogInfoCountrySubDivisionSelectListItems = new Dictionary<long, List<SelectListItem>>();
        //    string deliveryInfoDemogInfoCountryIds = ArchLibCache.GetApplicationDefault(clientId, "DeliveryInfo", "DemogInfoCountryIds");
        //    foreach (var deliveryInfoDemogInfoCountryId in deliveryInfoDemogInfoCountryIds.Split(';'))
        //    {
        //        deliveryDemogInfoCountryModels.Add(demogInfoCountryModel = DemogInfoCache.DemogInfoCountryModels.First(x => x.DemogInfoCountryId == long.Parse(deliveryInfoDemogInfoCountryId)));
        //        deliveryDemogInfoCountrySubDivisionSelectListItems[demogInfoCountryModel.DemogInfoCountryId] = (deliveryDemogInfoSubDivisionSelectListItemsTemp = new List<SelectListItem>());
        //        deliveryDemogInfoCountrySelectListItems.Add
        //        (
        //            new SelectListItem
        //            {
        //                Text = demogInfoCountryModel.CountryDesc + " " + demogInfoCountryModel.CountryAbbrev,
        //                Value = demogInfoCountryModel.DemogInfoCountryId.ToString(),
        //            }
        //        );
        //    }
        //    return;
        //}
        //private static void BuildCacheModels4(out List<ApiCodeDataModel> deliveryMethods, out List<ApiCodeDataModel> paymentMethodsCreditSale, out List<ApiCodeDataModel> paymentMethods, out List<KeyValuePair<long, string>> deliveryCountrys, out List<KeyValuePair<long, List<KeyValuePair<long, string>>>> deliveryCountryStates, out BusinessInfoModel businessInfoModel, List<DemogInfoCountryModel> deliveryDemogInfoCountryModels, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    deliveryMethods = new List<ApiCodeDataModel>();
        //    var codeDataModels = LookupCache.CodeTypeModels.First(x => x.CodeTypeNameDesc == "DeliveryMethod").CodeDataModelsCodeDataNameId;
        //    foreach (var codeDataModel in codeDataModels)
        //    {
        //        deliveryMethods.Add
        //        (
        //            new ApiCodeDataModel
        //            {
        //                CodeDataId = codeDataModel.CodeDataId,
        //                CodeTypeId = codeDataModel.CodeTypeId,
        //                CodeTypeNameId = codeDataModel.CodeTypeNameId,
        //                CodeDataNameId = codeDataModel.CodeDataNameId,
        //                CodeDataNameDesc = codeDataModel.CodeDataNameDesc,
        //                CodeDataDesc0 = codeDataModel.CodeDataDesc0,
        //                CodeDataDesc1 = codeDataModel.CodeDataDesc1,
        //                CodeDataDesc2 = codeDataModel.CodeDataDesc2,
        //                CodeDataDesc3 = codeDataModel.CodeDataDesc3,
        //                CodeDataDesc4 = codeDataModel.CodeDataDesc4,
        //                CodeTypeModel = null,
        //            }
        //        );
        //    }
        //    codeDataModels = LookupCache.CodeTypeModels.First(x => x.CodeTypeNameDesc == "PaymentMode").CodeDataModelsCodeDataNameId;
        //    paymentMethodsCreditSale = new List<ApiCodeDataModel>();
        //    int i;
        //    for (i = 0; i < 1; i++)
        //    {
        //        paymentMethodsCreditSale.Add
        //        (
        //            new ApiCodeDataModel
        //            {
        //                CodeDataId = codeDataModels[i].CodeDataId,
        //                CodeTypeId = codeDataModels[i].CodeTypeId,
        //                CodeTypeNameId = codeDataModels[i].CodeTypeNameId,
        //                CodeDataNameId = codeDataModels[i].CodeDataNameId,
        //                CodeDataNameDesc = codeDataModels[i].CodeDataNameDesc,
        //                CodeDataDesc0 = codeDataModels[i].CodeDataDesc0,
        //                CodeDataDesc1 = codeDataModels[i].CodeDataDesc1,
        //                CodeDataDesc2 = codeDataModels[i].CodeDataDesc2,
        //                CodeDataDesc3 = codeDataModels[i].CodeDataDesc3,
        //                CodeDataDesc4 = codeDataModels[i].CodeDataDesc4,
        //                CodeTypeModel = null,
        //            }
        //        );
        //    }
        //    paymentMethods = new List<ApiCodeDataModel>();
        //    for (i = 1; i < codeDataModels.Count; i++)
        //    {
        //        paymentMethods.Add
        //        (
        //            new ApiCodeDataModel
        //            {
        //                CodeDataId = codeDataModels[i].CodeDataId,
        //                CodeTypeId = codeDataModels[i].CodeTypeId,
        //                CodeTypeNameId = codeDataModels[i].CodeTypeNameId,
        //                CodeDataNameId = codeDataModels[i].CodeDataNameId,
        //                CodeDataNameDesc = codeDataModels[i].CodeDataNameDesc,
        //                CodeDataDesc0 = codeDataModels[i].CodeDataDesc0,
        //                CodeDataDesc1 = codeDataModels[i].CodeDataDesc1,
        //                CodeDataDesc2 = codeDataModels[i].CodeDataDesc2,
        //                CodeDataDesc3 = codeDataModels[i].CodeDataDesc3,
        //                CodeDataDesc4 = codeDataModels[i].CodeDataDesc4,
        //                CodeTypeModel = null,
        //            }
        //        );
        //    }
        //    deliveryCountrys = new List<KeyValuePair<long, string>>();
        //    deliveryCountryStates = new List<KeyValuePair<long, List<KeyValuePair<long, string>>>>();
        //    List<KeyValuePair<long, string>> deliveryStates;
        //    foreach (var deliveryDemogInfoCountryModel in deliveryDemogInfoCountryModels)
        //    {
        //        deliveryCountrys.Add(new KeyValuePair<long, string>(deliveryDemogInfoCountryModel.DemogInfoCountryId, deliveryDemogInfoCountryModel.CountryDesc));
        //        deliveryStates = new List<KeyValuePair<long, string>>();
        //        deliveryCountryStates.Add(new KeyValuePair<long, List<KeyValuePair<long, string>>>(deliveryDemogInfoCountryModel.DemogInfoCountryId, deliveryStates));
        //        var demogInfoSubDivisionModels = DemogInfoCache.DemogInfoSubDivisionModels.FindAll(x => x.DemogInfoCountryId == deliveryDemogInfoCountryModel.DemogInfoCountryId);
        //        foreach (var demogInfoSubDivisionModel in demogInfoSubDivisionModels)
        //        {
        //            deliveryStates.Add(new KeyValuePair<long, string>(deliveryDemogInfoCountryModel.DemogInfoCountryId, demogInfoSubDivisionModel.SubDivisionDesc));
        //        }
        //    }
        //    businessInfoModel = new BusinessInfoModel
        //    {
        //        ClientId = clientId,
        //        BaseUrl = ArchLibCache.GetApplicationDefault(clientId, "BaseUrl", ""),
        //        BusinessName1 = ArchLibCache.GetApplicationDefault(clientId, "BusinessName1", ""),
        //        BusinessName2 = ArchLibCache.GetApplicationDefault(clientId, "BusinessName2", ""),
        //        BusinessType = ArchLibCache.GetApplicationDefault(clientId, "BusinessType", ""),
        //        ContactPhoneFormatted = ArchLibCache.GetApplicationDefault(clientId, "ContactPhoneFormatted", ""),
        //        ContactPhoneHref = ArchLibCache.GetApplicationDefault(clientId, "ContactPhoneHref", ""),
        //        ContactTextPhoneFormatted = ArchLibCache.GetApplicationDefault(clientId, "ContactTextPhoneFormatted", ""),
        //        ContactTextPhoneHref = ArchLibCache.GetApplicationDefault(clientId, "ContactTextPhoneHref", ""),
        //        ContactWhatsAppPhone = ArchLibCache.GetApplicationDefault(clientId, "ContactWhatsAppPhone", ""),
        //        ContactWhatsAppPhoneFormatted = ArchLibCache.GetApplicationDefault(clientId, "ContactWhatsAppPhoneFormatted", ""),
        //        DemogInfoAddressModels = new List<DemogInfoAddressModel>
        //        {
        //            new DemogInfoAddressModel
        //            {
        //                AddressLine1 = ArchLibCache.GetApplicationDefault(clientId, "AddressLine1", ""),
        //                AddressLine2 = ArchLibCache.GetApplicationDefault(clientId, "AddressLine1A", ""),
        //                AddressLine3 = ArchLibCache.GetApplicationDefault(clientId, "AddressLine2", ""),
        //                CityName = ArchLibCache.GetApplicationDefault(clientId, "AddressCityName", ""),
        //                ZipCode = ArchLibCache.GetApplicationDefault(clientId, "AddressZipCode", ""),
        //                StateAbbrev = ArchLibCache.GetApplicationDefault(clientId, "AddressStateAbbrev", ""),
        //                CountryDesc = ArchLibCache.GetApplicationDefault(clientId, "AddressCountryName", ""),
        //            },
        //        },
        //        LogoImageName = "Image_000.webp",
        //        LogoRelativeUrl = "/ClientSpecific/" + clientId + "_" + ArchLibCache.ClientName + "/Documents/Images/",
        //        WhatsAppUrl = "https://api.whatsapp.com/send?phone=",
        //    };
        //    businessInfoModel.LogoImageFullUrl = businessInfoModel.BaseUrl + businessInfoModel.LogoRelativeUrl + businessInfoModel.LogoImageName;
        //    businessInfoModel.PhoneImageFullUrl = businessInfoModel.BaseUrl + "Images/Phone1_Small.png";
        //    businessInfoModel.SMSImageFullUrl = businessInfoModel.BaseUrl + "Images/SMSIcon3_Small.png";
        //    businessInfoModel.WhatsAppImageFullUrl = businessInfoModel.BaseUrl + "Images/WhatsApp1_Small.png";
        //    return;
        //}
        //private static void BuildCacheModels5(List<ItemBundleItemModel> itemBundleItemModels, List<FestivalListModel> festivalListModels, List<FestivalListImageModel> festivalListImageModels, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    foreach (var itemBundleItemModel in itemBundleItemModels)
        //    {
        //        itemBundleItemModel.BundleItemModel = ItemModels.First(x => x.ItemId == itemBundleItemModel.BundleItemId);
        //        itemBundleItemModel.ItemModel = ItemModels.First(x => x.ItemId == itemBundleItemModel.ItemId);
        //    }
        //    foreach (var festivalListModel in festivalListModels)
        //    {
        //        festivalListModel.FestivalListImageModels = festivalListImageModels.FindAll(x => x.FestivalListId == festivalListModel.FestivalListId);
        //    }
        //    return;
        //}
        #endregion
    }
}
