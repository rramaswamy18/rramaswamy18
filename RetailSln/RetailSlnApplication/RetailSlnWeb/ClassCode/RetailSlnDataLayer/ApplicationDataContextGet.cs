using ArchitectureLibraryEnumerations;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using RetailSlnEnumerations;
using RetailSlnModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Reflection;

namespace RetailSlnDataLayer
{
    public static partial class ApplicationDataContext
    {
        public static CategoryModel GetCategory(long categoryId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                sqlStmt += "SELECT * FROM RetailSlnSch.Category WHERE CategoryId = " + categoryId + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                CategoryModel categoryModel;
                if (sqlDataReader.Read())
                {
                    categoryModel = new CategoryModel
                    {
                        CategoryId = long.Parse(sqlDataReader["CategoryId"].ToString()),
                        CategoryDesc = sqlDataReader["CategoryDesc"].ToString(),
                        CategoryName = sqlDataReader["CategoryName"].ToString(),
                        CategoryNameDesc = sqlDataReader["CategoryNameDesc"].ToString(),
                        CategoryStatusId = (CategoryStatusEnum)int.Parse(sqlDataReader["CategoryStatusId"].ToString()),
                        CategoryTypeId = (CategoryTypeEnum)int.Parse(sqlDataReader["CategoryTypeId"].ToString()),
                        ImageName = sqlDataReader["ImageName"].ToString(),
                        MaxPerPage = short.Parse(sqlDataReader["MaxPerPage"].ToString()),
                        UploadImageFileName = sqlDataReader["UploadImageFileName"].ToString(),
                        ViewName = sqlDataReader["ViewName"].ToString(),
                    };
                }
                else
                {
                    categoryModel = null;
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return categoryModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static float GetCorpAcctLocationMaxSeqNum(long corpAcctId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                sqlStmt += "SELECT MAX(SeqNum) AS MaxSeqNum FROM RetailSlnSch.CorpAcctLocation WHERE CorpAcctId = " + corpAcctId + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                float seqNum;
                if (sqlDataReader.Read())
                {
                    if (sqlDataReader.IsDBNull(0))
                    {
                        seqNum = 0;
                    }
                    else
                    {
                        seqNum = float.Parse(sqlDataReader["MaxSeqNum"].ToString());
                    }
                }
                else
                {
                    seqNum = 0;
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return seqNum;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static PersonExtn1Model GetPersonExtn1FromPersonId(long personId, long corpAcctLocationId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                //sqlStmt += "SELECT * FROM RetailSlnSch.PersonExtn1 INNER JOIN RetailSlnSch.CorpAcct ON PersonExtn1.CorpAcctId = CorpAcct.CorpAcctId WHERE PersonExtn1.PersonId = " + personId + Environment.NewLine;
                if (corpAcctLocationId > -1)
                {
                    sqlStmt += "SELECT * FROM RetailSlnSch.PersonExtn1 WHERE PersonExtn1.PersonId = " + personId + " AND PersonExtn1.CorpAcctLocationId = " + corpAcctLocationId + Environment.NewLine;
                }
                else
                {
                    sqlStmt += "SELECT TOP 1 * FROM RetailSlnSch.PersonExtn1 WHERE PersonExtn1.PersonId = " + personId + " ORDER BY PersonExtn1.CorpAcctLocationId" + Environment.NewLine;
                }
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                PersonExtn1Model personExtn1Model;
                if (sqlDataReader.Read())
                {
                    personExtn1Model = new PersonExtn1Model
                    {
                        PersonExtn1Id = long.Parse(sqlDataReader["PersonExtn1Id"].ToString()),
                        ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                        PersonId = long.Parse(sqlDataReader["PersonId"].ToString()),
                        CorpAcctId = long.Parse(sqlDataReader["CorpAcctId"].ToString()),
                        CorpAcctLocationId = long.Parse(sqlDataReader["CorpAcctLocationId"].ToString()),
                        //CorpAcctModel = new CorpAcctModel
                        //{
                        //    CorpAcctId = long.Parse(sqlDataReader["CorpAcctId"].ToString()),
                        //    ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                        //    CorpAcctName = sqlDataReader["CorpAcctName"].ToString(),
                        //    CorpAcctTypeId = (CorpAcctTypeEnum)int.Parse(sqlDataReader["CorpAcctTypeId"].ToString()),
                        //    CreditDays = short.Parse(sqlDataReader["CreditDays"].ToString()),
                        //    CreditLimit = float.Parse(sqlDataReader["CreditLimit"].ToString()),
                        //    CreditSale = bool.Parse(sqlDataReader["CreditSale"].ToString()),
                        //    MinOrderAmount = float.Parse(sqlDataReader["MinOrderAmount"].ToString()),
                        //    ShippingAndHandlingCharges = bool.Parse(sqlDataReader["ShippingAndHandlingCharges"].ToString()),
                        //    TaxIdentNum = sqlDataReader["TaxIdentNum"].ToString(),
                        //    CorpAcctLocationModels = new List<CorpAcctLocationModel>(),
                        //    DiscountDtlModels = new List<DiscountDtlModel>(),
                        //}
                    };
                }
                else
                {
                    personExtn1Model = null;
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return personExtn1Model;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static ItemModel GetItem(long itemId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ItemModel itemModel;
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM RetailSlnSch.Item WHERE ItemId = " + itemId, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    itemModel = new ItemModel
                    {
                        ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
                        ExpectedAvailability = string.IsNullOrWhiteSpace(sqlDataReader["ExpectedAvailability"].ToString()) ? null : DateTime.Parse(sqlDataReader["ExpectedAvailability"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
                        ItemName = sqlDataReader["ItemName"].ToString(),
                        ItemForSaleId = (YesNoEnum)int.Parse(sqlDataReader["ItemForSaleId"].ToString()),
                        ItemMasterId = long.Parse(sqlDataReader["ItemMasterId"].ToString()),
                        ImageName = sqlDataReader["ImageName"].ToString(),
                        ItemShortDesc = sqlDataReader["ItemShortDesc"].ToString(),
                        ItemStatusId = (ItemStatusEnum)int.Parse(sqlDataReader["ItemStatusId"].ToString()),
                        ItemTypeId = (ItemTypeEnum)int.Parse(sqlDataReader["ItemTypeId"].ToString()),
                        ProductItemId = long.Parse(sqlDataReader["ProductItemId"].ToString()),
                        UploadImageFileName = sqlDataReader["UploadImageFileName"].ToString(),
                    };
                }
                else
                {
                    itemModel = null;
                }
                sqlDataReader.Close();
                return itemModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static long? GetMaxOrderHeaderWIPId(long personId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            long? orderHeaderWIPId;
            SqlDataReader sqlDataReader = null;
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT MAX(OrderHeaderWIPId) FROM RetailSlnSch.OrderHeaderWIP WHERE PersonId = " + personId, sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    orderHeaderWIPId = long.Parse(sqlDataReader[0].ToString());
                }
                else
                {
                    orderHeaderWIPId = null;
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                orderHeaderWIPId = null;
            }
            finally
            {
                sqlDataReader.Close();
            }
            return orderHeaderWIPId;
        }
        public static OrderHeaderWIPModel GetOrderHeaderWIP(long orderHeaderWIPId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            OrderHeaderWIPModel orderHeaderWIPModel = null;
            try
            {
                #region
                string sqlStmt = "";
                sqlStmt += "        SELECT *" + Environment.NewLine;
                sqlStmt += "          FROM RetailSlnSch.OrderHeaderWIP" + Environment.NewLine;
                sqlStmt += "    INNER JOIN RetailSlnSch.OrderDetailWIP" + Environment.NewLine;
                sqlStmt += "            ON OrderHeaderWIP.OrderHeaderWIPId = OrderDetailWIP.OrderHeaderWIPId" + Environment.NewLine;
                sqlStmt += "         WHERE OrderHeaderWIP.OrderHeaderWIPId = " + orderHeaderWIPId + Environment.NewLine;
                sqlStmt += "      ORDER BY OrderDetailWIP.SeqNum" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                #endregion
                #region
                bool sqlDataReaderRead = sqlDataReader.Read();
                while (sqlDataReaderRead)
                {
                    orderHeaderWIPModel = new OrderHeaderWIPModel
                    {
                        OrderHeaderWIPId = long.Parse(sqlDataReader["OrderHeaderWIPId"].ToString()),
                        ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                        CorpAcctLocationId = long.Parse(sqlDataReader["CorpAcctLocationId"].ToString()),
                        CreatedForPersonId = long.Parse(sqlDataReader["CreatedForPersonId"].ToString()),
                        OrderDateTime = sqlDataReader["CreatedForPersonId"].ToString(),
                        OrderStatusId = sqlDataReader["OrderStatusId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["OrderStatusId"].ToString()),
                        PersonId = long.Parse(sqlDataReader["PersonId"].ToString()),
                        OrderDetailWIPModels = new List<OrderDetailWIPModel>(),
                    };
                    while (sqlDataReaderRead)
                    {
                        orderHeaderWIPModel.OrderDetailWIPModels.Add
                        (
                            new OrderDetailWIPModel
                            {
                                ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                                ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
                                OrderHeaderWIPId = long.Parse(sqlDataReader["OrderHeaderWIPId"].ToString()),
                                OrderQty = long.Parse(sqlDataReader["OrderQty"].ToString()),
                                SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                            }
                        );
                        sqlDataReaderRead = sqlDataReader.Read();
                    }
                }
                sqlDataReader.Close();
                #endregion
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
            }
            return orderHeaderWIPModel;
        }
        public static GiftCertModel GetGiftCert(string giftCertNumber, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            GiftCertModel giftCertModel;
            SqlCommand sqlCommand = BuildSqlCommandGiftCertSelect(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
            sqlCommand.Parameters["@GiftCertNumber"].Value = giftCertNumber;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if(sqlDataReader.Read())
            {
                giftCertModel = AssignGiftCert(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId);
            }
            else
            {
                giftCertModel = null;
            }
            sqlDataReader.Close();
            return giftCertModel;
        }
        public static ItemInfoModel GetItemInfo(long itemInfoId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM RetailSlnSch.ItemInfo WHERE ItemInfoId = " + itemInfoId, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                ItemInfoModel itemInfoModel;
                if (sqlDataReader.Read())
                {
                    itemInfoModel = AssignItemInfoSelect(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId);
                }
                else
                {
                    itemInfoModel = null;
                }
                sqlDataReader.Close();
                return itemInfoModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static float GetItemInfoMaxSeqNum(long itemId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT MAX(SeqNum) FROM RetailSlnSch.ItemInfo WHERE ItemId = " + itemId, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                float seqNum;
                if (sqlDataReader.Read())
                {
                    seqNum = float.Parse(sqlDataReader[0].ToString());
                }
                else
                {
                    seqNum = 1;
                }
                sqlDataReader.Close();
                return seqNum;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static OrderApprovalModel GetOrderApproval(long orderApprovalId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                #region
                string sqlStmt = "";
                sqlStmt += "        SELECT OrderApproval.*" + Environment.NewLine;
                sqlStmt += "              ,AspNetUserRequestedBy.Email AS ApprovalRequestedByEmailAddress" + Environment.NewLine;
                sqlStmt += "              ,PersonRequestedBy.FirstName AS ApprovalRequestedByFirstName" + Environment.NewLine;
                sqlStmt += "              ,PersonRequestedBy.LastName AS ApprovalRequestedByLastName" + Environment.NewLine;
                sqlStmt += "              ,AspNetUserRequestedBy.PhoneNumber AS ApprovalRequestedByTelephoneNumber" + Environment.NewLine;
                sqlStmt += "              ,AspNetUserRequestedFor.Email AS ApprovalRequestedForEmailAddress" + Environment.NewLine;
                sqlStmt += "              ,PersonRequestedFor.FirstName AS ApprovalRequestedForFirstName" + Environment.NewLine;
                sqlStmt += "              ,PersonRequestedFor.LastName AS ApprovalRequestedForLastName" + Environment.NewLine;
                sqlStmt += "              ,AspNetUserRequestedFor.PhoneNumber AS ApprovalRequestedForTelephoneNumber" + Environment.NewLine;
                sqlStmt += "          FROM RetailSlnSch.OrderApproval" + Environment.NewLine;
                sqlStmt += "    INNER JOIN ArchLib.Person AS PersonRequestedBy" + Environment.NewLine;
                sqlStmt += "            ON OrderApproval.ApprovalRequestedByPersonId = PersonRequestedBy.PersonId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN ArchLib.AspNetUser AS AspNetUserRequestedBy" + Environment.NewLine;
                sqlStmt += "            ON PersonRequestedBy.AspNetUserId = AspNetUserRequestedBy.AspNetUserId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN ArchLib.Person AS PersonRequestedFor" + Environment.NewLine;
                sqlStmt += "            ON OrderApproval.ApprovalRequestedForPersonId = PersonRequestedFor.PersonId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN ArchLib.AspNetUser AS AspNetUserRequestedFor" + Environment.NewLine;
                sqlStmt += "            ON PersonRequestedFor.AspNetUserId = AspNetUserRequestedFor.AspNetUserId" + Environment.NewLine;
                sqlStmt += "         WHERE OrderApproval.OrderApprovalId = " + orderApprovalId + Environment.NewLine;
                //sqlStmt += "        " + Environment.NewLine;
                #endregion
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                OrderApprovalModel orderApprovalModel;
                if (sqlDataReader.Read())
                {
                    orderApprovalModel = new OrderApprovalModel
                    {
                        OrderApprovalId = long.Parse(sqlDataReader["OrderApprovalId"].ToString()),
                        ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                        ApprovalOTPCode = sqlDataReader["ApprovalOTPCode"].ToString(),
                        ApprovalOTPCompletedDateTime = sqlDataReader["ApprovalOTPCompletedDateTime"].ToString(),
                        ApprovalOTPCreatedDateTime = sqlDataReader["ApprovalOTPCreatedDateTime"].ToString(),
                        ApprovalOTPExpiryDateTime = sqlDataReader["ApprovalOTPExpiryDateTime"].ToString(),
                        ApprovalOTPExpiryDuration = int.Parse(sqlDataReader["ApprovalOTPExpiryDuration"].ToString()),
                        ApprovalOTPSendTypeId = (OTPSendTypeEnum)long.Parse(sqlDataReader["ApprovalOTPSendTypeId"].ToString()),
                        ApprovalRequestedByPersonId = long.Parse(sqlDataReader["ApprovalRequestedByPersonId"].ToString()),
                        ApprovalRequestedDateTime = sqlDataReader["ApprovalRequestedDateTime"].ToString(),
                        ApprovalRequestedForPersonId = long.Parse(sqlDataReader["ApprovalRequestedForPersonId"].ToString()),
                        ApprovalStatusNameDesc = sqlDataReader["ApprovalStatusNameDesc"].ToString(),
                        ApprovedByPersonId = sqlDataReader["ApprovedByPersonId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["ApprovedByPersonId"].ToString()),
                        ApprovedDateTime = sqlDataReader["ApprovedDateTime"].ToString(),
                        ApproverComments = sqlDataReader["ApproverComments"].ToString(),
                        ApproverConsent = bool.Parse(sqlDataReader["ApproverConsent"].ToString()),
                        ApproverInitialsTextId = sqlDataReader["ApproverInitialsTextId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["ApproverInitialsTextId"].ToString()),
                        ApproverInitialsTextValue = sqlDataReader["ApproverInitialsTextValue"].ToString(),
                        ApproverSignatureTextId = sqlDataReader["ApproverSignatureTextId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["ApproverSignatureTextId"].ToString()),
                        ApproverSignatureTextValue = sqlDataReader["ApproverSignatureTextValue"].ToString(),
                        Comments = sqlDataReader["Comments"].ToString(),
                        OrderHeaderId = long.Parse(sqlDataReader["OrderHeaderId"].ToString()),
                        ApprovalRequestedByEmailAddress = sqlDataReader["ApprovalRequestedByEmailAddress"].ToString(),
                        ApprovalRequestedByFirstName = sqlDataReader["ApprovalRequestedByFirstName"].ToString(),
                        ApprovalRequestedByLastName = sqlDataReader["ApprovalRequestedByLastName"].ToString(),
                        ApprovalRequestedByTelephoneNumber = sqlDataReader["ApprovalRequestedByTelephoneNumber"].ToString(),
                        ApprovalRequestedForEmailAddress = sqlDataReader["ApprovalRequestedForEmailAddress"].ToString(),
                        ApprovalRequestedForFirstName = sqlDataReader["ApprovalRequestedForFirstName"].ToString(),
                        ApprovalRequestedForLastName = sqlDataReader["ApprovalRequestedForLastName"].ToString(),
                        ApprovalRequestedForTelephoneNumber = sqlDataReader["ApprovalRequestedForTelephoneNumber"].ToString(),
                    };
                }
                else
                {
                    orderApprovalModel = new OrderApprovalModel();
                }
                return orderApprovalModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static int GetOrderHeaderCount(long personId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM RetailSlnSch.OrderHeader WHERE PersonId = " + personId, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                int orderHeaderCount;
                if (sqlDataReader.Read())
                {
                    orderHeaderCount = int.Parse(sqlDataReader[0].ToString());
                }
                else
                {
                    orderHeaderCount = 0;
                }
                sqlDataReader.Close();
                return orderHeaderCount;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static int GetTotalBalanceDue(long corpAcctId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                sqlStmt += "" + Environment.NewLine;
                sqlStmt += "        SELECT PersonExtn1.CorpAcctId" + Environment.NewLine;
                sqlStmt += "              ,OrderDetail.OrderDetailTypeId" + Environment.NewLine;
                sqlStmt += "              ,SUM(OrderDetail.OrderAmount) AS OrderAmount" + Environment.NewLine;
                sqlStmt += "          FROM RetailSlnSch.PersonExtn1" + Environment.NewLine;
                sqlStmt += "    INNER JOIN RetailSlnSch.OrderHeader" + Environment.NewLine;
                sqlStmt += "            ON PersonExtn1.PersonId = OrderHeader.PersonId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN RetailSlnSch.OrderDetail" + Environment.NewLine;
                sqlStmt += "            ON OrderHeader.OrderHeaderId = OrderDetail.OrderHeaderId" + Environment.NewLine;
                sqlStmt += "         WHERE PersonExtn1.CorpAcctId = @CorpAcctId" + Environment.NewLine;
                sqlStmt += "           AND OrderDetail.OrderDetailTypeId IN(1100, 1200)" + Environment.NewLine;
                sqlStmt += "      GROUP BY PersonExtn1.CorpAcctId" + Environment.NewLine;
                sqlStmt += "              ,OrderDetail.OrderDetailTypeId" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                sqlCommand.Parameters.Add("@CorpAcctId", SqlDbType.BigInt);
                sqlCommand.Parameters["@CorpAcctId"].Value = corpAcctId;
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                int orderHeaderCount;
                if (sqlDataReader.Read())
                {
                    orderHeaderCount = int.Parse(sqlDataReader[0].ToString());
                }
                else
                {
                    orderHeaderCount = 0;
                }
                sqlDataReader.Close();
                return orderHeaderCount;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static void GetPersonInfoFromEmailAddress(string emailAddress, out PersonModel personModel, out CorpAcctModel corpAcctModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            personModel = null;
            corpAcctModel = null;
            #region
            string sqlStmt = "";
            sqlStmt += "        SELECT Person.*" + Environment.NewLine;
            sqlStmt += "              ,CorpAcct.*" + Environment.NewLine;
            sqlStmt += "          FROM ArchLib.AspNetUser" + Environment.NewLine;
            sqlStmt += "    INNER JOIN ArchLib.Person" + Environment.NewLine;
            sqlStmt += "            ON AspNetUser.AspNetUserId = Person.AspNetUserId" + Environment.NewLine;
            sqlStmt += "    INNER JOIN RetailSlnSch.PersonExtn1" + Environment.NewLine;
            sqlStmt += "            ON Person.PersonId = PersonExtn1.PersonId" + Environment.NewLine;
            sqlStmt += "    INNER JOIN RetailSlnSch.CorpAcct" + Environment.NewLine;
            sqlStmt += "            ON PersonExtn1.CorpAcctId = CorpAcct.CorpAcctId" + Environment.NewLine;
            sqlStmt += "         WHERE AspNetUser.UserName = '" + emailAddress + "'" + Environment.NewLine;
            #endregion
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.Read())
            {
                personModel = new PersonModel
                {
                    PersonId = long.Parse(sqlDataReader["PersonId"].ToString()),
                    FirstName = sqlDataReader["FirstName"].ToString(),
                    LastName = sqlDataReader["LastName"].ToString(),
                };
                corpAcctModel = new CorpAcctModel
                {
                    CorpAcctId = long.Parse(sqlDataReader["CorpAcctId"].ToString()),
                    ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                    CorpAcctName = sqlDataReader["CorpAcctName"].ToString(),
                    CorpAcctTypeId = (CorpAcctTypeEnum)int.Parse(sqlDataReader["CorpAcctTypeId"].ToString()),
                    CreditDays = short.Parse(sqlDataReader["CreditDays"].ToString()),
                    CreditLimit = float.Parse(sqlDataReader["CreditLimit"].ToString()),
                    CreditSale = (YesNoEnum)long.Parse(sqlDataReader["CreditSale"].ToString()),
                    MinOrderAmount = float.Parse(sqlDataReader["MinOrderAmount"].ToString()),
                    OrderApprovalRequired = (YesNoEnum)long.Parse(sqlDataReader["OrderApprovalRequired"].ToString()),
                    ShippingAndHandlingCharges = (YesNoEnum)long.Parse(sqlDataReader["ShippingAndHandlingCharges"].ToString()),
                    TaxIdentNum = sqlDataReader["CorpAcctName"].ToString(),
                };
            }
            sqlDataReader.Close();
        }
        public static int GetPersonCountForCorpAcctId(long corpAcctId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(DISTINCT PersonId) FROM RetailSlnSch.PersonExtn1 WHERE CorpAcctId = " + corpAcctId, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                int personCount;
                if (sqlDataReader.Read())
                {
                    personCount = int.Parse(sqlDataReader[0].ToString());
                }
                else
                {
                    personCount = 0;
                }
                sqlDataReader.Close();
                return personCount;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
    }
}
