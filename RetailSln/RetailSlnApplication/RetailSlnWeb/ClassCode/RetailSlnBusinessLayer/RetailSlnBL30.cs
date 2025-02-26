using ArchitectureLibraryBusinessLayer;
using ArchitectureLibraryCacheData;
using ArchitectureLibraryCreditCardBusinessLayer;
using ArchitectureLibraryCreditCardModels;
using ArchitectureLibraryDataLayer;
using ArchitectureLibraryEnumerations;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryShippingLibrary;
using ArchitectureLibraryUtility;
using RetailSlnCacheData;
using RetailSlnDataLayer;
using RetailSlnEnumerations;
using RetailSlnModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using static System.Collections.Specialized.BitVector32;

namespace RetailSlnBusinessLayer
{
    public partial class RetailSlnBL
    {
        // GET: BusinessInfo
        public ApiBusinessInfoModel BusinessInfo(HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                ApiBusinessInfoModel businessInfoModel = RetailSlnCache.ApiBusinessInfoModel;
                return businessInfoModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }

        // GET: Categorys
        public ApiCategorysModel Categorys(string aspNetRoleName, string parentCategoryIdParm, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ApiCategorysModel apiCategorysModel;
            try
            {
                if (string.IsNullOrWhiteSpace(aspNetRoleName))
                {
                    aspNetRoleName = "DEFAULTROLE";
                }
                long? parentCategoryId;
                try
                {
                    parentCategoryId = int.Parse(parentCategoryIdParm);
                }
                catch
                {
                    //For now hardcode based on role - Modify code to get it from Asp KVP table
                    //Until data in this table needs to be organizws
                    switch (aspNetRoleName)
                    {
                        case "BULKORDERSROLE":
                        case "MARKETINGROLE":
                        case "PRIESTROLE":
                            parentCategoryId = 102;
                            break;
                        case "WHOLESALEROLE":
                            parentCategoryId = 120;
                            break;
                        default:
                            parentCategoryId = 100;
                            break;
                    }
                }
                apiCategorysModel = new ApiCategorysModel
                {
                    AspNetRoleName = aspNetRoleName,
                    ParentCategoryId = parentCategoryId.Value,
                    ApiCategoryModels = new List<ApiCategoryModel>(),
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Success,
                    },
                };
                var categoryModels = RetailSlnCache.AspNetRoleParentCategoryCategoryModels[aspNetRoleName][0];
                foreach (var categoryModel in categoryModels)
                {
                    apiCategorysModel.ApiCategoryModels.Add
                    (
                        new ApiCategoryModel
                        {
                            CategoryId = categoryModel.CategoryId,
                            ClientId = categoryModel.ClientId,
                            AssignItem = categoryModel.AssignItem,
                            AssignSubCategory = categoryModel.AssignSubCategory,
                            CategoryDesc = categoryModel.CategoryDesc,
                            CategoryName = categoryModel.CategoryName,
                            CategoryNameDesc = categoryModel.CategoryNameDesc,
                            CategoryStatusId = categoryModel.CategoryStatusId,
                            CategoryTypeId = categoryModel.CategoryTypeId,
                            DefaultCategory = categoryModel.DefaultCategory,
                            ImageExtension = categoryModel.ImageExtension,
                            ImageName = categoryModel.ImageName,
                            MaxPerPage = categoryModel.MaxPerPage,
                            UploadImageFileName = categoryModel.UploadImageFileName,
                            ViewName = categoryModel.ViewName,
                        }
                    );
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                apiCategorysModel = new ApiCategorysModel
                {
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseMessages = new List<string>
                        {
                            exception.Message,
                            exception.StackTrace,
                        },
                        ResponseTypeId = ResponseTypeEnum.Error,
                    },
                };
            }
            return apiCategorysModel;
        }

