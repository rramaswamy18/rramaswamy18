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
        public static List<ItemModel> ItemModels { set; get; }
        public static List<ItemSpecMasterModel> ItemSpecMasterModels { set; get; }
        public static List<ItemSpecModel> ItemSpecModels { set; get; }
        public static List<ItemInfoModel> ItemInfoModels { set; get; }
        public static List<ItemImageModel> ItemImageModels { set; get; }
        public static List<ItemBundleItemModel> ItemBundleItemModels { set; get; }
        public static List<ItemBundleDiscountModel> ItemBundleDiscountModels { set; get; }
        public static List<CategoryItemHierModel> CategoryItemHierModels { set; get; }
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
            retailSlnCacheBL.Initialize(out List<CategoryModel> categoryModels, out List<ItemModel> itemModels, out List<ItemSpecModel> itemSpecModels, out List<ItemSpecMasterModel> itemSpecMasterModels, out List<ItemInfoModel> itemInfoModels, out List<ItemImageModel> itemImageModels, out List<ItemBundleItemModel> itemBundleItemModels, out List<ItemBundleDiscountModel> itemBundleDiscountModels, out List<CategoryItemHierModel> categoryItemHierModels, out List<CorpAcctModel> corpAcctModels, out List<DiscountDtlModel> discountDtlModels, clientId, ipAddress, execUniqueId, loggedInUserId);
            CategoryModels = categoryModels;
            ItemBundleItemModels = itemBundleItemModels;
            ItemModels = itemModels;
            ItemSpecModels = itemSpecModels;
            ItemInfoModels = itemInfoModels;
            ItemImageModels = itemImageModels;
            ItemSpecMasterModels = itemSpecMasterModels;
            CategoryItemHierModels = categoryItemHierModels;
            //DeliveryListModels = deliveryListModels;
            //DeliveryChargeModels = deliveryChargeModels;
            CorpAcctModels = corpAcctModels;
            DiscountDtlModels = discountDtlModels;
            CurrencyCultureInfo = new CultureInfo(ArchLibCache.GetApplicationDefault(clientId, "Currency", "CultureInfo"));
            CurrencyDecimalPlaces = ArchLibCache.GetApplicationDefault(clientId, "Currency", "CurrencyDecimalPlaces");
            var regionInfo = new RegionInfo(ArchLibCache.GetApplicationDefault(clientId, "Currency", "CultureInfo"));
            CurrencySymbol = regionInfo.CurrencySymbol;
            BuildCacheModels(categoryModels, out Dictionary<long, CategoryLayoutModel> categoryLayoutModels, itemModels, itemBundleItemModels, itemSpecModels, itemInfoModels, itemImageModels, itemSpecMasterModels, categoryItemHierModels, corpAcctModels, discountDtlModels, out List<DemogInfoCountryModel> deliveryDemogInfoCountryModels, out List<SelectListItem> deliveryDemogInfoCountrySelectListItems, out Dictionary<long, List<SelectListItem>> deliveryDemogInfoSubDivisionSelectListItems, out List<ApiCodeDataModel> deliveryMethods, out List<ApiCodeDataModel> paymentMethodsCreditSale, out List<ApiCodeDataModel> paymentMethods, out List<KeyValuePair<long, string>> deliveryCountrys, out List<KeyValuePair<long, List<KeyValuePair<long, string>>>> deliveryCountryStates, out BusinessInfoModel businessInfoModel, clientId, ipAddress, execUniqueId, loggedInUserId);
            DeliveryDemogInfoCountryModels = deliveryDemogInfoCountryModels;
            DeliveryDemogInfoCountrySelectListItems = deliveryDemogInfoCountrySelectListItems;
            DeliveryDemogInfoCountrySubDivisionSelectListItems = deliveryDemogInfoSubDivisionSelectListItems;
            DefaultDeliveryDemogInfoCountryId = long.Parse(ArchLibCache.GetApplicationDefault(clientId, "DeliveryInfo", "DefaultDemogInfoCountry"));
            CategoryLayoutModels = categoryLayoutModels;
            DeliveryMethods = deliveryMethods;
            PaymentMethodsCreditSale = paymentMethodsCreditSale;
            PaymentMethods = paymentMethods;
            DeliveryDemogInfoCountrys = deliveryCountrys;
            DeliveryDemogInfoCountryStates = deliveryCountryStates;
            BusinessInfoModel = businessInfoModel;
        }
        private static void BuildCacheModels(List<CategoryModel> categoryModels, out Dictionary<long, CategoryLayoutModel> categoryLayoutModels, List<ItemModel> itemModels, List<ItemBundleItemModel> itemBundleItemModels, List<ItemSpecModel> itemSpecModels, List<ItemInfoModel> itemInfoModels, List<ItemImageModel> itemImageModels, List<ItemSpecMasterModel> itemSpecMasterModels, List<CategoryItemHierModel> categoryItemHierModels, List<CorpAcctModel> corpAcctModels, List<DiscountDtlModel> discountDtlModels, out List<DemogInfoCountryModel> deliveryDemogInfoCountryModels, out List<SelectListItem> deliveryDemogInfoCountrySelectListItems, out Dictionary<long, List<SelectListItem>> deliveryDemogInfoCountrySubDivisionSelectListItems, out List<ApiCodeDataModel> deliveryMethods, out List<ApiCodeDataModel> paymentMethodsCreditSale, out List<ApiCodeDataModel> paymentMethods, out List<KeyValuePair<long, string>> deliveryCountrys, out List<KeyValuePair<long, List<KeyValuePair<long, string>>>> deliveryCountryStates, out BusinessInfoModel businessInfoModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
            foreach (var itemSpecModel in itemSpecModels)
            {
                itemSpecModel.ItemSpecMasterModel = itemSpecMasterModels.First(x => x.ItemSpecMasterId == itemSpecModel.ItemSpecMasterId);
                itemSpecModel.ItemSpecValueForDisplay = itemSpecModel.ItemSpecMasterModel.SpecDesc + " " + itemSpecModel.ItemSpecValue;
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
            foreach (var itemModel in itemModels)
            {
                itemModel.ItemRateFormatted = itemModel.ItemRate.Value.ToString(CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                itemModel.ImageTitle = itemModel.ItemShortDesc0 + " " + itemModel.ItemShortDesc1;
                itemModel.ImageTitle += string.IsNullOrWhiteSpace(itemModel.ItemShortDesc2) ? "" : " " + itemModel.ItemShortDesc2;
                itemModel.ImageTitle += string.IsNullOrWhiteSpace(itemModel.ItemShortDesc3) ? "" : " " + itemModel.ItemShortDesc3;
                itemModel.ItemShortDesc = itemModel.ImageTitle;
                itemModel.ImageTitle += " #" + itemModel.ItemId;
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
                itemModel.ItemSpecModels = itemSpecModels.FindAll(x => x.ItemId == itemModel.ItemId);
                itemModel.ItemInfoModels = itemInfoModels.FindAll(x => x.ItemId == itemModel.ItemId);
                itemModel.ItemImageModels = itemImageModels.FindAll(x => x.ItemId == itemModel.ItemId);
                itemModel.ItemSpecModelsForDisplay = new Dictionary<string, ItemSpecModel>();
                foreach (var itemSpecModel in itemModel.ItemSpecModels)
                {
                    switch (itemSpecModel.ItemSpecMasterModel.SpecName)
                    {
                        case "HSNCode":
                        case "ProductCode":
                            itemModel.ItemSpecModelsForDisplay[itemSpecModel.ItemSpecMasterModel.SpecName] = itemSpecModel;
                            itemModel.ImageTitle += " " + itemSpecModel.ItemSpecValueForDisplay;
                            break;
                        case "WeightCalc":
                            itemModel.ImageTitle += " Wt: " + itemSpecModel.ItemSpecValue + " G";
                            break;
                        case "ProductOrVolumetricWeight":
                            itemModel.ImageTitle += " Vol Wt: " + itemSpecModel.ItemSpecValue + " G";
                            break;
                    }
                    if (itemSpecModel.ShowValue)
                    {
                        itemModel.ItemSpecModelsForDisplay[itemSpecModel.ItemSpecMasterModel.SpecName] = itemSpecModel;
                    }
                }
            }
            //
            foreach (var corpAcctModel in corpAcctModels)
            {
                corpAcctModel.DiscountDtlModels = new List<DiscountDtlModel>();
                corpAcctModel.DiscountDtlModels.AddRange(discountDtlModels.FindAll(x => x.CorpAcctId == corpAcctModel.CorpAcctId));
            }
            DemogInfoCountryModel demogInfoCountryModel;
            deliveryDemogInfoCountryModels = new List<DemogInfoCountryModel>();
            deliveryDemogInfoCountrySelectListItems = new List<SelectListItem>();
            deliveryDemogInfoCountrySubDivisionSelectListItems = new Dictionary<long, List<SelectListItem>>();
            string deliveryInfoDemogInfoCountryIds = ArchLibCache.GetApplicationDefault(clientId, "DeliveryInfo", "DemogInfoCountryIds");
            List<SelectListItem> deliveryDemogInfoSubDivisionSelectListItems;
            foreach (var deliveryInfoDemogInfoCountryId in deliveryInfoDemogInfoCountryIds.Split(';'))
            {
                deliveryDemogInfoCountryModels.Add(demogInfoCountryModel = DemogInfoCache.DemogInfoCountryModels.First(x => x.DemogInfoCountryId == long.Parse(deliveryInfoDemogInfoCountryId)));
                deliveryDemogInfoCountrySubDivisionSelectListItems[demogInfoCountryModel.DemogInfoCountryId] = (deliveryDemogInfoSubDivisionSelectListItems = new List<SelectListItem>());
                deliveryDemogInfoCountrySelectListItems.Add
                (
                    new SelectListItem
                    {
                        Text = demogInfoCountryModel.CountryDesc + " " + demogInfoCountryModel.CountryAbbrev,
                        Value = demogInfoCountryModel.DemogInfoCountryId.ToString(),
                    }
                );
            }
            deliveryMethods = new List<ApiCodeDataModel>();
            var codeDataModels = LookupCache.CodeTypeModels.First(x => x.CodeTypeNameDesc == "DeliveryMethod").CodeDataModelsCodeDataNameId;
            foreach (var codeDataModel in codeDataModels)
            {
                deliveryMethods.Add
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
            paymentMethodsCreditSale = new List<ApiCodeDataModel>();
            int i;
            for (i = 0; i < 1; i++)
            {
                paymentMethodsCreditSale.Add
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
            paymentMethods = new List<ApiCodeDataModel>();
            for (i = 1; i < codeDataModels.Count; i++)
            {
                paymentMethods.Add
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
            deliveryCountrys = new List<KeyValuePair<long, string>>();
            deliveryCountryStates = new List<KeyValuePair<long, List<KeyValuePair<long, string>>>>();
            List<KeyValuePair<long, string>> deliveryStates;
            foreach (var deliveryDemogInfoCountryModel in deliveryDemogInfoCountryModels)
            {
                deliveryCountrys.Add(new KeyValuePair<long, string>(deliveryDemogInfoCountryModel.DemogInfoCountryId, deliveryDemogInfoCountryModel.CountryDesc));
                deliveryStates = new List<KeyValuePair<long, string>>();
                deliveryCountryStates.Add(new KeyValuePair<long, List<KeyValuePair<long, string>>>(deliveryDemogInfoCountryModel.DemogInfoCountryId, deliveryStates));
                var demogInfoSubDivisionModels = DemogInfoCache.DemogInfoSubDivisionModels.FindAll(x => x.DemogInfoCountryId == deliveryDemogInfoCountryModel.DemogInfoCountryId);
                foreach (var demogInfoSubDivisionModel in demogInfoSubDivisionModels)
                {
                    deliveryStates.Add(new KeyValuePair<long, string>(deliveryDemogInfoCountryModel.DemogInfoCountryId, demogInfoSubDivisionModel.SubDivisionDesc));
                }
            }
            businessInfoModel = new BusinessInfoModel
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
            businessInfoModel.LogoImageFullUrl = businessInfoModel.BaseUrl + businessInfoModel.LogoRelativeUrl + businessInfoModel.LogoImageName;
            businessInfoModel.PhoneImageFullUrl = businessInfoModel.BaseUrl + "Images/Phone1_Small.png";
            businessInfoModel.SMSImageFullUrl = businessInfoModel.BaseUrl + "Images/SMSIcon3_Small.png";
            businessInfoModel.WhatsAppImageFullUrl = businessInfoModel.BaseUrl + "Images/WhatsApp1_Small.png";
            foreach (var itemBundleItemModel in itemBundleItemModels)
            {
                itemBundleItemModel.BundleItemModel = ItemModels.First(x => x.ItemId == itemBundleItemModel.BundleItemId);
                itemBundleItemModel.ItemModel = ItemModels.First(x => x.ItemId == itemBundleItemModel.ItemId);
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
        //private static void BuildItemBundleRecursive(ref int i, List<ItemModel> itemModels, List<ItemBundleItemModel> itemBundleItemModels, ItemModel bundleItemModel)
        //{
        //    int iTemp;
        //    ItemModel itemModel;
        //    while (i < itemBundleItemModels.Count && itemBundleItemModels[i].BundleItemId == bundleItemModel.ItemId)
        //    {
        //        iTemp = i;
        //        bundleItemModel.ItemModels.Add(itemModel = itemModels.First(x => x.ItemId == itemBundleItemModels[iTemp].ItemId));
        //        i++;
        //        if (itemModel.ItemTypeId == ItemTypeEnum.ItemBundle)
        //        {
        //            itemModel.ItemModels = new List<ItemModel>();
        //            BuildItemBundleRecursive(ref i, itemModels, itemBundleItemModels, itemModel);
        //        }
        //    }
        //}
    }
}
