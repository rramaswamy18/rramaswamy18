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
        public static List<ItemAttribMasterModel> ItemAttribMasterModels { set; get; }
        public static List<ItemAttribModel> ItemAttribModels { set; get; }
        public static List<ItemBundleItemModel> ItemBundleItemModels { set; get; }
        public static List<ItemBundleDiscountModel> ItemBundleDiscountModels { set; get; }
        public static List<CategoryItemHierModel> CategoryItemHierModels { set; get; }
        public static List<DemogInfoCountryModel> DeliveryDemogInfoCountryModels { set; get; }
        public static List<SelectListItem> DeliveryDemogInfoCountrySelectListItems { set; get; }
        public static List<ApiCodeDataModel> DeliveryMethods { set; get; }
        public static List<ApiCodeDataModel> PaymentMethodsCreditSale { set; get; }
        public static List<ApiCodeDataModel> PaymentMethods { set; get; }
        public static long DefaultDeliveryDemogInfoCountryId { set; get; }
        public static List<KeyValuePair<long, string>> DeliveryCountrys { set; get; }
        public static List<KeyValuePair<long, List<KeyValuePair<long, string>>>> DeliveryCountryStates { set; get; }
        public static void Initialize(long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            RetailSlnCacheBL retailSlnCacheBL = new RetailSlnCacheBL();
            retailSlnCacheBL.Initialize(out List<CategoryModel> categoryModels, out List<ItemModel> itemModels, out List<ItemAttribModel> itemAttribModels, out List<ItemAttribMasterModel> itemAttribMasterModels, out List<ItemBundleItemModel> itemBundleItemModels, out List<ItemBundleDiscountModel> itemBundleDiscountModels, out List<CategoryItemHierModel> categoryItemHierModels, out List<CorpAcctModel> corpAcctModels, out List<DiscountDtlModel> discountDtlModels, clientId, ipAddress, execUniqueId, loggedInUserId);
            CategoryModels = categoryModels;
            ItemBundleItemModels = ItemBundleItemModels;
            ItemModels = itemModels;
            ItemAttribModels = itemAttribModels;
            ItemAttribMasterModels = itemAttribMasterModels;
            CategoryItemHierModels = categoryItemHierModels;
            //DeliveryListModels = deliveryListModels;
            //DeliveryChargeModels = deliveryChargeModels;
            CorpAcctModels = corpAcctModels;
            DiscountDtlModels = discountDtlModels;
            CurrencyCultureInfo = new CultureInfo(ArchLibCache.GetApplicationDefault(clientId, "Currency", "CultureInfo"));
            CurrencyDecimalPlaces = ArchLibCache.GetApplicationDefault(clientId, "Currency", "CurrencyDecimalPlaces");
            var regionInfo = new RegionInfo(ArchLibCache.GetApplicationDefault(clientId, "Currency", "CultureInfo"));
            CurrencySymbol = regionInfo.CurrencySymbol;
            BuildCacheModels(categoryModels, out Dictionary<long, CategoryLayoutModel> categoryLayoutModels, itemModels, itemAttribModels, itemAttribMasterModels, categoryItemHierModels, corpAcctModels, discountDtlModels, out List<DemogInfoCountryModel> deliveryDemogInfoCountryModels, out List<SelectListItem> deliveryDemogInfoCountrySelectListItems, out List<ApiCodeDataModel> deliveryMethods, out List<ApiCodeDataModel> paymentMethodsCreditSale, out List<ApiCodeDataModel> paymentMethods, out List<KeyValuePair<long, string>> deliveryCountrys, out List<KeyValuePair<long, List<KeyValuePair<long, string>>>> deliveryCountryStates, out BusinessInfoModel businessInfoModel, clientId, ipAddress, execUniqueId, loggedInUserId);
            DeliveryDemogInfoCountryModels = deliveryDemogInfoCountryModels;
            DeliveryDemogInfoCountrySelectListItems = deliveryDemogInfoCountrySelectListItems;
            DefaultDeliveryDemogInfoCountryId = long.Parse(ArchLibCache.GetApplicationDefault(clientId, "DeliveryInfo", "DefaultDemogInfoCountry"));
            CategoryLayoutModels = categoryLayoutModels;
            DeliveryMethods = deliveryMethods;
            PaymentMethodsCreditSale = paymentMethodsCreditSale;
            PaymentMethods = paymentMethods;
            DeliveryCountrys = deliveryCountrys;
            DeliveryCountryStates = deliveryCountryStates;
            BusinessInfoModel = businessInfoModel;
        }
        private static void BuildCacheModels(List<CategoryModel> categoryModels, out Dictionary<long, CategoryLayoutModel> categoryLayoutModels, List<ItemModel> itemModels, List<ItemAttribModel> itemAttribModels, List<ItemAttribMasterModel> itemAttribMasterModels, List<CategoryItemHierModel> categoryItemHierModels, List<CorpAcctModel> corpAcctModels, List<DiscountDtlModel> discountDtlModels, out List<DemogInfoCountryModel> deliveryDemogInfoCountryModels, out List<SelectListItem> deliveryDemogInfoCountrySelectListItems, out List<ApiCodeDataModel> deliveryMethods, out List<ApiCodeDataModel> paymentMethodsCreditSale, out List<ApiCodeDataModel> paymentMethods, out List<KeyValuePair<long, string>> deliveryCountrys, out List<KeyValuePair<long, List<KeyValuePair<long, string>>>> deliveryCountryStates, out BusinessInfoModel businessInfoModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
                itemAttribModel.ItemAttribValueForDisplay = itemAttribModel.ItemAttribMasterModel.AttribDesc + " " + itemAttribModel.ItemAttribValue;
                if (itemAttribModel.ItemAttribValue != "")
                {
                    if (itemAttribModel.ItemAttribMasterModel.CodeTypeId != null)
                    {
                        var abc1 = LookupCache.GetCodeDatasForCodeTypeIdByCodeDataNameId(itemAttribModel.ItemAttribMasterModel.CodeTypeId.Value, execUniqueId);
                        var abc2 = abc1.First(x => x.CodeDataNameId == long.Parse(itemAttribModel.ItemAttribUnitValue));
                        itemAttribModel.ItemAttribValueForDisplay += " " + abc2.CodeDataDesc0;
                    }
                }
            }
            //
            //ItemAttribModel itemAttribModel1;
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
                itemModel.ItemAttribModels = itemAttribModels.FindAll(x => x.ItemId == itemModel.ItemId);
                itemModel.ItemAttribModelsForDisplay = new Dictionary<string, ItemAttribModel>();
                foreach (var itemAttribModel in itemModel.ItemAttribModels)
                {
                    switch (itemAttribModel.ItemAttribMasterModel.AttribName)
                    {
                        case "HSNCode":
                        case "ProductCode":
                            itemModel.ItemAttribModelsForDisplay[itemAttribModel.ItemAttribMasterModel.AttribName] = itemAttribModel;
                            itemModel.ImageTitle += " " + itemAttribModel.ItemAttribValueForDisplay;
                            break;
                        case "WeightCalc":
                            itemModel.ImageTitle += " Wt: " + itemAttribModel.ItemAttribValue + " G";
                            break;
                        case "ProductOrVolumetricWeight":
                            itemModel.ImageTitle += " Vol Wt: " + itemAttribModel.ItemAttribValue + " G";
                            break;
                    }
                    if (itemAttribModel.ShowValue)
                    {
                        itemModel.ItemAttribModelsForDisplay[itemAttribModel.ItemAttribMasterModel.AttribName] = itemAttribModel;
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
            for (int i = 0; i < 1; i++)
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
            for (int i = 1; i < codeDataModels.Count; i++)
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
