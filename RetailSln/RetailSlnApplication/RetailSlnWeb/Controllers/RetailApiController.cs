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
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
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

        // GET: Categories
        [AllowAnonymous]
        [HttpGet]
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
                ApiItemMasterModel apiItemMasterModel = new ApiItemMasterModel
                {
                    ItemMasterModels = retailSlnBL.ItemMasters(id, pageNum, rowCount, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId),
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Success,
                    },
                };
                actionResult = Json(new { jsonString = JsonConvert.SerializeObject(apiItemMasterModel) }, JsonRequestBehavior.AllowGet);
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

        #region
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
                var itemModels = RetailSlnCache.ItemModels.Where(x => x.ItemShortDesc0.ToLower().Contains(searchText) || x.ItemShortDesc1.ToLower().Contains(searchText)).ToList();
                apiSearchResultModel = new ApiSearchResultModel
                {
                    CategoryModels = RetailSlnCache.CategoryModels.Where(x => x.CategoryDesc.ToLower().Contains(searchText)).ToList(),
                    ItemModels = itemModels,
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Success,
                    },
                };
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
    }
}
