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
        public static void ModifyCategory(CategoryModel categoryModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                SqlCommand sqlCommand = BuildSqlCommandCategoryUpdate(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                AssignCategoryUpdate(categoryModel, sqlCommand, clientId, ipAddress, execUniqueId, loggedInUserId);
                sqlCommand.Parameters["@CategoryId"].Value = categoryModel.CategoryId;
                sqlCommand.ExecuteNonQuery();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }

        }
        public static void ModifyCategoryImageName(CategoryModel categoryModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                SqlCommand sqlCommand = new SqlCommand("UPDATE RetailSlnSch.Category SET ImageName = @ImageName, UpdUserId = @LoggedInUserId, UpdDateTime = GETDATE() WHERE CategoryId = @CategoryId", sqlConnection);
                sqlCommand.Parameters.Add("@ImageName", SqlDbType.NVarChar, 50);
                sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 512);
                sqlCommand.Parameters.Add("@CategoryId", SqlDbType.BigInt);
                sqlCommand.Parameters["@ImageName"].Value = categoryModel.ImageName;
                sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
                sqlCommand.Parameters["@CategoryId"].Value = categoryModel.CategoryId;
                sqlCommand.ExecuteNonQuery();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static void ModifyItem(ItemModel itemModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                SqlCommand sqlCommand = BuildSqlCommandItemUpdate(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                AssignItemUpdate(itemModel, sqlCommand, clientId, ipAddress, execUniqueId, loggedInUserId);
                sqlCommand.Parameters["@ItemId"].Value = itemModel.ItemId;
                sqlCommand.ExecuteNonQuery();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }

        }
        public static void ModifyItemImageName(ItemModel itemModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                SqlCommand sqlCommand = new SqlCommand("UPDATE RetailSlnSch.Item SET ImageName = @ImageName, UpdUserId = @LoggedInUserId, UpdDateTime = GETDATE() WHERE ItemId = @ItemId", sqlConnection);
                sqlCommand.Parameters.Add("@ImageName", SqlDbType.NVarChar, 50);
                sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 512);
                sqlCommand.Parameters.Add("@ItemId", SqlDbType.BigInt);
                sqlCommand.Parameters["@ImageName"].Value = itemModel.ImageName;
                sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
                sqlCommand.Parameters["@ItemId"].Value = itemModel.ItemId;
                sqlCommand.ExecuteNonQuery();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }

        }
        public static void ModifyGiftCertBalance(GiftCertModel giftCertModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                SqlCommand sqlCommand = new SqlCommand("UPDATE RetailSlnSch.GiftCert SET GiftCertBalanceAmount = @GiftCertBalanceAmount, GiftCertUsedAmount = @GiftCertUsedAmount, UpdUserId = @LoggedInUserId, UpdDateTime = GETDATE() WHERE GiftCertId = @GiftCertId", sqlConnection);
                sqlCommand.Parameters.Add("@GiftCertBalanceAmount", SqlDbType.Float);
                sqlCommand.Parameters.Add("@GiftCertUsedAmount", SqlDbType.Float);
                sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 512);
                sqlCommand.Parameters.Add("@GiftCertId", SqlDbType.Float);
                sqlCommand.Parameters["@GiftCertBalanceAmount"].Value = giftCertModel.GiftCertBalanceAmount;
                sqlCommand.Parameters["@GiftCertUsedAmount"].Value = giftCertModel.GiftCertUsedAmount;
                sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
                sqlCommand.Parameters["@GiftCertId"].Value = giftCertModel.GiftCertId;
                sqlCommand.ExecuteNonQuery();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
    }
}
