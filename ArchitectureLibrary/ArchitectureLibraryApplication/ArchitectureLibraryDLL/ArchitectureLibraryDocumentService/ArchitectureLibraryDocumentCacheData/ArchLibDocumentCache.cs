using ArchitectureLibraryDocumentCacheBusinessLayer;
using ArchitectureLibraryDocumentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryDocumentCacheData
{
    public static class ArchLibDocumentCache
    {
        public static DocumentModel DocumentModelNoImage { set; get; }
        public static void Initialize(long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            ArchLibDocumentCacheBL archLibDocumentCacheBL = new ArchLibDocumentCacheBL();
            archLibDocumentCacheBL.Initialize(out DocumentModel documentModelNoImage, clientId, ipAddress, execUniqueId, loggedInUserId);
            DocumentModelNoImage = documentModelNoImage;
        }
    }
}
