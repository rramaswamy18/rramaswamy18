using ArchitectureLibraryBusinessLayer;
using ArchitectureLibraryCacheData;
using ArchitectureLibraryCreditCardBusinessLayer;
using ArchitectureLibraryCreditCardModels;
using ArchitectureLibraryEnumerations;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using Newtonsoft.Json;
using RetailSlnBusinessLayer;
using RetailSlnCacheData;
using RetailSlnEnumerations;
using RetailSlnModels;
using RetailSlnWeb.ClassCode;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RetailSlnWeb.Controllers
{
    public partial class HomeController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public ActionResult AddToCart(string id, string itemId, string orderQty)
        {
            ViewData["ActionName"] = "AddToCart";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            int shoppingCartItemsCount = 0;
            string shoppingCartTotalAmount = 0f.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
            try
            {
                //int x = 1, y = 0, z = x / y;
                ShoppingCartModel shoppingCartModel = retailSlnBL.AddToCart(long.Parse(itemId), long.Parse(orderQty), Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                long.TryParse(id, out long tempId);
                success = true;
                processMessage = "SUCCESS!!!";
                htmlString = archLibBL.ViewToHtmlString(this, "_OrderCategoryItem", tempId);
                shoppingCartItemsCount = shoppingCartModel.ShoppingCartItems.Count;
                shoppingCartTotalAmount = shoppingCartModel.ShoppingCartTotalAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                processMessage = "ERROR???";
                htmlString = "Error while adding item to cart";
            }
            actionResult = Json(new { success, processMessage, htmlString, shoppingCartItemsCount, shoppingCartTotalAmount }, JsonRequestBehavior.AllowGet);
            return actionResult;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult AddToCart(AddToCartModel addToCartModel)
        {
            ViewData["ActionName"] = "AddToCart";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            int shoppingCartItemsCount = 0;
            string shoppingCartTotalAmount = 0f.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
            string actionName, aspNetRoleName, controllerName, viewName;
            try
            {
                //int x = 1, y = 0, z = x / y;
                success = false;
                if (addToCartModel.ShoppingCartItemModels != null)
                {
                    foreach (var shoppingCartItemModel in addToCartModel.ShoppingCartItemModels)
                    {
                        if (shoppingCartItemModel.OrderQty != null)
                        {
                            success = true;
                            break;
                        }
                    }
                }
                if (!success)
                {
                    processMessage = "ERROR???";
                    htmlString = "Please enter order quantity for a min of 1 item";
                }
                else
                {
                    ShoppingCartModel shoppingCartModel = retailSlnBL.AddToCart(addToCartModel.ShoppingCartItemModels, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    success = true;
                    processMessage = "SUCCESS!!!";
                    SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
                    if (sessionObjectModel == null)
                    {
                        aspNetRoleName = "DEFAULTROLE";
                    }
                    else
                    {
                        aspNetRoleName = sessionObjectModel.AspNetRoleName;
                    }
                    var aspNetRoleKVPs = ArchLibCache.AspNetRoleKVPs[aspNetRoleName];
                    actionName = aspNetRoleKVPs["ActionName02"].KVPValueData;
                    controllerName = aspNetRoleKVPs["ControllerName02"].KVPValueData;
                    viewName = aspNetRoleKVPs["ViewName02"].KVPValueData;
                    OrderCategoryItemModel orderCategoryItemModel = new OrderCategoryItemModel
                    {
                        ActionName = actionName,
                        ControllerName = controllerName,
                        ParentCategoryId = addToCartModel.ParentCategoryId.Value,
                        PageNum = addToCartModel.PageNum.Value,
                        PageSize = addToCartModel.PageSize.Value,
                        TotalRowCount = addToCartModel.TotalRowCount.Value,
                    };
                    htmlString = archLibBL.ViewToHtmlString(this, viewName, orderCategoryItemModel);
                    shoppingCartItemsCount = shoppingCartModel.ShoppingCartItems.Count;
                    shoppingCartTotalAmount = shoppingCartModel.ShoppingCartTotalAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                processMessage = "ERROR???";
                htmlString = "Error while adding item to cart";
            }
            actionResult = Json(new { success, processMessage, htmlString, shoppingCartItemsCount, shoppingCartTotalAmount }, JsonRequestBehavior.AllowGet);
            return actionResult;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult CategoryListView()
        {
            ViewData["ActionName"] = "CategoryListView";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            try
            {
                //int x = 1, y = 0, z = x / y;
                CategoryListModel categoryListModel = retailSlnBL.CategoryListView(Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("CategoryListView", categoryListModel);
                Response.StatusCode = (int)HttpStatusCode.OK;
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "Category List View / GET");
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            return actionResult;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Checkout")]
        public ActionResult Checkout()
        {
            //int x = 1, y = 0, z = x / y;
            ViewData["ActionName"] = "Checkout";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            try
            {
                //int x = 1, y = 0, z = x / y;
                CheckoutModel checkoutModel = retailSlnBL.Checkout(Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (checkoutModel.ShoppingCartModel == null)
                {
                    actionResult = RedirectToAction("Index");
                }
                else
                {
                    var ctx = Request.GetOwinContext();
                    var authManager = ctx.Authentication;
                    bool loggedIn = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
                    if (loggedIn && Session["SessionObject"] != null)
                    {
                        DeliveryInfoModel deliveryInfoModel = retailSlnBL.DeliveryInfo(Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                        actionResult = View("DeliveryInfo", deliveryInfoModel);
                    }
                    else
                    {
                        actionResult = View("Checkout", checkoutModel);
                    }
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                //ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                //ModelState.AddModelError("", "Checkout / GET");
                //actionResult = View("Error", responseObjectModel);
                actionResult = RedirectToAction("Index");
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Checkout(LoginUserProfModel loginUserProfModel)
        {
            //int x = 1, y = 0, z = x / y;
            ViewData["ActionName"] = "Checkout";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            try
            {
                //int x = 1, y = 0, z = x / y;
                ModelState.Clear();
                TryValidateModel(loginUserProfModel);
                SessionObjectModel sessionObjectModel = archLibBL.LoginUserProf(ref loginUserProfModel, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (ModelState.IsValid)
                {
                    ApplSessionObjectModel applSessionObjectModel = retailSlnBL.LoginUserProf(sessionObjectModel.PersonId, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    sessionObjectModel.ApplSessionObjectModel = applSessionObjectModel;
                    Session["SessionObject"] = sessionObjectModel;
                    Session.Timeout = int.Parse(ConfigurationManager.AppSettings["AccessTokenExpiryMinutes"]);
                    var identity = new ClaimsIdentity
                    (
                        new[]
                        {
                            new Claim(ClaimTypes.Name, sessionObjectModel.FirstName + " " + sessionObjectModel.LastName),
                            new Claim(ClaimTypes.Email, sessionObjectModel.EmailAddress),
                            //new Claim(ClaimTypes.Country, "India"),
                        },
                        "ApplicationCookie"
                    );
                    var ctx = Request.GetOwinContext();
                    var authManager = ctx.Authentication;
                    authManager.SignIn(identity);
                    DeliveryInfoModel deliveryInfoModel = retailSlnBL.DeliveryInfo(Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    success = true;
                    processMessage = "SUCCESS!!!";
                    htmlString = archLibBL.ViewToHtmlString(this, "_DeliveryInfo", deliveryInfoModel);
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
                }
                else
                {
                    success = false;
                    processMessage = "ERROR???";
                    loginUserProfModel.ResponseObjectModel = new ResponseObjectModel
                    {
                        ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                    };
                    htmlString = archLibBL.ViewToHtmlString(this, "_LoginUserProfData", loginUserProfModel);
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process Error");
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                archLibBL.GenerateCaptchaQuesion(Session, "CaptchaNumberLogin0", "CaptchaNumberLogin1");
                loginUserProfModel.CaptchaAnswerLogin = null;
                loginUserProfModel.CaptchaNumberLogin0 = Session["CaptchaNumberLogin0"].ToString();
                loginUserProfModel.CaptchaNumberLogin1 = Session["CaptchaNumberLogin1"].ToString();
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                success = false;
                processMessage = "ERROR???";
                loginUserProfModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                };
                htmlString = archLibBL.ViewToHtmlString(this, "_LoginUserProfData", loginUserProfModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00099100 :: Error Exit");
            }
            actionResult = Json(new { success, processMessage, htmlString });
            return actionResult;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult CheckoutGuest(CheckoutGuestModel checkoutGuestModel)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            try
            {
                //int x = 1, y = 0, z = x / y;
                ModelState.Clear();
                TryValidateModel(checkoutGuestModel);
                SessionObjectModel sessionObjectModel = archLibBL.CheckoutGuest(ref checkoutGuestModel, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (ModelState.IsValid)
                {
                    Session["SessionObject"] = sessionObjectModel;
                    Session.Timeout = int.Parse(ConfigurationManager.AppSettings["AccessTokenExpiryMinutes"]);
                    var identity = new ClaimsIdentity
                    (
                        new[]
                        {
                            new Claim(ClaimTypes.Name, sessionObjectModel.FirstName + " " + sessionObjectModel.LastName),
                            new Claim(ClaimTypes.Email, sessionObjectModel.EmailAddress),
                            new Claim(ClaimTypes.Role, sessionObjectModel.AspNetRoleName),
                            //new Claim(ClaimTypes.Country, "India"),
                        },
                        "ApplicationCookie"
                    );
                    var ctx = Request.GetOwinContext();
                    var authManager = ctx.Authentication;
                    authManager.SignIn(identity);
                    DeliveryInfoModel deliveryInfoModel = retailSlnBL.DeliveryInfo(Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    success = true;
                    processMessage = "SUCCESS!!!";
                    htmlString = archLibBL.ViewToHtmlString(this, "_DeliveryInfo", deliveryInfoModel);
                    string loggedInUserFullName = sessionObjectModel.FirstName + " " + sessionObjectModel.LastName;
                    string loggedInUserEmailAddress = sessionObjectModel.EmailAddress;
                    actionResult = Json(new { success, processMessage, htmlString, loggedInUserFullName, loggedInUserEmailAddress });
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
                }
                else
                {
                    success = false;
                    processMessage = "ERROR???";
                    htmlString = archLibBL.ViewToHtmlString(this, "_CheckoutGuestData", checkoutGuestModel);
                    actionResult = Json(new { success, processMessage, htmlString });
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process Error");
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                archLibBL.GenerateCaptchaQuesion(Session, "CaptchaNumberCheckoutGuest0", "CaptchaNumberCheckoutGuest1");
                checkoutGuestModel.CaptchaAnswerCheckoutGuest = null;
                checkoutGuestModel.CaptchaNumberCheckoutGuest0 = Session["CaptchaNumberCheckoutGuest0"].ToString();
                checkoutGuestModel.CaptchaNumberCheckoutGuest1 = Session["CaptchaNumberCheckoutGuest1"].ToString();
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                htmlString = archLibBL.ViewToHtmlString(this, "_CheckoutGuestData", checkoutGuestModel);
                success = false;
                processMessage = "ERROR???";
                actionResult = Json(new { success, processMessage, htmlString });
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00099100 :: Error Exit");
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult CheckoutValidate()
        {
            //int x = 1, y = 0, z = x / y;
            ViewData["ActionName"] = "CheckoutValidate";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            int shoppingCartItemsCount = 0;
            string shoppingCartTotalAmount = 0f.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
            try
            {
                //int x = 1, y = 0, z = x / y;
                ShoppingCartModel shoppingCartModel = retailSlnBL.CheckoutValidate(Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                shoppingCartItemsCount = shoppingCartModel.ShoppingCartItems.Count;
                shoppingCartTotalAmount = shoppingCartModel.ShoppingCartTotalAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                if (ModelState.IsValid)
                {
                    success = true;
                    processMessage = "SUCCESS!!!";
                    htmlString = "";
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
                }
                else
                {
                    success = false;
                    processMessage = "ERROR???";
                    htmlString = "Error during checkout";
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process Error");
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                processMessage = "ERROR???";
                htmlString = "Error during checkout";
            }
            actionResult = Json(new { success, processMessage, htmlString, shoppingCartItemsCount, shoppingCartTotalAmount }, JsonRequestBehavior.AllowGet);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        [Authorize]
        [AjaxAuthorize]
        [HttpPost]
        public ActionResult DeliveryInfo(DeliveryInfoDataModel deliveryInfoDataModel)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            bool success;
            string processMessage, htmlString;
            ArchLibBL archLibBL = new ArchLibBL();
            ActionResult actionResult;
            try
            {
                ModelState.Clear();
                TryValidateModel(deliveryInfoDataModel);
                TryValidateModel(deliveryInfoDataModel.DeliveryAddressModel, "DeliveryAddressModel");
                if (deliveryInfoDataModel.DeliveryMethodId == DeliveryMethodEnum.PickupFromStore)
                {
                    ModelState.Remove("DeliveryAddressModel.AddressLine1");
                    ModelState.Remove("DeliveryAddressModel.AddressLine2");
                    ModelState.Remove("DeliveryAddressModel.CityName");
                    ModelState.Remove("DeliveryAddressModel.ZipCode");
                    ModelState.Remove("DeliveryAddressModel.DemogInfoSubDivisionId");
                    ModelState.Remove("DeliveryAddressModel.DemogInfoCountryId");
                    ModelState.Remove("DeliveryAddressModel.BuildingTypeId");
                    //ModelState.Remove("");
                    deliveryInfoDataModel.DeliveryAddressModel.AddressLine1 = null;
                    deliveryInfoDataModel.DeliveryAddressModel.AddressLine2 = null;
                    deliveryInfoDataModel.DeliveryAddressModel.AddressLine3 = null;
                    deliveryInfoDataModel.DeliveryAddressModel.CityName = null;
                    deliveryInfoDataModel.DeliveryAddressModel.StateAbbrev = null;
                    deliveryInfoDataModel.DeliveryAddressModel.ZipCode = null;
                    deliveryInfoDataModel.DeliveryAddressModel.DemogInfoCountyId = null;
                    deliveryInfoDataModel.DeliveryAddressModel.DemogInfoSubDivisionId = null;
                    deliveryInfoDataModel.DeliveryAddressModel.DemogInfoCountryId = null;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(deliveryInfoDataModel.DeliveryAddressModel.AddressLine1))
                    {
                        ModelState.AddModelError("DeliveryAddressModel.AddressLine1", "Address line 1");
                    }
                    if (string.IsNullOrWhiteSpace(deliveryInfoDataModel.DeliveryAddressModel.CityName))
                    {
                        ModelState.AddModelError("DeliveryAddressModel.CityName", "City name");
                    }
                    if (string.IsNullOrWhiteSpace(deliveryInfoDataModel.DeliveryAddressModel.ZipCode))
                    {
                        ModelState.AddModelError("DeliveryAddressModel.ZipCode", "Postal Code");
                    }
                    if (deliveryInfoDataModel.DeliveryAddressModel.DemogInfoSubDivisionId == null)
                    {
                        ModelState.AddModelError("DeliveryAddressModel.DemogInfoSubDivisionId", "State");
                    }
                    if (deliveryInfoDataModel.DeliveryAddressModel.DemogInfoCountryId == null)
                    {
                        ModelState.AddModelError("DeliveryAddressModel.DemogInfoCountryId", "Country");
                    }
                    if (deliveryInfoDataModel.DeliveryAddressModel.DemogInfoCountryId != null && !string.IsNullOrWhiteSpace(deliveryInfoDataModel.DeliveryAddressModel.ZipCode))
                    {
                        Regex regex = new Regex(DemogInfoCache.DemogInfoCountryModels.First(x => x.DemogInfoCountryId == deliveryInfoDataModel.DeliveryAddressModel.DemogInfoCountryId.Value).PostalCodeRegEx);
                        if (!regex.IsMatch(deliveryInfoDataModel.DeliveryAddressModel.ZipCode))
                        {
                            ModelState.AddModelError("DeliveryAddressModel.ZipCode", "Postal Code");
                        }
                    }
                    if (ModelState.IsValid)
                    {
                        //Get the Address Info based on Zip and fill in the rest
                        SearchDataModel searchDataModel = new SearchDataModel
                        {
                            SearchType = "ZipCode",
                            SearchKeyValuePairs = new Dictionary<string, string>
                        {
                            { "DemogInfoCountryId", deliveryInfoDataModel.DeliveryAddressModel.DemogInfoCountryId.ToString() },
                            { "ZipCode", deliveryInfoDataModel.DeliveryAddressModel.ZipCode },
                        },
                        };
                        List<Dictionary<string, string>> sqlQueryResults = archLibBL.SearchData(searchDataModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                        foreach (var sqlQueryResult in sqlQueryResults)
                        {
                            if (
                                sqlQueryResult["DemogInfoCountryId"] == deliveryInfoDataModel.DeliveryAddressModel.DemogInfoCountryId.ToString()
                                && sqlQueryResult["ZipCode"] == deliveryInfoDataModel.DeliveryAddressModel.ZipCode
                               )
                            {
                                deliveryInfoDataModel.DeliveryAddressModel.CityName = sqlQueryResult["CityName"];
                                deliveryInfoDataModel.DeliveryAddressModel.CountryAbbrev = sqlQueryResult["CountryAbbrev"];
                                deliveryInfoDataModel.DeliveryAddressModel.CountryDesc = sqlQueryResult["CountryDesc"];
                                deliveryInfoDataModel.DeliveryAddressModel.CountyName = sqlQueryResult["CountyName"];
                                deliveryInfoDataModel.DeliveryAddressModel.DemogInfoCityId = long.Parse(sqlQueryResult["DemogInfoCityId"]);
                                deliveryInfoDataModel.DeliveryAddressModel.DemogInfoCountyId = long.Parse(sqlQueryResult["DemogInfoCountyId"]);
                                deliveryInfoDataModel.DeliveryAddressModel.DemogInfoSubDivisionId = long.Parse(sqlQueryResult["DemogInfoSubDivisionId"]);
                                deliveryInfoDataModel.DeliveryAddressModel.DemogInfoZipId = long.Parse(sqlQueryResult["DemogInfoZipId"]);
                                deliveryInfoDataModel.DeliveryAddressModel.DemogInfoZipPlusId = long.Parse(sqlQueryResult["DemogInfoZipPlusId"]);
                                deliveryInfoDataModel.DeliveryAddressModel.StateAbbrev = sqlQueryResult["StateAbbrev"];
                            }
                        }
                    }
                }
                if (ModelState.IsValid)
                {
                    PaymentInfoModel paymentInfoModel = retailSlnBL.DeliveryInfo(deliveryInfoDataModel, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    if (ModelState.IsValid)
                    {
                        success = true;
                        processMessage = "SUCCESS!!!";
                        htmlString = archLibBL.ViewToHtmlString(this, "_PaymentInfo", paymentInfoModel);
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
                    }
                    else
                    {
                        success = false;
                        processMessage = "ERROR???";
                        htmlString = archLibBL.ViewToHtmlString(this, "_DeliveryInfoData", deliveryInfoDataModel);
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: Payment BL Error");
                    }
                }
                else
                {
                    success = false;
                    processMessage = "ERROR???";
                    htmlString = archLibBL.ViewToHtmlString(this, "_DeliveryInfoData", deliveryInfoDataModel);
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: Model Validation Error");
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                processMessage = "ERROR???";
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                deliveryInfoDataModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                };
                htmlString = archLibBL.ViewToHtmlString(this, "_DeliveryInfoData", deliveryInfoDataModel);
            }
            actionResult = Json(new { success, processMessage, htmlString });
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult GiftCert()
        {
            ViewData["ActionName"] = "GiftCert";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            try
            {
                GiftCertModel giftCertModel = retailSlnBL.GiftCert(Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("GiftCert", giftCertModel);
                Response.StatusCode = (int)HttpStatusCode.OK;
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                Response.StatusCode = (int)HttpStatusCode.OK;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                actionResult = Json(new { errorMessage = "Error ocurred while checking out" }, JsonRequestBehavior.AllowGet);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            actionResult = View();
            return actionResult;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult GiftCert(GiftCertModel giftCertModel)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            try
            {
                //int x = 1, y = 0, z = x / y;
                ModelState.Clear();
                TryValidateModel(giftCertModel);
                retailSlnBL.GiftCert(ref giftCertModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (ModelState.IsValid)
                {
                    actionResult = PartialView("_GiftCertReceipt", giftCertModel);
                }
                else
                {
                    actionResult = PartialView("_GiftCertData", giftCertModel);
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                archLibBL.GenerateCaptchaQuesion(Session, "CaptchaNumberLogin0", "CaptchaNumberLogin1");
                giftCertModel.CaptchaAnswer = null;
                giftCertModel.CaptchaNumber0 = Session["CaptchaNumberLogin0"].ToString();
                giftCertModel.CaptchaNumber1 = Session["CaptchaNumberLogin1"].ToString();
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                giftCertModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                };
                actionResult = PartialView("_GiftCertData", giftCertModel);
                //actionResult = Json(new { errorMessage = "Error ocurred while checking out" }, JsonRequestBehavior.AllowGet);
                //ModelState.AddModelError("", "Gift Cert / POST");
                //archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                //actionResult = PartialView("_Error");
                //Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            return actionResult;
        }

        [Authorize]
        [AjaxAuthorize]
        [HttpPost]
        public ActionResult GiftCertBalance(GiftCertModel giftCertModel)
        {
            ViewData["ActionName"] = "GiftCertBalance";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            try
            {
                retailSlnBL.GiftCertBalance(giftCertModel.GiftCertNumber.ToString(), giftCertModel.GiftCertKey, out string errorMessage, out float? giftCertBalAmount, clientId, ipAddress, execUniqueId, loggedInUserId);
                bool success;
                string giftCertBalanceAmount;
                if (errorMessage == "")
                {
                    success = true;
                    giftCertBalanceAmount = giftCertBalAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                }
                else
                {
                    success = false;
                    giftCertBalanceAmount = "";
                }
                actionResult = Json(new { success, errorMessage, giftCertBalanceAmount }, JsonRequestBehavior.AllowGet);
                //int x = 1, y = 0, z = x / y;
                //actionResult = Json(new { success = false, errorMessage = "Invalid Gift Cert Number/Key", giftCertBalanceAmount = "" }, JsonRequestBehavior.AllowGet);
                //actionResult = Json(new { success = true, errorMessage = "", giftCertBalanceAmount = "$180.27" }, JsonRequestBehavior.AllowGet);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                actionResult = Json(new { success = false, errorMessage = "System error occurred" }, JsonRequestBehavior.AllowGet);
            }
            return actionResult;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ItemBundleItemListView")]
        public ActionResult ItemBundleItemListView(string id)
        {
            ViewData["ActionName"] = "ItemBundleItemListView";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "0000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            try
            {
                long itemId = long.Parse(id);
                ItemBundleItemListModel itemBundleItemList = retailSlnBL.ItemBundleItemList(itemId, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("ItemBundleItemListView", itemBundleItemList);
                Response.StatusCode = (int)HttpStatusCode.OK;
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "Item Image List View / GET");
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            return actionResult;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ItemImageListView")]
        public ActionResult ItemImageListView(string id)
        {
            ViewData["ActionName"] = "ItemImageListView";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            try
            {
                //int x = 1, y = 0, z = x / y;
                long itemId = long.Parse(id);
                ItemImageListModel itemImageListModel = retailSlnBL.ItemImageList(itemId, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("ItemImageListView", itemImageListModel);
                Response.StatusCode = (int)HttpStatusCode.OK;
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "Item Image List View / GET");
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            return actionResult;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ItemListView()
        {
            ViewData["ActionName"] = "ItemListView";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            try
            {
                //int x = 1, y = 0, z = x / y;
                ItemListModel ItemListModel = retailSlnBL.ItemListView(Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("ItemListView", ItemListModel);
                Response.StatusCode = (int)HttpStatusCode.OK;
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "Item List View / GET");
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            return actionResult;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ItemSpecList")]
        public ActionResult ItemSpecList(string id)
        {
            ViewData["ActionName"] = "ItemSpecList";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            try
            {
                //int x = 1, y = 0, z = x / y;
                long itemId = long.Parse(id);
                ItemSpecListModel itemSpecListModel = retailSlnBL.ItemSpecList(itemId, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("ItemSpecList", itemSpecListModel);
                Response.StatusCode = (int)HttpStatusCode.OK;
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "Item Spec List / GET");
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            return actionResult;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("OrderCategoryItemAll")]
        public ActionResult OrderCategoryItemAll(string id)
        {
            ViewData["ActionName"] = "OrderCategoryItem";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            try
            {
                //int x = 1, y = 0, z = x / y;
                actionResult = View("OrderCategoryItemAll", long.Parse(id));
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "Order Category Item / GET");
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error");
            }
            return actionResult;
        }

        [HttpGet]
        public ActionResult OrderCategoryItem(string id, string pageNum)
        {
            //int x = 1, y = 0, z = x / y;
            ViewData["ActionName"] = "OrderCategoryItemData";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            try
            {
                //int x = 1, y = 0, z = x / y;
                SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
                string actionName, aspNetRoleName, controllerName;
                if (sessionObjectModel == null)
                {
                    aspNetRoleName = "DEFAULTROLE";
                }
                else
                {
                    aspNetRoleName = sessionObjectModel.AspNetRoleName;
                }
                var aspNetRoleKVPs = ArchLibCache.AspNetRoleKVPs[aspNetRoleName];
                actionName = aspNetRoleKVPs["ActionName02"].KVPValueData;
                controllerName = aspNetRoleKVPs["ControllerName02"].KVPValueData;
                var parentCategoryId = long.Parse(aspNetRoleKVPs["ParentCategoryId"].KVPValueData);
                OrderCategoryItemModel orderCategoryItemModel = new OrderCategoryItemModel
                {
                    ActionName = actionName,
                    ControllerName = controllerName,
                    ParentCategoryId = long.TryParse(id, out long tempParentCategoryId) ? tempParentCategoryId : parentCategoryId,
                    PageNum = int.TryParse(pageNum, out int tempPageNum) ? tempPageNum : 50,
                    PageSize = 50, //int.TryParse(pageSize, out tempLong) ? int.Parse(pageSize) : 50,
                };
                success = true;
                processMessage = "SUCCESS!!!";
                htmlString = archLibBL.ViewToHtmlString(this, "_OrderCategoryItem", orderCategoryItemModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                ModelState.AddModelError("", "OrderCategoryItemData / GET");
                archLibBL.CopyReponseObjectToModelErrors(ModelState, null, responseObjectModel.ResponseMessages);
                success = false;
                processMessage = "ERROR???";
                htmlString = archLibBL.ViewToHtmlString(this, "Error", responseObjectModel);
            }
            actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            return actionResult;
        }

        [AjaxAuthorize]
        [Authorize]
        [HttpGet]
        //[Route("OrderCategoryItemList")]
        public ActionResult OrderCategoryItemList(string id, string pageNum)
        {
            //int x = 1, y = 0, z = x / y;
            ViewData["ActionName"] = "OrderCategoryItemListData";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
            string actionName, aspNetRoleName, controllerName;
            if (sessionObjectModel == null)
            {
                aspNetRoleName = "DEFAULTROLE";
            }
            else
            {
                aspNetRoleName = sessionObjectModel.AspNetRoleName;
            }
            try
            {
                //int x = 1, y = 0, z = x / y;
                var aspNetRoleKVPs = ArchLibCache.AspNetRoleKVPs[aspNetRoleName];
                actionName = aspNetRoleKVPs["ActionName02"].KVPValueData;
                controllerName = aspNetRoleKVPs["ControllerName02"].KVPValueData;
                var parentCategoryId = long.Parse(aspNetRoleKVPs["ParentCategoryId"].KVPValueData);
                OrderCategoryItemModel orderCategoryItemModel = new OrderCategoryItemModel
                {
                    ActionName = actionName,
                    ControllerName = controllerName,
                    ParentCategoryId = long.TryParse(id, out long tempParentCategoryId) ? tempParentCategoryId : parentCategoryId,
                    PageNum = int.TryParse(pageNum, out int tempPageNum) ? tempPageNum : 50,
                    PageSize = 50, //int.TryParse(pageSize, out tempLong) ? int.Parse(pageSize) : 50,
                };
                success = true;
                processMessage = "SUCCESS!!!";
                htmlString = archLibBL.ViewToHtmlString(this, "_OrderCategoryItemList", orderCategoryItemModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                ModelState.AddModelError("", "OrderCategoryItemListData / GET");
                archLibBL.CopyReponseObjectToModelErrors(ModelState, null, responseObjectModel.ResponseMessages);
                success = false;
                processMessage = "ERROR???";
                htmlString = archLibBL.ViewToHtmlString(this, "Error", responseObjectModel);
            }
            actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            return actionResult;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("OrdersReturns")]
        public ActionResult OrdersReturns()
        {
            return View();
        }

        [AjaxAuthorize]
        [Authorize]
        [HttpPost]
        public ActionResult PaymentInfo(PaymentInfoModel paymentInfoModel)
        {
            //int x = 1, y = 0, z = x / y;
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            try
            {
                //int x = 1, y = 0, z = x / y;
                success = true;
                processMessage = "SUCCESS!!!";
                PaymentModel paymentModel = retailSlnBL.PaymentInfo(Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                htmlString = archLibBL.ViewToHtmlString(this, "_Payment", paymentModel);
                actionResult = Json(new { success, processMessage, htmlString });
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process Success");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                processMessage = "ERROR???";
                htmlString = archLibBL.ViewToHtmlString(this, "_PaymentInfo", paymentInfoModel);
                actionResult = Json(new { success, processMessage, htmlString });
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00099100 :: Error Exit");
            }
            return actionResult;
        }

        [AjaxAuthorize]
        [Authorize]
        [HttpPost]
        public ActionResult PaymentInfo1(GiftCertPaymentModel giftCertPaymentModel)
        {
            //int x = 1, y = 0, z = x / y;
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            PaymentInfoModel paymentInfoModel = new PaymentInfoModel
            {
                GiftCertPaymentModel = giftCertPaymentModel,
                DeliveryInfoDataModel = (DeliveryInfoDataModel)Session["DeliveryInfoDataModel"],
                ShoppingCartModel = (ShoppingCartModel)Session["ShoppingCartModel"],
            };
            try
            {
                //int x = 1, y = 0, z = x / y;
                ModelState.Clear();
                TryValidateModel(giftCertPaymentModel);
                if (!ModelState.IsValid)
                {
                    if (string.IsNullOrWhiteSpace(giftCertPaymentModel.GiftCertNumber) && string.IsNullOrWhiteSpace(giftCertPaymentModel.GiftCertKey))
                    {
                        ModelState.Remove("GiftCertNumber");
                        ModelState.Remove("GiftCertKey");
                    }
                }
                if (ModelState.IsValid)
                {
                    success = true;
                    processMessage = "SUCCESS!!!";
                    htmlString = retailSlnBL.PaymentInfo1(paymentInfoModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    FormsAuthentication.SignOut();
                    Session.Abandon();
                    Request.GetOwinContext().Authentication.SignOut();
                    Session["SessionObject"] = null;
                    Session.Abandon();
                    Session["ShoppingCartModel"] = null;
                    Session["DeliveryInfoModel"] = null;
                    Session["PaymentInfoModel"] = null;
                    Session["SessionObject"] = null;
                    actionResult = Json(new { success, processMessage, htmlString });
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
                }
                else
                {
                    success = false;
                    processMessage = "ERROR???";
                    giftCertPaymentModel.ResponseObjectModel = new ResponseObjectModel
                    {
                        ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                    };
                    htmlString = archLibBL.ViewToHtmlString(this, "_GiftCertPaymentData", giftCertPaymentModel);
                    actionResult = Json(new { success, processMessage, htmlString });
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00003000 :: Validation Failure");
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                processMessage = "ERROR???";
                giftCertPaymentModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                };
                ModelState.AddModelError("", "Error occurred while processing");
                ModelState.AddModelError("", "Please try again, if not contact support personnel");
                htmlString = archLibBL.ViewToHtmlString(this, "_GiftCertPaymentData", giftCertPaymentModel);
                actionResult = Json(new { success, processMessage, htmlString });
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00099100 :: Error Exit");
            }
            return actionResult;
        }

        [AjaxAuthorize]
        [Authorize]
        [HttpPost]
        public ActionResult PaymentInfo2(GiftCertPaymentModel giftCertPaymentModel)
        {//Razorpay
            //int x = 1, y = 0, z = x / y;
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            PaymentInfoModel paymentInfoModel = new PaymentInfoModel
            {
                GiftCertPaymentModel = giftCertPaymentModel,
                DeliveryInfoDataModel = (DeliveryInfoDataModel)Session["DeliveryInfoDataModel"],
                ShoppingCartModel = (ShoppingCartModel)Session["ShoppingCartModel"],
            };
            try
            {
                //int x = 1, y = 0, z = x / y;
                ModelState.Clear();
                TryValidateModel(giftCertPaymentModel);
                if (!ModelState.IsValid)
                {
                    if (string.IsNullOrWhiteSpace(giftCertPaymentModel.GiftCertNumber) && string.IsNullOrWhiteSpace(giftCertPaymentModel.GiftCertKey))
                    {
                        ModelState.Remove("GiftCertNumber");
                        ModelState.Remove("GiftCertKey");
                    }
                }
                if (ModelState.IsValid)
                {
                    RazorPayResponse creditCardResponseObject = retailSlnBL.PaymentInfo2(paymentInfoModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    success = true;
                    processMessage = "SUCCESS!!!";
                    htmlString = JsonConvert.SerializeObject(creditCardResponseObject);
                    actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
                }
                else
                {
                    success = false;
                    processMessage = "ERROR???";
                    giftCertPaymentModel.ResponseObjectModel = new ResponseObjectModel
                    {
                        ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                    };
                    htmlString = archLibBL.ViewToHtmlString(this, "_GiftCertPaymentData", giftCertPaymentModel);
                    actionResult = Json(new { success, processMessage, htmlString });
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00003000 :: Validation Failure");
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                processMessage = "ERROR???";
                giftCertPaymentModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                };
                ModelState.AddModelError("", "Error occurred while processing");
                ModelState.AddModelError("", "Please try again, if not contact support personnel");
                htmlString = archLibBL.ViewToHtmlString(this, "_GiftCertPaymentData", giftCertPaymentModel);
                actionResult = Json(new { success, processMessage, htmlString });
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00099100 :: Error Exit");
            }
            return actionResult;
        }

        [HttpPost]
        [Authorize]
        public ActionResult PaymentInfo3(string razorpay_payment_id, string razorpay_order_id, string razorpay_signature)
        {//RazorpayReturn
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            var htmlString = retailSlnBL.PaymentInfo3(razorpay_payment_id, razorpay_order_id, razorpay_signature, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
            FormsAuthentication.SignOut();
            Session.Abandon();
            Request.GetOwinContext().Authentication.SignOut();
            Session["SessionObject"] = null;
            Session.Abandon();
            Session["ShoppingCartModel"] = null;
            Session["DeliveryInfoModel"] = null;
            Session["PaymentInfoModel"] = null;
            Session["SessionObject"] = null;
            //string loggedInUserFullName, loggedInUserEmailAddress;
            return View("RazorPayReturn", null, htmlString);
        }

        [AjaxAuthorize]
        [Authorize]
        [HttpPost]
        public ActionResult PaymentInfo4(PaymentInfoModel paymentInfoModel)
        {//Credit Card Synchronous
            return null;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult RemoveFromCart(RemoveFromCartModel removeFromCartModel)
        {
            ViewData["ActionName"] = "RemoveFromCart";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            int shoppingCartItemsCount = 0;
            string shoppingCartTotalAmount = 0f.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
            try
            {
                //int x = 1, y = 0, z = x / y;
                ShoppingCartModel shoppingCartModel = retailSlnBL.RemoveFromCart(removeFromCartModel.RemoveFromCartIndex.Value, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                success = true;
                processMessage = "SUCCESS!!!";
                string aspNetRoleName, actionName, controllerName, viewName;
                SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
                if (sessionObjectModel == null)
                {
                    aspNetRoleName = "DEFAULTROLE";
                }
                else
                {
                    aspNetRoleName = sessionObjectModel.AspNetRoleName;
                }
                var aspNetRoleKVPs = ArchLibCache.AspNetRoleKVPs[aspNetRoleName];
                actionName = aspNetRoleKVPs["ActionName02"].KVPValueData;
                controllerName = aspNetRoleKVPs["ControllerName02"].KVPValueData;
                viewName = aspNetRoleKVPs["ViewName02"].KVPValueData;
                OrderCategoryItemModel orderCategoryItemModel = new OrderCategoryItemModel
                {
                    ActionName = actionName,
                    ControllerName = controllerName,
                    ParentCategoryId = removeFromCartModel.ParentCategoryId.Value,
                    PageNum = removeFromCartModel.PageNum.Value,
                    PageSize = removeFromCartModel.PageSize.Value,
                    TotalRowCount = removeFromCartModel.TotalRowCount.Value,
                };
                htmlString = archLibBL.ViewToHtmlString(this, viewName, orderCategoryItemModel);
                shoppingCartItemsCount = shoppingCartModel.ShoppingCartItems.Count;
                shoppingCartTotalAmount = shoppingCartModel.ShoppingCartTotalAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                actionResult = Json(new { success, processMessage, htmlString, shoppingCartItemsCount = shoppingCartModel.ShoppingCartItems.Count, shoppingCartTotalAmount = shoppingCartModel.ShoppingCartTotalAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "") }, JsonRequestBehavior.AllowGet);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                processMessage = "ERROR???";
                htmlString = "Error while adding item to cart";
            }
            actionResult = Json(new { success, processMessage, htmlString, shoppingCartItemsCount, shoppingCartTotalAmount }, JsonRequestBehavior.AllowGet);
            return actionResult;
        }

        [AllowAnonymous]
        [HttpGet]
        //[Route("SearchResult")]
        public ActionResult SearchResult(string id)
        {
            return View("SearchResult", (object)id);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ShoppingCartComments(int index, string orderComments)
        {
            ViewData["ActionName"] = "ShoppingCartComments";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            //ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            try
            {
                //int x = 1, y = 0, z = x / y;
                retailSlnBL.ShoppingCartComments(index, orderComments, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = Json(new { success = true }, JsonRequestBehavior.AllowGet);
                Response.StatusCode = (int)HttpStatusCode.OK;
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                actionResult = Json(new { errorMessage = "Error in shopping cart comments" }, JsonRequestBehavior.AllowGet);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            return actionResult;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ShoppingCartSummary()
        {
            ViewData["ActionName"] = "ShoppingCart";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            int shoppingCartItemsCount;
            string shoppingCartTotalAmount;
            ShoppingCartModel shoppingCartModel = ((ShoppingCartModel)Session["ShoppingCartModel"]) ?? new ShoppingCartModel { ShoppingCartItems = new List<ShoppingCartItemModel>(), ShoppingCartTotalAmount = 0, };
            success = true;
            processMessage = "SUCCESS!!!";
            htmlString = "";
            shoppingCartItemsCount = shoppingCartModel.ShoppingCartItems.Count;
            shoppingCartTotalAmount = shoppingCartModel.ShoppingCartTotalAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
            actionResult = Json(new { success, processMessage, htmlString, shoppingCartItemsCount, shoppingCartTotalAmount }, JsonRequestBehavior.AllowGet);
            return actionResult;
        }
    }
}
