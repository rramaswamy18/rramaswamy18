using ArchitectureLibraryEnumerations;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using RetailSlnCacheData;
using RetailSlnDataLayer;
using RetailSlnEnumerations;
using RetailSlnModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace RetailSlnBusinessLayer
{
    public partial class RetailSlnBL
    {
        // GET: CategoryList
        public CategoryListModel CategoryList(long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            CategoryListModel categoryListModel;
            try
            {
                //int x = 1, y = 0, z = x / y;
                ApplicationDataContext.OpenSqlConnection();
                categoryListModel = new CategoryListModel
                {
                    CategoryModels = ApplicationDataContext.CategoryList(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Success,
                    },
                };
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                categoryListModel = new CategoryListModel
                {
                    CategoryModels = null,
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseMessages = new List<string>
                        {
                            exception.Message,
                            "Error while loading category(s) from database",
                        },
                        ResponseTypeId = ResponseTypeEnum.Error,
                    },
                };
            }
            finally
            {
                try
                {
                    ApplicationDataContext.CloseSqlConnection();
                }
                catch
                {

                }
            }
            return categoryListModel;
        }
        // GET: CategoryList
        public CRMListListModel CRMListList(string pageNumParm, string pageSizeParm, SessionObjectModel sessionObjectModel, SessionObjectModel createForessionObjectModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            CRMListListModel cRMListListModel;
            try
            {
                //int x = 1, y = 0, z = x / y;
                int.TryParse(pageNumParm, out int pageNum);
                if (pageNum <= 0) pageNum = 1;
                int.TryParse(pageSizeParm, out int pageSize);
                if (pageSize <= 0) pageSize = 135;
                //int offSetCount = (pageNum - 1) * pageSize;
                ApplicationDataContext.OpenSqlConnection();
                cRMListListModel = new CRMListListModel
                {
                    PageNum = pageNum,
                    RowCountFrom = (pageNum - 1) * pageSize + 1,
                    RowCountTo = pageNum * pageSize,
                    TotalRowCount = ApplicationDataContext.CRMListCount(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                    CRMListModels = ApplicationDataContext.CRMList(pageNum, pageSize, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Success,
                    },
                };
                cRMListListModel.TotalPageCount = (cRMListListModel.TotalRowCount + pageSize - 1) / pageSize;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                throw;
            }
            finally
            {
                ApplicationDataContext.CloseSqlConnection();
            }
            return cRMListListModel;
        }
        // GET : Item
        public ItemDataModel Item(string itemIdParm, SessionObjectModel sessionObjectModel, SessionObjectModel createForessionObjectModel, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            //int x = 1, y = 0, z = x / y;
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                int.TryParse(itemIdParm, out int itemId);
                ItemDataModel itemDataModel;
                if (itemId == 0)
                {
                    itemDataModel = new ItemDataModel
                    {
                        ItemModel = new ItemModel
                        {
                            ItemMasterId = 0,
                            ItemStatusId = ItemStatusEnum.Active,
                            ItemStockStatusId = ItemStockStatusEnum.InStock,
                            ItemInfoModels = new List<ItemInfoModel>
                            {
                                new ItemInfoModel
                                {
                                    SeqNum = 1,
                                },
                                new ItemInfoModel
                                {
                                    SeqNum = 2,
                                },
                                new ItemInfoModel
                                {
                                    SeqNum = 3,
                                },
                            },
                            ItemItemSpecModelsList = new List<ItemItemSpecModel>(),
                        },
                        ResponseObjectModel = new ResponseObjectModel
                        {
                            ResponseTypeId = ResponseTypeEnum.Info,
                        },
                    };
                    foreach (var itemSpecMasterModel in RetailSlnCache.ItemSpecMasterModels)
                    {
                        itemDataModel.ItemModel.ItemItemSpecModelsList.Add
                        (
                            new ItemItemSpecModel
                            {
                                ItemSpecMasterModel = itemSpecMasterModel,
                            }
                        );
                    }
                }
                else
                {
                    itemDataModel = new ItemDataModel
                    {
                        ItemModel = RetailSlnCache.ItemModels.First(x => x.ItemId == itemId),
                        ResponseObjectModel = new ResponseObjectModel
                        {
                            ResponseTypeId = ResponseTypeEnum.Info,
                        },
                    };
                }
                return itemDataModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                throw;
            }
            finally
            {
                ApplicationDataContext.CloseSqlConnection();
            }
        }
        // GET : ItemMaster
        public ItemMasterDataModel ItemMaster(string itemMasterIdParm, SessionObjectModel sessionObjectModel, SessionObjectModel createForessionObjectModel, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            //int x = 1, y = 0, z = x / y;
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                int.TryParse(itemMasterIdParm, out int itemMasterId);
                ItemMasterDataModel itemMasterDataModel;
                if (itemMasterId == 0)
                {
                    itemMasterDataModel = new ItemMasterDataModel
                    {
                        ItemMasterModel = new ItemMasterModel
                        {
                            CategoryItemMasterHierModels = new List<CategoryItemMasterHierModel>(),
                            ItemMasterId = 0,
                            ItemMasterDesc0 = "Divine Bija",
                            ItemTypeId = ItemTypeEnum.RegularItem,
                            ItemMasterStatusId = ItemStatusEnum.Active,
                            ItemMasterInfoModels = new List<ItemMasterInfoModel>
                            {
                                new ItemMasterInfoModel
                                {
                                    SeqNum = 1,
                                },
                                new ItemMasterInfoModel
                                {
                                    SeqNum = 2,
                                },
                                new ItemMasterInfoModel
                                {
                                    SeqNum = 3,
                                },
                            },
                            ItemMasterItemSpecModelsList = new List<ItemMasterItemSpecModel>(),
                            ItemModels = new List<ItemModel>(),
                        },
                        ResponseObjectModel = new ResponseObjectModel
                        {
                            ResponseTypeId = ResponseTypeEnum.Info,
                        },
                    };
                    foreach (var itemSpecMasterModel in RetailSlnCache.ItemSpecMasterModels.FindAll(x => x.ItemMasterFlag))
                    {
                        itemMasterDataModel.ItemMasterModel.ItemMasterItemSpecModelsList.Add
                        (
                            new ItemMasterItemSpecModel
                            {
                                ItemSpecMasterModel = itemSpecMasterModel,
                            }
                        );
                    }
                }
                else
                {
                    itemMasterDataModel = new ItemMasterDataModel
                    {
                        //Load this from database
                        ItemMasterModel = RetailSlnCache.ItemMasterModels.First(x => x.ItemMasterId == itemMasterId),
                        ResponseObjectModel = new ResponseObjectModel
                        {
                            ResponseTypeId = ResponseTypeEnum.Info,
                        },
                    };
                }
                return itemMasterDataModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                throw;
            }
            finally
            {
                ApplicationDataContext.CloseSqlConnection();
            }
        }
        // GET : ItemMaster
        public void ItemMaster(ref ItemMasterModel itemMasterModel, SessionObjectModel sessionObjectModel, SessionObjectModel createForessionObjectModel, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            //int x = 1, y = 0, z = x / y;
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                itemMasterModel.UploadImageFileName = itemMasterModel.ImageNameHttpPostedFileBase.FileName;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                throw;
            }
            finally
            {
                ApplicationDataContext.CloseSqlConnection();
            }
        }
        // GET : ItemMasterList
        public ItemMasterListModel ItemMasterList(string pageNumParm, string pageSizeParm, SessionObjectModel sessionObjectModel, SessionObjectModel createForessionObjectModel, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            //int x = 1, y = 0, z = x / y;
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                int.TryParse(pageNumParm, out int pageNum);
                if (pageNum <= 0) pageNum = 1;
                int.TryParse(pageSizeParm, out int pageSize);
                if (pageSize <= 0) pageSize = 45;
                int offSetCount = (pageNum - 1) * pageSize;
                ApplicationDataContext.OpenSqlConnection();
                SqlConnection sqlConnection = ApplicationDataContext.OpenSqlConnection(true);
                int totalRowCount = ApplicationDataContext.ItemMasterCount(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                int totalPageCount = (totalRowCount + pageSize - 1) / pageSize;
                ItemMasterListModel itemMasterListModel = new ItemMasterListModel
                {
                    ItemMasterModels = ApplicationDataContext.ItemMasterList(offSetCount, pageSize, ApplicationDataContext.SqlConnectionObject, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId),
                    PaginationModel = new PaginationModel
                    {
                        OffsetCount = offSetCount,
                        PageNum = pageNum,
                        PageSize = pageSize,
                        TotalPageCount = totalPageCount,
                        TotalRowCount = totalRowCount,
                    },
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Success,
                    },
                };
                return itemMasterListModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                throw;
            }
            finally
            {
                ApplicationDataContext.CloseSqlConnection();
            }
        }
        // GET : ItemMasterList
        public ItemSpecMasterListModel ItemSpecMasterList(SessionObjectModel sessionObjectModel, SessionObjectModel createForessionObjectModel, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            //int x = 1, y = 0, z = x / y;
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                SqlConnection sqlConnection = ApplicationDataContext.OpenSqlConnection(true);
                ItemSpecMasterListModel itemMasterListModel = new ItemSpecMasterListModel
                {
                    ItemSpecMasterModels = ApplicationDataContext.ItemSpecMasterList(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Success,
                    },
                };
                return itemMasterListModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                throw;
            }
            finally
            {
                ApplicationDataContext.CloseSqlConnection();
            }
        }
        // GET : OrderList
        public OrderListModel OrderList(string pageNumParm, string pageSizeParm, SessionObjectModel sessionObjectModel, SessionObjectModel createForessionObjectModel, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            //int x = 1, y = 0, z = x / y;
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                if (string.IsNullOrWhiteSpace(pageNumParm) || string.IsNullOrWhiteSpace(pageSizeParm))
                {
                    pageNumParm = "1";
                    pageSizeParm = "45";
                }
                long? corpAcctId, createdForPersonId, personId;
                switch (sessionObjectModel.AspNetRoleName)
                {
                    case "DEFAULTROLE":
                    case "BULKORDERSROLE":
                    case "WHOLESALEROLE":
                        corpAcctId = ((CorpAcctModel)createForessionObjectModel.ApplSessionObjectModel).CorpAcctId;
                        personId = null;
                        createdForPersonId = createForessionObjectModel.PersonId;
                        break;
                    case "APPLADMN1":
                    case "MARKETINGROLE":
                    case "REFERRALROLE":
                    case "SYSTADMIN":
                        corpAcctId = null;
                        personId = null;
                        createdForPersonId = null;
                        break;
                    default:
                        corpAcctId = -1;
                        personId = -1;
                        createdForPersonId = -1;
                        break;
                }
                OrderListModel orderListModel = new OrderListModel
                {
                    OrderListDataModels = ApplicationDataContext.OrderList(corpAcctId, personId, createdForPersonId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Success,
                    },
                };
                return orderListModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                throw;
            }
            finally
            {
                ApplicationDataContext.CloseSqlConnection();
            }
        }
        // GET : ItemMasterList
        public SearchMetaDataListModel SearchKeywordList(string pageNumParm, string pageSizeParm, SessionObjectModel sessionObjectModel, SessionObjectModel createForessionObjectModel, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            //int x = 1, y = 0, z = x / y;
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int.TryParse(pageNumParm, out int pageNum);
                //if (pageNum <= 0) pageNum = 1;
                //int.TryParse(pageSizeParm, out int pageSize);
                //if (pageSize <= 0) pageSize = 45;
                //int offSetCount = (pageNum - 1) * pageSize;
                ApplicationDataContext.OpenSqlConnection();
                //SqlConnection sqlConnection = ApplicationDataContext.OpenSqlConnection(true);
                //int totalRowCount = ApplicationDataContext.ItemMasterCount(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                //int totalPageCount = (totalRowCount + pageSize - 1) / pageSize;
                SearchMetaDataListModel searchKeywordListModel = new SearchMetaDataListModel
                {
                    SearchMetaDataModels = null,//ApplicationDataContext.SearchKeywordList(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                    //PaginationModel = new PaginationModel
                    //{
                    //    OffsetCount = offSetCount,
                    //    PageNum = pageNum,
                    //    PageSize = pageSize,
                    //    TotalPageCount = totalPageCount,
                    //    TotalRowCount = totalRowCount,
                    //},
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Success,
                    },
                };
                return searchKeywordListModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                throw;
            }
            finally
            {
                ApplicationDataContext.CloseSqlConnection();
            }
        }
        // GET / POST : SearchResult & SearchResultItemMaster
        public SearchResultModel SearchResult(string parentCategoryIdParm, string searchKeywordText, string pageNumParm, string pageSizeParm, SessionObjectModel sessionObjectModel, SessionObjectModel createForSessionObject, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                long corpAcctId = GetCorpAcctId(controller, sessionObjectModel, createForSessionObject, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                RetailSlnCache.CorpAcctItemDiscountModels.TryGetValue(corpAcctId, out Dictionary<long, ItemDiscountModel> itemDiscountModels);
                itemDiscountModels = itemDiscountModels ?? new Dictionary<long, ItemDiscountModel>();
                var aspNetRoleNameProxy = createForSessionObject?.AspNetRoleNameProxy;
                if (string.IsNullOrEmpty(aspNetRoleNameProxy))
                {
                    aspNetRoleNameProxy = "DEFAULTROLE";
                }
                int.TryParse(pageNumParm, out int pageNum);
                if (pageNum <= 0) pageNum = 1;
                int.TryParse(pageSizeParm, out int pageSize);
                if (pageSize <= 0) pageSize = 45;
                string sqlStmt = "";
                #region
                sqlStmt += "--Query 1 Item Count" + Environment.NewLine;
                sqlStmt += "        SELECT COUNT(*) AS ItemMasterCountTotal" + Environment.NewLine;
                sqlStmt += "          FROM" + Environment.NewLine;
                sqlStmt += "              (" + Environment.NewLine;
                sqlStmt += "        SELECT" + Environment.NewLine;
                sqlStmt += "               DISTINCT CategoryItemMasterHier.ItemMasterId" + Environment.NewLine;
                sqlStmt += "          FROM RetailSlnSch.SearchMetaData" + Environment.NewLine;
                sqlStmt += "    INNER JOIN RetailSlnSch.CategoryItemMasterHier" + Environment.NewLine;
                sqlStmt += "            ON SearchMetaData.EntityId = CategoryItemMasterHier.ItemMasterId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN RetailSlnSch.AspNetRoleCategory" + Environment.NewLine;
                sqlStmt += "            ON CategoryItemMasterHier.ParentCategoryId = AspNetRoleCategory.CategoryId" + Environment.NewLine;
                sqlStmt += "         WHERE" + Environment.NewLine;
                sqlStmt += "               SearchMetaData.EntityTypeNameDesc = 'ITEMMASTER'" + Environment.NewLine;
                sqlStmt += "           AND SearchMetaData.SearchKeyword LIKE '%' + @SearchKeyWordText + '%'" + Environment.NewLine;
                sqlStmt += "UNION" + Environment.NewLine;
                sqlStmt += "        SELECT" + Environment.NewLine;
                sqlStmt += "               DISTINCT CategoryItemMasterHier.ItemMasterId" + Environment.NewLine;
                sqlStmt += "          FROM RetailSlnSch.CategoryItemMasterHier" + Environment.NewLine;
                sqlStmt += "    INNER JOIN RetailSlnSch.ItemMaster" + Environment.NewLine;
                sqlStmt += "            ON CategoryItemMasterHier.ItemMasterId = ItemMaster.ItemMasterId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN RetailSlnSch.AspNetRoleCategory" + Environment.NewLine;
                sqlStmt += "            ON CategoryItemMasterHier.ParentCategoryId = AspNetRoleCategory.CategoryId" + Environment.NewLine;
                sqlStmt += "         WHERE" + Environment.NewLine;
                sqlStmt += "               AspNetRoleCategory.AspNetRoleName = @AspNetRoleName" + Environment.NewLine;
                sqlStmt += "           AND(" + Environment.NewLine;
                sqlStmt += "                ItemMaster.ItemMasterDesc0 LIKE '%' + @SearchKeyWordText + '%'" + Environment.NewLine;
                sqlStmt += "            OR  ItemMaster.ItemMasterDesc1 LIKE '%' + @SearchKeyWordText + '%'" + Environment.NewLine;
                sqlStmt += "            OR  ItemMaster.ItemMasterDesc2 LIKE '%' + @SearchKeyWordText + '%'" + Environment.NewLine;
                sqlStmt += "              )" + Environment.NewLine;
                sqlStmt += "              ) A" + Environment.NewLine;
                sqlStmt += "--Query 2 Item Master List" + Environment.NewLine;
                sqlStmt += "        SELECT DISTINCT" + Environment.NewLine;
                sqlStmt += "               CategoryItemMasterHier.ItemMasterId" + Environment.NewLine;
                sqlStmt += "              ,CategoryItemMasterHier.SeqNum" + Environment.NewLine;
                sqlStmt += "          FROM RetailSlnSch.SearchMetaData" + Environment.NewLine;
                sqlStmt += "    INNER JOIN RetailSlnSch.CategoryItemMasterHier" + Environment.NewLine;
                sqlStmt += "            ON SearchMetaData.EntityId = CategoryItemMasterHier.ItemMasterId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN RetailSlnSch.AspNetRoleCategory" + Environment.NewLine;
                sqlStmt += "            ON CategoryItemMasterHier.ParentCategoryId = AspNetRoleCategory.CategoryId" + Environment.NewLine;
                sqlStmt += "         WHERE" + Environment.NewLine;
                sqlStmt += "               SearchMetaData.EntityTypeNameDesc = 'ITEMMASTER'" + Environment.NewLine;
                sqlStmt += "           AND SearchMetaData.SearchKeyword LIKE '%' + @SearchKeyWordText + '%'" + Environment.NewLine;
                sqlStmt += "UNION" + Environment.NewLine;
                sqlStmt += "        SELECT DISTINCT" + Environment.NewLine;
                sqlStmt += "               CategoryItemMasterHier.ItemMasterId" + Environment.NewLine;
                sqlStmt += "              ,CategoryItemMasterHier.SeqNum" + Environment.NewLine;
                sqlStmt += "          FROM RetailSlnSch.CategoryItemMasterHier" + Environment.NewLine;
                sqlStmt += "    INNER JOIN RetailSlnSch.ItemMaster" + Environment.NewLine;
                sqlStmt += "            ON CategoryItemMasterHier.ItemMasterId = ItemMaster.ItemMasterId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN RetailSlnSch.AspNetRoleCategory" + Environment.NewLine;
                sqlStmt += "            ON CategoryItemMasterHier.ParentCategoryId = AspNetRoleCategory.CategoryId" + Environment.NewLine;
                sqlStmt += "         WHERE" + Environment.NewLine;
                sqlStmt += "               AspNetRoleCategory.AspNetRoleName = @AspNetRoleName" + Environment.NewLine;
                sqlStmt += "           AND(" + Environment.NewLine;
                sqlStmt += "                ItemMaster.ItemMasterDesc0 LIKE '%' + @SearchKeyWordText + '%'" + Environment.NewLine;
                sqlStmt += "            OR  ItemMaster.ItemMasterDesc1 LIKE '%' + @SearchKeyWordText + '%'" + Environment.NewLine;
                sqlStmt += "            OR  ItemMaster.ItemMasterDesc2 LIKE '%' + @SearchKeyWordText + '%'" + Environment.NewLine;
                sqlStmt += "               )" + Environment.NewLine;
                sqlStmt += "      ORDER BY" + Environment.NewLine;
                sqlStmt += "               CategoryItemMasterHier.SeqNum" + Environment.NewLine;
                sqlStmt += "        OFFSET @OffSetRowCount ROWS" + Environment.NewLine;
                sqlStmt += "    FETCH NEXT @FetchNextRowCount ROWS ONLY" + Environment.NewLine;
                sqlStmt += "--Query 3 Category List" + Environment.NewLine;
                sqlStmt += "        SELECT DISTINCT" + Environment.NewLine;
                sqlStmt += "               CategoryCategoryHier.CategoryId" + Environment.NewLine;
                sqlStmt += "              ,CategoryCategoryHier.SeqNum" + Environment.NewLine;
                sqlStmt += "          FROM RetailSlnSch.SearchMetaData" + Environment.NewLine;
                sqlStmt += "    INNER JOIN RetailSlnSch.CategoryCategoryHier" + Environment.NewLine;
                sqlStmt += "            ON SearchMetaData.EntityId = CategoryCategoryHier.CategoryId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN RetailSlnSch.AspNetRoleCategory" + Environment.NewLine;
                sqlStmt += "            ON CategoryCategoryHier.CategoryId = AspNetRoleCategory.CategoryId" + Environment.NewLine;
                sqlStmt += "         WHERE" + Environment.NewLine;
                sqlStmt += "               SearchMetaData.EntityTypeNameDesc = 'CATEGORY'" + Environment.NewLine;
                sqlStmt += "           AND AspNetRoleCategory.AspNetRoleName = @AspNetRoleName" + Environment.NewLine;
                sqlStmt += "           AND SearchMetaData.SearchKeyword LIKE '%' + @SearchKeyWordText + '%'" + Environment.NewLine;
                sqlStmt += "UNION" + Environment.NewLine;
                sqlStmt += "        SELECT DISTINCT" + Environment.NewLine;
                sqlStmt += "               Category.CategoryId" + Environment.NewLine;
                sqlStmt += "              ,CategoryCategoryHier.SeqNum" + Environment.NewLine;
                sqlStmt += "          FROM RetailSlnSch.Category" + Environment.NewLine;
                sqlStmt += "    INNER JOIN RetailSlnSch.AspNetRoleCategory" + Environment.NewLine;
                sqlStmt += "            ON Category.CategoryId = AspNetRoleCategory.CategoryId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN RetailSlnSch.CategoryCategoryHier" + Environment.NewLine;
                sqlStmt += "            ON Category.CategoryId = CategoryCategoryHier.CategoryId" + Environment.NewLine;
                sqlStmt += "         WHERE CategoryDesc LIKE '%' + @SearchKeyWordText + '%'" + Environment.NewLine;
                sqlStmt += "           AND AspNetRoleCategory.AspNetRoleName = @AspNetRoleName" + Environment.NewLine;
                sqlStmt += "      ORDER BY" + Environment.NewLine;
                sqlStmt += "               CategoryCategoryHier.SeqNum" + Environment.NewLine;
                #endregion
                #region
                ApplicationDataContext.OpenSqlConnection();
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, ApplicationDataContext.SqlConnectionObject);
                sqlCommand.Parameters.Add("@AspNetRoleName", SqlDbType.NVarChar, 50);
                sqlCommand.Parameters.Add("@OffSetRowCount", SqlDbType.Int);
                sqlCommand.Parameters.Add("@FetchNextRowCount", SqlDbType.Int);
                sqlCommand.Parameters.Add("@SearchKeywordText", SqlDbType.NVarChar, 50);
                sqlCommand.Parameters["@AspNetRoleName"].Value = aspNetRoleNameProxy;
                sqlCommand.Parameters["@SearchKeywordText"].Value = searchKeywordText;
                sqlCommand.Parameters["@OffSetRowCount"].Value = (pageNum - 1) * pageSize;
                sqlCommand.Parameters["@FetchNextRowCount"].Value = pageSize;
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                #endregion
                sqlDataReader.Read();
                long itemMasterCountTotal = long.Parse(sqlDataReader["ItemMasterCountTotal"].ToString());
                sqlDataReader.NextResult();
                //List<long> itemMasterIds = new List<long>();
                List<ItemMasterModel> itemMasterModels = new List<ItemMasterModel>();
                while (sqlDataReader.Read())
                {
                    //itemMasterIds.Add(long.Parse(sqlDataReader["ItemMasterId"].ToString()));
                    itemMasterModels.Add(RetailSlnCache.ItemMasterModels.First(x => x.ItemMasterId == long.Parse(sqlDataReader["ItemMasterId"].ToString())));
                }
                sqlDataReader.NextResult();
                //List<long> categoryIds = new List<long>();
                List<CategoryModel> categoryModels = new List<CategoryModel>();
                while (sqlDataReader.Read())
                {
                    //categoryIds.Add(long.Parse(sqlDataReader["CategoryId"].ToString()));
                    categoryModels.Add(RetailSlnCache.CategoryModels.First(x => x.CategoryId == long.Parse(sqlDataReader["CategoryId"].ToString())));
                }
                sqlDataReader.Close();
                long itemMasterCountFrom = (pageNum - 1) * pageSize + 1;
                long itemMasterCountTo = pageNum * pageSize;
                if (itemMasterCountTo > itemMasterCountTotal) itemMasterCountTo = itemMasterCountTotal;
                SearchResultModel searchResultModel = new SearchResultModel
                {
                    CategoryModels = categoryModels,
                    CategoryCountTotal = categoryModels.Count,
                    ItemMasterModels = itemMasterModels,
                    CurrencySymbol = RetailSlnCache.CurrencySymbol,
                    ItemDiscountModels = itemDiscountModels,
                    ItemMasterCountFrom = itemMasterCountFrom,
                    ItemMasterCountTo = itemMasterCountTo,
                    ItemMasterCountTotal = itemMasterCountTotal,
                    PageNum = pageNum,
                    TotalPageCount = (itemMasterCountTotal + pageSize - 1) / pageSize,
                    SearchKeywordText = searchKeywordText,
                };
                return searchResultModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            finally
            {
                ApplicationDataContext.CloseSqlConnection();
            }
        }
    }
}
