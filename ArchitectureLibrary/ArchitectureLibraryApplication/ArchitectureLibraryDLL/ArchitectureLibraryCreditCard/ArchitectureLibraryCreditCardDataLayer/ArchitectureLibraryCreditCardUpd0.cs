using ArchitectureLibraryUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryCreditCardDataLayer
{
    public static partial class ArchLibCreditCardDataContext
    {
        public static void UpdCreditCardData(long creditCardDataId, string razorpayJsonString, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ArchitectureLibraryException.ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                sqlStmt += "    UPDATE ArchLib.CreditCardData" + Environment.NewLine;
                sqlStmt += "       SET ResponseData1 = @ResponseData1" + Environment.NewLine;
                sqlStmt += "          ,UpdUserName = SUSER_NAME()" + Environment.NewLine;
                sqlStmt += "          ,UpdDateTime = GETDATE()" + Environment.NewLine;
                sqlStmt += "     WHERE CreditCardDataId = @CreditCardDataId" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection) ;
                sqlCommand.Parameters.Add("@ResponseData1", SqlDbType.NVarChar);
                sqlCommand.Parameters.Add("@CreditCardDataId", SqlDbType.BigInt);
                sqlCommand.Parameters["@ResponseData1"].Value = razorpayJsonString;
                sqlCommand.Parameters["@CreditCardDataId"].Value = creditCardDataId;
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
    }
}
