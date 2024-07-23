using ArchitectureLibraryException;
using ArchitectureLibraryUtility;
using RetailSlnModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace RetailSlnDataLayer
{
    public static partial class ApplicationDataContext
    {
        public static void UpdItemSpecs(List<ItemSpecModel> itemAttribModels, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            SqlCommand sqlCommand = new SqlCommand("UPDATE RetailSlnSch.ItemSpec SET ItemSpecUnitValue = @ItemSpecUnitValue, ItemSpecValue = @ItemSpecValue, UpdUserId = @LoggedInUserId, UpdDateTime = GETDATE() WHERE ItemSpecId = @ItemSpecId", sqlConnection);
            sqlCommand.Parameters.Add("@ItemSpecUnitValue", System.Data.SqlDbType.NVarChar, 50);
            sqlCommand.Parameters.Add("@ItemSpecValue", System.Data.SqlDbType.NVarChar, 50);
            sqlCommand.Parameters.Add("@LoggedInUserId", System.Data.SqlDbType.NVarChar, 512);
            sqlCommand.Parameters.Add("@ItemSpecId", System.Data.SqlDbType.BigInt);
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
            foreach (var itemAttribModel in itemAttribModels)
            {
                sqlCommand.Parameters["@ItemSpecUnitValue"].Value = string.IsNullOrWhiteSpace(itemAttribModel.ItemSpecUnitValue) ? "" : itemAttribModel.ItemSpecUnitValue;
                sqlCommand.Parameters["@ItemSpecValue"].Value = string.IsNullOrWhiteSpace(itemAttribModel.ItemSpecValue) ? "" : itemAttribModel.ItemSpecValue;
                sqlCommand.Parameters["@ItemSpecId"].Value = itemAttribModel.ItemSpecId;
                sqlCommand.ExecuteNonQuery();
            }
        }
        public static void UpdItemImageName(ItemModel itemModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                SqlCommand sqlCommand = BuildSqlCommandItemImageNameUpdate(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                AssignItemImageNameUpdate(itemModel, sqlCommand, clientId, ipAddress, execUniqueId, loggedInUserId);
                sqlCommand.ExecuteNonQuery();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }

        }
        public static void UpdItemInfo(ItemInfoModel itemInfoModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Before calling the BuildSqlCommandAspNetUserRoles()", "AspNetUserId", "");
                SqlCommand sqlCommand = BuildSqlCommandItemInfoUpdate(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                AssignItemInfoUpdate(itemInfoModel, sqlCommand, clientId, ipAddress, execUniqueId, loggedInUserId);
                sqlCommand.ExecuteNonQuery();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00009000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
    }
}
