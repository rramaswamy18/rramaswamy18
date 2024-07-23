using ArchitectureLibraryException;
using ArchitectureLibraryUtility;
using RetailSlnModels;
using System;
using System.Collections.Generic;
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
        public static void AddOrderDetail(OrderDetail orderDetail, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Before calling the BuildSqlCommandAspNetUserRoles()", "AspNetUserId", "");
                SqlCommand sqlCommand = BuildSqlCommandOrderDetailAdd(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                AssignOrderDetail(orderDetail, sqlCommand, clientId, ipAddress, execUniqueId, loggedInUserId);
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
                //orderHeaderModel.OrderNum = "2500000000000000" + orderHeaderModel.OrderHeaderId;
                //UpdOrderHeader(orderHeaderModel, execUniqueId);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00009000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        //public static void AddOrderDetails(long orderHeaderId, List<OrderDetailModel> orderDetailModels, List<OrderDetailModel> orderSummaryModels, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Before calling the BuildSqlCommandAspNetUserRoles()", "AspNetUserId", "");
        //        SqlCommand sqlCommand = BuildSqlCommandOrderDetailAdd(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        float seqNum = 0;
        //        foreach (var orderDetailModel in orderDetailModels)
        //        {
        //            orderDetailModel.OrderHeaderId = orderHeaderId;
        //            orderDetailModel.SeqNum = ++seqNum;
        //            AssignOrderDetail(orderDetailModel, sqlCommand, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            sqlCommand.ExecuteNonQuery();
        //        }
        //        foreach (var orderDetailModel in orderSummaryModels)
        //        {
        //            orderDetailModel.OrderHeaderId = orderHeaderId;
        //            orderDetailModel.SeqNum = ++seqNum;
        //            AssignOrderDetail(orderDetailModel, sqlCommand, clientId, ipAddress, execUniqueId, loggedInUserId);
        //            sqlCommand.ExecuteNonQuery();
        //        }
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00009000 :: Exit");
        //        return;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        public static void AddDeliveryInfo(DeliveryInfoDataModel deliveryInfoDataModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Before calling the BuildSqlCommandAspNetUserRoles()", "AspNetUserId", "");
                SqlCommand sqlCommand = BuildSqlCommandDeliveryInfoAdd(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                AssignDeliveryInfo(deliveryInfoDataModel, sqlCommand, clientId, ipAddress, execUniqueId, loggedInUserId);
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
        public static void AddOrderPayment(PaymentInfoModel paymentInfoModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Before calling the BuildSqlCommandAspNetUserRoles()", "AspNetUserId", "");
                SqlCommand sqlCommand = BuildSqlCommandOrderPaymentAdd(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                AssignOrderPayment(paymentInfoModel, sqlCommand, clientId, ipAddress, execUniqueId, loggedInUserId);
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
            //SqlCommand sqlCommandItemImage = BuildSqlCommandItemImageInsert(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
            //SqlCommand sqlCommandItemImageSrcSet = BuildSqlCommandItemImageSrcSetInsert(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        }
        public static void AddPersonExtn1(long personId, long corpAcctId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            SqlCommand sqlCommand = new SqlCommand("INSERT RetailSlnSch.PersonExtn1(ClientId, PersonId, CorpAcctId, AddUserId, UpdUserId) SELECT @ClientId, @PersonId, @CorpAcctId, @LoggedInUserId, @LoggedInUserId", sqlConnection);
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
