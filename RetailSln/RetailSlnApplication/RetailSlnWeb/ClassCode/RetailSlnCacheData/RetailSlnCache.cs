using ArchitectureLibraryCacheData;
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
        public static List<CategoryModel> CategoryModels { set; get; }
        public static CultureInfo CurrencyCultureInfo { set; get; }
        public static string CurrencyDecimalPlaces { set; get; }
        public static string CurrencySymbol { set; get; }
        public static Dictionary<long, CategoryLayoutModel> CategoryLayoutModels { set; get; }
        public static List<CorpAcctModel> CorpAcctModels { set; get; }
        public static List<DeliveryChargeModel> DeliveryChargeModels { set; get; }
        public static List<DeliveryListChargeModel> DeliveryListChargeModels { set; get; }
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
            retailSlnCacheBL.Initialize(out List<CategoryModel> categoryModels, out List<ItemModel> itemModels, out List<ItemAttribModel> itemAttribModels, out List<ItemAttribMasterModel> itemAttribMasterModels, out List<ItemBundleItemModel> itemBundleItemModels, out List<ItemBundleDiscountModel> itemBundleDiscountModels, out List<CategoryItemHierModel> categoryItemHierModels, out List<DeliveryListModel> deliveryListModels, out List<DeliveryListChargeModel> deliveryListChargeModels, out List<DeliveryChargeModel> deliveryChargeModels, out List<CorpAcctModel> corpAcctModels, out List<DiscountDtlModel> discountDtlModels, clientId, ipAddress, execUniqueId, loggedInUserId);
            CategoryModels = categoryModels;
            ItemBundleItemModels = ItemBundleItemModels;
            ItemModels = itemModels;
            ItemAttribModels = itemAttribModels;
            ItemAttribMasterModels = itemAttribMasterModels;
            CategoryItemHierModels = categoryItemHierModels;
            DeliveryListModels = deliveryListModels;
            DeliveryListChargeModels = deliveryListChargeModels;
            DeliveryChargeModels = deliveryChargeModels;
            CorpAcctModels = corpAcctModels;
            DiscountDtlModels = discountDtlModels;
            BuildCacheModels(categoryModels, out Dictionary<long, CategoryLayoutModel> categoryLayoutModels, itemModels, itemAttribModels, itemAttribMasterModels, categoryItemHierModels, deliveryListModels, deliveryListChargeModels, deliveryChargeModels, corpAcctModels, discountDtlModels, out List<DemogInfoCountryModel> deliveryDemogInfoCountryModels, out List<SelectListItem> deliveryDemogInfoCountrySelectListItems, clientId, ipAddress, execUniqueId, loggedInUserId);
            DeliveryDemogInfoCountryModels = deliveryDemogInfoCountryModels;
            DeliveryDemogInfoCountrySelectListItems = deliveryDemogInfoCountrySelectListItems;
            DefaultDeliveryDemogInfoCountryId = long.Parse(ArchLibCache.GetApplicationDefault(clientId, "DeliveryInfo", "DefaultDemogInfoCountry"));
            CategoryLayoutModels = categoryLayoutModels;
            CurrencyCultureInfo = new CultureInfo(ArchLibCache.GetApplicationDefault(clientId, "Currency", "CultureInfo"));
            CurrencyDecimalPlaces = ArchLibCache.GetApplicationDefault(clientId, "Currency", "CurrencyDecimalPlaces");
            var regionInfo = new RegionInfo(ArchLibCache.GetApplicationDefault(clientId, "Currency", "CultureInfo"));
            CurrencySymbol = regionInfo.CurrencySymbol;
        }
        private static void BuildCacheModels(List<CategoryModel> categoryModels, out Dictionary<long, CategoryLayoutModel> categoryLayoutModels, List<ItemModel> itemModels, List<ItemAttribModel> itemAttribModels, List<ItemAttribMasterModel> itemAttribMasterModels, List<CategoryItemHierModel> categoryItemHierModels, List<DeliveryListModel> deliveryListModels, List<DeliveryListChargeModel> deliveryListChargeModels, List<DeliveryChargeModel> deliveryChargeModels, List<CorpAcctModel> corpAcctModels, List<DiscountDtlModel> discountDtlModels, out List<DemogInfoCountryModel> deliveryDemogInfoCountryModels, out List<SelectListItem> deliveryDemogInfoCountrySelectListItems, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
            foreach (var itemModel in itemModels)
            {
                itemModel.ItemAttribModels = itemAttribModels.FindAll(x => x.ItemId == itemModel.ItemId);
            }
            //
            foreach (var deliveryListModel in deliveryListModels)
            {
                deliveryListModel.DeliveryListChargeModels = new List<DeliveryListChargeModel>();
                deliveryListModel.DeliveryChargeModels = new List<DeliveryChargeModel>();
                deliveryListModel.DeliveryListChargeModels.AddRange(deliveryListChargeModels.FindAll(x => x.DeliveryListId == deliveryListModel.DeliveryListId));
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
                        Text = demogInfoCountryModel.CountryDesc,
                        Value = demogInfoCountryModel.DemogInfoCountryId.ToString(),
                    }
                );
            }
        }
    }
}
