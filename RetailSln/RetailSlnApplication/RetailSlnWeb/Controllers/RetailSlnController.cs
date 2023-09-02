using ArchitectureLibraryBusinessLayer;
using ArchitectureLibraryClassCode;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using RetailSlnBusinessLayer;
using RetailSlnCacheData;
using RetailSlnModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace RetailSlnWeb.Controllers
{
    public partial class HomeController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public ActionResult AddToCart(string itemId, string orderQty)
        {
            //int x = 1, y = 0, z = x / y;
            ViewData["ActionName"] = "AddToCart";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            //ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            try
            {
                //int x = 1, y = 0, z = x / y;
                ShoppingCartModel shoppingCartModel = retailSlnBL.AddToCart(long.Parse(itemId), long.Parse(orderQty), Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = Json(new { shoppingCartItemsCount = shoppingCartModel.ShoppingCartItems.Count, shoppingCartTotalAmount = shoppingCartModel.ShoppingCartTotalAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "") }, JsonRequestBehavior.AllowGet);
                Response.StatusCode = (int)HttpStatusCode.OK;
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                actionResult = Json(new { errorMessage = "Error while adding item to cart" }, JsonRequestBehavior.AllowGet);
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
                actionResult = View("Checkout", checkoutModel);
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

        [AjaxAuthorize]
        [Authorize]
        [HttpPost]
        public ActionResult DeliveryInfo(DeliveryInfoDataModel deliveryInfoDataModel)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            try
            {
                ModelState.Clear();
                TryValidateModel(deliveryInfoDataModel);
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
                    PaymentModel paymentModel = retailSlnBL.Payment(Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    actionResult = PartialView("_Payment", paymentModel);
                    Response.StatusCode = (int)HttpStatusCode.OK;
                }
                else
                {
                    actionResult = PartialView("_DeliveryInfoData", deliveryInfoDataModel);
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                actionResult = Json(new { errorMessage = "Error ocurred while checking out" }, JsonRequestBehavior.AllowGet);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            return actionResult;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("OrderCategoryItem")]
        public ActionResult OrderCategoryItem(string id)
        {
            //int x = 1, y = 0, z = x / y;
            ViewData["ActionName"] = "OrderCategoryItem";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
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
            try
            {
                paymentDataModel.ResponseObjectModel = new ResponseObjectModel();
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
                    ModelState.AddModelError("", "Enter Gift Cert or Credit Card Info");
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
                        OrderReceiptModel orderReceiptModel = retailSlnBL.OrderReceipt(paymentDataModel, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                        Session["ShoppingCartModel"] = null;
                        Session["DeliveryInfoModel"] = null;
                        Response.StatusCode = (int)HttpStatusCode.OK;
                        actionResult = PartialView("_OrderReceipt", orderReceiptModel);
                    }
                    else
                    {
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00003000 :: BL Process Error");
                        Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        paymentDataModel.ResponseObjectModel.ValidationSummaryMessage = "PLEASE FIX ERRORS TO CONTINUE???";
                        actionResult = PartialView("_PaymentData", paymentDataModel);
                    }
                }
                else
                {
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00004000 :: Payment Data Model Validation Failed");
                    archLibBL.MergeModelStateErrorMessages(ModelState);
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    paymentDataModel.ResponseObjectModel.ValidationSummaryMessage = "PLEASE FIX ERRORS TO CONTINUE???";
                    actionResult = PartialView("_PaymentData", paymentDataModel);
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                paymentDataModel.ResponseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                paymentDataModel.ResponseObjectModel.ValidationSummaryMessage = "PLEASE FIX ERRORS TO CONTINUE???";
                archLibBL.MergeModelStateErrorMessages(ModelState);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                actionResult = PartialView("_PaymentData", paymentDataModel);
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
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
    }
}
