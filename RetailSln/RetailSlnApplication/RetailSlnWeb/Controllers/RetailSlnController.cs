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
        public HomeController()
        {
        }

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
                ShoppingCartModel shoppingCartModel = (ShoppingCartModel)Session["ShoppingCart"];
                SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
                SessionObjectModel createForSessionObject = (SessionObjectModel)Session["CreateForSessionObject"];
                htmlString = retailSlnBL.AddToCart(ref shoppingCartModel, addToCartModel, true, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                Session["ShoppingCart"] = shoppingCartModel;
                if (htmlString == "")
                {
                    shoppingCartItemsCount = shoppingCartModel.ShoppingCartItemModels.Count;
                    shoppingCartTotalAmount = shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountFormatted;
                    success = true;
                    processMessage = "SUCCESS!!!";
                    htmlString = archLibBL.ViewToHtmlString(this, "_ShoppingCart", shoppingCartModel);
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
                ShoppingCartModel shoppingCartModel = (ShoppingCartModel)Session["ShoppingCart"];
                CheckoutModel checkoutModel = retailSlnBL.Checkout(shoppingCartModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (checkoutModel == null)
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
                        DeliveryInfoModel deliveryInfoModel = retailSlnBL.Checkout(sessionObject, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                        Session["DeliveryInfo"] = deliveryInfoModel;
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
                actionResult = RedirectToAction("Index");
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        // POST: CheckoutOTPRequest
        [AllowAnonymous]
        [HttpPost]
        public ActionResult CheckoutOTPRequest(OTPRequestModel oTPRequestModel)
        {
            //int x = 1, y = 0, z = x / y;
            ViewData["ActionName"] = "CheckoutOTPRequest";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            bool success;
            string processMessage, htmlString;
            OTPResponseModel oTPResponseModel = null;
            //int x = 1, y = 0, z = x / y;
            try
            {
                ModelState.Clear();
                TryValidateModel(oTPRequestModel);
                if (ModelState.IsValid)
                {
                    oTPResponseModel = archLibBL.CheckoutOTPRequest(ref oTPRequestModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "Error during OTP setup");
                htmlString = archLibBL.ViewToHtmlString(this, "_OTPRequestData", oTPRequestModel);
            }
            if (ModelState.IsValid)
            {
                success = true;
                processMessage = "SUCCESS!!!";
                oTPResponseModel.RequestType = "Checkout";
                htmlString = archLibBL.ViewToHtmlString(this, "_OTPResponse", oTPResponseModel);
            }
            else
            {
                success = false;
                processMessage = "ERROR???";
                archLibBL.GenerateCaptchaQuesion(Session, "CaptchaNumber0", "CaptchaNumber1");
                oTPRequestModel.CaptchaAnswer = null;
                oTPRequestModel.CaptchaNumber0 = Session["CaptchaNumber0"].ToString();
                oTPRequestModel.CaptchaNumber1 = Session["CaptchaNumber1"].ToString();
                oTPRequestModel.RequestType = "Checkout";
                htmlString = archLibBL.ViewToHtmlString(this, "_OTPRequestData", oTPRequestModel);
            }
            actionResult = Json(new { success, processMessage, htmlString });
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        // POST: CheckoutOTPResponse
        [HttpPost]
        public ActionResult CheckoutOTPResponse(OTPResponseModel oTPResponseModel)
        {
            //int x = 1, y = 0, z = x / y;
            ViewData["ActionName"] = "CheckoutOTPResponse";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            bool success;
            string processMessage, htmlString;
            //DeliveryInfoModel deliveryInfoModel = null;
            try
            {
                //int x = 1, y = 0, z = x / y;
                ModelState.Clear();
                TryValidateModel(oTPResponseModel);
                if (string.IsNullOrWhiteSpace(oTPResponseModel.EmailAddress))
                {
                    ModelState.Remove("EmailAddress");
                }
                if (ModelState.IsValid)
                {
                    SessionObjectModel sessionObjectModel = archLibBL.CheckoutOTPResponse(ref oTPResponseModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    if (ModelState.IsValid)
                    {
                        if (sessionObjectModel.NewUser && sessionObjectModel.AspNetRoleName != "GUESTROLE")
                        {
                            retailSlnBL.RegisterUserProfPersonExtn1(sessionObjectModel.PersonId, 0, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                        }
                        Dictionary<string, AspNetRoleKVPModel> aspNetRoleKVPs = ArchLibCache.AspNetRoleKVPs[sessionObjectModel.AspNetRoleName];
                        sessionObjectModel.AspNetRoleNameProxy = aspNetRoleKVPs["ProxyAspNetRoleName00"].KVPValueData;
                        string currentLoggedInUserId = loggedInUserId;
                        LoginUserProfProcess(currentLoggedInUserId, sessionObjectModel);
                        SessionObjectModel createForSessionObject = (SessionObjectModel)Session["CreateForSessionObject"];
                        success = true;
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
                    }
                    else
                    {
                        success = false;
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process Error");
                    }
                }
                else
                {
                    success = false;
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                archLibBL.GenerateCaptchaQuesion(Session, "CaptchaNumber0", "CaptchaNumber1");
                ModelState.AddModelError("", "Error during OTP setup");
            }
            if (success)
            {
                processMessage = "SUCCESS!!!";
                SessionObjectModel sessionObject = (SessionObjectModel)Session["SessionObject"];
                SessionObjectModel createForSessionObject = (SessionObjectModel)Session["CreateForSessionObject"];
                DeliveryInfoModel deliveryInfoModel = retailSlnBL.Checkout(sessionObject, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                htmlString = archLibBL.ViewToHtmlString(this, "_DeliveryInfo", deliveryInfoModel);
                actionResult = Json(new { success, processMessage, htmlString });
            }
            else
            {
                oTPResponseModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                };
                processMessage = "ERROR???";
                htmlString = archLibBL.ViewToHtmlString(this, "_OTPResponseData", oTPResponseModel);
                actionResult = Json(new { success, processMessage, htmlString });
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        // POST: DeliveryInfo
        [Authorize]
        [AjaxAuthorize]
        [HttpPost]
        public ActionResult DeliveryInfo(DeliveryInfoModel deliveryInfoModel)
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
            if (sessionObjectModel == null || createForSessionObject == null)
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
                PaymentInfoModel paymentInfoModel = null;
                try
                {
                    Session["DeliveryInfo"] = deliveryInfoModel;
                    ModelState.Clear();
                    TryValidateModel(deliveryInfoModel);
                    TryValidateModel(deliveryInfoModel.CompleteOrderModel, "CompleteOrderModel");
                    if (!deliveryInfoModel.CompleteOrderModel.ApprovalRequired)
                    {
                        ModelState.Remove("CompleteOrderModel.ApproverSignatureTextId");
                        ModelState.Remove("CompleteOrderModel.ApproverSignatureTextValue");
                    }
                    TryValidateModel(deliveryInfoModel.CouponPaymentModel, "CouponPaymentModel");
                    TryValidateModel(deliveryInfoModel.DeliveryAddressModel, "DeliveryAddressModel");
                    TryValidateModel(deliveryInfoModel.DeliveryDataModel, "DeliveryDataModel");
                    TryValidateModel(deliveryInfoModel.DeliveryMethodModel, "DeliveryMethodModel");
                    TryValidateModel(deliveryInfoModel.GiftCertPaymentModel, "GiftCertPaymentModel");
                    TryValidateModel(deliveryInfoModel.OrderSummaryModel, "OrderSummaryModel");
                    TryValidateModel(deliveryInfoModel.PaymentModeModel, "PaymentModeModel");
                    if (ModelState.IsValid)
                    {
                        ShoppingCartModel shoppingCartModel = (ShoppingCartModel)Session["ShoppingCart"];
                        paymentInfoModel = retailSlnBL.DeliveryInfo(shoppingCartModel, deliveryInfoModel, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                        if (ModelState.IsValid && paymentInfoModel != null)
                        {
                            success = true;
                            paymentInfoModel.ShoppingCartModel = shoppingCartModel;
                            Session["PaymentInfo"] = paymentInfoModel;
                        }
                        else
                        {
                            ModelState.AddModelError("", "Error while processing delivery info");
                            success = false;
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Data errors in delivery info");
                        success = false;
                    }
                }
                catch (Exception exception)
                {
                    exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                    success = false;
                }
                if (success)
                {
                    processMessage = "SUCCESS!!!";
                    htmlString = archLibBL.ViewToHtmlString(this, "_PaymentInfo0", paymentInfoModel);
                }
                else
                {
                    processMessage = "ERROR???";
                    deliveryInfoModel.ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Error,
                        ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                    };
                    htmlString = archLibBL.ViewToHtmlString(this, "_DeliveryInfoData", deliveryInfoModel);
                }
                actionResult = Json(new { success, processMessage, htmlString });
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        #region Commented - Creating PDF file is taking time - Will address later
        //// GET : DownloadItemCatalog
        //[AllowAnonymous]
        //[HttpGet]
        //public FileResult DownloadItemCatalog()
        //{
        //    string fileName = "ItemCatalog.pdf";
        //    var downloadFullFileName = Server.MapPath("~/Files/ItemCatalog/") + @"\" + fileName;
        //    return File(downloadFullFileName, "application/force-download", fileName);
        //}
        #endregion

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
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        // GET : ItemCatalog
        [AllowAnonymous]
        [HttpGet]
        [Route("ItemCatalog")]
        public ActionResult ItemCatalog()
        {
            ViewData["ActionName"] = "ItemCatalog";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
            ArchLibBL archLibBL = new ArchLibBL();
            //RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            try
            {
                actionResult = View();
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

        // GET : ItemCatalogData
        [AllowAnonymous]
        [HttpGet]
        public ActionResult ItemCatalogData(string parentCategoryId)
        {
            ViewData["ActionName"] = "ItemCatalogData";
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
                ItemCatalogModel itemCatalogModel = retailSlnBL.ItemCatalogData(parentCategoryId, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = PartialView("_ItemCatalog", itemCatalogModel);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "ItemCatalogData / GET");
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error");
            }
            return actionResult;
        }

        #region Commented out code
        //// GET : ItemCatalog
        //[AllowAnonymous]
        //[HttpGet]
        //[Route("ItemCatalog")]
        //public ActionResult ItemCatalog(string id)
        //{
        //    ViewData["ActionName"] = "ItemCatalog";
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
        //    ArchLibBL archLibBL = new ArchLibBL();
        //    RetailSlnBL retailSlnBL = new RetailSlnBL();
        //    ActionResult actionResult;
        //    try
        //    {
        //        SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
        //        SessionObjectModel createForSessionObject = (SessionObjectModel)Session["CreateForSessionObject"];
        //        ItemCatalogModel itemCatalogModel = retailSlnBL.ItemCatalog(id, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        actionResult = View("ItemCatalog", itemCatalogModel);
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        ModelState.AddModelError("", "ItemCatalog / GET");
        //        archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        actionResult = View("Error");
        //    }
        //    return actionResult;
        //}
        #endregion

        #region Commented Out Code Real Time Processing
        //// GET : ItemCatalog
        //[AllowAnonymous]
        //[HttpGet]
        //[Route("ItemCatalog")]
        //public ActionResult ItemCatalog()
        //{
        //    ViewData["ActionName"] = "ItemCatalog";
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
        //    ArchLibBL archLibBL = new ArchLibBL();
        //    ActionResult actionResult;
        //    RetailSlnBL retailSlnBL = new RetailSlnBL();
        //    try
        //    {
        //        SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
        //        SessionObjectModel createForSessionObject = (SessionObjectModel)Session["CreateForSessionObject"];
        //        retailSlnBL.ItemCatalog(sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        actionResult = View();
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        ModelState.AddModelError("", "ItemCatalogNew / GET");
        //        archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        actionResult = View("Error");
        //    }
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //    return actionResult;
        //}

        //// GET : ItemCatalogData
        //[AllowAnonymous]
        //[HttpGet]
        //public ActionResult ItemCatalogData(string parentCategoryId)
        //{
        //    ViewData["ActionName"] = "ItemCatalogData";
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
        //    ArchLibBL archLibBL = new ArchLibBL();
        //    ActionResult actionResult;
        //    RetailSlnBL retailSlnBL = new RetailSlnBL();
        //    try
        //    {
        //        SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
        //        SessionObjectModel createForSessionObject = (SessionObjectModel)Session["CreateForSessionObject"];
        //        ItemCatalogModel itemCatalogModel = retailSlnBL.ItemCatalogData(parentCategoryId, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        actionResult = PartialView("_ItemCatalogData", itemCatalogModel);
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        ModelState.AddModelError("", "ItemCatalogNewData / GET");
        //        archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        actionResult = View("Error");
        //    }
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //    return actionResult;
        //}

        //// POST : ItemCatalogDetail
        //[AllowAnonymous]
        //[HttpPost]
        //public ActionResult ItemCatalogDetail(ItemCatalogModel itemCatalogModel)
        //{
        //    ViewData["ActionName"] = "ItemCatalogNewData";
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
        //    ArchLibBL archLibBL = new ArchLibBL();
        //    RetailSlnBL retailSlnBL = new RetailSlnBL();
        //    ActionResult actionResult;
        //    try
        //    {
        //        SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
        //        SessionObjectModel createForSessionObject = (SessionObjectModel)Session["CreateForSessionObject"];
        //        retailSlnBL.ItemCatalogDetail(itemCatalogModel, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        actionResult = PartialView("_ItemCatalogDetail", itemCatalogModel);
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        ModelState.AddModelError("", "ItemCatalogNewData / GET");
        //        archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        actionResult = PartialView("_Error");
        //    }
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //    return actionResult;
        //}
        #endregion

        #region Commented Code
        //// GET : ItemCatalog
        //[AllowAnonymous]
        //[HttpGet]
        //public ActionResult ItemCatalogItem(string id)
        //{
        //    ViewData["ActionName"] = "ItemCatalogItem";
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
        //    ArchLibBL archLibBL = new ArchLibBL();
        //    RetailSlnBL retailSlnBL = new RetailSlnBL();
        //    ActionResult actionResult;
        //    try
        //    {
        //        SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
        //        SessionObjectModel createForSessionObject = (SessionObjectModel)Session["CreateForSessionObject"];
        //        ItemCatalogItemFileModel itemCatalogItemModel = retailSlnBL.ItemCatalogItem(id, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        actionResult = View("_ItemCatalogItem", itemCatalogItemModel);
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        ModelState.AddModelError("", "ItemCatalog / GET");
        //        archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        actionResult = View("Error");
        //    }
        //    return actionResult;
        //}

        //// GET : ItemCatalogNewData
        //[AllowAnonymous]
        //[HttpGet]
        //public ActionResult ItemCatalogNewDetail(string parentCategoryId, string pageNum, string pageSize)
        //{
        //    ViewData["ActionName"] = "ItemCatalogNewData";
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
        //    ArchLibBL archLibBL = new ArchLibBL();
        //    ActionResult actionResult;
        //    try
        //    {
        //        int skipCount = (int.Parse(pageNum) - 1) * int.Parse(pageSize);
        //        ItemCatalogNewModel itemCatalogNewModel = new ItemCatalogNewModel
        //        {
        //            ItemMasterModels = RetailSlnCache.ItemMasterModels.Where(x => x.ItemMasterId > 0).Skip(skipCount).Take(int.Parse(pageSize)).ToList(),
        //        };
        //        actionResult = PartialView("_ItemCatalogNewDetail", itemCatalogNewModel);
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        ModelState.AddModelError("", "ItemCatalogNewData / GET");
        //        archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        actionResult = PartialView("_Error");
        //    }
        //    return actionResult;
        //}
        #endregion

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
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "Item Master Attributes View / GET");
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error");
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
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
                OrderInvoiceModel orderInvoiceModel = (OrderInvoiceModel)Session["OrderInvoice"];
                actionResult = View("OrderInvoice", orderInvoiceModel);
                Session["DeliveryInfo"] = null;
                Session["PaymentInfo"] = null;
                Session["ShoppingCart"] = null;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                actionResult = RedirectToAction("Index");
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

        // POST: PaymentInfo1
        [AjaxAuthorize]
        [Authorize]
        [HttpPost]
        public ActionResult PaymentInfo1()
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
            Session["PaymentInfo"] = paymentInfoModel;
            try
            {
                ModelState.Clear();
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

        // POST: PaymentInfo9
        [AjaxAuthorize]
        [Authorize]
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
                retailSlnBL.PaymentInfo9(paymentInfoModel, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process", "ModelStateIsValid", ModelState.IsValid.ToString());
                if (ModelState.IsValid)
                {
                    shoppingCartItemsCount = paymentInfoModel.ShoppingCartModel.ShoppingCartItemModels.Count;
                    shoppingCartTotalAmount = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountFormatted;
                    htmlStringShoppingCart = archLibBL.ViewToHtmlString(this, "_ShoppingCart", paymentInfoModel.ShoppingCartModel);
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

        // POST: PaymentInfo9Process
        [AjaxAuthorize]
        [Authorize]
        [HttpPost]
        public ActionResult PaymentInfo10(CreditCardDataModel creditCardDataModel)
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
                paymentInfoModel.CreditCardDataModel = creditCardDataModel;
                retailSlnBL.PaymentInfo9Validate(paymentInfoModel, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (ModelState.IsValid)
                {
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

        #region Commenting Code for now - Need this after fixing
        //[AjaxAuthorize]
        //[Authorize]
        //[HttpPost]
        //public ActionResult PaymentInfo1(CompleteOrderModel completeOrderModel)
        //{//Credit Sale
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
        //    ArchLibBL archLibBL = new ArchLibBL();
        //    RetailSlnBL retailSlnBL = new RetailSlnBL();
        //    ActionResult actionResult;
        //    bool success;
        //    string processMessage, htmlString, redirectUrl = "";
        //    SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
        //    SessionObjectModel createForSessionObject = (SessionObjectModel)Session["CreateForSessionObject"];
        //    if (createForSessionObject == null)
        //    {
        //        createForSessionObject = sessionObjectModel;
        //    }
        //    PaymentInfoModel paymentInfoModel = (PaymentInfoModel)Session["PaymentInfo"];
        //    paymentInfoModel.CompleteOrderModel = completeOrderModel;
        //    Session["PaymentInfo"] = paymentInfoModel;
        //    try
        //    {
        //        ModelState.Clear();
        //        if (paymentInfoModel.PaymentModeModel.PaymentModeId == PaymentModeEnum.CreditSale)
        //        {
        //            var corpAcctModel = ((ApplSessionObjectModel)createForSessionObject.ApplSessionObjectModel).CorpAcctModel;
        //            if (corpAcctModel.OrderApprovalRequired == YesNoEnum.Yes)
        //            {
        //                if (string.IsNullOrWhiteSpace(completeOrderModel.ApproverSignatureTextValue))
        //                {
        //                    ModelState.AddModelError("CompleteOrderModel.ApproverSignatureTextValue", "Enter name");
        //                }
        //                if (completeOrderModel.ApproverSignatureTextId == null)
        //                {
        //                    ModelState.AddModelError("CompleteOrderModel.ApproverSignatureTextId", "Select signature");
        //                }
        //            }
        //            try
        //            {
        //                completeOrderModel.PaymentAmount = long.Parse(completeOrderModel.PaymentAmount.ToString());
        //            }
        //            catch
        //            {
        //                completeOrderModel.PaymentAmount = null;
        //            }
        //        }
        //        if (ModelState.IsValid)
        //        {
        //            success = true;
        //            processMessage = "SUCCESS!!!";
        //            htmlString = retailSlnBL.PaymentInfo1(ref paymentInfoModel, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            Session["PaymentInfo"] = paymentInfoModel;
        //            redirectUrl = Url.Action("OrderInvoice", "Home");
        //            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
        //        }
        //        else
        //        {
        //            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Model Errors");
        //            success = false;
        //            processMessage = "ERROR???";
        //            paymentInfoModel.ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
        //            };
        //            //htmlString = retailSlnBL.PaymentInfo1(paymentInfoModel, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            htmlString = archLibBL.ViewToHtmlString(this, "_PaymentInfo0", paymentInfoModel);
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        ModelState.AddModelError("", "Error while creating order");
        //        paymentInfoModel.ResponseObjectModel = new ResponseObjectModel
        //        {
        //            ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
        //        };
        //        success = false;
        //        processMessage = "ERROR???";
        //        htmlString = archLibBL.ViewToHtmlString(this, "_PaymentInfo0", paymentInfoModel);
        //        //actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
        //    }
        //    actionResult = Json(new { success, processMessage, htmlString, redirectUrl }, JsonRequestBehavior.AllowGet);
        //    return actionResult;
        //}

        //// POST: PaymentInfo4
        //[AjaxAuthorize]
        //[Authorize]
        //[HttpPost]
        //public async Task<ActionResult> PaymentInfo4Intent()
        //{//Stripe Payment Intent
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
        //    ArchLibBL archLibBL = new ArchLibBL();
        //    RetailSlnBL retailSlnBL = new RetailSlnBL();
        //    ActionResult actionResult;
        //    bool success;
        //    string processMessage, htmlString;
        //    PaymentIntent paymentIntent = null;
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
        //        SessionObjectModel createForSessionObject = (SessionObjectModel)Session["CreateForSessionObject"];
        //        PaymentInfoModel paymentInfoModel = (PaymentInfoModel)Session["PaymentInfo"];
        //        StripeConfiguration.ApiKey = paymentInfoModel.CreditCardDataModel.CreditCardDataKVPs["ConfigurationApiKey"];
        //        var options = new PaymentIntentCreateOptions
        //        {
        //            Amount = long.Parse(paymentInfoModel.CreditCardDataModel.CreditCardAmount),
        //            Currency = paymentInfoModel.CreditCardDataModel.CurrencyCode,
        //            PaymentMethodTypes = new List<string> { "card" },

        //        };
        //        var service = new PaymentIntentService();
        //        paymentIntent = await service.CreateAsync(options);
        //        success = true;
        //        processMessage = "SUCCESS!!!";
        //        htmlString = "";
        //        actionResult = Json(new { success, processMessage, htmlString, clientSecret = paymentIntent.ClientSecret });
        //        return actionResult;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        success = false;
        //        processMessage = "ERROR???";
        //        htmlString = exception.Message;
        //        actionResult = Json(new { success, processMessage, htmlString });
        //        return actionResult;
        //    }
        //}

        //// GET: PaymentInfo4
        //[AjaxAuthorize]
        //[Authorize]
        //[HttpGet]
        //public ActionResult PaymentInfo4Success(string paymentIntent_status, string paymentIntent_payment_method, string paymentIntent_id)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "01000Url", "Url", Request.Url.AbsoluteUri);
        //    ArchLibBL archLibBL = new ArchLibBL();
        //    RetailSlnBL retailSlnBL = new RetailSlnBL();
        //    PaymentInfoModel paymentInfoModel = (PaymentInfoModel)Session["PaymentInfo"];
        //    SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
        //    SessionObjectModel createForSessionObject = (SessionObjectModel)Session["CreateForSessionObject"];
        //    retailSlnBL.PaymentInfo4Success(paymentInfoModel, paymentIntent_status, paymentIntent_payment_method, paymentIntent_id, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    Session["PaymentInfo"] = paymentInfoModel;
        //    return RedirectToAction("OrderInvoice");
        //}
        #endregion

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
                ShoppingCartModel shoppingCartModel = (ShoppingCartModel)Session["ShoppingCart"];
                SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
                SessionObjectModel createForSessionObject = (SessionObjectModel)Session["CreateForSessionObject"];
                retailSlnBL.RemoveFromCart(shoppingCartModel, removeFromCartModel.RemoveFromCartIndex.Value, true, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                success = true;
                processMessage = "SUCCESS!!!";
                shoppingCartItemsCount = shoppingCartModel.ShoppingCartItemModels.Count;
                shoppingCartTotalAmount = shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountFormatted;
                htmlString = archLibBL.ViewToHtmlString(this, "_ShoppingCart", shoppingCartModel);
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
        public ActionResult SearchResult(string id)
        {
            ViewData["ActionName"] = "SearchResult";
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
                SearchResultModel itemCatalogFileModel = retailSlnBL.SearchResult(id, null, null, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = PartialView("SearchResult", itemCatalogFileModel);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                actionResult = PartialView("Error");
            }
            return actionResult;
        }

        // POST: SearchResultItemMaster
        [AllowAnonymous]
        [HttpPost]
        public ActionResult SearchResultItemMaster(string searchText, string pageNum, string pageSize)
        {
            ViewData["ActionName"] = "SearchResultItemMaster";
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
                SearchResultModel searchResultModel = retailSlnBL.SearchResult(searchText, pageNum, pageSize, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = PartialView("_SearchResultItemMaster", searchResultModel);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                actionResult = PartialView("Error");
            }
            return actionResult;
        }

        // POST: SearchText
        [AllowAnonymous]
        [HttpPost]
        public ActionResult SearchText(string parentCategoryId, string searchText, string pageNum, string pageSize)
        {
            ViewData["ActionName"] = "SearchText";
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
                ItemCatalogFileModel itemCatalogFileModel = retailSlnBL.SearchText(parentCategoryId, searchText, pageNum, pageSize, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    actionResult = RedirectToAction("ItemCatalogData", new { parentCategoryId });
                }
                else
                {
                    actionResult = PartialView("_ItemCatalogFile", itemCatalogFileModel);
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                //success = false;
                //processMessage = "ERROR???";
                //htmlString = "Error while searching " + id;
                actionResult = PartialView("_Error");
            }
            return actionResult;
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
            ShoppingCartModel shoppingCartModel = ((ShoppingCartModel)Session["ShoppingCart"]);
            shoppingCartModel.ShoppingCartItemModels = shoppingCartModel.ShoppingCartItemModels ?? new List<ShoppingCartItemModel>();
            shoppingCartModel.ShoppingCartSummaryModel = shoppingCartModel.ShoppingCartSummaryModel ?? new ShoppingCartSummaryModel();
            success = true;
            processMessage = "SUCCESS!!!";
            htmlString = archLibBL.ViewToHtmlString(this, "_ShoppingCart", shoppingCartModel);
            shoppingCartItemsCount = shoppingCartModel.ShoppingCartItemModels.Count;
            shoppingCartTotalAmount = shoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmountFormatted;
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
