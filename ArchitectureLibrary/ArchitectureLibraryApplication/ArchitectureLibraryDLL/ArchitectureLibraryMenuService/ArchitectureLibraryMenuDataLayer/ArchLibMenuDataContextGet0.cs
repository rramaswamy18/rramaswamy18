using ArchitectureLibraryException;
using ArchitectureLibraryMenuModels;
using ArchitectureLibraryUtility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryMenuDataLayer
{
    public static partial class ArchLibMenuDataContext
    {
        public static List<MenuKVPModel> GetMenuKVPs(SqlConnection sqlConnection, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<MenuKVPModel> menuKVPModels = new List<MenuKVPModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM ArchLib.MenuKVP ORDER BY MenuKVPId", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    menuKVPModels.Add(AssignMenuKVP(sqlDataReader, ipAddress, execUniqueId, loggedInUserId));
                }
                sqlDataReader.Close();
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            return menuKVPModels;
        }
        public static List<MenuLayoutModel> GetMenuLayouts(SqlConnection sqlConnection, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<MenuLayoutModel> menuLayoutModels = new List<MenuLayoutModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM ArchLib.MenuLayout ORDER BY MenuLayoutId", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    menuLayoutModels.Add(AssignMenuLayout(sqlDataReader, ipAddress, execUniqueId, loggedInUserId));
                }
                sqlDataReader.Close();
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            return menuLayoutModels;
        }
        public static List<MenuListModel> GetMenuLists(SqlConnection sqlConnection, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<MenuListModel> menuListModels = new List<MenuListModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM ArchLib.MenuList ORDER BY MenuListId", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    menuListModels.Add(AssignMenuList(sqlDataReader, ipAddress, execUniqueId, loggedInUserId));
                }
                sqlDataReader.Close();
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            return menuListModels;
        }
    }
}
