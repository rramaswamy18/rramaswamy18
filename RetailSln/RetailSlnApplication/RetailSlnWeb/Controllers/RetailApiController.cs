using ArchitectureLibraryCacheData;
using ArchitectureLibraryEnumerations;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using Newtonsoft.Json;
using RetailSlnBusinessLayer;
using RetailSlnModels;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace RetailSlnWeb.Controllers
{
    [OutputCache(Duration = 0, NoStore = true, VaryByParam = "*")]
    public class RetailApiController : Controller
    {
        private readonly long clientId = long.Parse(Utilities.GetApplicationValue("ClientId"));
        private readonly string execUniqueId = Utilities.CreateExecUniqueId();
        private readonly string lastIpAddress = Utilities.GetLastIPAddress();

        // GET: RetailApi
        public ActionResult Index()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request, lastIpAddress, ArchLibCache.IpInfoClientAccessToken), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                ApiIndexModel apiIndexModel = new ApiIndexModel
                {
                    BusinessInfoModel = retailSlnBL.BusinessInfo(Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId),
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Success,
                    },
                };
                actionResult = Json(new { jsonString = JsonConvert.SerializeObject(apiIndexModel) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ApiIndexModel apiIndexModel = new ApiIndexModel
                {
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Error,
                    },
                };
                actionResult = Json(new { jsonString = JsonConvert.SerializeObject(apiIndexModel) }, JsonRequestBehavior.AllowGet);
            }
            return actionResult;
        }

        // GET: Categories
        public ActionResult Categorys()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request, lastIpAddress, ArchLibCache.IpInfoClientAccessToken), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            string id = null;
            try
            {
                //int x = 1, y = 0, z = x / y;
                ApiCategorysModel apiCategorysModel = new ApiCategorysModel
                {
                    CategoryModels = retailSlnBL.Categorys(id, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId),
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Success,
                    },
                };
                actionResult = Json(new { jsonString = JsonConvert.SerializeObject(apiCategorysModel) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ApiCategorysModel apiCategoriesModel = new ApiCategorysModel
                {
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Error,
                    },
                };
                actionResult = Json(new { jsonString = JsonConvert.SerializeObject(apiCategoriesModel) }, JsonRequestBehavior.AllowGet);
            }
            return actionResult;
        }

        // GET: Items
        public ActionResult Items(string id, string pageNum, string rowCount)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request, lastIpAddress, ArchLibCache.IpInfoClientAccessToken), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                ApiItemsModel apiItemsModel = new ApiItemsModel
                {
                    ItemModels = retailSlnBL.Items(id, pageNum, rowCount, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId),
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Success,
                    },
                };
                actionResult = Json(new { jsonString = JsonConvert.SerializeObject(apiItemsModel) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ApiCategorysModel apiCategoriesModel = new ApiCategorysModel
                {
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Error,
                    },
                };
                actionResult = Json(new { jsonString = JsonConvert.SerializeObject(apiCategoriesModel) }, JsonRequestBehavior.AllowGet);
            }
            return actionResult;
        }
    }
}
