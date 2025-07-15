using ArchitectureLibraryEnumerations;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using RetailSlnDataLayer;
using RetailSlnEnumerations;
using RetailSlnModels;
using System;
using System.Collections.Generic;
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
                int.TryParse(pageSizeParm, out int pageSize);
                if (pageNum == 0 )
                {
                    pageNum = 1;
                }
                if (pageSize == 0)
                {
                    pageSize = 45;
                }
                pageSize = 9999;
                int offSetCount = (pageNum - 1) * pageSize;
                ApplicationDataContext.OpenSqlConnection();
                SqlConnection sqlConnection = ApplicationDataContext.OpenSqlConnection(true);
                int totalRowCount = ApplicationDataContext.ItemMasterCount(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                int totalPageCount = totalRowCount / pageSize;
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
    }
}
