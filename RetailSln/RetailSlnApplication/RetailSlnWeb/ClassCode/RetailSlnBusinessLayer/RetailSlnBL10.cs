using ArchitectureLibraryBusinessLayer;
using ArchitectureLibraryCacheData;
using ArchitectureLibraryDataLayer;
using ArchitectureLibraryEmailService;
using ArchitectureLibraryEnumerations;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryTemplate;
using ArchitectureLibraryUtility;
using RetailSlnDataLayer;
using RetailSlnEnumerations;
using RetailSlnModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http.ExceptionHandling;
using System.Web.Mvc;

namespace RetailSlnBusinessLayer
{
    public partial class RetailSlnBL
    {
        public ApplSessionObjectModel LoginUserProf(long personId, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ArchitectureLibraryException.ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                PersonExtn1Model personExtn1Model = ApplicationDataContext.GetPersonExtn1FromPersonId(personId, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                ApplSessionObjectModel applSessionObjectModel = new ApplSessionObjectModel
                {
                    CorpAcctModel = personExtn1Model.CorpAcctModel,
                    TotalBalanceDue = 0,
                };
                return applSessionObjectModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            finally
            {
                ApplicationDataContext.CloseSqlConnection();
            }
        }
        private bool RegisterUserProf(RegisterUserProfModel registerUserProfModel, string loginPassword, string phoneNumber, string firstName, string lastName, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId, out AspNetUserModel aspNetUserModel)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ArchitectureLibraryException.ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                aspNetUserModel = ArchLibDataContext.SelectAspNetUserFromUserName(registerUserProfModel.RegisterEmailAddress, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (string.IsNullOrWhiteSpace(aspNetUserModel.AspNetUserId))
                {//User is not registered - create user with password - Do not need reset password - Set the expiry to 30 days
                    string aspNetUserId = Guid.NewGuid().ToString();
                    string privateKey = ArchLibCache.GetPrivateKey(clientId);
                    string loginPasswordEncrypted = EncryptDecrypt.EncryptDataMd5(loginPassword, privateKey);
                    DateTime loginPasswordExpiryDateTime = DateTime.Now.AddDays(30);
                    string aspNetRoleId = ArchLibCache.AspNetRoleModels.First(x => x.UserTypeId == (int)UserTypeEnum.DefaultRole).AspNetRoleId;
                    ArchLibDataContext.CreateRegisterUser(aspNetUserId, registerUserProfModel.RegisterEmailAddress, loginPasswordEncrypted, loginPasswordExpiryDateTime, registerUserProfModel.RegisterTelephoneCountryId, phoneNumber, null, null, null, firstName, lastName, 0, "", 0, "", UserTypeEnum.DefaultRole, aspNetRoleId, UserStatusEnum.Active, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                    aspNetUserModel = ArchLibDataContext.SelectAspNetUserFromUserName(registerUserProfModel.RegisterEmailAddress, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                    return true;
                }
                else
                {//User is already registered
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
        public void RegisterUserProfPersonExtn1(long personId, Controller controller, HttpSessionStateBase httpSessionStateBase, ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ArchitectureLibraryException.ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                ApplicationDataContext.OpenSqlConnection();
                ApplicationDataContext.AddPersonExtn1(personId, 0, ApplicationDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            finally
            {
                ApplicationDataContext.CloseSqlConnection();
            }
        }
    }
}
//System.Web.Mvc.Html.PartialExtensions.Partial(html, "~/Views/Orders/OrdersPartialView.cshtml", orderModel).ToString();
