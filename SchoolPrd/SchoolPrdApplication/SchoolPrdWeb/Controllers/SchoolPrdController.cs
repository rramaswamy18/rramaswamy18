using ArchitectureLibraryBusinessLayer;
using ArchitectureLibraryCacheData;
using ArchitectureLibraryClassCode;
using ArchitectureLibraryEnumerations;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using SchoolPrdBusinessLayer;
using SchoolPrdModels;
using SchoolPrdWeb.ClassCode;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace SchoolPrdWeb.Controllers
{
    public partial class HomeController : Controller
    {
        [AjaxAuthorize]
        [Authorize]
        [HttpGet]
        public ActionResult Admission()
        {
            //int x = 1, y = 0, z = x / y;
            ViewData["ActionName"] = "Admission";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            SchoolPrdBL schoolPrdBL = new SchoolPrdBL();
            ActionResult actionResult;
            try
            {
                //int x = 1, y = 0, z = x / y;
                long personId = ((SessionObjectModel)Session["SessionObject"]).PersonId;
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Before BL");
                AdmissionModel admissionModel = schoolPrdBL.Admission(this, Session, ModelState, personId, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Admission", admissionModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: After BL");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "Admission / GET");
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error");
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        [AjaxAuthorize]
        [Authorize]
        [HttpPost]
        public ActionResult Admission(AdmissionModel admissionModel)
        {
            //int x = 1, y = 0, z = x / y;
            ViewData["ActionName"] = "Admission";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            SchoolPrdBL schoolPrdBL = new SchoolPrdBL();
            bool success;
            string processMessage, htmlString, redirectUrl;
            try
            {
                //int x = 1, y = 0, z = x / y;
                ModelState.Clear();
                ClassEnrollModel classEnrollModel = admissionModel.ClassEnrollModel;
                TryValidateModel(classEnrollModel, "ClassEnrollModel");
                ModelState.Remove("ClassEnrollModel.CancelDate");
                ModelState.Remove("ClassEnrollModel.CourseCompletionDate");
                ModelState.Remove("ClassEnrollModel.DMVTestDate");
                ModelState.Remove("ClassEnrollModel.RegisterDate");
                ModelState.Remove("ClassEnrollModel.ClassEnrollStatusId");
                ModelState.Remove("ClassEnrollModel.PersonId");
                ModelState.Remove("ClassEnrollModel.PersonNameSearch");
                if (classEnrollModel.FundingRequired != null && classEnrollModel.FundingRequired == YesNoEnum.No)
                {
                    ModelState.Remove("ClassEnrollModel.FundingSoureName");
                }
                var sessionObjectModel = ((SessionObjectModel)Session["SessionObject"]);
                schoolPrdBL.Admission(ref admissionModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (ModelState.IsValid)
                {
                    success = true;
                    processMessage = "SUCCESS!!!";
                    redirectUrl = Url.Action("Payment", "Home");
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: BL Process Success");
                }
                else
                {
                    success = false;
                    processMessage = "ERROR???";
                    redirectUrl = "";
                    admissionModel.ClassEnrollModels = schoolPrdBL.ClassEnrolls(this, Session, ModelState, admissionModel.ClassEnrollModel.PersonId.Value, clientId, ipAddress, execUniqueId, loggedInUserId);
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: BL Process Error");
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                admissionModel.ClassEnrollModels = schoolPrdBL.ClassEnrolls(this, Session, ModelState, admissionModel.ClassEnrollModel.PersonId.Value, clientId, ipAddress, execUniqueId, loggedInUserId);
                admissionModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                };
                success = false;
                processMessage = "ERROR???";
                redirectUrl = "";
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00099100 :: Error Exit");
            }
            htmlString = archLibBL.ViewToHtmlString(this, "_AdmissionData", admissionModel);
            actionResult = Json(new { success, processMessage, htmlString, redirectUrl });
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        [HttpGet]
        [Route("Catalog")]
        public ActionResult Catalog()
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ViewData["ActionName"] = "Catalog";
            ActionResult actionResult = View("Catalog");
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
            //ViewResult ViewResult = new ViewResult();
            //AgreementModel agreementModel = new AgreementModel();
            //try
            //{
            //    if (Request.IsAuthenticated && Session["SessionObject"] != null)
            //    {
            //        ViewData["ActionName"] = "Catalog";
            //        SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];

            //        //string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"90%\" height=\"780px\">";
            //        //embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
            //        //embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            //        //embed += "</object>";
            //        //TempData["Embed"] = string.Format(embed, VirtualPathUtility.ToAbsolute("~/Documents/Catalog _2018.pdf"));
            //        TrainingBL trainingBL = new TrainingBL();
            //        agreementModel = trainingBL.Catalog(execUniqueId);
            //        if (agreementModel.ResponseObjectModel.IsSuccessStatusCode)
            //        {
            //            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            //        }
            //        else
            //        {
            //            WebUtilities.CopyReponseMessageToModelErrors(ModelState, agreementModel.ResponseObjectModel.ResponseMessages);
            //            exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00098000 :: Exception", new Exception("Error in Update Password"), "", "Error in Update Password");
            //        }

            //        ViewResult = View("_Catalog", agreementModel);

            //    }
            //    else
            //    {
            //        ViewData["ActionName"] = "Home";
            //        ViewResult = View("Catalog");
            //    }
            //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            //}
            //catch (Exception exception)
            //{
            //    exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
            //    ViewResult = View("~/Views/Home/Home.cshtml");
            //}
            //return ViewResult;
        }

        //[AjaxAuthorize]
        //[Authorize]
        //[HttpPost]
        //public ActionResult Admission(AdmissionModel admissionModel)
        //{
        //    ViewData["ActionName"] = "Admission";
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    ArchLibBL archLibBL = new ArchLibBL();
        //    SchoolPrdBLWeb schoolPrdBLWeb = new SchoolPrdBLWeb();
        //    ActionResult actionResult;
        //    try
        //    {
        //        //int x = 1, y = 0, z = x / y;
        //        ModelState.Clear();
        //        ClassEnrollModel classEnrollModel = admissionModel.ClassEnrollModel;
        //        TryValidateModel(classEnrollModel, "ClassEnrollModel");
        //        ModelState.Remove("ClassEnrollModel.CancelDate");
        //        ModelState.Remove("ClassEnrollModel.CourseCompletionDate");
        //        ModelState.Remove("ClassEnrollModel.DMVTestDate");
        //        if (classEnrollModel.FundingRequired != null && classEnrollModel.FundingRequired == YesNoEnum.No)
        //        {
        //            ModelState.Remove("ClassEnrollModel.FundingSoureName");
        //        }
        //        var sessionObjectModel = ((SessionObjectModel)Session["SessionObject"]);
        //        if (ModelState.IsValid)
        //        {
        //            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Before BL");
        //            schoolPrdBLWeb.Admission(ref admissionModel, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            string catalogInitialSignatureHtml, performanceFactSheetInitialSignatureHtml, enrollmentAgreementInitialSignatureHtml, signatureHtml;
        //            catalogInitialSignatureHtml = ViewToHtmlString(this, "_CatalogInitialsSignatureTemplate", admissionModel);
        //            performanceFactSheetInitialSignatureHtml = ViewToHtmlString(this, "_PerformanceFactSheetInitialsSignatureTemplate", admissionModel);
        //            enrollmentAgreementInitialSignatureHtml = ViewToHtmlString(this, "_EnrollmentAgreementInitialsSignatureTemplate", admissionModel);
        //            signatureHtml = ViewToHtmlString(this, "_SignatureTemplate", admissionModel);
        //            schoolPrdBLWeb.AdmissionCreateInitialsSignatureFiles(admissionModel, catalogInitialSignatureHtml, performanceFactSheetInitialSignatureHtml, enrollmentAgreementInitialSignatureHtml, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            string admissionEmailBodyHtml = ViewToHtmlString(this, "_AdmissionEmailBody", admissionModel);
        //            string admissionEmailSubjectText = ViewToHtmlString(this, "_AdmissionEmailSubject", admissionModel);
        //            schoolPrdBLWeb.AdmissionEmail(admissionModel, admissionEmailSubjectText, admissionEmailBodyHtml, signatureHtml, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            if (ModelState.IsValid)
        //            {
        //                admissionModel.ResponseObjectModel = new ResponseObjectModel
        //                {
        //                    ResponseMessages = new List<string>
        //                    {
        //                        "Your Admission has been successfully processed",
        //                        "Please proceed to signing the Catalog & Performance Fact Sheet",
        //                        "Please check your email for information about the course you registered",
        //                    },
        //                };
        //                admissionModel.ClassEnrollModel.FundingRequired = null;
        //                admissionModel.ClassEnrollModel.FundingSoureName = null;
        //                ModelState.Clear();
        //                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: After BL Success!!!");
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", "Error occurred while processing Admission");
        //                admissionModel.ResponseObjectModel.ValidationSummaryMessage = "PLEASE FIX ERRORS TO CONTINUE???";
        //                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00003000 :: After BL Success!!!");
        //            }
        //        }
        //        else
        //        {
        //            long personId = ((SessionObjectModel)Session["SessionObject"]).PersonId;
        //            admissionModel = schoolPrdBLWeb.Admission(personId, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            admissionModel.ClassEnrollModel = classEnrollModel;
        //            admissionModel.ResponseObjectModel.ValidationSummaryMessage = "PLEASE FIX ERRORS TO CONTINUE???";
        //            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00004000 :: ClassEnroll Model Validation Failed");
        //        }
        //        actionResult = PartialView("_AdmissionData", admissionModel);
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        ModelState.Remove("ClassEnrollModel.CancelDate");
        //        ModelState.Remove("ClassEnrollModel.CourseCompletionDate");
        //        ModelState.Remove("ClassEnrollModel.DMVTestDate");
        //        ModelState.AddModelError("", "Admission / POST");
        //        archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        actionResult = PartialView("_Error");
        //    }
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //    if (ModelState.IsValid)
        //    {
        //    }
        //    else
        //    {
        //    }
        //    return actionResult;
        //}

        [AllowAnonymous]
        [HttpGet]
        [Route("Enrollment")]
        public ActionResult Enrollment()
        {
            ViewData["ActionName"] = "Enrollment";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            SchoolPrdBL schoolPrdBL = new SchoolPrdBL();
            ActionResult actionResult;
            try
            {
                //int x = 1, y = 0, z = x / y;
                EnrollmentModel enrollmentModel = schoolPrdBL.Enrollment(Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Enrollment", enrollmentModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "Enrollment / GET");
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error");
            }
            return actionResult;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Enrollment(EnrollmentModel enrollmentModel)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            SchoolPrdBL schoolPrdBL = new SchoolPrdBL();
            bool success;
            string processMessage, htmlString;
            try
            {
                //int x = 1, y = 0, z = x / y;
                ModelState.Clear();
                TryValidateModel(enrollmentModel);
                schoolPrdBL.Enrollment(ref enrollmentModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
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
                archLibBL.GenerateCaptchaQuesion(Session, "CaptchaNumberEnrollment0", "CaptchaNumberEnrollment1");
                enrollmentModel.CaptchaAnswerEnrollment = null;
                enrollmentModel.CaptchaNumberEnrollment0 = Session["CaptchaNumberEnrollment0"].ToString();
                enrollmentModel.CaptchaNumberEnrollment1 = Session["CaptchaNumberEnrollment1"].ToString();
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                enrollmentModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                };
                success = false;
                processMessage = "ERROR???";
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00099100 :: Error Exit");
            }
            htmlString = archLibBL.ViewToHtmlString(this, "_EnrollmentData", enrollmentModel);
            actionResult = Json(new { success, processMessage, htmlString });
            //actionResult = PartialView("_EnrollmentData", enrollmentModel);
            //var xyz = archLibBL.ViewToHtmlString(this, "_EnrollmentData", enrollmentModel);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        [AjaxAuthorize]
        [Authorize]
        [HttpGet]
        public ActionResult Payment()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Programs")]
        public ActionResult Programs()
        {
            ViewData["ActionName"] = "Programs";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            SchoolPrdBL schoolPrdBL = new SchoolPrdBL();
            ActionResult actionResult;
            try
            {
                //int x = 1, y = 0, z = x / y;
                ProgramsModel programsModel = schoolPrdBL.Programs(Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Programs", programsModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "Programs / GET");
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error");
            }
            return actionResult;
        }

        [AjaxAuthorize]
        [Authorize]
        [HttpGet]
        [Route("Signature")]
        public ActionResult Signature()
        {
            ViewData["ActionName"] = "Signature";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ActionResult actionResult;
            ArchLibBL archLibBL = new ArchLibBL();
            SchoolPrdBL schoolPrdBL = new SchoolPrdBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                long personId = ((SessionObjectModel)Session["SessionObject"]).PersonId;
                SignatureModel signatureModel = schoolPrdBL.Signature(personId, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Signature", signatureModel);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                ModelState.AddModelError("", "Signature / GET");
                archLibBL.CopyReponseObjectToModelErrors(ModelState, null, responseObjectModel.ResponseMessages);
                actionResult = View("Error", responseObjectModel);
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        [HttpGet]
        [Route("Tuition")]
        public ActionResult Tuition()
        {
            ViewData["ActionName"] = "Tuition";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            SchoolPrdBL schoolPrdBL = new SchoolPrdBL();
            ActionResult actionResult;
            try
            {
                //int x = 1, y = 0, z = x / y;
                TuitionModel tuitionModel = schoolPrdBL.Tuition(Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Tuition", tuitionModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "Tuition / GET");
                archLibBL.CreateSystemError(ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                actionResult = View("Error");
            }
            return actionResult;
        }
    }
}
