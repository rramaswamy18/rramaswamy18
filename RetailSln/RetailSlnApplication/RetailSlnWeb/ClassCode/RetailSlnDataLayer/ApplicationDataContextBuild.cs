﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RetailSlnDataLayer
{
    public static partial class ApplicationDataContext
    {
        private static SqlCommand BuildSqlCommandOrderHeaderAdd(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "";
            sqlStmt += "        INSERT RetailSlnSch.OrderHeader" + Environment.NewLine;
            sqlStmt += "              (" + Environment.NewLine;
            sqlStmt += "               ClientId" + Environment.NewLine;
            sqlStmt += "              ,EmailAddress" + Environment.NewLine;
            sqlStmt += "              ,OrderCreatedForPersonId" + Environment.NewLine;
            sqlStmt += "              ,OrderDateTime" + Environment.NewLine;
            sqlStmt += "              ,OrderStatusId" + Environment.NewLine;
            sqlStmt += "              ,PersonId" + Environment.NewLine;
            sqlStmt += "              ,TelephoneCountryId" + Environment.NewLine;
            sqlStmt += "              ,TelephoneNumber" + Environment.NewLine;
            sqlStmt += "              ,AddUserId" + Environment.NewLine;
            sqlStmt += "              ,UpdUserId" + Environment.NewLine;
            sqlStmt += "              )" + Environment.NewLine;
            sqlStmt += "        OUTPUT INSERTED.OrderHeaderId" + Environment.NewLine;
            sqlStmt += "        SELECT" + Environment.NewLine;
            sqlStmt += "               @ClientId" + Environment.NewLine;
            sqlStmt += "              ,@EmailAddress" + Environment.NewLine;
            sqlStmt += "              ,@OrderCreatedForPersonId" + Environment.NewLine;
            sqlStmt += "              ,@OrderDateTime" + Environment.NewLine;
            sqlStmt += "              ,@OrderStatusId" + Environment.NewLine;
            sqlStmt += "              ,@PersonId" + Environment.NewLine;
            sqlStmt += "              ,@TelephoneCountryId" + Environment.NewLine;
            sqlStmt += "              ,@TelephoneNumber" + Environment.NewLine;
            sqlStmt += "              ,@LoggedInUserId" + Environment.NewLine;
            sqlStmt += "              ,@LoggedInUserId" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ClientId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 512);
            sqlCommand.Parameters.Add("@OrderCreatedForPersonId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@OrderDateTime", SqlDbType.VarChar, 21);
            sqlCommand.Parameters.Add("@OrderStatusId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@PersonId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@TelephoneCountryId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@TelephoneNumber", SqlDbType.NVarChar, 50);
            sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 256);
            sqlCommand.Parameters.Add("@OrderHeaderId", SqlDbType.BigInt);
            sqlCommand.Parameters["@OrderHeaderId"].Direction = ParameterDirection.ReturnValue;
            return sqlCommand;
        }
        private static SqlCommand BuildSqlCommandOrderDetailAdd(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
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
            sqlStmt += "              ,OrderHeaderId" + Environment.NewLine;
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
            sqlStmt += "              ,@OrderHeaderId" + Environment.NewLine;
            sqlStmt += "              ,@OrderDetailTypeId" + Environment.NewLine;
            sqlStmt += "              ,@OrderQty" + Environment.NewLine;
            sqlStmt += "              ,@SeqNum" + Environment.NewLine;
            sqlStmt += "              ,@VolumeValue" + Environment.NewLine;
            sqlStmt += "              ,@WeightUnitId" + Environment.NewLine;
            sqlStmt += "              ,@WeightValue" + Environment.NewLine;
            sqlStmt += "              ,@WidthValue" + Environment.NewLine;
            sqlStmt += "              ,@LoggedInUserId" + Environment.NewLine;
            sqlStmt += "              ,@LoggedInUserId" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ClientId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@DimensionUnitId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@ItemId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@ItemShortDesc", SqlDbType.NVarChar, 512);
            sqlCommand.Parameters.Add("@ItemRate", SqlDbType.Float);
            sqlCommand.Parameters.Add("@LengthValue", SqlDbType.Float);
            sqlCommand.Parameters.Add("@OrderAmount", SqlDbType.Float);
            sqlCommand.Parameters.Add("@OrderComments", SqlDbType.NVarChar, 250);
            sqlCommand.Parameters.Add("@OrderHeaderId", SqlDbType.BigInt);
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
            return sqlCommand;
        }
        private static SqlCommand BuildSqlCommandOrderDetailItemBundleAdd(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
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
            return sqlCommand;
        }
        private static SqlCommand BuildSqlCommandOrderDeliveryAdd(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
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
            return sqlCommand;
        }
        private static SqlCommand BuildSqlCommandOrderPaymentAdd(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
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
            return sqlCommand;
        }
        private static SqlCommand BuildSqlCommandGiftCertAdd(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "";
            sqlStmt += "        INSERT RetailSlnSch.GiftCert" + Environment.NewLine;
            sqlStmt += "              (" + Environment.NewLine;
            sqlStmt += "               ClientId" + Environment.NewLine;
            sqlStmt += "              ,CreditCardDataId" + Environment.NewLine;
            sqlStmt += "              ,GiftCertAmount" + Environment.NewLine;
            sqlStmt += "              ,GiftCertBalanceAmount" + Environment.NewLine;
            sqlStmt += "              ,GiftCertMessage" + Environment.NewLine;
            //sqlStmt += "               GiftCertNumber" + Environment.NewLine;
            sqlStmt += "              ,GiftCertKey" + Environment.NewLine;
            sqlStmt += "              ,GiftCertUsedAmount" + Environment.NewLine;
            sqlStmt += "              ,PersonId" + Environment.NewLine;
            sqlStmt += "              ,SenderFullName" + Environment.NewLine;
            sqlStmt += "              ,RecipientFullName" + Environment.NewLine;
            sqlStmt += "              ,RecipientPersonId" + Environment.NewLine;
            sqlStmt += "              ,AddUserId" + Environment.NewLine;
            sqlStmt += "              ,UpdUserId" + Environment.NewLine;
            sqlStmt += "              )" + Environment.NewLine;
            sqlStmt += "        OUTPUT INSERTED.GiftCertId" + Environment.NewLine;
            sqlStmt += "        SELECT" + Environment.NewLine;
            sqlStmt += "               @ClientId" + Environment.NewLine;
            sqlStmt += "              ,@CreditCardDataId" + Environment.NewLine;
            sqlStmt += "              ,@GiftCertAmount" + Environment.NewLine;
            sqlStmt += "              ,@GiftCertBalanceAmount" + Environment.NewLine;
            sqlStmt += "              ,@GiftCertMessage" + Environment.NewLine;
            //sqlStmt += "               @GiftCertNumber" + Environment.NewLine;
            sqlStmt += "              ,@GiftCertKey" + Environment.NewLine;
            sqlStmt += "              ,@GiftCertUsedAmount" + Environment.NewLine;
            sqlStmt += "              ,@PersonId" + Environment.NewLine;
            sqlStmt += "              ,@SenderFullName" + Environment.NewLine;
            sqlStmt += "              ,@RecipientFullName" + Environment.NewLine;
            sqlStmt += "              ,@RecipientPersonId" + Environment.NewLine;
            sqlStmt += "              ,@LoggedInUserId" + Environment.NewLine;
            sqlStmt += "              ,@LoggedInUserId" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ClientId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@CreditCardDataId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@GiftCertAmount", SqlDbType.Float);
            sqlCommand.Parameters.Add("@GiftCertBalanceAmount", SqlDbType.Float);
            sqlCommand.Parameters.Add("@GiftCertMessage", SqlDbType.NVarChar, 100);
            //sqlCommand.Parameters.Add("@GiftCertNumber", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@GiftCertKey", SqlDbType.NVarChar, 50);
            sqlCommand.Parameters.Add("@GiftCertUsedAmount", SqlDbType.Float);
            sqlCommand.Parameters.Add("@PersonId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@SenderFullName", SqlDbType.NVarChar, 100);
            sqlCommand.Parameters.Add("@RecipientFullName", SqlDbType.NVarChar, 100);
            sqlCommand.Parameters.Add("@RecipientPersonId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 256);
            return sqlCommand;
        }
        private static SqlCommand BuildSqlCommandItemInsert(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "INSERT RetailSlnSch.Item(ClientId, ExpectedAvailability, ItemRate, ItemShortDesc, ItemStarCount, ItemStatusId, AddUserId, UpdUserId)";
            sqlStmt += "OUTPUT INSERTED.ItemId SELECT @ClientId, @ExpectedAvailability, @ItemRate, @ItemShortDesc, @ItemStarCount, @ItemStatusId, @LoggedInUserId, @LoggedInUserId" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ClientId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@ExpectedAvailability", SqlDbType.NVarChar, 50);
            sqlCommand.Parameters.Add("@ItemRate", SqlDbType.Float);
            sqlCommand.Parameters.Add("@ItemShortDesc0", SqlDbType.NVarChar, 512);
            sqlCommand.Parameters.Add("@ItemShortDesc1", SqlDbType.NVarChar, 512);
            sqlCommand.Parameters.Add("@ItemShortDesc", SqlDbType.NVarChar, 512);
            sqlCommand.Parameters.Add("@ItemStarCount", SqlDbType.Int);
            sqlCommand.Parameters.Add("@ItemStatusId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 250);
            return sqlCommand;

        }
        private static SqlCommand BuildSqlCommandItemUpdate(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "UPDATE RetailSlnSch.Item SET ExpectedAvailability = @ExpectedAvailability, ItemRate = @ItemRate, ItemShortDesc = @ItemShortDesc, ItemStarCount = @ItemStarCount, ItemStatusId = @ItemStatusId, UpdUserId = @LoggedInUserId, UpdUserName = SUSER_NAME(), UpdDateTime = GETDATE() WHERE ItemId = @ItemId";
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ItemId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@ExpectedAvailability", SqlDbType.NVarChar, 50);
            sqlCommand.Parameters.Add("@ItemRate", SqlDbType.Float);
            sqlCommand.Parameters.Add("@ItemShortDesc", SqlDbType.NVarChar, 250);
            sqlCommand.Parameters.Add("@ItemStarCount", SqlDbType.Int);
            sqlCommand.Parameters.Add("@ItemStatusId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 250);
            return sqlCommand;
        }
        private static SqlCommand BuildSqlCommandItemSpecInsert(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "INSERT RetailSlnSch.ItemSpec(ClientId, ItemSpecMasterId, ItemSpecUnitValue, ItemSpecValue, ItemId, SeqNum, AddUserId, UpdUserId)";
            sqlStmt += "OUTPUT INSERTED.ItemSpecId SELECT @ClientId, @ItemSpecMasterId, @ItemSpecUnitValue, @ItemSpecValue, @ItemId, @SeqNum, @LoggedInUserId, @LoggedInUserId" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ClientId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@ItemSpecMasterId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@ItemSpecUnitValue", SqlDbType.NVarChar, 50);
            sqlCommand.Parameters.Add("@ItemSpecValue", SqlDbType.NVarChar, 50);
            sqlCommand.Parameters.Add("@ItemId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@SeqNum", SqlDbType.Float);
            sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 250);
            return sqlCommand;

        }
        private static SqlCommand BuildSqlCommandItemInfoInsert(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "INSERT RetailSlnSch.ItemInfo(ClientId, ItemId, ItemInfoLabelText, ItemInfoText, SeqNum, AddUserId, UpdUserId)";
            sqlStmt += "OUTPUT INSERTED.ItemInfoId SELECT @ClientId, @ItemId, @ItemInfoLabelText, @ItemInfoText, @SeqNum, @LoggedInUserId, @LoggedInUserId" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ClientId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@ItemId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@ItemInfoLabelText", SqlDbType.NVarChar, 50);
            sqlCommand.Parameters.Add("@ItemInfoText", SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@SeqNum", SqlDbType.Float);
            sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 250);
            return sqlCommand;
        }
        private static SqlCommand BuildSqlCommandItemInfoUpdate(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "UPDATE RetailSlnSch.ItemInfo SET ItemId = @ItemId, ItemInfoLabelText = @ItemInfoLabelText, ItemInfoText = @ItemInfoText, SeqNum = @SeqNum, UpdUserId = @LoggedInUserId, UpdUserName = SUSER_NAME(), UpdDateTime = GETDATE() WHERE ItemInfoId = @ItemInfoId";
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ClientId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@ItemId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@ItemInfoLabelText", SqlDbType.NVarChar, 50);
            sqlCommand.Parameters.Add("@ItemInfoText", SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@SeqNum", SqlDbType.Float);
            sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 250);
            sqlCommand.Parameters.Add("@ItemInfoId", SqlDbType.BigInt);
            return sqlCommand;
        }
        private static SqlCommand BuildSqlCommandCategoryInsert(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "INSERT RetailSlnSch.Category(ClientId, CategoryDesc, CategoryStatusId, CategoryTypeId, AddUserId, UpdUserId)";
            sqlStmt += "OUTPUT INSERTED.CategoryId SELECT @ClientId, @CategoryDesc, @CategoryStatusId, @CategoryTypeId, @LoggedInUserId, @LoggedInUserId" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ClientId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@CategoryDesc", SqlDbType.NVarChar, 50);
            sqlCommand.Parameters.Add("@CategoryStatusId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@CategoryTypeId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 250);
            return sqlCommand;
        }
        private static SqlCommand BuildSqlCommandCategoryUpdate(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "UPDATE RetailSlnSch.Category SET CategoryDesc = @CategoryDesc, CategoryStatusId = @CategoryStatusId, CategoryTypeId = @CategoryTypeId, UpdUserId = @LoggedInUserId, UpdUserName = SUSER_NAME(), UpdDateTime = GETDATE() WHERE CategoryId = @CategoryId";
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@CategoryId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@CategoryDesc", SqlDbType.NVarChar, 50);
            sqlCommand.Parameters.Add("@CategoryStatusId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@CategoryTypeId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 250);
            return sqlCommand;
        }
        private static SqlCommand BuildSqlCommandGiftCertSelect(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "";
            sqlStmt += "      SELECT" + Environment.NewLine;
            sqlStmt += "             GiftCert.GiftCertId" + Environment.NewLine;
            sqlStmt += "            ,GiftCert.ClientId" + Environment.NewLine;
            sqlStmt += "            ,GiftCert.CreditCardDataId" + Environment.NewLine;
            sqlStmt += "            ,GiftCert.GiftCertAmount" + Environment.NewLine;
            sqlStmt += "            ,GiftCert.GiftCertBalanceAmount" + Environment.NewLine;
            sqlStmt += "            ,GiftCert.GiftCertMessage" + Environment.NewLine;
            sqlStmt += "            ,GiftCert.GiftCertNumber" + Environment.NewLine;
            sqlStmt += "            ,GiftCert.GiftCertKey" + Environment.NewLine;
            sqlStmt += "            ,GiftCert.GiftCertUsedAmount" + Environment.NewLine;
            sqlStmt += "            ,GiftCert.PersonId" + Environment.NewLine;
            sqlStmt += "            ,GiftCert.SenderFullName" + Environment.NewLine;
            sqlStmt += "            ,GiftCert.RecipientFullName" + Environment.NewLine;
            sqlStmt += "            ,GiftCert.RecipientPersonId" + Environment.NewLine;
            sqlStmt += "            ,GiftCert.AddUserId" + Environment.NewLine;
            sqlStmt += "            ,GiftCert.AddUserName" + Environment.NewLine;
            sqlStmt += "            ,GiftCert.AddDateTime" + Environment.NewLine;
            sqlStmt += "            ,GiftCert.UpdUserId" + Environment.NewLine;
            sqlStmt += "            ,GiftCert.UpdUserName" + Environment.NewLine;
            sqlStmt += "            ,GiftCert.UpdDateTime" + Environment.NewLine;
            sqlStmt += "        FROM RetailSlnSch.GiftCert" + Environment.NewLine;
            sqlStmt += "       WHERE GiftCert.GiftCertNumber = @GiftCertNumber" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@GiftCertNumber", SqlDbType.NVarChar, 20);
            return sqlCommand;
        }
        private static SqlCommand BuildSqlCommandItemImageNameUpdate(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            SqlCommand sqlCommand = new SqlCommand("UPDATE RetailSlnSch.Item SET ImageName = @ImageName, UpdUserId = @LoggedInUserId, UpdDateTime = GETDATE() WHERE ItemId = @ItemId", sqlConnection);
            sqlCommand.Parameters.Add("@ImageName", SqlDbType.NVarChar, 50);
            sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 512);
            sqlCommand.Parameters.Add("@ItemId", SqlDbType.BigInt);
            return sqlCommand;
        }
    }
}
