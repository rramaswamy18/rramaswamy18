using ArchitectureLibraryBusinessLayer;
using ArchitectureLibraryCacheData;
using ArchitectureLibraryDataLayer;
using ArchitectureLibraryEmailService;
using ArchitectureLibraryEnumerations;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryTemplate;
using ArchitectureLibraryUtility;
using RetailSlnModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace RetailSlnBusinessLayer
{
    public partial class RetailSlnBL
    {
        private bool RegisterUserProf(RegisterUserProfModel registerUserProfModel, string loginPassword, string phoneNumber, string firstName, string lastName, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId, out AspNetUserModel aspNetUserModel)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
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
                    long certificateDocumentId = 0;
                    string aspNetRoleId = ArchLibCache.AspNetRoleModels.First(x => x.UserTypeId == (int)UserTypeEnum.DefaultRole).AspNetRoleId;
                    ArchLibDataContext.CreateRegisterUser(aspNetUserId, registerUserProfModel.RegisterEmailAddress, loginPasswordEncrypted, loginPasswordExpiryDateTime, registerUserProfModel.RegisterTelephoneCountryId, phoneNumber, null, null, null, firstName, lastName, certificateDocumentId, 0, "", 0, "", UserTypeEnum.DefaultRole, aspNetRoleId, UserStatusEnum.Active, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
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
    }
}
//System.Web.Mvc.Html.PartialExtensions.Partial(html, "~/Views/Orders/OrdersPartialView.cshtml", orderModel).ToString();
