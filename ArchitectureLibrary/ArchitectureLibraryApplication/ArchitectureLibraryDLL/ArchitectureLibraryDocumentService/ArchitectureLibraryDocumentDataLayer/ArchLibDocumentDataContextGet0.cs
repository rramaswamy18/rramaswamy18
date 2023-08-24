using ArchitectureLibraryDocumentEnumerations;
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
        public static DocumentModel GetDocument(long documentId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM ArchLib.Document WHERE DocumentId = " + documentId, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DocumentModel documentModel = null;
            if (sqlDataReader.Read())
            {
                documentModel = new DocumentModel
                {
                    DocumentId = long.Parse(sqlDataReader["DocumentId"].ToString()),
                    ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                    ClientFileName = sqlDataReader["ClientFileName"].ToString(),
                    ClientHeight = int.Parse(sqlDataReader["ClientHeight"].ToString()),
                    ClientHeightUnit = sqlDataReader["ClientHeightUnit"].ToString(),
                    ClientWidth = int.Parse(sqlDataReader["ClientWidth"].ToString()),
                    ClientWidthUnit = sqlDataReader["ClientWidthUnit"].ToString(),
                    ContentByteData = null,
                    ContentData = sqlDataReader["ContentData"].ToString(),
                    ContentLength = int.Parse(sqlDataReader["ContentLength"].ToString()),
                    ContentType = sqlDataReader["ContentType"].ToString(),
                    DocumentCategoryName = sqlDataReader["DocumentCategoryName"].ToString(),
                    DocumentDesc = sqlDataReader["DocumentDesc"].ToString(),
                    DocumentStatusId = (StatusEnum)int.Parse(sqlDataReader["DocumentStatusId"].ToString()),
                    DocumentTypeDesc = sqlDataReader["DocumentTypeDesc"].ToString(),
                    DocumentTypeId = (DocumentTypeEnum)int.Parse(sqlDataReader["DocumentTypeId"].ToString()),
                    FileExtension = sqlDataReader["FileExtension"].ToString(),
                    Height = int.Parse(sqlDataReader["Height"].ToString()),
                    HeightUnit = sqlDataReader["HeightUnit"].ToString(),
                    ServerFileName = sqlDataReader["ServerFileName"].ToString(),
                    Width = int.Parse(sqlDataReader["Width"].ToString()),
                    WidthUnit = sqlDataReader["WidthUnit"].ToString(),
                };
            }
            sqlDataReader.Close();
            return documentModel;
        }
        public static DocumentModel GetDocument(string serverFileName, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM ArchLib.Document WHERE ServerFileName = '" + serverFileName + "'", sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            DocumentModel documentModel = null;
            if (sqlDataReader.Read())
            {
                documentModel = new DocumentModel
                {
                    DocumentId = long.Parse(sqlDataReader["DocumentId"].ToString()),
                    ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                    ClientFileName = sqlDataReader["ClientFileName"].ToString(),
                    ClientHeight = int.Parse(sqlDataReader["ClientHeight"].ToString()),
                    ClientHeightUnit = sqlDataReader["ClientHeightUnit"].ToString(),
                    ClientWidth = int.Parse(sqlDataReader["ClientWidth"].ToString()),
                    ClientWidthUnit = sqlDataReader["ClientWidthUnit"].ToString(),
                    ContentByteData = null,
                    ContentData = sqlDataReader["ContentData"].ToString(),
                    ContentLength = int.Parse(sqlDataReader["ContentLength"].ToString()),
                    ContentType = sqlDataReader["ContentType"].ToString(),
                    DocumentCategoryName = sqlDataReader["DocumentCategoryName"].ToString(),
                    DocumentDesc = sqlDataReader["DocumentDesc"].ToString(),
                    DocumentStatusId = (StatusEnum)int.Parse(sqlDataReader["DocumentStatusId"].ToString()),
                    DocumentTypeDesc = sqlDataReader["DocumentTypeDesc"].ToString(),
                    DocumentTypeId = (DocumentTypeEnum)int.Parse(sqlDataReader["DocumentTypeId"].ToString()),
                    FileExtension = sqlDataReader["FileExtension"].ToString(),
                    Height = int.Parse(sqlDataReader["Height"].ToString()),
                    HeightUnit = sqlDataReader["HeightUnit"].ToString(),
                    ServerFileName = sqlDataReader["ServerFileName"].ToString(),
                    Width = int.Parse(sqlDataReader["Width"].ToString()),
                    WidthUnit = sqlDataReader["WidthUnit"].ToString(),
                };
            }
            sqlDataReader.Close();
            return documentModel;
        }
    }
}
