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
        public static PersonExtn1Model PersonExtn1FromPersonIdGet(long personId, long corpAcctLocationId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
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
                        orderHeaderWIPId = long.Parse(sqlDataReader[0].ToString());
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
                                OrderDetailWIPId = long.Parse(sqlDataReader["OrderDetailWIPId"].ToString()),
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
    }
}
