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
using System.Reflection;
using System.Web;

namespace RetailSlnDataLayer
{
    public static partial class ApplicationDataContext
    {
        public static CouponListModel CouponListGet(string couponNumber, string effDate, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SqlDataReader sqlDataReader = null;
            CouponListModel couponListModel;
            try
            {
                //SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM RetailSlnSch.CouponList WHERE CouponNum = '{couponNumber}' AND '{effDate}' BETWEEN BegEffDate AND EndEffDate", sqlConnection);
                SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM RetailSlnSch.CouponList WHERE CouponNum = '{couponNumber}'", sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    couponListModel = new CouponListModel
                    {
                        BegEffDate = sqlDataReader["BegEffDate"].ToString(),
                        CouponListId = long.Parse(sqlDataReader["CouponListId"].ToString()),
                        CouponNum = sqlDataReader["CouponListId"].ToString(),
                        DiscountPercent = float.Parse(sqlDataReader["DiscountPercent"].ToString()),
                        EndEffDate = sqlDataReader["EndEffDate"].ToString(),
                    };
                }
                else
                {
                    couponListModel = null;
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                couponListModel = null;
            }
            finally
            {
                sqlDataReader.Close();
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return couponListModel;
        }
        public static ReferralListModel ReferralListGet(long personId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SqlDataReader sqlDataReader = null;
            ReferralListModel referralListModel;
            try
            {
                #region
                string sqlStmt = "";
                sqlStmt += "    SELECT TOP 1" + Environment.NewLine;
                sqlStmt += "           *" + Environment.NewLine;
                sqlStmt += "      FROM " + Environment.NewLine;
                sqlStmt += "           RetailSlnSch.ReferralList" + Environment.NewLine;
                sqlStmt += "INNER JOIN RetailSlnSch.CouponList" + Environment.NewLine;
                sqlStmt += "        ON ReferralList.CouponListId = CouponList.CouponListId" + Environment.NewLine;
                sqlStmt += $"     WHERE ReferralList.PersonId = {personId}" + Environment.NewLine;
                sqlStmt += "   ORDER BY CouponList.BegEffDate" + Environment.NewLine;
                sqlStmt += "           ,CouponList.CouponListId" + Environment.NewLine;
                //sqlStmt = "           " + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                #endregion
                if (sqlDataReader.Read())
                {
                    referralListModel = new ReferralListModel
                    {
                        ReferralListId = long.Parse(sqlDataReader["ReferralListId"].ToString()),
                        ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                        CommissionPercent = float.Parse(sqlDataReader["CommissionPercent"].ToString()),
                        CouponListId = long.Parse(sqlDataReader["CouponListId"].ToString()),
                        DiscountPercent = float.Parse(sqlDataReader["DiscountPercent"].ToString()),
                        PersonId = long.Parse(sqlDataReader["PersonId"].ToString()),
                        CouponListModel = new CouponListModel
                        {
                            CouponListId = long.Parse(sqlDataReader["CouponListId"].ToString()),
                            ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                            BegEffDate = sqlDataReader["BegEffDate"].ToString(),
                            CouponNum = sqlDataReader["CouponListId"].ToString(),
                            DiscountPercent = float.Parse(sqlDataReader["DiscountPercent"].ToString()),
                            EndEffDate = sqlDataReader["EndEffDate"].ToString(),
                        }
                    };
                }
                else
                {
                    referralListModel = null;
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                referralListModel = null;
            }
            finally
            {
                sqlDataReader.Close();
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return referralListModel;
        }
        public static PersonExtn1Model PersonExtn1FromPersonIdGet(long personId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                sqlStmt += "SELECT * FROM RetailSlnSch.PersonExtn1 INNER JOIN RetailSlnSch.CorpAcct ON PersonExtn1.CorpAcctId = CorpAcct.CorpAcctId WHERE PersonExtn1.PersonId = " + personId + " ORDER BY PersonExtn1.CorpAcctId, PersonExtn1.CorpAcctLocationId" + Environment.NewLine;
                //if (corpAcctLocationId > -1)
                //{
                //    sqlStmt += "SELECT * FROM RetailSlnSch.PersonExtn1 WHERE PersonExtn1.PersonId = " + personId + " AND PersonExtn1.CorpAcctLocationId = " + corpAcctLocationId + Environment.NewLine;
                //}
                //else
                //{
                //    sqlStmt += "SELECT TOP 1 * FROM RetailSlnSch.PersonExtn1 WHERE PersonExtn1.PersonId = " + personId + " ORDER BY PersonExtn1.CorpAcctLocationId" + Environment.NewLine;
                //}
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
        public static ItemDiscountModel ItemDiscountGet(long corpAcctId, long itemId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ItemDiscountModel itemDiscountModel;
            try
            {
                string sqlStmt;
                sqlStmt = "SELECT * FROM RetailSlnSch.ItemDiscount WHERE ClientId = " + clientId + " AND CorpAcctId = " + corpAcctId + " AND ItemId = " + itemId;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    itemDiscountModel = new ItemDiscountModel
                    {
                        ItemDiscountId = long.Parse(sqlDataReader["ItemDiscountId"].ToString()),
                        ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                        CorpAcctId = long.Parse(sqlDataReader["CorpAcctId"].ToString()),
                        ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
                        DiscountPercent = float.Parse(sqlDataReader["DiscountPercent"].ToString()),
                    };
                }
                else
                {
                    itemDiscountModel = new ItemDiscountModel();
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return itemDiscountModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static OrderHeaderSummary OrderView(long orderHeaderSummaryId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SqlDataReader sqlDataReader = null;
            try
            {
                #region
                string sqlStmt = "";
                sqlStmt += $"        SELECT OrderHeader.*, OrderHeaderSummary.*, OrderDelivery.*, OrderPayment.*, OrderDetail.*" + Environment.NewLine;
                sqlStmt += $"              ,CreatedForPerson.PersonId AS CreatedForPersonId, CreatedForPerson.FirstName AS CreatedForFirstName, CreatedForPerson.LastName AS CreatedForLastName" + Environment.NewLine;
                sqlStmt += $"              ,Person.PersonId, Person.FirstName, Person.LastName" + Environment.NewLine;
                sqlStmt += $"          FROM RetailSlnSch.OrderHeader" + Environment.NewLine;
                sqlStmt += $"    INNER JOIN RetailSlnSch.OrderHeaderSummary" + Environment.NewLine;
                sqlStmt += $"            ON OrderHeader.OrderHeaderId = OrderHeaderSummary.OrderHeaderId" + Environment.NewLine;
                sqlStmt += $"    INNER JOIN RetailSlnSch.OrderDelivery" + Environment.NewLine;
                sqlStmt += $"            ON OrderHeader.OrderHeaderId = OrderDelivery.OrderHeaderId" + Environment.NewLine;
                sqlStmt += $"    INNER JOIN RetailSlnSch.OrderPayment" + Environment.NewLine;
                sqlStmt += $"            ON OrderHeader.OrderHeaderId = OrderPayment.OrderHeaderId" + Environment.NewLine;
                sqlStmt += $"    INNER JOIN RetailSlnSch.OrderDetail" + Environment.NewLine;
                sqlStmt += $"            ON OrderHeaderSummary.OrderHeaderSummaryId = OrderDetail.OrderHeaderSummaryId" + Environment.NewLine;
                sqlStmt += $"    INNER JOIN ArchLib.Person AS CreatedForPerson" + Environment.NewLine;
                sqlStmt += $"            ON OrderHeader.PersonId = CreatedForPerson.PersonId" + Environment.NewLine;
                sqlStmt += $"    INNER JOIN ArchLib.Person" + Environment.NewLine;
                sqlStmt += $"            ON OrderHeader.PersonId = Person.PersonId" + Environment.NewLine;
                sqlStmt += $"         WHERE OrderHeaderSummary.OrderHeaderSummaryId = {orderHeaderSummaryId}" + Environment.NewLine;
                sqlStmt += $"      ORDER BY OrderDetail.SeqNum" + Environment.NewLine;
                #endregion
                #region
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                bool sqlDataReaderRead = sqlDataReader.Read();
                #endregion
                #region
                OrderHeaderSummary orderHeaderSummary = new OrderHeaderSummary
                {
                    OrderHeaderSummaryId = long.Parse(sqlDataReader["OrderHeaderSummaryId"].ToString()),
                    AdditionalCharges = float.Parse(sqlDataReader["AdditionalCharges"].ToString()),
                    BalanceDue = float.Parse(sqlDataReader["BalanceDue"].ToString()),
                    InvoiceTypeId = (InvoiceTypeEnum)int.Parse(sqlDataReader["InvoiceTypeId"].ToString()),
                    OrderHeaderId = long.Parse(sqlDataReader["OrderHeaderId"].ToString()),
                    ShippingAndHandlingCharges = float.Parse(sqlDataReader["ShippingAndHandlingCharges"].ToString()),
                    TotalAmountPaid = float.Parse(sqlDataReader["TotalAmountPaid"].ToString()),
                    TotalDiscountAmount = float.Parse(sqlDataReader["TotalDiscountAmount"].ToString()),
                    TotalInvoiceAmount = float.Parse(sqlDataReader["TotalInvoiceAmount"].ToString()),
                    TotalOrderAmount = float.Parse(sqlDataReader["TotalOrderAmount"].ToString()),
                    TotalTaxAmount = float.Parse(sqlDataReader["TotalTaxAmount"].ToString()),
                    OrderDelivery = new OrderDelivery
                    {
                        OrderDeliveryId = long.Parse(sqlDataReader["OrderDeliveryId"].ToString()),
                        TrackingRefNumber = sqlDataReader["TrackingRefNumber"].ToString(),
                    },
                    OrderHeader = new OrderHeader
                    {
                        OrderHeaderId = long.Parse(sqlDataReader["OrderHeaderId"].ToString()),
                        CreatedForPersonId = long.Parse(sqlDataReader["CreatedForPersonId"].ToString()),
                        InvoiceTypeId = (InvoiceTypeEnum)int.Parse(sqlDataReader["InvoiceTypeId"].ToString()),
                        OrderDateTime = sqlDataReader["OrderDateTime"].ToString(),
                        OrderStatusId = (OrderStatusEnum)int.Parse(sqlDataReader["OrderStatusId"].ToString()),
                        PersonId = long.Parse(sqlDataReader["PersonId"].ToString()),
                        CreatedForPersonModel = new PersonModel
                        {
                            PersonId = long.Parse(sqlDataReader["CreatedForPersonId"].ToString()),
                            FirstName = sqlDataReader["CreatedForFirstName"].ToString(),
                            LastName = sqlDataReader["CreatedForLastName"].ToString(),
                        },
                        PersonModel = new PersonModel
                        {
                            PersonId = long.Parse(sqlDataReader["PersonId"].ToString()),
                            FirstName = sqlDataReader["FirstName"].ToString(),
                            LastName = sqlDataReader["LastName"].ToString(),
                        },
                    },
                    OrderPayment = new OrderPayment
                    {
                        OrderPaymentId = long.Parse(sqlDataReader["OrderPaymentId"].ToString()),
                        PaymentModeId = (PaymentModeEnum)int.Parse(sqlDataReader["PaymentModeId"].ToString()),
                        PaymentStatusId = (PaymentStatusEnum)int.Parse(sqlDataReader["PaymentStatusId"].ToString()),
                    },
                    OrderDetails = new List<OrderDetail>(),
                };
                while (sqlDataReaderRead)
                {
                    orderHeaderSummary.OrderDetails.Add
                    (
                        new OrderDetail
                        {
                            OrderDetailId = long.Parse(sqlDataReader["OrderDetailId"].ToString()),
                            OrderDetailTypeId = (OrderDetailTypeEnum)long.Parse(sqlDataReader["OrderDetailTypeId"].ToString()),
                            DiscountPercent = float.Parse(sqlDataReader["DiscountPercent"].ToString()),
                            DiscountPercentOriginal = float.Parse(sqlDataReader["DiscountPercentOriginal"].ToString()),
                            ItemItemSpecsForDisplay = sqlDataReader["ItemItemSpecsForDisplay"].ToString(),
                            ItemMasterDesc0 = sqlDataReader["ItemMasterDesc0"].ToString(),
                            ItemMasterDesc1 = sqlDataReader["ItemMasterDesc1"].ToString(),
                            ItemMasterDesc2 = sqlDataReader["ItemMasterDesc2"].ToString(),
                            ItemMasterDesc3 = sqlDataReader["ItemMasterDesc3"].ToString(),
                            ItemRateBeforeDiscount = float.Parse(sqlDataReader["ItemRateBeforeDiscount"].ToString()),
                            ItemRateOriginal = float.Parse(sqlDataReader["ItemRateOriginal"].ToString()),
                            ItemRate = float.Parse(sqlDataReader["ItemRateOriginal"].ToString()),
                            OrderAmount = float.Parse(sqlDataReader["OrderAmount"].ToString()),
                            OrderQty = long.Parse(sqlDataReader["OrderQty"].ToString()),
                        }
                    );
                    sqlDataReaderRead = sqlDataReader.Read();
                }
                #endregion
                return orderHeaderSummary;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            finally
            {
                sqlDataReader.Close();
            }
        }
        public static ShoppingCartWIPHdrModel ShoppingCartWIPHdrGet(long personId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                #region
                sqlStmt += "        SELECT *" + Environment.NewLine;
                sqlStmt += "          FROM RetailSlnSch.ShoppingCartWIPHdr" + Environment.NewLine;
                sqlStmt += "    INNER JOIN RetailSlnSch.ShoppingCartWIP" + Environment.NewLine;
                sqlStmt += "            ON ShoppingCartWIPHdr.ShoppingCartWIPHdrId = ShoppingCartWIP.ShoppingCartWIPHdrId" + Environment.NewLine;
                sqlStmt += "         WHERE ShoppingCartWIPHdr.CreatedForPersonId = @PersonId" + Environment.NewLine;
                sqlStmt += "      ORDER BY ShoppingCartWIP.ShoppingCartWIPId" + Environment.NewLine;
                #endregion
                #region
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                sqlCommand.Parameters.Add("@PersonId", SqlDbType.BigInt);
                sqlCommand.Parameters["@PersonId"].Value = personId;
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                #endregion
                ShoppingCartWIPHdrModel shoppingCartWIPHdrModel = null;
                #region
                bool sqlDataReaderRead = sqlDataReader.Read();
                while (sqlDataReaderRead)
                {
                    shoppingCartWIPHdrModel = new ShoppingCartWIPHdrModel
                    {
                        ShoppingCartWIPHdrId = long.Parse(sqlDataReader["ShoppingCartWIPHdrId"].ToString()),
                        ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                        CorpAcctLocationId = long.Parse(sqlDataReader["CorpAcctLocationId"].ToString()),
                        CreatedForPersonId = long.Parse(sqlDataReader["CreatedForPersonId"].ToString()),
                        PersonId = long.Parse(sqlDataReader["PersonId"].ToString()),
                        SeqNum = 0,
                        ShoppingCartWIPModels = new List<ShoppingCartWIPModel>(),
                    };
                    while (sqlDataReaderRead && shoppingCartWIPHdrModel.ShoppingCartWIPHdrId == long.Parse(sqlDataReader["ShoppingCartWIPHdrId"].ToString()))
                    {
                        shoppingCartWIPHdrModel.ShoppingCartWIPModels.Add
                        (
                            new ShoppingCartWIPModel
                            {
                                ShoppingCartWIPId = long.Parse(sqlDataReader["ShoppingCartWIPId"].ToString()),
                                ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                                DoNotBreakBundle = bool.Parse(sqlDataReader["DoNotBreakBundle"].ToString()),
                                ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
                                ItemSeqNum = float.Parse(sqlDataReader["ItemSeqNum"].ToString()),
                                OrderComments = sqlDataReader["OrderComments"].ToString(),
                                OrderQty = long.Parse(sqlDataReader["OrderQty"].ToString()),
                                ParentItemId = long.Parse(sqlDataReader["ParentItemId"].ToString()),
                                ShoppingCartWIPHdrId = long.Parse(sqlDataReader["ShoppingCartWIPHdrId"].ToString()),
                            }
                        );
                        sqlDataReaderRead = sqlDataReader.Read();
                    }
                }
                sqlDataReader.Close();
                if (shoppingCartWIPHdrModel == null)
                {
                    shoppingCartWIPHdrModel = new ShoppingCartWIPHdrModel
                    {
                        ShoppingCartWIPModels = new List<ShoppingCartWIPModel>(),
                    };
                }
                else
                {
                    if (shoppingCartWIPHdrModel.ShoppingCartWIPModels == null)
                    {
                        shoppingCartWIPHdrModel.ShoppingCartWIPModels = new List<ShoppingCartWIPModel>();
                    }
                }
                #endregion

                return shoppingCartWIPHdrModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        #region
        //public static long? OrderHeaderWIPMaxIdGet(long personId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    long? orderHeaderWIPId;
        //    SqlDataReader sqlDataReader = null;
        //    try
        //    {
        //        SqlCommand sqlCommand = new SqlCommand("SELECT MAX(OrderHeaderWIPId) FROM RetailSlnSch.OrderHeaderWIP WHERE PersonId = " + personId, sqlConnection);
        //        sqlDataReader = sqlCommand.ExecuteReader();
        //        if (sqlDataReader.Read())
        //        {
        //            try
        //            {
        //                long.TryParse(sqlDataReader[0].ToString(), out long orderHeaderWIPIdTemp);
        //                orderHeaderWIPId = orderHeaderWIPIdTemp == 0 ? (long?)null : orderHeaderWIPIdTemp;
        //            }
        //            catch
        //            {
        //                orderHeaderWIPId = null;
        //            }
        //        }
        //        else
        //        {
        //            orderHeaderWIPId = null;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        orderHeaderWIPId = null;
        //    }
        //    finally
        //    {
        //        sqlDataReader.Close();
        //    }
        //    return orderHeaderWIPId;
        //}
        //public static float OrderDetailWIPMaxSeqNumGet(long orderHeaderWIPId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    float maxSeqNum;
        //    SqlDataReader sqlDataReader = null;
        //    try
        //    {
        //        SqlCommand sqlCommand = new SqlCommand("SELECT MAX(SeqNum) FROM RetailSlnSch.OrderDetailWIP WHERE OrderHeaderWIPId = " + orderHeaderWIPId, sqlConnection);
        //        sqlDataReader = sqlCommand.ExecuteReader();
        //        if (sqlDataReader.Read())
        //        {
        //            maxSeqNum = long.Parse(sqlDataReader[0].ToString());
        //        }
        //        else
        //        {
        //            maxSeqNum = 0;
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        maxSeqNum = 0;
        //    }
        //    finally
        //    {
        //        sqlDataReader.Close();
        //    }
        //    return maxSeqNum;
        //}
        //public static OrderHeaderWIPModel OrderHeaderWIPGet(long orderHeaderWIPId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    OrderHeaderWIPModel orderHeaderWIPModel = null;
        //    try
        //    {
        //        #region
        //        string sqlStmt = "";
        //        sqlStmt += "        SELECT *" + Environment.NewLine;
        //        sqlStmt += "          FROM RetailSlnSch.OrderHeaderWIP" + Environment.NewLine;
        //        sqlStmt += "    INNER JOIN RetailSlnSch.OrderDetailWIP" + Environment.NewLine;
        //        sqlStmt += "            ON OrderHeaderWIP.OrderHeaderWIPId = OrderDetailWIP.OrderHeaderWIPId" + Environment.NewLine;
        //        sqlStmt += "         WHERE OrderHeaderWIP.OrderHeaderWIPId = " + orderHeaderWIPId + Environment.NewLine;
        //        sqlStmt += "      ORDER BY OrderDetailWIP.SeqNum" + Environment.NewLine;
        //        SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
        //        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
        //        #endregion
        //        #region
        //        bool sqlDataReaderRead = sqlDataReader.Read();
        //        while (sqlDataReaderRead)
        //        {
        //            orderHeaderWIPModel = new OrderHeaderWIPModel
        //            {
        //                OrderHeaderWIPId = long.Parse(sqlDataReader["OrderHeaderWIPId"].ToString()),
        //                ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
        //                CorpAcctLocationId = long.Parse(sqlDataReader["CorpAcctLocationId"].ToString()),
        //                CreatedForPersonId = long.Parse(sqlDataReader["CreatedForPersonId"].ToString()),
        //                InvoiceTypeId = long.Parse(sqlDataReader["InvoiceTypeId"].ToString()),
        //                OrderDateTime = DateTime.Parse(sqlDataReader["OrderDateTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
        //                OrderStatusId = sqlDataReader["OrderStatusId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["OrderStatusId"].ToString()),
        //                PersonId = long.Parse(sqlDataReader["PersonId"].ToString()),
        //                OrderDetailWIPModels = new List<OrderDetailWIPModel>(),
        //            };
        //            while (sqlDataReaderRead)
        //            {
        //                orderHeaderWIPModel.OrderDetailWIPModels.Add
        //                (
        //                    new OrderDetailWIPModel
        //                    {
        //                        OrderDetailWIPId = long.Parse(sqlDataReader["OrderDetailWIPId"].ToString()),
        //                        ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
        //                        ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
        //                        ItemRate = float.Parse(sqlDataReader["ItemRate"].ToString()),
        //                        OrderHeaderWIPId = long.Parse(sqlDataReader["OrderHeaderWIPId"].ToString()),
        //                        OrderQty = long.Parse(sqlDataReader["OrderQty"].ToString()),
        //                        SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
        //                    }
        //                );
        //                sqlDataReaderRead = sqlDataReader.Read();
        //            }
        //        }
        //        sqlDataReader.Close();
        //        #endregion
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //    }
        //    return orderHeaderWIPModel;
        //}
        //public static List<SearchMetaDataModel> SearchMetaDatasGet(string searchKeywordText, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        #region
        //        string sqlStmt = "";
        //        sqlStmt += "        SELECT DISTINCT SearchMetaData.EntityTypeNameDesc, SearchMetaData.EntityId, SearchMetaData.SeqNum" + Environment.NewLine;
        //        sqlStmt += "          FROM RetailSlnSch.SearchKeyword" + Environment.NewLine;
        //        sqlStmt += "    INNER JOIN RetailSlnSch.SearchMetaData" + Environment.NewLine;
        //        sqlStmt += "            ON SearchKeyword.SearchKeywordId = SearchMetaData.SearchKeywordId" + Environment.NewLine;
        //        sqlStmt += "           AND SearchKeyword.SearchKeywordText LIKE '%" + searchKeywordText + "%'" + Environment.NewLine;
        //        sqlStmt += "UNION" + Environment.NewLine;
        //        sqlStmt += "        SELECT DISTINCT 'ITEMMASTER' AS EntityTypeNameDesc, ItemMaster.ItemMasterId, ItemMaster.ItemMasterId AS SeqNum" + Environment.NewLine;
        //        sqlStmt += "          FROM RetailSlnSch.ItemMaster" + Environment.NewLine;
        //        sqlStmt += "         WHERE ItemMasterDesc LIKE '%" + searchKeywordText + "%'" + Environment.NewLine;
        //        sqlStmt += "UNION" + Environment.NewLine;
        //        sqlStmt += "        SELECT DISTINCT 'CATEGORY' AS EntityTypeNameDesc, Category.CategoryId, Category.CategoryId AS SeqNum" + Environment.NewLine;
        //        sqlStmt += "          FROM RetailSlnSch.Category" + Environment.NewLine;
        //        sqlStmt += "         WHERE CategoryDesc LIKE '%" + searchKeywordText + "%'" + Environment.NewLine;
        //        sqlStmt += "      ORDER BY" + Environment.NewLine;
        //        sqlStmt += "               EntityTypeNameDesc" + Environment.NewLine;
        //        sqlStmt += "              ,SeqNum" + Environment.NewLine;
        //        #endregion
        //        SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
        //        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
        //        List<SearchMetaDataModel> searchMetaDataModels = new List<SearchMetaDataModel>();
        //        SearchMetaDataModel searchMetaDataModel;
        //        while (sqlDataReader.Read())
        //        {
        //            searchMetaDataModels.Add
        //            (
        //                searchMetaDataModel = new SearchMetaDataModel
        //                {
        //                    //SearchMetaDataModelId = long.Parse(sqlDataReader["SearchMetaDataId"].ToString()),
        //                    //SearchKeywordId = long.Parse(sqlDataReader["SearchKeywordId"].ToString()),
        //                    EntityTypeNameDesc = sqlDataReader["EntityTypeNameDesc"].ToString(),
        //                    EntityId = long.Parse(sqlDataReader["EntityId"].ToString()),
        //                    SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
        //                    SearchKeywordModel = new SearchKeywordModel
        //                    {
        //                        //SearchKeywordId = long.Parse(sqlDataReader["SearchKeywordId"].ToString()),
        //                        //SearchKeywordText = sqlDataReader["SearchKeywordText"].ToString(),
        //                    },
        //                }
        //            );
        //        }
        //        return searchMetaDataModels;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        #endregion
    }
}
