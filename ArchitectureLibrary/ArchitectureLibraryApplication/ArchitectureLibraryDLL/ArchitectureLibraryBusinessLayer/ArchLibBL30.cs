using ArchitectureLibraryCacheBusinessLayer;
using ArchitectureLibraryCacheData;
using ArchitectureLibraryDataLayer;
using ArchitectureLibraryDocumentEnumerations;
using ArchitectureLibraryDocumentService;
using ArchitectureLibraryEmailService;
using ArchitectureLibraryEnumerations;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryTemplate;
using ArchitectureLibraryUtility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace ArchitectureLibraryBusinessLayer
{
    public partial class ArchLibBL
    {
        //AboutUs GET
        public AboutUsModel AboutUs()
        {
            return new AboutUsModel { Temp = "Ummachi Kapathu" };
        }
        //ContactUs GET
        public ContactUsModel ContactUs(Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                GenerateCaptchaQuesion(httpSessionStateBase, "CaptchaNumberContactUs0", "CaptchaNumberContactUs1");
                ContactUsModel contactUsModel = new ContactUsModel
                {
                    CaptchaNumberContactUs0 = httpSessionStateBase["CaptchaNumberContactUs0"].ToString(),
                    CaptchaNumberContactUs1 = httpSessionStateBase["CaptchaNumberContactUs1"].ToString(),
                    ContactUsTypeId = ContactUsTypeEnum.Request,
                };
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return contactUsModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        //ContactUs POST
        public void ContactUs(ref ContactUsModel contactUsModel, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Validate");
                if (ValidateCaptcha(httpSessionStateBase, "CaptchaNumberContactUs0", "CaptchaNumberContactUs1", contactUsModel.CaptchaAnswerContactUs))
                {
                }
                else
                {
                    modelStateDictionary.AddModelError("CaptchaAnswerContactUs", "Incorrect captcha answer");
                }
                if (modelStateDictionary.IsValid)
                {
                    contactUsModel.EmailAddress = contactUsModel.EmailAddress.Trim().ToLower();
                    ArchLibDataContext.OpenSqlConnection();
                    ArchLibDataContext.AddContactUs(contactUsModel, ArchLibDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    contactUsModel.LoginPassword = GenerateRandomKey(9);
                    contactUsModel.UserProfRegistered = !RegisterUserProf(contactUsModel.EmailAddress, contactUsModel.LoginPassword, contactUsModel.ContactUsTelephoneNumber, contactUsModel.FirstName, contactUsModel.LastName, ArchLibDataContext.SqlConnectionObject, out PersonModel personModel, clientId, ipAddress, execUniqueId, loggedInUserId);
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                    string contactUsEmailBodyHtml = ViewToHtmlString(controller, "_ContactUsEmailBody", contactUsModel);
                    string contactUsEmailSubjectHtml = ViewToHtmlString(controller, "_ContactUsEmailSubject", contactUsModel);
                    string signatureHtml = ViewToHtmlString(controller, "_SignatureTemplate", contactUsModel);
                    contactUsEmailBodyHtml += signatureHtml;
                    SendEmail(contactUsModel.EmailAddress, contactUsEmailSubjectHtml, contactUsEmailBodyHtml, null, clientId, ipAddress, execUniqueId, loggedInUserId);
                    contactUsModel = new ContactUsModel
                    {
                        ContactUsTypeId = ContactUsTypeEnum.Request,
                        ResponseObjectModel = new ResponseObjectModel
                        {
                            ColumnCount = 1,
                            ListStyleType = "decimal",
                            ResponseMessages = new List<string>
                            {
                                "Your email has been successfully registered",
                                "Welcome to our family",
                                "Please check your email to complete your registration",
                                "Your email could be in Junk/Spam folder",
                                "If so, mark this email address as not spam",
                            },
                            ResponseTypeId = ResponseTypeEnum.Success,
                            TextAlign = "left",
                        },
                    };
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                }
                else
                {
                    exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00098100 :: Exception", new Exception("Register User Profile Model Errors"), "", "Contact Us Model Errors");
                }
                if (modelStateDictionary.IsValid)
                {

                }
                else
                {
                    contactUsModel.ResponseObjectModel = new ResponseObjectModel
                    {
                        ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                    };
                    MergeModelStateErrorMessages(modelStateDictionary);
                }
                GenerateCaptchaQuesion(httpSessionStateBase, "CaptchaNumberContactUs0", "CaptchaNumberContactUs1");
                contactUsModel.CaptchaNumberContactUs0 = httpSessionStateBase["CaptchaNumberContactUs0"].ToString();
                contactUsModel.CaptchaNumberContactUs1 = httpSessionStateBase["CaptchaNumberContactUs1"].ToString();
                contactUsModel.CaptchaAnswerContactUs = null;
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
                    ArchLibDataContext.CloseSqlConnection();
                }
                catch
                {
                    ;
                }
            }
        }
        //FAQs GET
        public FAQsModel FAQs(Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            return new FAQsModel();
        }
        //LoginUserProf GET
        public LoginUserProfModel LoginUserProf(Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                LoginUserProfModel loginUserProfModel = new LoginUserProfModel
                {
                    ResponseObjectModel = new ResponseObjectModel
                    {
                        ColumnCount = 3,
                        ListStyleType = "decimal",
                        ResponseMessages = new List<string>(),
                        ResponseTypeId = ResponseTypeEnum.Success,
                        TextAlign = "left",
                    },
                };
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return loginUserProfModel;
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
        //LoginUserProf POST
        public SessionObjectModel LoginUserProf(ref LoginUserProfModel loginUserProfModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Validate");
                if (ValidateCaptcha(httpSessionStateBase, "CaptchaNumberLogin0", "CaptchaNumberLogin1", loginUserProfModel.CaptchaAnswerLogin))
                {
                }
                else
                {
                    modelStateDictionary.AddModelError("CaptchaAnswerLogin", "Incorrect captcha answer");
                }
                SessionObjectModel sessionObjectModel;
                if (modelStateDictionary.IsValid)
                {
                    //int x = 1, y = 0, z = x / y;
                    loginUserProfModel.LoginEmailAddress = loginUserProfModel.LoginEmailAddress.Trim().ToLower();
                    ArchLibDataContext.OpenSqlConnection();
                    string privateKey = ArchLibCache.GetPrivateKey(clientId);
                    string loginPassword = EncryptDecrypt.EncryptDataMd5(loginUserProfModel.LoginPassword, privateKey);
                    PersonModel personModel = ArchLibDataContext.SelectLoginUserProf(loginUserProfModel.LoginEmailAddress, ArchLibDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    if (personModel != null)
                    {
                        long passwordExpiry = long.Parse(DateTime.Parse(personModel.AspNetUserModel.PasswordExpiry).ToString("yyyyMMddHHmmss"));
                        if (personModel.AspNetUserModel.LoginPassword == loginPassword && passwordExpiry >= long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")) && personModel.StatusId == GenericStatusEnum.Active && personModel.AspNetUserModel.UserStatusId == UserStatusEnum.Active)
                        {
                            loginUserProfModel.ResponseObjectModel = new ResponseObjectModel
                            {
                                ResponseMessages = new List<string>(),
                                ResponseTypeId = ResponseTypeEnum.Success,
                            };
                            sessionObjectModel = new SessionObjectModel
                            {
                                AspNetRoleId = personModel.AspNetUserModel.AspNetUserRoleModel.AspNetRoleModel.AspNetRoleId,
                                AspNetRoleName = personModel.AspNetUserModel.AspNetUserRoleModel.AspNetRoleModel.AspNetRoleName,
                                ControllerName = personModel.AspNetUserModel.AspNetUserRoleModel.AspNetRoleModel.ControllerName,
                                ActionName = personModel.AspNetUserModel.AspNetUserRoleModel.AspNetRoleModel.ActionName,
                                AspNetUserId = personModel.AspNetUserModel.AspNetUserId,
                                ClientId = clientId,
                                EmailAddress = personModel.AspNetUserModel.Email ?? null,
                                FirstName = personModel.FirstName,
                                InitialsTextId = (long)personModel.InitialsTextId,
                                InitialsTextValue = personModel.InitialsTextValue,
                                LastName = personModel.LastName,
                                LoggedInUserId = personModel.AspNetUserId,
                                NicknameFirst = personModel.NicknameFirst,
                                NicknameLast = personModel.NicknameLast,
                                PersonId = (long)personModel.PersonId,
                                PhoneNumber = personModel.AspNetUserModel.PhoneNumber,
                                SignatureTextId = (long)personModel.SignatureTextId,
                                SignatureTextValue = personModel.SignatureTextValue,
                                UserProfileAdultAge = true,
                                UserProfileUpdated = true,
                            };
                        }
                        else
                        {
                            modelStateDictionary.AddModelError("LoginEmailAddress", "Please check your email address");
                            modelStateDictionary.AddModelError("LoginPassword", "Please check your login password");
                            modelStateDictionary.AddModelError("", "Unable to login with credentials supplied");
                            modelStateDictionary.AddModelError("", "It is likely your password is expired");
                            sessionObjectModel = null;
                        }
                    }
                    else
                    {
                        modelStateDictionary.AddModelError("LoginEmailAddress", "Please check your email address");
                        modelStateDictionary.AddModelError("LoginPassword", "Please check your login password");
                        modelStateDictionary.AddModelError("", "Unable to login with credentials supplied");
                        modelStateDictionary.AddModelError("", "It is likely your password is expired");
                        sessionObjectModel = null;
                    }
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                }
                else
                {
                    sessionObjectModel = null;
                }
                if (modelStateDictionary.IsValid)
                {

                }
                else
                {
                    GenerateCaptchaQuesion(httpSessionStateBase, "CaptchaNumberLogin0", "CaptchaNumberLogin1");
                    loginUserProfModel.CaptchaNumberLogin0 = httpSessionStateBase["CaptchaNumberLogin0"].ToString();
                    loginUserProfModel.CaptchaNumberLogin1 = httpSessionStateBase["CaptchaNumberLogin1"].ToString();
                    loginUserProfModel.CaptchaAnswerLogin = null;
                    MergeModelStateErrorMessages(modelStateDictionary);
                }
                return sessionObjectModel;
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
                    ArchLibDataContext.CloseSqlConnection();
                }
                catch
                {
                    ;
                }
            }
        }
        //PrivacyPolicy GET
        public PrivacyPolicyModel PrivacyPolicy(Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            return new PrivacyPolicyModel();
        }
        //PicGallery GET
        public PicGalleryModel PicGallery(Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            return new PicGalleryModel();
        }
        //Products GET
        public ProductsModel Products(Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            return new ProductsModel();
        }
        //ProductsAndServices GET
        public ProductsAndServicesModel ProductsAndServices(Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            return new ProductsAndServicesModel();
        }
        //RegisterUserLoginUser GET
        public RegisterUserLoginUserModel RegisterUserLoginUser(string queryString, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                modelStateDictionary.Clear();
                RegisterUserLoginUserModel registerUserLoginUserModel = new RegisterUserLoginUserModel
                {
                    ContactUsModel = new ContactUsModel
                    {
                        ContactUsTypeId = ContactUsTypeEnum.Request,
                        ResponseObjectModel = new ResponseObjectModel
                        {
                            ResponseTypeId = ResponseTypeEnum.Info,
                        },
                    },
                    LoginUserProfModel = new LoginUserProfModel
                    {
                        //LoginEmailAddress = "test1@email.com",
                        //LoginPassword = "Login9#9Password",
                        ResponseObjectModel = new ResponseObjectModel
                        {
                            ResponseTypeId = ResponseTypeEnum.Success,
                        },
                    },
                    QueryString = queryString,
                    RegisterUserProfModel = new RegisterUserProfModel
                    {
                        ResponseObjectModel = new ResponseObjectModel
                        {
                            ResponseTypeId = ResponseTypeEnum.Success,
                        },
                    },
                    ResetPasswordModel = new ResetPasswordModel
                    {
                        ResponseObjectModel = new ResponseObjectModel
                        {
                            ResponseTypeId = ResponseTypeEnum.Info,
                        },
                    },
                };
                List<string> numberSessions = new List<string>
                {
                    "CaptchaNumberLogin0",
                    "CaptchaNumberLogin1",
                    "CaptchaNumberRegister0",
                    "CaptchaNumberRegister1",
                    "CaptchaNumberResetPassword0",
                    "CaptchaNumberResetPassword1",
                    "CaptchaNumberContactUs0",
                    "CaptchaNumberContactUs1",
                };
                GenerateCaptchaQuesion(httpSessionStateBase, numberSessions);
                if (registerUserLoginUserModel.ContactUsModel.ResponseObjectModel.ResponseTypeId == ResponseTypeEnum.Error)
                {
                    ;
                }
                else
                {
                    registerUserLoginUserModel.ContactUsModel.CaptchaAnswerContactUs = null;
                    registerUserLoginUserModel.ContactUsModel.CaptchaNumberContactUs0 = httpSessionStateBase["CaptchaNumberContactUs0"].ToString();
                    registerUserLoginUserModel.ContactUsModel.CaptchaNumberContactUs1 = httpSessionStateBase["CaptchaNumberContactUs1"].ToString();
                }
                if (registerUserLoginUserModel.LoginUserProfModel.ResponseObjectModel.ResponseTypeId == ResponseTypeEnum.Error)
                {
                    ;
                }
                else
                {
                    registerUserLoginUserModel.LoginUserProfModel.CaptchaAnswerLogin = null;
                    registerUserLoginUserModel.LoginUserProfModel.CaptchaNumberLogin0 = httpSessionStateBase["CaptchaNumberLogin0"].ToString();
                    registerUserLoginUserModel.LoginUserProfModel.CaptchaNumberLogin1 = httpSessionStateBase["CaptchaNumberLogin1"].ToString();
                }
                if (registerUserLoginUserModel.RegisterUserProfModel.ResponseObjectModel.ResponseTypeId == ResponseTypeEnum.Error)
                {
                    ;
                }
                else
                {
                    registerUserLoginUserModel.RegisterUserProfModel.CaptchaAnswerRegister = null;
                    registerUserLoginUserModel.RegisterUserProfModel.CaptchaNumberRegister0 = httpSessionStateBase["CaptchaNumberRegister0"].ToString();
                    registerUserLoginUserModel.RegisterUserProfModel.CaptchaNumberRegister1 = httpSessionStateBase["CaptchaNumberRegister1"].ToString();
                }
                if (registerUserLoginUserModel.ResetPasswordModel.ResponseObjectModel.ResponseTypeId == ResponseTypeEnum.Error)
                {
                    ;
                }
                else
                {
                    registerUserLoginUserModel.ResetPasswordModel.CaptchaAnswerResetPassword = null;
                    registerUserLoginUserModel.ResetPasswordModel.CaptchaNumberResetPassword0 = httpSessionStateBase["CaptchaNumberResetPassword0"].ToString();
                    registerUserLoginUserModel.ResetPasswordModel.CaptchaNumberResetPassword1 = httpSessionStateBase["CaptchaNumberResetPassword1"].ToString();
                }
                return registerUserLoginUserModel;
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
        //RegisterUserProf GET
        public RegisterUserProfModel RegisterUserProf(HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                GenerateCaptchaQuesion(httpSessionStateBase, "CaptchaNumberRegister0", "CaptchaNumberRegister1");
                RegisterUserProfModel registerUserProfModel = new RegisterUserProfModel
                {
                    CaptchaNumberRegister0 = httpSessionStateBase["CaptchaNumberRegister0"].ToString(),
                    CaptchaNumberRegister1 = httpSessionStateBase["CaptchaNumberRegister1"].ToString(),
                };
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return registerUserProfModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            //try
            //{
            //    RegisterUserProfModel registerUserProfModel = new RegisterUserProfModel();
            //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            //    return registerUserProfModel;
            //}
            //catch (Exception exception)
            //{
            //    exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
            //    throw;
            //}
            finally
            {
            }
        }
        //RegisterUserProf POST
        public void RegisterUserProf(ref RegisterUserProfModel registerUserProfModel, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Validate");
                //int x = 1, y = 0, z = x / y;
                if (ValidateCaptcha(httpSessionStateBase, "CaptchaNumberRegister0", "CaptchaNumberRegister1", registerUserProfModel.CaptchaAnswerRegister))
                {
                }
                else
                {
                    modelStateDictionary.AddModelError("CaptchaAnswerRegister", "Incorrect captcha answer");
                }
                if (modelStateDictionary.IsValid)
                {
                    //int x = 1, y = 0, z = x / y;
                    registerUserProfModel.RegisterEmailAddress = registerUserProfModel.RegisterEmailAddress.Trim().ToLower();
                    registerUserProfModel.ConfirmRegisterEmailAddress = registerUserProfModel.ConfirmRegisterEmailAddress.Trim().ToLower();
                    ArchLibDataContext.OpenSqlConnection();
                    if (string.IsNullOrWhiteSpace(ArchLibDataContext.SelectAspNetUserFromUserName(registerUserProfModel.RegisterEmailAddress, ArchLibDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId).AspNetUserId))
                    {
                        //int x = 1, y = 0, z = x / y;
                        ArchLibDocumentBL archLibDocumentBL = new ArchLibDocumentBL();
                        //string documentCategoryName = "Certificate", documentCategoryDesc = "Certificate";
                        long certificateDocumentId = 0; //archLibDocumentBL.CreateEmptyDocument(documentCategoryName, documentCategoryDesc, ArchLibDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00005000 :: After Certificate Document", "certificateDocumentId", certificateDocumentId.ToString());
                        string aspNetUserId = Guid.NewGuid().ToString();
                        string privateKey = ArchLibCache.GetPrivateKey(clientId);
                        Random random = new Random();
                        string resetPasswordDateTime = DateTime.Now.AddDays(5).ToString("yyyy-MM-dd HH:mm:ss");
                        string resetPasswordKey = GenerateRandomKey(8);
                        registerUserProfModel.ResetPasswordKey = resetPasswordKey;
                        string randomNumber1 = random.Next(0, 999999999).ToString();
                        string randomNumber2 = random.Next(0, 999999999).ToString();
                        string currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmssfffff");
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00003000 :: Before Generating the Password String Query", "Username", registerUserProfModel.RegisterEmailAddress);
                        string resetPasswordQueryString = aspNetUserId + "_" + currentDateTime + "_" + randomNumber1 + "_" + randomNumber2;
                        registerUserProfModel.ResetPasswordQueryString = resetPasswordQueryString;
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001200 :: After Generating the Password Query String ", "Username", registerUserProfModel.RegisterEmailAddress);

                        string firstName = "", lastName = "", initialsTextValue = "", signatureTextValue = "";
                        long initialsTextId = 0, signatureTextId = 0;
                        ArchLibDataContext.CreateRegisterUser(aspNetUserId, registerUserProfModel.RegisterEmailAddress, null, null, registerUserProfModel.TelephoneNumber, resetPasswordQueryString, resetPasswordDateTime, resetPasswordKey, firstName, lastName, certificateDocumentId, initialsTextId, initialsTextValue, signatureTextId, signatureTextValue, UserStatusEnum.Inactive, ArchLibDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                        string registerUserProfEmailBodyHtml = ViewToHtmlString(controller, "_RegisterUserProfEmailBody", registerUserProfModel);
                        string registerUserProfEmailSubjectHtml = ViewToHtmlString(controller, "_RegisterUserProfEmailSubject", registerUserProfModel);
                        string signatureHtml = ViewToHtmlString(controller, "_SignatureTemplate", registerUserProfModel);
                        registerUserProfEmailBodyHtml += signatureHtml;
                        SendEmail(registerUserProfModel.RegisterEmailAddress, registerUserProfEmailSubjectHtml, registerUserProfEmailBodyHtml, null, clientId, ipAddress, execUniqueId, loggedInUserId);
                        registerUserProfModel = new RegisterUserProfModel
                        {
                            ResponseObjectModel = new ResponseObjectModel
                            {
                                ColumnCount = 1,
                                ListStyleType = "decimal",
                                ResponseMessages = new List<string>
                                {
                                    "Your email has been successfully registered",
                                    "Welcome to our family",
                                    "Please check your email to complete your registration",
                                    "Your email could be in Junk/Spam folder",
                                    "If so, mark this email address as not spam",
                                },
                                ResponseTypeId = ResponseTypeEnum.Success,
                                TextAlign = "left",
                            },
                        };
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                    }
                    else
                    {
                        modelStateDictionary.AddModelError("RegisterEmailAddress", "Email address already registered");
                        modelStateDictionary.AddModelError("RegisterEmailAddress", "Try with a different email address");
                        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00098000 :: Exception", new Exception("Email Address already registered"), "", "Email Address already registered");
                    }
                }
                else
                {
                    exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00098100 :: Exception", new Exception("Register User Profile Model Errors"), "", "Register User Profile Model Errors");
                }
                if (modelStateDictionary.IsValid)
                {

                }
                else
                {
                    registerUserProfModel.ResponseObjectModel = new ResponseObjectModel
                    {
                        ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                    };
                    MergeModelStateErrorMessages(modelStateDictionary);
                }
                GenerateCaptchaQuesion(httpSessionStateBase, "CaptchaNumberRegister0", "CaptchaNumberRegister1");
                registerUserProfModel.CaptchaNumberRegister0 = httpSessionStateBase["CaptchaNumberRegister0"].ToString();
                registerUserProfModel.CaptchaNumberRegister1 = httpSessionStateBase["CaptchaNumberRegister1"].ToString();
                registerUserProfModel.CaptchaAnswerRegister = null;
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
                    ArchLibDataContext.CloseSqlConnection();
                }
                catch
                {
                    ;
                }
            }
        }
        //ResetPassword GET
        public ResetPasswordModel ResetPassword(HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                GenerateCaptchaQuesion(httpSessionStateBase, "CaptchaNumberResetPassword0", "CaptchaNumberResetPassword1");
                ResetPasswordModel resetPasswordModel = new ResetPasswordModel
                {
                    CaptchaNumberResetPassword0 = httpSessionStateBase["CaptchaNumberResetPassword0"].ToString(),
                    CaptchaNumberResetPassword1 = httpSessionStateBase["CaptchaNumberResetPassword1"].ToString(),
                };
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return resetPasswordModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            //try
            //{
            //    ResetPasswordModel resetPasswordModel = new ResetPasswordModel();
            //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            //    return resetPasswordModel;
            //}
            //catch (Exception exception)
            //{
            //    exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
            //    throw;
            //}
            finally
            {
            }
        }
        //ResetPassword POST
        public void ResetPassword(ref ResetPasswordModel resetPasswordModel, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Validate");
                if (ValidateCaptcha(httpSessionStateBase, "CaptchaNumberResetPassword0", "CaptchaNumberResetPassword1", resetPasswordModel.CaptchaAnswerResetPassword))
                {
                }
                else
                {
                    modelStateDictionary.AddModelError("CaptchaAnswerResetPassword", "Incorrect captcha answer");
                }
                if (modelStateDictionary.IsValid)
                {
                    resetPasswordModel.ResetPasswordEmailAddress = resetPasswordModel.ResetPasswordEmailAddress.Trim().ToLower();
                    resetPasswordModel.ConfirmResetPasswordEmailAddress = resetPasswordModel.ConfirmResetPasswordEmailAddress.Trim().ToLower();
                    ArchLibDataContext.OpenSqlConnection();
                    string aspNetUserId = ArchLibDataContext.SelectAspNetUserFromUserName(resetPasswordModel.ResetPasswordEmailAddress, ArchLibDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId).AspNetUserId;
                    if (!string.IsNullOrWhiteSpace(aspNetUserId))
                    {
                        //int x = 1, y = 0, z = x / y;
                        string privateKey = ArchLibCache.GetPrivateKey(clientId);
                        Random random = new Random();
                        string resetPasswordExpiryDateTime = DateTime.Now.AddDays(5).ToString("yyyy-MM-dd HH:mm:ss");
                        string resetPasswordKey = GenerateRandomKey(8);
                        string randomNumber1 = random.Next(0, 999999999).ToString();
                        string randomNumber2 = random.Next(0, 999999999).ToString();
                        string currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmssfffff");
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00003000 :: Before Generating the Password String Query", "Username", resetPasswordModel.ResetPasswordEmailAddress);
                        string resetPasswordQueryString = aspNetUserId + "_" + currentDateTime + "_" + randomNumber1 + "_" + randomNumber2;
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001200 :: After Generating the Password Query String ", "Username", resetPasswordModel.ResetPasswordEmailAddress);
                        ArchLibDataContext.ModifyResetPassword(aspNetUserId, resetPasswordQueryString, resetPasswordKey, resetPasswordExpiryDateTime, ArchLibDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00004000 :: After CreateResetPassword", "aspNetUserId", aspNetUserId, "resetPasswordModel.EmailAddress", resetPasswordModel.ConfirmResetPasswordEmailAddress);
                        resetPasswordModel.FirstName = "Valued";
                        resetPasswordModel.LastName = "Customer";
                        resetPasswordModel.ResetPasswordKey = resetPasswordKey;
                        resetPasswordModel.ResetPasswordQueryString = resetPasswordQueryString;
                        string resetPasswordEmailBodyHtml = ViewToHtmlString(controller, "_ResetPasswordEmailBody", resetPasswordModel);
                        string resetPasswordEmailSubjectHtml = ViewToHtmlString(controller, "_ResetPasswordEmailSubject", resetPasswordModel);
                        string signatureHtml = ViewToHtmlString(controller, "_SignatureTemplate", resetPasswordModel);
                        resetPasswordEmailBodyHtml += signatureHtml;
                        SendEmail(resetPasswordModel.ResetPasswordEmailAddress, resetPasswordEmailSubjectHtml, resetPasswordEmailBodyHtml, null, clientId, ipAddress, execUniqueId, loggedInUserId);
                        resetPasswordModel = new ResetPasswordModel
                        {
                            ResponseObjectModel = new ResponseObjectModel
                            {
                                ColumnCount = 1,
                                ListStyleType = "decimal",
                                ResponseMessages = new List<string>
                                {
                                    "Your reset password request is completed successfully!!!",
                                    "Please check your email and follow instructions to update your password",
                                    "Your email could be in Junk/Spam folder",
                                    "If so, mark the sender as safe and move the email to Inbox",
                                },
                                ResponseTypeId = ResponseTypeEnum.Success,
                                TextAlign = "left",
                            },
                        };
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                        ArchLibDataContext.CloseSqlConnection();
                    }
                    else
                    {
                        modelStateDictionary.AddModelError("ResetPasswordEmailAddress", "Email address not found");
                        modelStateDictionary.AddModelError("ResetPasswordEmailAddress", "Try with a different email address");
                        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00098000 :: Exception", new Exception("Email Address already registered"), "", "Email Address already registered");
                    }
                }
                else
                {
                    exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00098100 :: Exception", new Exception("Register User Profile Model Errors"), "", "Register User Profile Model Errors");
                }
                if (modelStateDictionary.IsValid)
                {

                }
                else
                {
                    MergeModelStateErrorMessages(modelStateDictionary);
                }
                GenerateCaptchaQuesion(httpSessionStateBase, "CaptchaNumberResetPassword0", "CaptchaNumberResetPassword1");
                resetPasswordModel.CaptchaNumberResetPassword0 = httpSessionStateBase["CaptchaNumberResetPassword0"].ToString();
                resetPasswordModel.CaptchaNumberResetPassword1 = httpSessionStateBase["CaptchaNumberResetPassword1"].ToString();
                resetPasswordModel.CaptchaAnswerResetPassword = null;
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
                    ArchLibDataContext.CloseSqlConnection();
                }
                catch
                {
                    ;
                }
            }
        }
        //ReturnPolicy GET
        public ReturnPolicyModel ReturnPolicy(Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            return new ReturnPolicyModel();
        }
        //Services GET
        public ServicesModel Services(Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            return new ServicesModel();
        }
        //Testimonials GET
        public TestimonialsModel Testimonials(Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            return new TestimonialsModel();
        }
        //UpdatePassword GET
        public UpdatePasswordModel UpdatePassword(string resetPasswordQueryString, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //int x = 1, y = 0, z = x / y;
                bool success;
                ArchLibDataContext.OpenSqlConnection();
                AspNetUserModel aspNetUserModel = ArchLibDataContext.SelectAspNetUserFromResetPasswordQueryString(resetPasswordQueryString, ArchLibDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (aspNetUserModel == null)
                {
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: AspNetUser Not Found", "resetPasswordQueryString", resetPasswordQueryString);
                    success = false;
                }
                else
                {
                    if (aspNetUserModel.AspNetUserId == resetPasswordQueryString.Substring(0, aspNetUserModel.AspNetUserId.Length))
                    {
                        if (long.Parse(DateTime.Parse(aspNetUserModel.ResetPasswordExpiryDateTime).ToString("yyyyMMddHHmmss")) >= long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")))
                        {
                            success = true;
                        }
                        else
                        {
                            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00004000 :: Invalid ResetPasswordExpiryDateTime", "resetPasswordQueryString", resetPasswordQueryString, "ResetPasswordExpiryDateTime", aspNetUserModel.ResetPasswordExpiryDateTime);
                            success = false;
                        }
                    }
                    else
                    {
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: AspNetUserId Not Matching", "resetPasswordQueryString", resetPasswordQueryString, "AspNetUserId", aspNetUserModel.AspNetUserId);
                        success = false;
                    }
                }
                UpdatePasswordModel updatePasswordModel = new UpdatePasswordModel
                {
                    PasswordStrengthMessages = CreatePasswordStrengthMessages(),
                    ResetPasswordQueryString = resetPasswordQueryString,
                };
                if (success)
                {
                    GenerateCaptchaQuesion(httpSessionStateBase, "CaptchaNumber0", "CaptchaNumber1");
                    updatePasswordModel.CaptchaNumber0 = httpSessionStateBase["CaptchaNumber0"].ToString();
                    updatePasswordModel.CaptchaNumber1 = httpSessionStateBase["CaptchaNumber1"].ToString();
                }
                else
                {
                    modelStateDictionary.AddModelError("", "Your request is either invalid or expired");
                    modelStateDictionary.AddModelError("", "Please use the correct URL to update your password");
                    modelStateDictionary.AddModelError("", "You can try resetting the password one more time");
                    modelStateDictionary.AddModelError("", "Should the problem continue to persist, please contact our support personnel");
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return updatePasswordModel;
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
                }
                catch
                {
                    ;
                }
            }
        }
        //UpdatePassword POST
        public void UpdatePassword(ref UpdatePasswordModel updatePasswordModel, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            //int x = 1, y = 0, z = x / y;
            try
            {
                //int x = 1, y = 0, z = x / y;
                bool success = true;
                if (ValidateCaptcha(httpSessionStateBase, "CaptchaNumber0", "CaptchaNumber1", updatePasswordModel.CaptchaAnswer))
                {
                }
                else
                {
                    success = false;
                    modelStateDictionary.AddModelError("CaptchaAnswer", "Incorrect captcha answer");
                }
                List<string> passwordValidationMessages = CalculatePasswordStrength(updatePasswordModel.LoginPassword, out string passwordStrengthColor, out string passwordStrengthMessage);
                updatePasswordModel.LoginPasswordStrengthColor = passwordStrengthColor;
                updatePasswordModel.LoginPasswordStrengthMessage = passwordStrengthMessage;
                foreach (var passwordValidationMessage in passwordValidationMessages)
                {
                    if (passwordValidationMessage != "")
                    {
                        success = false;
                        modelStateDictionary.AddModelError("", passwordValidationMessage);
                    }
                }
                if (success)
                {
                    updatePasswordModel.EmailAddress = updatePasswordModel.EmailAddress.Trim().ToLower();
                    updatePasswordModel.ConfirmEmailAddress = updatePasswordModel.ConfirmEmailAddress.Trim().ToLower();
                    updatePasswordModel.LoginPassword = updatePasswordModel.LoginPassword.Trim();
                    updatePasswordModel.ConfirmLoginPassword = updatePasswordModel.ConfirmLoginPassword.Trim();
                    updatePasswordModel.ResetPasswordKey = updatePasswordModel.ResetPasswordKey.Trim();
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Before ArchLibDataContext.OpenSqlConnection");
                    ArchLibDataContext.OpenSqlConnection();
                    PersonModel personModel = ArchLibDataContext.SelectPersonFromResetPasswordQueryString(updatePasswordModel.ResetPasswordQueryString, ArchLibDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    DateTime resetPasswordCompletedDateTime = DateTime.Now; //long.Parse(aspNetUser.ResetPasswordCompletedDateTime.Value.ToString("yyyyMMddHHmmss"));
                    AspNetUserModel aspNetUserModel = personModel.AspNetUserModel;
                    if (!success && aspNetUserModel == null)
                    {
                        success = false;
                    }
                    if (success && aspNetUserModel.AspNetUserId == updatePasswordModel.ResetPasswordQueryString.Substring(0, aspNetUserModel.AspNetUserId.Length))
                    {
                    }
                    else
                    {
                        success = false;
                    }
                    if (success && updatePasswordModel.ResetPasswordKey == aspNetUserModel.ResetPasswordKey)
                    {
                    }
                    else
                    {
                        success = false;
                    }
                    if (success && long.Parse(DateTime.Parse(aspNetUserModel.ResetPasswordExpiryDateTime).ToString("yyyyMMddHHmmss")) >= long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")))
                    {
                    }
                    else
                    {
                        success = false;
                    }
                    if (success && updatePasswordModel.EmailAddress == aspNetUserModel.UserName && updatePasswordModel.EmailAddress == aspNetUserModel.Email && string.IsNullOrWhiteSpace(aspNetUserModel.ResetPasswordCompletedDateTime))
                    {
                    }
                    else
                    {
                        success = false;
                    }
                    if (success)
                    {
                        string privateKey = ArchLibCache.GetPrivateKey(clientId);
                        aspNetUserModel.LoginPassword = EncryptDecrypt.EncryptDataMd5(updatePasswordModel.LoginPassword, privateKey); //Encrypt this
                        aspNetUserModel.PasswordExpiry = DateTime.Now.AddDays(180).ToString("yyyy-MM-dd HH:mm:ss");
                        aspNetUserModel.ResetPasswordCompletedDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        aspNetUserModel.UpdUserId = aspNetUserModel.AspNetUserId;
                        aspNetUserModel.UserStatusId = UserStatusEnum.Active;
                        aspNetUserModel.PersonModels = new List<PersonModel>
                        {
                            new PersonModel
                            {
                                StatusId = GenericStatusEnum.Active,
                                UpdUserId = aspNetUserModel.AspNetUserId,
                            }
                        };
                        string aspNetUserId = aspNetUserModel.AspNetUserId;
                        ArchLibDataContext.ModifyUpdatePassword(aspNetUserModel, aspNetUserId, (int)UserStatusEnum.Active, (int)GenericStatusEnum.Active, ArchLibDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                        updatePasswordModel.FirstName = "Valued";
                        updatePasswordModel.LastName = "Customer";
                        string updatePasswordEmailBodyHtml = ViewToHtmlString(controller, "_UpdatePasswordEmailBody", updatePasswordModel);
                        string updatePasswordEmailSubjectHtml = ViewToHtmlString(controller, "_UpdatePasswordEmailSubject", updatePasswordModel);
                        string signatureHtml = ViewToHtmlString(controller, "_SignatureTemplate", updatePasswordModel);
                        updatePasswordEmailBodyHtml += signatureHtml;
                        SendEmail(updatePasswordModel.EmailAddress, updatePasswordEmailSubjectHtml, updatePasswordEmailBodyHtml, null, clientId, ipAddress, execUniqueId, loggedInUserId);
                        updatePasswordModel = new UpdatePasswordModel
                        {
                            ResponseObjectModel = new ResponseObjectModel
                            {
                                ColumnCount = 3,
                                ListStyleType = "decimal",
                                ResponseTypeId = ResponseTypeEnum.Success,
                                ResponseMessages = new List<string>
                                {
                                    "Your password has been updated successfully",
                                    "Please login to make sure the update password worked fine",
                                    "Take advantage of the exotic features after logging in",
                                    "Should you have any questions, please feel free to contact our support personnel",
                                },
                                TextAlign = "left",
                                ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageSuccess,
                            },
                        };
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                        ArchLibDataContext.CloseSqlConnection();
                        return;
                    }
                    else
                    {
                        modelStateDictionary.AddModelError("", "Error occurred while updating password");
                        modelStateDictionary.AddModelError("", "Either the request is expired or you had updated earlier");
                        modelStateDictionary.AddModelError("", "You may try again resetting your passowrd");
                        modelStateDictionary.AddModelError("", "Should the problem persist, please contact our support personnel");
                        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00098000 :: Exception", new Exception("Error occured in Update Password"), "", "");
                    }
                }
                else
                {
                    updatePasswordModel.ResponseObjectModel = new ResponseObjectModel
                    {
                        ColumnCount = 3,
                        ListStyleType = "decimal",
                        ResponseTypeId = ResponseTypeEnum.Success,
                        TextAlign = "left",
                        ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                    };
                }
                GenerateCaptchaQuesion(httpSessionStateBase, "CaptchaNumber0", "CaptchaNumber1");
                updatePasswordModel.CaptchaNumber0 = httpSessionStateBase["CaptchaNumber0"].ToString();
                updatePasswordModel.CaptchaNumber1 = httpSessionStateBase["CaptchaNumber1"].ToString();
                updatePasswordModel.CaptchaAnswer = null;
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
                    ArchLibDataContext.CloseSqlConnection();
                }
                catch
                {
                    ;
                }
            }
        }
        //UserProfile GET
        public PersonModel UserProfile(string aspNetUserId, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ArchLibDataContext.OpenSqlConnection();
                PersonModel personModel = ArchLibDataContext.SelectPersonAspNetUserFromAspNetUserId(aspNetUserId, "CertificateDocument_", ArchLibDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                personModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ResponseTypeId = ResponseTypeEnum.Info,
                };
                return personModel;
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
                    ArchLibDataContext.CloseSqlConnection();
                }
                catch
                {

                }
            }
        }
        //UserProfile POST
        public void UserProfile(ref PersonModel personModel, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                if (modelStateDictionary.IsValid)
                {
                    ArchLibDataContext.OpenSqlConnection();
                    personModel.AspNetUserModel.AspNetUserId = personModel.AspNetUserId;
                    if (personModel.CertificateDocumentHttpPostedFileBase != null)
                    {
                        ArchLibDocumentBL archLibDocumentBL = new ArchLibDocumentBL();
                        if (personModel.CertificateDocumentHttpPostedFileBase != null && personModel.CertificateDocumentHttpPostedFileBase.InputStream != null)
                        {
                            string documentsImagesDirectoryName = Utilities.GetServerMapPath("~/ClientSpecific/" + ArchLibCache.ClientId + "_" + ArchLibCache.ClientName + "/Documents/Upload/");
                            personModel.CertificateDocumentModel.DocumentCategoryName = "Certificate";
                            personModel.CertificateDocumentModel.DocumentDesc = "Certificate Document";
                            personModel.CertificateDocumentModel.DocumentStatusId = ArchitectureLibraryDocumentEnumerations.StatusEnum.Active;
                            personModel.CertificateDocumentModel.DocumentTypeId = DocumentTypeEnum.Upload;
                            personModel.CertificateDocumentModel.DocumentTypeDesc = "Upload";
                            personModel.CertificateDocumentModel.Height = 450;
                            personModel.CertificateDocumentModel.HeightUnit = "px";
                            personModel.CertificateDocumentModel.Width = 225;
                            personModel.CertificateDocumentModel.WidthUnit = "px";
                            archLibDocumentBL.CreateOrUpdateDocument(personModel.CertificateDocumentHttpPostedFileBase, personModel.CertificateDocumentModel, documentsImagesDirectoryName, ArchLibDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                        }
                    }
                    personModel.CertificateDocumentId = personModel.CertificateDocumentModel.DocumentId;
                    personModel.HomeDemogInfoAddressId = personModel.HomeDemogInfoAddressModel.DemogInfoAddressId;
                    ArchLibDataContext.ModifyUserProfile(personModel, ArchLibDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    string userProfEmailBodyHtml = ViewToHtmlString(controller, "_UserProfileEmailBody", personModel);
                    string userProfEmailSubjectHtml = ViewToHtmlString(controller, "_UserProfileEmailSubject", personModel);
                    string signatureHtml = ViewToHtmlString(controller, "_SignatureTemplate", personModel);
                    userProfEmailBodyHtml += signatureHtml;
                    SendEmail(personModel.AspNetUserModel.Email, userProfEmailSubjectHtml, userProfEmailBodyHtml, null, clientId, ipAddress, execUniqueId, loggedInUserId);
                    personModel.ResponseObjectModel = new ResponseObjectModel
                    {
                        ColumnCount = 1,
                        ListStyleType = "decimal",
                        ResponseMessages = new List<string>
                        {
                            "Your profile is updated successfully",
                            "You will receive confirmation email",
                            "If you did not intend to do this, please contact our support staff",
                        },
                        ResponseTypeId = ResponseTypeEnum.Success,
                        TextAlign = "left",
                    };
                }
                else
                {
                    personModel.ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Error,
                        ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                    };
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                modelStateDictionary.AddModelError("", "Error occured while saving");
                personModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ResponseTypeId = ResponseTypeEnum.Error,
                    ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                };
            }
            finally
            {
                try
                {
                    ArchLibDataContext.CloseSqlConnection();
                }
                catch
                {
                }
            }
        }
        //UserProfile EMAIL
        //public void UserProfileEmail(PersonModel personModel, string userProfileEmailSubjectHtml, string userProfileEmailBodyHtml, string signatureHtml, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        userProfileEmailBodyHtml += signatureHtml;
        //        string documentsImagesDirectoryName = Utilities.GetApplicationValue("DocumentsImagesDirectoryName");
        //        List<string> emailAttachmentFileNames = null;
        //        if (!string.IsNullOrWhiteSpace(personModel.CertificateDocumentModel.ServerFileName))
        //        {
        //            emailAttachmentFileNames = new List<string>
        //            {
        //                documentsImagesDirectoryName + @"\" + personModel.CertificateDocumentModel.ServerFileName,
        //            };
        //        }
        //        SendEmail(personModel.AspNetUserModel.Email, userProfileEmailSubjectHtml, userProfileEmailBodyHtml, emailAttachmentFileNames, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        //            ArchLibDataContext.CloseSqlConnection();
        //        }
        //        catch
        //        {
        //            ;
        //        }
        //    }
        //}
        public string ValidateEmailAddress(string emailAddress, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Validate");
                ArchLibDataContext.OpenSqlConnection();
                if (string.IsNullOrWhiteSpace(ArchLibDataContext.SelectAspNetUserFromUserName(emailAddress, ArchLibDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId).AspNetUserId))
                {
                    return "Email address not found. <a href='/RegisterUserLoginUser' style='color: #000000; font-family: Arial; text-decoration: underline;' target='_blank'>Click to <span style='color: #0000ff; font-weight: bold;'>R E G I S T E R</span></a><br />Or use email addres which is already registered";
                }
                else
                {
                    return "";
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
                    ArchLibDataContext.CloseSqlConnection();
                }
                catch
                {
                    ;
                }
            }
        }
        public SessionObjectModel LoginUserProfValidate(ref LoginUserProfModel loginUserProfModel, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Validate");
                SessionObjectModel sessionObjectModel;
                string privateKey = ArchLibCache.GetPrivateKey(clientId);
                loginUserProfModel.LoginPassword = EncryptDecrypt.EncryptDataMd5(loginUserProfModel.LoginPassword, privateKey);
                PersonModel personModel = ArchLibDataContext.SelectLoginUserProf(loginUserProfModel.LoginEmailAddress, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (personModel != null)
                {
                    long passwordExpiry = long.Parse(DateTime.Parse(personModel.AspNetUserModel.PasswordExpiry).ToString("yyyyMMddHHmmss"));
                    if (personModel.AspNetUserModel.LoginPassword == loginUserProfModel.LoginPassword && passwordExpiry >= long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")) && personModel.StatusId == GenericStatusEnum.Active && personModel.AspNetUserModel.UserStatusId == UserStatusEnum.Active)
                    {
                        loginUserProfModel.ResponseObjectModel = new ResponseObjectModel
                        {
                            ResponseMessages = new List<string>(),
                            ResponseTypeId = ResponseTypeEnum.Success,
                        };
                        sessionObjectModel = new SessionObjectModel
                        {
                            AspNetRoleId = personModel.AspNetUserModel.AspNetUserRoleModel.AspNetRoleId,
                            AspNetRoleName = personModel.AspNetUserModel.AspNetUserRoleModel.AspNetRoleModel.Name,
                            AspNetUserId = personModel.AspNetUserModel.AspNetUserId,
                            ClientId = clientId,
                            //EmailAddress = personModel.AspNetUserModel.Email == null ? loginUserProfModel.LoginUserProfDataModel.LoginEmailAddress : personModel.AspNetUserModel.Email,
                            EmailAddress = personModel.AspNetUserModel.Email ?? null, //? loginUserProfModel.LoginUserProfDataModel.LoginEmailAddress : personModel.AspNetUserModel.Email,
                            FirstName = personModel.FirstName,
                            InitialsTextId = (long)personModel.InitialsTextId,
                            InitialsTextValue = personModel.InitialsTextValue,
                            LastName = personModel.LastName,
                            LoggedInUserId = personModel.AspNetUserId,
                            NicknameFirst = personModel.NicknameFirst,
                            NicknameLast = personModel.NicknameLast,
                            PersonId = (long)personModel.PersonId,
                            PhoneNumber = personModel.AspNetUserModel.PhoneNumber,
                            SignatureTextId = (long)personModel.SignatureTextId,
                            SignatureTextValue = personModel.SignatureTextValue,
                            UserProfileAdultAge = true,
                            UserProfileUpdated = true,
                        };
                    }
                    else
                    {
                        modelStateDictionary.AddModelError("LoginEmailAddress", "Please check your email address");
                        modelStateDictionary.AddModelError("LoginPassword", "Please check your login password");
                        modelStateDictionary.AddModelError("", "Unable to login with credentials supplied");
                        modelStateDictionary.AddModelError("", "It is likely your password is expired");
                        sessionObjectModel = null;
                    }
                }
                else
                {
                    modelStateDictionary.AddModelError("LoginEmailAddress", "Please check your email address");
                    modelStateDictionary.AddModelError("LoginPassword", "Please check your login password");
                    modelStateDictionary.AddModelError("", "Unable to login with credentials supplied");
                    modelStateDictionary.AddModelError("", "It is likely your password is expired");
                    sessionObjectModel = null;
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return sessionObjectModel;
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
        //private void SendContactUsEmail(ContactUsModel contactUsModel, bool userProfRegistered, string loginPassword, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{

        //    TemplateBL templateBL = new TemplateBL();
        //    Dictionary<string, string> keywordValues = new Dictionary<string, string>
        //    {
        //        { "@@##FirstName##@@", contactUsModel.FirstName },
        //        { "@@##LastName##@@", contactUsModel.LastName },
        //        { "@@##EmailAddress##@@", contactUsModel.EmailAddress },
        //        { "@@##TelephoneNum##@@", contactUsModel.TelephoneNumber },
        //        { "@@##CommentsRequests##@@", string.IsNullOrWhiteSpace(contactUsModel.CommentsRequests) ? "" : contactUsModel.CommentsRequests },
        //        { "@@##SignatureTemplate##@@",  SignatureTemplate(clientId) },
        //    };
        //    if (userProfRegistered)
        //    {
        //        keywordValues["@@##Register##@@"] = "Your email address is registered Successfully!!!!!. Please use the below credentials.";
        //        keywordValues["@@##LoginDetails##@@"] = "UserName : " + contactUsModel.EmailAddress + "<br />" + "Password : " + loginPassword;
        //    }
        //    else
        //    {
        //        keywordValues["@@##Register##@@"] = "Your email address is already registered!!!!!. Please login with your credentials.";
        //        keywordValues["@@##LoginDetails##@@"] = "";
        //    }
        //    Dictionary<string, string> templateWithData = templateBL.PopulateKeyWords("ContactUsEmailTemplate", keywordValues);
        //    EmailService emailService = new EmailService();
        //    string privateKey = ArchLibCache.GetPrivateKey(clientId);
        //    string emailDirectoryName = Utilities.GetApplicationValue("EmailDirectoryName");
        //    var fromEmailAddress = new KeyValuePair<string, string>(ArchLibCache.GetApplicationDefault(clientId, "FromEmailAddress", ""), ArchLibCache.GetApplicationDefault(clientId, "FromEmailAddressDisplayName", ""));
        //    var toEmailAddresses = new List<KeyValuePair<string, string>>
        //    {
        //        new KeyValuePair<string, string>(contactUsModel.EmailAddress, ""),
        //    };
        //    List<KeyValuePair<string, string>> replyToEmailAddresses = new List<KeyValuePair<string, string>>
        //    {
        //        new KeyValuePair<string, string>(contactUsModel.EmailAddress, ""),
        //        new KeyValuePair<string, string>(ArchLibCache.GetApplicationDefault(clientId, "FromEmailAddress", ""), ArchLibCache.GetApplicationDefault(clientId, "FromEmailAddressDisplayName", "")),
        //    };
        //    List<KeyValuePair<string, string>> ccEmailAddresses = new List<KeyValuePair<string, string>>
        //    {
        //        fromEmailAddress,
        //    };
        //    List<KeyValuePair<string, string>> bccEmailAddresses = new List<KeyValuePair<string, string>>();
        //    try
        //    {
        //        bccEmailAddresses.Add(new KeyValuePair<string, string>(ArchLibCache.GetApplicationDefault(clientId, "BccEmailAddress", ""), ""));
        //    }
        //    catch
        //    {
        //        ;
        //    }
        //    emailService.SendEmail(emailDirectoryName, "", fromEmailAddress, templateWithData["EMAIL_SUBJECT"], templateWithData["EMAIL_BODY"], toEmailAddresses, execUniqueId, privateKey, replyToEmailAddresses, ccEmailAddresses, bccEmailAddresses);
        //}
        //private void SendEmailRegisterUser(string registerEmailAddress, string resetPasswordQueryString, string resetPasswordDateTime, string resetPasswordKey, string aspNetUserId, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    ArchLibCacheBL archLibCacheBL = new ArchLibCacheBL();
        //    try
        //    {
        //        string privateKey = ArchLibCache.GetPrivateKey(clientId);//Get this from Session Object
        //        Dictionary<string, string> keywordValues = new Dictionary<string, string>()
        //        {
        //            { "@@##BaseUrl##@@", ArchLibCache.GetApplicationDefault(clientId, "BaseUrl", "") },
        //            { "@@##ContactUsUrl##@@", ArchLibCache.GetApplicationDefault(0, "ContactUsUrl", "") },
        //            { "@@##ResetPasswordExpiryDateTime##@@", resetPasswordDateTime },
        //            { "@@##UpdatePasswordUrl##@@", ArchLibCache.GetApplicationDefault(0, "UpdatePasswordUrl", "") },
        //            { "@@##ResetPasswordQueryString##@@", resetPasswordQueryString },
        //            { "@@##ResetPasswordKey##@@", resetPasswordKey},
        //            { "@@##ContactPhone##@@", ArchLibCache.GetApplicationDefault(clientId, "ContactPhone", "") },
        //            { "@@##AdminRepresentativeName##@@", ArchLibCache.GetApplicationDefault(clientId, "AdminRepresentativeName", "") },
        //            { "@@##CurrentDateTime##@@", DateTime.Now.ToString("MMM-dd-yyyy hh:mm tt") },
        //            { "@@##RegisterEmailAddress##@@", registerEmailAddress },
        //            { "@@##SignatureTemplate##@@", SignatureTemplate(clientId) },
        //        };
        //        TemplateBL templateBL = new TemplateBL();
        //        Dictionary<string, string> templateWithData = templateBL.PopulateKeyWords("RegisterEmailTemplate", keywordValues);
        //        string emailDirectoryName = Utilities.GetApplicationValue("EmailDirectoryName");
        //        var fromEmailAddress = new KeyValuePair<string, string>
        //        (
        //            ArchLibCache.GetApplicationDefault(clientId, "FromEmailAddress", ""),
        //            ArchLibCache.GetApplicationDefault(clientId, "FromEmailAddressDisplayName", "")
        //        );
        //        var toEmailAddresses = new List<KeyValuePair<string, string>>
        //        {
        //            new KeyValuePair<string, string>(registerEmailAddress, ""),
        //        };
        //        List<KeyValuePair<string, string>> ccEmailAddresses = new List<KeyValuePair<string, string>> { fromEmailAddress };
        //        var replyToEmailAddresses = new List<KeyValuePair<string, string>>
        //        {
        //            new KeyValuePair<string, string>(fromEmailAddress.Key, fromEmailAddress.Value),
        //        };
        //        var bccEmailAddresses = new List<KeyValuePair<string, string>>
        //        {
        //            //new KeyValuePair<string, string>(fromEmailAddress, fromEmailAddressDisplayName),
        //        };
        //        EmailService emailService = new EmailService();
        //        emailService.SendEmail(emailDirectoryName, aspNetUserId, fromEmailAddress, templateWithData["EMAIL_SUBJECT"], templateWithData["EMAIL_BODY"], toEmailAddresses, execUniqueId, privateKey, replyToEmailAddresses, ccEmailAddresses, bccEmailAddresses);
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Error Occured", exception);
        //        throw;
        //    }
        //}
        //private void SendEmailResetPassword(string resetPaswwordEmailAddress, string resetPasswordQueryString, string resetPasswordExpiryDateTime, string resetPasswordKey, string aspNetUserId, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    ArchLibCacheBL archLibCacheBL = new ArchLibCacheBL();
        //    try
        //    {
        //        string privateKey = ArchLibCache.GetPrivateKey(clientId);//Get this from Session Object
        //        Dictionary<string, string> keywordValues = new Dictionary<string, string>()
        //        {
        //            { "@@##BaseUrl##@@", ArchLibCache.GetApplicationDefault(clientId, "BaseUrl", "") },
        //            { "@@##ContactUsUrl##@@", ArchLibCache.GetApplicationDefault(0, "ContactUsUrl", "") },
        //            { "@@##ResetPasswordExpiryDateTime##@@", resetPasswordExpiryDateTime },
        //            { "@@##UpdatePasswordUrl##@@", ArchLibCache.GetApplicationDefault(0, "UpdatePasswordUrl", "") },
        //            { "@@##ResetPasswordQueryString##@@", resetPasswordQueryString },
        //            { "@@##ResetPasswordKey##@@", resetPasswordKey},
        //            { "@@##ContactPhone##@@", ArchLibCache.GetApplicationDefault(clientId, "ContactPhone", "") },
        //            { "@@##AdminRepresentativeName##@@", ArchLibCache.GetApplicationDefault(clientId, "AdminRepresentativeName", "") },
        //            { "@@##CurrentDateTime##@@", DateTime.Now.ToString("MMM-dd-yyyy hh:mm tt") },
        //            { "@@##RegisterEmailAddress##@@", resetPaswwordEmailAddress },
        //            { "@@##SignatureTemplate##@@", SignatureTemplate(clientId) },
        //        };
        //        TemplateBL templateBL = new TemplateBL();
        //        Dictionary<string, string> templateWithData = templateBL.PopulateKeyWords("ResetPasswordEmailTemplate", keywordValues);
        //        string emailDirectoryName = Utilities.GetApplicationValue("EmailDirectoryName");
        //        var fromEmailAddress = new KeyValuePair<string, string>
        //        (
        //            ArchLibCache.GetApplicationDefault(clientId, "FromEmailAddress", ""),
        //            ArchLibCache.GetApplicationDefault(clientId, "FromEmailAddressDisplayName", "")
        //        );
        //        var toEmailAddresses = new List<KeyValuePair<string, string>>
        //        {
        //            new KeyValuePair<string, string>(resetPaswwordEmailAddress, ""),
        //        };
        //        List<KeyValuePair<string, string>> ccEmailAddresses = new List<KeyValuePair<string, string>> { fromEmailAddress };
        //        var replyToEmailAddresses = new List<KeyValuePair<string, string>>
        //        {
        //            new KeyValuePair<string, string>(fromEmailAddress.Key, fromEmailAddress.Value),
        //        };
        //        var bccEmailAddresses = new List<KeyValuePair<string, string>>
        //        {
        //            //new KeyValuePair<string, string>(fromEmailAddress, fromEmailAddressDisplayName),
        //        };
        //        EmailService emailService = new EmailService();
        //        emailService.SendEmail(emailDirectoryName, aspNetUserId, fromEmailAddress, templateWithData["EMAIL_SUBJECT"], templateWithData["EMAIL_BODY"], toEmailAddresses, ipAddress, execUniqueId, loggedInUserId, privateKey, replyToEmailAddresses, ccEmailAddresses, bccEmailAddresses);
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Error Occured", exception);
        //        throw;
        //    }
        //}
        public bool RegisterUserProf(string registerEmailAddress, string loginPassword, string telephoneNumber, string firstName, string lastName, SqlConnection sqlConnection, out PersonModel personModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Before ApplicationDataContext.OpenSqlConnection");
                AspNetUserModel aspNetUserModel = ArchLibDataContext.SelectAspNetUserFromUserName(registerEmailAddress, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (string.IsNullOrWhiteSpace(aspNetUserModel.AspNetUserId))
                {//User is not registered - create user with password - Do not need reset password - Set the expiry to 30 days
                    string aspNetUserId = Guid.NewGuid().ToString();
                    string privateKey = ArchLibCache.GetPrivateKey(clientId);
                    string loginPasswordEncrypted = EncryptDecrypt.EncryptDataMd5(loginPassword, privateKey);
                    DateTime loginPasswordExpiryDateTime = DateTime.Now.AddDays(180);
                    long certificateDocumentId = 0;
                    personModel = ArchLibDataContext.CreateRegisterUser(aspNetUserId, registerEmailAddress, loginPasswordEncrypted, loginPasswordExpiryDateTime, telephoneNumber, null, null, null, firstName, lastName, certificateDocumentId, 0, "", 0, "", UserStatusEnum.Active, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                    personModel.AspNetUserId = aspNetUserId;
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                    return true;
                }
                else
                {//User is already registered
                    personModel = aspNetUserModel.PersonModel;
                    return false;
                }
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
        //public bool RegisterUserProf(RegisterUserProfModel registerUserProfModel, string loginPassword, string telephoneNumber, string firstName, string lastName, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Before ApplicationDataContext.OpenSqlConnection");
        //        if (string.IsNullOrWhiteSpace(ArchLibDataContext.SelectAspNetUserFromUserName(registerUserProfModel.RegisterEmailAddress, ArchLibDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId).AspNetUserId))
        //        {//User is not registered - create user with password - Do not need reset password - Set the expiry to 30 days
        //            string aspNetUserId = Guid.NewGuid().ToString();
        //            string privateKey = ArchLibCache.GetPrivateKey(clientId);
        //            string loginPasswordEncrypted = EncryptDecrypt.EncryptDataMd5(loginPassword, privateKey);
        //            DateTime loginPasswordExpiryDateTime = DateTime.Now.AddDays(180);
        //            long certificateDocumentId = 0;
        //            ArchLibDataContext.CreateRegisterUser(aspNetUserId, registerUserProfModel.RegisterEmailAddress, loginPasswordEncrypted, loginPasswordExpiryDateTime, telephoneNumber, null, null, null, firstName, lastName, certificateDocumentId, 0, "", 0, "", UserStatusEnum.Active, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //            return true;
        //        }
        //        else
        //        {//User is already registered
        //            return false;
        //        }
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
        //private bool ResetPassword(ResetPasswordModel resetPasswordModel, string loginPassword, string phoneNumber, string firstName, string lastName, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Before ApplicationDataContext.OpenSqlConnection");
        //        if (string.IsNullOrWhiteSpace(ArchLibDataContext.SelectAspNetUserFromUserName(resetPasswordModel.ResetPasswordEmailAddress, execUniqueId).AspNetUserId))
        //        {//User is not registered - create user with password - Do not need reset password - Set the expiry to 30 days
        //            string aspNetUserId = Guid.NewGuid().ToString();
        //            string privateKey = ArchLibCache.GetPrivateKey(clientId);
        //            string loginPasswordEncrypted = EncryptDecrypt.EncryptDataMd5(loginPassword, privateKey);
        //            DateTime loginPasswordExpiryDateTime = DateTime.Now.AddDays(30);
        //            ArchLibDataContext.CreateResetPassword(aspNetUserId, resetPasswordModel.ResetPasswordEmailAddress, loginPasswordEncrypted, loginPasswordExpiryDateTime, phoneNumber, null, null, null, firstName, lastName, "0", "", "0", "", clientId, ipAddress, execUniqueId, loggedInUserId);
        //            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //            return true;
        //        }
        //        else
        //        {//User is already registered
        //            return false;
        //        }
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
        //private void SendEmailUpdatePassword(string toEmailAddress, string firstName, string lastName, string aspNetUserId, long clientId, string execUniqueId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    //SchoolPrdBL schoolPrdBL = new SchoolPrdBL();
        //    try
        //    {
        //        string privateKey = ArchLibCache.GetPrivateKey(clientId);
        //        Dictionary<string, string> keywordValues = new Dictionary<string, string>()
        //        {
        //            { "@@##BaseUrl##@@", ArchLibCache.GetApplicationDefault(clientId, "BaseUrl", "") },
        //            { "@@##ContactUsUrl##@@", ArchLibCache.GetApplicationDefault(0, "ContactUsUrl", "") },
        //            { "@@##AdminRepresentativeName##@@", ArchLibCache.GetApplicationDefault(clientId, "AdminRepresentativeName", "") },
        //            { "@@##CurrentDateTime##@@", DateTime.Now.ToShortDateString() },
        //            //{ "@@##UpdatePasswordUrl##@@", RetailAppCacheBusinessObject.GetApplicationDefault(0, "UpdatePasswordUrl", "") },
        //            { "@@##FirstName##@@", firstName },
        //            { "@@##LastName##@@", lastName },
        //            { "@@##ContactPhone##@@", ArchLibCache.GetApplicationDefault(clientId, "ContactPhone", "") },
        //            { "@@##SignatureTemplate##@@", SignatureTemplate(clientId) },
        //        };
        //        TemplateBL templateBL = new TemplateBL();
        //        Dictionary<string, string> templateWithData = templateBL.PopulateKeyWords("UpdatePasswordEmailTemplate", keywordValues);
        //        string emailDirectoryName = Utilities.GetApplicationValue("EmailDirectoryName");
        //        var fromEmailAddress = new KeyValuePair<string, string>
        //        (
        //            ArchLibCache.GetApplicationDefault(clientId, "FromEmailAddress", ""),
        //            ArchLibCache.GetApplicationDefault(clientId, "FromEmailAddressDisplayName", "")
        //        );
        //        var toEmailAddresses = new List<KeyValuePair<string, string>>
        //        {
        //            new KeyValuePair<string, string>(toEmailAddress, ""),
        //            new KeyValuePair<string, string>(fromEmailAddress.Key, fromEmailAddress.Value),
        //        };
        //        var replyToEmailAddresses = new List<KeyValuePair<string, string>>
        //        {
        //            new KeyValuePair<string, string>(fromEmailAddress.Key, fromEmailAddress.Value),
        //        };
        //        var bccEmailAddresses = new List<KeyValuePair<string, string>>
        //        {
        //            //new KeyValuePair<string, string>(fromEmailAddress, fromEmailAddressDisplayName),
        //        };
        //        EmailService emailService = new EmailService();
        //        emailService.SendEmail(emailDirectoryName, aspNetUserId, fromEmailAddress, templateWithData["EMAIL_SUBJECT"], templateWithData["EMAIL_BODY"], toEmailAddresses, execUniqueId, privateKey, replyToEmailAddresses, null, bccEmailAddresses);
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Error Occured", exception);
        //        throw;
        //    }
        //}
        private List<string> CreatePasswordStrengthMessages()
        {
            List<string> passwordStrengthMessages = new List<string>
            {
                "Password should be 9 to 18 characters long",
                "Password should contain uppercase A to Z",
                "Password should contain lowercase a to z",
                "Password should contain number 0 to 9",
                "Password should contain special characters !#$%^&*()",
            };
            return passwordStrengthMessages;
        }
        private List<string> CalculatePasswordStrength(string loginPassword, out string passwordStrengthColor, out string passwordStrengthMessage)
        {
            loginPassword = string.IsNullOrEmpty(loginPassword) ? "" : loginPassword;
            List<string> passwordValidationMessages = new List<string>();
            int i;
            for (i = 0; i < 5; i++)
            {
                passwordValidationMessages.Add("");
            }
            short strengthCounter = 0;
            string[] matchedCases = { "[A-Z]", "[a-z]", "[0-9]", "[!#$%^&*()]" };
            if (loginPassword.Length >= 9)
            {
                strengthCounter++;
            }
            else
            {
                passwordValidationMessages[0] = "Password should be 9 to 18 characters long";
            }
            Regex regex;
            i = 0;
            foreach (var matchedCase in matchedCases)
            {
                i++;
                regex = new Regex(matchedCase);
                if (regex.IsMatch(loginPassword))
                {
                    strengthCounter++;
                }
                else
                {
                    switch (i)
                    {
                        case 1:
                            passwordValidationMessages[i] = "Password should contain uppercase A to Z";
                            break;
                        case 2:
                            passwordValidationMessages[i] = "Password should contain lowercase a to z";
                            break;
                        case 3:
                            passwordValidationMessages[i] = "Password should contain number 0 to 9";
                            break;
                        case 4:
                            passwordValidationMessages[i] = "Password should contain special characters !#$%^&*()";
                            break;
                        default:
                            break;
                    }
                }
            }
            switch (strengthCounter)
            {
                default:
                case 0:
                case 1:
                case 2:
                    passwordStrengthColor = "red";
                    passwordStrengthMessage = "Very Wesk";
                    break;
                case 3:
                    passwordStrengthColor = "orange";
                    passwordStrengthMessage = "Medium";
                    break;
                case 4:
                    passwordStrengthColor = "navy";
                    passwordStrengthMessage = "Strong";
                    break;
                case 5:
                    passwordStrengthColor = "green";
                    passwordStrengthMessage = "Excellent";
                    break;
            }
            return passwordValidationMessages;
        }
    }
}
