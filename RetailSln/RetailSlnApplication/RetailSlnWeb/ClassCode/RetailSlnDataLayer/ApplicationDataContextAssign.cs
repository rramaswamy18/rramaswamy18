using RetailSlnModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RetailSlnDataLayer
{
    public static partial class ApplicationDataContext
    {
        public static void AssignOrderHeader(OrderHeader orderHeader, SqlCommand sqlCommand, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            sqlCommand.Parameters["@ClientId"].Value = clientId;
            sqlCommand.Parameters["@EmailAddress"].Value = orderHeader.EmailAddress;
            sqlCommand.Parameters["@OrderCreatedByPersonId"].Value = (int)orderHeader.OrderCreatedByPersonId;
            sqlCommand.Parameters["@OrderDateTime"].Value = orderHeader.OrderDateTime;
            sqlCommand.Parameters["@OrderStatusId"].Value = orderHeader.OrderStatusId;
            sqlCommand.Parameters["@PersonId"].Value = orderHeader.PersonId;
            sqlCommand.Parameters["@TelephoneCountryId"].Value = orderHeader.TelephoneCountryId;
            sqlCommand.Parameters["@TelephoneNumber"].Value = orderHeader.TelephoneNumber;
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
        }
        public static void AssignOrderDetail(OrderDetail orderDetail, SqlCommand sqlCommand, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            sqlCommand.Parameters["@ClientId"].Value = clientId;
            sqlCommand.Parameters["@DimensionUnitId"].Value = orderDetail.DimensionUnitId;
            sqlCommand.Parameters["@ItemDesc"].Value = string.IsNullOrEmpty(orderDetail.ItemDesc) ? (object)DBNull.Value : orderDetail.ItemDesc;
            sqlCommand.Parameters["@ItemId"].Value = orderDetail.ItemId == null ? (object)DBNull.Value : orderDetail.ItemId;
            sqlCommand.Parameters["@ItemRate"].Value = orderDetail.ItemRate;
            sqlCommand.Parameters["@ItemShortDesc"].Value = orderDetail.ItemShortDesc;
            sqlCommand.Parameters["@LengthValue"].Value = orderDetail.LengthValue;
            sqlCommand.Parameters["@OrderAmount"].Value = orderDetail.OrderAmount;
            sqlCommand.Parameters["@OrderComments"].Value = string.IsNullOrEmpty(orderDetail.OrderComments) ? (object)DBNull.Value : orderDetail.OrderComments;
            sqlCommand.Parameters["@OrderDetailTypeId"].Value = (int)orderDetail.OrderDetailTypeId;
            sqlCommand.Parameters["@OrderHeaderId"].Value = orderDetail.OrderHeaderId;
            sqlCommand.Parameters["@OrderQty"].Value = orderDetail.OrderQty;
            sqlCommand.Parameters["@SeqNum"].Value = orderDetail.SeqNum;
            sqlCommand.Parameters["@VolumeValue"].Value = orderDetail.VolumeValue;
            sqlCommand.Parameters["@WeightUnitId"].Value = orderDetail.WeightUnitId;
            sqlCommand.Parameters["@WeightValue"].Value = orderDetail.WeightValue;
            sqlCommand.Parameters["@WidthValue"].Value = orderDetail.WidthValue;
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
        }
        public static void AssignDeliveryInfo(DeliveryInfoDataModel deliveryInfoDataModel, SqlCommand sqlCommand, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            sqlCommand.Parameters["@ClientId"].Value = clientId;
            sqlCommand.Parameters["@AlternateTelephoneDemogInfoCountryId"].Value = deliveryInfoDataModel.AlternateTelephoneDemogInfoCountryId;
            sqlCommand.Parameters["@AlternateTelephoneNum"].Value = string.IsNullOrWhiteSpace(deliveryInfoDataModel.AlternateTelephoneNum) ? (object)DBNull.Value : deliveryInfoDataModel.AlternateTelephoneNum;
            sqlCommand.Parameters["@DeliveryAddressId"].Value = deliveryInfoDataModel.DeliveryAddressModel.DemogInfoAddressId;
            sqlCommand.Parameters["@DeliveryInstructions"].Value = string.IsNullOrWhiteSpace(deliveryInfoDataModel.DeliveryInstructions) ? (object)DBNull.Value : deliveryInfoDataModel.DeliveryInstructions;
            sqlCommand.Parameters["@OrderHeaderId"].Value = (int)deliveryInfoDataModel.OrderHeaderId;
            sqlCommand.Parameters["@PrimaryTelephoneDemogInfoCountryId"].Value = deliveryInfoDataModel.PrimaryTelephoneDemogInfoCountryId;
            sqlCommand.Parameters["@PrimaryTelephoneNum"].Value = deliveryInfoDataModel.PrimaryTelephoneNum;
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
        }
        public static void AssignOrderPayment(PaymentInfoModel paymentInfoModel, SqlCommand sqlCommand, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            sqlCommand.Parameters["@ClientId"].Value = clientId;
            sqlCommand.Parameters["@CreditCardDataId"].Value = paymentInfoModel.PaymentSummaryDataModel.CreditCardDataId;
            sqlCommand.Parameters["@GiftCertId"].Value = paymentInfoModel.PaymentSummaryDataModel.GiftCertId?? 0;
            sqlCommand.Parameters["@OrderHeaderId"].Value = paymentInfoModel.PaymentSummaryDataModel.OrderHeaderId;
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
        }
        public static void AssignGiftCert(GiftCertModel giftCertModel, SqlCommand sqlCommand, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            sqlCommand.Parameters["@ClientId"].Value = clientId;
            sqlCommand.Parameters["@CreditCardDataId"].Value = giftCertModel.CreditCardDataId;
            sqlCommand.Parameters["@GiftCertAmount"].Value = giftCertModel.GiftCertAmount;
            sqlCommand.Parameters["@GiftCertBalanceAmount"].Value = giftCertModel.GiftCertBalanceAmount;
            sqlCommand.Parameters["@GiftCertMessage"].Value = giftCertModel.GiftCertMessage;
            sqlCommand.Parameters["@GiftCertKey"].Value = giftCertModel.GiftCertKey;
            sqlCommand.Parameters["@GiftCertUsedAmount"].Value = giftCertModel.GiftCertUsedAmount;
            sqlCommand.Parameters["@PersonId"].Value = giftCertModel.PersonId;
            sqlCommand.Parameters["@SenderFullName"].Value = giftCertModel.SenderFullName;
            sqlCommand.Parameters["@RecipientFullName"].Value = giftCertModel.RecipientFullName;
            sqlCommand.Parameters["@RecipientPersonId"].Value = giftCertModel.RecipientPersonId;
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
        }
        public static void AssignItemInsert(ItemModel itemModel, SqlCommand sqlCommand, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            sqlCommand.Parameters["@ClientId"].Value = clientId;
            sqlCommand.Parameters["@ExpectedAvailability"].Value = string.IsNullOrEmpty(itemModel.ExpectedAvailability) ? (object)DBNull.Value : itemModel.ExpectedAvailability;
            sqlCommand.Parameters["@ItemDesc"].Value = itemModel.ItemDesc;
            sqlCommand.Parameters["@ItemShortDesc0"].Value = itemModel.ItemShortDesc0;
            sqlCommand.Parameters["@ItemShortDesc1"].Value = itemModel.ItemShortDesc1;
            sqlCommand.Parameters["@ItemShortDesc2"].Value = itemModel.ItemShortDesc2 ?? (object)DBNull.Value;
            sqlCommand.Parameters["@ItemShortDesc3"].Value = itemModel.ItemShortDesc3 ?? (object)DBNull.Value;
            sqlCommand.Parameters["@ItemShortDesc"].Value = itemModel.ItemShortDesc;
            sqlCommand.Parameters["@ItemStarCount"].Value = (int)itemModel.ItemStarCount;
            sqlCommand.Parameters["@ItemStatusId"].Value = (int)itemModel.ItemStatusId;
            sqlCommand.Parameters["@ItemTypeId"].Value = (int)itemModel.ItemTypeId;
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
        }
        private static void AssignItemUpdate(ItemModel itemModel, SqlCommand sqlCommand, long clientId, string ipAddress, string exceUniqueId, string loggedInUserId)
        {
            sqlCommand.Parameters["@ItemId"].Value = itemModel.ItemId;
            sqlCommand.Parameters["@ExpectedAvailability"].Value = string.IsNullOrEmpty(itemModel.ExpectedAvailability) ? (object)DBNull.Value : itemModel.ExpectedAvailability;
            sqlCommand.Parameters["@ItemDesc"].Value = itemModel.ItemDesc;
            sqlCommand.Parameters["@ItemShortDesc"].Value = itemModel.ItemShortDesc;
            sqlCommand.Parameters["@ItemStarCount"].Value = (int)itemModel.ItemStarCount;
            sqlCommand.Parameters["@ItemStatusId"].Value = (int)itemModel.ItemStatusId;
            sqlCommand.Parameters["@ItemTypeId"].Value = (int)itemModel.ItemTypeId;
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
        }
        public static void AssignItemAttribInsert(ItemAttribModel itemAttribModel, SqlCommand sqlCommand, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            sqlCommand.Parameters["@ClientId"].Value = clientId;
            sqlCommand.Parameters["@ItemAttribMasterId"].Value = itemAttribModel.ItemAttribMasterId;
            sqlCommand.Parameters["@ItemAttribUnitValue"].Value = itemAttribModel.ItemAttribUnitValue;
            sqlCommand.Parameters["@ItemAttribValue"].Value = itemAttribModel.ItemAttribValue;
            sqlCommand.Parameters["@ItemId"].Value = (long)itemAttribModel.ItemId;
            sqlCommand.Parameters["@SeqNum"].Value = (int)itemAttribModel.SeqNum;
            sqlCommand.Parameters["@ShowValue"].Value = (bool)itemAttribModel.ShowValue;
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
        }
        public static void AssignItemSpecInsert(ItemSpecModel itemSpecModel, SqlCommand sqlCommand, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            sqlCommand.Parameters["@ClientId"].Value = clientId;
            sqlCommand.Parameters["@ItemId"].Value = itemSpecModel.ItemId;
            sqlCommand.Parameters["@ItemSpecLabelText"].Value = itemSpecModel.ItemSpecLabelText;
            sqlCommand.Parameters["@ItemSpecText"].Value = itemSpecModel.ItemSpecText;
            sqlCommand.Parameters["@SeqNum"].Value = itemSpecModel.SeqNum;
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
        }
        public static ItemSpecModel AssignItemSpecSelect(SqlDataReader sqlDataReader, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            ItemSpecModel itemSpecModel = new ItemSpecModel
            {
                ItemSpecId = long.Parse(sqlDataReader["ItemSpecId"].ToString()),
                ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
                ItemSpecLabelText = sqlDataReader["ItemSpecLabelText"].ToString(),
                ItemSpecText = sqlDataReader["ItemSpecText"].ToString(),
                SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
            };
            return itemSpecModel;
        }
        public static void AssignItemSpecUpdate(ItemSpecModel itemSpecModel, SqlCommand sqlCommand, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            sqlCommand.Parameters["@ClientId"].Value = clientId;
            sqlCommand.Parameters["@ItemId"].Value = itemSpecModel.ItemId;
            sqlCommand.Parameters["@ItemSpecLabelText"].Value = itemSpecModel.ItemSpecLabelText;
            sqlCommand.Parameters["@ItemSpecText"].Value = itemSpecModel.ItemSpecText;
            sqlCommand.Parameters["@SeqNum"].Value = itemSpecModel.SeqNum;
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
            sqlCommand.Parameters["@ItemSpecId"].Value = itemSpecModel.ItemSpecId;
        }
        public static void AssignCategoryInsert(CategoryModel categoryModel, SqlCommand sqlCommand, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            sqlCommand.Parameters["@ClientId"].Value = clientId;
            sqlCommand.Parameters["@CategoryDesc"].Value = categoryModel.CategoryDesc;
            sqlCommand.Parameters["@CategoryStatusId"].Value = (int)categoryModel.CategoryStatusId;
            sqlCommand.Parameters["@CategoryTypeId"].Value = (int)categoryModel.CategoryTypeId;
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
        }
        private static void AssignCategoryUpdate(CategoryModel categoryModel, SqlCommand sqlCommand, long clientId, string ipAddress, string exceUniqueId, string loggedInUserId)
        {
            sqlCommand.Parameters["@CategoryId"].Value = categoryModel.CategoryId;
            sqlCommand.Parameters["@CategoryDesc"].Value = categoryModel.CategoryDesc;
            sqlCommand.Parameters["@CategoryStatusId"].Value = (int)categoryModel.CategoryStatusId;
            sqlCommand.Parameters["@CategoryTypeId"].Value = (int)categoryModel.CategoryTypeId;
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
        }
        private static GiftCertModel AssignGiftCert(SqlDataReader sqlDataReader, long clientId, string ipAddress, string exceUniqueId, string loggedInUserId)
        {
            GiftCertModel giftCertModel = new GiftCertModel
            {
                GiftCertId = long.Parse(sqlDataReader["GiftCertId"].ToString()),
                ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                CreditCardDataId = long.Parse(sqlDataReader["CreditCardDataId"].ToString()),
                GiftCertAmount = float.Parse(sqlDataReader["GiftCertAmount"].ToString()),
                GiftCertBalanceAmount = float.Parse(sqlDataReader["GiftCertBalanceAmount"].ToString()),
                GiftCertMessage = sqlDataReader["GiftCertMessage"].ToString(),
                GiftCertNumber = long.Parse(sqlDataReader["GiftCertNumber"].ToString()),
                GiftCertKey = sqlDataReader["GiftCertKey"].ToString(),
                GiftCertUsedAmount = float.Parse(sqlDataReader["GiftCertUsedAmount"].ToString()),
                PersonId = long.Parse(sqlDataReader["PersonId"].ToString()),
                SenderFullName = sqlDataReader["SenderFullName"].ToString(),
                RecipientFullName = sqlDataReader["RecipientFullName"].ToString(),
                RecipientPersonId = long.Parse(sqlDataReader["RecipientPersonId"].ToString()),
                AddUserId = sqlDataReader["AddUserId"].ToString(),
                AddUserName = sqlDataReader["AddUserName"].ToString(),
                AddDateTime = sqlDataReader["AddDateTime"].ToString(),
                UpdUserId = sqlDataReader["UpdUserId"].ToString(),
                UpdUserName = sqlDataReader["UpdUserName"].ToString(),
                UpdDateTime = sqlDataReader["UpdDateTime"].ToString(),
            };
            return giftCertModel;
        }
        private static void AssignItemImageNameUpdate(ItemModel itemModel, SqlCommand sqlCommand, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            sqlCommand.Parameters["@ImageName"].Value = itemModel.ImageName;
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
            sqlCommand.Parameters["@ItemId"].Value = itemModel.ItemId;
        }
    }
}
