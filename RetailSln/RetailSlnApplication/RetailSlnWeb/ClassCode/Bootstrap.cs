using ArchitectureLibraryCacheData;
using ArchitectureLibraryDocumentCacheData;
using ArchitectureLibraryException;
using ArchitectureLibraryMenuCacheData;
using ArchitectureLibraryUtility;
using RetailSlnCacheData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace RetailSlnWeb.ClassCode
{
    public static class Bootstrap
    {
        public static void Initialize(HttpContext httpContextCurrent)
        {
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = "", loggedInUserId = "";
            string execUniqueId = "WebBootstrap";
            Utilities.Initialize();
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, "WebBootstrap", loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.Initialize();
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            long clientId = long.Parse(Utilities.GetApplicationValue("ClientId"));
            ArchLibCache.Initialize(clientId, ipAddress, execUniqueId, loggedInUserId);
            ArchLibMenuCache.Initialize(clientId, ipAddress, execUniqueId, loggedInUserId);
            ArchLibDocumentCache.Initialize(clientId, ipAddress, execUniqueId, loggedInUserId);
            LookupCache.Initialize(clientId, ipAddress, execUniqueId, loggedInUserId);
            DemogInfoCache.Initialize(clientId, ipAddress, execUniqueId, loggedInUserId);
            RetailSlnCache.Initialize(clientId, ipAddress, execUniqueId, loggedInUserId);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        }
    }
}
