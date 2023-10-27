using ArchitectureLibraryBusinessLayer;
using ArchitectureLibraryCacheData;
using ArchitectureLibraryClassCode;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using Microsoft.Owin;
using Newtonsoft;
using Newtonsoft.Json;
using RetailSlnBusinessLayer;
using RetailSlnCacheData;
using RetailSlnEnumerations;
using RetailSlnModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace RetailSlnWeb.Controllers
{
    public class WholesaleController : Controller
    {
        private readonly long clientId = long.Parse(Utilities.GetApplicationValue("ClientId"));
        private readonly string execUniqueId = Utilities.CreateExecUniqueId();

        // GET: Wholesale
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Wholesale(string id)
        {
            //int x = 1, y = 0, z = x / y;
            if (string.IsNullOrWhiteSpace(id))
            {
                ViewData["ActionName"] = "REGISTER";
            }
            else
            {
                ViewData["ActionName"] = id.ToUpper();
            }
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                RegisterUserLoginUserModel registerUserLoginUserModel = archLibBL.RegisterUserLoginUser(id, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                registerUserLoginUserModel.RegisterUserProfModel.QueryString1 = id;
                actionResult = View("Index", registerUserLoginUserModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                ModelState.AddModelError("", "Register User / Login / Reset Password / GET");
                archLibBL.CopyReponseObjectToModelErrors(ModelState, null, responseObjectModel.ResponseMessages);
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        [AjaxAuthorize]
        [Authorize]
        [HttpPost]
        public ActionResult AddToCart(List<ShoppingCartItemModel> shoppingCartItemModels)
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
                    htmlString = archLibBL.ViewToHtmlString(this, "_OrderItemListData", null);
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

        [AjaxAuthorize]
        [Authorize]
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

        [AllowAnonymous]
        [HttpGet]
        public ActionResult DeliveryInfo()
        {
            ViewData["ActionName"] = "DeliveryInfo";
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
                DeliveryInfoModel deliveryInfoModel = retailSlnBL.DeliveryInfo(Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                success = true;
                processMessage = "SUCCESS!!!";
                htmlString = archLibBL.ViewToHtmlString(this, "_DeliveryInfo", deliveryInfoModel);
                actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
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
                if (deliveryInfoDataModel.DeliveryMethodId == RetailSlnEnumerations.DeliveryMethodEnum.PickupFromStore)
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
                        ModelState.AddModelError("DeliveryAddressModel.ZipCode", "Zip Code");
                    }
                    if (deliveryInfoDataModel.DeliveryAddressModel.DemogInfoSubDivisionId == null)
                    {
                        ModelState.AddModelError("DeliveryAddressModel.DemogInfoSubDivisionId", "State");
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
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            actionResult = Json(new { success, processMessage, htmlString });
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult LoginUserProf(LoginUserProfModel loginUserProfModel)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
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
                    if (sessionObjectModel.AspNetRoleName == "WSALEUSER")
                    {

                    }
                    else
                    {
                        archLibBL.GenerateCaptchaQuesion(Session, "CaptchaNumberLogin0", "CaptchaNumberLogin1");
                        loginUserProfModel.CaptchaAnswerLogin = null;
                        loginUserProfModel.CaptchaNumberLogin0 = Session["CaptchaNumberLogin0"].ToString();
                        loginUserProfModel.CaptchaNumberLogin1 = Session["CaptchaNumberLogin1"].ToString();
                        ModelState.AddModelError("LoginEmailAddress", "Please check your email address");
                        ModelState.AddModelError("LoginPassword", "Please check your login password");
                        ModelState.AddModelError("", "Unable to login with credentials supplied");
                        ModelState.AddModelError("", "It is likely your password is expired");
                        sessionObjectModel = null;
                    }
                }
                if (ModelState.IsValid)
                {
                    success = true;
                    processMessage = "SUCCESS!!!";
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
                    success = true;
                    processMessage = "ERROR???";
                    string redirectUrl = Url.Action(sessionObjectModel.ActionName, sessionObjectModel.ControllerName);
                    actionResult = Json(new { success, processMessage, redirectUrl });
                    //return Content(@"/Home/UserProfile");
                    //return new HttpStatusCodeResult(System.Net.HttpStatusCode.Redirect,url)
                    //return PartialView("JavascriptRedirect", new JavascriptRedirectModel("http://www.google.com"));
                    //https://stackoverflow.com/questions/1538523/how-to-get-an-asp-net-mvc-ajax-response-to-redirect-to-new-page-instead-of-inser
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
                }
                else
                {
                    success = false;
                    processMessage = "ERROR???";
                    htmlString = archLibBL.ViewToHtmlString(this, "_LoginUserProfData", loginUserProfModel);
                    actionResult = Json(new { success, processMessage, htmlString });
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process Error");
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                archLibBL.GenerateCaptchaQuesion(Session, "CaptchaNumberLogin0", "CaptchaNumberLogin1");
                loginUserProfModel.CaptchaAnswerLogin = null;
                loginUserProfModel.CaptchaNumberLogin0 = Session["CaptchaNumberLogin0"].ToString();
                loginUserProfModel.CaptchaNumberLogin1 = Session["CaptchaNumberLogin1"].ToString();
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                htmlString = archLibBL.ViewToHtmlString(this, "_LoginUserProfData", loginUserProfModel);
                success = false;
                processMessage = "ERROR???";
                actionResult = Json(new { success, processMessage, htmlString });
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00099100 :: Error Exit");
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Logout()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            Request.GetOwinContext().Authentication.SignOut();
            Request.GetOwinContext().Authentication.SignOut();
            Session["SessionObject"] = null;
            Session.Abandon();
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return RedirectToAction("Index");
        }

        [AjaxAuthorize]
        [Authorize]
        [HttpGet]
        public ActionResult OrderItemList()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            bool success;
            string processMessage, htmlString;
            try
            {
                //int x = 1, y = 0, z = x / y;
                //OrderItemListModel orderItemListModel = retailSlnBL.OrderItemListModel(this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                success = true;
                processMessage = "SUCCESS!!!";
                htmlString = archLibBL.ViewToHtmlString(this, "_OrderItemListData", null);
                string shoppingCartItemsCount, shoppingCartTotalAmount, shoppingCartHtmlString;
                ShoppingCartModel shoppingCartModel = (ShoppingCartModel)Session["ShoppingCartModel"];
                if (shoppingCartModel == null)
                {
                    shoppingCartModel = new ShoppingCartModel
                    {
                        Checkout = true,
                    };
                    shoppingCartItemsCount = "Empty";
                    shoppingCartTotalAmount = "Cart";
                }
                else
                {
                    shoppingCartItemsCount = shoppingCartModel.ShoppingCartItems.Count.ToString();
                    shoppingCartTotalAmount = shoppingCartModel.ShoppingCartTotalAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                }
                shoppingCartHtmlString = archLibBL.ViewToHtmlString(this, "_ShoppingCart", shoppingCartModel);
                actionResult = Json(new { success, processMessage, htmlString, shoppingCartItemsCount, shoppingCartTotalAmount, shoppingCartHtmlString }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                return View();
            }
            return actionResult;
        }

        [AjaxAuthorize]
        [Authorize]
        [HttpGet]
        public ActionResult OrderProcess()
        {
            //int x = 1, y = 0, z = x / y;
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
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00099100 :: Error Exit");
            }
            actionResult = Json(new { success, processMessage, htmlString });
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult RegisterUserProf(RegisterUserProfModel registerUserProfModel)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            bool success;
            string processMessage, htmlString;
            try
            {
                //int x = 1, y = 0, z = x / y;
                ModelState.Clear();
                TryValidateModel(registerUserProfModel);
                archLibBL.RegisterUserProf(ref registerUserProfModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (ModelState.IsValid)
                {
                    success = true;
                    processMessage = "SUCCESS!!!";
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
                }
                else
                {
                    success = false;
                    processMessage = "ERROR???";
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process Error");
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                archLibBL.GenerateCaptchaQuesion(Session, "CaptchaNumberRegister0", "CaptchaNumberRegister1");
                registerUserProfModel.CaptchaAnswerRegister = null;
                registerUserProfModel.CaptchaNumberRegister0 = Session["CaptchaNumberRegister0"].ToString();
                registerUserProfModel.CaptchaNumberRegister1 = Session["CaptchaNumberRegister1"].ToString();
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                registerUserProfModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                };
                success = false;
                processMessage = "ERROR???";
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00099100 :: Error Exit");
            }
            htmlString = archLibBL.ViewToHtmlString(this, "_RegisterUserProfData", registerUserProfModel);
            actionResult = Json(new { success, processMessage, htmlString });
            //actionResult = PartialView("_RegisterUserProfData", registerUserProfModel);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        [AjaxAuthorize]
        [Authorize]
        [HttpGet]
        public ActionResult RemoveFromCart(string id)
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
                htmlString = archLibBL.ViewToHtmlString(this, "_OrderItemListData", null);
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

        [AjaxAuthorize]
        [Authorize]
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

        [AjaxAuthorize]
        [Authorize]
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
                    actionResult = Json(new { shoppingCartItemsCount = "Empty", shoppingCartTotalAmount = "Cart" }, JsonRequestBehavior.AllowGet);
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
