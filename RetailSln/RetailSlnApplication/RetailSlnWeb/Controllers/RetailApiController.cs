using ArchitectureLibraryCacheData;
using ArchitectureLibraryEnumerations;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using Microsoft.Owin.BuilderProperties;
using Newtonsoft.Json;
using RetailSlnBusinessLayer;
using RetailSlnCacheData;
using RetailSlnEnumerations;
using RetailSlnModels;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AllowAnonymous = System.Web.Mvc.AllowAnonymousAttribute;
using HttpGet = System.Web.Mvc.HttpGetAttribute;
using HttpPost = System.Web.Mvc.HttpPostAttribute;

namespace RetailSlnWeb.Controllers
{
    [OutputCache(Duration = 0, NoStore = true, VaryByParam = "*")]
    public class RetailApiController : Controller
    {
        private readonly long clientId = long.Parse(Utilities.GetApplicationValue("ClientId"));
        private readonly string execUniqueId = Utilities.CreateExecUniqueId();
        private readonly string lastIpAddress = Utilities.GetLastIPAddress();

        // GET: RetailApi
        [AllowAnonymous]
        [HttpGet]
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
                    ApiBusinessInfoModel = retailSlnBL.BusinessInfo(Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId),
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

        // GET: Categorys
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Categorys(string id, string parentCategoryId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request, lastIpAddress, ArchLibCache.IpInfoClientAccessToken), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                ApiCategorysModel apiCategorysModel = retailSlnBL.Categorys(id, parentCategoryId, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = Json(new { jsonString = JsonConvert.SerializeObject(apiCategorysModel) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ApiCategorysModel apiCategorysModel = new ApiCategorysModel
                {
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Error,
                    },
                };
                actionResult = Json(new { jsonString = JsonConvert.SerializeObject(apiCategorysModel) }, JsonRequestBehavior.AllowGet);
            }
            return actionResult;
        }

        // GET: ItemMasters
        [AllowAnonymous]
        [HttpGet]
        public ActionResult ItemMasters(string id, string pageNum, string rowCount)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request, lastIpAddress, ArchLibCache.IpInfoClientAccessToken), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                ApiItemMastersModel apiItemMastersModel = new ApiItemMastersModel
                {
                    ApiItemMasterModels = null,//retailSlnBL.ItemMasters(id, pageNum, rowCount, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId),
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Success,
                    },
                };
                actionResult = Json(new { jsonString = JsonConvert.SerializeObject(apiItemMastersModel) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ApiItemMastersModel apiItemMastersModel = new ApiItemMastersModel
                {
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Error,
                    },
                };
                actionResult = Json(new { jsonString = JsonConvert.SerializeObject(apiItemMastersModel) }, JsonRequestBehavior.AllowGet);
            }
            return actionResult;
        }
    }
}
