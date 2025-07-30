using ArchitectureLibraryBusinessLayer;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using RetailSlnCacheData;
using RetailSlnBusinessLayer;
using RetailSlnModels;
using RetailSlnWeb.ClassCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Runtime.InteropServices;
using ArchitectureLibraryCacheData;
using System.Configuration;
using System.Security.Claims;
using ArchitectureLibraryEnumerations;
using RetailSlnEnumerations;
using ArchitectureLibraryCreditCardModels;
using Newtonsoft.Json;
using Stripe;
using System.Web.Security;
using System.Threading.Tasks;

namespace RetailSlnWeb.Controllers
{
    public partial class HomeController : Controller
    {
        // GET: AddToCart
        [AllowAnonymous]
        [HttpPost]
        public ActionResult AddToCart(AddToCartModel addToCartModel)
        {
            //int x = 1, y = 0, z = x / y;
            ViewData["ActionName"] = "AddToCart1";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            int shoppingCartItemsCount = 0;
            string shoppingCartTotalAmount = "";
            try
            {
                PaymentInfoModel paymentInfoModel = (PaymentInfoModel)Session["PaymentInfo"];
                SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
                SessionObjectModel createForSessionObject = (SessionObjectModel)Session["CreateForSessionObject"];
                htmlString = retailSlnBL.AddToCart(ref paymentInfoModel, addToCartModel, true, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                Session["PaymentInfo"] = paymentInfoModel;
                if (htmlString == "")
                {
                    shoppingCartItemsCount = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels.Count;
                    shoppingCartTotalAmount = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountFormatted;
                    success = true;
                    processMessage = "SUCCESS!!!";
                    htmlString = archLibBL.ViewToHtmlString(this, "_ShoppingCartContainer", paymentInfoModel);
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Success");
                }
                else
                {
                    success = false;
                    processMessage = "ERROR???";
                    htmlString = "Error while adding item to cart";
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Error");
                }
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

        // GET: Checkout
        [AllowAnonymous]
        [HttpGet]
        [Route("Checkout")]
        public ActionResult Checkout()
        {
            ViewData["ActionName"] = "Checkout";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            try
            {
                //int x = 1, y = 0, z = x / y;
                PaymentInfoModel paymentInfoModel = (PaymentInfoModel)Session["PaymentInfo"];
                CheckoutModel checkoutModel = retailSlnBL.Checkout(paymentInfoModel, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (checkoutModel.PaymentInfoModel == null)
                {
                    actionResult = RedirectToAction("Index");
                }
                else
                {
                    var ctx = Request.GetOwinContext();
                    var authManager = ctx.Authentication;
                    bool loggedIn = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
                    SessionObjectModel sessionObject = (SessionObjectModel)Session["SessionObject"];
                    SessionObjectModel createForSessionObject = (SessionObjectModel)Session["CreateForSessionObject"];
                    if (loggedIn && sessionObject != null & createForSessionObject != null)
                    {
                        retailSlnBL.Checkout(ref paymentInfoModel, sessionObject, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                        actionResult = View("DeliveryInfo", paymentInfoModel);
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
                actionResult = RedirectToAction("Index");
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        // POST: Checkout
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Checkout(LoginUserProfModel loginUserProfModel)
        {
            //int x = 1, y = 0, z = x / y;
            ViewData["ActionName"] = "Checkout";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
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
                SessionObjectModel sessionObject = archLibBL.LoginUserProf(ref loginUserProfModel, true, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (ModelState.IsValid)
                {
                    ApplSessionObjectModel applSessionObjectModel = retailSlnBL.LoginUserProf(sessionObject.PersonId, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    sessionObject.ApplSessionObjectModel = applSessionObjectModel;
                    SessionObjectModel createForSessionObject = archLibBL.CopySessionObject(sessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    applSessionObjectModel = retailSlnBL.LoginUserProf(createForSessionObject.PersonId, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    createForSessionObject.ApplSessionObjectModel = applSessionObjectModel;
                    Session["SessionObject"] = sessionObject;
                    Session["CreateForSessionObject"] = createForSessionObject;
                    Session.Timeout = int.Parse(ConfigurationManager.AppSettings["AccessTokenExpiryMinutes"]);
                    var identity = new ClaimsIdentity
                    (
                        new[]
                        {
                            new Claim(ClaimTypes.Name, sessionObject.FirstName + " " + sessionObject.LastName),
                            new Claim(ClaimTypes.Email, sessionObject.EmailAddress),
                            //new Claim(ClaimTypes.Country, "India"),
                        },
                        "ApplicationCookie"
                    );
                    var ctx = Request.GetOwinContext();
                    var authManager = ctx.Authentication;
                    authManager.SignIn(identity);
                    PaymentInfoModel paymentInfoModel = (PaymentInfoModel)Session["PaymentInfo"];
                    //retailSlnBL.CreateOrderWIP(ref paymentInfoModel, sessionObject, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    retailSlnBL.Checkout(ref paymentInfoModel, sessionObject, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    success = true;
                    processMessage = "SUCCESS!!!";
                    htmlString = Url.Action("Checkout", "Home");// archLibBL.ViewToHtmlString(this, "_DeliveryInfo", paymentInfoModel);
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

        // POST: DeliveryInfo
        [Authorize]
        [AjaxAuthorize]
        [HttpPost]
        public ActionResult DeliveryInfo(PaymentInfoModel paymentInfoModel)
        {
            //int x = 1, y = 0, z = x / y;
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            bool success;
            string processMessage, htmlString;
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
            SessionObjectModel createForSessionObject = (SessionObjectModel)Session["CreateForSessionObject"];
            PaymentInfoModel paymentInfoModelTemp = ((PaymentInfoModel)Session["PaymentInfo"]);
            if (sessionObjectModel == null || createForSessionObject == null || paymentInfoModelTemp == null)
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Invalid session");
                success = false;
                processMessage = "ERROR???";
                htmlString = string.Empty;
                string redirectUrl = Url.Action("Index", "Home"), errorMessage = "User not logged in";
                actionResult = Json(new { success, processMessage, htmlString, redirectUrl, errorMessage });
            }
            else
            {
                try
                {
                    ModelState.Clear();
                    retailSlnBL.BuildPaymentInfoFromDeliveryInfoPost(ref paymentInfoModel, paymentInfoModelTemp, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    TryValidateModel(paymentInfoModel);
                    TryValidateModel(paymentInfoModel.DeliveryAddressModel, "DeliveryAddressModel");
                    TryValidateModel(paymentInfoModel.DeliveryDataModel, "DeliveryDataModel");
                    TryValidateModel(paymentInfoModel.DeliveryMethodModel, "DeliveryMethodModel");
                    TryValidateModel(paymentInfoModel.OrderSummaryModel, "OrderSummaryModel");
                    TryValidateModel(paymentInfoModel.PaymentModeModel, "PaymentModeModel");
                    Session["PaymentInfo"] = paymentInfoModel;
                    if (ModelState.IsValid)
                    {
                        retailSlnBL.DeliveryInfo(ref paymentInfoModel, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                        Session["PaymentInfo"] = paymentInfoModel;
                        if (ModelState.IsValid)
                        {
                            success = true;
                            processMessage = "SUCCESS!!!";
                            htmlString = archLibBL.ViewToHtmlString(this, "_PaymentInfo0", paymentInfoModel);
                        }
                        else
                        {
                            success = false;
                            processMessage = "ERROR???";
                            htmlString = archLibBL.ViewToHtmlString(this, "_DeliveryInfoData", paymentInfoModel);
                        }
                    }
                    else
                    {
                        retailSlnBL.BuildDeliveryInfoLookupData(ref paymentInfoModel, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                        success = false;
                        processMessage = "ERROR???";
                        htmlString = archLibBL.ViewToHtmlString(this, "_DeliveryInfoData", paymentInfoModel);
                    }
                }
                catch (Exception exception)
                {
                    exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                    archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    retailSlnBL.BuildDeliveryInfoLookupData(ref paymentInfoModel, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    paymentInfoModel.ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Error,
                        ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                    };
                    success = false;
                    processMessage = "ERROR???";
                    htmlString = archLibBL.ViewToHtmlString(this, "_DeliveryInfoData", paymentInfoModel);
                }
                actionResult = Json(new { success, processMessage, htmlString });
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        // GET: CheckoutGuest
        [AllowAnonymous]
        [HttpGet]
        [Route("CheckoutGuest")]
        public ActionResult CheckoutGuest()
        {
            //int x = 1, y = 0, z = x / y;
            ViewData["ActionName"] = "CheckoutGuest";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ArchLibBL archLibBL = new ArchLibBL();
            LoginUserProfGuestModel loginUserProfGuestModel;
            try
            {
                loginUserProfGuestModel = archLibBL.LoginUserProfGuest(RetailSlnCache.DefaultDeliveryDemogInfoCountryId, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "Error while loading Checkout Guest");
                loginUserProfGuestModel = new LoginUserProfGuestModel
                {
                    CaptchaAnswerLoginUserProfGuest = null,
                    CaptchaNumberLoginUserProfGuest0 = Session["CaptchaNumberLoginUserProfGuest0"].ToString(),
                    CaptchaNumberLoginUserProfGuest1 = Session["CaptchaNumberLoginUserProfGuest1"].ToString(),
                    ResponseObjectModel = new ResponseObjectModel
                    {

                    },
                };
            }
            return View(loginUserProfGuestModel);
        }

        // POST: CheckoutGuest
        [AllowAnonymous]
        [HttpPost]
        public ActionResult CheckoutGuest(LoginUserProfGuestModel loginUserProfGuestModel)
        {
            //int x = 1, y = 0, z = x / y;
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ArchLibBL archLibBL = new ArchLibBL();
            bool success;
            string processMessage, htmlString, redirectUrl;
            ActionResult actionResult;
            try
            {
                ModelState.Clear();
                TryValidateModel(loginUserProfGuestModel);
                SessionObjectModel sessionObject = archLibBL.LoginUserProfGuest(ref loginUserProfGuestModel, RetailSlnCache.DefaultDeliveryDemogInfoCountryId, true, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (ModelState.IsValid)
                {
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
                    if (!loginUserProfGuestModel.ExistingUser)
                    {
                        retailSlnBL.RegisterUserProfPersonExtn1(sessionObject.PersonId, 0, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    }
                    ApplSessionObjectModel applSessionObjectModel = retailSlnBL.LoginUserProf(sessionObject.PersonId, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    sessionObject.ApplSessionObjectModel = applSessionObjectModel;
                    SessionObjectModel createForSessionObject = archLibBL.CopySessionObject(sessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    applSessionObjectModel = retailSlnBL.LoginUserProf(createForSessionObject.PersonId, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    createForSessionObject.ApplSessionObjectModel = applSessionObjectModel;
                    Session["SessionObject"] = sessionObject;
                    Session["CreateForSessionObject"] = createForSessionObject;
                    Session.Timeout = int.Parse(ConfigurationManager.AppSettings["AccessTokenExpiryMinutes"]);
                    var identity = new ClaimsIdentity
                    (
                        new[]
                        {
                            new Claim(ClaimTypes.Name, sessionObject.FirstName + " " + sessionObject.LastName),
                            new Claim(ClaimTypes.Email, sessionObject.EmailAddress),
                            //new Claim(ClaimTypes.Country, "India"),
                        },
                        "ApplicationCookie"
                    );
                    var ctx = Request.GetOwinContext();
                    var authManager = ctx.Authentication;
                    authManager.SignIn(identity);
                    success = true;
                    processMessage = "SUCCESS!!!";
                    htmlString = "";
                    redirectUrl = Url.Action("Checkout", "Home");
                }
                else
                {
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process Error");
                    success = false;
                    processMessage = "ERROR???";
                    redirectUrl = "";
                    htmlString = archLibBL.ViewToHtmlString(this, "_CheckoutGuestData", loginUserProfGuestModel);
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "Error while loading Checkout Guest");
                success = false;
                processMessage = "ERROR???";
                redirectUrl = "";
                htmlString = archLibBL.ViewToHtmlString(this, "_CheckoutGuestData", loginUserProfGuestModel);
            }
            actionResult = Json(new { success, processMessage, htmlString, redirectUrl });
            return actionResult;
        }

        // GET : DownloadFile
        [Authorize]
        [AjaxAuthorize]
        [HttpGet]
        public FileResult DownloadFile(string fileName)
        {
            var downloadFullFileName = Server.MapPath("~/Invoices/") + @"\" + fileName;
            return File(downloadFullFileName, "application/force-download", fileName);
        }

        // GET : ItemBundleItemData
        [AllowAnonymous]
        [HttpGet]
        public ActionResult ItemBundleData(string id)
        {
            ViewData["ActionName"] = "ItemBundleData";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            try
            {
                SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
                SessionObjectModel createForSessionObject = (SessionObjectModel)Session["CreateForSessionObject"];
                PaymentInfoModel paymentInfoModel = (PaymentInfoModel)Session["PaymentInfo"];
                ItemBundleDataModel itemBundleDataModel = retailSlnBL.ItemBundleData(long.Parse(id), paymentInfoModel, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (itemBundleDataModel.ResponseObjectModel.ResponseTypeId == ResponseTypeEnum.Success)
                {
                    success = true;
                    processMessage = "SUCCESS!!!";
                    htmlString = archLibBL.ViewToHtmlString(this, "_ItemBundleData", itemBundleDataModel);
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
                }
                else
                {
                    success = false;
                    processMessage = "ERROR???";
                    htmlString = archLibBL.ViewToHtmlString(this, "_Error", itemBundleDataModel);
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process Error");
                }
                actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "Item Bundle Item Data / GET");
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error");
            }
            return actionResult;
        }

        // GET : ItemCatalog
        [AllowAnonymous]
        [HttpGet]
        [Route("ItemCatalog")]
        public ActionResult ItemCatalog(string id)
        {
            ViewData["ActionName"] = "ItemCatalog";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            try
            {
                SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
                SessionObjectModel createForSessionObject = (SessionObjectModel)Session["CreateForSessionObject"];
                OrderItemDataModel itemCatalogModel = retailSlnBL.ItemCatalog(id, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("ItemCatalog", itemCatalogModel);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "ItemCatalog / GET");
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error");
            }
            return actionResult;
        }

        // GET : ItemMasterAttributes
        [AllowAnonymous]
        [HttpGet]
        [Route("ItemMasterAttributes")]
        public ActionResult ItemMasterAttributes(string id, string tabId)
        {
            ViewData["ActionName"] = "itemAttribsView";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            try
            {
                //int x = 1, y = 0, z = x / y;
                SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
                SessionObjectModel createForSessionObject = (SessionObjectModel)Session["CreateForSessionObject"];
                PaymentInfoModel paymentInfoModel = (PaymentInfoModel)Session["PaymentInfo"];
                long itemMasterId = long.Parse(id);
                ItemMasterAttributesModel itemMasterAttributesModel = retailSlnBL.ItemMasterAttributes(itemMasterId, paymentInfoModel, long.Parse(tabId), sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("ItemMasterAttributes", itemMasterAttributesModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "Item Master Attributes View / GET");
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error");
            }
            return actionResult;
        }

        // GET: OrderInvoice
        [AjaxAuthorize]
        [Authorize]
        [HttpGet]
        [Route("OrderInvoice")]
        public ActionResult OrderInvoice()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
            SessionObjectModel createForSessionObject = (SessionObjectModel)Session["CreateForSessionObject"];
            try
            {
                PaymentInfoModel paymentInfoModel = (PaymentInfoModel)Session["PaymentInfo"];
                retailSlnBL.CreateInvoice(ref paymentInfoModel, this, sessionObjectModel, createForSessionObject, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                Session["PaymentInfo"] = paymentInfoModel;
                actionResult = View("OrderInvoice", paymentInfoModel);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                //string aspNetRoleName;
                //try
                //{
                //    aspNetRoleName = sessionObjectModel.AspNetRoleName;
                //}
                //catch
                //{
                //    aspNetRoleName = "DEFAULTROLE";
                //}
                //OrderItemModel orderItemModel = retailSlnBL.OrderItem(aspNetRoleName, null, null, null, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                //actionResult = View("Index", orderItemModel);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                ModelState.AddModelError("", "OrderInvoice / GET");
                archLibBL.CopyReponseObjectToModelErrors(ModelState, null, responseObjectModel.ResponseMessages);
                actionResult = View("Error", responseObjectModel);
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        // GET: OrderItem
        [AllowAnonymous]
        [HttpGet]
        public ActionResult OrderItem(string id, string pageNum)
        {
            ViewData["ActionName"] = "OrderItem";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            try
            {
                //int x = 1, y = 0, z = x / y;
                SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
                SessionObjectModel createForSessionObject = (SessionObjectModel)Session["CreateForSessionObject"];
                string aspNetRoleName;
                if (createForSessionObject == null)
                {
                    aspNetRoleName = "DEFAULTROLE";
                }
                else
                {
                    aspNetRoleName = createForSessionObject.AspNetRoleName;
                }
                var orderItemModel = retailSlnBL.OrderItem(aspNetRoleName, id, pageNum, "45", sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                htmlString = archLibBL.ViewToHtmlString(this, orderItemModel.ViewName, orderItemModel);
                success = true;
                processMessage = "SUCCESS!!!";
            }
            catch (ApplicationException ex)
            {
                actionResult = Json(new { success = false, errorCode = "RELOAD_PAGE", message = ex.Message }, JsonRequestBehavior.AllowGet);
                return actionResult;
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
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        [AjaxAuthorize]
        [Authorize]
        [HttpPost]
        public ActionResult PaymentInfo1(CompleteOrderModel completeOrderModel)
        {//Credit Sale
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString, redirectUrl = "";
            SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
            SessionObjectModel createForSessionObject = (SessionObjectModel)Session["CreateForSessionObject"];
            if (createForSessionObject == null)
            {
                createForSessionObject = sessionObjectModel;
            }
            PaymentInfoModel paymentInfoModel = (PaymentInfoModel)Session["PaymentInfo"];
            paymentInfoModel.CompleteOrderModel = completeOrderModel;
            Session["PaymentInfo"] = paymentInfoModel;
            try
            {
                ModelState.Clear();
                if (paymentInfoModel.PaymentModeModel.PaymentModeId == PaymentModeEnum.CreditSale)
                {
                    var corpAcctModel = ((ApplSessionObjectModel)createForSessionObject.ApplSessionObjectModel).CorpAcctModel;
                    if (corpAcctModel.OrderApprovalRequired == YesNoEnum.Yes)
                    {
                        if (string.IsNullOrWhiteSpace(completeOrderModel.ApproverSignatureTextValue))
                        {
                            ModelState.AddModelError("CompleteOrderModel.ApproverSignatureTextValue", "Enter name");
                        }
                        if (completeOrderModel.ApproverSignatureTextId == null)
                        {
                            ModelState.AddModelError("CompleteOrderModel.ApproverSignatureTextId", "Select signature");
                        }
                    }
                    try
                    {
                        completeOrderModel.PaymentAmount = long.Parse(completeOrderModel.PaymentAmount.ToString());
                    }
                    catch
                    {
                        completeOrderModel.PaymentAmount = null;
                    }
                }
                if (ModelState.IsValid)
                {
                    success = true;
                    processMessage = "SUCCESS!!!";
                    htmlString = retailSlnBL.PaymentInfo1(ref paymentInfoModel, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    Session["PaymentInfo"] = paymentInfoModel;
                    redirectUrl = Url.Action("OrderInvoice", "Home");
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
                }
                else
                {
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Model Errors");
                    success = false;
                    processMessage = "ERROR???";
                    paymentInfoModel.ResponseObjectModel = new ResponseObjectModel
                    {
                        ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                    };
                    //htmlString = retailSlnBL.PaymentInfo1(paymentInfoModel, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    htmlString = archLibBL.ViewToHtmlString(this, "_PaymentInfo0", paymentInfoModel);
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "Error while creating order");
                paymentInfoModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                };
                success = false;
                processMessage = "ERROR???";
                htmlString = archLibBL.ViewToHtmlString(this, "_PaymentInfo0", paymentInfoModel);
                //actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            }
            actionResult = Json(new { success, processMessage, htmlString, redirectUrl }, JsonRequestBehavior.AllowGet);
            return actionResult;
        }

        // POST: PaymentInfo2
        [AjaxAuthorize]
        [Authorize]
        [HttpPost]
        public ActionResult PaymentInfo2()
        {//Razorpay
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            bool success;
            string processMessage, htmlString;
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
            SessionObjectModel createsessionObjectModel = (SessionObjectModel)Session["CreateSessionObject"];
            PaymentInfoModel paymentInfoModel = (PaymentInfoModel)Session["PaymentInfo"];
            try
            {
                if (sessionObjectModel == null)
                {
                    success = false;
                    processMessage = "ERROR???";
                    ModelState.AddModelError("", "Error occurred in Payment Gateway call");
                    htmlString = archLibBL.ViewToHtmlString(this, "_PaymentInfo0Data", paymentInfoModel);
                }
                else
                {
                    RazorPayResponse razorPayResponse = retailSlnBL.PaymentInfo2(paymentInfoModel, sessionObjectModel, createsessionObjectModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    success = true;
                    processMessage = "SUCCESS!!!";
                    htmlString = JsonConvert.SerializeObject(razorPayResponse);
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                ModelState.AddModelError("", "Error occurred in Payment Gateway call");
                processMessage = "ERROR???";
                htmlString = archLibBL.ViewToHtmlString(this, "_PaymentInfo0Data", paymentInfoModel);
            }
            actionResult = Json(new { success, processMessage, htmlString });
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        // POST: PaymentInfo3
        [AjaxAuthorize]
        [HttpPost]
        [Authorize]
        public ActionResult PaymentInfo3(string razorpay_payment_id, string razorpay_order_id, string razorpay_signature)
        {//RazorpayReturn
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            PaymentInfoModel paymentInfoModel = (PaymentInfoModel)Session["PaymentInfo"];
            SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
            SessionObjectModel createForSessionObject = (SessionObjectModel)Session["CreateForSessionObject"];
            retailSlnBL.PaymentInfo3(paymentInfoModel, razorpay_payment_id, razorpay_order_id, razorpay_signature, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
            Session["PaymentInfo"] = paymentInfoModel;
            return RedirectToAction("OrderInvoice");
        }

        // POST: PaymentInfo4
        [AjaxAuthorize]
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> PaymentInfo4Intent()
        {//Stripe Payment Intent
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            PaymentIntent paymentIntent = null;
            try
            {
                //int x = 1, y = 0, z = x / y;
                SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
                SessionObjectModel createForSessionObject = (SessionObjectModel)Session["CreateForSessionObject"];
                PaymentInfoModel paymentInfoModel = (PaymentInfoModel)Session["PaymentInfo"];
                StripeConfiguration.ApiKey = paymentInfoModel.CreditCardDataModel.CreditCardDataKVPs["ConfigurationApiKey"];
                var options = new PaymentIntentCreateOptions
                {
                    Amount = long.Parse(paymentInfoModel.CreditCardDataModel.CreditCardAmount),
                    Currency = paymentInfoModel.CreditCardDataModel.CurrencyCode,
                    PaymentMethodTypes = new List<string> { "card" },
                    
                };
                var service = new PaymentIntentService();
                paymentIntent = await service.CreateAsync(options);
                success = true;
                processMessage = "SUCCESS!!!";
                htmlString = "";
                actionResult = Json(new { success, processMessage, htmlString, clientSecret = paymentIntent.ClientSecret });
                return actionResult;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                processMessage = "ERROR???";
                htmlString = exception.Message;
                actionResult = Json(new { success, processMessage, htmlString });
                return actionResult;
            }
        }

        // GET: PaymentInfo4
        [AjaxAuthorize]
        [Authorize]
        [HttpGet]
        //[Route("PaymentInfo4")]
        public ActionResult PaymentInfo4Success(string paymentIntent_status, string paymentIntent_payment_method, string paymentIntent_id)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            PaymentInfoModel paymentInfoModel = (PaymentInfoModel)Session["PaymentInfo"];
            SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
            SessionObjectModel createForSessionObject = (SessionObjectModel)Session["CreateForSessionObject"];
            retailSlnBL.PaymentInfo4Success(paymentInfoModel, paymentIntent_status, paymentIntent_payment_method, paymentIntent_id, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
            Session["PaymentInfo"] = paymentInfoModel;
            return RedirectToAction("OrderInvoice");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult PaymentInfo9()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString, htmlStringShoppingCart;
            int shoppingCartItemsCount = 0;
            string shoppingCartTotalAmount = "";

            PaymentInfoModel paymentInfoModel = (PaymentInfoModel)Session["PaymentInfo"];
            SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
            SessionObjectModel createForSessionObject = (SessionObjectModel)Session["CreateForSessionObject"];
            try
            {
                ModelState.Clear();
                retailSlnBL.PaymentInfo9(ref paymentInfoModel, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process", "ModelStateIsValid", ModelState.IsValid.ToString());
                if (ModelState.IsValid)
                {
                    shoppingCartItemsCount = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels.Count;
                    shoppingCartTotalAmount = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountFormatted;
                    htmlStringShoppingCart = archLibBL.ViewToHtmlString(this, "_ShoppingCart", paymentInfoModel);
                    success = true;
                    processMessage = "SUCCESS!!!";
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process success");
                }
                else
                {
                    ModelState.AddModelError("", "Error while initializing credit card process BL");
                    success = false;
                    processMessage = "ERROR???";
                    htmlStringShoppingCart = "";
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: Error while initializing credit card process BL");
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "Error while initializing credit card process");
                success = false;
                processMessage = "ERROR???";
                htmlStringShoppingCart = "";
            }
            htmlString = archLibBL.ViewToHtmlString(this, "_PaymentInfo9", paymentInfoModel.CreditCardDataModel);
            actionResult = Json(new { success, processMessage, htmlString, htmlStringShoppingCart, shoppingCartItemsCount, shoppingCartTotalAmount }, JsonRequestBehavior.AllowGet);
            return actionResult;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult PaymentInfo9Process(CreditCardDataModel creditCardDataModel)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString, redirectUrl = "";

            PaymentInfoModel paymentInfoModel = (PaymentInfoModel)Session["PaymentInfo"];
            SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
            SessionObjectModel createForSessionObject = (SessionObjectModel)Session["CreateForSessionObject"];
            try
            {
                ModelState.Clear();
                long.TryParse(creditCardDataModel.CreditCardNumber, out long tempLong);
                if (tempLong > 0)
                {
                    if (creditCardDataModel.CreditCardNumber.Length >= 13 && creditCardDataModel.CreditCardNumber.Length <= 19)
                    {
                    }
                    else
                    {
                        ModelState.AddModelError("CreditCardNumber", "Invalid credit card number");
                    }
                }
                else
                {
                    ModelState.AddModelError("CreditCardNumber", "Invalid credit card number");
                }
                long.TryParse(creditCardDataModel.CreditCardSecCode, out tempLong);
                if (tempLong > 0)
                {
                    if (creditCardDataModel.CreditCardSecCode.Length >= 3 && creditCardDataModel.CreditCardSecCode.Length <= 4)
                    {
                    }
                    else
                    {
                        ModelState.AddModelError("CreditCardSecCode", "Invalid CVV (Sec Code)");
                    }
                }
                else
                {
                    ModelState.AddModelError("CreditCardSecCode", "Invalid CVV (Sec Code)");
                }
                long.TryParse(creditCardDataModel.CreditCardExpMM, out tempLong);
                if (tempLong > 0 && tempLong < 13)
                {
                }
                else
                {
                    ModelState.AddModelError("CreditCardExpMM", "Invalid month");
                }
                long currentYear = DateTime.Now.Year;
                long currentYearMonth = long.Parse(DateTime.Now.ToString("yyyyMM"));
                long.TryParse(creditCardDataModel.CreditCardExpYear, out tempLong);
                if (tempLong >= currentYear && tempLong < currentYear + 15)
                {
                    if (long.Parse(creditCardDataModel.CreditCardExpYear + creditCardDataModel.CreditCardExpMM) >= currentYearMonth)
                    {
                    }
                    else
                    {
                        ModelState.AddModelError("CreditCardExpMM", "Invalid month");
                        ModelState.AddModelError("CreditCardExpYear", "Invalid year");
                    }
                }
                else
                {
                    ModelState.AddModelError("CreditCardExpYear", "Invalid year");
                }
                if (ModelState.IsValid)
                {
                    paymentInfoModel.CreditCardDataModel = creditCardDataModel;
                    retailSlnBL.PaymentInfo9Process(ref paymentInfoModel, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process", "ModelStateIsValid", ModelState.IsValid.ToString());
                    if (ModelState.IsValid)
                    {
                        success = true;
                        processMessage = "SUCCESS!!!";
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process success");
                        redirectUrl = Url.Action("OrderInvoice", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error while processing credit card");
                        success = false;
                        processMessage = "ERROR???";
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: Error while initializing credit card process BL");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Data validation failed");
                    success = false;
                    processMessage = "ERROR???";
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: Model State Error(s)");
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "Error while initializing credit card process");
                success = false;
                processMessage = "ERROR???";
            }
            paymentInfoModel.CreditCardDataModel.ResponseObjectModel = new ResponseObjectModel
            {
                ResponseTypeId = success ? ResponseTypeEnum.Success : ResponseTypeEnum.Error,
                ValidationSummaryMessage = success ? "" : ArchLibCache.ValidationSummaryMessageFixErrors,
            };
            htmlString = archLibBL.ViewToHtmlString(this, "_PaymentInfo9", paymentInfoModel.CreditCardDataModel);
            actionResult = Json(new { success, processMessage, htmlString, redirectUrl }, JsonRequestBehavior.AllowGet);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        // POST: RemoveFromCart
        [AllowAnonymous]
        [HttpPost]
        public ActionResult RemoveFromCart(RemoveFromCartModel removeFromCartModel)
        {
            ViewData["ActionName"] = "RemoveFromCart";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            int shoppingCartItemsCount = 0;
            string shoppingCartTotalAmount = ""; //0f.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
            try
            {
                //int x = 1, y = 0, z = x / y;
                PaymentInfoModel paymentInfoModel = (PaymentInfoModel)Session["PaymentInfo"];
                SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
                SessionObjectModel createForSessionObject = (SessionObjectModel)Session["CreateForSessionObject"];
                retailSlnBL.RemoveFromCart(ref paymentInfoModel, removeFromCartModel.RemoveFromCartIndex.Value, true, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                success = true;
                processMessage = "SUCCESS!!!";
                shoppingCartItemsCount = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels.Count;
                shoppingCartTotalAmount = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountFormatted;
                htmlString = archLibBL.ViewToHtmlString(this, "_ShoppingCart", paymentInfoModel);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                processMessage = "ERROR???";
                htmlString = "Error while removing item from cart";
            }
            actionResult = Json(new { success, processMessage, htmlString, shoppingCartItemsCount, shoppingCartTotalAmount }, JsonRequestBehavior.AllowGet);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        // GET: SearchResult
        [AllowAnonymous]
        [HttpGet]
        //[Route("SearchResult")]
        public ActionResult SearchResult(string id)
        {
            ViewData["ActionName"] = "SearchResult";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            //ActionResult actionResult;
            //bool success;
            //string processMessage, htmlString;
            //List<long> searchIds = new List<long> { 0, 9, 18 };
            //List<ItemModel> searchItems = RetailSlnCache.ItemModels.Where(x => searchIds.Contains(x.ItemId.Value)).ToList();
            try
            {
                SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
                SessionObjectModel createForSessionObject = (SessionObjectModel)Session["CreateForSessionObject"];
                var searchResultModel = retailSlnBL.SearchResult(id, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                return View("SearchResult", searchResultModel);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                //success = false;
                //processMessage = "ERROR???";
                //htmlString = "Error while searching " + id;
                return null;
            }
        }

        // GET: ShoppingCart
        [AllowAnonymous]
        [HttpGet]
        public ActionResult ShoppingCart()
        {
            ViewData["ActionName"] = "ShoppingCart";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ArchLibBL archLibBL = new ArchLibBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            int shoppingCartItemsCount;
            string shoppingCartTotalAmount;
            PaymentInfoModel paymentInfoModel = ((PaymentInfoModel)Session["PaymentInfo"]);
            success = true;
            processMessage = "SUCCESS!!!";
            htmlString = archLibBL.ViewToHtmlString(this, "_ShoppingCart", paymentInfoModel);
            shoppingCartItemsCount = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels.Count;
            shoppingCartTotalAmount = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountFormatted;
            actionResult = Json(new { success, processMessage, htmlString, shoppingCartItemsCount, shoppingCartTotalAmount }, JsonRequestBehavior.AllowGet);
            return actionResult;
        }

        // GET: ShoppingCartComments
        [AllowAnonymous]
        [HttpGet]
        public ActionResult ShoppingCartComments(string index, string bundleIndex, string orderComments)
        {
            ViewData["ActionName"] = "ShoppingCartComments";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            try
            {
                //int x = 1, y = 0, z = x / y;
                PaymentInfoModel paymentInfoModel = (PaymentInfoModel)Session["PaymentInfo"];
                SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
                SessionObjectModel createForSessionObject = (SessionObjectModel)Session["CreateForSessionObject"];
                retailSlnBL.ShoppingCartComments(paymentInfoModel, index, bundleIndex, orderComments, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                success = true;
                processMessage = "SUCCESS!!!";
                htmlString = archLibBL.ViewToHtmlString(this, "_ShoppingCart", paymentInfoModel);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                processMessage = "ERROR???";
                htmlString = "Error while updating comments";
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            return actionResult;
        }
    }
}
