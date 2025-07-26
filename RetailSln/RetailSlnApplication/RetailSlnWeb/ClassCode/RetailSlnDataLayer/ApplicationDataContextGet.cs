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
        public static long? OrderHeaderWIPMaxIdGet(long personId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
                    try
                    {
                        long.TryParse(sqlDataReader[0].ToString(), out long orderHeaderWIPIdTemp);
                        orderHeaderWIPId = orderHeaderWIPIdTemp == 0 ? (long?)null : orderHeaderWIPIdTemp;
                    }
                    catch
                    {
                        orderHeaderWIPId = null;
                    }
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
        public static float OrderDetailWIPMaxSeqNumGet(long orderHeaderWIPId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            float maxSeqNum;
            SqlDataReader sqlDataReader = null;
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT MAX(SeqNum) FROM RetailSlnSch.OrderDetailWIP WHERE OrderHeaderWIPId = " + orderHeaderWIPId, sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    maxSeqNum = long.Parse(sqlDataReader[0].ToString());
                }
                else
                {
                    maxSeqNum = 0;
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                maxSeqNum = 0;
            }
            finally
            {
                sqlDataReader.Close();
            }
            return maxSeqNum;
        }
        public static OrderHeaderWIPModel OrderHeaderWIPGet(long orderHeaderWIPId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
                        InvoiceTypeId = long.Parse(sqlDataReader["InvoiceTypeId"].ToString()),
                        OrderDateTime = DateTime.Parse(sqlDataReader["OrderDateTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
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
                                OrderDetailWIPId = long.Parse(sqlDataReader["OrderDetailWIPId"].ToString()),
                                ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                                ItemId = long.Parse(sqlDataReader["ItemId"].ToString()),
                                ItemRate = float.Parse(sqlDataReader["ItemRate"].ToString()),
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
        public static List<SearchMetaDataModel> SearchMetaDatasGet(string searchKeywordText, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                #region
                string sqlStmt = "";
                sqlStmt += "        SELECT DISTINCT SearchMetaData.EntityTypeNameDesc, SearchMetaData.EntityId, SearchMetaData.SeqNum" + Environment.NewLine;
                sqlStmt += "          FROM RetailSlnSch.SearchKeyword" + Environment.NewLine;
                sqlStmt += "    INNER JOIN RetailSlnSch.SearchMetaData" + Environment.NewLine;
                sqlStmt += "            ON SearchKeyword.SearchKeywordId = SearchMetaData.SearchKeywordId" + Environment.NewLine;
                sqlStmt += "           AND SearchKeyword.SearchKeywordText LIKE '%" + searchKeywordText + "%'" + Environment.NewLine;
                sqlStmt += "UNION" + Environment.NewLine;
                sqlStmt += "        SELECT DISTINCT 'ITEMMASTER' AS EntityTypeNameDesc, ItemMaster.ItemMasterId, ItemMaster.ItemMasterId AS SeqNum" + Environment.NewLine;
                sqlStmt += "          FROM RetailSlnSch.ItemMaster" + Environment.NewLine;
                sqlStmt += "         WHERE ItemMasterDesc LIKE '%" + searchKeywordText + "%'" + Environment.NewLine;
                sqlStmt += "UNION" + Environment.NewLine;
                sqlStmt += "        SELECT DISTINCT 'CATEGORY' AS EntityTypeNameDesc, Category.CategoryId, Category.CategoryId AS SeqNum" + Environment.NewLine;
                sqlStmt += "          FROM RetailSlnSch.Category" + Environment.NewLine;
                sqlStmt += "         WHERE CategoryDesc LIKE '%" + searchKeywordText + "%'" + Environment.NewLine;
                sqlStmt += "      ORDER BY" + Environment.NewLine;
                sqlStmt += "               EntityTypeNameDesc" + Environment.NewLine;
                sqlStmt += "              ,SeqNum" + Environment.NewLine;
                #endregion
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<SearchMetaDataModel> searchMetaDataModels = new List<SearchMetaDataModel>();
                SearchMetaDataModel searchMetaDataModel;
                while (sqlDataReader.Read())
                {
                    searchMetaDataModels.Add
                    (
                        searchMetaDataModel = new SearchMetaDataModel
                        {
                            //SearchMetaDataModelId = long.Parse(sqlDataReader["SearchMetaDataId"].ToString()),
                            //SearchKeywordId = long.Parse(sqlDataReader["SearchKeywordId"].ToString()),
                            EntityTypeNameDesc = sqlDataReader["EntityTypeNameDesc"].ToString(),
                            EntityId = long.Parse(sqlDataReader["EntityId"].ToString()),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                            SearchKeywordModel = new SearchKeywordModel
                            {
                                //SearchKeywordId = long.Parse(sqlDataReader["SearchKeywordId"].ToString()),
                                //SearchKeywordText = sqlDataReader["SearchKeywordText"].ToString(),
                            },
                        }
                    );
                }
                return searchMetaDataModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
    }
}
