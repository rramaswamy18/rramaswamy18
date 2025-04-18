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
        public static void OrderDeliveryAdd(DeliveryDataModel deliveryDataModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Before calling the BuildSqlCommandAspNetUserRoles()", "AspNetUserId", "");
                #region
                string sqlStmt = "         INSERT RetailSlnSch.OrderDelivery" + Environment.NewLine;
                sqlStmt += "              (" + Environment.NewLine;
                sqlStmt += "               ClientId" + Environment.NewLine;
                sqlStmt += "              ,AlternateTelephoneDemogInfoCountryId" + Environment.NewLine;
                sqlStmt += "              ,AlternateTelephoneNum" + Environment.NewLine;
                sqlStmt += "              ,DeliveryAddressId" + Environment.NewLine;
                sqlStmt += "              ,DeliveryInstructions" + Environment.NewLine;
                sqlStmt += "              ,DeliveryMethodId" + Environment.NewLine;
                sqlStmt += "              ,OrderHeaderId" + Environment.NewLine;
                sqlStmt += "              ,PickupLocationId" + Environment.NewLine;
                sqlStmt += "              ,PrimaryTelephoneDemogInfoCountryId" + Environment.NewLine;
                sqlStmt += "              ,PrimaryTelephoneNum" + Environment.NewLine;
                sqlStmt += "              ,AddUserId" + Environment.NewLine;
                sqlStmt += "              ,UpdUserId" + Environment.NewLine;
                sqlStmt += "              )" + Environment.NewLine;
                sqlStmt += "        OUTPUT INSERTED.OrderDeliveryId" + Environment.NewLine;
                sqlStmt += "        SELECT" + Environment.NewLine;
                sqlStmt += "               @ClientId" + Environment.NewLine;
                sqlStmt += "              ,@AlternateTelephoneDemogInfoCountryId" + Environment.NewLine;
                sqlStmt += "              ,@AlternateTelephoneNum" + Environment.NewLine;
                sqlStmt += "              ,@DeliveryAddressId" + Environment.NewLine;
                sqlStmt += "              ,@DeliveryInstructions" + Environment.NewLine;
                sqlStmt += "              ,@DeliveryMethodId" + Environment.NewLine;
                sqlStmt += "              ,@OrderHeaderId" + Environment.NewLine;
                sqlStmt += "              ,@PickupLocationId" + Environment.NewLine;
                sqlStmt += "              ,@PrimaryTelephoneDemogInfoCountryId" + Environment.NewLine;
                sqlStmt += "              ,@PrimaryTelephoneNum" + Environment.NewLine;
                sqlStmt += "              ,@LoggedInUserId" + Environment.NewLine;
                sqlStmt += "              ,@LoggedInUserId" + Environment.NewLine;
                #endregion
                #region
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                sqlCommand.Parameters.Add("@ClientId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@AlternateTelephoneDemogInfoCountryId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@AlternateTelephoneNum", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@DeliveryAddressId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@DeliveryInstructions", SqlDbType.NVarChar, 250);
                sqlCommand.Parameters.Add("@DeliveryMethodId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@OrderHeaderId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@PickupLocationId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@PrimaryTelephoneDemogInfoCountryId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@PrimaryTelephoneNum", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 256);
                sqlCommand.Parameters.Add("@OrderDeliveryId", SqlDbType.BigInt);
                sqlCommand.Parameters["@OrderDeliveryId"].Direction = ParameterDirection.ReturnValue;
                #endregion
                #region
                sqlCommand.Parameters["@ClientId"].Value = clientId;
                sqlCommand.Parameters["@AlternateTelephoneDemogInfoCountryId"].Value = deliveryDataModel.AlternateTelephoneDemogInfoCountryId;
                sqlCommand.Parameters["@AlternateTelephoneNum"].Value = string.IsNullOrWhiteSpace(deliveryDataModel.AlternateTelephoneNum) ? (object)DBNull.Value : deliveryDataModel.AlternateTelephoneNum;
                sqlCommand.Parameters["@DeliveryAddressId"].Value = deliveryDataModel.DeliveryAddressModel.DemogInfoAddressId;
                sqlCommand.Parameters["@DeliveryInstructions"].Value = string.IsNullOrWhiteSpace(deliveryDataModel.DeliveryInstructions) ? (object)DBNull.Value : deliveryDataModel.DeliveryInstructions;
                sqlCommand.Parameters["@DeliveryMethodId"].Value = deliveryDataModel.DeliveryMethodId.Value;
                sqlCommand.Parameters["@OrderHeaderId"].Value = deliveryDataModel.OrderHeaderId;
                sqlCommand.Parameters["@PickupLocationId"].Value = deliveryDataModel.PickupLocationId.Value;
                sqlCommand.Parameters["@PrimaryTelephoneDemogInfoCountryId"].Value = deliveryDataModel.PrimaryTelephoneDemogInfoCountryId;
                sqlCommand.Parameters["@PrimaryTelephoneNum"].Value = deliveryDataModel.PrimaryTelephoneNum;
                sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
                #endregion
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
        public static void OrderDetailAdd(OrderDetail orderDetail, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Before calling BuildSqlCommandOrderDetailAdd()", "orderDetail.OrderHeaderId", orderDetail.OrderHeaderSummaryId.ToString(), "orderDetail.ItemId", orderDetail.ItemId.ToString(), "orderDetail.SeqNum", orderDetail.SeqNum.ToString());
                #region
                string sqlStmt = "";
                sqlStmt += "        INSERT RetailSlnSch.OrderDetail" + Environment.NewLine;
                sqlStmt += "              (" + Environment.NewLine;
                sqlStmt += "               ClientId" + Environment.NewLine;
                sqlStmt += "              ,DimensionUnitId" + Environment.NewLine;
                sqlStmt += "              ,ItemId" + Environment.NewLine;
                sqlStmt += "              ,ItemShortDesc" + Environment.NewLine;
                sqlStmt += "              ,ItemRate" + Environment.NewLine;
                sqlStmt += "              ,LengthValue" + Environment.NewLine;
                sqlStmt += "              ,OrderAmount" + Environment.NewLine;
                sqlStmt += "              ,OrderComments" + Environment.NewLine;
                sqlStmt += "              ,OrderHeaderSummaryId" + Environment.NewLine;
                sqlStmt += "              ,OrderDetailTypeId" + Environment.NewLine;
                sqlStmt += "              ,OrderQty" + Environment.NewLine;
                sqlStmt += "              ,SeqNum" + Environment.NewLine;
                sqlStmt += "              ,VolumeValue" + Environment.NewLine;
                sqlStmt += "              ,WeightUnitId" + Environment.NewLine;
                sqlStmt += "              ,WeightValue" + Environment.NewLine;
                sqlStmt += "              ,WidthValue" + Environment.NewLine;
                sqlStmt += "              ,AddUserId" + Environment.NewLine;
                sqlStmt += "              ,UpdUserId" + Environment.NewLine;
                sqlStmt += "              )" + Environment.NewLine;
                sqlStmt += "        OUTPUT INSERTED.OrderDetailId" + Environment.NewLine;
                sqlStmt += "        SELECT" + Environment.NewLine;
                sqlStmt += "               @ClientId" + Environment.NewLine;
                sqlStmt += "              ,@DimensionUnitId" + Environment.NewLine;
                sqlStmt += "              ,@ItemId" + Environment.NewLine;
                sqlStmt += "              ,@ItemShortDesc" + Environment.NewLine;
                sqlStmt += "              ,@ItemRate" + Environment.NewLine;
                sqlStmt += "              ,@LengthValue" + Environment.NewLine;
                sqlStmt += "              ,@OrderAmount" + Environment.NewLine;
                sqlStmt += "              ,@OrderComments" + Environment.NewLine;
                sqlStmt += "              ,@OrderHeaderSummaryId" + Environment.NewLine;
                sqlStmt += "              ,@OrderDetailTypeId" + Environment.NewLine;
                sqlStmt += "              ,@OrderQty" + Environment.NewLine;
                sqlStmt += "              ,@SeqNum" + Environment.NewLine;
                sqlStmt += "              ,@VolumeValue" + Environment.NewLine;
                sqlStmt += "              ,@WeightUnitId" + Environment.NewLine;
                sqlStmt += "              ,@WeightValue" + Environment.NewLine;
                sqlStmt += "              ,@WidthValue" + Environment.NewLine;
                sqlStmt += "              ,@LoggedInUserId" + Environment.NewLine;
                sqlStmt += "              ,@LoggedInUserId" + Environment.NewLine;
                #endregion
                #region
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                sqlCommand.Parameters.Add("@ClientId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@DimensionUnitId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@ItemId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@ItemShortDesc", SqlDbType.NVarChar, 512);
                sqlCommand.Parameters.Add("@ItemRate", SqlDbType.Float);
                sqlCommand.Parameters.Add("@LengthValue", SqlDbType.Float);
                sqlCommand.Parameters.Add("@OrderAmount", SqlDbType.Float);
                sqlCommand.Parameters.Add("@OrderComments", SqlDbType.NVarChar, 250);
                sqlCommand.Parameters.Add("@OrderHeaderSummaryId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@OrderDetailTypeId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@OrderQty", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@SeqNum", SqlDbType.Float);
                sqlCommand.Parameters.Add("@VolumeValue", SqlDbType.Float);
                sqlCommand.Parameters.Add("@WeightUnitId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@WeightValue", SqlDbType.Float);
                sqlCommand.Parameters.Add("@WidthValue", SqlDbType.Float);
                sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 256);
                sqlCommand.Parameters.Add("@OrderDetailId", SqlDbType.BigInt);
                sqlCommand.Parameters["@OrderDetailId"].Direction = ParameterDirection.ReturnValue;
                #endregion
                #region
                sqlCommand.Parameters["@ClientId"].Value = clientId;
                sqlCommand.Parameters["@DimensionUnitId"].Value = orderDetail.DimensionUnitId;
                sqlCommand.Parameters["@ItemId"].Value = orderDetail.ItemId == null ? (object)DBNull.Value : orderDetail.ItemId;
                sqlCommand.Parameters["@ItemRate"].Value = orderDetail.ItemRate;
                sqlCommand.Parameters["@ItemShortDesc"].Value = orderDetail.ItemShortDesc;
                sqlCommand.Parameters["@LengthValue"].Value = orderDetail.LengthValue;
                sqlCommand.Parameters["@OrderAmount"].Value = orderDetail.OrderAmount;
                sqlCommand.Parameters["@OrderComments"].Value = string.IsNullOrEmpty(orderDetail.OrderComments) ? (object)DBNull.Value : orderDetail.OrderComments;
                sqlCommand.Parameters["@OrderDetailTypeId"].Value = (int)orderDetail.OrderDetailTypeId;
                sqlCommand.Parameters["@OrderHeaderSummaryId"].Value = orderDetail.OrderHeaderSummaryId;
                sqlCommand.Parameters["@OrderQty"].Value = orderDetail.OrderQty;
                sqlCommand.Parameters["@SeqNum"].Value = orderDetail.SeqNum;
                sqlCommand.Parameters["@VolumeValue"].Value = orderDetail.VolumeValue;
                sqlCommand.Parameters["@WeightUnitId"].Value = orderDetail.WeightUnitId;
                sqlCommand.Parameters["@WeightValue"].Value = orderDetail.WeightValue;
                sqlCommand.Parameters["@WidthValue"].Value = orderDetail.WidthValue;
                sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
                #endregion
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
        public static void OrderDetailItemBundleAdd(OrderDetailItemBundle orderDetailItemBundle, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Before calling the BuildSqlCommandAspNetUserRoles()", "AspNetUserId", "");
                #region
                string sqlStmt = "";
                sqlStmt += "        INSERT RetailSlnSch.OrderDetailItemBundle" + Environment.NewLine;
                sqlStmt += "              (" + Environment.NewLine;
                sqlStmt += "               ClientId" + Environment.NewLine;
                sqlStmt += "              ,DiscountPercent" + Environment.NewLine;
                sqlStmt += "              ,ItemBundleId" + Environment.NewLine;
                sqlStmt += "              ,ItemBundleIItemId" + Environment.NewLine;
                sqlStmt += "              ,ItemMasterDesc" + Environment.NewLine;
                sqlStmt += "              ,ItemRate" + Environment.NewLine;
                sqlStmt += "              ,ItemRateBeforeDiscount" + Environment.NewLine;
                sqlStmt += "              ,OrderAmount" + Environment.NewLine;
                sqlStmt += "              ,OrderAmountBeforeDiscount" + Environment.NewLine;
                sqlStmt += "              ,OrderDetailId" + Environment.NewLine;
                sqlStmt += "              ,OrderQty" + Environment.NewLine;
                sqlStmt += "              ,SeqNum" + Environment.NewLine;
                sqlStmt += "              ,AddUserId" + Environment.NewLine;
                sqlStmt += "              ,UpdUserId" + Environment.NewLine;
                sqlStmt += "              )" + Environment.NewLine;
                sqlStmt += "        OUTPUT INSERTED.OrderDetailItemBundleId" + Environment.NewLine;
                sqlStmt += "        SELECT" + Environment.NewLine;
                sqlStmt += "               @ClientId" + Environment.NewLine;
                sqlStmt += "              ,@DiscountPercent" + Environment.NewLine;
                sqlStmt += "              ,@ItemBundleId" + Environment.NewLine;
                sqlStmt += "              ,@ItemBundleIItemId" + Environment.NewLine;
                sqlStmt += "              ,@ItemMasterDesc" + Environment.NewLine;
                sqlStmt += "              ,@ItemRate" + Environment.NewLine;
                sqlStmt += "              ,@ItemRateBeforeDiscount" + Environment.NewLine;
                sqlStmt += "              ,@OrderAmount" + Environment.NewLine;
                sqlStmt += "              ,@OrderAmountBeforeDiscount" + Environment.NewLine;
                sqlStmt += "              ,@OrderDetailId" + Environment.NewLine;
                sqlStmt += "              ,@OrderQty" + Environment.NewLine;
                sqlStmt += "              ,@SeqNum" + Environment.NewLine;
                sqlStmt += "              ,@LoggedInUserId" + Environment.NewLine;
                sqlStmt += "              ,@LoggedInUserId" + Environment.NewLine;
                #endregion
                #region
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                sqlCommand.Parameters.Add("@ClientId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@DiscountPercent", SqlDbType.Float);
                sqlCommand.Parameters.Add("@ItemBundleId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@ItemBundleIItemId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@ItemMasterDesc", SqlDbType.NVarChar, 512);
                sqlCommand.Parameters.Add("@ItemRate", SqlDbType.Float);
                sqlCommand.Parameters.Add("@ItemRateBeforeDiscount", SqlDbType.Float);
                sqlCommand.Parameters.Add("@OrderAmount", SqlDbType.Float);
                sqlCommand.Parameters.Add("@OrderAmountBeforeDiscount", SqlDbType.Float);
                sqlCommand.Parameters.Add("@OrderDetailId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@OrderQty", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@SeqNum", SqlDbType.Float);
                sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 256);
                sqlCommand.Parameters.Add("@OrderDetailItemBundleId", SqlDbType.BigInt);
                sqlCommand.Parameters["@OrderDetailItemBundleId"].Direction = ParameterDirection.ReturnValue;
                #endregion
                #region
                sqlCommand.Parameters["@ClientId"].Value = clientId;
                sqlCommand.Parameters["@DiscountPercent"].Value = orderDetailItemBundle.DiscountPercent;
                sqlCommand.Parameters["@ItemBundleId"].Value = orderDetailItemBundle.ItemBundleId;
                sqlCommand.Parameters["@ItemBundleIItemId"].Value = orderDetailItemBundle.ItemBundleItemId;
                sqlCommand.Parameters["@ItemMasterDesc"].Value = orderDetailItemBundle.ItemMasterDesc;
                sqlCommand.Parameters["@ItemRate"].Value = orderDetailItemBundle.ItemRate;
                sqlCommand.Parameters["@ItemRateBeforeDiscount"].Value = orderDetailItemBundle.ItemRateBeforeDiscount;
                sqlCommand.Parameters["@OrderAmount"].Value = orderDetailItemBundle.OrderAmount;
                sqlCommand.Parameters["@OrderAmountBeforeDiscount"].Value = orderDetailItemBundle.OrderAmountBeforeDiscount;
                sqlCommand.Parameters["@OrderDetailId"].Value = orderDetailItemBundle.OrderDetailId;
                sqlCommand.Parameters["@OrderQty"].Value = orderDetailItemBundle.OrderQty;
                sqlCommand.Parameters["@SeqNum"].Value = orderDetailItemBundle.SeqNum;
                sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
                #endregion
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
        public static void OrderDetailWIPAdd(OrderDetailWIPModel orderDetailWIPModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
        public static void OrderDetailWIPUpd(long orderHeaderWIPId, long itemId, long orderQty, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                #region
                string sqlStmt = "";
                sqlStmt += "        UPDATE RetailSlnSch.OrderDetailWIP" + Environment.NewLine;
                sqlStmt += "           SET OrderQty = @OrderQty" + Environment.NewLine;
                sqlStmt += "              ,UpdUserId = @LoggedInUserId" + Environment.NewLine;
                sqlStmt += "              ,UpdDateTime = GETDATE()" + Environment.NewLine;
                sqlStmt += "         WHERE OrderHeaderWIPId = @OrderHeaderWIPId" + Environment.NewLine;
                sqlStmt += "           AND ItemId = @ItemId" + Environment.NewLine;
                #endregion
                #region
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                sqlCommand.Parameters.Add("@OrderQty", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 256);
                sqlCommand.Parameters.Add("@OrderHeaderWIPId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@ItemId", SqlDbType.BigInt);
                #endregion
                #region
                sqlCommand.Parameters["@OrderQty"].Value = orderQty;
                sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
                sqlCommand.Parameters["@OrderHeaderWIPId"].Value = orderHeaderWIPId;
                sqlCommand.Parameters["@ItemId"].Value = itemId;
                #endregion
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static void OrderHeaderAdd(OrderHeader orderHeader, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Before calling the BuildSqlCommandAspNetUserRoles()", "AspNetUserId", "");
                #region
                string sqlStmt = "";
                sqlStmt += "        INSERT RetailSlnSch.OrderHeader" + Environment.NewLine;
                sqlStmt += "              (" + Environment.NewLine;
                sqlStmt += "               ClientId" + Environment.NewLine;
                sqlStmt += "              ,CreatedForPersonId" + Environment.NewLine;
                sqlStmt += "              ,OrderDateTime" + Environment.NewLine;
                sqlStmt += "              ,OrderStatusId" + Environment.NewLine;
                sqlStmt += "              ,PersonId" + Environment.NewLine;
                sqlStmt += "              ,SaveThisAddress" + Environment.NewLine;
                sqlStmt += "              ,AddUserId" + Environment.NewLine;
                sqlStmt += "              ,UpdUserId" + Environment.NewLine;
                sqlStmt += "              )" + Environment.NewLine;
                sqlStmt += "        OUTPUT INSERTED.OrderHeaderId" + Environment.NewLine;
                sqlStmt += "        SELECT" + Environment.NewLine;
                sqlStmt += "               @ClientId" + Environment.NewLine;
                sqlStmt += "              ,@CreatedForPersonId" + Environment.NewLine;
                sqlStmt += "              ,@OrderDateTime" + Environment.NewLine;
                sqlStmt += "              ,@OrderStatusId" + Environment.NewLine;
                sqlStmt += "              ,@PersonId" + Environment.NewLine;
                sqlStmt += "              ,@SaveThisAddress" + Environment.NewLine;
                sqlStmt += "              ,@LoggedInUserId" + Environment.NewLine;
                sqlStmt += "              ,@LoggedInUserId" + Environment.NewLine;
                #endregion
                #region
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                sqlCommand.Parameters.Add("@ClientId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@CreatedForPersonId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@OrderDateTime", SqlDbType.VarChar, 21);
                sqlCommand.Parameters.Add("@OrderStatusId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@PersonId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@SaveThisAddress", SqlDbType.Bit);
                sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 256);
                sqlCommand.Parameters.Add("@OrderHeaderId", SqlDbType.BigInt);
                sqlCommand.Parameters["@OrderHeaderId"].Direction = ParameterDirection.ReturnValue;
                #endregion
                #region
                sqlCommand.Parameters["@ClientId"].Value = clientId;
                sqlCommand.Parameters["@CreatedForPersonId"].Value = (int)orderHeader.CreatedForPersonId;
                sqlCommand.Parameters["@OrderDateTime"].Value = orderHeader.OrderDateTime;
                sqlCommand.Parameters["@OrderStatusId"].Value = orderHeader.OrderStatusId;
                sqlCommand.Parameters["@PersonId"].Value = orderHeader.PersonId;
                sqlCommand.Parameters["@SaveThisAddress"].Value = orderHeader.SaveThisAddress ? 1 : 0;
                sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
                #endregion
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
        public static void OrderHeaderSummaryAdd(OrderHeaderSummary orderHeaderSummary, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Before calling the BuildSqlCommandAspNetUserRoles()", "AspNetUserId", "");
                #region
                string sqlStmt = "";
                sqlStmt += "        INSERT RetailSlnSch.OrderHeaderSummary" + Environment.NewLine;
                sqlStmt += "              (" + Environment.NewLine;
                sqlStmt += "               ClientId" + Environment.NewLine;
                sqlStmt += "              ,BalanceDue" + Environment.NewLine;
                sqlStmt += "              ,InvoiceTypeId" + Environment.NewLine;
                sqlStmt += "              ,OrderHeaderId" + Environment.NewLine;
                sqlStmt += "              ,ShippingAndHandlingCharges" + Environment.NewLine;
                sqlStmt += "              ,TotalAmountPaid" + Environment.NewLine;
                sqlStmt += "              ,TotalDiscountAmount" + Environment.NewLine;
                sqlStmt += "              ,TotalInvoiceAmount" + Environment.NewLine;
                sqlStmt += "              ,TotalOrderAmount" + Environment.NewLine;
                sqlStmt += "              ,TotalTaxAmount" + Environment.NewLine;
                sqlStmt += "              ,AddUserId" + Environment.NewLine;
                sqlStmt += "              ,UpdUserId" + Environment.NewLine;
                sqlStmt += "              )" + Environment.NewLine;
                sqlStmt += "        OUTPUT INSERTED.OrderHeaderSummaryId" + Environment.NewLine;
                sqlStmt += "        SELECT" + Environment.NewLine;
                sqlStmt += "               @ClientId" + Environment.NewLine;
                sqlStmt += "              ,@BalanceDue" + Environment.NewLine;
                sqlStmt += "              ,@InvoiceTypeId" + Environment.NewLine;
                sqlStmt += "              ,@OrderHeaderId" + Environment.NewLine;
                sqlStmt += "              ,@ShippingAndHandlingCharges" + Environment.NewLine;
                sqlStmt += "              ,@TotalAmountPaid" + Environment.NewLine;
                sqlStmt += "              ,@TotalDiscountAmount" + Environment.NewLine;
                sqlStmt += "              ,@TotalInvoiceAmount" + Environment.NewLine;
                sqlStmt += "              ,@TotalOrderAmount" + Environment.NewLine;
                sqlStmt += "              ,@TotalTaxAmount" + Environment.NewLine;
                sqlStmt += "              ,@LoggedInUserId" + Environment.NewLine;
                sqlStmt += "              ,@LoggedInUserId" + Environment.NewLine;
                #endregion
                #region
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                sqlCommand.Parameters.Add("@ClientId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@BalanceDue", SqlDbType.Float);
                sqlCommand.Parameters.Add("@InvoiceTypeId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@OrderHeaderId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@ShippingAndHandlingCharges", SqlDbType.Float);
                sqlCommand.Parameters.Add("@TotalAmountPaid", SqlDbType.Float);
                sqlCommand.Parameters.Add("@TotalDiscountAmount", SqlDbType.Float);
                sqlCommand.Parameters.Add("@TotalInvoiceAmount", SqlDbType.Float);
                sqlCommand.Parameters.Add("@TotalOrderAmount", SqlDbType.Float);
                sqlCommand.Parameters.Add("@TotalTaxAmount", SqlDbType.Float);
                sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 256);
                sqlCommand.Parameters.Add("@OrderHeaderSummaryId", SqlDbType.BigInt);
                sqlCommand.Parameters["@OrderHeaderSummaryId"].Direction = ParameterDirection.ReturnValue;
                #endregion
                #region
                sqlCommand.Parameters["@ClientId"].Value = clientId;
                sqlCommand.Parameters["@BalanceDue"].Value = orderHeaderSummary.BalanceDue;
                sqlCommand.Parameters["@InvoiceTypeId"].Value = (int)orderHeaderSummary.InvoiceTypeId;
                sqlCommand.Parameters["@OrderHeaderId"].Value = orderHeaderSummary.OrderHeaderId;
                sqlCommand.Parameters["@ShippingAndHandlingCharges"].Value = orderHeaderSummary.ShippingAndHandlingCharges;
                sqlCommand.Parameters["@TotalAmountPaid"].Value = orderHeaderSummary.TotalAmountPaid;
                sqlCommand.Parameters["@TotalDiscountAmount"].Value = orderHeaderSummary.TotalDiscountAmount;
                sqlCommand.Parameters["@TotalInvoiceAmount"].Value = orderHeaderSummary.TotalInvoiceAmount;
                sqlCommand.Parameters["@TotalOrderAmount"].Value = orderHeaderSummary.TotalOrderAmount;
                sqlCommand.Parameters["@TotalTaxAmount"].Value = orderHeaderSummary.TotalTaxAmount;
                sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
                #endregion
                orderHeaderSummary.OrderHeaderSummaryId = (long)sqlCommand.ExecuteScalar();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00009000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static void OrderHeaderWIPAdd(OrderHeaderWIPModel orderHeaderWIPModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
                sqlStmt += "              ,InvoiceTypeId" + Environment.NewLine;
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
                sqlStmt += "              ,@InvoiceTypeId" + Environment.NewLine;
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
                sqlCommand.Parameters.Add("@InvoiceTypeId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@OrderDateTime", SqlDbType.NVarChar, 21);
                sqlCommand.Parameters.Add("@OrderStatusId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@PersonId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 256);
                #endregion
                #region
                sqlCommand.Parameters["@ClientId"].Value = orderHeaderWIPModel.ClientId;
                sqlCommand.Parameters["@CorpAcctLocationId"].Value = orderHeaderWIPModel.CorpAcctLocationId;
                sqlCommand.Parameters["@CreatedForPersonId"].Value = orderHeaderWIPModel.CreatedForPersonId;
                sqlCommand.Parameters["@InvoiceTypeId"].Value = orderHeaderWIPModel.InvoiceTypeId;
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
        public static void OrderPaymentAdd(PaymentDataModel paymentDataModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 Before SqlStmt", "orderDetail.OrderHeaderId", paymentDataModel.OrderHeaderId.ToString(), "paymentData1Model.CreditCardDataId", paymentDataModel.CreditCardDataId.ToString());
                #region
                string sqlStmt = "";
                sqlStmt += "        INSERT RetailSlnSch.OrderPayment" + Environment.NewLine;
                sqlStmt += "              (" + Environment.NewLine;
                sqlStmt += "               ClientId" + Environment.NewLine;
                sqlStmt += "              ,CouponId" + Environment.NewLine;
                sqlStmt += "              ,CreditCardDataId" + Environment.NewLine;
                sqlStmt += "              ,GiftCertId" + Environment.NewLine;
                sqlStmt += "              ,OrderHeaderId" + Environment.NewLine;
                sqlStmt += "              ,PaymentModeId" + Environment.NewLine;
                sqlStmt += "              ,AddUserId" + Environment.NewLine;
                sqlStmt += "              ,UpdUserId" + Environment.NewLine;
                sqlStmt += "              )" + Environment.NewLine;
                sqlStmt += "        OUTPUT INSERTED.OrderPaymentId" + Environment.NewLine;
                sqlStmt += "        SELECT" + Environment.NewLine;
                sqlStmt += "               @ClientId" + Environment.NewLine;
                sqlStmt += "              ,@CouponId" + Environment.NewLine;
                sqlStmt += "              ,@CreditCardDataId" + Environment.NewLine;
                sqlStmt += "              ,@GiftCertId" + Environment.NewLine;
                sqlStmt += "              ,@OrderHeaderId" + Environment.NewLine;
                sqlStmt += "              ,@PaymentModeId" + Environment.NewLine;
                sqlStmt += "              ,@LoggedInUserId" + Environment.NewLine;
                sqlStmt += "              ,@LoggedInUserId" + Environment.NewLine;
                #endregion
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Before SqlCommand", "orderDetail.OrderHeaderId", paymentDataModel.OrderHeaderId.ToString(), "paymentData1Model.CreditCardDataId", paymentDataModel.CreditCardDataId.ToString());
                #region
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                sqlCommand.Parameters.Add("@ClientId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@CouponId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@CreditCardDataId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@GiftCertId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@OrderHeaderId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@PaymentModeId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 256);
                sqlCommand.Parameters.Add("@OrderPaymentId", SqlDbType.BigInt);
                sqlCommand.Parameters["@OrderPaymentId"].Direction = ParameterDirection.ReturnValue;
                #endregion
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00003000 Before SqlCommand Assign", "orderDetail.OrderHeaderId", paymentDataModel.OrderHeaderId.ToString(), "paymentData1Model.CreditCardDataId", paymentDataModel.CreditCardDataId.ToString());
                #region
                sqlCommand.Parameters["@ClientId"].Value = clientId;
                sqlCommand.Parameters["@CouponId"].Value = paymentDataModel.CouponId;
                sqlCommand.Parameters["@CreditCardDataId"].Value = paymentDataModel.CreditCardDataId;
                sqlCommand.Parameters["@GiftCertId"].Value = paymentDataModel.GiftCertId;
                sqlCommand.Parameters["@OrderHeaderId"].Value = paymentDataModel.OrderHeaderId;
                sqlCommand.Parameters["@PaymentModeId"].Value = paymentDataModel.PaymentModeId;
                sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
                #endregion
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00004000 Before ExecuteScalar", "orderDetail.OrderHeaderId", paymentDataModel.OrderHeaderId.ToString(), "paymentData1Model.CreditCardDataId", paymentDataModel.CreditCardDataId.ToString());
                paymentDataModel.OrderPaymentId = (long)sqlCommand.ExecuteScalar();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00009000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static void PersonExtn1Add(long personId, long corpAcctId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
    }
}
