using ArchitectureLibraryException;
using ArchitectureLibraryUtility;
using RetailSlnModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace RetailSlnDataLayer
{
    public static partial class ApplicationDataContext
    {
        //public static void OrderDetailWIPDel(long orderDetailWIPId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        #region
        //        string sqlStmt = "";
        //        sqlStmt += "        DELETE RetailSlnSch.OrderDetailWIP" + Environment.NewLine;
        //        sqlStmt += "         WHERE OrderDetailWIPId = @OrderDetailWIPId" + Environment.NewLine;
        //        #endregion
        //        #region
        //        SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
        //        sqlCommand.Parameters.Add("@OrderDetailWIPId", SqlDbType.BigInt);
        //        #endregion
        //        #region
        //        sqlCommand.Parameters["@OrderDetailWIPId"].Value = orderDetailWIPId;
        //        #endregion
        //        sqlCommand.ExecuteNonQuery();
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        public static void ShoppingCartWIPUpd(string sqlStmtUpd, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                #region
                string sqlStmt = "";
                sqlStmt += "    CREATE TABLE #TEMP1(ShoppingCartWIPId BIGINT, OrderQty FLOAT)" + Environment.NewLine;
                sqlStmt += "    INSERT #TEMP1(ShoppingCartWIPId, OrderQty)" + Environment.NewLine;
                sqlStmt += sqlStmtUpd;
                sqlStmt += "    UPDATE RetailSlnSch.ShoppingCartWIP" + Environment.NewLine;
                sqlStmt += "       SET OrderQty = #TEMP1.OrderQty" + Environment.NewLine;
                sqlStmt += "      FROM #TEMP1" + Environment.NewLine;
                sqlStmt += "     WHERE ShoppingCartWIP.ShoppingCartWIPId = #TEMP1.ShoppingCartWIPId" + Environment.NewLine;
                #endregion
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
    }
}
