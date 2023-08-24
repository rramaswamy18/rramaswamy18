using ArchitectureLibraryCreditCardModels;
using ArchitectureLibraryUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryCreditCardDataLayer
{
    public static partial class ArchLibCreditCardDataContext
    {
        public static void AddCreditCardData(CreditCardDataModel creditCardDataModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ArchitectureLibraryException.ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                SqlCommand sqlCommand = BuildSqlCommandCreditCardData(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                AssignCreditCardData(creditCardDataModel, sqlCommand, clientId, ipAddress, execUniqueId, loggedInUserId);
                creditCardDataModel.CreditCardDataId = (long)sqlCommand.ExecuteScalar();
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
