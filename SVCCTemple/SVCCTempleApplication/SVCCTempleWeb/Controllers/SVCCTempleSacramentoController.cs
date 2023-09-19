using ArchitectureLibraryBusinessLayer;
using ArchitectureLibraryCacheData;
using ArchitectureLibraryEnumerations;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using SVCCTempleBusinessLayer;
using SVCCTempleCacheData;
using SVCCTempleModels;
using SVCCTempleWeb.ClassCode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SVCCTempleWeb.Controllers
{
    public class SVCCTempleSacramentoController : Controller
    {
        private const string LOCATION_NAME_DESC = "SACRAMENTO";
        private const string LOCATION_NAME_DESC1 = "Sacramento";
        private readonly long clientId = -1;//long.Parse(Utilities.GetApplicationValue("ClientId"));
        private readonly string execUniqueId = Utilities.CreateExecUniqueId();

        public SVCCTempleSacramentoController()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = "", loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SVCCTempleCache.BuildTodaysInfo(clientId, ipAddress, execUniqueId, loggedInUserId);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        }

        // GET: SVCCTempleSacramento
        public ActionResult Index()
        {
            ViewData["ActionName"] = "Index";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            SVCCTempleBL sVCCTempleBL = new SVCCTempleBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                string importantIdsList = "5001, 5002, 5005, 5006, 5007, 5008, 5009";
                string startDate = DateTime.Now.ToString("yyyy-MM-dd"), finishDate = DateTime.Now.AddDays(8).ToString("yyyy-MM-dd");
                IndexModel indexModel = sVCCTempleBL.BuildIndexModel(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, importantIdsList, 12, startDate, finishDate, execUniqueId);
                actionResult = View("Index", indexModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "Index / GET");
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error");
            }
            return actionResult;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult AboutUs()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ViewData["ActionName"] = "AboutUs";
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Calendar(string id)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ViewData["ActionName"] = "Calendar";
            SVCCTempleBL svccTempleBL = new SVCCTempleBL();
            string importantDatesIds = "5001, 5002, 5003, 5004, 5005, 5006, 5051, 5052, 5053, 5054, 5055, 5056, 5057, 5058";
            CalendarModel calendarModel = svccTempleBL.GenerateCalendarData(LOCATION_NAME_DESC, id, importantDatesIds, clientId, ipAddress, execUniqueId, loggedInUserId);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return View(calendarModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ContactUs()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ViewData["ActionName"] = "ContactUs";
            SVCCTempleBL svccTempleBL = new SVCCTempleBL();
            SVCCTempleModels.ContactUsModel contactUsModel = svccTempleBL.ContactUs(LOCATION_NAME_DESC, clientId, ipAddress, execUniqueId, loggedInUserId);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return View(contactUsModel);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ContactUs(SVCCTempleModels.ContactUsModel contactUsModel)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ViewData["ActionName"] = "ContactUs";
            JsonResult jsonResult;
            WebUtilities webUtilities = new WebUtilities();
            contactUsModel.ResponseObjectModel = webUtilities.InitializeResponseObjectModel();
            ResponseModel responseModel;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                ModelState.Clear();
                if (TryValidateModel(contactUsModel))
                {
                    SVCCTempleBL svccTempleBL = new SVCCTempleBL();
                    svccTempleBL.ContactUs(LOCATION_NAME_DESC, LOCATION_NAME_DESC1, contactUsModel, this, clientId, ipAddress, execUniqueId, loggedInUserId);
                    responseModel = new ResponseModel
                    {
                        ResponseMessagesHtml = archLibBL.ViewToHtmlString(this, "_ResponseMessages", contactUsModel.ResponseObjectModel),
                        ResponseMessagesData = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("ContactUsTypeId", ""),
                            new KeyValuePair<string, string>("FirstName", ""),
                            new KeyValuePair<string, string>("LastName", ""),
                            new KeyValuePair<string, string>("EmailAddress", ""),
                            new KeyValuePair<string, string>("TelephoneNumber", ""),
                            new KeyValuePair<string, string>("Comments", ""),
                        },
                    };
                    jsonResult = Json(responseModel, JsonRequestBehavior.AllowGet);
                }
                else //Model Errors
                {
                    contactUsModel.ResponseObjectModel.ResponseTypeId = SVCCTempleEnumerations.ResponseTypeEnum.Error;
                    ValidationSummaryModel validationSummaryModel = new ValidationSummaryModel
                    {
                        ResponseMessagesError = webUtilities.CopyModelErrorsToReponseMessagesError(ModelState),
                        ValidationSummaryMessage = "Please fix errors to continue",
                    };
                    validationSummaryModel.ValidationSummaryPropertiesHtml = archLibBL.ViewToHtmlString(this, "_ValidationSummary", validationSummaryModel);
                    jsonResult = Json(validationSummaryModel, JsonRequestBehavior.AllowGet);
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Model Errors");
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Error", exception);
                contactUsModel.ResponseObjectModel.ResponseTypeId = SVCCTempleEnumerations.ResponseTypeEnum.Error;
                ModelState.AddModelError("", "System error occurred");
                ModelState.AddModelError("", "Please contact support personnel");
                ValidationSummaryModel validationSummaryModel = new ValidationSummaryModel
                {
                    ResponseMessagesError = webUtilities.CopyModelErrorsToReponseMessagesError(ModelState),
                    ValidationSummaryMessage = "Please fix errors to continue",
                };
                validationSummaryModel.ValidationSummaryPropertiesHtml = archLibBL.ViewToHtmlString(this, "_ValidationSummary", validationSummaryModel);
                jsonResult = Json(validationSummaryModel, JsonRequestBehavior.AllowGet);
            }
            if (contactUsModel.ResponseObjectModel.ResponseTypeId == SVCCTempleEnumerations.ResponseTypeEnum.Error)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.OK;
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return jsonResult;
        }


        [AllowAnonymous]
        [HttpGet]
        //[Route("RegisterUserLoginUser")]
        public ActionResult RegisterUserLoginUser(string id)
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
                actionResult = View("RegisterUserLoginUser", registerUserLoginUserModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ArchitectureLibraryModels.ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                ModelState.AddModelError("", "Register User / Login / Reset Password / GET");
                archLibBL.CopyReponseObjectToModelErrors(ModelState, null, responseObjectModel.ResponseMessages);
                actionResult = View("Error", responseObjectModel);
            }
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
            SVCCTempleBL sVCCTempleBL = new SVCCTempleBL();
            bool success;
            string processMessage, htmlString;
            try
            {
                //int x = 1, y = 0, z = x / y;
                ModelState.Clear();
                TryValidateModel(registerUserProfModel);
                sVCCTempleBL.RegisterUserProf(LOCATION_NAME_DESC, ref registerUserProfModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
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
                registerUserProfModel.ResponseObjectModel = new ArchitectureLibraryModels.ResponseObjectModel
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
        public ActionResult UpdatePassword(string id)
        {
            //int x = 1, y = 0, z = x / y;
            ViewData["ActionName"] = "UpdatePassword";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            SVCCTempleBL sVCCTempleBL = new SVCCTempleBL();
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
                ArchitectureLibraryModels.ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
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
                updatePasswordModel.ResponseObjectModel = new ArchitectureLibraryModels.ResponseObjectModel
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
    }
}
