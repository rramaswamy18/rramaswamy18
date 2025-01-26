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
            sqlCommand.Parameters["@CreatedForPersonId"].Value = (int)orderHeader.CreatedForPersonId;
            sqlCommand.Parameters["@OrderDateTime"].Value = orderHeader.OrderDateTime;
            sqlCommand.Parameters["@OrderStatusId"].Value = orderHeader.OrderStatusId;
            sqlCommand.Parameters["@PersonId"].Value = orderHeader.PersonId;
            sqlCommand.Parameters["@SaveThisAddress"].Value = orderHeader.SaveThisAddress ? 1 : 0;
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
        }
        public static void AssignOrderDetail(OrderDetail orderDetail, SqlCommand sqlCommand, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            sqlCommand.Parameters["@ClientId"].Value = clientId;
            sqlCommand.Parameters["@DimensionUnitId"].Value = orderDetail.DimensionUnitId;
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
        public static void AssignOrderDetailItemBundle(OrderDetailItemBundle orderDetailItemBundle, SqlCommand sqlCommand, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
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
        }
        public static void AssignOrderDelivery(DeliveryDataModel deliveryDataModel, SqlCommand sqlCommand, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
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
        }
        public static void AssignOrderPayment(PaymentData1Model paymentDataModel, SqlCommand sqlCommand, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            sqlCommand.Parameters["@ClientId"].Value = clientId;
            sqlCommand.Parameters["@CouponId"].Value = paymentDataModel.CouponId;
            sqlCommand.Parameters["@CreditCardDataId"].Value = paymentDataModel.CreditCardDataId;
            sqlCommand.Parameters["@GiftCertId"].Value = paymentDataModel.GiftCertId;
            sqlCommand.Parameters["@OrderHeaderId"].Value = paymentDataModel.OrderHeaderId;
            sqlCommand.Parameters["@PaymentModeId"].Value = paymentDataModel.PaymentModeId;
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
            sqlCommand.Parameters["@ItemShortDesc"].Value = itemModel.ItemShortDesc;
            sqlCommand.Parameters["@ItemStarCount"].Value = (int)itemModel.ItemStarCount;
            sqlCommand.Parameters["@ItemStatusId"].Value = (int)itemModel.ItemStatusId;
            sqlCommand.Parameters["@ItemTypeId"].Value = (int)itemModel.ItemTypeId;
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
        }
        public static void AssignItemSpecInsert(ItemSpecModel itemAttribModel, SqlCommand sqlCommand, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            sqlCommand.Parameters["@ClientId"].Value = clientId;
            sqlCommand.Parameters["@ItemSpecMasterId"].Value = itemAttribModel.ItemSpecMasterId;
            sqlCommand.Parameters["@ItemSpecUnitValue"].Value = itemAttribModel.ItemSpecUnitValue;
            sqlCommand.Parameters["@ItemSpecValue"].Value = itemAttribModel.ItemSpecValue;
            sqlCommand.Parameters["@ItemId"].Value = (long)itemAttribModel.ItemId;
            sqlCommand.Parameters["@SeqNum"].Value = (int)itemAttribModel.SeqNum;
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
        }
        public static void AssignItemInfoInsert(ItemInfoModel itemInfoModel, SqlCommand sqlCommand, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            sqlCommand.Parameters["@ClientId"].Value = clientId;
            sqlCommand.Parameters["@ItemId"].Value = itemInfoModel.ItemId;
            sqlCommand.Parameters["@ItemInfoLabelText"].Value = itemInfoModel.ItemInfoLabelText;
            sqlCommand.Parameters["@ItemInfoText"].Value = itemInfoModel.ItemInfoText;
            sqlCommand.Parameters["@SeqNum"].Value = itemInfoModel.SeqNum;
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
        }
        public static ItemInfoModel AssignItemInfoSelect(SqlDataReader sqlDataReader, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            ItemInfoModel itemInfoModel = new ItemInfoModel
            {
                ItemInfoId = long.Parse(sqlDataReader["ItemInfoId"].ToString()),
                ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
                ItemInfoLabelText = sqlDataReader["ItemInfoLabelText"].ToString(),
                ItemInfoText = sqlDataReader["ItemInfoText"].ToString(),
                SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                SeqNumItem = sqlDataReader["SeqNumItem"].ToString() == "" ? (float?)null : float.Parse(sqlDataReader["SeqNumItem"].ToString()),
                SeqNumItemMaster = sqlDataReader["SeqNumItemMaster"].ToString() == "" ? (float?)null : float.Parse(sqlDataReader["SeqNumItemMaster"].ToString()),
            };
            return itemInfoModel;
        }
        public static void AssignItemInfoUpdate(ItemInfoModel itemInfoModel, SqlCommand sqlCommand, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            sqlCommand.Parameters["@ClientId"].Value = clientId;
            sqlCommand.Parameters["@ItemId"].Value = itemInfoModel.ItemId;
            sqlCommand.Parameters["@ItemInfoLabelText"].Value = itemInfoModel.ItemInfoLabelText;
            sqlCommand.Parameters["@ItemInfoText"].Value = itemInfoModel.ItemInfoText;
            sqlCommand.Parameters["@SeqNum"].Value = itemInfoModel.SeqNum;
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
            sqlCommand.Parameters["@ItemInfoId"].Value = itemInfoModel.ItemInfoId;
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
        public static CategoryModel AssignCategory(SqlDataReader sqlDataReader, long clientId, string ipAddress, string exceUniqueId, string loggedInUserId)
        {
            CategoryModel categoryModel = new CategoryModel
            {
                CategoryId = long.Parse(sqlDataReader["CategoryId"].ToString()),
                ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
            };
            return categoryModel;
        }
        public static ItemModel AssignItem(SqlDataReader sqlDataReader, long clientId, string ipAddress, string exceUniqueId, string loggedInUserId)
        {
            ItemModel itemModel = new ItemModel
            {
                ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
                ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                ImageName = sqlDataReader["ImageName"].ToString(),
            };
            return itemModel;
        }
    }
}
