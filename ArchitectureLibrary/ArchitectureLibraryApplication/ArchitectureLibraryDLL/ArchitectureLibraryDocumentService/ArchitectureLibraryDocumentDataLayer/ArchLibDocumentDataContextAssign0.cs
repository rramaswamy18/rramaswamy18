using ArchitectureLibraryDocumentModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryDocumentDataLayer
{
    public static partial class ArchLibDocumentDataContext
    {
        public static DocumentModel AssignDocumentModel(SqlDataReader sqlDataReader, string columnPrefix, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            DocumentModel documentModel = new DocumentModel
            {
                DocumentId = long.Parse(sqlDataReader[columnPrefix + "DocumentId"].ToString()),
                ClientId = long.Parse(sqlDataReader[columnPrefix + "ClientId"].ToString()),
                ClientFileName = sqlDataReader[columnPrefix + "ClientFileName"].ToString(),
                ClientHeight = sqlDataReader[columnPrefix + "ClientHeight"].ToString() == "" ? (int?)null : int.Parse(sqlDataReader[columnPrefix + "ClientHeight"].ToString()),
                ClientHeightUnit = sqlDataReader[columnPrefix + "ClientHeightUnit"].ToString(),
                ClientWidth = sqlDataReader[columnPrefix + "ClientWidth"].ToString() == "" ? (int?)null : int.Parse(sqlDataReader[columnPrefix + "ClientWidth"].ToString()),
                ClientWidthUnit = sqlDataReader[columnPrefix + "ClientWidthUnit"].ToString(),
                ContentData = sqlDataReader[columnPrefix + "ContentData"].ToString(),
                ContentLength = sqlDataReader[columnPrefix + "ContentLength"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader[columnPrefix + "ContentLength"].ToString()),
                ContentType = sqlDataReader[columnPrefix + "ContentType"].ToString(),
                FileExtension = sqlDataReader[columnPrefix + "FileExtension"].ToString(),
                Height = sqlDataReader[columnPrefix + "Height"].ToString() == "" ? (int?)null : int.Parse(sqlDataReader[columnPrefix + "Height"].ToString()),
                HeightUnit = sqlDataReader[columnPrefix + "HeightUnit"].ToString(),
                ServerFileName = sqlDataReader[columnPrefix + "ServerFileName"].ToString(),
                Width = sqlDataReader[columnPrefix + "Width"].ToString() == "" ? (int?)null : int.Parse(sqlDataReader[columnPrefix + "Width"].ToString()),
                WidthUnit = sqlDataReader[columnPrefix + "WidthUnit"].ToString(),
            };
            return documentModel;
        }
        public static void AssignDocumentModel(DocumentModel documentModel, SqlCommand sqlCommand, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            sqlCommand.Parameters["@ClientFileName"].Value = documentModel.ClientFileName;
            sqlCommand.Parameters["@ClientHeight"].Value = documentModel.ClientHeight;
            sqlCommand.Parameters["@ClientHeightUnit"].Value = documentModel.ClientHeightUnit;
            sqlCommand.Parameters["@ClientWidth"].Value = documentModel.ClientWidth;
            sqlCommand.Parameters["@ClientWidthUnit"].Value = documentModel.ClientWidthUnit;
            sqlCommand.Parameters["@ContentData"].Value = documentModel.ContentData;
            sqlCommand.Parameters["@ContentLength"].Value = documentModel.ContentLength;
            sqlCommand.Parameters["@ContentType"].Value = documentModel.ContentType;
            sqlCommand.Parameters["@DocumentId"].Value = documentModel.DocumentId;
            sqlCommand.Parameters["@FileExtension"].Value = documentModel.FileExtension;
            sqlCommand.Parameters["@Height"].Value = documentModel.Height ?? (object)DBNull.Value;
            sqlCommand.Parameters["@HeightUnit"].Value = documentModel.HeightUnit ?? (object)DBNull.Value;
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
            sqlCommand.Parameters["@Width"].Value = documentModel.Height ?? (object)DBNull.Value;
            sqlCommand.Parameters["@WidthUnit"].Value = documentModel.HeightUnit ?? (object)DBNull.Value;
        }
    }
}
