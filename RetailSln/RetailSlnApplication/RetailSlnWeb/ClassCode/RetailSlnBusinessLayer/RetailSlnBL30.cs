using ArchitectureLibraryCacheData;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using RetailSlnCacheData;
using RetailSlnModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace RetailSlnBusinessLayer
{
    public partial class RetailSlnBL
    {
        // GET: BusinessInfo
        public BusinessInfoModel BusinessInfo(HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                BusinessInfoModel businessInfoModel = new BusinessInfoModel
                {
                    BaseUrl = ArchLibCache.GetApplicationDefault(clientId, "BaseUrl", ""),
                    BusinessName1 = ArchLibCache.GetApplicationDefault(clientId, "BusinessName1", ""),
                    BusinessName2 = ArchLibCache.GetApplicationDefault(clientId, "BusinessName2", ""),
                    DemogInfoAddressModels = new List<DemogInfoAddressModel>(),
                    LogoImageName = "Image_000.webp",
                    LogoRelativeUrl = "/ClientSpecific/" + clientId + "/" + ArchLibCache.ClientName + "/Documents/Images/",
                };
                return businessInfoModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }

        // GET: Categories
        public List<CategoryModel> Categorys(string parentCategoryIdParm, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                long.TryParse(parentCategoryIdParm, out long parentCategoryId);
                List<CategoryItemHierModel> categoryItemHierModels = RetailSlnCache.CategoryItemHierModels.FindAll(x => x.ParentCategoryId == parentCategoryId).OrderBy(x => x.SeqNum).ToList();
                List<CategoryModel> categoryModels = new List<CategoryModel>();
                foreach (var categoryItemHierModel in categoryItemHierModels)
                {
                    categoryModels.Add(categoryItemHierModel.CategoryModel);
                }
                return categoryModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }

        // GET: Items
        public List<ItemModel> Items(string categoryIdParm, string pageNumParm, string rowCountParm, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                long.TryParse(categoryIdParm, out long categoryId);
                List<CategoryItemHierModel> categoryItemHierModels = RetailSlnCache.CategoryItemHierModels.FindAll(x => x.ParentCategoryId == categoryId).OrderBy(x => x.SeqNum).ToList();
                List<ItemModel> itemModels = new List<ItemModel>();
                foreach (var categoryItemHierModel in categoryItemHierModels)
                {
                    itemModels.Add(categoryItemHierModel.ItemModel);
                }
                return itemModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
    }
}
