﻿using ArchitectureLibraryCacheData;
using RetailSlnCacheBusinessLayer;
using RetailSlnEnumerations;
using RetailSlnModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace RetailSlnCacheData
{
    public static class RetailSlnCache
    {
        public static List<CategoryModel> CategoryModels { set; get; }
        public static CultureInfo CurrencyCultureInfo { set; get; }
        public static string CurrencyDecimalPlaces { set; get; }
        public static string CurrencySymbol { set; get; }
        public static Dictionary<long, CategoryLayoutModel> CategoryLayoutModels { set; get; }
        public static List<ItemModel> ItemModels { set; get; }
        public static List<ItemAttribMasterModel> ItemAttribMasterModels { set; get; }
        public static List<ItemAttribModel> ItemAttribModels { set; get; }
        public static List<ItemBundleItemModel> ItemBundleItemModels { set; get; }
        public static List<ItemBundleDiscountModel> ItemBundleDiscountModels { set; get; }
        public static List<CategoryItemHierModel> CategoryItemHierModels { set; get; }
        public static void Initialize(long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            RetailSlnCacheBL retailSlnCacheBL = new RetailSlnCacheBL();
            retailSlnCacheBL.Initialize(out List<CategoryModel> categoryModels, out List<ItemModel> itemModels, out List<ItemAttribModel> itemAttribModels, out List<ItemAttribMasterModel> itemAttribMasterModels, out List<ItemBundleItemModel> itemBundleItemModels, out List<ItemBundleDiscountModel> itemBundleDiscountModels, out List<CategoryItemHierModel> categoryItemHierModels, clientId, ipAddress, execUniqueId, loggedInUserId);
            CategoryModels = categoryModels;
            ItemBundleItemModels = ItemBundleItemModels;
            ItemModels = itemModels;
            ItemAttribModels = itemAttribModels;
            ItemAttribMasterModels = itemAttribMasterModels;
            CategoryItemHierModels = categoryItemHierModels;
            BuildCacheModels(categoryModels, out Dictionary<long, CategoryLayoutModel> categoryLayoutModels, itemModels, itemAttribModels, itemAttribMasterModels, categoryItemHierModels, clientId, ipAddress, execUniqueId, loggedInUserId);
            CategoryLayoutModels = categoryLayoutModels;
            CurrencyCultureInfo = new CultureInfo(ArchLibCache.GetApplicationDefault(clientId, "Currency", "CultureInfo"));
            CurrencyDecimalPlaces = ArchLibCache.GetApplicationDefault(clientId, "Currency", "CurrencyDecimalPlaces");
            var regionInfo = new RegionInfo(ArchLibCache.GetApplicationDefault(clientId, "Currency", "CultureInfo"));
            CurrencySymbol = regionInfo.CurrencySymbol;
        }
        private static void BuildCacheModels(List<CategoryModel> categoryModels, out Dictionary<long, CategoryLayoutModel> categoryLayoutModels, List<ItemModel> itemModels, List<ItemAttribModel> itemAttribModels, List<ItemAttribMasterModel> itemAttribMasterModels, List<CategoryItemHierModel> categoryItemHierModels, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            foreach (var categoryItemHierModel in categoryItemHierModels)
            {
                categoryItemHierModel.CategoryModel = categoryModels.FirstOrDefault(x => x.CategoryId == categoryItemHierModel.CategoryId);
                categoryItemHierModel.ItemModel = itemModels.FirstOrDefault(x => x.ItemId == categoryItemHierModel.ItemId);
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
        }
    }
}