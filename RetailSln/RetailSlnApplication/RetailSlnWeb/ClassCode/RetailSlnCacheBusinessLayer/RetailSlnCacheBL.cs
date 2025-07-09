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
                CategoryModels = ApplicationDataContext.CategoryList(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                CategoryItemMasterHierModels = ApplicationDataContext.CategoryItemMasterHierList(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                CorpAcctModels = ApplicationDataContext.CorpAcctList(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                CorpAcctLocationModels = ApplicationDataContext.CorpAcctLocationList(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                DeliveryMethodFilterModels = ApplicationDataContext.DeliveryMethodFilterList(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                ItemBundleModels = ApplicationDataContext.ItemBundleList(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                ItemDiscountModels = ApplicationDataContext.ItemDiscountList(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                ItemModels = ApplicationDataContext.ItemList(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                ItemMasterModels = ApplicationDataContext.ItemMasterList(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                ItemSpecMasterModels = ApplicationDataContext.ItemSpecMasterList(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                ItemSpecModels = ApplicationDataContext.ItemSpecList(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                PaymentModeFilterModels = ApplicationDataContext.PaymentModeFilterList(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                PickupLocationModels = ApplicationDataContext.PickupLocationList(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
            };
            ApplicationDataContext.CloseSqlConnection();
        }
    }
}
