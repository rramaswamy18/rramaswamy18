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
        public static void UpdDocument(DocumentModel documentModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                SqlCommand sqlCommand = BuildSqlCommandDocumentUpd(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                sqlCommand.Parameters["@ClientContentLength"].Value = documentModel.ClientContentLength == null ? (object)DBNull.Value : documentModel.ClientContentLength;
                sqlCommand.Parameters["@ClientFileName"].Value = documentModel.ClientFileName == null ? (object)DBNull.Value : documentModel.ClientFileName;
                sqlCommand.Parameters["@ClientHeight"].Value = documentModel.ClientHeight == null ? (object)DBNull.Value : documentModel.ClientHeight;
                sqlCommand.Parameters["@ClientHeightUnit"].Value = documentModel.ClientHeightUnit == null ? (object)DBNull.Value : documentModel.ClientHeightUnit;
                sqlCommand.Parameters["@ClientWidth"].Value = documentModel.ClientWidth == null ? (object)DBNull.Value : documentModel.ClientWidth;
                sqlCommand.Parameters["@ClientWidthUnit"].Value = documentModel.ClientWidthUnit == null ? (object)DBNull.Value : documentModel.ClientWidthUnit;
                sqlCommand.Parameters["@ContentByteData"].Value = documentModel.ContentByteData == null ? (object)DBNull.Value : documentModel.ContentByteData;
                sqlCommand.Parameters["@ContentData"].Value = documentModel.ContentData == null ? (object)DBNull.Value : documentModel.ContentData;
                sqlCommand.Parameters["@ContentLength"].Value = documentModel.ContentLength == null ? (object)DBNull.Value : documentModel.ContentLength;
                sqlCommand.Parameters["@ContentType"].Value = documentModel.ContentType == null ? (object)DBNull.Value : documentModel.ContentType;
                sqlCommand.Parameters["@DocumentCategoryName"].Value = documentModel.DocumentCategoryName == null ? (object)DBNull.Value : documentModel.DocumentCategoryName;
                sqlCommand.Parameters["@DocumentDesc"].Value = documentModel.DocumentDesc == null ? (object)DBNull.Value : documentModel.DocumentDesc;
                sqlCommand.Parameters["@DocumentStatusId"].Value = documentModel.DocumentStatusId == null ? (object)DBNull.Value : documentModel.DocumentStatusId;
                sqlCommand.Parameters["@DocumentTypeId"].Value = documentModel.DocumentTypeId == null ? (object)DBNull.Value : documentModel.DocumentTypeId;
                sqlCommand.Parameters["@DocumentTypeDesc"].Value = documentModel.DocumentTypeDesc == null ? (object)DBNull.Value : documentModel.DocumentTypeDesc;
                sqlCommand.Parameters["@FileExtension"].Value = documentModel.FileExtension == null ? (object)DBNull.Value : documentModel.FileExtension;
                sqlCommand.Parameters["@Height"].Value = documentModel.Height == null ? (object)DBNull.Value : documentModel.Height;
                sqlCommand.Parameters["@HeightUnit"].Value = documentModel.HeightUnit == null ? (object)DBNull.Value : documentModel.HeightUnit;
                sqlCommand.Parameters["@ServerFileName"].Value = documentModel.ServerFileName == null ? (object)DBNull.Value : documentModel.ServerFileName;
                sqlCommand.Parameters["@Width"].Value = documentModel.Width == null ? (object)DBNull.Value : documentModel.Width;
                sqlCommand.Parameters["@WidthUnit"].Value = documentModel.WidthUnit == null ? (object)DBNull.Value : documentModel.WidthUnit;
                sqlCommand.Parameters["@DocumentId"].Value = documentModel.DocumentId;
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }

        }
        public static void UpdDocument1(DocumentModel documentModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                SqlCommand sqlCommand = BuildSqlCommandUpdateDocument(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                sqlCommand.Parameters["@DocumentId"].Value = documentModel.DocumentId;
                sqlCommand.Parameters["@ContentData"].Value = documentModel.ContentData;
                sqlCommand.Parameters["@ContentLength"].Value = documentModel.ContentLength;
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static void UpdDocument2(DocumentModel documentModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                SqlCommand sqlCommand = BuildSqlCommandDocumentUpd2(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                sqlCommand.Parameters["@DocumentId"].Value = documentModel.DocumentId;
                sqlCommand.Parameters["@ServerFileName"].Value = documentModel.ServerFileName;
                sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
    }
}
