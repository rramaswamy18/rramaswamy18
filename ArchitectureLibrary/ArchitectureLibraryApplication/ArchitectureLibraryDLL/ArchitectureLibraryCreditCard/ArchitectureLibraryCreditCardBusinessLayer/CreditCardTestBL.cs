using ArchitectureLibraryException;
using ArchitectureLibraryUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryCreditCardBusinessLayer
{
    public class CreditCardTestBL
    {
        public bool ProcessCreditCard(string creditCardAmount, string currencyCode, string creditCardNumber, string creditCardSecCode, string creditCardExpMM, string creditCardExpYear, string nameAsOnCard, string creditCardTranType, Dictionary<string, string> creditCardKVPs, SqlConnection sqlConnection, out string cardNumberLast4, out string processMessage, out string requestData, out string responseData, long clientId = 0, string ipAddress = "", string execUniqueId = "", string loggedInUserId = "")
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string transactionType = "PURCHASE";
                SqlCommand sqlCommand = BuildSqlCommand(sqlConnection);
                sqlCommand.Parameters["@CreditCardNumber"].Value = creditCardNumber;
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    if (sqlDataReader["CVVCode"].ToString() == creditCardSecCode)
                    {
                        if (sqlDataReader["CardExpiryYYYYMM"].ToString() == creditCardExpYear + creditCardExpMM)
                        {
                            if (sqlDataReader["CurrencyCode"].ToString() == currencyCode)
                            {
                                if (sqlDataReader["NameAsOnCard"].ToString() == nameAsOnCard)
                                {
                                    if (sqlDataReader["TransactionTYpe"].ToString() == transactionType)
                                    {
                                        if (float.Parse(sqlDataReader["AmountAvailable"].ToString()) >= float.Parse(creditCardAmount))
                                        {
                                            processMessage = "SUCCESS";
                                        }
                                        else
                                        {
                                            processMessage = "Insufficient funds";
                                        }
                                    }
                                    else
                                    {
                                        processMessage = "Invalid transation type";
                                    }
                                }
                                else
                                {
                                    processMessage = "Name on card does not match";
                                }
                            }
                            else
                            {
                                processMessage = "Invalid currency code";
                            }
                        }
                        else
                        {
                            processMessage = "Invalid card expiry";
                        }
                    }
                    else
                    {
                        processMessage = "Invalid CVV Code";
                    }
                }
                else
                {
                    processMessage = "Invalid card number";
                }
                sqlDataReader.Close();
                cardNumberLast4 = creditCardNumber.Substring(creditCardNumber.Length - 4);
                requestData = "Test Mode " + creditCardNumber + " " + creditCardSecCode;
                responseData = processMessage + " " + requestData;
                return processMessage == "SUCCESS" ? true : false;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        private SqlCommand BuildSqlCommand(SqlConnection sqlConnection)
        {
            string sqlStmt = "";
            sqlStmt += "SELECT * FROM ArchLib.CreditCardTest WHERE CreditCardNumber = @CreditCardNumber" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@CreditCardNumber", SqlDbType.NVarChar, 50);
            return sqlCommand;
        }
    }
}
