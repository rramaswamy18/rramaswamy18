using ArchitectureLibraryBusinessLayer;
using ArchitectureLibraryCacheData;
using ArchitectureLibraryCreditCardModels;
using ArchitectureLibraryEnumerations;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using Newtonsoft.Json;
using RetailSlnBusinessLayer;
using RetailSlnCacheData;
using RetailSlnModels;
using RetailSlnWeb.ClassCode;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
        // GET: AddToCart1
        [AllowAnonymous]
        [HttpGet]
        public ActionResult AddToCart1(string itemId, string orderQty)
        {
            ViewData["ActionName"] = "AddToCart1";
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
                PaymentInfo1Model paymentInfoModel = (PaymentInfo1Model)Session["PaymentInfo"];
                List<ShoppingCartItemModel> shoppingCartItemModels = new List<ShoppingCartItemModel>
                {
                    new ShoppingCartItemModel
                    {
                        ItemId = long.Parse(itemId),
                        OrderQty = long.Parse(orderQty),
                    }
                };
                retailSlnBL.AddToCart(ref paymentInfoModel, shoppingCartItemModels, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                //long.TryParse(id, out long tempId);
                success = true;
                processMessage = "SUCCESS!!!";
                Session["PaymentInfo"] = paymentInfoModel;
                htmlString = ""; //archLibBL.ViewToHtmlString(this, "_ShoppingCart", paymentInfoModel.ShoppingCartModel);
                shoppingCartItemsCount = paymentInfoModel.ShoppingCartModel.ShoppingCartItems.Count;
                shoppingCartTotalAmount = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
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
        public ActionResult Checkout(string id)
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
                PaymentInfo1Model paymentInfoModel = (PaymentInfo1Model)Session["PaymentInfo"];
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
                    if (loggedIn && Session["SessionObject"] != null)
                    {
                        SessionObjectModel createForSessionObject;
                        if (long.TryParse(id, out long personId) && personId > 0)
                        {
                            createForSessionObject = archLibBL.BuildSessionObject(personId, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                        }
                        else
                        {
                            createForSessionObject = null;
                        }
                        Session["CreateForSessionObject"] = createForSessionObject;
                        retailSlnBL.DeliveryInfo(ref paymentInfoModel, (SessionObjectModel)Session["SessionObject"], false, true, clientId, ipAddress, execUniqueId, loggedInUserId);
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
                //ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                //ModelState.AddModelError("", "Checkout / GET");
                //actionResult = View("Error", responseObjectModel);
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
                    PaymentInfo1Model paymentInfoModel = (PaymentInfo1Model)Session["PaymentInfo"];
                    retailSlnBL.DeliveryInfo(ref paymentInfoModel, sessionObjectModel, false, true, clientId, ipAddress, execUniqueId, loggedInUserId);
                    success = true;
                    processMessage = "SUCCESS!!!";
                    htmlString = archLibBL.ViewToHtmlString(this, "_DeliveryInfo", paymentInfoModel);
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

        // POST: CheckoutValidate
        [AllowAnonymous]
        [HttpGet]
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
                PaymentInfo1Model paymentInfoModel = (PaymentInfo1Model)Session["PaymentInfo"];
                retailSlnBL.CheckoutValidate(paymentInfoModel, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                shoppingCartItemsCount = paymentInfoModel.ShoppingCartModel.ShoppingCartItems.Count;
                shoppingCartTotalAmount = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
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

        // POST: DeliveryInfo
        [Authorize]
        [AjaxAuthorize]
        [HttpPost]
        public ActionResult DeliveryInfo(PaymentInfo1Model paymentInfoModel)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            bool success;
            string processMessage, htmlString;
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            try
            {
                SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
                if (sessionObjectModel == null)
                {
                    success = false;
                    processMessage = "ERROR???";
                    htmlString = "";
                }
                else
                {
                    var paymentInfoModelTemp = ((PaymentInfo1Model)Session["PaymentInfo"]);
                    paymentInfoModel.CouponPaymentModel = paymentInfoModelTemp.CouponPaymentModel;
                    paymentInfoModel.GiftCertPaymentModel = paymentInfoModelTemp.GiftCertPaymentModel;
                    //paymentInfoModel.OrderSummaryModel = paymentInfoModelTemp.OrderSummaryModel;
                    paymentInfoModel.OrderSummaryModel.AspNetUserId = paymentInfoModelTemp.OrderSummaryModel.AspNetUserId;
                    paymentInfoModel.OrderSummaryModel.CorpAcctModel = paymentInfoModelTemp.OrderSummaryModel.CorpAcctModel;
                    paymentInfoModel.OrderSummaryModel.EmailAddress = paymentInfoModelTemp.OrderSummaryModel.EmailAddress;
                    paymentInfoModel.OrderSummaryModel.OrderHeaderId = paymentInfoModelTemp.OrderSummaryModel.OrderHeaderId;
                    paymentInfoModel.OrderSummaryModel.PersonId = paymentInfoModelTemp.OrderSummaryModel.PersonId;
                    paymentInfoModel.OrderSummaryModel.OrderHeaderId = paymentInfoModelTemp.OrderSummaryModel.OrderHeaderId;
                    paymentInfoModel.OrderSummaryModel.TelephoneCode = paymentInfoModelTemp.OrderSummaryModel.TelephoneCode;
                    paymentInfoModel.OrderSummaryModel.TelephoneCountryId = paymentInfoModelTemp.OrderSummaryModel.TelephoneCountryId;
                    paymentInfoModel.OrderSummaryModel.TelephoneNumber = paymentInfoModelTemp.OrderSummaryModel.TelephoneNumber;
                    paymentInfoModel.ShoppingCartModel = paymentInfoModelTemp.ShoppingCartModel;
                    paymentInfoModel.CreditCardDataModel = paymentInfoModelTemp.CreditCardDataModel;
                    retailSlnBL.BuildDeliveryInfoLookup(paymentInfoModel, sessionObjectModel, false, true, clientId, ipAddress, execUniqueId, loggedInUserId);
                    retailSlnBL.UpdateDeliveryAddressInfo(paymentInfoModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                    ModelState.Clear();
                    if (
                        string.IsNullOrWhiteSpace(paymentInfoModel.GiftCertPaymentModel.GiftCertNumber) &&
                        string.IsNullOrWhiteSpace(paymentInfoModel.GiftCertPaymentModel.GiftCertKey)
                       )
                    {
                        ;
                    }
                    else
                    {
                        if (long.TryParse(paymentInfoModel.GiftCertPaymentModel.GiftCertNumber, out long tempLong))
                        {
                            if (paymentInfoModel.GiftCertPaymentModel.GiftCertNumber.Length == 18 && paymentInfoModel.GiftCertPaymentModel.GiftCertKey.Length == 9)
                            {
                                ;
                            }
                            else
                            {
                                ModelState.AddModelError("GiftCertPaymentModel.GiftCertNumber", "Please enter valid Gift Cert#");
                                ModelState.AddModelError("GiftCertPaymentModel.GiftCertKey", "Please enter valid Gift Cert Key");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("GiftCertPaymentModel.GiftCertNumber", "Please enter valid Gift Cert#");
                        }
                    }
                    TryValidateModel(paymentInfoModel);
                    TryValidateModel(paymentInfoModel.DeliveryAddressModel, "DeliveryAddressModel");
                    TryValidateModel(paymentInfoModel.DeliveryDataModel, "DeliveryDataModel");
                    TryValidateModel(paymentInfoModel.DeliveryMethodModel, "DeliveryMethodModel");
                    TryValidateModel(paymentInfoModel.OrderSummaryModel, "OrderSummaryModel");
                    TryValidateModel(paymentInfoModel.PaymentModeModel, "PaymentModeModel");
                    if (paymentInfoModel.DeliveryAddressModel.DemogInfoCountryId != null && !string.IsNullOrWhiteSpace(paymentInfoModel.DeliveryAddressModel.ZipCode))
                    {
                        Regex regex = new Regex(DemogInfoCache.DemogInfoCountryModels.First(x => x.DemogInfoCountryId == paymentInfoModel.DeliveryAddressModel.DemogInfoCountryId.Value).PostalCodeRegEx);
                        if (!regex.IsMatch(paymentInfoModel.DeliveryAddressModel.ZipCode))
                        {
                            ModelState.AddModelError("DeliveryAddressModel.ZipCode", "Postal Code");
                        }
                    }
                    if (ModelState.IsValid)
                    {
                        retailSlnBL.DeliveryInfo(paymentInfoModel, sessionObjectModel, false, true, clientId, ipAddress, execUniqueId, loggedInUserId);
                        Session["PaymentInfo"] = paymentInfoModel;
                        if (paymentInfoModel.ResponseObjectModel.ResponseTypeId == ResponseTypeEnum.Success)
                        {
                        }
                        else
                        {
                            ModelState.AddModelError("", "Please fix errors to continue");
                        }
                    }
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
                        htmlString = archLibBL.ViewToHtmlString(this, "_DeliveryInfoData1", paymentInfoModel);
                    }
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                processMessage = "ERROR???";
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                paymentInfoModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ResponseTypeId = ResponseTypeEnum.Error,
                    ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                };
                htmlString = archLibBL.ViewToHtmlString(this, "_DeliveryInfoData1", paymentInfoModel);
            }
            actionResult = Json(new { success, processMessage, htmlString });
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
            #region
            ////Get the Address Info based on Zip and fill in the rest
            //SearchDataModel searchDataModel = new SearchDataModel
            //{
            //    SearchType = "ZipCode",
            //    SearchKeyValuePairs = new Dictionary<string, string>
            //    {
            //        { "DemogInfoCountryId", paymentInfoModel.DeliveryAddressModel.DemogInfoCountryId.ToString() },
            //        { "ZipCode", paymentInfoModel.DeliveryAddressModel.ZipCode },
            //    },
            //};
            //List<Dictionary<string, string>> sqlQueryResults = archLibBL.SearchData(searchDataModel, clientId, ipAddress, execUniqueId, loggedInUserId);
            //foreach (var sqlQueryResult in sqlQueryResults)
            //{
            //    if (
            //        sqlQueryResult["DemogInfoCountryId"] == paymentInfoModel.DeliveryAddressModel.DemogInfoCountryId.ToString()
            //        && sqlQueryResult["ZipCode"] == paymentInfoModel.DeliveryAddressModel.ZipCode
            //       )
            //    {
            //        paymentInfoModel.DeliveryAddressModel.CityName = sqlQueryResult["CityName"];
            //        paymentInfoModel.DeliveryAddressModel.CountryAbbrev = sqlQueryResult["CountryAbbrev"];
            //        paymentInfoModel.DeliveryAddressModel.CountryDesc = sqlQueryResult["CountryDesc"];
            //        paymentInfoModel.DeliveryAddressModel.CountyName = sqlQueryResult["CountyName"];
            //        paymentInfoModel.DeliveryAddressModel.DemogInfoCityId = long.Parse(sqlQueryResult["DemogInfoCityId"]);
            //        paymentInfoModel.DeliveryAddressModel.DemogInfoCountyId = long.Parse(sqlQueryResult["DemogInfoCountyId"]);
            //        paymentInfoModel.DeliveryAddressModel.DemogInfoSubDivisionId = long.Parse(sqlQueryResult["DemogInfoSubDivisionId"]);
            //        paymentInfoModel.DeliveryAddressModel.DemogInfoZipId = long.Parse(sqlQueryResult["DemogInfoZipId"]);
            //        paymentInfoModel.DeliveryAddressModel.DemogInfoZipPlusId = long.Parse(sqlQueryResult["DemogInfoZipPlusId"]);
            //        paymentInfoModel.DeliveryAddressModel.StateAbbrev = sqlQueryResult["StateAbbrev"];
            //    }
            //}

            //if (string.IsNullOrWhiteSpace(paymentInfoModel.DeliveryAddressModel.AddressLine1))
            //{
            //    ModelState.AddModelError("DeliveryAddressModel.AddressLine1", "Address line 1");
            //}
            //if (string.IsNullOrWhiteSpace(paymentInfoModel.DeliveryAddressModel.CityName))
            //{
            //    ModelState.AddModelError("DeliveryAddressModel.CityName", "City name");
            //}
            //if (string.IsNullOrWhiteSpace(paymentInfoModel.DeliveryAddressModel.ZipCode))
            //{
            //    ModelState.AddModelError("DeliveryAddressModel.ZipCode", "Postal Code");
            //}
            //if (paymentInfoModel.DeliveryAddressModel.DemogInfoSubDivisionId == null)
            //{
            //    ModelState.AddModelError("DeliveryAddressModel.DemogInfoSubDivisionId", "State");
            //}
            //if (paymentInfoModel.DeliveryAddressModel.DemogInfoCountryId == null)
            //{
            //    ModelState.AddModelError("DeliveryAddressModel.DemogInfoCountryId", "Country");
            //}
            //try
            //{
            //    ModelState.Clear();
            //    TryValidateModel(paymentInfoModel);
            //    TryValidateModel(paymentInfoModel.DeliveryAddressModel, "DeliveryAddressModel");
            //        if (string.IsNullOrWhiteSpace(paymentInfoModel.DeliveryAddressModel.AddressLine1))
            //        {
            //            ModelState.AddModelError("DeliveryAddressModel.AddressLine1", "Address line 1");
            //        }
            //        if (string.IsNullOrWhiteSpace(paymentInfoModel.DeliveryAddressModel.CityName))
            //        {
            //            ModelState.AddModelError("DeliveryAddressModel.CityName", "City name");
            //        }
            //        if (string.IsNullOrWhiteSpace(paymentInfoModel.DeliveryAddressModel.ZipCode))
            //        {
            //            ModelState.AddModelError("DeliveryAddressModel.ZipCode", "Postal Code");
            //        }
            //        if (paymentInfoModel.DeliveryAddressModel.DemogInfoSubDivisionId == null)
            //        {
            //            ModelState.AddModelError("DeliveryAddressModel.DemogInfoSubDivisionId", "State");
            //        }
            //        if (paymentInfoModel.DeliveryAddressModel.DemogInfoCountryId == null)
            //        {
            //            ModelState.AddModelError("DeliveryAddressModel.DemogInfoCountryId", "Country");
            //        }
            //        if (paymentInfoModel.DeliveryAddressModel.DemogInfoCountryId != null && !string.IsNullOrWhiteSpace(paymentInfoModel.DeliveryAddressModel.ZipCode))
            //        {
            //            Regex regex = new Regex(DemogInfoCache.DemogInfoCountryModels.First(x => x.DemogInfoCountryId == paymentInfoModel.DeliveryAddressModel.DemogInfoCountryId.Value).PostalCodeRegEx);
            //            if (!regex.IsMatch(paymentInfoModel.DeliveryAddressModel.ZipCode))
            //            {
            //                ModelState.AddModelError("DeliveryAddressModel.ZipCode", "Postal Code");
            //            }
            //        }
            //    if (ModelState.IsValid)
            //    {
            //        //Get the Address Info based on Zip and fill in the rest
            //        SearchDataModel searchDataModel = new SearchDataModel
            //        {
            //            SearchType = "ZipCode",
            //            SearchKeyValuePairs = new Dictionary<string, string>
            //                {
            //                    { "DemogInfoCountryId", paymentInfoModel.DeliveryAddressModel.DemogInfoCountryId.ToString() },
            //                    { "ZipCode", paymentInfoModel.DeliveryAddressModel.ZipCode },
            //                },
            //        };
            //        List<Dictionary<string, string>> sqlQueryResults = archLibBL.SearchData(searchDataModel, clientId, ipAddress, execUniqueId, loggedInUserId);
            //        foreach (var sqlQueryResult in sqlQueryResults)
            //        {
            //            if (
            //                sqlQueryResult["DemogInfoCountryId"] == paymentInfoModel.DeliveryAddressModel.DemogInfoCountryId.ToString()
            //                && sqlQueryResult["ZipCode"] == paymentInfoModel.DeliveryAddressModel.ZipCode
            //               )
            //            {
            //                paymentInfoModel.DeliveryAddressModel.CityName = sqlQueryResult["CityName"];
            //                paymentInfoModel.DeliveryAddressModel.CountryAbbrev = sqlQueryResult["CountryAbbrev"];
            //                paymentInfoModel.DeliveryAddressModel.CountryDesc = sqlQueryResult["CountryDesc"];
            //                paymentInfoModel.DeliveryAddressModel.CountyName = sqlQueryResult["CountyName"];
            //                paymentInfoModel.DeliveryAddressModel.DemogInfoCityId = long.Parse(sqlQueryResult["DemogInfoCityId"]);
            //                paymentInfoModel.DeliveryAddressModel.DemogInfoCountyId = long.Parse(sqlQueryResult["DemogInfoCountyId"]);
            //                paymentInfoModel.DeliveryAddressModel.DemogInfoSubDivisionId = long.Parse(sqlQueryResult["DemogInfoSubDivisionId"]);
            //                paymentInfoModel.DeliveryAddressModel.DemogInfoZipId = long.Parse(sqlQueryResult["DemogInfoZipId"]);
            //                paymentInfoModel.DeliveryAddressModel.DemogInfoZipPlusId = long.Parse(sqlQueryResult["DemogInfoZipPlusId"]);
            //                paymentInfoModel.DeliveryAddressModel.StateAbbrev = sqlQueryResult["StateAbbrev"];
            //            }
            //        }
            //    }
            //    if (ModelState.IsValid)
            //    {
            //        PaymentInfoModel paymentInfoModel = retailSlnBL.DeliveryInfo(null, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
            //        if (ModelState.IsValid)
            //        {
            //            success = true;
            //            processMessage = "SUCCESS!!!";
            //            htmlString = archLibBL.ViewToHtmlString(this, "_PaymentInfo", paymentInfoModel);
            //            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
            //        }
            //        else
            //        {
            //            success = false;
            //            processMessage = "ERROR???";
            //            htmlString = archLibBL.ViewToHtmlString(this, "_DeliveryInfoData", paymentInfoModel);
            //            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: Payment BL Error");
            //        }
            //    }
            //    else
            //    {
            //        success = false;
            //        processMessage = "ERROR???";
            //        htmlString = archLibBL.ViewToHtmlString(this, "_DeliveryInfoData", paymentInfoModel);
            //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: Model Validation Error");
            //    }
            //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            //}
            //catch (Exception exception)
            //{
            //    exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
            //    success = false;
            //    processMessage = "ERROR???";
            //    archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
            //    paymentInfoModel.ResponseObjectModel = new ResponseObjectModel
            //    {
            //        ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
            //    };
            //    htmlString = archLibBL.ViewToHtmlString(this, "_DeliveryInfoData", paymentInfoModel);
            //}
            #endregion
        }

        // GET : ItemAttributes
        [AllowAnonymous]
        [HttpGet]
        [Route("ItemAttributes")]
        public ActionResult ItemAttributes(string id, string tabId)
        {
            ViewData["ActionName"] = "itemAttribsView";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            try
            {
                //int x = 1, y = 0, z = x / y;
                long itemMasterId = long.Parse(id);
                ItemMasterAttributesModel itemMasterAttributesModel = retailSlnBL.ItemMasterAttributes(itemMasterId, long.Parse(tabId), clientId, ipAddress, execUniqueId, loggedInUserId);
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

        // GET : ItemBundleItemData
        [AllowAnonymous]
        [HttpGet]
        public ActionResult ItemBundleData(string id)
        {
            ViewData["ActionName"] = "ItemBundleData";
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
                ItemBundleDataModel itemBundleDataModel = retailSlnBL.ItemBundleData(long.Parse(id), clientId, ipAddress, execUniqueId, loggedInUserId);
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

        // GET: OrderCategoryItem
        [AllowAnonymous]
        [HttpGet]
        public ActionResult OrderCategoryItem(string id, string pageNum)
        {
            //int x = 1, y = 0, z = x / y;
            ViewData["ActionName"] = "OrderCategoryItemData";
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
                SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
                string aspNetRoleName;
                if (sessionObjectModel == null)
                {
                    aspNetRoleName = "DEFAULTROLE";
                }
                else
                {
                    aspNetRoleName = sessionObjectModel.AspNetRoleName;
                }
                string viewName;
                switch (aspNetRoleName)
                {
                    case "BULKORDERSROLE":
                    case "MARKETINGROLE":
                    case "WHOLESALEROLE":
                        viewName = "_OrderCategoryItemList";
                        break;
                    default:
                        viewName = "_OrderCategoryItem";
                        break;
                }
                var orderCategoryItemModel = retailSlnBL.OrderCategoryItem(aspNetRoleName, id, pageNum, "45", Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                htmlString = archLibBL.ViewToHtmlString(this, viewName, orderCategoryItemModel);
                success = true;
                processMessage = "SUCCESS!!!";
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

        // POST: PaymentInfo1
        [AjaxAuthorize]
        [Authorize]
        [HttpPost]
        public ActionResult PaymentInfo1()
        {//Credit Sale
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            success = true;
            processMessage = "SUCCESS!!!";
            PaymentInfo1Model paymentInfoModel = (PaymentInfo1Model)Session["PaymentInfo"];
            SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
            htmlString = retailSlnBL.PaymentInfo1(paymentInfoModel, sessionObjectModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
            actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            FormsAuthentication.SignOut();
            Session.Abandon();
            Request.GetOwinContext().Authentication.SignOut();
            Session["SessionObject"] = null;
            Session["PaymentInfo"] = null;
            Session.Abandon();
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
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
            bool success;
            string processMessage, htmlString;
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            try
            {
                SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
                if (sessionObjectModel == null)
                {
                    success = false;
                    processMessage = "ERROR???";
                    htmlString = "";
                }
                else
                {
                    PaymentInfo1Model paymentInfoModel = ((PaymentInfo1Model)Session["PaymentInfo"]);
                    RazorPayResponse razorPayResponse = retailSlnBL.PaymentInfo2(paymentInfoModel, sessionObjectModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                    if (razorPayResponse == null)
                    {
                        success = false;
                        processMessage = "ERROR???";
                        htmlString = "";
                    }
                    else
                    {
                        success = true;
                        processMessage = "SUCCESS!!!";
                        htmlString = JsonConvert.SerializeObject(razorPayResponse);
                    }
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                processMessage = "ERROR???";
                htmlString = "";
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
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            PaymentInfo1Model paymentInfoModel = (PaymentInfo1Model)Session["PaymentInfo"];
            SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
            string htmlString = retailSlnBL.PaymentInfo3(paymentInfoModel, sessionObjectModel, razorpay_payment_id, razorpay_order_id, razorpay_signature, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
            ArchLibBL archLibBL = new ArchLibBL();
            ActionResult actionResult = View("PaymentInfo3", paymentInfoModel);
            FormsAuthentication.SignOut();
            Session.Abandon();
            Request.GetOwinContext().Authentication.SignOut();
            Session["SessionObject"] = null;
            Session["PaymentInfo"] = null;
            Session.Abandon();
            return actionResult;
        }

        // GET: PaymentInfo4
        [AjaxAuthorize]
        [Authorize]
        [HttpGet]
        public ActionResult PaymentInfo4()
        {//Credit Card Payment Setup
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
                PaymentInfo1Model paymentInfoModel = (PaymentInfo1Model)Session["PaymentInfo"];
                paymentInfoModel.CreditCardProcessModel = new CreditCardProcessModel
                {
                    CreditCardAmount = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDue.Value,
                    CreditCardAmountFormatted = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.BalanceDueFormatted,
                    //CreditCardNumber = "4111111111111111",
                    //CVV = "123",
                    //CardExpiryMM = "09",
                    //CardExpiryYYYY = "2025",
                    //CardHolderName = "John Miller",
                };
                htmlString = archLibBL.ViewToHtmlString(this, "_PaymentInfo4", paymentInfoModel);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                processMessage = "ERROR???";
                htmlString = "Error while setting up credit card";
            }
            actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        // POST: PaymentInfo5
        [AjaxAuthorize]
        [Authorize]
        [HttpPost]
        public ActionResult PaymentInfo5(CreditCardProcessModel creditCardProcessModel)
        {//Credit Card Payment Process
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
                SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
                if (sessionObjectModel == null)
                {
                    string aspNetRoleName, absoluteUri = Request.Url.AbsoluteUri;
                    success = false;
                    processMessage = "ERROR???";
                    aspNetRoleName = "DEFAULTROLE";
                    var aspNetRoleKVPs = ArchLibCache.AspNetRoleKVPs[aspNetRoleName];
                    var parentCategoryId = long.Parse(aspNetRoleKVPs["ParentCategoryId"].KVPValueData);
                    CategoryModel categoryModel = RetailSlnCache.CategoryModels.First(x => x.CategoryId == parentCategoryId);
                    categoryModel = RetailSlnCache.CategoryModels.First(x => x.CategoryId == parentCategoryId);
                    OrderCategoryItemModel orderCategoryItemModel = new OrderCategoryItemModel
                    {
                        ParentCategoryId = parentCategoryId,
                        PageNum = 1,
                        PageSize = categoryModel.MaxPerPage, //int.TryParse(pageSize, out tempLong) ? int.Parse(pageSize) : 50,
                    };
                    htmlString = archLibBL.ViewToHtmlString(this, categoryModel.ViewName, orderCategoryItemModel);
                }
                else
                {
                    PaymentInfo1Model paymentInfoModel = (PaymentInfo1Model)Session["PaymentInfo"];
                    paymentInfoModel.CreditCardProcessModel = creditCardProcessModel;
                    ModelState.Clear();
                    bool modelValidation = TryValidateModel(creditCardProcessModel);
                    if (modelValidation)
                    {
                        htmlString = retailSlnBL.PaymentInfo5(paymentInfoModel, sessionObjectModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                        if (htmlString != null)
                        {
                            success = true;
                            processMessage = "SUCCESS!!!";
                        }
                        else
                        {
                            success = false;
                            processMessage = "ERROR???";
                        }
                    }
                    else
                    {
                        success = false;
                        processMessage = "ERROR???";
                        paymentInfoModel.ResponseObjectModel = new ResponseObjectModel
                        {
                            ResponseTypeId = ResponseTypeEnum.Error,
                            ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                        };
                        htmlString = archLibBL.ViewToHtmlString(this, "_PaymentInfo4", paymentInfoModel);
                    }
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                processMessage = "ERROR???";
                htmlString = "Error while processing credit card";
            }
            actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
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
                PaymentInfo1Model paymentInfoModel = (PaymentInfo1Model)Session["PaymentInfo"];
                retailSlnBL.RemoveFromCart(paymentInfoModel, removeFromCartModel.RemoveFromCartIndex.Value, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                success = true;
                processMessage = "SUCCESS!!!";
                shoppingCartItemsCount = paymentInfoModel.ShoppingCartModel.ShoppingCartItems.Count;
                shoppingCartTotalAmount = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                htmlString = archLibBL.ViewToHtmlString(this, "_ShoppingCart", paymentInfoModel.ShoppingCartModel);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                processMessage = "ERROR???";
                htmlString = "Error while removing item to cart";
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            actionResult = Json(new { success, processMessage, htmlString, shoppingCartItemsCount, shoppingCartTotalAmount }, JsonRequestBehavior.AllowGet);
            return actionResult;
        }

        // GET: SearchForEmailAddress
        [AllowAnonymous]
        [HttpGet]
        //[Route("SearchForEmailAddress")]
        public ActionResult SearchForEmailAddress(string id)
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
                var searchForEmailAddressModel = retailSlnBL.SearchForEmailAddress(id, clientId, ipAddress, execUniqueId, loggedInUserId);
                success = true;
                processMessage = "SUCCESS!!!";
                htmlString = archLibBL.ViewToHtmlString(this, "_SearchForEmailAddress", searchForEmailAddressModel);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                processMessage = "ERROR???";
                htmlString = "Error while searching " + id;
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            return actionResult;
        }

        // GET: SearchResult
        [AllowAnonymous]
        [HttpGet]
        //[Route("SearchResult")]
        public ActionResult SearchResult(string id)
        {
            ViewData["ActionName"] = "RemoveFromCart";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            //ActionResult actionResult;
            //bool success;
            //string processMessage, htmlString;
            //List<long> searchIds = new List<long> { 0, 9, 18 };
            //List<ItemModel> searchItems = RetailSlnCache.ItemModels.Where(x => searchIds.Contains(x.ItemId.Value)).ToList();
            try
            {
                var searchResultModel = retailSlnBL.SearchResult(id, clientId, ipAddress, execUniqueId, loggedInUserId);
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

        // GET: ShoppingCartSummary
        [AllowAnonymous]
        [HttpGet]
        public ActionResult ShoppingCart()
        {
            ViewData["ActionName"] = "ShoppingCart";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            var paymentInfoModel = (PaymentInfo1Model)Session["PaymentInfo"];
            int shoppingCartItemsCount = 0;
            string shoppingCartTotalAmount = 0f.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
            paymentInfoModel = paymentInfoModel ?? new PaymentInfo1Model
            {
                ShoppingCartModel = new ShoppingCartModel
                {
                    ShoppingCartItems = new List<ShoppingCartItemModel>(),
                    ShoppingCartSummaryModel = new ShoppingCartSummaryModel
                    {
                        TotalOrderAmount = 0,
                    },
                },
            };
            shoppingCartItemsCount = paymentInfoModel.ShoppingCartModel.ShoppingCartItems.Count;
            shoppingCartTotalAmount = paymentInfoModel.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
            paymentInfoModel.ShoppingCartModel = paymentInfoModel.ShoppingCartModel ?? new ShoppingCartModel();
            paymentInfoModel.ShoppingCartModel.Checkout = true;
            success = true;
            processMessage = "SUCCESS!!!";
            htmlString = archLibBL.ViewToHtmlString(this, "_ShoppingCart2", paymentInfoModel.ShoppingCartModel);
            actionResult = Json(new { success, processMessage, htmlString, shoppingCartItemsCount, shoppingCartTotalAmount }, JsonRequestBehavior.AllowGet);
            return actionResult;
        }

        // GET: ShoppingCartComments
        [AllowAnonymous]
        [HttpGet]
        public ActionResult ShoppingCartComments(int index, string orderComments)
        {
            ViewData["ActionName"] = "ShoppingCartComments";
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
                PaymentInfo1Model paymentInfoModel = (PaymentInfo1Model)Session["PaymentInfo"];
                retailSlnBL.ShoppingCartComments(paymentInfoModel, index, orderComments, clientId, ipAddress, execUniqueId, loggedInUserId);
                success = true;
                processMessage = "SUCCESS!!!";
                htmlString = archLibBL.ViewToHtmlString(this, "_ShoppingCart", paymentInfoModel.ShoppingCartModel);
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

        // GET: ShoppingCartSummary
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
            PaymentInfo1Model paymentInfo1Model = ((PaymentInfo1Model)Session["PaymentInfo"]);
            paymentInfo1Model = paymentInfo1Model ?? new PaymentInfo1Model
            {
                ShoppingCartModel = new ShoppingCartModel
                {
                    ShoppingCartItems = new List<ShoppingCartItemModel>(),
                    ShoppingCartSummaryModel = new ShoppingCartSummaryModel
                    {
                        TotalOrderAmount = 0,
                    },
                },
            };
            success = true;
            processMessage = "SUCCESS!!!";
            htmlString = "";
            shoppingCartItemsCount = paymentInfo1Model.ShoppingCartModel.ShoppingCartItems.Count;
            shoppingCartTotalAmount = paymentInfo1Model.ShoppingCartModel.ShoppingCartSummaryModel.TotalOrderAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
            actionResult = Json(new { success, processMessage, htmlString, shoppingCartItemsCount, shoppingCartTotalAmount }, JsonRequestBehavior.AllowGet);
            return actionResult;
        }
    }
}
