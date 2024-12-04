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

        // GET: Categorys
        //[AllowAnonymous]
        //[HttpGet]
        //public ActionResult Categorys(string id)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request, lastIpAddress, ArchLibCache.IpInfoClientAccessToken), loggedInUserId = Utilities.GetLoggedInUserId(Session);
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    ActionResult actionResult;
        //    RetailSlnBL retailSlnBL = new RetailSlnBL();
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        ApiCategorysModel apiCategorysModel = new ApiCategorysModel
        //        {
        //            ApiCategoryModels = retailSlnBL.Categorys(id, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId),
        //            ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ResponseTypeId = ResponseTypeEnum.Success,
        //            },
        //        };
        //        actionResult = Json(new { jsonString = JsonConvert.SerializeObject(apiCategorysModel) }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        ApiCategorysModel apiCategorysModel = new ApiCategorysModel
        //        {
        //            ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ResponseTypeId = ResponseTypeEnum.Error,
        //            },
        //        };
        //        actionResult = Json(new { jsonString = JsonConvert.SerializeObject(apiCategorysModel) }, JsonRequestBehavior.AllowGet);
        //    }
        //    return actionResult;
        //}

        // GET: Category
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Category(string id)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request, lastIpAddress, ArchLibCache.IpInfoClientAccessToken), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                ApiCategoryModel apiCategoryModel = retailSlnBL.Category(id, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                apiCategoryModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ResponseTypeId = ResponseTypeEnum.Success,
                };
                actionResult = Json(new { jsonString = JsonConvert.SerializeObject(apiCategoryModel) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ApiCategoryModel apiCategoryModel = new ApiCategoryModel
                {
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Error,
                    },
                };
                actionResult = Json(new { jsonString = JsonConvert.SerializeObject(apiCategoryModel) }, JsonRequestBehavior.AllowGet);
            }
            return actionResult;
        }

        // GET: ItemMasters
        //[AllowAnonymous]
        //[HttpGet]
        //public ActionResult ItemMasters(string id, string pageNum, string rowCount)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request, lastIpAddress, ArchLibCache.IpInfoClientAccessToken), loggedInUserId = Utilities.GetLoggedInUserId(Session);
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    ActionResult actionResult;
        //    RetailSlnBL retailSlnBL = new RetailSlnBL();
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        ApiItemMastersModel apiItemMastersModel = new ApiItemMastersModel
        //        {
        //            ApiItemMasterModels = retailSlnBL.ItemMasters(id, pageNum, rowCount, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId),
        //            ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ResponseTypeId = ResponseTypeEnum.Success,
        //            },
        //        };
        //        actionResult = Json(new { jsonString = JsonConvert.SerializeObject(apiItemMastersModel) }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        ApiItemMastersModel apiItemMastersModel = new ApiItemMastersModel
        //        {
        //            ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ResponseTypeId = ResponseTypeEnum.Error,
        //            },
        //        };
        //        actionResult = Json(new { jsonString = JsonConvert.SerializeObject(apiItemMastersModel) }, JsonRequestBehavior.AllowGet);
        //    }
        //    return actionResult;
        //}

        // GET: ItemMaster
        //[AllowAnonymous]
        //[HttpGet]
        //public ActionResult ItemMaster(string id)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request, lastIpAddress, ArchLibCache.IpInfoClientAccessToken), loggedInUserId = Utilities.GetLoggedInUserId(Session);
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    ActionResult actionResult;
        //    RetailSlnBL retailSlnBL = new RetailSlnBL();
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        ApiItemMasterModel apiItemMasterModel = retailSlnBL.ItemMaster(id, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        apiItemMasterModel.ResponseObjectModel = new ResponseObjectModel
        //        {
        //            ResponseTypeId = ResponseTypeEnum.Success,
        //        };
        //        actionResult = Json(new { jsonString = JsonConvert.SerializeObject(apiItemMasterModel) }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        ApiItemMasterModel apiItemMasterModel = new ApiItemMasterModel
        //        {
        //            ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ResponseTypeId = ResponseTypeEnum.Error,
        //            },
        //        };
        //        actionResult = Json(new { jsonString = JsonConvert.SerializeObject(apiItemMasterModel) }, JsonRequestBehavior.AllowGet);
        //    }
        //    return actionResult;
        //}

        #region Comments
        //// GET: Categories
        //[AllowAnonymous]
        //[HttpGet]
        //public ActionResult ItemMasters()
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request, lastIpAddress, ArchLibCache.IpInfoClientAccessToken), loggedInUserId = Utilities.GetLoggedInUserId(Session);
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    ActionResult actionResult;
        //    RetailSlnBL retailSlnBL = new RetailSlnBL();
        //    string id = null;
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        ApiCategorysModel apiCategorysModel = new ApiCategorysModel
        //        {
        //            CategoryModels = retailSlnBL.Categorys(id, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId),
        //            ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ResponseTypeId = ResponseTypeEnum.Success,
        //            },
        //        };
        //        actionResult = Json(new { jsonString = JsonConvert.SerializeObject(apiCategorysModel) }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        ApiCategorysModel apiCategoriesModel = new ApiCategorysModel
        //        {
        //            ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ResponseTypeId = ResponseTypeEnum.Error,
        //            },
        //        };
        //        actionResult = Json(new { jsonString = JsonConvert.SerializeObject(apiCategoriesModel) }, JsonRequestBehavior.AllowGet);
        //    }
        //    return actionResult;
        //}

        //// GET: ItemMasters
        //[AllowAnonymous]
        //[HttpGet]
        //public ActionResult ItemMasters(string id, string pageNum, string rowCount)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request, lastIpAddress, ArchLibCache.IpInfoClientAccessToken), loggedInUserId = Utilities.GetLoggedInUserId(Session);
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    ActionResult actionResult;
        //    RetailSlnBL retailSlnBL = new RetailSlnBL();
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        ApiItemMasterModel apiItemMasterModel = new ApiItemMasterModel
        //        {
        //            ItemMasterModels = retailSlnBL.ItemMasters(id, pageNum, rowCount, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId),
        //            ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ResponseTypeId = ResponseTypeEnum.Success,
        //            },
        //        };
        //        actionResult = Json(new { jsonString = JsonConvert.SerializeObject(apiItemMasterModel) }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        ApiCategorysModel apiCategoriesModel = new ApiCategorysModel
        //        {
        //            ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ResponseTypeId = ResponseTypeEnum.Error,
        //            },
        //        };
        //        actionResult = Json(new { jsonString = JsonConvert.SerializeObject(apiCategoriesModel) }, JsonRequestBehavior.AllowGet);
        //    }
        //    return actionResult;
        //}

        //// GET: ItemMasters
        //[AllowAnonymous]
        //[HttpGet]
        //public ActionResult ItemSpecs(string id)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request, lastIpAddress, ArchLibCache.IpInfoClientAccessToken), loggedInUserId = Utilities.GetLoggedInUserId(Session);
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    ActionResult actionResult;
        //    RetailSlnBL retailSlnBL = new RetailSlnBL();
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        ApiItemMasterSpecModel apiItemMasterSpecModel = new ApiItemMasterSpecModel
        //        {
        //            //ItemMasterModels = retailSlnBL.ItemSpecs(id, pageNum, rowCount, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId),
        //            ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ResponseTypeId = ResponseTypeEnum.Success,
        //            },
        //        };
        //        actionResult = Json(new { jsonString = JsonConvert.SerializeObject(apiItemMasterSpecModel) }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        ApiItemMasterSpecModel apiItemMasterSpecModel = new ApiItemMasterSpecModel
        //        {
        //            ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ResponseTypeId = ResponseTypeEnum.Error,
        //            },
        //        };
        //        actionResult = Json(new { jsonString = JsonConvert.SerializeObject(apiItemMasterSpecModel) }, JsonRequestBehavior.AllowGet);
        //    }
        //    return actionResult;
        //}
        #endregion

        #region Comments
        //// GET: ItemAttributes
        //[AllowAnonymous]
        //[HttpGet]
        //public ActionResult ItemAttributes(string id)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request, lastIpAddress, ArchLibCache.IpInfoClientAccessToken), loggedInUserId = Utilities.GetLoggedInUserId(Session);
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    ActionResult actionResult;
        //    RetailSlnBL retailSlnBL = new RetailSlnBL();
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        long itemMasterId = long.Parse(id);
        //        ItemMasterAttributesModel itemMasterAttributesModel = retailSlnBL.ItemMasterAttributes(itemMasterId, 0, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        //ApiItemMasterAttributesModel apiItemMasterModel = new ApiItemMasterAttributesModel
        //        //{
        //        //    ItemMasterModels = retailSlnBL.ItemMasterAttributes(long.Parse(id), clientId, ipAddress, execUniqueId, loggedInUserId),
        //        //    ResponseObjectModel = new ResponseObjectModel
        //        //    {
        //        //        ResponseTypeId = ResponseTypeEnum.Success,
        //        //    },
        //        //};
        //        actionResult = Json(new { jsonString = JsonConvert.SerializeObject(itemMasterAttributesModel) }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        ItemMasterAttributesModel itemMasterAttributesModel = new ItemMasterAttributesModel
        //        {
        //            ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ResponseTypeId = ResponseTypeEnum.Error,
        //            },
        //        };
        //        actionResult = Json(new { jsonString = JsonConvert.SerializeObject(itemMasterAttributesModel) }, JsonRequestBehavior.AllowGet);
        //    }
        //    return actionResult;
        //}
        //// GET: ItemBundle
        //[AllowAnonymous]
        //[HttpGet]
        //public ActionResult ItemBundle(string id)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request, lastIpAddress, ArchLibCache.IpInfoClientAccessToken), loggedInUserId = Utilities.GetLoggedInUserId(Session);
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    ActionResult actionResult;
        //    RetailSlnBL retailSlnBL = new RetailSlnBL();
        //    //try
        //    //{
        //    //    //int x = 1, y = 0, z = x / y;
        //    //    ApiItemMasterModel apiItemMasterModel = new ApiItemMasterModel
        //    //    {
        //    //        ItemMasterModels = retailSlnBL.ItemMasters(id, pageNum, rowCount, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId),
        //    //        ResponseObjectModel = new ResponseObjectModel
        //    //        {
        //    //            ResponseTypeId = ResponseTypeEnum.Success,
        //    //        },
        //    //    };
        //    //    actionResult = Json(new { jsonString = JsonConvert.SerializeObject(apiItemMasterModel) }, JsonRequestBehavior.AllowGet);
        //    //}
        //    //catch (Exception exception)
        //    //{
        //    //    exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //    //    ApiCategorysModel apiCategoriesModel = new ApiCategorysModel
        //    //    {
        //    //        ResponseObjectModel = new ResponseObjectModel
        //    //        {
        //    //            ResponseTypeId = ResponseTypeEnum.Error,
        //    //        },
        //    //    };
        //    //    actionResult = Json(new { jsonString = JsonConvert.SerializeObject(apiCategoriesModel) }, JsonRequestBehavior.AllowGet);
        //    //}
        //    //return actionResult;
        //    return null;
        //}
        //// GET: ItemMasters
        //[AllowAnonymous]
        //[HttpGet]
        //public ActionResult ItemSpecs(string id, string pageNum, string rowCount)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request, lastIpAddress, ArchLibCache.IpInfoClientAccessToken), loggedInUserId = Utilities.GetLoggedInUserId(Session);
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    ActionResult actionResult;
        //    RetailSlnBL retailSlnBL = new RetailSlnBL();
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        ApiItemMasterModel apiItemMasterModel = new ApiItemMasterModel
        //        {
        //            ItemMasterModels = retailSlnBL.ItemMasters(id, pageNum, rowCount, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId),
        //            ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ResponseTypeId = ResponseTypeEnum.Success,
        //            },
        //        };
        //        actionResult = Json(new { jsonString = JsonConvert.SerializeObject(apiItemMasterModel) }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        ApiCategorysModel apiCategoriesModel = new ApiCategorysModel
        //        {
        //            ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ResponseTypeId = ResponseTypeEnum.Error,
        //            },
        //        };
        //        actionResult = Json(new { jsonString = JsonConvert.SerializeObject(apiCategoriesModel) }, JsonRequestBehavior.AllowGet);
        //    }
        //    return actionResult;
        //}
        #endregion

        #region Commented
        //// GET: Items
        //[AllowAnonymous]
        //[HttpGet]
        //public ActionResult Items(string id, string pageNum, string rowCount)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request, lastIpAddress, ArchLibCache.IpInfoClientAccessToken), loggedInUserId = Utilities.GetLoggedInUserId(Session);
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    ActionResult actionResult;
        //    RetailSlnBL retailSlnBL = new RetailSlnBL();
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        ApiItemsModel apiItemsModel = new ApiItemsModel
        //        {
        //            ItemModels = null,//retailSlnBL.Items(id, pageNum, rowCount, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId),
        //            ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ResponseTypeId = ResponseTypeEnum.Success,
        //            },
        //        };
        //        actionResult = Json(new { jsonString = JsonConvert.SerializeObject(apiItemsModel) }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        ApiCategorysModel apiCategoriesModel = new ApiCategorysModel
        //        {
        //            ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ResponseTypeId = ResponseTypeEnum.Error,
        //            },
        //        };
        //        actionResult = Json(new { jsonString = JsonConvert.SerializeObject(apiCategoriesModel) }, JsonRequestBehavior.AllowGet);
        //    }
        //    return actionResult;
        //}
        #endregion

        // GET: LoginUserProf
        [AllowAnonymous]
        [HttpPost]
        public ActionResult LoginUserProf([FromBody] string LoginEmailAddress, [FromBody]string LoginPassword)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request, lastIpAddress, ArchLibCache.IpInfoClientAccessToken), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ApiLoginUserProfModel apiLoginUserProfModel;
            try
            {
                //int x = 1, y = 0, z = x / y;
                apiLoginUserProfModel = retailSlnBL.LoginUserProf(LoginEmailAddress, LoginPassword, clientId, ipAddress, execUniqueId, loggedInUserId);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                apiLoginUserProfModel = new ApiLoginUserProfModel
                {
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseMessages = new List<string>
                        {
                            exception.Message,
                        },
                        ResponseTypeId = ResponseTypeEnum.Error,
                    },
                };
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            actionResult = Json(new { jsonString = apiLoginUserProfModel }, JsonRequestBehavior.AllowGet);
            return actionResult;
        }

        // GET: LoginUserProf
        [HttpGet]
        [JwtAuthentication]
        public ActionResult SessionInfo()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request, lastIpAddress, ArchLibCache.IpInfoClientAccessToken), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ApiLoginUserProfModel apiLoginUserProfModel;
            try
            {
                //int x = 1, y = 0, z = x / y;
                var jwtToken = Request.Headers["Authorization"].Substring(7);
                apiLoginUserProfModel = retailSlnBL.SessionInfo(jwtToken, clientId, ipAddress, execUniqueId, loggedInUserId);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                apiLoginUserProfModel = new ApiLoginUserProfModel
                {
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseMessages = new List<string>
                        {
                            exception.Message,
                        },
                        ResponseTypeId = ResponseTypeEnum.Error,
                    },
                };
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            actionResult = Json(new { jsonString = apiLoginUserProfModel }, JsonRequestBehavior.AllowGet);
            return actionResult;
        }

        [HttpGet]
        public ActionResult CreateShoppingCartInput()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request, lastIpAddress, ArchLibCache.IpInfoClientAccessToken), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            //string x = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImN1b25nIiwibmJmIjoxNzE3NTUxNDE0LCJleHAiOjE3MTc1NTI2MTQsImlhdCI6MTcxNzU1MTQxNH0.FpKGIix8FH9azm9aLT7oW8pl0hT-xRz3aq-mPpoJ1wk";
            ApiShoppingCartModel apiShoppingCartModel = retailSlnBL.CreateShoppingCartInput();
            actionResult = Json(new { apiShoppingCartModel }, JsonRequestBehavior.AllowGet);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        // POST: ProcessShoppingCart
        [HttpPost]
        //[JwtAuthentication]
        public ActionResult ProcessShoppingCart(ApiShoppingCartModel apiShoppingCartModel)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request, lastIpAddress, ArchLibCache.IpInfoClientAccessToken), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                apiShoppingCartModel.JwtToken = Request.Headers["Authorization"].Substring(7);
                retailSlnBL.ProcessShoppingCart(apiShoppingCartModel, clientId, ipAddress, execUniqueId, loggedInUserId);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                apiShoppingCartModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ResponseMessages = new List<string>
                    {
                        "Error while processing shopping cart"
                    },
                    ResponseTypeId = ResponseTypeEnum.Error,
                };
            }
            actionResult = Json(new { apiShoppingCartModel }, JsonRequestBehavior.AllowGet);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        // GET: SearchResult
        [AllowAnonymous]
        [HttpGet]
        public ActionResult SearchResult(string id, string pageNum, string rowCount)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request, lastIpAddress, ArchLibCache.IpInfoClientAccessToken), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ArchitectureLibraryException.ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ApiSearchResultModel apiSearchResultModel;
            var searchText = id.Trim().ToLower();
            try
            {
                apiSearchResultModel = new ApiSearchResultModel();
                apiSearchResultModel = retailSlnBL.SearchResultApi(id, clientId, ipAddress, execUniqueId, loggedInUserId);
            }
            catch (Exception exception)
            {
                apiSearchResultModel = new ApiSearchResultModel
                {
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Error,
                    },
                };
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
            }
            actionResult = Json(new { jsonString = JsonConvert.SerializeObject(apiSearchResultModel) }, JsonRequestBehavior.AllowGet);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        // GET: ShoppingCart
        [HttpGet]
        public ActionResult ShoppingCart()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request, lastIpAddress, ArchLibCache.IpInfoClientAccessToken), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            actionResult = View();
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        // POST: ShoppingCart
        [HttpPost]
        public ActionResult ShoppingCart(string ShoppingCartInput)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request, lastIpAddress, ArchLibCache.IpInfoClientAccessToken), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            //ApiShoppingCartModel apiShoppingCartModel1 = retailSlnBL.CreateShoppingCartInput();
            ApiShoppingCartModel apiShoppingCartModel = JsonConvert.DeserializeObject<ApiShoppingCartModel>(ShoppingCartInput);
            try
            {
                //int x = 1, y = 0, z = x / y;
                retailSlnBL.ProcessShoppingCart(apiShoppingCartModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = PartialView("_ShoppingCart", apiShoppingCartModel);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                apiShoppingCartModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ResponseMessages = new List<string>
                    {
                        exception.Message,
                        "Error while processing shopping cart"
                    },
                    ResponseTypeId = ResponseTypeEnum.Error,
                };
                actionResult = PartialView("_ResponseObject", apiShoppingCartModel.ResponseObjectModel);
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }
    }
}
