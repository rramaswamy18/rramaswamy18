using ArchitectureLibraryDataLayer;
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
        //public static void CreateOrder(OrderModel orderModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: Before CreateOrderHeader");//, "PersonId", orderHeaderModel.PersonId);
        //        AddOrderHeader(orderModel.OrderHeaderModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        AddOrderDetails(orderModel.OrderHeaderModel.OrderHeaderId, orderModel.OrderHeaderModel.OrderDetailModels, orderModel.OrderHeaderModel.OrderSummaryModels, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        if (orderModel.DeliveryInfoModel.DeliveryInfoDataModel.CreateDeliveryAddress)
        //        {
        //            ArchLibDataContext.CreateDemogInfoAddress(orderModel.DeliveryInfoModel.DeliveryInfoDataModel.DeliveryAddressModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        }
        //        else
        //        {
        //            orderModel.DeliveryInfoModel.DeliveryInfoDataModel.DeliveryAddressModel.DemogInfoAddressId = 0;
        //        }
        //        orderModel.DeliveryInfoModel.DeliveryInfoDataModel.OrderHeaderId = orderModel.OrderHeaderModel.OrderHeaderId;
        //        AddDeliveryInfo(orderModel.DeliveryInfoModel.DeliveryInfoDataModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00009000 :: Exit");
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        //public static void CreatePayment(PaymentInfoModel paymentInfoModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: Before CreateOrderHeader");//, "PersonId", orderHeaderModel.PersonId);
        //        AddOrderPayment(paymentInfoModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00009000 :: Exit");
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        //public static void CreateGiftCert(GiftCertModel giftCertModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: Before CreateOrderHeader");//, "PersonId", orderHeaderModel.PersonId);
        //        AddGiftCert(giftCertModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00009000 :: Exit");
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        //public static void CreateItem(ItemModel itemModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: Before CreateOrderHeader");
        //        AddItem(itemModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        float seqNum = 0;
        //        foreach (var itemAttribModel in itemModel.ItemAttribModels)
        //        {
        //            itemAttribModel.ItemId = itemModel.ItemId.Value;
        //            itemAttribModel.SeqNum = ++seqNum;
        //            AddItemAttrib(itemAttribModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        }
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00009000 :: Exit");
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        public static void CreateCategory(CategoryModel categoryModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: Before CreateOrderHeader");
                AddCategory(categoryModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00009000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static void CreateCategoryItem(CategoryItemListModel categoryItemListModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: Before CreateOrderHeader");
                //1. Delete items assigned to unassigned
                //2. Insert items unassigned to assigned
                int recCount;
                string prefixString = "", itemIds = "";
                string sqlStmt;
                SqlCommand sqlCommand;
                if (categoryItemListModel.Assigned != null && categoryItemListModel.Assigned.Count > 0)
                {
                    foreach (var assigned in categoryItemListModel.Assigned)
                    {
                        itemIds += prefixString + assigned;
                        prefixString = ", ";
                    }
                    sqlStmt = "DELETE RetailSlnSch.CategoryItem WHERE CategoryId = " + categoryItemListModel.ParentCategoryModel.CategoryId + " AND ItemId NOT IN(" + itemIds + ")";
                }
                else
                {
                    sqlStmt = "DELETE RetailSlnSch.CategoryItem WHERE CategoryId = " + categoryItemListModel.ParentCategoryModel.CategoryId;
                }
                sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                recCount = sqlCommand.ExecuteNonQuery();
                //Write the above to log
                recCount = 0;
                if (categoryItemListModel.Unassigned != null && categoryItemListModel.Unassigned.Count > 0)
                {
                    sqlStmt = "SELECT ISNULL(MAX(SeqNum), 0) AS MaxSeqNum FROM RetailSlnSch.CategoryItem WHERE CategoryId = " + categoryItemListModel.ParentCategoryModel.CategoryId;
                    sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    float seqNum = 0;
                    if (sqlDataReader.Read())
                    {
                        seqNum = float.Parse(sqlDataReader["MaxSeqNum"].ToString());
                    }
                    sqlDataReader.Close();
                    sqlStmt = "INSERT RetailSlnSch.CategoryItem(ClientId, CategoryId, SeqNum, ItemId, AddUserId, UpdUserId) SELECT @ClientId, @CategoryId, @SeqNum, @ItemId, @LoggedInUserId, @LoggedInUserId";
                    sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                    sqlCommand.Parameters.Add("@ClientId", SqlDbType.BigInt);
                    sqlCommand.Parameters.Add("@CategoryId", SqlDbType.BigInt);
                    sqlCommand.Parameters.Add("@SeqNum", SqlDbType.Float);
                    sqlCommand.Parameters.Add("@ItemId", SqlDbType.BigInt);
                    sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 256);
                    sqlCommand.Parameters["@ClientId"].Value = clientId;
                    sqlCommand.Parameters["@CategoryId"].Value = categoryItemListModel.ParentCategoryModel.CategoryId;
                    sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
                    foreach (var unassigned in categoryItemListModel.Unassigned)
                    {
                        sqlCommand.Parameters["@SeqNum"].Value = ++seqNum;
                        sqlCommand.Parameters["@ItemId"].Value = unassigned;
                        recCount += sqlCommand.ExecuteNonQuery();
                    }
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00009000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
    }
}
