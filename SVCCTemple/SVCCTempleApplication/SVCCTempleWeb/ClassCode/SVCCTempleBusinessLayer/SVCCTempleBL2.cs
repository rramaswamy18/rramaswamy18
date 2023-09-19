using ArchitectureLibraryBusinessLayer;
using ArchitectureLibraryCacheData;
using ArchitectureLibraryDataLayer;
using ArchitectureLibraryDocumentService;
using ArchitectureLibraryEnumerations;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using SVCCTempleDataLayer;
using SVCCTempleModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SVCCTempleBusinessLayer
{
    public partial class SVCCTempleBL
    {
        //RegisterUserProf POST
        public void RegisterUserProf(string locationNameDesc, ref RegisterUserProfModel registerUserProfModel, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Validate");
                //int x = 1, y = 0, z = x / y;
                if (archLibBL.ValidateCaptcha(httpSessionStateBase, "CaptchaNumberRegister0", "CaptchaNumberRegister1", registerUserProfModel.CaptchaAnswerRegister))
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
                    SVCCTempleDataContext.OpenSqlConnection();
                    if (!SVCCTempleDataContext.GetLoginUser(locationNameDesc, registerUserProfModel.RegisterEmailAddress, SVCCTempleDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId))
                    {
                        //int x = 1, y = 0, z = x / y;
                        string aspNetUserId = Guid.NewGuid().ToString();
                        string privateKey = ArchLibCache.GetPrivateKey(clientId);
                        Random random = new Random();
                        string resetPasswordDateTime = DateTime.Now.AddDays(5).ToString("yyyy-MM-dd HH:mm:ss");
                        string resetPasswordKey = archLibBL.GenerateRandomKey(8);
                        registerUserProfModel.ResetPasswordKey = resetPasswordKey;
                        string randomNumber1 = random.Next(0, 999999999).ToString();
                        string randomNumber2 = random.Next(0, 999999999).ToString();
                        string currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmssfffff");
                        //exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00003000 :: Before Generating the Password String Query", "Username", registerUserProfModel.RegisterEmailAddress);
                        string resetPasswordQueryString = aspNetUserId + "_" + currentDateTime + "_" + randomNumber1 + "_" + randomNumber2;
                        //registerUserProfModel.ResetPasswordQueryString = resetPasswordQueryString;
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001200 :: After Generating the Password Query String ", "Username", registerUserProfModel.RegisterEmailAddress);

                        var personId = SVCCTempleDataContext.AddPerson(locationNameDesc, registerUserProfModel.RegisterEmailAddress, "12", "", "", "", "17", "1900-01-01", registerUserProfModel.TelephoneNumber, SVCCTempleDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                        if (personId == null)
                        {
                            throw new Exception("Error while registering user (Person)");
                        }
                        var resetPasswordExpiryDateTime = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd HH:mm:ss");
                        var loginUserId = SVCCTempleDataContext.AddLoginUser(locationNameDesc, personId.Value, registerUserProfModel.RegisterEmailAddress, 8, 9, resetPasswordQueryString, resetPasswordExpiryDateTime, resetPasswordKey, aspNetUserId, SVCCTempleDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                        if (loginUserId == null)
                        {
                            throw new Exception("Error while registering user (LoginUser)");
                        }
                        string registerUserProfEmailBodyHtml = archLibBL.ViewToHtmlString(controller, "_RegisterUserProfEmailBody", registerUserProfModel);
                        string registerUserProfEmailSubjectHtml = archLibBL.ViewToHtmlString(controller, "_RegisterUserProfEmailSubject", registerUserProfModel);
                        string signatureHtml = archLibBL.ViewToHtmlString(controller, "_SignatureTemplate", registerUserProfModel);
                        registerUserProfEmailBodyHtml += signatureHtml;
                        archLibBL.SendEmail(registerUserProfModel.RegisterEmailAddress, registerUserProfEmailSubjectHtml, registerUserProfEmailBodyHtml, null, clientId, ipAddress, execUniqueId, loggedInUserId);
                        registerUserProfModel = new RegisterUserProfModel
                        {
                            ResponseObjectModel = new ArchitectureLibraryModels.ResponseObjectModel
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
                    registerUserProfModel.ResponseObjectModel = new ArchitectureLibraryModels.ResponseObjectModel
                    {
                        ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                    };
                    archLibBL.MergeModelStateErrorMessages(modelStateDictionary);
                }
                archLibBL.GenerateCaptchaQuesion(httpSessionStateBase, "CaptchaNumberRegister0", "CaptchaNumberRegister1");
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
                    SVCCTempleDataContext.CloseSqlConnection();
                }
                catch
                {
                    ;
                }
            }
        }
        //UpdatePassword GET
        public UpdatePasswordModel UpdatePassword(string locationNameDesc, string resetPasswordQueryString, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                bool success;
                SVCCTempleDataContext.OpenSqlConnection();
                LoginUserModel loginUserModel = SVCCTempleDataContext.GettLoginUserFromResetPasswordQueryString(locationNameDesc, resetPasswordQueryString, SVCCTempleDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (loginUserModel == null)
                {
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: AspNetUser Not Found", "resetPasswordQueryString", resetPasswordQueryString);
                    success = false;
                }
                else
                {
                    if (loginUserModel.AspNetUserId == resetPasswordQueryString.Substring(0, loginUserModel.AspNetUserId.Length))
                    {
                        if (long.Parse(DateTime.Parse(loginUserModel.ResetPasswordExpiryDateTime).ToString("yyyyMMddHHmmss")) >= long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")))
                        {
                            success = true;
                        }
                        else
                        {
                            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00004000 :: Invalid ResetPasswordExpiryDateTime", "resetPasswordQueryString", resetPasswordQueryString, "ResetPasswordExpiryDateTime", loginUserModel.ResetPasswordExpiryDateTime);
                            success = false;
                        }
                    }
                    else
                    {
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: AspNetUserId Not Matching", "resetPasswordQueryString", resetPasswordQueryString, "AspNetUserId", loginUserModel.AspNetUserId);
                        success = false;
                    }
                }
                UpdatePasswordModel updatePasswordModel = new UpdatePasswordModel
                {
                    PasswordStrengthMessages = archLibBL.CreatePasswordStrengthMessages(),
                    ResetPasswordQueryString = resetPasswordQueryString,
                };
                if (success)
                {
                    archLibBL.GenerateCaptchaQuesion(httpSessionStateBase, "CaptchaNumber0", "CaptchaNumber1");
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
        public void UpdatePassword(string locationNameDesc, ref UpdatePasswordModel updatePasswordModel, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            //int x = 1, y = 0, z = x / y;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                //int x = 1, y = 0, z = x / y;
                bool success = true;
                if (archLibBL.ValidateCaptcha(httpSessionStateBase, "CaptchaNumber0", "CaptchaNumber1", updatePasswordModel.CaptchaAnswer))
                {
                }
                else
                {
                    success = false;
                    modelStateDictionary.AddModelError("CaptchaAnswer", "Incorrect captcha answer");
                }
                List<string> passwordValidationMessages = archLibBL.CalculatePasswordStrength(updatePasswordModel.LoginPassword, out string passwordStrengthColor, out string passwordStrengthMessage);
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
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Before SVCCTempleDataContext.OpenSqlConnection");
                    SVCCTempleDataContext.OpenSqlConnection();
                    LoginUserModel loginUserModel = SVCCTempleDataContext.GettLoginUserFromResetPasswordQueryString(locationNameDesc, updatePasswordModel.ResetPasswordQueryString, SVCCTempleDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                    DateTime resetPasswordCompletedDateTime = DateTime.Now; //long.Parse(aspNetUser.ResetPasswordCompletedDateTime.Value.ToString("yyyyMMddHHmmss"));
                    if (!success && loginUserModel == null)
                    {
                        success = false;
                    }
                    if (success && loginUserModel.AspNetUserId == updatePasswordModel.ResetPasswordQueryString.Substring(0, loginUserModel.AspNetUserId.Length))
                    {
                    }
                    else
                    {
                        success = false;
                    }
                    if (success && updatePasswordModel.ResetPasswordKey == loginUserModel.ResetPasswordKey)
                    {
                    }
                    else
                    {
                        success = false;
                    }
                    if (success && long.Parse(DateTime.Parse(loginUserModel.ResetPasswordExpiryDateTime).ToString("yyyyMMddHHmmss")) >= long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")))
                    {
                    }
                    else
                    {
                        success = false;
                    }
                    if (success && updatePasswordModel.EmailAddress == loginUserModel.LoginNameId1 && string.IsNullOrWhiteSpace(loginUserModel.ResetPasswordCompletedDateTime))
                    {
                    }
                    else
                    {
                        success = false;
                    }
                    if (success)
                    {
                        string privateKey = ArchLibCache.GetPrivateKey(clientId);
                        loginUserModel.LoginPassword = EncryptDecrypt.EncryptDataMd5(updatePasswordModel.LoginPassword, privateKey); //Encrypt this
                        loginUserModel.PasswordExpiry = DateTime.Now.AddDays(180).ToString("yyyy-MM-dd HH:mm:ss");
                        loginUserModel.ResetPasswordCompletedDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        loginUserModel.UpdUserId = loginUserModel.LoginUserId.ToString();
                        SVCCTempleDataContext.UpdLoginUserForPassword(loginUserModel, SVCCTempleDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                        updatePasswordModel.FirstName = "Valued";
                        updatePasswordModel.LastName = "Customer";
                        string updatePasswordEmailBodyHtml = archLibBL.ViewToHtmlString(controller, "_UpdatePasswordEmailBody", updatePasswordModel);
                        string updatePasswordEmailSubjectHtml = archLibBL.ViewToHtmlString(controller, "_UpdatePasswordEmailSubject", updatePasswordModel);
                        string signatureHtml = archLibBL.ViewToHtmlString(controller, "_SignatureTemplate", updatePasswordModel);
                        updatePasswordEmailBodyHtml += signatureHtml;
                        archLibBL.SendEmail(updatePasswordModel.EmailAddress, updatePasswordEmailSubjectHtml, updatePasswordEmailBodyHtml, null, clientId, ipAddress, execUniqueId, loggedInUserId);
                        updatePasswordModel = new UpdatePasswordModel
                        {
                            ResponseObjectModel = new ArchitectureLibraryModels.ResponseObjectModel
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
                        SVCCTempleDataContext.CloseSqlConnection();
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
                    updatePasswordModel.ResponseObjectModel = new ArchitectureLibraryModels.ResponseObjectModel
                    {
                        ColumnCount = 3,
                        ListStyleType = "decimal",
                        ResponseTypeId = ResponseTypeEnum.Success,
                        TextAlign = "left",
                        ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                    };
                }
                archLibBL.GenerateCaptchaQuesion(httpSessionStateBase, "CaptchaNumber0", "CaptchaNumber1");
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
                    SVCCTempleDataContext.CloseSqlConnection();
                }
                catch
                {
                    ;
                }
            }
        }
    }
}
