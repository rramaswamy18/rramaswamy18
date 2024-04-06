using ArchitectureLibraryBusinessLayer;
using ArchitectureLibraryCacheData;
using ArchitectureLibraryDataLayer;
using ArchitectureLibraryEmailService;
using ArchitectureLibraryEnumerations;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryPDFLibrary;
using ArchitectureLibraryUtility;
using SchoolPrdCacheData;
using SchoolPrdDataLayer;
using SchoolPrdEnumerations;
using SchoolPrdModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SchoolPrdBusinessLayer
{
    public partial class SchoolPrdBL
    {
        //Admission GET
        public AdmissionModel Admission(Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long personId, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                AdmissionModel admissionModel = new AdmissionModel
                {
                    ClassEnrollModels = ApplicationDataContext.GetClassEnrolls(personId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId),
                    ClassEnrollModel = new ClassEnrollModel
                    {
                        PersonId = personId,
                    },
                };
                return admissionModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            finally
            {
                try
                {
                    ApplicationDataContext.CloseSqlConnection();
                }
                catch
                {

                }
            }
        }
        //ClassEnrolls
        public List<ClassEnrollModel> ClassEnrolls(Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long personId, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            ApplicationDataContext.OpenSqlConnection();
            var classEnrollModels = ApplicationDataContext.GetClassEnrolls(personId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
            ApplicationDataContext.CloseSqlConnection();
            return classEnrollModels;
        }
        //Admission POST
        public void Admission(ref AdmissionModel admissionModel, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                if (modelStateDictionary.IsValid)
                {
                    ApplicationDataContext.OpenSqlConnection();
                    if (admissionModel.ClassEnrollModel.ClassEnrollId == null)
                    {
                        admissionModel.ClassEnrollModel.ClassEnrollStatusId = ClassEnrollStatusEnum.Approved;
                        ApplicationDataContext.AddClassEnroll(admissionModel.ClassEnrollModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                        ApplicationDataContext.AddClassEnrollFeesFromClassFees((long)admissionModel.ClassEnrollModel.ClassEnrollId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    }
                    else
                    {
                    }
                    admissionModel.ClassEnrollModel.PersonModel = ArchLibDataContext.GetPersonAspNetUserFromPersonId(admissionModel.ClassEnrollModel.PersonId.Value, "", ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    admissionModel.ClassEnrollModels = ApplicationDataContext.GetClassEnrolls(admissionModel.ClassEnrollModel.PersonModel.PersonId.Value, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    long classScheduleId = admissionModel.ClassEnrollModel.ClassScheduleId.Value;
                    admissionModel.ClassEnrollModel.ClassScheduleModel = SchoolPrdCache.ClassScheduleModels.First(x => x.ClassScheduleId == classScheduleId);
                    string catalogInitialSignatureHtml, performanceFactSheetInitialSignatureHtml, enrollmentAgreementInitialSignatureHtml;
                    catalogInitialSignatureHtml = archLibBL.ViewToHtmlString(controller, "_CatalogInitialsSignatureTemplate", admissionModel);
                    performanceFactSheetInitialSignatureHtml = archLibBL.ViewToHtmlString(controller, "_PerformanceFactSheetInitialsSignatureTemplate", admissionModel);
                    enrollmentAgreementInitialSignatureHtml = archLibBL.ViewToHtmlString(controller, "_EnrollmentAgreementInitialsSignatureTemplate", admissionModel);
                    string documentsImagesDirectoryName = Utilities.GetServerMapPath("~/ClientSpecific/" + ArchLibCache.ClientId + "_" + ArchLibCache.ClientName + "/Documents/Upload/");
                    AdmissionCreateInitialsSignatureFiles(admissionModel, catalogInitialSignatureHtml, performanceFactSheetInitialSignatureHtml, enrollmentAgreementInitialSignatureHtml, httpSessionStateBase, modelStateDictionary, clientId, ipAddress, execUniqueId, loggedInUserId);
                    string admissionEmailBodyHtml = archLibBL.ViewToHtmlString(controller, "_AdmissionEmailBody", admissionModel);
                    string admissionEmailSubjectHtml = archLibBL.ViewToHtmlString(controller, "_AdmissionEmailSubject", admissionModel);
                    string signatureHtml = archLibBL.ViewToHtmlString(controller, "_SignatureTemplate", admissionModel);
                    admissionEmailBodyHtml += signatureHtml;
                    List<string> emailAttachmentFileNames = new List<string>
                    {
                        documentsImagesDirectoryName + @"\" + admissionModel.ClassEnrollModel.PersonModel.CertificateDocumentModel.ServerFileName,
                    };
                    archLibBL.SendEmail(admissionModel.ClassEnrollModel.PersonModel.AspNetUserModel.Email, admissionEmailSubjectHtml, admissionEmailBodyHtml, emailAttachmentFileNames, clientId, ipAddress, execUniqueId, loggedInUserId);
                    admissionModel.ClassEnrollModel.ClassEnrollId = null;
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                }
                else
                {
                    exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00098100 :: Exception", new Exception("Register User Profile Model Errors"), "", "Admission Save Model Errors");
                }
                if (modelStateDictionary.IsValid)
                {

                }
                else
                {
                    admissionModel.ResponseObjectModel = new ResponseObjectModel
                    {
                        ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                    };
                    archLibBL.MergeModelStateErrorMessages(modelStateDictionary);
                }

            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            finally
            {
                try
                {
                    ApplicationDataContext.CloseSqlConnection();
                }
                catch
                {

                }
            }
        }
        //Admission EMAIL
        //private void AdmissionEmail(AdmissionModel admissionModel, string admissionEmailSubjectText, string admissionEmailBodyHtml, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        //public void AdmissionEmail(AdmissionModel admissionModel, string admissionEmailSubjectText, string admissionEmailBodyHtml, string signatureHtml, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        string documentsImagesDirectoryName = Utilities.GetApplicationValue("DocumentsImagesDirectoryName");
        //        admissionEmailBodyHtml += signatureHtml;
        //        List<string> emailAttachmentFileNames = new List<string>
        //        {
        //            documentsImagesDirectoryName + @"\" + admissionModel.ClassEnrollModel.PersonModel.CertificateDocumentModel.ServerFileName,
        //        };
        //        SendEmail(admissionModel.ClassEnrollModel.PersonModel.AspNetUserModel.Email, admissionEmailSubjectText, admissionEmailBodyHtml, emailAttachmentFileNames, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        //Agreement GET
        //public SignatureModel Agreement(long personId, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        //long classEnrollId = 1; //Get it from database with person id. We will assume only one class per person for now
        //        ApplicationDataContext.OpenSqlConnection();
        //        ClassEnrollModel classEnrollModel = ApplicationDataContext.GetClassEnrollFromPersonId(personId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        string documentImagesDirectoryName = Utilities.GetApplicationValue("DocumentsImagesDirectoryName") + @"\InitialsSignature_" + classEnrollModel.ClassEnrollId + @"\";
        //        SignatureModel signatureModel = new SignatureModel
        //        {
        //            ClassEnrollId = classEnrollModel.ClassEnrollId,
        //            PostMethod = "Agreement",
        //            ClassEnrollModel = new ClassEnrollModel
        //            {
        //                //ClassScheduleId = 7,
        //                PersonId = personId,
        //            },
        //            ServerDateTimeBase = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
        //            SignatureDataModels = new List<SignatureDataModel>(),
        //            ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ResponseMessages = new List<string>(),
        //                ResponseTypeId = ResponseTypeEnum.Info,
        //            }
        //        };
        //        foreach (var initialSignatureModel in SchoolPrdCache.InitialSignatureModels)
        //        {
        //            if (initialSignatureModel.DocumentTypeNameDesc == "EnrollmentAgreement")
        //            {
        //                signatureModel.SignatureDataModels.Add
        //                (
        //                    new SignatureDataModel
        //                    {
        //                        ClientDateTimes = new List<string>(new string[initialSignatureModel.InitialSignatureDetailModels.Count - 1]),
        //                        DocumentTypeNameDesc = initialSignatureModel.DocumentTypeNameDesc,
        //                        HtmlContent = LoadSignatureHtmlContent(documentImagesDirectoryName, initialSignatureModel.DocumentTypeNameDesc + "_" + classEnrollModel.ClassEnrollId + ".html"),
        //                        InitialSignatureDetailIds = GetInitialSignatureDetailIds(initialSignatureModel.InitialSignatureDetailModels, clientId, ipAddress, execUniqueId, loggedInUserId),
        //                        InitialSignatureSignedCount = 0,
        //                        InitialSignatureTotalCount = initialSignatureModel.InitialSignatureDetailModels.Count - 1,
        //                        TabName = initialSignatureModel.TabName,
        //                    }
        //                );
        //            }
        //        }
        //        return signatureModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            ApplicationDataContext.CloseSqlConnection();
        //        }
        //        catch
        //        {

        //        }
        //    }
        //}
        //Agreement POST
        public void Agreement(ref SignatureModel signatureModel, ModelStateDictionary modelStateDictionary, SessionObjectModel sessionObjectModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //signatureModel.ClassEnrollModel.ClassEnrollId = signatureModel.ClassEnrollId;
                ApplicationDataContext.OpenSqlConnection();
                signatureModel.PostMethod = "Agreement";
                signatureModel.ClassEnrollModel = ApplicationDataContext.GetClassEnroll(signatureModel.ClassEnrollId.Value, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                ApplicationDataContext.CreateAgreement(signatureModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                string documentImagesDirectoryName = Utilities.GetApplicationValue("DocumentsImagesDirectoryName") + @"\InitialsSignature_" + signatureModel.ClassEnrollId + @"\";
                signatureModel.ClassEnrollModel.PersonModel = ArchLibDataContext.GetPersonAspNetUserFromPersonId((long)signatureModel.ClassEnrollModel.PersonId, "", ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                long classScheduleId = (long)signatureModel.ClassEnrollModel.ClassScheduleId;
                signatureModel.ClassEnrollModel.ClassScheduleModel = SchoolPrdCache.ClassScheduleModels.First(x => x.ClassScheduleId == classScheduleId);
                //CreateEnrollmentAgreementHtmlFiles(signatureModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                SendEnrollmentAgreementEmail(signatureModel.ClassEnrollModel, sessionObjectModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                foreach (var signatureDataModel in signatureModel.SignatureDataModels)
                {
                    signatureDataModel.HtmlContent = LoadSignatureHtmlContent(documentImagesDirectoryName, signatureDataModel.DocumentTypeNameDesc + "_" + signatureModel.ClassEnrollId + ".html");
                    signatureDataModel.TabName = "Tab";
                }
                signatureModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ColumnCount = 2,
                    ResponseMessages = new List<string>
                    {
                        "Congratulations!!! You are successfully registered",
                        "Please check your email for the confirmation",
                        "Reach out to our staff for further assistance",
                    },
                    ResponseTypeId = ResponseTypeEnum.Success,
                };
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
            }
            finally
            {
                try
                {
                    ApplicationDataContext.CloseSqlConnection();
                }
                catch
                {

                }
            }
        }
        //Enrollment GET
        public EnrollmentModel Enrollment(HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ArchLibBL archLibBL = new ArchLibBL();
                archLibBL.GenerateCaptchaQuesion(httpSessionStateBase, "CaptchaNumberEnrollment0", "CaptchaNumberEnrollment1");
                EnrollmentModel enrollmentModel = new EnrollmentModel
                {
                    CaptchaNumberEnrollment0 = httpSessionStateBase["CaptchaNumberEnrollment0"].ToString(),
                    CaptchaNumberEnrollment1 = httpSessionStateBase["CaptchaNumberEnrollment1"].ToString(),
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseMessages = new List<string>(),
                        ResponseTypeId = ResponseTypeEnum.Info,
                    },
                };
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return enrollmentModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        //Enrollment POST
        public void Enrollment(ref EnrollmentModel enrollmentModel, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x1 = 1, y1 = 0, z1 = x1 / y1;
                ArchLibBL archLibBL = new ArchLibBL();
                if (archLibBL.ValidateCaptcha(httpSessionStateBase, "CaptchaNumberEnrollment0", "CaptchaNumberEnrollment1", enrollmentModel.CaptchaAnswerEnrollment))
                {
                }
                else
                {
                    modelStateDictionary.AddModelError("CaptchaAnswerEnrollment", "Incorrect captcha answer");
                }
                if (modelStateDictionary.IsValid)
                {
                    //int x1 = 1, y1 = 0, z1 = x1 / y1;
                    ApplicationDataContext.OpenSqlConnection();
                    enrollmentModel.EnrollmentTypeIds = "";
                    enrollmentModel.EnrollmentTypeDescs = "";
                    List<CodeDataModel> codeDataModels = LookupCache.CodeTypeModels.First(x => x.CodeTypeNameDesc == "EnrollmentType").CodeDataModelsCodeDataNameId;
                    string enrollmentTypeDesc;
                    foreach (var x in enrollmentModel.EnrollmentTypeIdList)
                    {
                        enrollmentModel.EnrollmentTypeIds += x + ",";
                        enrollmentTypeDesc = "";
                        foreach (var y in codeDataModels)
                        {
                            if (y.CodeDataNameId.ToString() == x)
                            {
                                enrollmentTypeDesc = y.CodeDataDesc0;
                            }
                        }
                        enrollmentModel.EnrollmentTypeDescs += enrollmentTypeDesc + Environment.NewLine;
                    }
                    enrollmentModel.EnrollmentTypeIds = enrollmentModel.EnrollmentTypeIds.Substring(0, enrollmentModel.EnrollmentTypeIds.Length - 1);
                    enrollmentModel.LoginPassword = archLibBL.GenerateRandomKey(9);
                    enrollmentModel.UserProfRegistered = !archLibBL.RegisterUserProf(enrollmentModel.EmailAddress, enrollmentModel.LoginPassword, 236, enrollmentModel.TelephoneNumber, enrollmentModel.FirstName, enrollmentModel.LastName, ApplicationDataContext.SqlConnectionObject, out PersonModel personModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                    ApplicationDataContext.AddEnrollment(enrollmentModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    string enrollmentEmailBodyHtml = archLibBL.ViewToHtmlString(controller, "_EnrollmentEmailBody", enrollmentModel);
                    string enrollmentEmailSubjectHtml = archLibBL.ViewToHtmlString(controller, "_EnrollmentEmailSubject", enrollmentModel);
                    string signatureHtml = archLibBL.ViewToHtmlString(controller, "_SignatureTemplate", enrollmentModel);
                    enrollmentEmailBodyHtml += signatureHtml;
                    archLibBL.SendEmail(enrollmentModel.EmailAddress, enrollmentEmailSubjectHtml, enrollmentEmailBodyHtml, null, clientId, ipAddress, execUniqueId, loggedInUserId);
                    enrollmentModel = new EnrollmentModel
                    {
                        ResponseObjectModel = new ResponseObjectModel
                        {
                            ColumnCount = 1,
                            ListStyleType = "decimal",
                            ResponseMessages = new List<string>
                            {
                                "Your are successfully enrolled",
                                "Please check your email for a copy",
                                "Your email could be in Junk/Spam folder",
                                "If so, mark this email address as not spam",
                            },
                            ResponseTypeId = ResponseTypeEnum.Success,
                            TextAlign = "left",
                        },
                    };
                }
                else
                {
                    enrollmentModel.ResponseObjectModel = new ResponseObjectModel
                    {
                        ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                    };
                }
                archLibBL.GenerateCaptchaQuesion(httpSessionStateBase, "CaptchaNumberEnrollment0", "CaptchaNumberEnrollment1");
                enrollmentModel.CaptchaNumberEnrollment0 = httpSessionStateBase["CaptchaNumberEnrollment0"].ToString();
                enrollmentModel.CaptchaNumberEnrollment1 = httpSessionStateBase["CaptchaNumberEnrollment1"].ToString();
                enrollmentModel.CaptchaAnswerEnrollment = null;
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            finally
            {
                try
                {
                    ApplicationDataContext.CloseSqlConnection();
                }
                catch
                {

                }
            }
        }
        //Programs GET
        public ProgramsModel Programs(HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ArchLibBL archLibBL = new ArchLibBL();
                archLibBL.GenerateCaptchaQuesion(httpSessionStateBase, "CaptchaNumberPrograms0", "CaptchaNumberPrograms1");
                ProgramsModel ProgramsModel = new ProgramsModel
                {
                    ClassScheduleModels = SchoolPrdCache.ClassScheduleModels.FindAll(x => DateTime.Parse(x.StartDate).Year >= DateTime.Now.Year).OrderBy(x => x.StartDate).ToList(),
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseMessages = new List<string>(),
                        ResponseTypeId = ResponseTypeEnum.Info,
                    },
                };
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return ProgramsModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        //Tuition GET
        public TuitionModel Tuition(HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ArchLibBL archLibBL = new ArchLibBL();
                archLibBL.GenerateCaptchaQuesion(httpSessionStateBase, "CaptchaNumberTuition0", "CaptchaNumberTuition1");
                TuitionModel TuitionModel = new TuitionModel
                {
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseMessages = new List<string>(),
                        ResponseTypeId = ResponseTypeEnum.Info,
                    },
                };
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return TuitionModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        //Signatute GET
        public SignatureModel Signature(long personId, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                ClassEnrollModel classEnrollModel = ApplicationDataContext.GetClassEnrollFromPersonId(personId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);//10;Get it from database with person id. We will assume only one class per person for now
                string documentImagesDirectoryName = Utilities.GetApplicationValue("DocumentsImagesDirectoryName") + @"\InitialsSignature_" + classEnrollModel.ClassEnrollId + @"\";
                SignatureModel signatureModel = new SignatureModel
                {
                    ClassEnrollId = classEnrollModel.ClassEnrollId,
                    PostMethod = "Signature",
                    ClassEnrollModel = classEnrollModel,
                    ServerDateTimeBase = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    SignatureDataModels = new List<SignatureDataModel>(),
                };
                foreach (var initialSignatureModel in SchoolPrdCache.InitialSignatureModels)
                {
                    if (initialSignatureModel.DocumentTypeNameDesc == "Catalog" || initialSignatureModel.DocumentTypeNameDesc == "PerformanceFactSheet")
                    {
                        signatureModel.SignatureDataModels.Add
                        (
                            new SignatureDataModel
                            {
                                ClientDateTimes = new List<string>(new string[initialSignatureModel.InitialSignatureDetailModels.Count - 1]),
                                DocumentTypeNameDesc = initialSignatureModel.DocumentTypeNameDesc,
                                HtmlContent = LoadSignatureHtmlContent(documentImagesDirectoryName, initialSignatureModel.DocumentTypeNameDesc + "_" + classEnrollModel.ClassEnrollId + ".html"),
                                InitialSignatureDetailIds = GetInitialSignatureDetailIds(initialSignatureModel.InitialSignatureDetailModels, clientId, ipAddress, execUniqueId, loggedInUserId),
                                InitialSignatureSignedCount = 0,
                                InitialSignatureTotalCount = initialSignatureModel.InitialSignatureDetailModels.Count - 1,
                                TabName = initialSignatureModel.TabName,
                            }
                        );
                    }
                }
                return signatureModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            finally
            {
                try
                {
                    ApplicationDataContext.CloseSqlConnection();
                }
                catch
                {
                    ;
                }
            }
        }
        ////ClassFeesList  GET
        //public ClassFeesListModel ClassFeesList(int pageNumber, int rowCount, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ApplicationDataContext.OpenSqlConnection();
        //        ClassFeesListModel classFeesListModel = new ClassFeesListModel
        //        {
        //            ClassFeesModels = ApplicationDataContext.GetClassFeess(ApplicationDataContext.SqlConnectionObject, pageNumber, rowCount, clientId, ipAddress, execUniqueId, loggedInUserId),
        //        };
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //        return classFeesListModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            ApplicationDataContext.CloseSqlConnection();
        //        }
        //        catch
        //        {

        //        }
        //    }
        //}
        ////Holiday GET
        //public HolidayModel Holiday(long? holidayId, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        HolidayModel holidayModel;
        //        if (holidayId == null)
        //        {
        //            holidayModel = new HolidayModel
        //            {
        //                ResponseObjectModel = new ResponseObjectModel
        //                {
        //                    ResponseTypeId = ResponseTypeEnum.Info,
        //                },
        //            };
        //        }
        //        else
        //        {
        //            ApplicationDataContext.OpenSqlConnection();
        //            holidayModel = ApplicationDataContext.SelectHoliday((long)holidayId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            holidayModel.ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ResponseTypeId = ResponseTypeEnum.Info,
        //            };
        //            ApplicationDataContext.CloseSqlConnection();
        //        }
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //        return holidayModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //    }
        //}
        ////Holiday POST
        //public void Holiday(ref HolidayModel holidayModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ApplicationDataContext.OpenSqlConnection();
        //        if (holidayModel.HolidayId == null)
        //        {
        //            ApplicationDataContext.CreateHoliday(holidayModel, ipAddress, execUniqueId, loggedInUserId);
        //            holidayModel = new HolidayModel
        //            {
        //                ResponseObjectModel = new ResponseObjectModel
        //                {
        //                    ColumnCount = 3,
        //                    ResponseMessages = new List<string>
        //                    {
        //                        "Enter the below data",
        //                        "\"Holiday \" should be unique",
        //                        "Click the \"Save\" button to save the data",
        //                    },
        //                    ResponseTypeId = ResponseTypeEnum.Info,
        //                },
        //            };
        //        }
        //        else
        //        {
        //            ApplicationDataContext.ModifyHoliday(holidayModel, ipAddress, execUniqueId, loggedInUserId);
        //            holidayModel.ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ColumnCount = 3,
        //                ResponseMessages = new List<string>
        //                {
        //                    "Holiday saved successfully",
        //                    "Continue with the next Holiday",
        //                    "Or move to the List page",
        //                },
        //                ResponseTypeId = ResponseTypeEnum.Success,
        //            };
        //        }
        //        return;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        holidayModel.ResponseObjectModel = new ResponseObjectModel
        //        {
        //            ColumnCount = 3,
        //            ResponseMessages = new List<string>
        //            {
        //                "Error while saving the List",
        //            },
        //            ResponseTypeId = ResponseTypeEnum.Error,
        //        };
        //        throw;
        //    }
        //}
        private void BuildClassScheduleListModel(ref ClassScheduleListModel classScheduleListModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                int i, j;
                ClassScheduleModel classScheduleModel;
                classScheduleListModel.ClassScheduleModels = new List<ClassScheduleModel>();
                for (i = 0; i < 3; i++)
                {
                    for (j = 0; j < SchoolPrdCache.ClassSessionModels.Count; j++)
                    {
                        classScheduleModel = new ClassScheduleModel
                        {
                            ClassScheduleId = -1,
                            ClassSessionId = SchoolPrdCache.ClassSessionModels[j].ClassSessionId,
                            ClassSessionModel = new ClassSessionModel
                            {
                                ClassSessionDesc = SchoolPrdCache.ClassSessionModels[j].ClassSessionDesc,
                            },
                            StatusId = null,//StatusEnum.Active,
                        };
                        classScheduleListModel.ClassScheduleModels.Add(classScheduleModel);
                    }
                }
                for (i = 0; i < SchoolPrdCache.ClassScheduleModels.Count; i++)
                {
                    classScheduleModel = new ClassScheduleModel
                    {
                        ClassScheduleDesc = SchoolPrdCache.ClassScheduleModels[i].ClassScheduleDesc,
                        ClassScheduleId = SchoolPrdCache.ClassScheduleModels[i].ClassScheduleId,
                        ClassSessionId = SchoolPrdCache.ClassScheduleModels[i].ClassSessionModel.ClassSessionId,
                        GraduationDate = SchoolPrdCache.ClassScheduleModels[i].GraduationDate,
                        StartDate = SchoolPrdCache.ClassScheduleModels[i].StartDate,
                        ClassSessionModel = new ClassSessionModel
                        {
                            ClassSessionDesc = SchoolPrdCache.ClassScheduleModels[i].ClassSessionModel.ClassSessionDesc,
                        },
                        StatusId = SchoolPrdCache.ClassScheduleModels[i].StatusId,
                    };
                    classScheduleListModel.ClassScheduleModels.Add(classScheduleModel);
                }
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            finally
            {
            }
        }
        private List<string> GetInitialSignatureDetailIds(List<InitialSignatureDetailModel> initialSignatureDetailModels, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            List<string> initialSignatureDetailIds = new List<string>();
            foreach (var initialSignatureDetailModel in initialSignatureDetailModels)
            {
                initialSignatureDetailIds.Add(initialSignatureDetailModel.InitialSignatureDetailId.ToString());
            }
            initialSignatureDetailIds.RemoveAt(initialSignatureDetailIds.Count - 1);
            return initialSignatureDetailIds;
        }
        private void ValidateClassSchedules(List<ClassScheduleModel> classScheduleModels, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            int i = -1;
            modelStateDictionary.Clear();
            DateTime dateTime;
            foreach (var classScheduleModel in classScheduleModels)
            {
                i++;
                if (classScheduleModel.ClassScheduleId == -1)
                {
                    if (string.IsNullOrWhiteSpace(classScheduleModel.StartDate) && string.IsNullOrWhiteSpace(classScheduleModel.GraduationDate) && classScheduleModel.StatusId == null)
                    {
                    }
                    else
                    {
                        try
                        {
                            dateTime = DateTime.Parse(classScheduleModel.StartDate);
                        }
                        catch
                        {
                            modelStateDictionary.AddModelError("", "Invalid Start Date at " + (i + 1));
                            modelStateDictionary.AddModelError("ClassScheduleModels[" + i + "].StartDate", "Invalid Start Date");
                        }
                        try
                        {
                            dateTime = DateTime.Parse(classScheduleModel.GraduationDate);
                        }
                        catch
                        {
                            modelStateDictionary.AddModelError("", "Invalid Graduation Date at " + (i + 1));
                            modelStateDictionary.AddModelError("ClassScheduleModels[" + i + "].GraduationDate", "Invalid Graduation Date");
                        }
                        if (classScheduleModel.StatusId == null)
                        {
                            modelStateDictionary.AddModelError("", "Invalid Status at " + (i + 1));
                            modelStateDictionary.AddModelError("ClassScheduleModels[" + i + "].StatusId", "Invalid Status");
                        }
                    }
                }
                else
                {

                }
            }
        }
        ////GET Enrollment
        //public EnrollmentModel Enrollment(HttpSessionStateBase sessionObject, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ArchLibBL archLibBL = new ArchLibBL();
        //        archLibBL.GenerateCaptchaQuesion(sessionObject, "CaptchaNumberEnrollment0", "CaptchaNumberEnrollment1");
        //        EnrollmentModel enrollmentModel = new EnrollmentModel
        //        {
        //            CaptchaNumberEnrollment0 = sessionObject["CaptchaNumberEnrollment0"].ToString(),
        //            CaptchaNumberEnrollment1 = sessionObject["CaptchaNumberEnrollment1"].ToString(),
        //        };
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //        return enrollmentModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        ////Enrollment POST
        //public void Enrollment(ref EnrollmentModel enrollmentModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ArchLibBL archLibBL = new ArchLibBL();
        //        if (archLibBL.ValidateCaptcha(httpSessionStateBase, "CaptchaNumberEnrollment0", "CaptchaNumberEnrollment0", enrollmentModel.CaptchaAnswerEnrollment))
        //        {
        //        }
        //        else
        //        {
        //            modelStateDictionary.AddModelError("CaptchaAnswerEnrollment", "Incorrect captcha answer");
        //        }
        //        if (modelStateDictionary.IsValid)
        //        {
        //            string prefixChar = "";
        //            enrollmentModel.EnrollmentTypeIds = "";
        //            enrollmentModel.EnrollmentTypeDescs = "";
        //            if (enrollmentModel.EnrollmentTypeIdList != null)
        //            {
        //                foreach (var enrollmentTypeId in enrollmentModel.EnrollmentTypeIdList)
        //                {
        //                    enrollmentModel.EnrollmentTypeIds += prefixChar + enrollmentTypeId;
        //                    enrollmentModel.EnrollmentTypeDescs += prefixChar + LookupCache.GetCodeDatasForCodeTypeNameDescByCodeDataNameId("EnrollmentType", execUniqueId).First(x => x.CodeDataNameId == long.Parse(enrollmentTypeId)).CodeDataDesc0;
        //                    prefixChar = Environment.NewLine;
        //                }
        //            }
        //            ApplicationDataContext.OpenSqlConnection();
        //            ApplicationDataContext.CreateEnrollment(enrollmentModel, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            RegisterUserProfModel registerUserProfModel = new RegisterUserProfModel
        //            {
        //                RegisterEmailAddress = enrollmentModel.EmailAddress,
        //            };
        //            string loginPassword = archLibBL.GenerateRandomKey(9);
        //            bool userProfRegistered = RegisterUserProf(registerUserProfModel, loginPassword, enrollmentModel.TelephoneNumber, enrollmentModel.FirstName, enrollmentModel.LastName, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            SendEnrollmentEmail(enrollmentModel, userProfRegistered, loginPassword, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            enrollmentModel = new EnrollmentModel
        //            {
        //                ResponseObjectModel = new ResponseObjectModel
        //                {
        //                    ColumnCount = 3,
        //                    ResponseMessages = new List<string>
        //                    {
        //                        "Your enrollment information is successfully saved in the database",
        //                        "Please check your email for a copy of what you submitted",
        //                        "One of our staff will reach out to you",
        //                    },
        //                    ResponseTypeId = ResponseTypeEnum.Success,
        //                },
        //            };
        //            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //        }
        //        else
        //        {
        //            exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00098100 :: Exception", new Exception("Register User Profile Model Errors"), "", "Register User Profile Model Errors");
        //        }
        //        if (modelStateDictionary.IsValid)
        //        {

        //        }
        //        else
        //        {
        //            archLibBL.MergeModelStateErrorMessages(modelStateDictionary);
        //        }
        //        archLibBL.GenerateCaptchaQuesion(httpSessionStateBase, "CaptchaNumberEnrollment0", "CaptchaNumberEnrollment1");
        //        enrollmentModel.CaptchaNumberEnrollment0 = httpSessionStateBase["CaptchaNumberEnrollment0"].ToString();
        //        enrollmentModel.CaptchaNumberEnrollment1 = httpSessionStateBase["CaptchaNumberEnrollment1"].ToString();
        //        enrollmentModel.CaptchaAnswerEnrollment = null;
        //        return;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            ApplicationDataContext.CloseSqlConnection();
        //        }
        //        catch
        //        {
        //            ;
        //        }
        //    }
        //}
        ////ClassEnrollList GET
        //public ClassEnrollListModel ClassEnrollList(int pageNumber, int rowCount, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ApplicationDataContext.OpenSqlConnection();
        //        ClassEnrollListModel classEnrollListModel = new ClassEnrollListModel
        //        {
        //            //ClassSessionModels = ApplicationDataContext.GetClassSessions(ApplicationDataContext.SqlConnectionObject, pageNumber, rowCount, clientId, ipAddress, execUniqueId, loggedInUserId),
        //            ClassEnrollModels = ApplicationDataContext.GetClassEnrolls(ApplicationDataContext.SqlConnectionObject, pageNumber, rowCount, clientId, ipAddress, execUniqueId, loggedInUserId),
        //        };
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //        return classEnrollListModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            ApplicationDataContext.CloseSqlConnection();
        //        }
        //        catch
        //        {

        //        }
        //    }
        //}
        ////ClassEnroll GET
        //public ClassEnrollModel ClassEnroll(long? classEnrollId, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ApplicationDataContext.OpenSqlConnection();
        //        ClassEnrollModel classEnrollModel;
        //        if (classEnrollId == null)
        //        {
        //            classEnrollModel = new ClassEnrollModel
        //            {
        //                FundingSoureName = "Funding Soure Name",
        //                ClassScheduleId = 1,
        //                PersonId = 1,
        //                PerformanceFactSheetInitialsSignaturesCount = 2,
        //                EnrollmentAgreementInitialsSignaturesCount = 2,
        //                CatalogInitialsSignaturesCount = 2,
        //                FundingRequired = YesNoEnum.Yes,
        //                CancelDate = "02-10-2022",
        //                CourseCompletionDate = "03-04-2022",
        //                DMVTestDate = "10-10-2022",
        //                ClassEnrollStatusId = ClassEnrollStatusEnum.Approved,
        //                ClassEnrollFeesModels = new List<ClassEnrollFeesModel>(),
        //                ResponseObjectModel = new ResponseObjectModel
        //                {
        //                    ResponseTypeId = ResponseTypeEnum.Info,
        //                },
        //            };
        //            foreach (var classFeesModel in SchoolPrdCache.ClassFeesModels)
        //            {
        //                if (classFeesModel.ClassFeesId > 0)
        //                {
        //                    classEnrollModel.ClassEnrollFeesModels.Add
        //                    (
        //                        new ClassEnrollFeesModel
        //                        {
        //                            ClassEnrollFeesId = -1,
        //                            ClassEnrollId = -1,
        //                            ClassFeesId = (long)classFeesModel.ClassFeesId,
        //                            ClassEnrollFeesDesc = classFeesModel.ClassFeesDesc,
        //                            ClassEnrollFeesAmount = classFeesModel.ClassFeesAmount,
        //                            ClassFeesTypeId = classFeesModel.ClassFeesTypeId,
        //                            DueDate = "07-10-2022",
        //                        }
        //                    );
        //                }
        //            }
        //            for (int i = 0; i < 5; i++)
        //            {
        //                classEnrollModel.ClassEnrollFeesModels.Add
        //                (
        //                    new ClassEnrollFeesModel
        //                    {
        //                        ClassEnrollFeesId = -1,
        //                        ClassEnrollId = -1,
        //                        ClassFeesId = -11,
        //                    }
        //                );
        //            }
        //            for (int i = 0; i < 5; i++)
        //            {
        //                classEnrollModel.ClassEnrollFeesModels.Add
        //                (
        //                    new ClassEnrollFeesModel
        //                    {
        //                        ClassEnrollFeesId = -1,
        //                        ClassEnrollId = -1,
        //                        ClassFeesId = -1,
        //                    }
        //                );
        //            }
        //            classEnrollModel.ClassEnrollFeesModels[7].ClassEnrollFeesDesc = "Discount1";
        //            classEnrollModel.ClassEnrollFeesModels[7].DueDate = "07-11-2022";
        //            classEnrollModel.ClassEnrollFeesModels[7].ClassEnrollFeesAmount = 100;
        //            classEnrollModel.ClassEnrollFeesModels[8].ClassEnrollFeesDesc = "Discount2";
        //            classEnrollModel.ClassEnrollFeesModels[8].DueDate = "07-11-2022";
        //            classEnrollModel.ClassEnrollFeesModels[8].ClassEnrollFeesAmount = 200;
        //            classEnrollModel.ClassEnrollFeesModels[12].ClassEnrollFeesDesc = "Fees1";
        //            classEnrollModel.ClassEnrollFeesModels[12].DueDate = "07-11-2022";
        //            classEnrollModel.ClassEnrollFeesModels[12].ClassEnrollFeesAmount = 100;
        //            classEnrollModel.ClassEnrollFeesModels[13].ClassEnrollFeesDesc = "Fees2";
        //            classEnrollModel.ClassEnrollFeesModels[13].DueDate = "07-11-2022";
        //            classEnrollModel.ClassEnrollFeesModels[13].ClassEnrollFeesAmount = 200;
        //        }
        //        else
        //        {
        //            classEnrollModel = ApplicationDataContext.SelectClassEnroll((long)classEnrollId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            classEnrollModel.ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ResponseTypeId = ResponseTypeEnum.Info,
        //            };
        //        }
        //        classEnrollModel.PersonModels = ArchLibDataContext.GetPersons(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        ApplicationDataContext.CloseSqlConnection();
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //        return classEnrollModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //    }
        //}
        ////ClassEnroll POST
        //public void ClassEnroll(ref ClassEnrollModel classEnrollModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ApplicationDataContext.OpenSqlConnection();
        //        ClassFeesModel classFeesModel;
        //        foreach (var classEnrollFeesModel in classEnrollModel.ClassEnrollFeesModels)
        //        {
        //            if (classEnrollFeesModel.ClassFeesId > 0)
        //            {
        //                classFeesModel = SchoolPrdCache.ClassFeesModels.First(x => x.ClassFeesId == classEnrollFeesModel.ClassFeesId);
        //                classEnrollFeesModel.ClassEnrollFeesDesc = classFeesModel.ClassFeesDesc;
        //                classEnrollFeesModel.ClassEnrollFeesAmount = classFeesModel.ClassFeesAmount;
        //                classEnrollFeesModel.ClassFeesTypeId = classFeesModel.ClassFeesTypeId;
        //            }
        //            else
        //            {
        //                break;
        //            }
        //        }
        //        if (classEnrollModel.ClassEnrollId == null)
        //        {
        //            ApplicationDataContext.CreateClassEnroll(classEnrollModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            classEnrollModel = new ClassEnrollModel
        //            {
        //                ResponseObjectModel = new ResponseObjectModel
        //                {
        //                    ColumnCount = 3,
        //                    ResponseMessages = new List<string>
        //                    {
        //                        "Enter the below data",
        //                        "Enroll saved successfully",
        //                        "Continue with the next Enroll",
        //                    },
        //                    ResponseTypeId = ResponseTypeEnum.Info,
        //                },
        //            };
        //        }
        //        else
        //        {
        //            ApplicationDataContext.ModifyClassEnroll(classEnrollModel, ApplicationDataContext.SqlConnectionObject, ipAddress, execUniqueId, loggedInUserId);
        //            classEnrollModel.ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ColumnCount = 3,
        //                ResponseMessages = new List<string>
        //                {
        //                    "Enroll saved successfully",
        //                    "Continue with the next Enroll",
        //                    "Or move to the List page",
        //                },
        //                ResponseTypeId = ResponseTypeEnum.Success,
        //            };
        //        }
        //        classEnrollModel.PersonModels = ArchLibDataContext.GetPersons(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        ApplicationDataContext.CloseSqlConnection();
        //        return;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        classEnrollModel.ResponseObjectModel = new ResponseObjectModel
        //        {
        //            ColumnCount = 3,
        //            ResponseMessages = new List<string>
        //            {
        //                "Error while saving the List",
        //            },
        //            ResponseTypeId = ResponseTypeEnum.Error,
        //        };
        //        throw;
        //    }
        //}
        ////ClassList GET
        //public ClassListModel ClassList(long? classListId, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ClassListModel classListModel;
        //        if (classListId == null)
        //        {
        //            classListModel = new ClassListModel
        //            {
        //                ResponseObjectModel = new ResponseObjectModel
        //                {
        //                    ResponseTypeId = ResponseTypeEnum.Info,
        //                },
        //            };
        //        }
        //        else
        //        {
        //            ApplicationDataContext.OpenSqlConnection();
        //            //classListModel = ApplicationDataContext.SelectClassList((long)classListId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            classListModel = SchoolPrdCache.ClassListModels.First(x => x.ClassListId == classListId);
        //            classListModel.ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ResponseTypeId = ResponseTypeEnum.Info,
        //            };
        //            ApplicationDataContext.CloseSqlConnection();
        //        }
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //        return classListModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //    }
        //}
        ////ClassList POST
        //public ClassListModel ClassList(ref ClassListModel classListModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ApplicationDataContext.OpenSqlConnection();
        //        if (classListModel.ClassListId == null)
        //        {
        //            ApplicationDataContext.CreateClassList(classListModel, ipAddress, execUniqueId, loggedInUserId);
        //            var classListModelTemp = ApplicationDataContext.SelectClassList((long)classListModel.ClassListId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            classListModelTemp.ClassFeesModels = new List<ClassFeesModel>();
        //            classListModelTemp.ClassSessionModels = new List<ClassSessionModel>();
        //            SchoolPrdCache.ClassListModels.Add(classListModelTemp);
        //            classListModel = new ClassListModel
        //            {
        //                ResponseObjectModel = new ResponseObjectModel
        //                {
        //                    ColumnCount = 3,
        //                    ResponseMessages = new List<string>
        //                    {
        //                        "Enter the below data",
        //                        "\"Class List \" should be unique",
        //                        "Click the \"Save\" button to save the data",
        //                    },
        //                    ResponseTypeId = ResponseTypeEnum.Info,
        //                },
        //            };
        //        }
        //        else
        //        {
        //            ApplicationDataContext.ModifyClassList(classListModel, ipAddress, execUniqueId, loggedInUserId);
        //            var classListId = classListModel.ClassListId;
        //            var classListModelTemp = SchoolPrdCache.ClassListModels.First(x => x.ClassListId == classListId);//ApplicationDataContext.SelectClassList((long)classListModel.ClassListId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            classListModelTemp.ClassListDesc = classListModel.ClassListDesc;
        //            classListModel.ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ColumnCount = 3,
        //                ResponseMessages = new List<string>
        //                {
        //                    "Class List saved successfully",
        //                    "Continue with the next Class List",
        //                    "Or move to the List page",
        //                },
        //                ResponseTypeId = ResponseTypeEnum.Success,
        //            };
        //        }
        //        return classListModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        classListModel.ResponseObjectModel = new ResponseObjectModel
        //        {
        //            ColumnCount = 3,
        //            ResponseMessages = new List<string>
        //            {
        //                "Error while saving the List",
        //            },
        //            ResponseTypeId = ResponseTypeEnum.Error,
        //        };
        //        throw;
        //    }
        //}
        //ClassSession GET
        //public ClassSessionModel ClassSession(long? classSessionId, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ApplicationDataContext.OpenSqlConnection();
        //        ClassSessionModel classSessionModel;
        //        if (classSessionId == null)
        //        {
        //            classSessionModel = new ClassSessionModel
        //            {
        //                ResponseObjectModel = new ResponseObjectModel
        //                {
        //                    ResponseTypeId = ResponseTypeEnum.Info,
        //                },
        //            };
        //        }
        //        else
        //        {
        //            classSessionModel = ApplicationDataContext.SelectClassSession((long)classSessionId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            classSessionModel.ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ResponseTypeId = ResponseTypeEnum.Info,
        //            };
        //        }
        //        //classSessionModel.ClassListModels = ApplicationDataContext.GetClassLists(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        ApplicationDataContext.CloseSqlConnection();
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //        return classSessionModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //    }
        //}
        ////ClassSession POST
        //public ClassSessionModel ClassSession(ref ClassSessionModel classSessionModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ApplicationDataContext.OpenSqlConnection();
        //        if (classSessionModel.ClassSessionId == null)
        //        {
        //            ApplicationDataContext.CreateClassSession(classSessionModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            var classSessionModelTemp = ApplicationDataContext.SelectClassSession((long)classSessionModel.ClassSessionId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            classSessionModelTemp.ClassDetailModels = new List<ClassDetailModel>();
        //            classSessionModelTemp.ClassScheduleModels = new List<ClassScheduleModel>();
        //            SchoolPrdCache.ClassSessionModels.Add(classSessionModelTemp);
        //            classSessionModel = new ClassSessionModel
        //            {
        //                ResponseObjectModel = new ResponseObjectModel
        //                {
        //                    ColumnCount = 3,
        //                    ResponseMessages = new List<string>
        //                    {
        //                        "Enter the below data",
        //                        "\"Class Session \" should be unique",
        //                        "Click the \"Save\" button to save the data",
        //                    },
        //                    ResponseTypeId = ResponseTypeEnum.Info,
        //                },
        //            };
        //        }
        //        else
        //        {
        //            ApplicationDataContext.ModifyClassSession(classSessionModel, ApplicationDataContext.SqlConnectionObject, ipAddress, execUniqueId, loggedInUserId);
        //            var classSessionId = classSessionModel.ClassSessionId;
        //            var classSessionModelTemp = SchoolPrdCache.ClassSessionModels.First(x => x.ClassSessionId == classSessionId); //ApplicationDataContext.SelectClassSession((long)classSessionModel.ClassSessionId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            classSessionModelTemp.ClassSessionDesc = classSessionModel.ClassSessionDesc;
        //            classSessionModelTemp.ClassListModel = SchoolPrdCache.ClassListModels.First(x => x.ClassListId == classSessionModelTemp.ClassListId);
        //            classSessionModel.ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ColumnCount = 3,
        //                ResponseMessages = new List<string>
        //                {
        //                    "Class Session saved successfully",
        //                    "Continue with the next Class Session",
        //                    "Or move to the List page",
        //                },
        //                ResponseTypeId = ResponseTypeEnum.Success,
        //            };
        //        }
        //        //classSessionModel.ClassListModels = ApplicationDataContext.GetClassLists(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        ApplicationDataContext.CloseSqlConnection();
        //        return classSessionModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        classSessionModel.ResponseObjectModel = new ResponseObjectModel
        //        {
        //            ColumnCount = 3,
        //            ResponseMessages = new List<string>
        //            {
        //                "Error while saving the List",
        //            },
        //            ResponseTypeId = ResponseTypeEnum.Error,
        //        };
        //        throw;
        //    }
        //}
        ////ClassSchedule GET
        //public ClassScheduleModel ClassSchedule(long? classScheduleId, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ApplicationDataContext.OpenSqlConnection();
        //        ClassScheduleModel classScheduleModel;
        //        if (classScheduleId == null)
        //        {
        //            classScheduleModel = new ClassScheduleModel
        //            {
        //                ResponseObjectModel = new ResponseObjectModel
        //                {
        //                    ResponseTypeId = ResponseTypeEnum.Info,
        //                },
        //            };
        //        }
        //        else
        //        {
        //            classScheduleModel = ApplicationDataContext.SelectClassSchedule((long)classScheduleId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            classScheduleModel.ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ResponseTypeId = ResponseTypeEnum.Info,
        //            };
        //        }
        //        classScheduleModel.ClassSessionModels = ApplicationDataContext.GetClassSessions(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        ApplicationDataContext.CloseSqlConnection();
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //        return classScheduleModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //    }
        //}
        ////ClassSchedule POST
        //public ClassScheduleModel ClassSchedule(ref ClassScheduleModel classScheduleModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ApplicationDataContext.OpenSqlConnection();
        //        if (classScheduleModel.ClassScheduleId == null)
        //        {
        //            ApplicationDataContext.CreateClassSchedule(classScheduleModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            var classScheduleModelTemp = ApplicationDataContext.SelectClassSchedule((long)classScheduleModel.ClassScheduleId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            classScheduleModelTemp.ClassEnrollModels = new List<ClassEnrollModel>();
        //            classScheduleModelTemp.ClassSessionModels = new List<ClassSessionModel>();
        //            classScheduleModelTemp.ClassListModels = new List<ClassListModel>();
        //            SchoolPrdCache.ClassScheduleModels.Add(classScheduleModelTemp);
        //            classScheduleModel = new ClassScheduleModel
        //            {
        //                ResponseObjectModel = new ResponseObjectModel
        //                {
        //                    ColumnCount = 3,
        //                    ResponseMessages = new List<string>
        //                    {
        //                        "Enter the below data",
        //                        "\"Class Schedule \" should be unique",
        //                        "Click the \"Save\" button to save the data",
        //                    },
        //                    ResponseTypeId = ResponseTypeEnum.Info,
        //                },
        //            };
        //        }
        //        else
        //        {
        //            ApplicationDataContext.ModifyClassSchedule(classScheduleModel, ApplicationDataContext.SqlConnectionObject, ipAddress, execUniqueId, loggedInUserId);
        //            var classScheduleId = classScheduleModel.ClassScheduleId;
        //            var classScheduleModelTemp = SchoolPrdCache.ClassScheduleModels.First(x => x.ClassScheduleId == classScheduleId); //ApplicationDataContext.SelectClassSession((long)classSessionModel.ClassSessionId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            classScheduleModelTemp.ClassScheduleDesc = classScheduleModel.ClassScheduleDesc;
        //            //classScheduleModelTemp.ClassListModel = SchoolPrdCache.ClassListModels.First(x => x.ClassListId == classScheduleModelTemp.ClassListId);
        //            classScheduleModel.ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ColumnCount = 3,
        //                ResponseMessages = new List<string>
        //                {
        //                    "Class Session saved successfully",
        //                    "Continue with the next Class Session",
        //                    "Or move to the List page",
        //                },
        //                ResponseTypeId = ResponseTypeEnum.Success,
        //            };
        //        }
        //        //classScheduleModel.ClassSessionModels = ApplicationDataContext.GetClassSessions(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        ApplicationDataContext.CloseSqlConnection();
        //        return classScheduleModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        classScheduleModel.ResponseObjectModel = new ResponseObjectModel
        //        {
        //            ColumnCount = 3,
        //            ResponseMessages = new List<string>
        //            {
        //                "Error while saving the List",
        //            },
        //            ResponseTypeId = ResponseTypeEnum.Error,
        //        };
        //        throw;
        //    }
        //}
        //ClassFeesList GET 
        ////ClassFees GET
        //public ClassFeesModel ClassFees(long? classFeesId, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ApplicationDataContext.OpenSqlConnection();
        //        ClassFeesModel classFeesModel;
        //        if (classFeesId == null)
        //        {
        //            classFeesModel = new ClassFeesModel
        //            {
        //                ResponseObjectModel = new ResponseObjectModel
        //                {
        //                    ResponseTypeId = ResponseTypeEnum.Info,
        //                },
        //            };
        //        }
        //        else
        //        {
        //            classFeesModel = ApplicationDataContext.SelectClassFees((long)classFeesId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            classFeesModel.ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ResponseTypeId = ResponseTypeEnum.Info,
        //            };
        //        }
        //        classFeesModel.ClassListModels = ApplicationDataContext.GetClassLists(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        ApplicationDataContext.CloseSqlConnection();
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //        return classFeesModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //    }
        //}
        ////ClassFees POST
        //public ClassFeesModel ClassFees(ref ClassFeesModel classFeesModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ApplicationDataContext.OpenSqlConnection();
        //        if (classFeesModel.ClassFeesId == null)
        //        {
        //            ApplicationDataContext.CreateClassFees(classFeesModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            classFeesModel = new ClassFeesModel
        //            {
        //                ResponseObjectModel = new ResponseObjectModel
        //                {
        //                    ColumnCount = 3,
        //                    ResponseMessages = new List<string>
        //                    {
        //                        "Enter the below data",
        //                        "\"ClassFees \" should be unique",
        //                        "Click the \"Save\" button to save the data",
        //                    },
        //                    ResponseTypeId = ResponseTypeEnum.Info,
        //                },
        //            };
        //        }
        //        else
        //        {
        //            ApplicationDataContext.ModifyClassFees(classFeesModel, ApplicationDataContext.SqlConnectionObject, ipAddress, execUniqueId, loggedInUserId);
        //            var classFeesId = classFeesModel.ClassFeesId;
        //            var classFeesModelTemp = SchoolPrdCache.ClassFeesModels.First(x => x.ClassFeesId == classFeesId);
        //            classFeesModelTemp.ClassFeesDesc = classFeesModel.ClassFeesDesc;
        //            classFeesModel.ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ColumnCount = 3,
        //                ResponseMessages = new List<string>
        //                {
        //                    "Class saved successfully",
        //                    "Continue with the next Class Fees",
        //                    "Or move to the List page",
        //                },
        //                ResponseTypeId = ResponseTypeEnum.Success,
        //            };
        //        }
        //        classFeesModel.ClassListModels = ApplicationDataContext.GetClassLists(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        ApplicationDataContext.CloseSqlConnection();
        //        return classFeesModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        classFeesModel.ResponseObjectModel = new ResponseObjectModel
        //        {
        //            ColumnCount = 3,
        //            ResponseMessages = new List<string>
        //            {
        //                "Error while saving the List",
        //            },
        //            ResponseTypeId = ResponseTypeEnum.Error,
        //        };
        //        throw;
        //    }
        //}
        //Holiday GET
        //Payment GET
        //Signatute GET
        //public SignatureModel Signature(long personId, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ApplicationDataContext.OpenSqlConnection();
        //        ClassEnrollModel classEnrollModel = ApplicationDataContext.GetClassEnrollFromPersonId(personId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);//10;Get it from database with person id. We will assume only one class per person for now
        //        string documentImagesDirectoryName = Utilities.GetApplicationValue("DocumentsImagesDirectoryName") + @"\InitialsSignature_" + classEnrollModel.ClassEnrollId + @"\";
        //        SignatureModel signatureModel = new SignatureModel
        //        {
        //            ClassEnrollId = classEnrollModel.ClassEnrollId,
        //            PostMethod = "Signature",
        //            ClassEnrollModel = classEnrollModel,
        //            ServerDateTimeBase = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
        //            SignatureDataModels = new List<SignatureDataModel>(),
        //            ResponseObjectModel = new ResponseObjectModel
        //            {
        //                ResponseMessages = new List<string>(),
        //                ResponseTypeId = ResponseTypeEnum.Info,
        //            }
        //        };
        //        foreach (var initialSignatureModel in SchoolPrdCache.InitialSignatureModels)
        //        {
        //            if (initialSignatureModel.DocumentTypeNameDesc == "Catalog" || initialSignatureModel.DocumentTypeNameDesc == "PerformanceFactSheet")
        //            {
        //                signatureModel.SignatureDataModels.Add
        //                (
        //                    new SignatureDataModel
        //                    {
        //                        ClientDateTimes = new List<string>(new string[initialSignatureModel.InitialSignatureDetailModels.Count - 1]),
        //                        DocumentTypeNameDesc = initialSignatureModel.DocumentTypeNameDesc,
        //                        HtmlContent = LoadSignatureHtmlContent(documentImagesDirectoryName, initialSignatureModel.DocumentTypeNameDesc + "_" + classEnrollModel.ClassEnrollId + ".html"),
        //                        InitialSignatureDetailIds = GetInitialSignatureDetailIds(initialSignatureModel.InitialSignatureDetailModels, clientId, ipAddress, execUniqueId, loggedInUserId),
        //                        InitialSignatureSignedCount = 0,
        //                        InitialSignatureTotalCount = initialSignatureModel.InitialSignatureDetailModels.Count - 1,
        //                        TabName = initialSignatureModel.TabName,
        //                    }
        //                );
        //            }
        //        }
        //        return signatureModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            ApplicationDataContext.CloseSqlConnection();
        //        }
        //        catch
        //        {
        //            ;
        //        }
        //    }
        //}
        ////Signatute POST
        //public void Signature(ref SignatureModel signatureModel, ModelStateDictionary modelStateDictionary, SessionObjectModel sessionObjectModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        signatureModel.ClassEnrollModel.ClassEnrollId = signatureModel.ClassEnrollId;
        //        ApplicationDataContext.OpenSqlConnection();
        //        ApplicationDataContext.CreateSignature(signatureModel, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        string documentImagesDirectoryName = Utilities.GetApplicationValue("DocumentsImagesDirectoryName") + @"\InitialsSignature_" + signatureModel.ClassEnrollId + @"\";
        //        signatureModel.ClassEnrollModel.PersonModel = ArchLibDataContext.GetPersonAspNetUserFromPersonId((long)signatureModel.ClassEnrollModel.PersonId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        long classScheduleId = (long)signatureModel.ClassEnrollModel.ClassScheduleId;
        //        signatureModel.ClassEnrollModel.ClassScheduleModel = SchoolPrdCache.ClassScheduleModels.First(x => x.ClassScheduleId == classScheduleId);
        //        CreateSignatureAgreementHtmlFiles(signatureModel, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        SendSignatureEmail(signatureModel.ClassEnrollModel, sessionObjectModel, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        foreach (var signatureDataModel in signatureModel.SignatureDataModels)
        //        {
        //            signatureDataModel.HtmlContent = LoadSignatureHtmlContent(documentImagesDirectoryName, signatureDataModel.DocumentTypeNameDesc + "_" + signatureModel.ClassEnrollId + ".html");
        //            signatureDataModel.TabName = "Tab";
        //            signatureDataModel.InitialSignatureTotalCount = signatureDataModel.ClientDateTimes.Count;
        //            signatureDataModel.InitialSignatureSignedCount = signatureDataModel.InitialSignatureTotalCount;
        //        }
        //        signatureModel.ResponseObjectModel = new ResponseObjectModel
        //        {
        //            ColumnCount = 2,
        //            ResponseMessages = new List<string>
        //            {
        //                "Congratulations!!! You have successfully signed",
        //                "Please check your email for the confirmation",
        //                "Reach out to our staff for further assistance",
        //            },
        //            ResponseTypeId = ResponseTypeEnum.Success,
        //        };
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            ApplicationDataContext.CloseSqlConnection();
        //        }
        //        catch
        //        {

        //        }
        //    }
        //}
        //public List<ClassListModel> GetClassLists(long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ApplicationDataContext.OpenSqlConnection();
        //        List<ClassListModel> classListModels = ApplicationDataContext.GetClassLists(ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        ApplicationDataContext.CloseSqlConnection();
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //        return classListModels;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        //public List<ClassEnrollModel> GetClassEnrolls(long personId, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        ApplicationDataContext.OpenSqlConnection();
        //        List<ClassEnrollModel> classEnrollModels = ApplicationDataContext.GetClassEnrolls(personId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        ApplicationDataContext.CloseSqlConnection();
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //        return classEnrollModels;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        //private List<string> GetInitialSignatureDetailIds(List<InitialSignatureDetailModel> initialSignatureDetailModels, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    List<string> initialSignatureDetailIds = new List<string>();
        //    foreach (var initialSignatureDetailModel in initialSignatureDetailModels)
        //    {
        //        initialSignatureDetailIds.Add(initialSignatureDetailModel.InitialSignatureDetailId.ToString());
        //    }
        //    initialSignatureDetailIds.RemoveAt(initialSignatureDetailIds.Count - 1);
        //    return initialSignatureDetailIds;
        //}
        public void CatalogGenerateData(string catalogGenerateTemplateHtml, string pdfFileName)
        {
            PDFUtility pDFUtility = new PDFUtility();
            string documentsImagesDirectoryName = Utilities.GetApplicationValue("DocumentsImagesDirectoryName");
            pDFUtility.GeneratePDFFromHtmlString(catalogGenerateTemplateHtml, documentsImagesDirectoryName + @"\" + pdfFileName);
        }
        private void SendEmail(string toEmailAddress, string emailSubjectText, string emailBodyHtml, List<string> emailAttachmentFileNames, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            EmailService emailService = new EmailService();
            string privateKey = ArchLibCache.GetPrivateKey(clientId);
            string emailDirectoryName = Utilities.GetApplicationValue("EmailDirectoryName");
            string documentsImagesDirectoryName = Utilities.GetApplicationValue("DocumentsImagesDirectoryName");
            var fromEmailAddress = new KeyValuePair<string, string>(ArchLibCache.GetApplicationDefault(clientId, "FromEmailAddress", ""), ArchLibCache.GetApplicationDefault(clientId, "FromEmailAddressDisplayName", ""));
            var toEmailAddresses = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(toEmailAddress, ""),
            };
            List<KeyValuePair<string, string>> ccEmailAddresses = new List<KeyValuePair<string, string>>
            {
                fromEmailAddress,
            };
            List<KeyValuePair<string, string>> bccEmailAddresses = new List<KeyValuePair<string, string>>();
            try
            {
                bccEmailAddresses.Add(new KeyValuePair<string, string>(ArchLibCache.GetApplicationDefault(clientId, "BccEmailAddress", ""), ""));
            }
            catch
            {
                ;
            }
            //emailService.SendEmail(emailDirectoryName, "", fromEmailAddress, emailSubjectText, emailBodyHtml, toEmailAddresses, ipAddress, execUniqueId, loggedInUserId, privateKey, null, ccEmailAddresses, bccEmailAddresses, emailAttachmentFileNames);
        }
        //Admission CREATE FILES
        private void AdmissionCreateInitialsSignatureFiles(AdmissionModel admissionModel, string catalogInitialSignatureHtml, string performanceFactSheetInitialSignatureHtml, string enrollmentAgreementInitialSignatureHtml, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            string documentsImagesDirectoryName = Utilities.GetServerMapPath("~/ClientSpecific/" + ArchLibCache.ClientId + "_" + ArchLibCache.ClientName + "/Documents/");
            string imagesDirectoryName = Utilities.GetServerMapPath("~/ClientSpecific/" + ArchLibCache.ClientId + "_" + ArchLibCache.ClientName + "/Documents/Images_Original/");
            //string templatesDirectoryName = Utilities.GetApplicationValue("TemplatesDirectoryName");
            try
            {
                Directory.Delete(documentsImagesDirectoryName + @"\InitialsSignature_" + admissionModel.ClassEnrollModel.ClassEnrollId, true);
            }
            catch
            {
                ;
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Create Directory");
            Directory.CreateDirectory(documentsImagesDirectoryName + @"\InitialsSignature_" + admissionModel.ClassEnrollModel.ClassEnrollId);
            //Banner
            File.Copy(imagesDirectoryName + @"\Image_000.png", documentsImagesDirectoryName + @"\InitialsSignature_" + admissionModel.ClassEnrollModel.ClassEnrollId + @"\Banner.png");
            //Catalog
            string dataFullFileName = documentsImagesDirectoryName + @"\InitialsSignature_" + admissionModel.ClassEnrollModel.ClassEnrollId + @"\Catalog_" + admissionModel.ClassEnrollModel.ClassEnrollId + ".html";
            File.WriteAllText(dataFullFileName, catalogInitialSignatureHtml);
            //Performance Fact Sheet
            dataFullFileName = documentsImagesDirectoryName + @"\InitialsSignature_" + admissionModel.ClassEnrollModel.ClassEnrollId + @"\PerformanceFactSheet_" + admissionModel.ClassEnrollModel.ClassEnrollId + ".html";
            File.WriteAllText(dataFullFileName, performanceFactSheetInitialSignatureHtml);
            //Enrollment Agreement
            dataFullFileName = documentsImagesDirectoryName + @"\InitialsSignature_" + admissionModel.ClassEnrollModel.ClassEnrollId + @"\EnrollmentAgreement_" + admissionModel.ClassEnrollModel.ClassEnrollId + ".html";
            File.WriteAllText(dataFullFileName, enrollmentAgreementInitialSignatureHtml);
        }
    }
}