        // GET: ItemMasters
        public List<ApiItemMasterModel> ItemMasters(string aspNetRoleName, string parentCategoryIdParm, string pageNumParm, string pageSizeParm, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                long.TryParse(parentCategoryIdParm, out long parentCategoryId);
                int.TryParse(pageNumParm, out int pageNum);
                pageNum = pageNum == 0 ? 1 : pageNum;
                int.TryParse(pageSizeParm, out int pageSize);
                pageSize = pageSize == 0 || pageSize > 45 ? 45 : pageSize;
                List<CategoryItemMasterHierModel> categoryItemMasterHierModels = RetailSlnCache.AspNetRoleParentCategoryCategoryItemMasterHierModels[aspNetRoleName][parentCategoryId];
                int totalRowCount = categoryItemMasterHierModels.Count;
                int pageCount = totalRowCount / pageSize;
                if (totalRowCount % pageSize != 0)
                {
                    pageCount++;
                }
                List<ApiItemMasterModel> apiItemMasterModels = new List<ApiItemMasterModel>();
                //ApiItemMasterModel apiItemMasterModel;
                //foreach (var categoryItemHierModel in categoryItemHierModels)
                //{
                //    apiItemMasterModels.Add
                //    (
                //        apiItemMasterModel = new ApiItemMasterModel
                //        {
                //            ItemMasterId = categoryItemHierModel.ItemMasterModel.ItemMasterId,
                //            ClientId = categoryItemHierModel.ItemMasterModel.ItemMasterId,
                //            ImageExtension = categoryItemHierModel.ItemMasterModel.ImageExtension,
                //            ImageName = categoryItemHierModel.ItemMasterModel.ImageName,
                //            ImageTitle = categoryItemHierModel.ItemMasterModel.ImageTitle,
                //            ItemMasterDesc = categoryItemHierModel.ItemMasterModel.ItemMasterDesc,
                //            ItemMasterDesc0 = categoryItemHierModel.ItemMasterModel.ItemMasterDesc0,
                //            ItemMasterDesc1 = categoryItemHierModel.ItemMasterModel.ItemMasterDesc1,
                //            ItemMasterDesc2 = categoryItemHierModel.ItemMasterModel.ItemMasterDesc2,
                //            ItemMasterDesc3 = categoryItemHierModel.ItemMasterModel.ItemMasterDesc3,
                //            ItemMasterName = categoryItemHierModel.ItemMasterModel.ItemMasterName,
                //            ItemTypeId = categoryItemHierModel.ItemMasterModel.ItemTypeId,
                //            ProductItemId = categoryItemHierModel.ItemMasterModel.ProductItemId,
                //            UploadImageFileName = categoryItemHierModel.ItemMasterModel.UploadImageFileName,
                //            ApiItemModels = new List<ApiItemModel>(),
                //        }
                //    );
                //    foreach (var itemModel in categoryItemHierModel.ItemMasterModel.ItemModels)
                //    {
                //        apiItemMasterModel.ApiItemModels.Add
                //        (
                //            new ApiItemModel
                //            {
                //                ItemId = itemModel.ItemId,
                //                ClientId = itemModel.ClientId,
                //                ExpectedAvailability = itemModel.ExpectedAvailability,
                //                ExpectedAvailabilityFormatted = itemModel.ExpectedAvailabilityFormatted,
                //                ImageName = itemModel.ImageName,
                //                ImageTitle = itemModel.ImageTitle,
                //                ItemForSaleId = itemModel.ItemForSaleId,
                //                ItemMasterId = itemModel.ItemMasterId,
                //                ItemName = itemModel.ItemName,
                //                ItemRate = itemModel.ItemRate,
                //                ItemRateFormatted = itemModel.ItemRateFormatted,
                //                ItemRateMSRP = itemModel.ItemRateMSRP,
                //                ItemShortDesc = itemModel.ItemShortDesc,
                //                ItemShortDesc0 = itemModel.ItemShortDesc0,
                //                ItemShortDesc1 = itemModel.ItemShortDesc1,
                //                ItemShortDesc2 = itemModel.ItemShortDesc2,
                //                ItemShortDesc3 = itemModel.ItemShortDesc3,
                //                ItemStarCount = itemModel.ItemStarCount,
                //                ItemStatusId = itemModel.ItemStatusId,
                //                ItemTypeId = itemModel.ItemTypeId,
                //                ProductItemId = itemModel.ProductItemId,
                //                UploadImageFileName = itemModel.UploadImageFileName,
                //            }
                //        );
                //    }
                //}
                return apiItemMasterModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
    }
}
