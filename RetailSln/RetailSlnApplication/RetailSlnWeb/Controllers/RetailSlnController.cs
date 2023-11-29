using ArchitectureLibraryBusinessLayer;
using ArchitectureLibraryCacheData;
using ArchitectureLibraryEnumerations;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
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
            try
            {
                //int x = 1, y = 0, z = x / y;
                ShoppingCartModel shoppingCartModel = retailSlnBL.AddToCart(long.Parse(itemId), long.Parse(orderQty), Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                long.TryParse(id, out long tempId);
                success = true;
                processMessage = "SUCCESS!!!";
                htmlString = archLibBL.ViewToHtmlString(this, "_OrderCategoryItem", tempId);
                actionResult = Json(new { success, processMessage, htmlString, shoppingCartItemsCount = shoppingCartModel.ShoppingCartItems.Count, shoppingCartTotalAmount = shoppingCartModel.ShoppingCartTotalAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "") }, JsonRequestBehavior.AllowGet);
                //actionResult = Json(new { shoppingCartItemsCount = shoppingCartModel.ShoppingCartItems.Count, shoppingCartTotalAmount = shoppingCartModel.ShoppingCartTotalAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "") }, JsonRequestBehavior.AllowGet);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                //exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                //actionResult = Json(new { errorMessage = "Error while adding item to cart" }, JsonRequestBehavior.AllowGet);
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                processMessage = "ERROR???";
                htmlString = "Error while adding item to cart";
                actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            }
            return actionResult;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult AddToCart([System.Web.Http.FromUri] string id, [System.Web.Http.FromBody] List<ShoppingCartItemModel> shoppingCartItemModels)
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
            try
            {
                //int x = 1, y = 0, z = x / y;
                success = false;
                if (shoppingCartItemModels != null)
                {
                    foreach (var shoppingCartItemModel in shoppingCartItemModels)
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
                    actionResult = Json(new { success, processMessage, htmlString });
                }
                else
                {
                    ShoppingCartModel shoppingCartModel = retailSlnBL.AddToCart(shoppingCartItemModels, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    success = true;
                    processMessage = "SUCCESS!!!";
                    if (long.TryParse(id, out long tempId))
                    {
                        htmlString = archLibBL.ViewToHtmlString(this, "_OrderCategoryItem", tempId);
                    }
                    else
                    {
                        htmlString = archLibBL.ViewToHtmlString(this, "_OrderListView", null);
                    }
                    actionResult = Json(new { success, processMessage, htmlString, shoppingCartItemsCount = shoppingCartModel.ShoppingCartItems.Count, shoppingCartTotalAmount = shoppingCartModel.ShoppingCartTotalAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "") }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                processMessage = "ERROR???";
                htmlString = "Error while adding item to cart";
                actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            }
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
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                ModelState.AddModelError("", "Checkout / GET");
                actionResult = View("Error", responseObjectModel);
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
            try
            {
                //int x = 1, y = 0, z = x / y;
                retailSlnBL.CheckoutValidate(Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
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
                    htmlString = "Error while validating checkout";
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process Error");
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                processMessage = "ERROR???";
                htmlString = "Error while validating checkout";
            }
            actionResult = Json(new { success, processMessage, htmlString });
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
                    ModelState["AlternateTelephoneNum"].Errors.Clear();
                    ModelState["PrimaryTelephoneNum"].Errors.Clear();
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
                        if(!regex.IsMatch(deliveryInfoDataModel.DeliveryAddressModel.ZipCode))
                        {
                            ModelState.AddModelError("DeliveryAddressModel.ZipCode", "Postal Code");
                        }
                    }
                }
                if (ModelState.IsValid)
                {
                    retailSlnBL.DeliveryInfo(deliveryInfoDataModel, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    if (ModelState.IsValid)
                    {
                        PaymentModel paymentModel = retailSlnBL.Payment(Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                        if (ModelState.IsValid)
                        {
                            success = true;
                            processMessage = "SUCCESS!!!";
                            htmlString = archLibBL.ViewToHtmlString(this, "_Payment", paymentModel);
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
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: DeliveryInfo BL Error");
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
                //actionResult = Json(new { errorMessage = "Error ocurred while checking out" }, JsonRequestBehavior.AllowGet);
                ModelState.AddModelError("", "Gift Cert / POST");
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = PartialView("_Error");
                //Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            return actionResult;
        }

        [Authorize]
        [AjaxAuthorize]
        [HttpGet]
        public ActionResult GiftCertBalance(string giftCertNumber, string giftCertKey)
        {
            ViewData["ActionName"] = "GiftCertBalance";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            try
            {
                retailSlnBL.GiftCertBalance(giftCertNumber, giftCertKey, out string errorMessage, out float? giftCertBalAmount, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        [Route("OrderCategoryItem")]
        public ActionResult OrderCategoryItem(string id)
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
                actionResult = View("OrderCategoryItem", long.Parse(id));
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

        [AllowAnonymous]
        [HttpGet]
        [Route("OrderListView")]
        public ActionResult OrderListView()
        {
            return View();
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
        public ActionResult Payment(PaymentDataModel paymentDataModel)
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
                //paymentDataModel.ResponseObjectModel = new ResponseObjectModel();
                ModelState.Clear();
                bool giftCertPresent, creditCardPresent;
                var giftCertValidateModel = new GiftCertValidateModel
                {
                    GiftCertKey = paymentDataModel.GiftCertKey,
                    GiftCertNumber = paymentDataModel.GiftCertNumber,
                };
                var giftCertModelIsValid = TryValidateModel(giftCertValidateModel);
                if (string.IsNullOrWhiteSpace(giftCertValidateModel.GiftCertNumber) && string.IsNullOrWhiteSpace(giftCertValidateModel.GiftCertKey))
                {
                    ModelState.Remove("GiftCertNumber");
                    ModelState.Remove("GiftCertKey");
                    giftCertPresent = false;
                }
                else
                {
                    giftCertPresent = true;
                }
                var creditCardValidateModel = new CreditCardValidateModel
                {
                    CardExpiryMM = paymentDataModel.CardExpiryMM,
                    CardExpiryYYYY = paymentDataModel.CardExpiryYYYY,
                    CardHolderName = paymentDataModel.CardHolderName,
                    CreditCardNumber = paymentDataModel.CreditCardNumber,
                    CVV = paymentDataModel.CVV,
                };
                var creditCardModelIsValid = TryValidateModel(creditCardValidateModel);
                if (
                    string.IsNullOrWhiteSpace(creditCardValidateModel.CardExpiryMM) &&
                    string.IsNullOrWhiteSpace(creditCardValidateModel.CardExpiryYYYY) &&
                    string.IsNullOrWhiteSpace(creditCardValidateModel.CardHolderName) &&
                    string.IsNullOrWhiteSpace(creditCardValidateModel.CreditCardNumber) &&
                    string.IsNullOrWhiteSpace(creditCardValidateModel.CVV)
                   )
                {
                    ModelState.Remove("CardExpiryMM");
                    ModelState.Remove("CardExpiryYYYY");
                    ModelState.Remove("CardHolderName");
                    ModelState.Remove("CreditCardNumber");
                    ModelState.Remove("CVV");
                    creditCardPresent = false;
                }
                else
                {
                    creditCardPresent = true;
                }
                if (!giftCertPresent && !creditCardPresent)
                {
                    ModelState.AddModelError("GiftCertNumber", "Enter Gift Cert#");
                    ModelState.AddModelError("GiftCertKey", "Enter Gift Cert Key");
                    ModelState.AddModelError("CreditCardNumber", "Enter Credit Card#");
                    ModelState.AddModelError("CardHolderName", "Enter Card Holder Name");
                    ModelState.AddModelError("CVV", "CVV");
                    ModelState.AddModelError("CardExpiryMM", "***");
                    ModelState.AddModelError("CardExpiryYYYY", "***");
                }
                else
                {

                }
                if (ModelState.IsValid)
                {
                    retailSlnBL.Payment(ref paymentDataModel, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    if (ModelState.IsValid)
                    {
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process Success");
                        OrderReceiptModel orderReceiptModel = retailSlnBL.OrderReceipt(paymentDataModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                        Session["ShoppingCartModel"] = null;
                        Session["DeliveryInfoModel"] = null;
                        success = true;
                        processMessage = "SUCCESS";
                        htmlString = archLibBL.ViewToHtmlString(this, "_OrderReceipt", orderReceiptModel);
                        var sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
                        string loggedInUserFullName, loggedInUserEmailAddress;
                        if (sessionObjectModel.AspNetRoleName == "GUESTROLE")
                        {
                            FormsAuthentication.SignOut();
                            Session.Abandon();
                            Request.GetOwinContext().Authentication.SignOut();
                            Session["SessionObject"] = null;
                            Session.Abandon();
                            loggedInUserFullName = "";
                            loggedInUserEmailAddress = "";
                        }
                        else
                        {
                            loggedInUserFullName = sessionObjectModel.FirstName + " " + sessionObjectModel.LastName;
                            loggedInUserEmailAddress = sessionObjectModel.EmailAddress;
                        }
                        actionResult = Json(new { success, processMessage, htmlString, loggedInUserFullName, loggedInUserEmailAddress });
                    }
                    else
                    {
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00003000 :: BL Process Error");
                        success = false;
                        processMessage = "ERROR???";
                        paymentDataModel.ResponseObjectModel = new ResponseObjectModel
                        {
                            ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                        };
                        htmlString = archLibBL.ViewToHtmlString(this, "_PaymentData", paymentDataModel);
                        actionResult = Json(new { success, processMessage, htmlString });
                    }
                }
                else
                {
                    success = false;
                    processMessage = "ERROR???";
                    paymentDataModel.ResponseObjectModel = new ResponseObjectModel
                    {
                        ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                    };
                    htmlString = archLibBL.ViewToHtmlString(this, "_PaymentData", paymentDataModel);
                    actionResult = Json(new { success, processMessage, htmlString });
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process Error");
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                success = false;
                processMessage = "ERROR???";
                paymentDataModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                };
                htmlString = archLibBL.ViewToHtmlString(this, "_PaymentData", paymentDataModel);
                actionResult = Json(new { success, processMessage, htmlString });
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00099100 :: Error Exit");
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult RemoveFromCart(string id, string index)
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
            try
            {
                //int x = 1, y = 0, z = x / y;
                ShoppingCartModel shoppingCartModel = retailSlnBL.RemoveFromCart(int.Parse(index), Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                success = true;
                processMessage = "SUCCESS!!!";
                if (long.TryParse(id, out long tempId))
                {
                    htmlString = archLibBL.ViewToHtmlString(this, "_OrderCategoryItem", tempId);
                }
                else
                {
                    htmlString = archLibBL.ViewToHtmlString(this, "_OrderListView", null);
                }
                actionResult = Json(new { success, processMessage, htmlString, shoppingCartItemsCount = shoppingCartModel.ShoppingCartItems.Count, shoppingCartTotalAmount = shoppingCartModel.ShoppingCartTotalAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "") }, JsonRequestBehavior.AllowGet);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                processMessage = "ERROR???";
                htmlString = "Error while adding item to cart";
                actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            }
            return actionResult;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult RemoveFromCart2(string id)
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
            try
            {
                //int x = 1, y = 0, z = x / y;
                ShoppingCartModel shoppingCartModel = retailSlnBL.RemoveFromCart(int.Parse(id), Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                success = true;
                processMessage = "SUCCESS!!!";
                htmlString = archLibBL.ViewToHtmlString(this, "_ShoppingCart", shoppingCartModel);
                actionResult = Json(new { success, processMessage, htmlString, shoppingCartItemsCount = shoppingCartModel.ShoppingCartItems.Count, shoppingCartTotalAmount = shoppingCartModel.ShoppingCartTotalAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "") }, JsonRequestBehavior.AllowGet);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                processMessage = "ERROR???";
                htmlString = "Error while adding item to cart";
                actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            }
            return actionResult;
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
            //ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            try
            {
                //int x = 1, y = 0, z = x / y;
                ShoppingCartModel shoppingCartModel;
                shoppingCartModel = (ShoppingCartModel)Session["ShoppingCartModel"];
                if (shoppingCartModel == null)
                {
                    actionResult = Json(new { shoppingCartItemsCount = "", shoppingCartTotalAmount = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    actionResult = Json(new { shoppingCartItemsCount = shoppingCartModel.ShoppingCartItems.Count, shoppingCartTotalAmount = shoppingCartModel.ShoppingCartTotalAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "") }, JsonRequestBehavior.AllowGet);
                }
                Response.StatusCode = (int)HttpStatusCode.OK;
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                actionResult = Json(new { errorMessage = "Error while getting shopping cart" }, JsonRequestBehavior.AllowGet);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            return actionResult;
        }
    }
}
