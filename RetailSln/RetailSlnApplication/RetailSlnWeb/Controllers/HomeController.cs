using ArchitectureLibraryBusinessLayer;
using ArchitectureLibraryCacheData;
using ArchitectureLibraryEnumerations;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using Microsoft.Owin.BuilderProperties;
using RetailSlnBusinessLayer;
using RetailSlnCacheData;
using RetailSlnDataLayer;
using RetailSlnModels;
using RetailSlnWeb.ClassCode;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Security;

namespace RetailSlnWeb.Controllers
{
    [OutputCache(Duration = 0, NoStore = true, VaryByParam = "*")]
    public partial class HomeController : Controller
    {
        private readonly long clientId = long.Parse(Utilities.GetApplicationValue("ClientId"));
        private readonly string execUniqueId = Utilities.CreateExecUniqueId();
        private readonly string lastIpAddress = Utilities.GetLastIPAddress();

        // GET: Index
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index(string id)
        {
            //int x = 1, y = 0, z = x / y;
            //Session.Timeout = 2;
            ViewData["ActionName"] = "Index";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request, lastIpAddress, ArchLibCache.IpInfoClientAccessToken), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                string parentCategoryIdParm, pageNumParm;
                if (Session["LastVisitedParentCategoryId"] != null && long.TryParse(Session["LastVisitedParentCategoryId"].ToString(), out long parentCategoryIdTemp))
                {
                    parentCategoryIdParm = parentCategoryIdTemp.ToString();
                }
                else
                {
                    parentCategoryIdParm = null;
                }
                Session["LastVisitedParentCategoryId"] = parentCategoryIdParm;
                if (Session["LastVisitedPageNum"] != null && long.TryParse(Session["LastVisitedPageNum"].ToString(), out long pageNumTemp))
                {
                    pageNumParm = pageNumTemp.ToString();
                }
                else
                {
                    pageNumParm = null;
                }
                Session["LastVisitedPageNum"] = pageNumParm;
                string aspNetRoleName;
                SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
                SessionObjectModel createForSessionObject = (SessionObjectModel)Session["CreateForSessionObject"];
                if (sessionObjectModel == null)
                {
                    string absoluteUri = Request.Url.AbsoluteUri;
                    if (
                        absoluteUri.ToUpper().IndexOf("BULKORDER") > -1 || id?.ToUpper().IndexOf("BULKORDER") > -1 ||
                        absoluteUri.ToUpper().IndexOf("MARKETING") > -1 || id?.ToUpper().IndexOf("MARKETING") > -1 ||
                        absoluteUri.ToUpper().IndexOf("WHOLESALE") > -1 || id?.ToUpper().IndexOf("WHOLESALE") > -1
                       )
                    {
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                        return RedirectToAction("LoginUserProf");
                    }
                    if (absoluteUri.ToUpper().IndexOf("PUROHIT") > -1 || id?.ToUpper().IndexOf("PUROHIT") > -1)
                    {
                        aspNetRoleName = "PRIESTROLE";
                    }
                    else
                    {
                        aspNetRoleName = "DEFAULTROLE";
                    }
                }
                else
                {
                    aspNetRoleName = sessionObjectModel.AspNetRoleName;
                }
                switch (aspNetRoleName)
                {
                    case "APPLADMN1":
                    case "MARKETINGROLE":
                    case "SYSTADMIN":
                        actionResult = RedirectToAction("Index", "Dashboard");
                        break;
                    case "PRIESTROLE":
                        OrderItemModel orderItemModel1 = retailSlnBL.OrderItem(aspNetRoleName, parentCategoryIdParm, pageNumParm, null, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                        actionResult = View("Index", orderItemModel1);
                        break;
                    default:
                        OrderItemModel orderItemModel = retailSlnBL.OrderItem(aspNetRoleName, parentCategoryIdParm, pageNumParm, null, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                        actionResult = View("Index", orderItemModel);
                        break;
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                ModelState.AddModelError("", "Index / GET");
                archLibBL.CopyReponseObjectToModelErrors(ModelState, null, responseObjectModel.ResponseMessages);
                actionResult = View("Error", responseObjectModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        // GET: AboutUs
        [AllowAnonymous]
        [HttpGet]
        [Route("AboutUs")]
        public ActionResult AboutUs()
        {
            ViewData["ActionName"] = "AboutUs";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                AboutUsModel AboutUsModel = archLibBL.AboutUs();
                actionResult = View("AboutUs", AboutUsModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                ModelState.AddModelError("", "Temple Festivals / GET");
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        // GET: CheckIsAuthenticated
        [HttpGet]
        public JsonResult CheckIsAuthenticated(string id)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            //System.Threading.Thread.Sleep(5000); //Sleep for 2 seconds to make sure session has timedout
            var sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
            string aspNetUserId;
            if (sessionObjectModel != null)
            {
                aspNetUserId = sessionObjectModel.AspNetUserId;
            }
            else
            {
                aspNetUserId = string.Empty;
            }
            var isAuthenticated = User.Identity.IsAuthenticated;
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: IsAuthenticatedStatus", "isAuthenticated", isAuthenticated.ToString(), "aspNetUserId", aspNetUserId);
            //var isAuthenticated = User.Identity.IsAuthenticated && sessionObjectModel != null;
            isAuthenticated = isAuthenticated && sessionObjectModel != null;
            if (isAuthenticated)
            {
                ;
            }
            else
            {
                if (id == "1")
                {
                    FormsAuthentication.SignOut();
                    Session.Abandon();
                    Request.GetOwinContext().Authentication.SignOut();
                    Session["SessionObject"] = null;
                    Session.Abandon();
                }
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return Json(new { isAuthenticated }, JsonRequestBehavior.AllowGet);
        }

        // GET: CookiePolicy
        [AllowAnonymous]
        [HttpGet]
        [Route("CookiePolicy")]
        public ActionResult CookiePolicy()
        {
            ViewData["ActionName"] = "CookiePolicy";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                CookiePolicyModel cookiePolicyModel = archLibBL.CookiePolicy(this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("CookiePolicy", cookiePolicyModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        // GET: ContactUs
        [AllowAnonymous]
        [HttpGet]
        [Route("ContactUs")]
        public ActionResult ContactUs()
        {
            ViewData["ActionName"] = "ContactUs";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                ContactUsModel contactUsModel = archLibBL.ContactUs(this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("ContactUs", contactUsModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                ModelState.AddModelError("", "Update Password / GET");
                archLibBL.CopyReponseObjectToModelErrors(ModelState, null, responseObjectModel.ResponseMessages);
                Session["CaptchaAnswerContactUs"] = null;
                Session["CaptchaNumberContactUs0"] = null;
                Session["CaptchaNumberContactUs1"] = null;
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        // POST: ContactUs
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ContactUs(ContactUsModel contactUsModel)
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
                TryValidateModel(contactUsModel);
                archLibBL.ContactUs(ref contactUsModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
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
                archLibBL.GenerateCaptchaQuesion(Session, "CaptchaNumberContactUs0", "CaptchaNumberContactUs1");
                contactUsModel.CaptchaAnswerContactUs = null;
                contactUsModel.CaptchaNumberContactUs0 = Session["CaptchaNumberContactUs0"].ToString();
                contactUsModel.CaptchaNumberContactUs1 = Session["CaptchaNumberContactUs1"].ToString();
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                contactUsModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                };
                success = false;
                processMessage = "ERROR???";
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00099100 :: Error Exit");
            }
            htmlString = archLibBL.ViewToHtmlString(this, "_ContactUsData", contactUsModel);
            actionResult = Json(new { success, processMessage, htmlString });
            //actionResult = PartialView("_ContactUsData", contactUsModel);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        // GET: Error
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }

        // GET: Error404
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Error404(string id)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            string execUniqueId = Utilities.CreateExecUniqueId();
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            Session["RequestUrl"] = Request.Url.AbsoluteUri;
            ViewResult viewResult;
            Exception exception;
            if (Request.Url.AbsoluteUri.IndexOf("job_board_news") > -1)
            {
                exception = new Exception("SEO URL Not Found");
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00001000 :: 404 - SEOError - " + Request.HttpMethod + " - " + Request.Url.AbsoluteUri, exception);
                viewResult = View("SEOPage1");
            }
            else
            {
                exception = new Exception("Other URL Not Found");
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00001000 :: 404 - OtherError - " + Request.HttpMethod + " - " + Request.Url.AbsoluteUri, exception);
                viewResult = View();
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return viewResult;
        }

        // GET: FAQs
        [AllowAnonymous]
        [HttpGet]
        [Route("FAQs")]
        public ActionResult FAQs()
        {
            ViewData["ActionName"] = "FAQs";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                FAQsModel fAQsModel = archLibBL.FAQs(this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("FAQs", fAQsModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "FAQs / GET");
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        // GET: Forbidden
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Forbidden()
        {
            return View();
            //string url = "/Home/Forbidden";
            //return JavaScript(string.Format("window.open('{0}', '_blank', 'left=100,top=100,width=500,height=500,toolbar=no,resizable=no,scrollable=yes');", url));
        }

        // GET: Home
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Home()
        {
            ViewData["ActionName"] = "Index";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                actionResult = View("Home");
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "Home / GET");
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error");
            }
            return actionResult;
        }

        // GET: Logout
        [AllowAnonymous]
        [HttpGet]
        [Route("Logout")]
        public ActionResult Logout()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            FormsAuthentication.SignOut();
            Session.Abandon();
            Request.GetOwinContext().Authentication.SignOut();
            Session["SessionObject"] = null;
            Session["PaymentInfo"] = null;
            Session.Abandon();
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return RedirectToAction("Index");
        }

        // GET: LoginUserProf
        [AllowAnonymous]
        [HttpGet]
        [Route("LoginUserProf")]
        public ActionResult LoginUserProf()
        {
            ViewData["ActionName"] = "LoginUserProf";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                LoginUserProfModel loginUserProfModel = archLibBL.LoginUserProf(this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                archLibBL.GenerateCaptchaQuesion(Session, "CaptchaNumberLogin0", "CaptchaNumberLogin1");
                loginUserProfModel.CaptchaAnswerLogin = null;
                loginUserProfModel.CaptchaNumberLogin0 = Session["CaptchaNumberLogin0"].ToString();
                loginUserProfModel.CaptchaNumberLogin1 = Session["CaptchaNumberLogin1"].ToString();
                actionResult = View("LoginUserProf", loginUserProfModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        // POST: LoginUserProf
        [AllowAnonymous]
        [HttpPost]
        public ActionResult LoginUserProf(LoginUserProfModel loginUserProfModel)
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
                ModelState.Clear();
                TryValidateModel(loginUserProfModel);
                string currentLoggedInUserId = loggedInUserId;
                SessionObjectModel sessionObjectModel = archLibBL.LoginUserProf(ref loginUserProfModel, true, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (ModelState.IsValid)
                {
                    var redirectUrl = LoginUserProfProcess(currentLoggedInUserId, sessionObjectModel);
                    success = true;
                    processMessage = "SUCCESS!!!";
                    actionResult = Json(new { success, processMessage, redirectUrl });
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
                }
                else
                {
                    loginUserProfModel.ResponseObjectModel = new ResponseObjectModel
                    {
                        ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                    };
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
                loginUserProfModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                };
                htmlString = archLibBL.ViewToHtmlString(this, "_LoginUserProfData", loginUserProfModel);
                success = false;
                processMessage = "ERROR???";
                actionResult = Json(new { success, processMessage, htmlString });
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00099100 :: Error Exit");
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        // GET: OTP
        [AllowAnonymous]
        [HttpGet]
        public ActionResult OTP(string id, string emailAddress)
        {
            ViewData["ActionName"] = "OTP";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            bool success;
            string processMessage, htmlString;
            string oTPExpiryDate, oTPExpiryTime, oTPExpiryDuration;
            try
            {
                OTPSendTypeEnum oTPSendTypeId;
                try
                {
                    ModelState.Clear();
                    oTPSendTypeId = (OTPSendTypeEnum)long.Parse(id);
                    OTPModel oTPModel = archLibBL.OTP(id, emailAddress, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    success = ModelState.IsValid;
                    if (success)
                    {
                        processMessage = "SUCCESS!!!";
                        htmlString = "OTP generated successfully";
                        oTPExpiryDate = oTPModel.OTPExpiryDate;
                        oTPExpiryTime = oTPModel.OTPExpiryTime;
                        oTPExpiryDuration = oTPModel.OTPExpiryDuration.ToString();
                    }
                    else
                    {
                        processMessage = "ERROR???";
                        htmlString = "Error occurred while generating OTP";
                        oTPExpiryDate = "Error";
                        oTPExpiryTime = "Error";
                        oTPExpiryDuration = "Error";
                    }
                }
                catch (Exception exception)
                {
                    exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                    success = false;
                    processMessage = "ERROR???";
                    htmlString = "Error while generating OTP";
                    oTPExpiryDate = "Error";
                    oTPExpiryTime = "Error";
                    oTPExpiryDuration = "Error";
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                processMessage = "ERROR???";
                htmlString = "Error while generating OTP";
                oTPExpiryDate = "";
                oTPExpiryTime = "";
                oTPExpiryDuration = "";
            }
            actionResult = Json(new { success, processMessage, htmlString, oTPExpiryDate, oTPExpiryTime, oTPExpiryDuration }, JsonRequestBehavior.AllowGet);
            //Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
            return actionResult;
            //string url = "/Home/Forbidden";
            //return JavaScript(string.Format("window.open('{0}', '_blank', 'left=100,top=100,width=500,height=500,toolbar=no,resizable=no,scrollable=yes');", url));
        }

        // GET: PicGallery
        [AllowAnonymous]
        [HttpGet]
        [Route("PicGallery")]
        public ActionResult PicGallery()
        {
            ViewData["ActionName"] = "PicGallery";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                PicGalleryModel picGalleryModel = archLibBL.PicGallery(this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("PicGallery", picGalleryModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "PicGallery / GET");
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        // GET: PrivacyPolicy
        [AllowAnonymous]
        [HttpGet]
        [Route("PrivacyPolicy")]
        public ActionResult PrivacyPolicy()
        {
            ViewData["ActionName"] = "PrivacyPolicy";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                PrivacyPolicyModel privacyPolicyModel = archLibBL.PrivacyPolicy(this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("PrivacyPolicy", privacyPolicyModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        // GET: RefundPolicy
        [AllowAnonymous]
        [HttpGet]
        [Route("RefundPolicy")]
        public ActionResult RefundPolicy()
        {
            ViewData["ActionName"] = "RefundPolicy";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                RefundPolicyModel refundPolicyModel = archLibBL.RefundPolicy(this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("RefundPolicy", refundPolicyModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        // GET: RegisterUser
        [AllowAnonymous]
        [HttpGet]
        [Route("RegisterUser")]
        public ActionResult RegisterUser()
        {
            //int x = 1, y = 0, z = x / y;
            ViewData["ActionName"] = "RegisterUserProf";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                RegisterUserModel registerUserModel = archLibBL.RegisterUser("700", RetailSlnCache.DefaultDeliveryDemogInfoCountryId, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                //long.TryParse(id, out long userTypeId);
                //var aspNetRoleModels = ArchLibCache.AspNetRoleModels.FindAll(x => x.UserTypeId == userTypeId);
                //registerUserModel.AspNetRoleModels = aspNetRoleModels.Count == 0 ? RetailSlnCache.AspNetRoleModelsPriest : aspNetRoleModels;
                #region For Testing - Delete
                //registerUserModel.DemogInfoAddressModel.BuildingTypeId = BuildingTypeEnum._;
                //registerUserModel.TelephoneNumber = "9880110045";
                //registerUserModel.LoginPassword = "Login9@9Password";
                //registerUserModel.ConfirmLoginPassword = "Login9@9Password";
                //registerUserModel.FirstName = "Priest First";
                //registerUserModel.LastName = "Priest Last";
                //registerUserModel.DemogInfoAddressModel.AddressLine1 = "123 Sri Rama Apt";
                //registerUserModel.DemogInfoAddressModel.AddressLine2 = "Near Metro Station";
                //registerUserModel.DemogInfoAddressModel.ZipCode = "600003";
                //registerUserModel.DemogInfoAddressModel.CityName = "CHENNAI";
                //registerUserModel.DemogInfoAddressModel.DemogInfoSubDivisionId = 391;
                #endregion
                Session["CaptchaNumberRegisterUser0"] = registerUserModel.CaptchaNumberRegisterUser0;
                Session["CaptchaNumberRegisterUser1"] = registerUserModel.CaptchaNumberRegisterUser1;
                actionResult = View("RegisterUser", registerUserModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        // POST: RegisterUser
        [AllowAnonymous]
        [HttpPost]
        public ActionResult RegisterUser(RegisterUserModel registerUserModel)
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
                ModelState.Clear();
                TryValidateModel(registerUserModel);
                TryValidateModel(registerUserModel.DemogInfoAddressModel, "DemogInfoAddressModel");
                archLibBL.RegisterUser(ref registerUserModel, true, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (ModelState.IsValid)
                {
                    if (!registerUserModel.RedirectToUpdatePassword)
                    {
                        retailSlnBL.RegisterUserProfPersonExtn1(registerUserModel.PersonId, 0, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    }
                    RegisterUserEmailModel registerUserEmailModel = new RegisterUserEmailModel
                    {
                        RegisterUserModel = registerUserModel,
                    };
                    retailSlnBL.RegisterUserExtn1(registerUserEmailModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    string registerUserEmailBodyHtml = archLibBL.ViewToHtmlString(this, "_RegisterUserEmailBody", registerUserEmailModel);
                    string registerUserEmailSubjectHtml = archLibBL.ViewToHtmlString(this, "_RegisterUserEmailSubject", registerUserEmailModel);
                    string signatureHtml = archLibBL.ViewToHtmlString(this, "_SignatureTemplate", registerUserEmailModel);
                    registerUserEmailBodyHtml += signatureHtml;
                    archLibBL.SendEmail(registerUserModel.RegisterEmailAddress, registerUserEmailSubjectHtml, registerUserEmailBodyHtml, null, clientId, ipAddress, execUniqueId, loggedInUserId);
                    success = true;
                    processMessage = "SUCCESS!!!";
                    htmlString = archLibBL.ViewToHtmlString(this, "_RegisterUserSuccess", registerUserEmailModel);
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
                }
                else
                {
                    success = false;
                    processMessage = "ERROR???";
                    var aspNetRoleModels = ArchLibCache.AspNetRoleModels.FindAll(x => x.UserTypeId == registerUserModel.AspNetRoleUserTypeId);
                    registerUserModel.AspNetRoleModels = aspNetRoleModels.Count == 0 ? RetailSlnCache.AspNetRoleModelsPriest : aspNetRoleModels;
                    registerUserModel.DemogInfoAddressModel.BuildingTypeSelectListItems = LookupCache.CodeTypeSelectListItems["BuildingType"]["CodeDataNameId"];
                    registerUserModel.DemogInfoAddressModel.DemogInfoCountrySelectListItems = DemogInfoCache.DemogInfoCountrySelectListItems;
                    if (registerUserModel.DemogInfoAddressModel.DemogInfoCountryId == null || registerUserModel.DemogInfoAddressModel.DemogInfoCountryId < 1)
                    {
                        registerUserModel.DemogInfoAddressModel.DemogInfoSubDivisionSelectListItems = DemogInfoCache.DemogInfoSubDivisionSelectListItems[RetailSlnCache.DefaultDeliveryDemogInfoCountryId];
                    }
                    else
                    {
                        registerUserModel.DemogInfoAddressModel.DemogInfoSubDivisionSelectListItems = DemogInfoCache.DemogInfoSubDivisionSelectListItems[registerUserModel.DemogInfoAddressModel.DemogInfoCountryId.Value];
                    }
                    htmlString = archLibBL.ViewToHtmlString(this, "_RegisterUserData", registerUserModel);
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process Error");
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                archLibBL.GenerateCaptchaQuesion(Session, "CaptchaNumberRegister0", "CaptchaNumberRegister1");
                registerUserModel.CaptchaAnswerRegisterUser = null;
                registerUserModel.CaptchaNumberRegisterUser0 = Session["CaptchaNumberRegisterUser0"].ToString();
                registerUserModel.CaptchaNumberRegisterUser1 = Session["CaptchaNumberRegisterUser1"].ToString();
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                var aspNetRoleModels = ArchLibCache.AspNetRoleModels.FindAll(x => x.UserTypeId == registerUserModel.AspNetRoleUserTypeId);
                registerUserModel.AspNetRoleModels = aspNetRoleModels.Count == 0 ? RetailSlnCache.AspNetRoleModelsPriest : aspNetRoleModels;
                registerUserModel.DemogInfoAddressModel.BuildingTypeSelectListItems = LookupCache.CodeTypeSelectListItems["BuildingType"]["CodeDataNameId"];
                registerUserModel.DemogInfoAddressModel.DemogInfoCountrySelectListItems = DemogInfoCache.DemogInfoCountrySelectListItems;
                if (registerUserModel.DemogInfoAddressModel.DemogInfoCountryId == null || registerUserModel.DemogInfoAddressModel.DemogInfoCountryId < 1)
                {
                    registerUserModel.DemogInfoAddressModel.DemogInfoSubDivisionSelectListItems = DemogInfoCache.DemogInfoSubDivisionSelectListItems[RetailSlnCache.DefaultDeliveryDemogInfoCountryId];
                }
                else
                {
                    registerUserModel.DemogInfoAddressModel.DemogInfoSubDivisionSelectListItems = DemogInfoCache.DemogInfoSubDivisionSelectListItems[registerUserModel.DemogInfoAddressModel.DemogInfoCountryId.Value];
                }
                registerUserModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                };
                success = false;
                processMessage = "ERROR???";
                htmlString = archLibBL.ViewToHtmlString(this, "_RegisterUserData", registerUserModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00099100 :: Error Exit");
            }
            actionResult = Json(new { success, processMessage, htmlString });
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        // GET: RegisterUser
        [AllowAnonymous]
        [HttpGet]
        [Route("RegisterUserProf")]
        public ActionResult RegisterUserProf()
        {
            //int x = 1, y = 0, z = x / y;
            ViewData["ActionName"] = "RegisterUserProf";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                RegisterUserModel registerUserModel = archLibBL.RegisterUser("100", RetailSlnCache.DefaultDeliveryDemogInfoCountryId, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                #region For Testing - Delete
                //registerUserModel.DemogInfoAddressModel.BuildingTypeId = BuildingTypeEnum._;
                //registerUserModel.TelephoneNumber = "9880110045";
                //registerUserModel.LoginPassword = "Login9@9Password";
                //registerUserModel.ConfirmLoginPassword = "Login9@9Password";
                //registerUserModel.FirstName = "Priest First";
                //registerUserModel.LastName = "Priest Last";
                //registerUserModel.DemogInfoAddressModel.AddressLine1 = "123 Sri Rama Apt";
                //registerUserModel.DemogInfoAddressModel.AddressLine2 = "Near Metro Station";
                //registerUserModel.DemogInfoAddressModel.ZipCode = "600003";
                //registerUserModel.DemogInfoAddressModel.CityName = "CHENNAI";
                //registerUserModel.DemogInfoAddressModel.DemogInfoSubDivisionId = 391;
                #endregion
                Session["CaptchaNumberRegisterUser0"] = registerUserModel.CaptchaNumberRegisterUser0;
                Session["CaptchaNumberRegisterUser1"] = registerUserModel.CaptchaNumberRegisterUser1;
                actionResult = View("RegisterUserProf", registerUserModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        // POST: RegisterUser
        [AllowAnonymous]
        [HttpPost]
        public ActionResult RegisterUserProf(RegisterUserModel registerUserModel)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            bool success;
            string processMessage, htmlString, redirectUrl;
            try
            {
                //int x = 1, y = 0, z = x / y;
                ModelState.Clear();
                TryValidateModel(registerUserModel);
                TryValidateModel(registerUserModel.DemogInfoAddressModel, "DemogInfoAddressModel");
                archLibBL.RegisterUser(ref registerUserModel, true, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (ModelState.IsValid)
                {
                    if (!registerUserModel.RedirectToUpdatePassword)
                    {
                        retailSlnBL.RegisterUserProfPersonExtn1(registerUserModel.PersonId, 0, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    }
                    RegisterUserEmailModel registerUserEmailModel = new RegisterUserEmailModel
                    {
                        RegisterUserModel = registerUserModel,
                    };
                    string registerUserEmailBodyHtml = archLibBL.ViewToHtmlString(this, "_RegisterUserEmailBody", registerUserEmailModel);
                    string registerUserEmailSubjectHtml = archLibBL.ViewToHtmlString(this, "_RegisterUserEmailSubject", registerUserEmailModel);
                    string signatureHtml = archLibBL.ViewToHtmlString(this, "_SignatureTemplate", registerUserEmailModel);
                    registerUserEmailBodyHtml += signatureHtml;
                    archLibBL.SendEmail(registerUserModel.RegisterEmailAddress, registerUserEmailSubjectHtml, registerUserEmailBodyHtml, null, clientId, ipAddress, execUniqueId, loggedInUserId);
                    LoginUserProfModel loginUserProfModel = new LoginUserProfModel
                    {
                        LoginEmailAddress = registerUserModel.RegisterEmailAddress,
                        LoginPassword = registerUserModel.LoginPassword,
                    };
                    SessionObjectModel sessionObjectModel = archLibBL.LoginUserProf(ref loginUserProfModel, false, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    redirectUrl = LoginUserProfProcess(sessionObjectModel.AspNetUserId, sessionObjectModel);
                    success = true;
                    processMessage = "SUCCESS!!!";
                    htmlString = "";
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
                }
                else
                {
                    var aspNetRoleModels = ArchLibCache.AspNetRoleModels.FindAll(x => x.UserTypeId == registerUserModel.AspNetRoleUserTypeId);
                    registerUserModel.AspNetRoleModels = aspNetRoleModels.Count == 0 ? RetailSlnCache.AspNetRoleModelsPriest : aspNetRoleModels;
                    registerUserModel.DemogInfoAddressModel.BuildingTypeSelectListItems = LookupCache.CodeTypeSelectListItems["BuildingType"]["CodeDataNameId"];
                    registerUserModel.DemogInfoAddressModel.DemogInfoCountrySelectListItems = DemogInfoCache.DemogInfoCountrySelectListItems;
                    if (registerUserModel.DemogInfoAddressModel.DemogInfoCountryId == null || registerUserModel.DemogInfoAddressModel.DemogInfoCountryId < 1)
                    {
                        registerUserModel.DemogInfoAddressModel.DemogInfoSubDivisionSelectListItems = DemogInfoCache.DemogInfoSubDivisionSelectListItems[RetailSlnCache.DefaultDeliveryDemogInfoCountryId];
                    }
                    else
                    {
                        registerUserModel.DemogInfoAddressModel.DemogInfoSubDivisionSelectListItems = DemogInfoCache.DemogInfoSubDivisionSelectListItems[registerUserModel.DemogInfoAddressModel.DemogInfoCountryId.Value];
                    }
                    success = false;
                    processMessage = "ERROR???";
                    htmlString = archLibBL.ViewToHtmlString(this, "_RegisterUserProfData", registerUserModel);
                    redirectUrl = "";
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process Error");
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                archLibBL.GenerateCaptchaQuesion(Session, "CaptchaNumberRegister0", "CaptchaNumberRegister1");
                registerUserModel.CaptchaAnswerRegisterUser = null;
                registerUserModel.CaptchaNumberRegisterUser0 = Session["CaptchaNumberRegisterUser0"].ToString();
                registerUserModel.CaptchaNumberRegisterUser1 = Session["CaptchaNumberRegisterUser1"].ToString();
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                var aspNetRoleModels = ArchLibCache.AspNetRoleModels.FindAll(x => x.UserTypeId == registerUserModel.AspNetRoleUserTypeId);
                registerUserModel.AspNetRoleModels = aspNetRoleModels.Count == 0 ? RetailSlnCache.AspNetRoleModelsPriest : aspNetRoleModels;
                registerUserModel.DemogInfoAddressModel.BuildingTypeSelectListItems = LookupCache.CodeTypeSelectListItems["BuildingType"]["CodeDataNameId"];
                registerUserModel.DemogInfoAddressModel.DemogInfoCountrySelectListItems = DemogInfoCache.DemogInfoCountrySelectListItems;
                if (registerUserModel.DemogInfoAddressModel.DemogInfoCountryId == null || registerUserModel.DemogInfoAddressModel.DemogInfoCountryId < 1)
                {
                    registerUserModel.DemogInfoAddressModel.DemogInfoSubDivisionSelectListItems = DemogInfoCache.DemogInfoSubDivisionSelectListItems[RetailSlnCache.DefaultDeliveryDemogInfoCountryId];
                }
                else
                {
                    registerUserModel.DemogInfoAddressModel.DemogInfoSubDivisionSelectListItems = DemogInfoCache.DemogInfoSubDivisionSelectListItems[registerUserModel.DemogInfoAddressModel.DemogInfoCountryId.Value];
                }
                registerUserModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                };
                success = false;
                processMessage = "ERROR???";
                htmlString = archLibBL.ViewToHtmlString(this, "_RegisterUserData", registerUserModel);
                redirectUrl = "";
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00099100 :: Error Exit");
            }
            actionResult = Json(new { success, processMessage, htmlString, redirectUrl });
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ResetPassword")]
        public ActionResult ResetPassword(string id)
        {
            //int x = 1, y = 0, z = x / y;
            if (string.IsNullOrWhiteSpace(id))
            {
                ViewData["ActionName"] = "RESETPASSWORD";
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
                ResetPasswordModel resetPasswordModel = archLibBL.ResetPassword(Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("ResetPassword", resetPasswordModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                ModelState.AddModelError("", "Reset Password GET");
                archLibBL.CopyReponseObjectToModelErrors(ModelState, null, responseObjectModel.ResponseMessages);
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        // POST: ResetPassword
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordModel resetPasswordModel)
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
                TryValidateModel(resetPasswordModel);
                UpdatePasswordModel updatePasswordModel = archLibBL.ResetPassword(ref resetPasswordModel, RetailSlnCache.DefaultDeliveryDemogInfoCountryId, true, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (ModelState.IsValid)
                {
                    success = true;
                    processMessage = "SUCCESS!!!";
                    //UpdatePasswordModel updatePasswordModel = archLibBL.UpdatePassword(resetPasswordModel.ResetPasswordEmailAddress, RetailSlnCache.DefaultDeliveryDemogInfoCountryId, resetPasswordModel.OTPCreatedDateTime, resetPasswordModel.OTPExpiryDateTime, resetPasswordModel.OTPExpiryDuration, resetPasswordModel.OTPSendTypeId, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    //updatePasswordModel.DemogInfoAddressModel = new DemogInfoAddressModel
                    //{
                    //    BuildingTypeId = BuildingTypeEnum._,
                    //    BuildingTypeSelectListItems = LookupCache.CodeTypeSelectListItems["BuildingType"]["CodeDataNameId"],
                    //    DemogInfoCountryId = RetailSlnCache.DefaultDeliveryDemogInfoCountryId,
                    //    DemogInfoCountrySelectListItems = RetailSlnCache.DeliveryDemogInfoCountrySelectListItems,
                    //    DemogInfoSubDivisionSelectListItems = DemogInfoCache.DemogInfoSubDivisionSelectListItems[RetailSlnCache.DefaultDeliveryDemogInfoCountryId],
                    //};
                    htmlString = archLibBL.ViewToHtmlString(this, "_UpdatePassword", updatePasswordModel);
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
                }
                else
                {
                    success = false;
                    processMessage = "ERROR???";
                    htmlString = archLibBL.ViewToHtmlString(this, "_ResetPasswordData", resetPasswordModel);
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process Error");
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                archLibBL.GenerateCaptchaQuesion(Session, "CaptchaNumberResetPassword0", "CaptchaNumberResetPassword1");
                resetPasswordModel.CaptchaAnswerResetPassword = null;
                resetPasswordModel.CaptchaNumberResetPassword0 = Session["CaptchaNumberResetPassword0"].ToString();
                resetPasswordModel.CaptchaNumberResetPassword1 = Session["CaptchaNumberResetPassword1"].ToString();
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                resetPasswordModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                };
                success = false;
                processMessage = "ERROR???";
                htmlString = archLibBL.ViewToHtmlString(this, "_ResetPasswordData", resetPasswordModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00099100 :: Error Exit");
            }
            actionResult = Json(new { success, processMessage, htmlString });
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        // GET: ResetPasswordContactUs
        [AllowAnonymous]
        [HttpGet]
        [Route("ReturnPolicy")]
        public ActionResult ReturnPolicy()
        {
            ViewData["ActionName"] = "ReturnPolicy";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                ReturnPolicyModel returnPolicyModel = archLibBL.ReturnPolicy(this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("ReturnPolicy", returnPolicyModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        // GET: ResetPasswordContactUs
        [AllowAnonymous]
        [HttpGet]
        [Route("ShippingPolicy")]
        public ActionResult ShippingPolicy()
        {
            ViewData["ActionName"] = "ShippingPolicy";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                ShippingPolicyModel shippingPolicyModel = archLibBL.ShippingPolicy(this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("ShippingPolicy", shippingPolicyModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        // GET: SEOPage1
        [AllowAnonymous]
        [HttpGet]
        [Route("SEOPage1")]
        public ActionResult SEOPage1()
        {
            return View();
        }

        // GET: TermsofService
        [AllowAnonymous]
        [HttpGet]
        [Route("TermsofService")]
        public ActionResult TermsofService()
        {
            ViewData["ActionName"] = "TermsofService";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                TermsofServiceModel termsofServiceModel = archLibBL.TermsofService(this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("TermsofService", termsofServiceModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        // GET: Testimonials
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Testimonials()
        {
            ViewData["ActionName"] = "Testimonials";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                TestimonialsModel testimonialsModel = archLibBL.Testimonials(this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Testimonials", testimonialsModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "Testimonials / GET");
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        // GET: Unauthorized
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Unauthorized()
        {
            return RedirectToAction("LoginUserProf");
        }

        // POST: UpdatePassword
        [AllowAnonymous]
        [HttpPost]
        public ActionResult UpdatePassword(UpdatePasswordModel updatePasswordModel)
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
                ModelState.Clear();
                TryValidateModel(updatePasswordModel);
                TryValidateModel(updatePasswordModel.DemogInfoAddressModel, "DemogInfoAddressModel");
                string currentLoggedInUserId = loggedInUserId;
                archLibBL.UpdatePassword(ref updatePasswordModel, false, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (ModelState.IsValid)
                {
                    LoginUserProfModel loginUserProfModel = new LoginUserProfModel
                    {
                        LoginEmailAddress = updatePasswordModel.EmailAddress,
                        LoginPassword = updatePasswordModel.LoginPassword,
                    };
                    SessionObjectModel sessionObjectModel = archLibBL.LoginUserProf(ref loginUserProfModel, false, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    if (ModelState.IsValid)
                    {
                        var redirectUrl = LoginUserProfProcess(currentLoggedInUserId, sessionObjectModel);
                        success = true;
                        processMessage = "SUCCESS!!!";
                        actionResult = Json(new { success, processMessage, redirectUrl });
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
                    }
                    else
                    {
                        success = false;
                        processMessage = "ERROR???";
                        var demogInfoCountryId = updatePasswordModel.DemogInfoAddressModel.DemogInfoCountryId == null ? RetailSlnCache.DefaultDeliveryDemogInfoCountryId : updatePasswordModel.DemogInfoAddressModel.DemogInfoCountryId.Value;
                        updatePasswordModel.DemogInfoAddressModel.BuildingTypeSelectListItems = LookupCache.CodeTypeSelectListItems["BuildingType"]["CodeDataNameId"];
                        updatePasswordModel.DemogInfoAddressModel.DemogInfoCountrySelectListItems = DemogInfoCache.DemogInfoCountrySelectListItems;
                        updatePasswordModel.DemogInfoAddressModel.DemogInfoCountrySubDivisionModels = DemogInfoCache.DemogInfoSubDivisionModels.FindAll(x => x.DemogInfoCountryId == demogInfoCountryId);
                        archLibBL.UpdatePasswordPostData(ref updatePasswordModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                        htmlString = archLibBL.ViewToHtmlString(this, "_UpdatePasswordData", updatePasswordModel);
                        actionResult = Json(new { success, processMessage, htmlString });
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process Error");
                    }
                }
                else
                {
                    success = false;
                    processMessage = "ERROR???";
                    var demogInfoCountryId = updatePasswordModel.DemogInfoAddressModel.DemogInfoCountryId == null ? RetailSlnCache.DefaultDeliveryDemogInfoCountryId : updatePasswordModel.DemogInfoAddressModel.DemogInfoCountryId.Value;
                    updatePasswordModel.DemogInfoAddressModel.BuildingTypeSelectListItems = LookupCache.CodeTypeSelectListItems["BuildingType"]["CodeDataNameId"];
                    updatePasswordModel.DemogInfoAddressModel.DemogInfoCountrySelectListItems = DemogInfoCache.DemogInfoCountrySelectListItems;
                    updatePasswordModel.DemogInfoAddressModel.DemogInfoCountrySubDivisionModels = DemogInfoCache.DemogInfoSubDivisionModels.FindAll(x => x.DemogInfoCountryId == demogInfoCountryId);
                    archLibBL.UpdatePasswordPostData(ref updatePasswordModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    htmlString = archLibBL.ViewToHtmlString(this, "_UpdatePasswordData", updatePasswordModel);
                    actionResult = Json(new { success, processMessage, htmlString });
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process Error");
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                archLibBL.UpdatePasswordPostData(ref updatePasswordModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                var demogInfoCountryId = updatePasswordModel.DemogInfoAddressModel.DemogInfoCountryId == null ? RetailSlnCache.DefaultDeliveryDemogInfoCountryId : updatePasswordModel.DemogInfoAddressModel.DemogInfoCountryId.Value;
                updatePasswordModel.DemogInfoAddressModel.BuildingTypeSelectListItems = LookupCache.CodeTypeSelectListItems["BuildingType"]["CodeDataNameId"];
                updatePasswordModel.DemogInfoAddressModel.DemogInfoCountrySelectListItems = RetailSlnCache.DeliveryDemogInfoCountrySelectListItems;
                updatePasswordModel.DemogInfoAddressModel.DemogInfoCountrySubDivisionModels = DemogInfoCache.DemogInfoSubDivisionModels.FindAll(x => x.DemogInfoCountryId == demogInfoCountryId);
                updatePasswordModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                };
                //actionResult = PartialView("_UpdatePasswordData", updatePasswordModel);
                htmlString = archLibBL.ViewToHtmlString(this, "_UpdatePasswordData", updatePasswordModel);
                success = false;
                processMessage = "ERROR???";
                actionResult = Json(new { success, processMessage, htmlString });
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00099100 :: Error Exit");
            }
            //htmlString = archLibBL.ViewToHtmlString(this, "_UpdatePasswordData", updatePasswordModel);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        // GET: UserProfile
        [Authorize]
        [AjaxAuthorize]
        [HttpGet]
        [Route("UserProfile")]
        public ActionResult UserProfile()
        {
            ViewData["ActionName"] = "UserProfile";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            ActionResult actionResult;
            try
            {
                //int x = 1, y = 0, z = x / y;
                string aspNetUserId = ((SessionObjectModel)Session["SessionObject"]).AspNetUserId;
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Before BL");
                PersonModel personModel = archLibBL.UserProfile(aspNetUserId, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("UserProfile", personModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: After BL");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "UserProfile / GET");
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error");
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        private string LoginUserProfProcess(string currentLoggedInUserId, SessionObjectModel sessionObjectModel)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ApplSessionObjectModel applSessionObjectModel;
            applSessionObjectModel = retailSlnBL.LoginUserProf(sessionObjectModel.PersonId, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
            sessionObjectModel.ApplSessionObjectModel = applSessionObjectModel;
            SessionObjectModel createForSessionObject = archLibBL.CopySessionObject(sessionObjectModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
            applSessionObjectModel = retailSlnBL.LoginUserProf(createForSessionObject.PersonId, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
            createForSessionObject.ApplSessionObjectModel = applSessionObjectModel;
            Session["SessionObject"] = sessionObjectModel;
            Session["CreateForSessionObject"] = createForSessionObject;
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
            string aspNetRoleName, redirectUrl;
            aspNetRoleName = sessionObjectModel.AspNetRoleName;
            Dictionary<string, AspNetRoleKVPModel> aspNetRoleKVPs = ArchLibCache.AspNetRoleKVPs[aspNetRoleName];
            if (aspNetRoleName != aspNetRoleKVPs["ProxyAspNetRoleName00"].KVPValueData)
            {
                aspNetRoleName = aspNetRoleKVPs[aspNetRoleName].KVPValueData;
                aspNetRoleKVPs = ArchLibCache.AspNetRoleKVPs[aspNetRoleName];
            }
            redirectUrl = Url.Action(aspNetRoleKVPs["ActionName00"].KVPValueData, aspNetRoleKVPs["ControllerName00"].KVPValueData);
            PaymentInfoModel paymentInfoModel = (PaymentInfoModel)Session["PaymentInfo"];
            retailSlnBL.CreateOrderWIP(ref paymentInfoModel, sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
            //Take a look at the below logic Begin
            //if (currentLoggedInUserId != createForSessionObject.AspNetUserId)
            //{
            //    if (currentLoggedInUserId != "")
            //    {
            //    }
            //}
            //Take a look at the below logic End
            //actionResult = Json(new { success, processMessage, redirectUrl });
            //redirectUrl = Url.Action(actionName, controllerName);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return redirectUrl;
        }
    }
}
