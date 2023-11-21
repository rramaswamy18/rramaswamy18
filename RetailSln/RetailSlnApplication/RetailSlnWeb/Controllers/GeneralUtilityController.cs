using ArchitectureLibraryBusinessLayer;
using ArchitectureLibraryCacheData;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using RetailSlnCacheData;
using RetailSlnModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace RetailSlnWeb.Controllers
{
    [OutputCache(Duration = 0, NoStore = true, VaryByParam = "*")]
    public class GeneralUtilityController : Controller
    {
        private readonly long clientId = long.Parse(Utilities.GetApplicationValue("ClientId"));
        private readonly string execUniqueId = Utilities.CreateExecUniqueId();
        private readonly string lastIpAddress = Utilities.GetLastIPAddress();

        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult DemongInfoStates(string id)
        {
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                processMessage = "SUCCESS!!!";
                success = true;
                htmlString = archLibBL.ViewToHtmlString(this, "_DemongInfoStates", id);
            }
            catch
            {
                processMessage = "ERROR???";
                success = false;
                htmlString = "Error while loading states for country id = " + id;
            }
            actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            return actionResult;
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult SearchData(SearchDataModel searchDataModel, string execUniqueId)
        {
            ViewData["ActionName"] = "Index";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request, lastIpAddress, ArchLibCache.IpInfoClientAccessToken), loggedInUserId = Utilities.GetLoggedInUserId(Session);
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL generalUtilityBL = new ArchLibBL();
            List<Dictionary<string, string>> sqlQueryResults = generalUtilityBL.SearchData(searchDataModel, clientId, ipAddress, execUniqueId, loggedInUserId);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");

            return Json(sqlQueryResults, JsonRequestBehavior.AllowGet);
        }
    }
}
