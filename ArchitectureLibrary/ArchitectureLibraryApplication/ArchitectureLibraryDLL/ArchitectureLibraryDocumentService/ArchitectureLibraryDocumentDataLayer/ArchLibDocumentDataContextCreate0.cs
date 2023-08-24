using ArchitectureLibraryDocumentModels;
using ArchitectureLibraryException;
using ArchitectureLibraryUtility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryDocumentDataLayer
{
    public static partial class ArchLibDocumentDataContext
    {
        //public static void CreateDocument(SqlConnection sqlConnection, DocumentModel documentModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId, bool dummy1)
        //{
        //    //This supports when this needs to be executed standalone
        //    //This opens the connection locally and closes locally
        //    OpenSqlConnection();
        //    CreateDocument(documentModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //    CloseSqlConnection();
        //}
        //public static void CreateDocument(SqlConnection sqlConnection, DocumentModel documentModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    //Use the connection opened in this context - Just for convenience not to pass the connection object
        //    //Sql connection in this context is expected to be opened before this is called
        //    //Open and close is done by caller
        //    CreateDocument(documentModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //}
        //public static void CreateDocument(DocumentModel documentModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    //Supports sql connection being established by the caller
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: Before Add Document", "execUniqueId", execUniqueId, "loggedInUserId", loggedInUserId);
        //        AddDocument(documentModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00009000 :: Exit");
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
    }
}
