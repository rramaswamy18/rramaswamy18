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
        public static AspNetUserModel SelectAspNetUserFromUserName(string userName, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            //AspNetUserModel aspNetUserModel;
            try
            {
                AspNetUserModel aspNetUserModel = new AspNetUserModel
                {
                    UserName = userName,
                };
                aspNetUserModel = GetAspNetUserFromUserName(userName, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (aspNetUserModel == null)
                {
                    aspNetUserModel = new AspNetUserModel();
                }
                return aspNetUserModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static PersonModel SelectLoginUserProf(string loginEmailAddress, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Before Calling GetPersonFromEmailAddress in DL", "Username", loginEmailAddress);
                PersonModel personModel = GetPersonFromEmailAddress(loginEmailAddress, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001100 :: After calling GetPersonFromEmailAddress", "Username", loginEmailAddress);
                return personModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static AspNetUserModel SelectAspNetUserFromResetPasswordQueryString(string resetPasswordQueryString, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                AspNetUserModel aspNetUserModel = GetAspNetUserFromResetPasswordQueryString(resetPasswordQueryString, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                return aspNetUserModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static PersonModel SelectPersonFromResetPasswordQueryString(string resetPasswordQueryString, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                AspNetUserModel aspNetUserModel = GetAspNetUserFromResetPasswordQueryString(resetPasswordQueryString, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                PersonModel personModel = GetPersonFromAspNetUserId(aspNetUserModel.AspNetUserId, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                personModel.AspNetUserModel = aspNetUserModel;
                return personModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static PersonModel SelectPersonAspNetUserFromAspNetUserId(string aspNetUserId, string documentColumnPrefix, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                PersonModel personModel = GetPersonAspNetUserFromAspNetUserId(aspNetUserId, documentColumnPrefix, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                return personModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
    }
}
