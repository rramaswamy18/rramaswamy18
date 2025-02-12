using ArchitectureLibraryException;
using ArchitectureLibraryUtility;
using Microsoft.Ajax.Utilities;
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
        public static void AddCategory(CategoryModel categoryModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Before calling the BuildSqlCommandAspNetUserRoles()", "AspNetUserId", "");
                SqlCommand sqlCommand = BuildSqlCommandCategoryInsert(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                AssignCategoryInsert(categoryModel, sqlCommand, clientId, ipAddress, execUniqueId, loggedInUserId);
                categoryModel.CategoryId = (long)sqlCommand.ExecuteScalar();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00009000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static void AddCorpAcct(CorpAcctModel corpAcctModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Before calling the BuildSqlCommandAspNetUserRoles()", "AspNetUserId", "");
                SqlCommand sqlCommand = BuildSqlCommandCorpAcctInsert(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                AssignCorpAcctInsert(corpAcctModel, sqlCommand, clientId, ipAddress, execUniqueId, loggedInUserId);
                corpAcctModel.CorpAcctId = (long)sqlCommand.ExecuteScalar();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00009000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static void AddCorpAcctLocation(CorpAcctLocationModel corpAcctLocationModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Before calling the BuildSqlCommandAspNetUserRoles()", "AspNetUserId", "");
                SqlCommand sqlCommand = BuildSqlCommandCorpAcctLocationInsert(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                AssignCorpAcctLocationInsert(corpAcctLocationModel, sqlCommand, clientId, ipAddress, execUniqueId, loggedInUserId);
                corpAcctLocationModel.CorpAcctLocationId = (long)sqlCommand.ExecuteScalar();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00009000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static void AddOrderHeaderWIP(OrderHeaderWIPModel orderHeaderWIPModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                #region
                string sqlStmt = "";
                sqlStmt += "        INSERT RetailSlnSch.OrderHeaderWIP" + Environment.NewLine;
                sqlStmt += "              (" + Environment.NewLine;
                sqlStmt += "               ClientId" + Environment.NewLine;
                sqlStmt += "              ,CorpAcctLocationId" + Environment.NewLine;
                sqlStmt += "              ,CreatedForPersonId" + Environment.NewLine;
                sqlStmt += "              ,OrderDateTime" + Environment.NewLine;
                sqlStmt += "              ,OrderStatusId" + Environment.NewLine;
                sqlStmt += "              ,PersonId" + Environment.NewLine;
                sqlStmt += "              ,AddUserId" + Environment.NewLine;
                sqlStmt += "              ,UpdUserId" + Environment.NewLine;
                sqlStmt += "              )" + Environment.NewLine;
                sqlStmt += "        OUTPUT INSERTED.OrderHeaderWIPId" + Environment.NewLine;
                sqlStmt += "        SELECT " + Environment.NewLine;
                sqlStmt += "               @ClientId" + Environment.NewLine;
                sqlStmt += "              ,@CorpAcctLocationId" + Environment.NewLine;
                sqlStmt += "              ,@CreatedForPersonId" + Environment.NewLine;
                sqlStmt += "              ,@OrderDateTime" + Environment.NewLine;
                sqlStmt += "              ,@OrderStatusId" + Environment.NewLine;
                sqlStmt += "              ,@PersonId" + Environment.NewLine;
                sqlStmt += "              ,@LoggedInUserId" + Environment.NewLine;
                sqlStmt += "              ,@LoggedInUserId" + Environment.NewLine;
                #endregion
                #region
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                sqlCommand.Parameters.Add("@ClientId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@CorpAcctLocationId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@CreatedForPersonId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@OrderDateTime", SqlDbType.NVarChar, 21);
                sqlCommand.Parameters.Add("@OrderStatusId", SqlDbType.NVarChar, 21);
                sqlCommand.Parameters.Add("@PersonId", SqlDbType.NVarChar, 21);
                sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 256);
                #endregion
                #region
                sqlCommand.Parameters["@ClientId"].Value = orderHeaderWIPModel.ClientId;
                sqlCommand.Parameters["@CorpAcctLocationId"].Value = orderHeaderWIPModel.CorpAcctLocationId;
                sqlCommand.Parameters["@CreatedForPersonId"].Value = orderHeaderWIPModel.CreatedForPersonId;
                sqlCommand.Parameters["@OrderDateTime"].Value = string.IsNullOrWhiteSpace(orderHeaderWIPModel.OrderDateTime) ? (object)DBNull.Value : orderHeaderWIPModel.OrderDateTime;
                sqlCommand.Parameters["@OrderStatusId"].Value = orderHeaderWIPModel.OrderStatusId == null ? (object)DBNull.Value : orderHeaderWIPModel.OrderStatusId;
                sqlCommand.Parameters["@PersonId"].Value = orderHeaderWIPModel.PersonId;
                sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
                #endregion
                orderHeaderWIPModel.OrderHeaderWIPId = (long)sqlCommand.ExecuteScalar();
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static void AddOrderDetailWIP(OrderDetailWIPModel orderDetailWIPModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                #region
                string sqlStmt = "";
                sqlStmt += "        INSERT RetailSlnSch.OrderDetailWIP" + Environment.NewLine;
                sqlStmt += "              (" + Environment.NewLine;
                sqlStmt += "               ClientId" + Environment.NewLine;
                sqlStmt += "              ,ItemId" + Environment.NewLine;
                sqlStmt += "              ,OrderHeaderWIPId" + Environment.NewLine;
                sqlStmt += "              ,OrderQty" + Environment.NewLine;
                sqlStmt += "              ,SeqNum" + Environment.NewLine;
                sqlStmt += "              ,AddUserId" + Environment.NewLine;
                sqlStmt += "              ,UpdUserId" + Environment.NewLine;
                sqlStmt += "              )" + Environment.NewLine;
                sqlStmt += "        OUTPUT INSERTED.OrderDetailWIPId" + Environment.NewLine;
                sqlStmt += "        SELECT " + Environment.NewLine;
                sqlStmt += "               @ClientId" + Environment.NewLine;
                sqlStmt += "              ,@ItemId" + Environment.NewLine;
                sqlStmt += "              ,@OrderHeaderWIPId" + Environment.NewLine;
                sqlStmt += "              ,@OrderQty" + Environment.NewLine;
                sqlStmt += "              ,@SeqNum" + Environment.NewLine;
                sqlStmt += "              ,@LoggedInUserId" + Environment.NewLine;
                sqlStmt += "              ,@LoggedInUserId" + Environment.NewLine;
                #endregion
                #region
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                sqlCommand.Parameters.Add("@ClientId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@ItemId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@OrderHeaderWIPId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@OrderQty", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@SeqNum", SqlDbType.Float);
                sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 256);
                #endregion
                #region
                sqlCommand.Parameters["@ClientId"].Value = orderDetailWIPModel.ClientId;
                sqlCommand.Parameters["@ItemId"].Value = orderDetailWIPModel.ItemId;
                sqlCommand.Parameters["@OrderHeaderWIPId"].Value = orderDetailWIPModel.OrderHeaderWIPId;
                sqlCommand.Parameters["@OrderQty"].Value = orderDetailWIPModel.OrderQty;
                sqlCommand.Parameters["@SeqNum"].Value = orderDetailWIPModel.SeqNum;
                sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
                #endregion
                orderDetailWIPModel.OrderDetailWIPId = (long)sqlCommand.ExecuteScalar();
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static void AddOrderApproval(OrderApprovalModel orderApprovalModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Before calling the BuildSqlCommandAspNetUserRoles()", "AspNetUserId", "");
                SqlCommand sqlCommand = BuildSqlCommandOrderApprovalAdd(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                AssignOrderApproval(orderApprovalModel, sqlCommand, clientId, ipAddress, execUniqueId, loggedInUserId);
                orderApprovalModel.OrderApprovalId = (long)sqlCommand.ExecuteScalar();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00009000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static void AddOrderDetail(OrderDetail orderDetail, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Before calling BuildSqlCommandOrderDetailAdd()", "orderDetail.OrderHeaderId", orderDetail.OrderHeaderId.ToString(), "orderDetail.ItemId", orderDetail.ItemId.ToString(), "orderDetail.SeqNum", orderDetail.SeqNum.ToString());
                SqlCommand sqlCommand = BuildSqlCommandOrderDetailAdd(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                AssignOrderDetail(orderDetail, sqlCommand, clientId, ipAddress, execUniqueId, loggedInUserId);
                orderDetail.OrderDetailId = (long)sqlCommand.ExecuteScalar();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00003000 After ExecuteScalar()", "orderDetail.OrderDetailId", orderDetail.OrderDetailId.ToString());
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00009000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static void AddOrderDetailItemBundle(OrderDetailItemBundle orderDetailItemBundle, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Before calling the BuildSqlCommandAspNetUserRoles()", "AspNetUserId", "");
                SqlCommand sqlCommand = BuildSqlCommandOrderDetailItemBundleAdd(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                AssignOrderDetailItemBundle(orderDetailItemBundle, sqlCommand, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        public static void AddOrderDelivery(DeliveryDataModel deliveryInfoDataModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Before calling the BuildSqlCommandAspNetUserRoles()", "AspNetUserId", "");
                SqlCommand sqlCommand = BuildSqlCommandOrderDeliveryAdd(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                AssignOrderDelivery(deliveryInfoDataModel, sqlCommand, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        public static void AddOrderHeader(OrderHeader orderHeader, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Before calling the BuildSqlCommandAspNetUserRoles()", "AspNetUserId", "");
                SqlCommand sqlCommand = BuildSqlCommandOrderHeaderAdd(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                AssignOrderHeader(orderHeader, sqlCommand, clientId, ipAddress, execUniqueId, loggedInUserId);
                orderHeader.OrderHeaderId = (long)sqlCommand.ExecuteScalar();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00009000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static void AddOrderPayment(PaymentData1Model paymentData1Model, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Before calling the BuildSqlCommandOrderPaymentAdd()", "orderDetail.OrderHeaderId", paymentData1Model.OrderHeaderId.ToString(), "paymentData1Model.CreditCardDataId", paymentData1Model.CreditCardDataId.ToString());
                SqlCommand sqlCommand = BuildSqlCommandOrderPaymentAdd(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                AssignOrderPayment(paymentData1Model, sqlCommand, clientId, ipAddress, execUniqueId, loggedInUserId);
                paymentData1Model.OrderPaymentId = (long)sqlCommand.ExecuteScalar();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00003000 After ExecuteScalar()", "paymentData1Model.OrderPaymentId", paymentData1Model.OrderPaymentId.ToString());
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00009000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static void AddGiftCert(GiftCertModel giftCertModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Before calling the BuildSqlCommandAspNetUserRoles()", "AspNetUserId", "");
                SqlCommand sqlCommand = BuildSqlCommandGiftCertAdd(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                AssignGiftCert(giftCertModel, sqlCommand, clientId, ipAddress, execUniqueId, loggedInUserId);
                giftCertModel.GiftCertId = (long)sqlCommand.ExecuteScalar();
                giftCertModel.GiftCertNumber = 3500000000000000 + (long)giftCertModel.GiftCertId;
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00009000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static void AddItem(ItemModel itemModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                SqlCommand sqlCommand = BuildSqlCommandItemInsert(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                AssignItemInsert(itemModel, sqlCommand, clientId, ipAddress, execUniqueId, loggedInUserId);
                itemModel.ItemId = (long)sqlCommand.ExecuteScalar();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00009000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static void AddItemSpec(ItemSpecModel itemAttribModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                SqlCommand sqlCommand = BuildSqlCommandItemSpecInsert(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                AssignItemSpecInsert(itemAttribModel, sqlCommand, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        public static void AddItemInfo(ItemInfoModel itemInfoModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Before calling the BuildSqlCommandAspNetUserRoles()", "AspNetUserId", "");
                SqlCommand sqlCommand = BuildSqlCommandItemInfoInsert(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                AssignItemInfoInsert(itemInfoModel, sqlCommand, clientId, ipAddress, execUniqueId, loggedInUserId);
                itemInfoModel.ItemInfoId = (long)sqlCommand.ExecuteScalar();
                //sqlCommand.ExecuteNonQuery();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00009000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static void AddItemSpecs(long itemId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            SqlCommand sqlCommand = new SqlCommand("INSERT RetailSlnSch.ItemSpec(ItemSpecMasterId, ItemSpecUnitValue, ItemSpecValue, ItemId, SeqNum, AddUserId, UpdUserId) SELECT ItemSpecMasterId, '' AS AttribUnitValue, '' AS AttribValue, @ItemId, SeqNum, @LoggedInUser, @LoggedInUser FROM RetailSlnSch.ItemSpecMaster ORDER BY SeqNum", sqlConnection);
            sqlCommand.Parameters.Add("@ItemId", System.Data.SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@LoggedInUser", System.Data.SqlDbType.NVarChar, 512);
            sqlCommand.Parameters["@ItemId"].Value = itemId;
            sqlCommand.Parameters["@LoggedInUser"].Value = loggedInUserId;
            sqlCommand.ExecuteNonQuery();
        }
        public static void AddItemImagesItemImageSrcSets(long itemId, List<ItemImageModel> itemImageModels, string imageExtension, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            SqlCommand sqlCommandItemImage = new SqlCommand("INSERT RetailSlnSch.ItemImage(ImageDesc, ItemId, SeqNum, AddUserId, UpdUserId) OUTPUT INSERTED.ItemImageId SELECT @ImageDesc, @ItemId, @SeqNum, @LoggedInUser, @LoggedInUser", sqlConnection);

            sqlCommandItemImage.Parameters.Add("@ImageDesc", System.Data.SqlDbType.NVarChar, 50);
            sqlCommandItemImage.Parameters.Add("@ItemId", System.Data.SqlDbType.BigInt);
            sqlCommandItemImage.Parameters.Add("@SeqNum", System.Data.SqlDbType.Float);
            sqlCommandItemImage.Parameters.Add("@LoggedInUser", System.Data.SqlDbType.NVarChar, 512);

            sqlCommandItemImage.Parameters["@ItemId"].Value = itemId;
            sqlCommandItemImage.Parameters["@LoggedInUser"].Value = loggedInUserId;

            SqlCommand sqlCommandItemImageSrcSet = new SqlCommand("INSERT RetailSlnSch.ItemImageSrcSet(ImageHeight, ImageHeightUnit, ImageName, ImageWidth, ImageWidthUnit, ItemImageId, SeqNum, AddUserId, UpdUserId) OUTPUT INSERTED.ItemImageSrcSetId SELECT @ImageHeight, @ImageHeightUnit, @ImageName, @ImageWidth, @ImageWidthUnit, @ItemImageId, @SeqNum, @LoggedInUser, @LoggedInUser", sqlConnection);

            sqlCommandItemImageSrcSet.Parameters.Add("@ImageHeight", System.Data.SqlDbType.Int);
            sqlCommandItemImageSrcSet.Parameters.Add("@ImageHeightUnit", System.Data.SqlDbType.NVarChar, 50);
            sqlCommandItemImageSrcSet.Parameters.Add("@ImageName", System.Data.SqlDbType.NVarChar, 50);
            sqlCommandItemImageSrcSet.Parameters.Add("@ImageWidth", System.Data.SqlDbType.Int);
            sqlCommandItemImageSrcSet.Parameters.Add("@ImageWidthUnit", System.Data.SqlDbType.NVarChar, 50);
            sqlCommandItemImageSrcSet.Parameters.Add("@ItemImageId", System.Data.SqlDbType.BigInt);
            sqlCommandItemImageSrcSet.Parameters.Add("@SeqNum", System.Data.SqlDbType.Float);
            sqlCommandItemImageSrcSet.Parameters.Add("@LoggedInUser", System.Data.SqlDbType.NVarChar, 512);

            sqlCommandItemImageSrcSet.Parameters["@LoggedInUser"].Value = loggedInUserId;

            SqlCommand sqlCommandItemImageSrcSetImageName = new SqlCommand("UPDATE RetailSlnSch.ItemImageSrcSet SET ImageName = @ImageName, UpdUserId = @LoggedInUser, UpdUserName = SUSER_NAME(), UpdDateTime = GETDATE() WHERE ItemImageSrcSetId = @ItemImageSrcSetId", sqlConnection);
            sqlCommandItemImageSrcSetImageName.Parameters.Add("@ImageName", System.Data.SqlDbType.NVarChar, 50);
            sqlCommandItemImageSrcSetImageName.Parameters.Add("@ItemImageSrcSetId", System.Data.SqlDbType.BigInt);
            sqlCommandItemImageSrcSetImageName.Parameters.Add("@LoggedInUser", System.Data.SqlDbType.NVarChar, 512);

            sqlCommandItemImageSrcSetImageName.Parameters["@LoggedInUser"].Value = loggedInUserId;

            foreach (var itemImageModel in itemImageModels)
            {
                sqlCommandItemImage.Parameters["@ImageDesc"].Value = itemImageModel.ImageDesc;
                sqlCommandItemImage.Parameters["@SeqNum"].Value = itemImageModel.SeqNum;
                itemImageModel.ItemImageId = (long)sqlCommandItemImage.ExecuteScalar();

                sqlCommandItemImageSrcSet.Parameters["@ItemImageId"].Value = itemImageModel.ItemImageId;
                foreach (var itemImageSrcSetModel in itemImageModel.ItemImageSrcSetModels)
                {
                    sqlCommandItemImageSrcSet.Parameters["@ImageHeight"].Value = itemImageSrcSetModel.ImageHeight;
                    sqlCommandItemImageSrcSet.Parameters["@ImageHeightUnit"].Value = itemImageSrcSetModel.ImageHeightUnit;
                    sqlCommandItemImageSrcSet.Parameters["@ImageName"].Value = itemImageSrcSetModel.ImageName;
                    sqlCommandItemImageSrcSet.Parameters["@ImageWidth"].Value = itemImageSrcSetModel.ImageWidth;
                    sqlCommandItemImageSrcSet.Parameters["@ImageWidthUnit"].Value = itemImageSrcSetModel.ImageWidthUnit;
                    sqlCommandItemImageSrcSet.Parameters["@SeqNum"].Value = itemImageSrcSetModel.SeqNum;
                    itemImageSrcSetModel.ItemImageSrcSetId = (long)sqlCommandItemImageSrcSet.ExecuteScalar();

                    sqlCommandItemImageSrcSetImageName.Parameters["@ImageName"].Value = "ItemImage" + itemImageSrcSetModel.ItemImageSrcSetId + imageExtension;
                    sqlCommandItemImageSrcSetImageName.Parameters["@ItemImageSrcSetId"].Value = itemImageSrcSetModel.ItemImageSrcSetId;
                    sqlCommandItemImageSrcSetImageName.ExecuteScalar();
                }
            }
        }
        public static void AddPersonExtn1(long personId, long corpAcctId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            SqlCommand sqlCommand = new SqlCommand("INSERT RetailSlnSch.PersonExtn1(ClientId, PersonId, CorpAcctId, CorpAcctLocationId, AddUserId, UpdUserId) SELECT @ClientId, @PersonId, @CorpAcctId, CorpAcctLocationId, @LoggedInUserId, @LoggedInUserId FROM RetailSlnSch.CorpAcctLocation WHERE CorpAcctId = @CorpAcctId", sqlConnection);
            sqlCommand.Parameters.Add("@ClientId", System.Data.SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@PersonId", System.Data.SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@CorpAcctId", System.Data.SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@LoggedInUserId", System.Data.SqlDbType.NVarChar, 512);
            sqlCommand.Parameters["@ClientId"].Value = clientId;
            sqlCommand.Parameters["@PersonId"].Value = personId;
            sqlCommand.Parameters["@CorpAcctId"].Value = corpAcctId;
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
            sqlCommand.ExecuteNonQuery();
        }
        public static void AddPersonExtn1CorpAcctLocation(long corpAcctId, long corpAcctLocationId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "";
            sqlStmt += "INSERT RetailSlnSch.PersonExtn1(ClientId, PersonId, CorpAcctId, CorpAcctLocationId, AddUserId, UpdUserId)" + Environment.NewLine;
            sqlStmt += "SELECT DISTINCT ClientId, PersonId, CorpAcctId, @CorpAcctLocationId, @LoggedInUserId, @LoggedInUserId FROM RetailSlnSch.PersonExtn1 WHERE CorpAcctId = @CorpAcctId" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@CorpAcctId", System.Data.SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@CorpAcctLocationId", System.Data.SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@LoggedInUserId", System.Data.SqlDbType.NVarChar, 512);
            sqlCommand.Parameters["@CorpAcctLocationId"].Value = corpAcctLocationId;
            sqlCommand.Parameters["@CorpAcctId"].Value = corpAcctId;
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
            sqlCommand.ExecuteNonQuery();
        }
    }
}
