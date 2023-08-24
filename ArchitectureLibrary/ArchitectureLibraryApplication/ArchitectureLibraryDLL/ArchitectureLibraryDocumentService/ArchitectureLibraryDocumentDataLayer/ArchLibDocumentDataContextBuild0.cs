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

namespace ArchitectureLibraryDocumentDataLayer
{
    public static partial class ArchLibDocumentDataContext
    {
        private static SqlCommand BuildSqlCommandDocumentAdd(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "";
            sqlStmt += "        INSERT ArchLib.Document" + Environment.NewLine;
            sqlStmt += "              (" + Environment.NewLine;
            sqlStmt += "               ClientId" + Environment.NewLine;
            sqlStmt += "              ,ClientContentLength" + Environment.NewLine;
            sqlStmt += "              ,ClientFileName" + Environment.NewLine;
            sqlStmt += "              ,ClientHeight" + Environment.NewLine;
            sqlStmt += "              ,ClientHeightUnit" + Environment.NewLine;
            sqlStmt += "              ,ClientWidth" + Environment.NewLine;
            sqlStmt += "              ,ClientWidthUnit" + Environment.NewLine;
            sqlStmt += "              ,ContentByteData" + Environment.NewLine;
            sqlStmt += "              ,ContentData" + Environment.NewLine;
            sqlStmt += "              ,ContentLength" + Environment.NewLine;
            sqlStmt += "              ,ContentType" + Environment.NewLine;
            sqlStmt += "              ,DocumentCategoryName" + Environment.NewLine;
            sqlStmt += "              ,DocumentDesc" + Environment.NewLine;
            sqlStmt += "              ,DocumentStatusId" + Environment.NewLine;
            sqlStmt += "              ,DocumentTypeId" + Environment.NewLine;
            sqlStmt += "              ,DocumentTypeDesc" + Environment.NewLine;
            sqlStmt += "              ,FileExtension" + Environment.NewLine;
            sqlStmt += "              ,Height" + Environment.NewLine;
            sqlStmt += "              ,HeightUnit" + Environment.NewLine;
            sqlStmt += "              ,ServerFileName" + Environment.NewLine;
            sqlStmt += "              ,Width" + Environment.NewLine;
            sqlStmt += "              ,WidthUnit" + Environment.NewLine;
            sqlStmt += "              )" + Environment.NewLine;
            sqlStmt += "        OUTPUT INSERTED.DocumentId" + Environment.NewLine;
            sqlStmt += "        SELECT" + Environment.NewLine;
            sqlStmt += "               @ClientId" + Environment.NewLine;
            sqlStmt += "              ,@ClientContentLength" + Environment.NewLine;
            sqlStmt += "              ,@ClientFileName" + Environment.NewLine;
            sqlStmt += "              ,@ClientHeight" + Environment.NewLine;
            sqlStmt += "              ,@ClientHeightUnit" + Environment.NewLine;
            sqlStmt += "              ,@ClientWidth" + Environment.NewLine;
            sqlStmt += "              ,@ClientWidthUnit" + Environment.NewLine;
            sqlStmt += "              ,NULL" + Environment.NewLine;
            sqlStmt += "              ,@ContentData" + Environment.NewLine;
            sqlStmt += "              ,@ContentLength" + Environment.NewLine;
            sqlStmt += "              ,@ContentType" + Environment.NewLine;
            sqlStmt += "              ,@DocumentCategoryName" + Environment.NewLine;
            sqlStmt += "              ,@DocumentDesc" + Environment.NewLine;
            sqlStmt += "              ,@DocumentStatusId" + Environment.NewLine;
            sqlStmt += "              ,@DocumentTypeId" + Environment.NewLine;
            sqlStmt += "              ,@DocumentTypeDesc" + Environment.NewLine;
            sqlStmt += "              ,@FileExtension" + Environment.NewLine;
            sqlStmt += "              ,@Height" + Environment.NewLine;
            sqlStmt += "              ,@HeightUnit" + Environment.NewLine;
            sqlStmt += "              ,@ServerFileName" + Environment.NewLine;
            sqlStmt += "              ,@Width" + Environment.NewLine;
            sqlStmt += "              ,@WidthUnit" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@ClientId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ClientContentLength", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ClientFileName", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ClientHeight", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ClientHeightUnit", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ClientWidth", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ClientWidthUnit", System.DBNull.Value);
            //sqlCommand.Parameters.AddWithValue("@ContentByteData", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ContentData", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ContentLength", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ContentType", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@DocumentCategoryName", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@DocumentDesc", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@DocumentStatusId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@DocumentTypeId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@DocumentTypeDesc", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@FileExtension", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@Height", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@HeightUnit", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ServerFileName", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@Width", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@WidthUnit", System.DBNull.Value);
            return sqlCommand;
        }
        private static SqlCommand BuildSqlCommandDocumentUpd(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "";
            sqlStmt += "        UPDATE ArchLib.Document" + Environment.NewLine;
            sqlStmt += "           SET " + Environment.NewLine;
            sqlStmt += "               ClientContentLength = @ClientContentLength" + Environment.NewLine;
            sqlStmt += "              ,ClientFileName = @ClientFileName" + Environment.NewLine;
            sqlStmt += "              ,ClientHeight = @ClientHeight" + Environment.NewLine;
            sqlStmt += "              ,ClientHeightUnit = @ClientHeightUnit" + Environment.NewLine;
            sqlStmt += "              ,ClientWidth = @ClientWidth" + Environment.NewLine;
            sqlStmt += "              ,ClientWidthUnit = @ClientWidthUnit" + Environment.NewLine;
            sqlStmt += "              ,ContentByteData = @ContentByteData" + Environment.NewLine;
            sqlStmt += "              ,ContentData = @ContentData" + Environment.NewLine;
            sqlStmt += "              ,ContentLength = @ContentLength" + Environment.NewLine;
            sqlStmt += "              ,ContentType = @ContentType" + Environment.NewLine;
            sqlStmt += "              ,DocumentCategoryName = @DocumentCategoryName" + Environment.NewLine;
            sqlStmt += "              ,DocumentDesc = @DocumentDesc" + Environment.NewLine;
            sqlStmt += "              ,DocumentStatusId = @DocumentStatusId" + Environment.NewLine;
            sqlStmt += "              ,DocumentTypeId = @DocumentTypeId" + Environment.NewLine;
            sqlStmt += "              ,DocumentTypeDesc = @DocumentTypeDesc" + Environment.NewLine;
            sqlStmt += "              ,FileExtension = @FileExtension" + Environment.NewLine;
            sqlStmt += "              ,Height = @Height" + Environment.NewLine;
            sqlStmt += "              ,HeightUnit = @HeightUnit" + Environment.NewLine;
            sqlStmt += "              ,ServerFileName = @ServerFileName" + Environment.NewLine;
            sqlStmt += "              ,Width = @Width" + Environment.NewLine;
            sqlStmt += "              ,WidthUnit = @WidthUnit" + Environment.NewLine;
            sqlStmt += "         WHERE " + Environment.NewLine;
            sqlStmt += "               DocumentId = @DocumentId" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ClientContentLength", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@ClientFileName", SqlDbType.NVarChar, 500);
            sqlCommand.Parameters.Add("@ClientHeight", SqlDbType.Int);
            sqlCommand.Parameters.Add("@ClientHeightUnit", SqlDbType.NVarChar, 10);
            sqlCommand.Parameters.Add("@ClientWidth", SqlDbType.Int);
            sqlCommand.Parameters.Add("@ClientWidthUnit", SqlDbType.NVarChar, 10);
            sqlCommand.Parameters.Add("@ContentByteData", SqlDbType.VarBinary);
            sqlCommand.Parameters.Add("@ContentData", SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@ContentLength", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@ContentType", SqlDbType.NVarChar, 100);
            sqlCommand.Parameters.Add("@DocumentCategoryName", SqlDbType.NVarChar, 100);
            sqlCommand.Parameters.Add("@DocumentDesc", SqlDbType.NVarChar, 250);
            sqlCommand.Parameters.Add("@DocumentStatusId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@DocumentTypeId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@DocumentTypeDesc", SqlDbType.NVarChar, 50);
            sqlCommand.Parameters.Add("@FileExtension", SqlDbType.NVarChar, 50);
            sqlCommand.Parameters.Add("@Height", SqlDbType.Int);
            sqlCommand.Parameters.Add("@HeightUnit", SqlDbType.NVarChar, 10);
            sqlCommand.Parameters.Add("@ServerFileName", SqlDbType.NVarChar, 100);
            sqlCommand.Parameters.Add("@Width", SqlDbType.Int);
            sqlCommand.Parameters.Add("@WidthUnit", SqlDbType.NVarChar, 10);
            sqlCommand.Parameters.Add("@DocumentId", SqlDbType.NVarChar);
            return sqlCommand;
        }
        private static SqlCommand BuildSqlCommandDocumentUpd1(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "";
            sqlStmt += "        UPDATE ArchLib.Document" + Environment.NewLine;
            sqlStmt += "           SET " + Environment.NewLine;
            sqlStmt += "               ContentData = @ContentData" + Environment.NewLine;
            sqlStmt += "              ,ContentLength = @ContentLength" + Environment.NewLine;
            sqlStmt += "         WHERE DocumentId = @DocumentId" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@DocumentId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ContentData", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ContentLength", System.DBNull.Value);
            return sqlCommand;
        }
        private static SqlCommand BuildSqlCommandDocumentUpd2(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "";
            sqlStmt += "        UPDATE ArchLib.Document" + Environment.NewLine;
            sqlStmt += "           SET " + Environment.NewLine;
            sqlStmt += "               ServerFileName = @ServerFileName" + Environment.NewLine;
            sqlStmt += "              ,UpdUserId = @LoggedInUserId" + Environment.NewLine;
            sqlStmt += "              ,UpdUserName = SUSER_NAME()" + Environment.NewLine;
            sqlStmt += "              ,UpdDateTime = GETDATE()" + Environment.NewLine;
            sqlStmt += "         WHERE DocumentId = @DocumentId" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@DocumentId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ServerFileName", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@LoggedInUserId", System.DBNull.Value);
            return sqlCommand;
        }
        private static SqlCommand BuildSqlCommandUpdateDocument(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "";
            sqlStmt += "        UPDATE ArchLib.Document" + Environment.NewLine;
            sqlStmt += "           SET " + Environment.NewLine;
            sqlStmt += "               ClientFileName = @ClientFileName" + Environment.NewLine;
            sqlStmt += "              ,ClientHeight = @ClientHeight" + Environment.NewLine;
            sqlStmt += "              ,ClientHeightUnit = @ClientHeightUnit" + Environment.NewLine;
            sqlStmt += "              ,ClientWidth = @ClientWidth" + Environment.NewLine;
            sqlStmt += "              ,ClientWidthUnit = @ClientWidthUnit" + Environment.NewLine;
            sqlStmt += "              ,ContentData = @ContentData" + Environment.NewLine;
            sqlStmt += "              ,ContentLength = @ContentLength" + Environment.NewLine;
            sqlStmt += "              ,ContentType = @ContentType" + Environment.NewLine;
            sqlStmt += "              ,FileExtension = @FileExtension" + Environment.NewLine;
            sqlStmt += "              ,Height = @Height" + Environment.NewLine;
            sqlStmt += "              ,HeightUnit = @HeightUnit" + Environment.NewLine;
            sqlStmt += "              ,Width = @Width" + Environment.NewLine;
            sqlStmt += "              ,WidthUnit = @WidthUnit" + Environment.NewLine;
            sqlStmt += "              ,UpdUserId = @LoggedInUserId" + Environment.NewLine;
            sqlStmt += "              ,UpdUserName = SUSER_NAME()" + Environment.NewLine;
            sqlStmt += "         WHERE DocumentId = @DocumentId" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@ClientFileName", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ClientHeight", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ClientHeightUnit", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ClientWidth", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ClientWidthUnit", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ContentData", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ContentLength", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ContentType", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@FileExtension", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@Height", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@HeightUnit", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@Width", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@WidthUnit", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@LoggedInUserId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@DocumentId", System.DBNull.Value);
            return sqlCommand;
        }
    }
}
