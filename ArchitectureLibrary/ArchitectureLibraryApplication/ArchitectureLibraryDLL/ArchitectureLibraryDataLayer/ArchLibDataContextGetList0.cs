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
        public static List<ApplicationDefaultModel> GetApplicationDefaults(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<ApplicationDefaultModel> applicationDefaultModels = new List<ApplicationDefaultModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM ArchLib.ApplicationDefault ORDER BY ApplicationDefaultId", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    applicationDefaultModels.Add(AssignApplicationDefault(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId));
                }
                sqlDataReader.Close();
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            return applicationDefaultModels;
        }
        public static List<AspNetRoleModel> GetAspNetRoles(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<AspNetRoleModel> aspNetRoleModels = new List<AspNetRoleModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM ArchLib.AspNetRole ORDER BY UserTypeId", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    aspNetRoleModels.Add(AssignAspNetRole(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId));
                }
                sqlDataReader.Close();
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            return aspNetRoleModels;
        }
        public static List<AspNetRoleParentMenu> GetAspNetRoleParentMenus(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<AspNetRoleParentMenu> aspNetRoleParentMenuModels = new List<AspNetRoleParentMenu>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM ArchLib.AspNetRoleParentMenu ORDER BY AspNetRoleParentMenuId", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    aspNetRoleParentMenuModels.Add(AssignAspNetRoleParentMenu(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId));
                }
                sqlDataReader.Close();
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            return aspNetRoleParentMenuModels;
        }
        public static List<ClientModel> GetClients(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<ClientModel> clientModels = new List<ClientModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM ArchLib.Client ORDER BY ClientId", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    clientModels.Add(AssignClient(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId));
                }
                sqlDataReader.Close();
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            return clientModels;
        }
        public static List<PersonModel> GetPersons(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<PersonModel> personModels = new List<PersonModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM ArchLib.Person ORDER BY PersonId", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    personModels.Add(AssignPerson(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId));
                }
                sqlDataReader.Close();
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            return personModels;
        }
        public static List<UserProfileMetaDataModel> GetUserProfileMetaDatas(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<UserProfileMetaDataModel> userProfileMetaDataModels = new List<UserProfileMetaDataModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM ArchLib.UserProfileMetaData ORDER BY SeqNum", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    userProfileMetaDataModels.Add(AssignUserProfileMetaData(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId));
                }
                sqlDataReader.Close();
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            return userProfileMetaDataModels;
        }
        public static List<SalesTaxListModel> GetSalesTaxLists(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<SalesTaxListModel> salesTaxListModels = new List<SalesTaxListModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM ArchLib.SalesTaxList ORDER BY SalesTaxListId", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    salesTaxListModels.Add(AssignSalesTaxList(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId));
                }
                sqlDataReader.Close();
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            return salesTaxListModels;
        }
    }
}
