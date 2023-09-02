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
        public static void UpdItemAttribs(List<ItemAttribModel> itemAttribModels, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            SqlCommand sqlCommand = new SqlCommand("UPDATE RetailSlnSch.ItemAttrib SET ItemAttribUnitValue = @ItemAttribUnitValue, ItemAttribValue = @ItemAttribValue, UpdUserId = @LoggedInUserId, UpdDateTime = GETDATE() WHERE ItemAttribId = @ItemAttribId", sqlConnection);
            sqlCommand.Parameters.Add("@ItemAttribUnitValue", System.Data.SqlDbType.NVarChar, 50);
            sqlCommand.Parameters.Add("@ItemAttribValue", System.Data.SqlDbType.NVarChar, 50);
            sqlCommand.Parameters.Add("@LoggedInUserId", System.Data.SqlDbType.NVarChar, 512);
            sqlCommand.Parameters.Add("@ItemAttribId", System.Data.SqlDbType.BigInt);
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
            foreach (var itemAttribModel in itemAttribModels)
            {
                sqlCommand.Parameters["@ItemAttribUnitValue"].Value = string.IsNullOrWhiteSpace(itemAttribModel.ItemAttribUnitValue) ? "" : itemAttribModel.ItemAttribUnitValue;
                sqlCommand.Parameters["@ItemAttribValue"].Value = string.IsNullOrWhiteSpace(itemAttribModel.ItemAttribValue) ? "" : itemAttribModel.ItemAttribValue;
                sqlCommand.Parameters["@ItemAttribId"].Value = itemAttribModel.ItemAttribId;
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
        public static void UpdItemSpec(ItemSpecModel itemSpecModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Before calling the BuildSqlCommandAspNetUserRoles()", "AspNetUserId", "");
                SqlCommand sqlCommand = BuildSqlCommandItemSpecUpdate(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                AssignItemSpecUpdate(itemSpecModel, sqlCommand, clientId, ipAddress, execUniqueId, loggedInUserId);
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
