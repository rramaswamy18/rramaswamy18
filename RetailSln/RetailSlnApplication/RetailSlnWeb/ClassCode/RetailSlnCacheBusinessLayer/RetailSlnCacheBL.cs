using RetailSlnDataLayer;
using RetailSlnModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnCacheBusinessLayer
{
    public class RetailSlnCacheBL
    {
        public void Initialize(out RetailSlnInitModel retailSlnInitModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            ApplicationDataContext.OpenSqlConnection();
            retailSlnInitModel = new RetailSlnInitModel
            {
                CategoryModels = ApplicationDataContext.GetCategorys(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                ItemModels = ApplicationDataContext.GetItems(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                ItemMasterModels = ApplicationDataContext.GetItemMasters(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                ItemMasterItemSpecModels = ApplicationDataContext.GetItemMasterItemSpecs(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                ItemSpecMasterModels = ApplicationDataContext.GetItemSpecMasters(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                ItemSpecModels = ApplicationDataContext.GetItemSpecs(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                ItemInfoModels = ApplicationDataContext.GetItemInfos(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                ItemImageModels = ApplicationDataContext.GetItemImages(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                ItemBundleItemModels = ApplicationDataContext.GetItemBundleItems(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                ItemBundleDiscountModels = ApplicationDataContext.GetItemBundleDiscounts(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                CategoryItemMasterHierModels = ApplicationDataContext.GetCategoryItemMasterHiers(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                CorpAcctModels = ApplicationDataContext.GetCorpAccts(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                DiscountDtlModels = ApplicationDataContext.GetDiscountDtls(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                FestivalListModels = ApplicationDataContext.GetFestivalLists(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                FestivalListImageModels = ApplicationDataContext.GetFestivalListImages(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
            };
            ApplicationDataContext.CloseSqlConnection();
        }
    }
}
