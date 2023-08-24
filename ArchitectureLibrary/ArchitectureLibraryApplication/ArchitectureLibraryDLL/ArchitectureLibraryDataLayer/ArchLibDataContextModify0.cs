using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryDataLayer
{
    public static partial class ArchLibDataContext
    {
        public static void ModifyUpdatePassword(AspNetUserModel aspNetUserModel, string aspNetUserId, int userStatusId, int statusId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            string databaseConnectionString = Utilities.GetDatabaseConnectionString("DatabaseConnectionString");
            try
            {
                // sqlTransaction = sqlConnection.BeginTransaction();                                       
                UpdAspNetUser(aspNetUserModel, aspNetUserId, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                PersonModel personModel = GetPersonFromAspNetUserId(aspNetUserId, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                UpdPerson(personModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                //sqlTransaction.Commit();
                return;
            }
            catch (Exception exception)
            {
                //sqlTransaction.Rollback();
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static void ModifyUserProfile(PersonModel personModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                UpdPerson1(personModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                UpdAspNetUser1(personModel.AspNetUserModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                UpdDemogInfoAddress(personModel.HomeDemogInfoAddressModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                return;
            }
            catch (Exception exception)
            {
                //sqlTransaction.Rollback();
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static void ModifyResetPassword(string aspNetUserId, string resetPasswordQueryString, string resetPasswordKey, string resetPasswordExpiryDateTime, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string databaseConnectionString = Utilities.GetDatabaseConnectionString("DatabaseConnectionString");
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            //SqlTransaction sqlTransaction = null;
            AspNetUserModel aspNetUserModel;
            try
            {
                aspNetUserModel = new AspNetUserModel
                {
                    AspNetUserId = aspNetUserId,
                    ClientId = clientId,
                    ResetPasswordQueryString = resetPasswordQueryString,
                    ResetPasswordExpiryDateTime = resetPasswordExpiryDateTime,
                    ResetPasswordKey = resetPasswordKey,
                    UpdUserId = "",
                    UpdUserName = "",
                };
                UpdAspNetUserResetPassword(aspNetUserModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                return;
            }
            catch (Exception exception)
            {
                //sqlTransaction.Rollback();
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
    }
}
