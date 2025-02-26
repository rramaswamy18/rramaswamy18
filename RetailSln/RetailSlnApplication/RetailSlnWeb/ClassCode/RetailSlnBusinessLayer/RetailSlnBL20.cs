using ArchitectureLibraryBusinessLayer;
using ArchitectureLibraryCacheData;
using ArchitectureLibraryDataLayer;
using ArchitectureLibraryDocumentService;
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
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace RetailSlnBusinessLayer
{
    public partial class RetailSlnBL
    {
        // GET: CategoryHierList
        public CategoryItemHierListModel CategoryHierList(long parentCategoryId, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            CategoryItemHierListModel categoryHierListModel;
            try
            {
                //int x = 1, y = 0, z = x / y;
                ApplicationDataContext.OpenSqlConnection();
                categoryHierListModel = new CategoryItemHierListModel
                {
                    ParentCategoryId = parentCategoryId,
                    CategoryModels = ApplicationDataContext.GetCategorys(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                    CategoryItemHierModels = ApplicationDataContext.GetCategoryItemMasterHiers(parentCategoryId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                    ItemModels = null,
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Success,
                    },
                };
                categoryHierListModel.CategoryModels.Remove(categoryHierListModel.CategoryModels.First(x => x.CategoryId == parentCategoryId));
                //var list = categoryHierListModel.CategoryItemHierModels.Select(x => new { x.CategoryId }).ToList();
                //List<long?> categoryIds = (from x in categoryHierListModel.CategoryItemHierModels select x.CategoryId).ToList();
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                categoryHierListModel = new CategoryItemHierListModel
                {
                    CategoryModels = null,
                    CategoryItemHierModels = null,
                    ItemModels = null,
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseMessages = new List<string>
                        {
                            exception.Message,
                            "Error while loading category hierarchy list from database",
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
            return categoryHierListModel;
        }
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
                    CategoryModels = ApplicationDataContext.GetCategorys(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
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
        // GET: CorpAcct
        public CorpAcctModel CorpAcct(string corpAcctIdParm, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            CorpAcctModel corpAcctModel;
            try
            {
                //int x = 1, y = 0, z = x / y;
                if (long.TryParse(corpAcctIdParm, out long corpAcctId))
                {
                    ApplicationDataContext.OpenSqlConnection();
                    corpAcctModel = ApplicationDataContext.GetCorpAcctCorpAcctLocation(corpAcctId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    if (corpAcctModel == null)
                    {
                        corpAcctModel = new CorpAcctModel
                        {
                            CorpAcctTypeId = CorpAcctTypeEnum.Individual,
                            CorpAcctLocationModels = new List<CorpAcctLocationModel>
                            {
                                new CorpAcctLocationModel
                                {
                                    DemogInfoAddressModel = new DemogInfoAddressModel
                                    {

                                    },
                                },
                            },
                        };
                    }
                    corpAcctModel.ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Success,
                    };
                }
                else
                {
                    corpAcctModel = new CorpAcctModel
                    {
                        CorpAcctId = null,
                        CorpAcctTypeId = CorpAcctTypeEnum.Wholesale,
                        CreditDays = 30,
                        CreditLimit = 10000,
                        CreditSale = YesNoEnum.Yes,
                        DefaultDiscountPercent = 35,
                        MinOrderAmount = 1000,
                        OrderApprovalRequired = YesNoEnum.Yes,
                        ShippingAndHandlingCharges = YesNoEnum.No,
                        StatusId = YesNoEnum.Yes,
                        ResponseObjectModel = new ResponseObjectModel
                        {
                            ResponseTypeId = ResponseTypeEnum.Info,
                        },
                    };
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                corpAcctModel = new CorpAcctModel
                {
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseMessages = new List<string>
                        {
                            exception.Message,
                            $"Error while loading Corp Acct {corpAcctIdParm} from database",
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
            return corpAcctModel;
        }
        // POST: CorpAcct
        public void CorpAcct(ref CorpAcctModel corpAcctModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                if (modelStateDictionary.IsValid)
                {
                    ApplicationDataContext.OpenSqlConnection();
                    ApplicationDataContext.AddCorpAcct(corpAcctModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    ApplicationDataContext.AddItemDiscountsForCorpAcct(corpAcctModel.CorpAcctId.Value, corpAcctModel.DefaultDiscountPercent.Value, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    RetailSlnCache.CorpAcctModels.Add(ApplicationDataContext.GetCorpAcct(corpAcctModel.CorpAcctId.Value, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId));
                }
                else
                {
                    corpAcctModel.ResponseObjectModel = new ResponseObjectModel
                    {
                        ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                    };
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
            }
            finally
            {
                ApplicationDataContext.CloseSqlConnection();
            }
        }
        // GET: CorpAcctList
        public CorpAcctListModel CorpAcctList(string pageNumParm, string rowCountParm, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            CorpAcctListModel corpAcctListModel;
            try
            {
                //int x = 1, y = 0, z = x / y;
                int.TryParse(pageNumParm, out int pageNum);
                pageNum = pageNum == 0 ? 1 : pageNum;
                int.TryParse(rowCountParm, out int rowCount);
                rowCount = rowCount == 0 ? 50 : rowCount;
                ApplicationDataContext.OpenSqlConnection();
                corpAcctListModel = new CorpAcctListModel
                {
                    CorpAcctModels = ApplicationDataContext.GetCorpAccts((pageNum - 1) * rowCount, rowCount, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Success,
                    },
                };
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                corpAcctListModel = new CorpAcctListModel
                {
                    CorpAcctModels = null,
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
            return corpAcctListModel;
        }
        // GET: CorpAcct
        public CorpAcctLocationModel CorpAcctLocation(string corpAcctLocationIdParm, string corpAcctIdParm, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                long demogInfoCountryId = long.Parse(ArchLibCache.GetApplicationDefault(clientId, "Currency", "DemogInfoCountryId"));
                CorpAcctLocationModel corpAcctLocationModel;
                ApplicationDataContext.OpenSqlConnection();
                if (long.TryParse(corpAcctLocationIdParm, out long corpAcctLocationId))
                {
                    corpAcctLocationModel = null;
                }
                else
                {
                    corpAcctLocationModel = new CorpAcctLocationModel
                    {
                        CorpAcctModel = ApplicationDataContext.GetCorpAcct(long.Parse(corpAcctIdParm), ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                        AlternateTelephoneCountryId = demogInfoCountryId,
                        PrimaryTelephoneCountryId = demogInfoCountryId,
                        DemogInfoAddressModel = new DemogInfoAddressModel
                        {
                            BuildingTypeId = BuildingTypeEnum._,
                            BuildingTypeSelectListItems = LookupCache.CodeTypeSelectListItems["BuildingType"]["CodeDataNameId"],
                            DemogInfoCountryId = demogInfoCountryId,
                            DemogInfoCountrySelectListItems = RetailSlnCache.DeliveryDemogInfoCountrySelectListItems,
                            DemogInfoSubDivisionSelectListItems = DemogInfoCache.DemogInfoSubDivisionSelectListItems[demogInfoCountryId],
                        },
                        StatusId = YesNoEnum.Yes,
                    };
                    corpAcctLocationModel.CorpAcctId = corpAcctLocationModel.CorpAcctModel.CorpAcctId;
                }
                return corpAcctLocationModel;
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
        // POST: CorpAcct
        public void CorpAcctLocation(ref CorpAcctLocationModel corpAcctLocationModel, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                ApplicationDataContext.OpenSqlConnection();
                if (modelStateDictionary.IsValid)
                {
                    if (corpAcctLocationModel.CorpAcctLocationId == null)
                    {
                        archLibBL.GetDemogInfoDataFromDemogInfoZipCode(corpAcctLocationModel.DemogInfoAddressModel, ApplicationDataContext.SqlConnectionObject, controller, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                        corpAcctLocationModel.DemogInfoAddressModel.FromDate = "1900-01-01";
                        corpAcctLocationModel.DemogInfoAddressModel.ToDate = "9999-12-31";
                        ArchLibDataContext.AddDemogInfoAddress(corpAcctLocationModel.DemogInfoAddressModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                        float seqNum = ApplicationDataContext.GetCorpAcctLocationMaxSeqNum(corpAcctLocationModel.CorpAcctId.Value, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                        corpAcctLocationModel.SeqNum = ++seqNum;
                        ApplicationDataContext.AddCorpAcctLocation(corpAcctLocationModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                        ApplicationDataContext.AddPersonExtn1CorpAcctLocation(corpAcctLocationModel.CorpAcctId.Value, corpAcctLocationModel.CorpAcctLocationId.Value, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    }
                    else
                    {
                        //Update existing CorpAcctLocation and DemogInfoAddress
                    }
                }
                else
                {
                    corpAcctLocationModel.CorpAcctModel = ApplicationDataContext.GetCorpAcct(corpAcctLocationModel.CorpAcctId.Value, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    long demogInfoCountryId = long.Parse(ArchLibCache.GetApplicationDefault(clientId, "Currency", "DemogInfoCountryId"));
                    if (corpAcctLocationModel.DemogInfoAddressModel == null)
                    {
                        corpAcctLocationModel.DemogInfoAddressModel = new DemogInfoAddressModel
                        {
                            BuildingTypeId = BuildingTypeEnum._,
                            BuildingTypeSelectListItems = LookupCache.CodeTypeSelectListItems["BuildingType"]["CodeDataNameId"],
                            DemogInfoCountryId = demogInfoCountryId,
                            DemogInfoCountrySelectListItems = RetailSlnCache.DeliveryDemogInfoCountrySelectListItems,
                            DemogInfoSubDivisionSelectListItems = DemogInfoCache.DemogInfoSubDivisionSelectListItems[demogInfoCountryId],
                        };
                    }
                    else
                    {
                        corpAcctLocationModel.DemogInfoAddressModel.BuildingTypeSelectListItems = LookupCache.CodeTypeSelectListItems["BuildingType"]["CodeDataNameId"];
                        corpAcctLocationModel.DemogInfoAddressModel.DemogInfoCountrySelectListItems = RetailSlnCache.DeliveryDemogInfoCountrySelectListItems;
                        corpAcctLocationModel.DemogInfoAddressModel.DemogInfoSubDivisionSelectListItems = DemogInfoCache.DemogInfoSubDivisionSelectListItems[demogInfoCountryId];
                    }
                    corpAcctLocationModel.ResponseObjectModel = new ResponseObjectModel
                    {
                        ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                    };
                }
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
        // GET: ItemMasterList
        public ItemMasterListModel ItemMasterList(string pageNumParm, string rowCountParm, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ItemMasterListModel itemMasterListModel;
            try
            {
                //int x = 1, y = 0, z = x / y;
                int.TryParse(pageNumParm, out int pageNum);
                pageNum = pageNum == 0 ? 1 : pageNum;
                int.TryParse(rowCountParm, out int rowCount);
                rowCount = rowCount == 0 ? 50 : rowCount;
                ApplicationDataContext.OpenSqlConnection();
                itemMasterListModel = new ItemMasterListModel
                {
                    ItemMasterModels = ApplicationDataContext.GetItemMasters((pageNum - 1) * rowCount, rowCount, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                    PageNum = pageNum,
                    RowCount = rowCount,
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Success,
                    },
                };
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                itemMasterListModel = new ItemMasterListModel
                {
                    ItemMasterModels = null,
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseMessages = new List<string>
                        {
                            exception.Message,
                            "Error while loading item master(s) from database",
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
            return itemMasterListModel;
        }
        // GET: ItemMasterInfo
        public ItemMasterModel ItemMasterInfo(string itemMasterIdParm, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ItemMasterModel itemMasterModel;
            try
            {
                //int x = 1, y = 0, z = x / y;
                int.TryParse(itemMasterIdParm, out int itemMasterId);
                ApplicationDataContext.OpenSqlConnection();
                itemMasterModel = ApplicationDataContext.GetItemMaster(itemMasterId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                itemMasterModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ResponseTypeId = ResponseTypeEnum.Success,
                };
                for (int i = 0; i < 2; i++)
                {
                    itemMasterModel.ItemMasterInfoModels.Add
                    (
                        new ItemMasterInfoModel
                        {
                            ItemMasterInfoId = null,
                            ItemMasterId = itemMasterId,
                            ItemMasterInfoLabelText = null,
                            ItemMasterInfoText = null,
                        }
                    );
                }
                itemMasterModel.ItemMasterInfoModels[1].ItemMasterInfoLabelText = "Label";
                itemMasterModel.ItemMasterInfoModels[2].ItemMasterInfoText = "Text";
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                itemMasterModel = new ItemMasterModel
                {
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseMessages = new List<string>
                        {
                            exception.Message,
                            "Error while loading item master(s) from database",
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
            return itemMasterModel;
        }
        // POST: ItemMasterInfo
        public void ItemMasterInfo(ref ItemMasterModel itemMasterModel, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                ItemMasterInfoValidate(itemMasterModel, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (modelStateDictionary.IsValid)
                {
                    ApplicationDataContext.OpenSqlConnection();
                    foreach (var itemMasterInfoModel in itemMasterModel.ItemMasterInfoModels)
                    {
                        if (string.IsNullOrWhiteSpace(itemMasterInfoModel.ItemMasterInfoLabelText) && string.IsNullOrWhiteSpace(itemMasterInfoModel.ItemMasterInfoText) && itemMasterInfoModel.SeqNum == null && itemMasterInfoModel.ItemMasterInfoId != null)
                        {//All entries are blank excepting id - This is valid - Delete this row from DB
                            ApplicationDataContext.ItemMasterInfoDelete(itemMasterInfoModel.ItemMasterInfoId.Value, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                        }
                    }
                }
                else
                {
                    modelStateDictionary.AddModelError("", "Error while validating item master info");
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                itemMasterModel = new ItemMasterModel
                {
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseMessages = new List<string>
                        {
                            exception.Message,
                            "Error while loading item master(s) from database",
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
            return;
        }
        // Validation : Validate ItemMasterInfo
        public void ItemMasterInfoValidate(ItemMasterModel itemMasterModel, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            modelStateDictionary.Clear();
            int validEntries = 0;
            for (int i = 0; i < itemMasterModel.ItemMasterInfoModels.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(itemMasterModel.ItemMasterInfoModels[i].ItemMasterInfoLabelText) && string.IsNullOrWhiteSpace(itemMasterModel.ItemMasterInfoModels[i].ItemMasterInfoText) && itemMasterModel.ItemMasterInfoModels[i].SeqNum == null && itemMasterModel.ItemMasterInfoModels[i].ItemMasterInfoId == null)
                {//All entries are blank - skip this

                }
                else
                {
                    if (string.IsNullOrWhiteSpace(itemMasterModel.ItemMasterInfoModels[i].ItemMasterInfoLabelText) && string.IsNullOrWhiteSpace(itemMasterModel.ItemMasterInfoModels[i].ItemMasterInfoText) && itemMasterModel.ItemMasterInfoModels[i].SeqNum == null && itemMasterModel.ItemMasterInfoModels[i].ItemMasterInfoId != null)
                    {//All entries are blank excepting id - This is valid - Delete this row from DB
                        validEntries++;
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(itemMasterModel.ItemMasterInfoModels[i].ItemMasterInfoLabelText) && !string.IsNullOrWhiteSpace(itemMasterModel.ItemMasterInfoModels[i].ItemMasterInfoText) && float.TryParse(itemMasterModel.ItemMasterInfoModels[i].SeqNum.ToString(), out float tempFloat))
                        {//All fields exist - no errors - Add or Upd DB
                            validEntries++;
                        }
                        else
                        {
                            if (string.IsNullOrWhiteSpace(itemMasterModel.ItemMasterInfoModels[i].ItemMasterInfoLabelText))
                            {
                                modelStateDictionary.AddModelError("ItemMasterInfoModels[" + i + "].ItemMasterInfoLabelText", "Enter label");
                                modelStateDictionary.AddModelError("", "Enter label " + (i + 1));
                            }
                            if (string.IsNullOrWhiteSpace(itemMasterModel.ItemMasterInfoModels[i].ItemMasterInfoText))
                            {
                                modelStateDictionary.AddModelError("ItemMasterInfoModels[" + i + "].ItemMasterInfoText", "Enter text");
                                modelStateDictionary.AddModelError("", "Enter text " + (i + 1));
                            }
                            if (!float.TryParse(itemMasterModel.ItemMasterInfoModels[i].SeqNum.ToString(), out tempFloat))
                            {
                                modelStateDictionary.AddModelError("ItemMasterInfoModels[" + i + "].SeqNum", "Seq#");
                                modelStateDictionary.AddModelError("", "Seq# " + (i + 1));
                            }
                        }
                    }
                }
            }
            if (validEntries == 0)
            {
                modelStateDictionary.AddModelError("", "No action to be taken");
            }
        }
        // GET : ItemAttributes
        public ItemMasterAttributesModel ItemMasterAttributes(long itemMasterId, long tabId, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ItemMasterAttributesModel itemMasterAttributesModel = new ItemMasterAttributesModel
                {
                    ItemMasterAttributesTabs = new List<string>
                    {
                        "Specification(s)",
                        "Product Info",
                        "Image(s)",
                        //"Bundled Item(s)",
                    },
                    ItemMasterAttributesViews = new List<string>
                    {
                        "_ItemMasterAttributes0",
                        "_ItemMasterAttributes1",
                        "_ItemMasterAttributes2",
                        //"_ItemMasterAttributes3",
                    },
                    ItemId = itemMasterId,
                    TabId = tabId,
                    ItemMasterModel = RetailSlnCache.ItemMasterModels.First(x => x.ItemMasterId == itemMasterId),
                    ItemMasterAttributesDatas = new List<object>(),
                };
                var itemMasterSpecListModel = new ItemMasterSpecListModel
                {
                    ItemMasterModel = itemMasterAttributesModel.ItemMasterModel,
                    //ItemSpecModels = itemAttributesModel.ItemMasterModel.ItemMasterSpecModels,
                };
                var itemInfoListModel = new ItemInfoListModel
                {
                    //ItemModel = itemAttributesModel.ItemMasterSpecListModel,
                    //ItemInfoModels = itemAttributesModel.ItemMasterModel.ItemInfoModels,
                };
                var itemImageListModel = new ItemImageListModel
                {
                    //ItemModel = itemAttributesModel.ItemMasterModel,
                };
                //ItemBundleItemData(itemMasterId, "", 0, clientId, ipAddress, execUniqueId, loggedInUserId);
                itemMasterAttributesModel.ItemMasterAttributesDatas.Add(itemMasterSpecListModel);
                itemMasterAttributesModel.ItemMasterAttributesDatas.Add(itemInfoListModel);
                itemMasterAttributesModel.ItemMasterAttributesDatas.Add(itemImageListModel);
                if (itemMasterAttributesModel.ItemMasterModel.ItemTypeId == ItemTypeEnum.ItemBundle)
                {
                    itemMasterAttributesModel.ItemMasterAttributesTabs.Add("Bundled Item(s)");
                    itemMasterAttributesModel.ItemMasterAttributesViews.Add("_ItemMasterAttributes3");
                    ItemBundleDataModel itemBundleDataModel = new ItemBundleDataModel
                    {
                        ItemMasterId = itemMasterId,
                        ItemMasterModel = RetailSlnCache.ItemMasterModels.First(x => x.ItemMasterId == itemMasterId),
                        //ItemBundleModel = RetailSlnCache.ItemBundleModels.First(x => x.ItemId == itemMasterId),
                    };
                    itemMasterAttributesModel.ItemMasterAttributesDatas.Add(itemBundleDataModel);
                }
                return itemMasterAttributesModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                throw;
            }
            finally
            {
            }
        }
        // GET : ItemBundleItemData
        public ItemBundleDataModel ItemBundleData(long itemId, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ItemBundleDataModel itemBundleDataModel;
            try
            {
                var itemModel = RetailSlnCache.ItemModels.First(x => x.ItemId == itemId);
                itemBundleDataModel = new ItemBundleDataModel
                {
                    ItemMasterId = itemModel.ItemMasterId,
                    ItemBundleModel = RetailSlnCache.ItemBundleModels.FirstOrDefault(x => x.ItemId == itemId),
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseMessages = new List<string>(),
                        ResponseTypeId = ResponseTypeEnum.Success,
                    },
                };
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                itemBundleDataModel = new ItemBundleDataModel
                {
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseMessages = new List<string>
                        {
                            "Error occurred while populating for Item " + itemId,
                            exception.Message,
                        },
                        ResponseTypeId = ResponseTypeEnum.Error,
                        ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                    }
                };
            }
            finally
            {
            }
            return itemBundleDataModel;
        }
        //// GET : ItemAttributes
        //public ItemBundleItemDataModel ItemBundleItemData(long bundleItemId, string prefixSeqNum, int paddingLeft, int itemSeqNumStart, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    ItemBundleItemDataModel_zzz itemBundleItemDataModel;
        //    try
        //    {
        //        var bundleItemModel = RetailSlnCache.ItemModels.First(x => x.ItemId == bundleItemId);
        //        itemBundleItemDataModel = new ItemBundleItemDataModel_zzz
        //        {
        //            BundleItemId = bundleItemId,
        //            PrefixSeqNum = prefixSeqNum,
        //            ItemSeqNumStart = itemSeqNumStart,
        //            BundleItemModel = bundleItemModel,
        //            ItemMasterModel = RetailSlnCache.ItemMasterModels.First(x => x.ItemMasterId == bundleItemModel.ItemMasterId),
        //            ItemBundleItemModels = RetailSlnCache.ItemBundleItemModels.FindAll(x => x.ItemId == bundleItemId),
        //            ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ResponseMessages = new List<string>(),
        //                ResponseTypeId = ResponseTypeEnum.Success,
        //            },
        //            BundleItemMasterModel = RetailSlnCache.ItemMasterModels.First(x => x.ItemMasterId == bundleItemModel.ItemMasterId),
        //        };
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //        itemBundleItemDataModel = new ItemBundleItemDataModel_zzz
        //        {
        //            ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ResponseMessages = new List<string>
        //                {
        //                    "Error occurred while populating for Bundle " + bundleItemId,
        //                    exception.Message,
        //                },
        //                ResponseTypeId = ResponseTypeEnum.Error,
        //                ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
        //            }
        //        };
        //    }
        //    finally
        //    {
        //    }
        //    return itemBundleItemDataModel;
        //}        // GET : ItemAttributes
        // GET : ItemHierList
        public CategoryItemHierListModel ItemHierList(long itemId, int assignedPageNum, int assignedRowCount, int pageNum, int rowCount, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            CategoryItemHierListModel categoryItemHierListModel;
            try
            {
                //int x = 1, y = 0, z = x / y;
                ApplicationDataContext.OpenSqlConnection();
                categoryItemHierListModel = new CategoryItemHierListModel
                {
                    CategoryModels = null,
                    CategoryItemHierModels = new List<CategoryItemMasterHierModel>(),
                    ItemModels = ApplicationDataContext.GetItems(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Success,
                    },
                };
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                categoryItemHierListModel = new CategoryItemHierListModel
                {
                    CategoryModels = null,
                    CategoryItemHierModels = null,
                    ItemModels = null,
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseMessages = new List<string>
                        {
                            exception.Message,
                            "Error while loading item hierarchy list from database",
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
            return categoryItemHierListModel;
        }
        // GET : OrderList
        public OrderListModel OrderList(string pageNumParm, string pageSizeParm, SessionObjectModel sessionObjectModel, SessionObjectModel createForessionObjectModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
                    case "PRIESTROLE":
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
                List<OrderHeader> orderHeaders = ApplicationDataContext.GetOrdersForList(corpAcctId, personId, createdForPersonId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                OrderListModel orderListModel = new OrderListModel
                {
                    OrderHeaders = new List<OrderHeader>(),
                    OrderHeaderSummarys = new List<OrderHeaderSummary>(),
                };
                foreach (var orderHeader in orderHeaders)
                {
                    //orderListModels.Add
                    //(
                    //    new OrderListModel
                    //    {

                    //    }
                    //);
                }
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
        // GET : SearchKeywordList
        public SearchKeywordListModel SearchKeywordList(int pageNum, int rowCount, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SearchKeywordListModel searchKeywordListModel;
            try
            {
                //int x = 1, y = 0, z = x / y;
                ApplicationDataContext.OpenSqlConnection();
                searchKeywordListModel = new SearchKeywordListModel
                {
                    SearchKeywordModels = ApplicationDataContext.GetSearchKeywords(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Success,
                    },
                };
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                searchKeywordListModel = new SearchKeywordListModel
                {
                    SearchKeywordModels = null,
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseMessages = new List<string>
                        {
                            exception.Message,
                            "Error while loading search keyword list from database",
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
            return searchKeywordListModel;
        }
        // GET: UserAddEdit
        public UserAddEditModel UserAddEdit(string personIdParm, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            UserAddEditModel userAddEditModel;
            try
            {
                //int x = 1, y = 0, z = x / y;
                if (long.TryParse(personIdParm, out long personId))
                {
                    ApplicationDataContext.OpenSqlConnection();
                    userAddEditModel = null;//ApplicationDataContext.GetCorpAcctCorpAcctLocation(corpAcctId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    if (userAddEditModel == null)
                    {
                        userAddEditModel = new UserAddEditModel
                        {
                            //CorpAcctTypeId = CorpAcctTypeEnum.Individual,
                            //CorpAcctLocationModels = new List<CorpAcctLocationModel>
                            //{
                            //    new CorpAcctLocationModel
                            //    {
                            //        DemogInfoAddressModel = new DemogInfoAddressModel
                            //        {

                            //        },
                            //    },
                            //},
                        };
                    }
                    userAddEditModel.ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Success,
                    };
                }
                else
                {
                    userAddEditModel = new UserAddEditModel
                    {
                        AlternateTelephoneCountryId = long.Parse(ArchLibCache.GetApplicationDefault(clientId, "Currency", "DemogInfoCountryId")),
                        TelephoneCountryId = long.Parse(ArchLibCache.GetApplicationDefault(clientId, "Currency", "DemogInfoCountryId")),
                        ResponseObjectModel = new ResponseObjectModel
                        {
                            ResponseTypeId = ResponseTypeEnum.Info,
                        },
                    };
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                userAddEditModel = new UserAddEditModel
                {
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseMessages = new List<string>
                        {
                            exception.Message,
                            $"Error while loading Corp Acct {personIdParm} from database",
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
            return userAddEditModel;
        }
        // POST: UserAddEdit
        public void UserAddEdit(ref UserAddEditModel userAddEditModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                if (modelStateDictionary.IsValid)
                {
                    ApplicationDataContext.OpenSqlConnection();
                    //ApplicationDataContext.AddCorpAcct(corpAcctModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    //ApplicationDataContext.AddItemDiscountsForCorpAcct(corpAcctModel.CorpAcctId.Value, corpAcctModel.DefaultDiscountPercent.Value, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                }
                else
                {
                    userAddEditModel.ResponseObjectModel = new ResponseObjectModel
                    {
                        ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                    };
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
            }
            finally
            {
                ApplicationDataContext.CloseSqlConnection();
            }
        }
        // GET: UserList
        public UserListModel UserList(string pageNumParm, string rowCountParm, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            UserListModel userListModel;
            try
            {
                //int x = 1, y = 0, z = x / y;
                int.TryParse(pageNumParm, out int pageNum);
                pageNum = pageNum == 0 ? 1 : pageNum;
                int.TryParse(rowCountParm, out int rowCount);
                rowCount = rowCount == 0 ? 50 : rowCount;
                ApplicationDataContext.OpenSqlConnection();
                userListModel = new UserListModel
                {
                    PersonExtn1Models = ApplicationDataContext.GetPersonExtn1s((pageNum - 1) * rowCount, rowCount, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Success,
                    },
                };
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
                userListModel = new UserListModel
                {
                    PersonExtn1Models = null,
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseMessages = new List<string>
                        {
                            exception.Message,
                            "Error while loading user(s) from database",
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
            return userListModel;
        }
        //#region
        ////GET Category
        //public CategoryModel Category(long? categoryId, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        ApplicationDataContext.OpenSqlConnection();
        //        CategoryModel categoryModel;
        //        if (categoryId == null)
        //        {
        //            categoryModel = new CategoryModel
        //            {
        //                CategoryTypeId = CategoryTypeEnum.RegularCategory,
        //                CategoryStatusId = CategoryStatusEnum.Active,
        //            };
        //        }
        //        else
        //        {
        //            categoryModel = ApplicationDataContext.GetCategory((long)categoryId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        }
        //        return categoryModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            ApplicationDataContext.CloseSqlConnection();
        //        }
        //        catch
        //        {

        //        }
        //    }
        //}
        ////POST Category
        //public void Category(ref CategoryModel categoryModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ApplicationDataContext.OpenSqlConnection();
        //        if (categoryModel.CategoryId == null)
        //        {
        //            ApplicationDataContext.CreateCategory(categoryModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            categoryModel.ImageName = UploadFile(categoryModel.CategoryId.Value, null, "Category", "~/Documents/Images/Category", categoryModel.HttpPostedFileBase, 252, 252, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            ApplicationDataContext.ModifyCategoryImageName(categoryModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            categoryModel = new CategoryModel
        //            {
        //                CategoryStatusId = CategoryStatusEnum.Active,
        //                CategoryTypeId = CategoryTypeEnum.RegularCategory,
        //                ResponseObjectModel = new ResponseObjectModel
        //                {
        //                    ResponseTypeId = ResponseTypeEnum.Success,
        //                    ResponseMessages = new List<string>
        //                    {
        //                        "Category added successfully",
        //                        "Please continue with the next category",
        //                    },
        //                }
        //            };
        //        }
        //        else
        //        {
        //            ApplicationDataContext.ModifyCategory(categoryModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            if (categoryModel.HttpPostedFileBase != null)
        //            {
        //                categoryModel.ImageName = UploadFile(categoryModel.CategoryId.Value, categoryModel.ImageName, "Category", "~/Documents/Images/Category", categoryModel.HttpPostedFileBase, 252, 252, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //                ApplicationDataContext.ModifyCategoryImageName(categoryModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            }
        //            categoryModel.ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ResponseTypeId = ResponseTypeEnum.Success,
        //                ResponseMessages = new List<string>
        //                {
        //                    "Category updated successfully",
        //                    "Please continue to edit the category",
        //                },
        //            };
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            ApplicationDataContext.CloseSqlConnection();
        //        }
        //        catch
        //        {
        //        }
        //    }
        //}
        ////GET CategoryHierList
        //public CategoryHierListModel CategoryHierList(long parentCategoryId, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    var categoryLayoutModel = RetailSlnCache.CategoryLayoutModels[parentCategoryId];
        //    var parentCategoryModel = categoryLayoutModel.ParentCategoryModel;
        //    CategoryHierListModel categoryHierListModel = new CategoryHierListModel();
        //    categoryHierListModel.CategoryModels = categoryLayoutModel.CategoryModels;
        //    //RetailSlnCache.CategoryHierModels.First(x => x.ParentCategoryId == parentCategoryId).ParentCategoryModel.CategoryModels;
        //    return categoryHierListModel;
        //}
        ////GET CategoryItem
        //public CategoryItemListModel CategoryItem(long parentCategoryId, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        ApplicationDataContext.OpenSqlConnection();
        //        CategoryItemListModel categoryItemListModel = new CategoryItemListModel
        //        {
        //            ParentCategoryModel = ApplicationDataContext.GetCategory(parentCategoryId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
        //            ItemModelsAssigned = ApplicationDataContext.GetItemsAssigned(parentCategoryId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
        //            ItemModelsUnassigned = ApplicationDataContext.GetItemsUnassigned(parentCategoryId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
        //        };
        //        return categoryItemListModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            ApplicationDataContext.CloseSqlConnection();
        //        }
        //        catch
        //        {
        //        }
        //    }
        //}
        ////POST CategoryItem
        //public void CategoryItem(ref CategoryItemListModel categoryItemListModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        modelStateDictionary.Clear();
        //        categoryItemListModel.ResponseObjectModel = new ResponseObjectModel();
        //        ApplicationDataContext.OpenSqlConnection();
        //        if (categoryItemListModel.ParentCategoryModel.CategoryTypeId == CategoryTypeEnum.FeaturedItem)
        //        {
        //            long assignedCount = categoryItemListModel.Assigned == null ? 0 : categoryItemListModel.Assigned.Count;
        //            long unassignedCount = categoryItemListModel.Unassigned == null ? 0 : categoryItemListModel.Unassigned.Count;
        //            if (assignedCount + unassignedCount > 4)
        //            {
        //                modelStateDictionary.AddModelError("", "Total items to be selected for Featurd Items cannot exceed 4");
        //                categoryItemListModel.ResponseObjectModel.ValidationSummaryMessage = "PLEASE FIX ERRORS TO CONTINUE???";
        //            }
        //        }
        //        if (modelStateDictionary.IsValid)
        //        {
        //            ApplicationDataContext.CreateCategoryItem(categoryItemListModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            categoryItemListModel.ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ValidationSummaryMessage = "Items Assign/Unassign completed successfully!!!",
        //            };
        //        }
        //        categoryItemListModel.ParentCategoryModel = ApplicationDataContext.GetCategory((long)categoryItemListModel.ParentCategoryModel.CategoryId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        categoryItemListModel.ItemModelsAssigned = ApplicationDataContext.GetItemsAssigned((long)categoryItemListModel.ParentCategoryModel.CategoryId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        categoryItemListModel.ItemModelsUnassigned = ApplicationDataContext.GetItemsUnassigned((long)categoryItemListModel.ParentCategoryModel.CategoryId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //        modelStateDictionary.AddModelError("", "Error while saving assign/unassign");
        //        modelStateDictionary.AddModelError("", "Please try again");
        //        modelStateDictionary.AddModelError("", "Should issue exists, please contact our support personnel");
        //        categoryItemListModel.ResponseObjectModel = new ResponseObjectModel
        //        {
        //            ValidationSummaryMessage = "PLEASE FIX ERRORS TO CONTINUE???",
        //        };
        //        categoryItemListModel.ParentCategoryModel = ApplicationDataContext.GetCategory((long)categoryItemListModel.ParentCategoryModel.CategoryId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        categoryItemListModel.ItemModelsAssigned = ApplicationDataContext.GetItemsAssigned((long)categoryItemListModel.ParentCategoryModel.CategoryId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        categoryItemListModel.ItemModelsUnassigned = ApplicationDataContext.GetItemsUnassigned((long)categoryItemListModel.ParentCategoryModel.CategoryId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            ApplicationDataContext.CloseSqlConnection();
        //        }
        //        catch
        //        {
        //        }
        //    }
        //    return;
        //}
        ////GET Item
        //public ItemModel Item(long? itemId, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        ApplicationDataContext.OpenSqlConnection();
        //        ItemModel itemModel;
        //        if (itemId == null)
        //        {
        //            itemModel = new ItemModel
        //            {
        //                ItemSpecModels = new List<ItemSpecModel>(),
        //                ItemStatusId = ItemStatusEnum.InStock,
        //                ItemTypeId = ItemTypeEnum.RegularItem,
        //            };
        //            foreach (var itemSpecMasterModel in RetailSlnCache.ItemSpecMasterModels)
        //            {
        //                itemModel.ItemSpecModels.Add
        //                (
        //                    new ItemSpecModel
        //                    {
        //                        ItemSpecId = -1,
        //                        ItemSpecMasterId = itemSpecMasterModel.ItemSpecMasterId.Value,
        //                        ItemSpecMasterModel = itemSpecMasterModel,
        //                        ItemSpecValue = "",
        //                    }
        //                );
        //            }
        //        }
        //        else
        //        {
        //            itemModel = ApplicationDataContext.GetItem((long)itemId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        }
        //        return itemModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            ApplicationDataContext.CloseSqlConnection();
        //        }
        //        catch
        //        {
        //        }
        //    }
        //}
        ////POST Item
        //public void Item(ref ItemModel itemModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ApplicationDataContext.OpenSqlConnection();
        //        if (itemModel.ItemId == null)
        //        {
        //            ApplicationDataContext.AddItem(itemModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            itemModel.ImageName = UploadFile(itemModel.ItemId.Value, null, "Item", "~/Documents/Images/Items", itemModel.HttpPostedFileBase, 180, 180, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            ApplicationDataContext.UpdItemImageName(itemModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            //Create ItemSpec Rows from Master (all rows)
        //            ApplicationDataContext.AddItemSpecs(itemModel.ItemId.Value, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            //Create rows for ItemImage (3 rows) & ItemImageSecSet (9 rows)
        //            int i, j;
        //            string[] imageDescs = { "Front View", "Top View", "Side View" };
        //            int[] imageHeights = { 450, 630, 810 };
        //            List<ItemImageModel> itemImageModels = new List<ItemImageModel>();
        //            ItemImageModel itemImageModel;
        //            for (i = 0; i < imageDescs.Length; i++)
        //            {
        //                itemImageModels.Add
        //                (
        //                    itemImageModel = new ItemImageModel
        //                    {
        //                        ImageDesc = imageDescs[i],
        //                        SeqNum = i + 1,
        //                        ItemImageSrcSetModels = new List<ItemImageSrcSetModel>(),
        //                    }
        //                );
        //                for (j = 0; j < imageHeights.Length; j++)
        //                {
        //                    itemImageModel.ItemImageSrcSetModels.Add
        //                    (
        //                        new ItemImageSrcSetModel
        //                        {
        //                            ImageHeight = imageHeights[j],
        //                            ImageHeightUnit = "px",
        //                            ImageName = "",
        //                            ImageWidth = imageHeights[j],
        //                            ImageWidthUnit = "px",
        //                            SeqNum = j + 1,
        //                        }
        //                    );
        //                }
        //            }
        //            string imageExtension = itemModel.ImageName.Substring(itemModel.ImageName.IndexOf('.'));
        //            ApplicationDataContext.AddItemImagesItemImageSrcSets(itemModel.ItemId.Value, itemImageModels, imageExtension, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            itemModel = new ItemModel
        //            {
        //                ItemStatusId = ItemStatusEnum.InStock,
        //                ItemTypeId = ItemTypeEnum.RegularItem,
        //                ResponseObjectModel = new ResponseObjectModel
        //                {
        //                    ResponseTypeId = ResponseTypeEnum.Success,
        //                    ResponseMessages = new List<string>
        //                    {
        //                        "Item added successfully",
        //                        "Please continue with the next item",
        //                    },
        //                }
        //            };
        //        }
        //        else
        //        {
        //            ApplicationDataContext.ModifyItem(itemModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            if (itemModel.HttpPostedFileBase != null)
        //            {
        //                itemModel.ImageName = UploadFile(itemModel.ItemId.Value, itemModel.ImageName, "Item", "~/Documents/Images/Items", itemModel.HttpPostedFileBase, 180, 180, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //                ApplicationDataContext.ModifyItemImageName(itemModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            }
        //            itemModel.ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ResponseTypeId = ResponseTypeEnum.Success,
        //                ResponseMessages = new List<string>
        //                {
        //                    "Item edited successfully",
        //                    "Please continue with editing item",
        //                },
        //            };
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            ApplicationDataContext.CloseSqlConnection();
        //        }
        //        catch
        //        {
        //        }
        //    }
        //}
        ////GET ItemSpecList
        //public ItemSpecListModel ItemSpecList(long itemId, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ApplicationDataContext.OpenSqlConnection();
        //        ItemSpecListModel itemSpecListModel = new ItemSpecListModel
        //        {
        //            ItemModel = RetailSlnCache.ItemModels.First(x => x.ItemId == itemId),
        //            ItemSpecModels = ApplicationDataContext.GetItemSpecs(itemId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
        //        };
        //        return itemSpecListModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            ApplicationDataContext.CloseSqlConnection();
        //        }
        //        catch
        //        {

        //        }
        //    }
        //}
        ////POST ItemSpecList
        //public void ItemSpecList(ref ItemSpecListModel itemSpecListModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ApplicationDataContext.OpenSqlConnection();
        //        ApplicationDataContext.UpdItemSpecs(itemSpecListModel.ItemSpecModels, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        itemSpecListModel.ResponseObjectModel = new ResponseObjectModel
        //        {
        //            ResponseMessages = new List<string>
        //            {
        //                "Item attributes updated successfully!!!",
        //                "Please continue to edit as necessry",
        //                "If you are done with editing click the appropriate menu",
        //            },
        //            ResponseTypeId = ResponseTypeEnum.Success
        //        };
        //        return;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //        itemSpecListModel.ResponseObjectModel = new ResponseObjectModel
        //        {
        //            ResponseTypeId = ResponseTypeEnum.Error,
        //            ValidationSummaryMessage = "PLEASE FIX ERRORS TO CONTINUE???",
        //        };
        //        modelStateDictionary.AddModelError("", "Error while updating Item Attributes");
        //        modelStateDictionary.AddModelError("", "Please try again");
        //        modelStateDictionary.AddModelError("", "If problem persists, please contact support personnel");
        //        return;
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            ApplicationDataContext.CloseSqlConnection();
        //        }
        //        catch
        //        {

        //        }
        //    }
        //}
        //// GET : ItemAttributes
        //public ItemAttributesModel ItemAttributes(long itemId, long tabId, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ItemAttributesModel itemAttributesModel = new ItemAttributesModel
        //        {
        //            ItemAttributesTabs = new List<string>
        //            {
        //                "Specification(s)",
        //                "Product Info",
        //                "Image(s)",
        //                "Bundled Item(s)",
        //            },
        //            ItemAttributesViews= new List<string>
        //            {
        //                "_ItemAttributes0",
        //                "_ItemAttributes1",
        //                "_ItemAttributes2",
        //                "_ItemAttributes3",
        //            },
        //            ItemId = itemId,
        //            TabId = tabId,
        //            ItemModel = RetailSlnCache.ItemModels.First(x => x.ItemId == itemId),
        //            ItemAttributesDatas = new List<object>(),
        //            //ItemInfoListModel = ItemInfoList(itemId, clientId, ipAddress, execUniqueId, loggedInUserId),
        //        };
        //        var itemSpecListModel = new ItemSpecListModel
        //        {
        //            ItemModel = itemAttributesModel.ItemModel,
        //            ItemSpecModels = itemAttributesModel.ItemModel.ItemSpecModels,
        //        };
        //        var itemInfoListModel = new ItemInfoListModel
        //        {
        //            ItemModel = itemAttributesModel.ItemModel,
        //            ItemInfoModels = itemAttributesModel.ItemModel.ItemInfoModels,
        //        };
        //        itemAttributesModel.ItemAttributesDatas.Add(itemSpecListModel);
        //        itemAttributesModel.ItemAttributesDatas.Add(itemInfoListModel);
        //        itemAttributesModel.ItemAttributesDatas.Add(ItemInfoList(itemId, clientId, ipAddress, execUniqueId, loggedInUserId));
        //        itemAttributesModel.ItemAttributesDatas.Add(ItemInfoList(itemId, clientId, ipAddress, execUniqueId, loggedInUserId));
        //        return itemAttributesModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //    }
        //}

        ////GET ItemBundleItemList
        //public ItemBundleItemListModel ItemBundleItemList(long itemId, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ApplicationDataContext.OpenSqlConnection();
        //        ItemBundleItemListModel itemBundleItemListModel = new ItemBundleItemListModel
        //        {
        //            ItemModel = RetailSlnCache.ItemModels.First(x => x.ItemId == itemId),
        //            ItemBundleItemModels = ApplicationDataContext.GetItemBundleItems(itemId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
        //        };
        //        foreach (var itemBundleItemModel in itemBundleItemListModel.ItemBundleItemModels)
        //        {
        //            itemBundleItemModel.BundleItemModel = RetailSlnCache.ItemModels.First(x => x.ItemId == itemBundleItemModel.BundleItemId);
        //        }
        //        return itemBundleItemListModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            ApplicationDataContext.CloseSqlConnection();
        //        }
        //        catch
        //        {

        //        }
        //    }
        //}
        ////GET ItemImageList
        //public ItemImageListModel ItemImageList(long itemId, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ApplicationDataContext.OpenSqlConnection();
        //        ItemImageListModel itemImageListModel = new ItemImageListModel
        //        {
        //            ItemModel = RetailSlnCache.ItemModels.First(x => x.ItemId == itemId),
        //            ItemImageModels = ApplicationDataContext.GetItemImages(itemId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
        //        };
        //        return itemImageListModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            ApplicationDataContext.CloseSqlConnection();
        //        }
        //        catch
        //        {

        //        }
        //    }
        //}
        ////POST ItemImageList
        //public void ItemImageList(ref ItemImageListModel itemImageListModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ApplicationDataContext.OpenSqlConnection();
        //        ArchLibDocumentBL archLibDocumentBL = new ArchLibDocumentBL();
        //        string documentsImagesDirectoryName = Utilities.GetApplicationValue("DocumentsImagesDirectoryName") + @"ItemImages\";
        //        int i;
        //        int[] resizedHeight = { 0, 0, 0 }, resizedWidth = { 0, 0, 0 };
        //        string[] serverFullFileName = { "", "", "" };
        //        foreach (var itemImageModel in itemImageListModel.ItemImageModels)
        //        {
        //            if (itemImageModel.HttpPostedFileBase != null)
        //            {
        //                //Resize the uploaded image to the 3 sizes and replace the files
        //                for (i = 0; i < itemImageModel.ItemImageSrcSetModels.Count; i++)
        //                {
        //                    resizedHeight[i] = itemImageModel.ItemImageSrcSetModels[i].ImageHeight;
        //                    resizedWidth[i] = itemImageModel.ItemImageSrcSetModels[i].ImageWidth;
        //                    serverFullFileName[i] = documentsImagesDirectoryName + itemImageModel.ItemImageSrcSetModels[i].ImageName;
        //                }
        //                archLibDocumentBL.CreateResizedImageFile(itemImageModel.HttpPostedFileBase, resizedHeight, resizedWidth, serverFullFileName, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            }
        //        }
        //        long itemId = itemImageListModel.ItemModel.ItemId.Value;
        //        itemImageListModel = new ItemImageListModel
        //        {
        //            ItemModel = RetailSlnCache.ItemModels.First(x => x.ItemId == itemId),
        //            ItemImageModels = ApplicationDataContext.GetItemImages(itemId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
        //        };
        //        return;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            ApplicationDataContext.CloseSqlConnection();
        //        }
        //        catch
        //        {

        //        }
        //    }
        //}
        ////GET ItemList
        //public ItemListModel ItemList(HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        ApplicationDataContext.OpenSqlConnection();
        //        ItemListModel itemListModel = new ItemListModel
        //        {
        //            ItemModels = RetailSlnCache.ItemModels,//ApplicationDataContext.GetItems(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
        //        };
        //        return itemListModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            ApplicationDataContext.CloseSqlConnection();
        //        }
        //        catch
        //        {

        //        }
        //    }
        //}
        ////GET ItemInfo
        //public ItemInfoModel ItemInfo(long? itemInfoId, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        ItemInfoModel itemInfoModel;
        //        if (itemInfoId == null)
        //        {
        //            itemInfoModel = new ItemInfoModel();
        //        }
        //        else
        //        {
        //            ApplicationDataContext.OpenSqlConnection();
        //            itemInfoModel = ApplicationDataContext.GetItemInfo(itemInfoId.Value, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            if (itemInfoModel == null)
        //            {
        //                modelStateDictionary.AddModelError("", "Error while getting Item Spec for Id " + itemInfoId);
        //            }
        //        }
        //        return itemInfoModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            ApplicationDataContext.CloseSqlConnection();
        //        }
        //        catch
        //        {
        //        }
        //    }
        //}
        ////POST ItemInfo
        //public void ItemInfo(ref ItemInfoModel itemInfoModel, ref ItemInfoListModel itemInfoListModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        ApplicationDataContext.OpenSqlConnection();
        //        itemInfoModel.ResponseObjectModel = new ResponseObjectModel();
        //        if (itemInfoModel.ItemInfoId == null)
        //        {
        //            itemInfoModel.SeqNum = ApplicationDataContext.GetItemInfoMaxSeqNum(itemInfoModel.ItemId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            itemInfoModel.SeqNum++;
        //            ApplicationDataContext.AddItemInfo(itemInfoModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            itemInfoModel.ResponseObjectModel.ValidationSummaryMessage = "Item spec added successfully!!!";
        //        }
        //        else
        //        {
        //            ApplicationDataContext.UpdItemInfo(itemInfoModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            itemInfoModel.ResponseObjectModel.ValidationSummaryMessage = "Item spec updated successfully!!!";
        //        }
        //        itemInfoListModel.ItemInfoModels = ApplicationDataContext.GetItemInfos(itemInfoModel.ItemId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //        itemInfoModel.ResponseObjectModel.ValidationSummaryMessage = "PLEASE FIX ERRORS TO CONTINUE???";
        //        modelStateDictionary.AddModelError("", "Error while saving Item Spec");
        //        modelStateDictionary.AddModelError("", "Please try again");
        //        modelStateDictionary.AddModelError("", "If problem persists, please contact support personnel");
        //        //throw;
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            ApplicationDataContext.CloseSqlConnection();
        //        }
        //        catch
        //        {
        //        }
        //    }
        //}
        //////DELETE ItemInfo
        ////public ItemInfoListAddEditModel ItemInfoDelete(long itemInfoId, long itemId, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        ////{
        ////    string methodName = MethodBase.GetCurrentMethod().Name;
        ////    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        ////    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        ////    ItemInfoListAddEditModel itemInfoListAddEditModel;
        ////    try
        ////    {
        ////        //int x = 1, y = 0, z = x / y;
        ////        ApplicationDataContext.OpenSqlConnection();
        ////        ApplicationDataContext.ItemInfoDelete(itemInfoId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        ////        itemInfoListAddEditModel = new ItemInfoListAddEditModel
        ////        {
        ////            ItemInfoListModel = ItemInfoList(itemId, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId),
        ////            ItemInfoModel = new ItemInfoModel
        ////            {
        ////                ItemId = itemId,
        ////                ResponseObjectModel = new ResponseObjectModel
        ////                {
        ////                    ValidationSummaryMessage = "Item spec deleted successfully!!!",
        ////                },
        ////            },
        ////        };
        ////    }
        ////    catch (Exception exception)
        ////    {
        ////        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        ////        itemInfoListAddEditModel = new ItemInfoListAddEditModel
        ////        {
        ////            ItemInfoModel = new ItemInfoModel
        ////            {
        ////                ItemId = itemId,
        ////                ResponseObjectModel = new ResponseObjectModel
        ////                {
        ////                    ValidationSummaryMessage = "PLEASE FIX ERRORS TO CONTINUE???",
        ////                },
        ////            },
        ////        };
        ////        modelStateDictionary.AddModelError("", "Error while deleting Item Spec");
        ////        modelStateDictionary.AddModelError("", "Please try again");
        ////        modelStateDictionary.AddModelError("", "If problem persists, please contact support personnel");
        ////    }
        ////    finally
        ////    {
        ////        try
        ////        {
        ////            ApplicationDataContext.CloseSqlConnection();
        ////        }
        ////        catch
        ////        {
        ////        }
        ////    }
        ////    return itemInfoListAddEditModel;
        ////}
        ////GET ItemInfoList
        //public ItemInfoListModel ItemInfoList(long itemId, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ItemInfoListModel itemInfoListModel = new ItemInfoListModel
        //        {
        //            ItemId = itemId,
        //            ItemModel = RetailSlnCache.ItemModels.First(x => x.ItemId == itemId),
        //            ItemInfoModels = ItemInfos(itemId, clientId, ipAddress, execUniqueId, loggedInUserId),
        //        };
        //        itemInfoListModel.ItemInfoModels.Insert(0, new ItemInfoModel { ItemInfoLabelText = "Attribute(s)" });
        //        return itemInfoListModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //    }
        //}
        //////GET ItemInfoListAddEditModel
        ////public ItemInfoListAddEditModel ItemInfoListAddEdit(long itemId, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        ////{
        ////    string methodName = MethodBase.GetCurrentMethod().Name;
        ////    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        ////    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        ////    try
        ////    {
        ////        //int x = 1, y = 0, z = x / y;
        ////        ApplicationDataContext.OpenSqlConnection();
        ////        ItemInfoListAddEditModel itemDescListAddEditModel = new ItemInfoListAddEditModel
        ////        {
        ////            ItemInfoListModel = ItemInfoList(itemId, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId),
        ////            ItemInfoModel = new ItemInfoModel
        ////            {
        ////                ItemId = itemId,
        ////            },
        ////        };
        ////        return itemDescListAddEditModel;
        ////    }
        ////    catch (Exception exception)
        ////    {
        ////        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        ////        throw;
        ////    }
        ////    finally
        ////    {
        ////        try
        ////        {
        ////            ApplicationDataContext.CloseSqlConnection();
        ////        }
        ////        catch
        ////        {
        ////        }
        ////    }
        ////}
        ////ItemInfos
        //public List<ItemInfoModel> ItemInfos(long itemId, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ApplicationDataContext.OpenSqlConnection();
        //        List<ItemInfoModel> itemInfoModels = ApplicationDataContext.GetItemInfos(itemId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);

        //        return itemInfoModels;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            ApplicationDataContext.CloseSqlConnection();
        //        }
        //        catch
        //        {
        //        }
        //    }
        //}
        ////GET OrderList
        //public List<OrderListModel> OrderList(long? personId, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        ApplicationDataContext.OpenSqlConnection();
        //        List<OrderListModel> orderListModels = ApplicationDataContext.GetOrderLists(personId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId );
        //        return orderListModels;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            ApplicationDataContext.CloseSqlConnection();
        //        }
        //        catch
        //        {

        //        }
        //    }
        //}
        ////GET RefreshCache
        //public void RefreshCacheSubmit(HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        RetailSlnCache.Initialize(clientId, ipAddress, execUniqueId, loggedInUserId);
        //        //int x = 1, y = 0, z = x / y;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //        try
        //        {
        //        }
        //        catch
        //        {

        //        }
        //    }
        //}
        ////GET GiftCertList
        //public GiftCertListModel GiftCertList(HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        ApplicationDataContext.OpenSqlConnection();
        //        GiftCertListModel giftCertListModel = new GiftCertListModel();
        //        giftCertListModel.GiftCertModels = ApplicationDataContext.GetGiftCerts(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        return giftCertListModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception Occurred", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            ApplicationDataContext.CloseSqlConnection();
        //        }
        //        catch
        //        {

        //        }
        //    }
        //}
        //public void AssignItemSpecMaster(ItemSpecListModel itemSpecListModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    int i = -1;
        //    foreach (var itemSpecMasterModel in RetailSlnCache.ItemSpecMasterModels)
        //    {
        //        i++;
        //        itemSpecListModel.ItemSpecModels[i].ItemSpecMasterId = itemSpecMasterModel.ItemSpecMasterId.Value;
        //        itemSpecListModel.ItemSpecModels[i].ItemSpecMasterModel = itemSpecMasterModel;
        //    }
        //}
        //private string UploadFile(long id, string serverFileName, string fileNamePrefix, string serverRelativeUrl, HttpPostedFileBase httpPostedFileBase,  int resizedWidth, int resizedHeight, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string serverPathName = Utilities.GetServerMapPath(serverRelativeUrl);
        //    if (!string.IsNullOrWhiteSpace(serverFileName))
        //    {
        //        File.Delete(serverPathName + @"\" + serverFileName);
        //    }
        //    string imageFileName = "";
        //    using (var binaryReader = new BinaryReader(httpPostedFileBase.InputStream))
        //    {
        //        byte[] fileData = binaryReader.ReadBytes(httpPostedFileBase.ContentLength);
        //        using (var memoryStream = new MemoryStream(fileData))
        //        {
        //            var originalImage = Image.FromStream(memoryStream);
        //            var resizedImage = new Bitmap(originalImage, resizedWidth, resizedHeight);
        //            ImageConverter imageConverter = new ImageConverter();
        //            var contentByteData = (byte[])imageConverter.ConvertTo(resizedImage, typeof(byte[]));
        //            imageFileName = fileNamePrefix + id + Path.GetExtension(httpPostedFileBase.FileName);
        //            string fullFileName = serverPathName + @"\" + imageFileName;
        //            using (FileStream fileStream = File.Create(fullFileName))
        //            {
        //                fileStream.Write(contentByteData, 0, contentByteData.Length);
        //                fileStream.Close();
        //            }
        //            memoryStream.Close();
        //        }
        //        binaryReader.Close();
        //    }
        //    return imageFileName;
        //}
        //#endregion
    }
}
