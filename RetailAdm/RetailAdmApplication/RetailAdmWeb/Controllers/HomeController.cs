using ArchitectureLibraryBusinessLayer;
using ArchitectureLibraryCacheData;
using ArchitectureLibraryClassCode;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RetailAdmWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly long clientId = long.Parse(Utilities.GetApplicationValue("ClientId"));
        private readonly string execUniqueId = Utilities.CreateExecUniqueId();
        private readonly string lastIpAddress = Utilities.GetLastIPAddress();

        // GET: Home
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index()
        {
            ViewData["ActionName"] = "Index";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request, lastIpAddress, ArchLibCache.IpInfoClientAccessToken), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                IndexModel indexModel = archLibBL.Index(this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Index", indexModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                ModelState.AddModelError("", "Index / GET");
                archLibBL.CopyReponseObjectToModelErrors(ModelState, null, responseObjectModel.ResponseMessages);
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        [Authorize]
        [AjaxAuthorize]
        [HttpGet]
        [Route("Dashboard")]
        public ActionResult Dashboard()
        {
            return View();
        }

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

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Forbidden()
        {
            return View();
            //string url = "/Home/Forbidden";
            //return JavaScript(string.Format("window.open('{0}', '_blank', 'left=100,top=100,width=500,height=500,toolbar=no,resizable=no,scrollable=yes');", url));
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
            Session.Abandon();
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return RedirectToAction("Index");
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

        [AllowAnonymous]
        [HttpGet]
        [Route("ResetPassword")]
        public ActionResult ResetPassword()
        {
            ViewData["ActionName"] = "ResetPassword";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                ResetPasswordModel resetPasswordModel = archLibBL.ResetPassword(Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                Session["CaptchaNumberResetPassword0"] = resetPasswordModel.CaptchaNumberResetPassword0;
                Session["CaptchaNumberResetPassword1"] = resetPasswordModel.CaptchaNumberResetPassword1;
                actionResult = View("ResetPassword", resetPasswordModel);
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
                archLibBL.ResetPassword(ref resetPasswordModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
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
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00099100 :: Error Exit");
            }
            htmlString = archLibBL.ViewToHtmlString(this, "_ResetPasswordData", resetPasswordModel);
            actionResult = Json(new { success, processMessage, htmlString });
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("SEOPage1")]
        public ActionResult SEOPage1()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Unauthorized()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult UpdatePassword(string id)
        {
            //int x = 1, y = 0, z = x / y;
            ViewData["ActionName"] = "UpdatePassword";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                UpdatePasswordModel updatePasswordModel = archLibBL.UpdatePassword(id, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (ModelState.IsValid)
                {
                }
                else
                {
                }
                actionResult = View("UpdatePassword", updatePasswordModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                ModelState.AddModelError("", "Update Password / GET");
                archLibBL.CopyReponseObjectToModelErrors(ModelState, null, responseObjectModel.ResponseMessages);
                Session["CaptchaNumber0"] = null;
                Session["CaptchaNumber1"] = null;
                actionResult = View("Error", responseObjectModel);
            }
            return actionResult;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult UpdatePassword(UpdatePasswordModel updatePasswordModel)
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
                TryValidateModel(updatePasswordModel);
                archLibBL.UpdatePassword(ref updatePasswordModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (ModelState.IsValid)
                {
                    success = true;
                    processMessage = "SUCCESS!!!";
                    //actionResult = PartialView("_UpdatePasswordSuccess", updatePasswordModel);
                    htmlString = archLibBL.ViewToHtmlString(this, "_UpdatePasswordSuccess", updatePasswordModel);
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
                }
                else
                {
                    success = false;
                    processMessage = "ERROR???";
                    //actionResult = PartialView("_UpdatePasswordData", updatePasswordModel);
                    htmlString = archLibBL.ViewToHtmlString(this, "_UpdatePasswordData", updatePasswordModel);
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process Error");
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                archLibBL.GenerateCaptchaQuesion(Session, "CaptchaNumber0", "CaptchaNumber1");
                updatePasswordModel.CaptchaAnswer = null;
                updatePasswordModel.CaptchaNumber0 = Session["CaptchaNumber0"].ToString();
                updatePasswordModel.CaptchaNumber1 = Session["CaptchaNumber1"].ToString();
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                updatePasswordModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                };
                //actionResult = PartialView("_UpdatePasswordData", updatePasswordModel);
                htmlString = archLibBL.ViewToHtmlString(this, "_UpdatePasswordData", updatePasswordModel);
                success = false;
                processMessage = "ERROR???";
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00099100 :: Error Exit");
            }
            //htmlString = archLibBL.ViewToHtmlString(this, "_UpdatePasswordData", updatePasswordModel);
            actionResult = Json(new { success, processMessage, htmlString });
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

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

        [Authorize]
        [AjaxAuthorize]
        [HttpPost]
        public ActionResult UserProfile(PersonModel personModel)
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
                ModelState.Clear();
                TryValidateModel(personModel);
                TryValidateModel(personModel.HomeDemogInfoAddressModel, "HomeDemogInfoAddressModel");
                TryValidateModel(personModel.AspNetUserModel, "AspNetUserModel");
                if (personModel.CertificateDocumentHttpPostedFileBase == null && personModel.CertificateDocumentModel.ServerFileName == null)
                {
                    ModelState.AddModelError("CertificateDocumentModel.ServerFileName", "Select certificate document");
                }
                if (!ArchLibCache.UserProfileMetaDatas.First(x => x.MetaDataName == "Nickname").IsMapped)
                {
                    ModelState.Remove("NicknameFirst");
                    ModelState.Remove("NicknameLast");
                }
                if (!ArchLibCache.UserProfileMetaDatas.First(x => x.MetaDataName == "Name").IsMapped)
                {
                    ModelState.Remove("SalutationId");
                    ModelState.Remove("FirstName");
                    ModelState.Remove("MiddleName");
                    ModelState.Remove("LastName");
                    ModelState.Remove("SuffixId");
                }
                if (!ArchLibCache.UserProfileMetaDatas.First(x => x.MetaDataName == "MaritalStatus").IsMapped)
                {
                    ModelState.Remove("MaritalStatusId");
                }
                if (!ArchLibCache.UserProfileMetaDatas.First(x => x.MetaDataName == "Citizenship").IsMapped)
                {
                    ModelState.Remove("CitizenshipId");
                }
                if (!ArchLibCache.UserProfileMetaDatas.First(x => x.MetaDataName == "DateOfBirth").IsMapped)
                {
                    ModelState.Remove("DateOfBirth");
                }
                if (!ArchLibCache.UserProfileMetaDatas.First(x => x.MetaDataName == "ElectronicSignatureConsent").IsMapped)
                {
                    ModelState.Remove("ElectronicSignatureConsentAccepted");
                    ModelState.Remove("ElectronicSignatureConsent");
                }
                if (!ArchLibCache.UserProfileMetaDatas.First(x => x.MetaDataName == "InitialsSignature").IsMapped)
                {
                    ModelState.Remove("InitialsTextValue");
                    ModelState.Remove("InitialsTextId");
                    ModelState.Remove("SignatureTextValue");
                    ModelState.Remove("SignatureTextId");
                }
                if (!ArchLibCache.UserProfileMetaDatas.First(x => x.MetaDataName == "Address").IsMapped)
                {
                    ModelState.Remove("HomeDemogInfoAddressModel.DemogInfoCountryId");
                    ModelState.Remove("HomeDemogInfoAddressModel.BuildingTypeId");
                    ModelState.Remove("HomeDemogInfoAddressModel.AddressLine1");
                    ModelState.Remove("HomeDemogInfoAddressModel.AddressLine2");
                    ModelState.Remove("HomeDemogInfoAddressModel.DemogInfoSubDivisionId");
                    ModelState.Remove("HomeDemogInfoAddressModel.ZipCode");
                    ModelState.Remove("HomeDemogInfoAddressModel.CityName");
                }
                if (!ArchLibCache.UserProfileMetaDatas.First(x => x.MetaDataName == "Telephone").IsMapped)
                {
                    ModelState.Remove("AspNetUserModel.PhoneNumber");
                }
                if (!ArchLibCache.UserProfileMetaDatas.First(x => x.MetaDataName == "SSN").IsMapped)
                {
                    ModelState.Remove("SSN");
                }
                if (!ArchLibCache.UserProfileMetaDatas.First(x => x.MetaDataName == "DriverLicense").IsMapped)
                {
                    ModelState.Remove("DriverLicenseType");
                    ModelState.Remove("DriverLicenseNumber");
                    ModelState.Remove("DriverLicenseDemogInfoSubDivisionId");
                    ModelState.Remove("DriverLicenseExpiryDate");
                }
                if (!ArchLibCache.UserProfileMetaDatas.First(x => x.MetaDataName == "CertificateDocument").IsMapped)
                {
                    ModelState.Remove("CertificateDocumentModel.ServerFileName");
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Before BL");
                archLibBL.UserProfile(ref personModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (ModelState.IsValid)
                {
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
                }
                else
                {
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process Error");
                }
                actionResult = PartialView("_UserProfileData", personModel);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "UserProfile / POST");
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = PartialView("_Error");
            }
            if (ModelState.IsValid)
            {
            }
            else
            {
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }
    }
}
