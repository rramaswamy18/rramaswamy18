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
        public static void CouponListAdd(CouponListModel couponListModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                #region
                string sqlStmt = "";
                sqlStmt += "         INSERT RetailSlnSch.CouponList" + Environment.NewLine;
                sqlStmt += "               (" + Environment.NewLine;
                sqlStmt += "                ClientId" + Environment.NewLine;
                sqlStmt += "               ,CouponNum" + Environment.NewLine;
                sqlStmt += "               ,BegEffDate" + Environment.NewLine;
                sqlStmt += "               ,DiscountPercent" + Environment.NewLine;
                sqlStmt += "               ,EndEffDate" + Environment.NewLine;
                sqlStmt += "               ,AddUserId" + Environment.NewLine;
                sqlStmt += "               ,UpdUserId" + Environment.NewLine;
                sqlStmt += "               )" + Environment.NewLine;
                sqlStmt += "         OUTPUT INSERTED.CouponListId" + Environment.NewLine;
                sqlStmt += "         SELECT " + Environment.NewLine;
                sqlStmt += "                @ClientId" + Environment.NewLine;
                sqlStmt += "               ,@CouponNum" + Environment.NewLine;
                sqlStmt += "               ,@BegEffDate" + Environment.NewLine;
                sqlStmt += "               ,@DiscountPercent" + Environment.NewLine;
                sqlStmt += "               ,@EndEffDate" + Environment.NewLine;
                sqlStmt += "               ,@LoggedInUserId" + Environment.NewLine;
                sqlStmt += "               ,@LoggedInUserId" + Environment.NewLine;
                #endregion
                #region
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                sqlCommand.Parameters.Add("@ClientId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@CouponNum", SqlDbType.NVarChar, 50);
                sqlCommand.Parameters.Add("@BegEffDate", SqlDbType.NVarChar, 10);
                sqlCommand.Parameters.Add("@DiscountPercent", SqlDbType.Float);
                sqlCommand.Parameters.Add("@EndEffDate", SqlDbType.NVarChar, 10);
                sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 256);
                #endregion
                #region
                sqlCommand.Parameters["@ClientId"].Value = clientId;
                sqlCommand.Parameters["@CouponNum"].Value = couponListModel.CouponNum;
                sqlCommand.Parameters["@BegEffDate"].Value = couponListModel.BegEffDate;
                sqlCommand.Parameters["@DiscountPercent"].Value = couponListModel.DiscountPercent;
                sqlCommand.Parameters["@EndEffDate"].Value = couponListModel.EndEffDate;
                sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
                #endregion
                couponListModel.CouponListId = (long)sqlCommand.ExecuteScalar();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00009000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
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
                sqlStmt += "              ,DoNotBreakBundle" + Environment.NewLine;
                sqlStmt += "              ,DimensionUnitId" + Environment.NewLine;
                sqlStmt += "              ,DiscountPercent" + Environment.NewLine;
                sqlStmt += "              ,DiscountPercentOriginal" + Environment.NewLine;
                sqlStmt += "              ,HeightValue" + Environment.NewLine;
                sqlStmt += "              ,HSNCode" + Environment.NewLine;
                sqlStmt += "              ,ItemDiscountAmount" + Environment.NewLine;
                sqlStmt += "              ,ItemId" + Environment.NewLine;
                sqlStmt += "              ,ItemItemSpecsForDisplay" + Environment.NewLine;
                sqlStmt += "              ,ItemMasterDesc0" + Environment.NewLine;
                sqlStmt += "              ,ItemMasterDesc1" + Environment.NewLine;
                sqlStmt += "              ,ItemMasterDesc2" + Environment.NewLine;
                sqlStmt += "              ,ItemMasterDesc3" + Environment.NewLine;
                sqlStmt += "              ,ItemRate" + Environment.NewLine;
                sqlStmt += "              ,ItemRateBeforeDiscount" + Environment.NewLine;
                sqlStmt += "              ,ItemRateOriginal" + Environment.NewLine;
                sqlStmt += "              ,LengthValue" + Environment.NewLine;
                sqlStmt += "              ,OrderAmount" + Environment.NewLine;
                sqlStmt += "              ,OrderAmountBeforeDiscount" + Environment.NewLine;
                sqlStmt += "              ,OrderComments" + Environment.NewLine;
                sqlStmt += "              ,OrderDetailTypeId" + Environment.NewLine;
                sqlStmt += "              ,OrderHeaderSummaryId" + Environment.NewLine;
                sqlStmt += "              ,OrderQty" + Environment.NewLine;
                sqlStmt += "              ,ParentItemId" + Environment.NewLine;
                sqlStmt += "              ,ProductCode" + Environment.NewLine;
                sqlStmt += "              ,ProductOrVolumetricWeight" + Environment.NewLine;
                sqlStmt += "              ,ProductOrVolumetricWeightUnitId" + Environment.NewLine;
                sqlStmt += "              ,SeqNum" + Environment.NewLine;
                sqlStmt += "              ,VolumeValue" + Environment.NewLine;
                sqlStmt += "              ,WeightCalcUnitId" + Environment.NewLine;
                sqlStmt += "              ,WeightCalcValue" + Environment.NewLine;
                sqlStmt += "              ,WeightUnitId" + Environment.NewLine;
                sqlStmt += "              ,WeightValue" + Environment.NewLine;
                sqlStmt += "              ,WidthValue" + Environment.NewLine;
                sqlStmt += "              ,AddUserId" + Environment.NewLine;
                sqlStmt += "              ,UpdUserId" + Environment.NewLine;
                sqlStmt += "              )" + Environment.NewLine;
                sqlStmt += "        OUTPUT INSERTED.OrderDetailId" + Environment.NewLine;
                sqlStmt += "        SELECT" + Environment.NewLine;
                sqlStmt += "               @ClientId" + Environment.NewLine;
                sqlStmt += "              ,@DoNotBreakBundle" + Environment.NewLine;
                sqlStmt += "              ,@DimensionUnitId" + Environment.NewLine;
                sqlStmt += "              ,@DiscountPercent" + Environment.NewLine;
                sqlStmt += "              ,@DiscountPercentOriginal" + Environment.NewLine;
                sqlStmt += "              ,@HeightValue" + Environment.NewLine;
                sqlStmt += "              ,@HSNCode" + Environment.NewLine;
                sqlStmt += "              ,@ItemDiscountAmount" + Environment.NewLine;
                sqlStmt += "              ,@ItemId" + Environment.NewLine;
                sqlStmt += "              ,@ItemItemSpecsForDisplay" + Environment.NewLine;
                sqlStmt += "              ,@ItemMasterDesc0" + Environment.NewLine;
                sqlStmt += "              ,@ItemMasterDesc1" + Environment.NewLine;
                sqlStmt += "              ,@ItemMasterDesc2" + Environment.NewLine;
                sqlStmt += "              ,@ItemMasterDesc3" + Environment.NewLine;
                sqlStmt += "              ,@ItemRate" + Environment.NewLine;
                sqlStmt += "              ,@ItemRateBeforeDiscount" + Environment.NewLine;
                sqlStmt += "              ,@ItemRateOriginal" + Environment.NewLine;
                sqlStmt += "              ,@LengthValue" + Environment.NewLine;
                sqlStmt += "              ,@OrderAmount" + Environment.NewLine;
                sqlStmt += "              ,@OrderAmountBeforeDiscount" + Environment.NewLine;
                sqlStmt += "              ,@OrderComments" + Environment.NewLine;
                sqlStmt += "              ,@OrderDetailTypeId" + Environment.NewLine;
                sqlStmt += "              ,@OrderHeaderSummaryId" + Environment.NewLine;
                sqlStmt += "              ,@OrderQty" + Environment.NewLine;
                sqlStmt += "              ,@ParentItemId" + Environment.NewLine;
                sqlStmt += "              ,@ProductCode" + Environment.NewLine;
                sqlStmt += "              ,@ProductOrVolumetricWeight" + Environment.NewLine;
                sqlStmt += "              ,@ProductOrVolumetricWeightUnitId" + Environment.NewLine;
                sqlStmt += "              ,@SeqNum" + Environment.NewLine;
                sqlStmt += "              ,@VolumeValue" + Environment.NewLine;
                sqlStmt += "              ,@WeightCalcUnitId" + Environment.NewLine;
                sqlStmt += "              ,@WeightCalcValue" + Environment.NewLine;
                sqlStmt += "              ,@WeightUnitId" + Environment.NewLine;
                sqlStmt += "              ,@WeightValue" + Environment.NewLine;
                sqlStmt += "              ,@WidthValue" + Environment.NewLine;
                sqlStmt += "              ,@LoggedInUserId" + Environment.NewLine;
                sqlStmt += "              ,@LoggedInUserId" + Environment.NewLine;
                #endregion
                #region
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                sqlCommand.Parameters.Add("@ClientId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@DoNotBreakBundle", SqlDbType.Bit);
                sqlCommand.Parameters.Add("@DimensionUnitId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@DiscountPercent", SqlDbType.Float);
                sqlCommand.Parameters.Add("@DiscountPercentOriginal", SqlDbType.Float);
                sqlCommand.Parameters.Add("@HeightValue", SqlDbType.Float);
                sqlCommand.Parameters.Add("@HSNCode", SqlDbType.NVarChar, 64);
                sqlCommand.Parameters.Add("@ItemDiscountAmount", SqlDbType.Float);
                sqlCommand.Parameters.Add("@ItemId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@ItemItemSpecsForDisplay", SqlDbType.NVarChar, 512);
                sqlCommand.Parameters.Add("@ItemMasterDesc0", SqlDbType.NVarChar, 512);
                sqlCommand.Parameters.Add("@ItemMasterDesc1", SqlDbType.NVarChar, 512);
                sqlCommand.Parameters.Add("@ItemMasterDesc2", SqlDbType.NVarChar, 512);
                sqlCommand.Parameters.Add("@ItemMasterDesc3", SqlDbType.NVarChar, 512);
                sqlCommand.Parameters.Add("@ItemRate", SqlDbType.Float);
                sqlCommand.Parameters.Add("@ItemRateBeforeDiscount", SqlDbType.Float);
                sqlCommand.Parameters.Add("@ItemRateOriginal", SqlDbType.Float);
                sqlCommand.Parameters.Add("@LengthValue", SqlDbType.Float);
                sqlCommand.Parameters.Add("@OrderAmount", SqlDbType.Float);
                sqlCommand.Parameters.Add("@OrderAmountBeforeDiscount", SqlDbType.Float);
                sqlCommand.Parameters.Add("@OrderComments", SqlDbType.NVarChar, 250);
                sqlCommand.Parameters.Add("@OrderDetailTypeId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@OrderHeaderSummaryId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@OrderQty", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@ParentItemId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@ProductCode", SqlDbType.NVarChar, 64);
                sqlCommand.Parameters.Add("@ProductOrVolumetricWeight", SqlDbType.Float);
                sqlCommand.Parameters.Add("@ProductOrVolumetricWeightUnitId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@SeqNum", SqlDbType.Float);
                sqlCommand.Parameters.Add("@VolumeValue", SqlDbType.Float);
                sqlCommand.Parameters.Add("@WeightCalcUnitId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@WeightCalcValue", SqlDbType.Float);
                sqlCommand.Parameters.Add("@WeightUnitId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@WeightValue", SqlDbType.Float);
                sqlCommand.Parameters.Add("@WidthValue", SqlDbType.Float);
                sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 256);
                sqlCommand.Parameters.Add("@OrderDetailId", SqlDbType.BigInt);
                sqlCommand.Parameters["@OrderDetailId"].Direction = ParameterDirection.ReturnValue;
                #endregion
                #region
                sqlCommand.Parameters["@ClientId"].Value = clientId;
                sqlCommand.Parameters["@DoNotBreakBundle"].Value = orderDetail.DoNotBreakBundle;
                sqlCommand.Parameters["@DimensionUnitId"].Value = orderDetail.DimensionUnitId;
                sqlCommand.Parameters["@DiscountPercent"].Value = orderDetail.DiscountPercent;
                sqlCommand.Parameters["@DiscountPercentOriginal"].Value = orderDetail.DiscountPercentOriginal;
                sqlCommand.Parameters["@HeightValue"].Value = orderDetail.HeightValue;
                sqlCommand.Parameters["@HSNCode"].Value = string.IsNullOrWhiteSpace(orderDetail.HSNCode) ? (object)DBNull.Value : orderDetail.HSNCode;
                sqlCommand.Parameters["@ItemDiscountAmount"].Value = orderDetail.ItemDiscountAmount;
                sqlCommand.Parameters["@ItemId"].Value = orderDetail.ItemId == null ? 0 : orderDetail.ItemId;
                sqlCommand.Parameters["@ItemItemSpecsForDisplay"].Value = string.IsNullOrWhiteSpace(orderDetail.ItemItemSpecsForDisplay) ? (object)DBNull.Value : orderDetail.ItemItemSpecsForDisplay;
                sqlCommand.Parameters["@ItemMasterDesc0"].Value = string.IsNullOrWhiteSpace(orderDetail.ItemMasterDesc0) ? (object)DBNull.Value : orderDetail.ItemMasterDesc0;
                sqlCommand.Parameters["@ItemMasterDesc1"].Value = string.IsNullOrWhiteSpace(orderDetail.ItemMasterDesc1) ? (object)DBNull.Value : orderDetail.ItemMasterDesc1;
                sqlCommand.Parameters["@ItemMasterDesc2"].Value = string.IsNullOrWhiteSpace(orderDetail.ItemMasterDesc2) ? (object)DBNull.Value : orderDetail.ItemMasterDesc2;
                sqlCommand.Parameters["@ItemMasterDesc3"].Value = string.IsNullOrWhiteSpace(orderDetail.ItemMasterDesc3) ? (object)DBNull.Value : orderDetail.ItemMasterDesc3;
                sqlCommand.Parameters["@ItemRate"].Value = orderDetail.ItemRate;
                sqlCommand.Parameters["@ItemRateBeforeDiscount"].Value = orderDetail.ItemRateBeforeDiscount;
                sqlCommand.Parameters["@ItemRateOriginal"].Value = orderDetail.ItemRateOriginal;
                sqlCommand.Parameters["@LengthValue"].Value = orderDetail.LengthValue;
                sqlCommand.Parameters["@OrderAmount"].Value = orderDetail.OrderAmount;
                sqlCommand.Parameters["@OrderAmountBeforeDiscount"].Value = orderDetail.OrderAmountBeforeDiscount;
                sqlCommand.Parameters["@OrderComments"].Value = string.IsNullOrEmpty(orderDetail.OrderComments) ? "" : orderDetail.OrderComments;
                sqlCommand.Parameters["@OrderDetailTypeId"].Value = orderDetail.OrderDetailTypeId;
                sqlCommand.Parameters["@OrderHeaderSummaryId"].Value = orderDetail.OrderHeaderSummaryId;
                sqlCommand.Parameters["@OrderQty"].Value = orderDetail.OrderQty;
                sqlCommand.Parameters["@ParentItemId"].Value = orderDetail.ParentItemId;
                sqlCommand.Parameters["@ProductCode"].Value = string.IsNullOrWhiteSpace(orderDetail.ProductCode) ? "" : orderDetail.ProductCode;
                sqlCommand.Parameters["@ProductOrVolumetricWeight"].Value = orderDetail.ProductOrVolumetricWeight;
                sqlCommand.Parameters["@ProductOrVolumetricWeightUnitId"].Value = orderDetail.ProductOrVolumetricWeightUnitId;
                sqlCommand.Parameters["@SeqNum"].Value = orderDetail.SeqNum;
                sqlCommand.Parameters["@VolumeValue"].Value = orderDetail.VolumeValue;
                sqlCommand.Parameters["@WeightCalcUnitId"].Value = orderDetail.WeightCalcUnitId;
                sqlCommand.Parameters["@WeightCalcValue"].Value = orderDetail.WeightCalcValue;
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
                sqlStmt += "              ,DoNotBreakBundle" + Environment.NewLine;
                sqlStmt += "              ,ItemId" + Environment.NewLine;
                sqlStmt += "              ,ItemRate" + Environment.NewLine;
                sqlStmt += "              ,OrderHeaderWIPId" + Environment.NewLine;
                sqlStmt += "              ,OrderQty" + Environment.NewLine;
                sqlStmt += "              ,ParentItemId" + Environment.NewLine;
                sqlStmt += "              ,SeqNum" + Environment.NewLine;
                sqlStmt += "              ,AddUserId" + Environment.NewLine;
                sqlStmt += "              ,UpdUserId" + Environment.NewLine;
                sqlStmt += "              )" + Environment.NewLine;
                sqlStmt += "        OUTPUT INSERTED.OrderDetailWIPId" + Environment.NewLine;
                sqlStmt += "        SELECT " + Environment.NewLine;
                sqlStmt += "               @ClientId" + Environment.NewLine;
                sqlStmt += "              ,@DoNotBreakBundle" + Environment.NewLine;
                sqlStmt += "              ,@ItemId" + Environment.NewLine;
                sqlStmt += "              ,@ItemRate" + Environment.NewLine;
                sqlStmt += "              ,@OrderHeaderWIPId" + Environment.NewLine;
                sqlStmt += "              ,@OrderQty" + Environment.NewLine;
                sqlStmt += "              ,@ParentItemId" + Environment.NewLine;
                sqlStmt += "              ,@SeqNum" + Environment.NewLine;
                sqlStmt += "              ,@LoggedInUserId" + Environment.NewLine;
                sqlStmt += "              ,@LoggedInUserId" + Environment.NewLine;
                #endregion
                #region
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                sqlCommand.Parameters.Add("@ClientId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@DoNotBreakBundle", SqlDbType.Bit);
                sqlCommand.Parameters.Add("@ItemId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@ItemRate", SqlDbType.Float);
                sqlCommand.Parameters.Add("@OrderHeaderWIPId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@OrderQty", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@ParentItemId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@SeqNum", SqlDbType.Float);
                sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 256);
                #endregion
                #region
                sqlCommand.Parameters["@ClientId"].Value = orderDetailWIPModel.ClientId;
                sqlCommand.Parameters["@DoNotBreakBundle"].Value = orderDetailWIPModel.DoNotBreakBundle;
                sqlCommand.Parameters["@ItemId"].Value = orderDetailWIPModel.ItemId;
                sqlCommand.Parameters["@ItemRate"].Value = orderDetailWIPModel.ItemRate;
                sqlCommand.Parameters["@OrderHeaderWIPId"].Value = orderDetailWIPModel.OrderHeaderWIPId;
                sqlCommand.Parameters["@OrderQty"].Value = orderDetailWIPModel.OrderQty;
                sqlCommand.Parameters["@ParentItemId"].Value = orderDetailWIPModel.ParentItemId;
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
                sqlCommand.Parameters["@OrderDateTime"].Value = orderHeaderWIPModel.OrderDateTime;//string.IsNullOrWhiteSpace(orderHeaderWIPModel.OrderDateTime) ? (object)DBNull.Value : orderHeaderWIPModel.OrderDateTime;
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
            sqlCommand.Parameters.Add("@ClientId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@PersonId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@CorpAcctId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 512);
            sqlCommand.Parameters["@ClientId"].Value = clientId;
            sqlCommand.Parameters["@PersonId"].Value = personId;
            sqlCommand.Parameters["@CorpAcctId"].Value = corpAcctId;
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
            sqlCommand.ExecuteNonQuery();
        }
        public static void ReferralListAdd(ReferralListModel referralListModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                #region
                string sqlStmt = "";
                sqlStmt += "         INSERT RetailSlnSch.ReferralList" + Environment.NewLine;
                sqlStmt += "               (" + Environment.NewLine;
                sqlStmt += "                ClientId" + Environment.NewLine;
                sqlStmt += "               ,CommissionPercent" + Environment.NewLine;
                sqlStmt += "               ,CouponListId" + Environment.NewLine;
                sqlStmt += "               ,DiscountPercent" + Environment.NewLine;
                sqlStmt += "               ,PersonId" + Environment.NewLine;
                sqlStmt += "               ,AddUserId" + Environment.NewLine;
                sqlStmt += "               ,UpdUserId" + Environment.NewLine;
                sqlStmt += "               )" + Environment.NewLine;
                sqlStmt += "         OUTPUT INSERTED.ReferralListId" + Environment.NewLine;
                sqlStmt += "         SELECT " + Environment.NewLine;
                sqlStmt += "                @ClientId" + Environment.NewLine;
                sqlStmt += "               ,@CommissionPercent" + Environment.NewLine;
                sqlStmt += "               ,@CouponListId" + Environment.NewLine;
                sqlStmt += "               ,@DiscountPercent" + Environment.NewLine;
                sqlStmt += "               ,@PersonId" + Environment.NewLine;
                sqlStmt += "               ,@LoggedInUserId" + Environment.NewLine;
                sqlStmt += "               ,@LoggedInUserId" + Environment.NewLine;
                #endregion
                #region
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                sqlCommand.Parameters.Add("@ClientId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@CommissionPercent", SqlDbType.Float);
                sqlCommand.Parameters.Add("@CouponListId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@DiscountPercent", SqlDbType.Float);
                sqlCommand.Parameters.Add("@PersonId", SqlDbType.BigInt);
                sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 256);
                #endregion
                #region
                sqlCommand.Parameters["@ClientId"].Value = clientId;
                sqlCommand.Parameters["@CommissionPercent"].Value = referralListModel.CommissionPercent;
                sqlCommand.Parameters["@CouponListId"].Value = referralListModel.CouponListId;
                sqlCommand.Parameters["@DiscountPercent"].Value = referralListModel.DiscountPercent;
                sqlCommand.Parameters["@PersonId"].Value = referralListModel.PersonId;
                sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
                #endregion
                referralListModel.ReferralListId = (long)sqlCommand.ExecuteScalar();
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
