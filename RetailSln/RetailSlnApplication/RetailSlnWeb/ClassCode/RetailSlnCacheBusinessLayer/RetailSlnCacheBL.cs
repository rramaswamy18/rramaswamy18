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
        public void Initialize(out List<CategoryModel> categoryModels, out List<ItemModel> itemModels, out List<ItemAttribModel> itemAttribModels, out List<ItemAttribMasterModel> itemAttribMasterModels, out List<ItemBundleItemModel> itemBundleItemModels, out List<ItemBundleDiscountModel> itemBundleDiscountModels, out List<CategoryItemHierModel> categoryItemHierModels, out List<CorpAcctModel> corpAcctModels, out List<DiscountDtlModel> discountDtlModels, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            ApplicationDataContext.OpenSqlConnection();
            categoryModels = ApplicationDataContext.GetCategorys(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
            itemModels = ApplicationDataContext.GetItems(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
            itemAttribMasterModels = ApplicationDataContext.GetItemAttribMasters(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
            itemAttribModels = ApplicationDataContext.GetItemAttribs(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
            itemBundleItemModels = ApplicationDataContext.GetItemBundleItems(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
            itemBundleDiscountModels = ApplicationDataContext.GetItemBundleDiscounts(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
            categoryItemHierModels = ApplicationDataContext.GetCategoryItemHiers(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
            corpAcctModels = ApplicationDataContext.GetCorpAccts(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
            discountDtlModels = ApplicationDataContext.GetDiscountDtls(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
            ApplicationDataContext.CloseSqlConnection();
        }
    }
}
