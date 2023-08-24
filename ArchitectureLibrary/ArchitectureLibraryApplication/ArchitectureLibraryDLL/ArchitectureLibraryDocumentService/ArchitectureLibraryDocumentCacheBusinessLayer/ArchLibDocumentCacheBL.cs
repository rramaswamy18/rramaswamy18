using ArchitectureLibraryDocumentDataLayer;
using ArchitectureLibraryDocumentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryDocumentCacheBusinessLayer
{
    public class ArchLibDocumentCacheBL
    {
        public void Initialize(out DocumentModel documentModelNoImage, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            ArchLibDocumentDataContext.OpenSqlConnection();
            documentModelNoImage = ArchLibDocumentDataContext.GetDocument(0, ArchLibDocumentDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
            ArchLibDocumentDataContext.CloseSqlConnection();
        }
    }
}
