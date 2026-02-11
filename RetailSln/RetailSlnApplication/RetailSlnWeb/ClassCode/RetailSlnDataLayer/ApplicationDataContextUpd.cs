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
        public static void ShoppingCartWIPUpd(ShoppingCartItemModel shoppingCartItemModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                #region
                string sqlStmt = "";
                sqlStmt += "    UPDATE RetailSlnSch.ShoppingCartWIP" + Environment.NewLine;
                sqlStmt += "       SET OrderComments = @OrderComments" + Environment.NewLine;
                sqlStmt += "          ,OrderQty = @OrderQty" + Environment.NewLine;
                sqlStmt += "          ,UpdUserName = @LoggedInUserId" + Environment.NewLine;
                sqlStmt += "          ,UpdDateTime = GETDATE()" + Environment.NewLine;
                sqlStmt += "     WHERE ShoppingCartWIPId = @ShoppingCartWIPId" + Environment.NewLine;
                #endregion
                #region
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                sqlCommand.Parameters.Add("@OrderComments", SqlDbType.NVarChar, 256);
                sqlCommand.Parameters.Add("@OrderQty", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 512);
                sqlCommand.Parameters.Add("@ShoppingCartWIPId", SqlDbType.BigInt);
                sqlCommand.Parameters["@OrderComments"].Value = shoppingCartItemModel.OrderComments;
                sqlCommand.Parameters["@OrderQty"].Value = shoppingCartItemModel.OrderQty;
                sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
                sqlCommand.Parameters["@ShoppingCartWIPId"].Value = shoppingCartItemModel.ShoppingCartWIPId;
                #endregion
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        #region
        //public static void ShoppingCartWIPUpdByItemId(ShoppingCartItemModel shoppingCartItemModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        #region
        //        string sqlStmt = "";
        //        sqlStmt += "    CREATE TABLE #TEMP1(ShoppingCartWIPId BIGINT, OrderComments NVARCHAR(256), OrderQty FLOAT)" + Environment.NewLine;
        //        sqlStmt += "    INSERT #TEMP1(ShoppingCartWIPId, OrderComments, OrderQty)" + Environment.NewLine;
        //        sqlStmt += sqlStmtUpd;
        //        sqlStmt += "    UPDATE RetailSlnSch.ShoppingCartWIP" + Environment.NewLine;
        //        sqlStmt += "       SET OrderComments = #TEMP1.OrderComments" + Environment.NewLine;
        //        sqlStmt += "          ,OrderQty = #TEMP1.OrderQty" + Environment.NewLine;
        //        sqlStmt += "      FROM #TEMP1" + Environment.NewLine;
        //        sqlStmt += "     WHERE ShoppingCartWIP.ShoppingCartWIPId = #TEMP1.ShoppingCartWIPId" + Environment.NewLine;
        //        #endregion
        //        SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
        //        sqlCommand.ExecuteNonQuery();
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        //public static void ShoppingCartWIPUpd(string sqlStmtUpd, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        #region
        //        string sqlStmt = "";
        //        sqlStmt += "    CREATE TABLE #TEMP1(ShoppingCartWIPId BIGINT, OrderComments NVARCHAR(256), OrderQty FLOAT)" + Environment.NewLine;
        //        sqlStmt += "    INSERT #TEMP1(ShoppingCartWIPId, OrderComments, OrderQty)" + Environment.NewLine;
        //        sqlStmt += sqlStmtUpd;
        //        sqlStmt += "    UPDATE RetailSlnSch.ShoppingCartWIP" + Environment.NewLine;
        //        sqlStmt += "       SET OrderComments = #TEMP1.OrderComments" + Environment.NewLine;
        //        sqlStmt += "          ,OrderQty = #TEMP1.OrderQty" + Environment.NewLine;
        //        sqlStmt += "      FROM #TEMP1" + Environment.NewLine;
        //        sqlStmt += "     WHERE ShoppingCartWIP.ShoppingCartWIPId = #TEMP1.ShoppingCartWIPId" + Environment.NewLine;
        //        #endregion
        //        SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
        //        sqlCommand.ExecuteNonQuery();
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        #endregion
    }
}
