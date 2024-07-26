using ArchitectureLibraryBusinessLayer;
using ArchitectureLibraryClassCode;
using ArchitectureLibraryException;
using ArchitectureLibraryUtility;
using RetailSlnBusinessLayer;
using RetailSlnModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace RetailSlnWeb.Controllers
{
    public class DashboardController : Controller
    {
        private readonly long clientId = long.Parse(Utilities.GetApplicationValue("ClientId"));
        private readonly string execUniqueId = Utilities.CreateExecUniqueId();
        private readonly string lastIpAddress = Utilities.GetLastIPAddress();

        // GET: Dashboard
        //[AjaxAuthorize]
        //[Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // GET: CategoryHierList
        //[AjaxAuthorize]
        //[Authorize]
        [HttpGet]
        public ActionResult CategoryHierList(string id)
        {
            ViewData["ActionName"] = "CategoryHierList";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            CategoryItemHierListModel categoryHierListModel = retailSlnBL.CategoryHierList(long.Parse(id), clientId, ipAddress, execUniqueId, loggedInUserId);
            success = true;
            processMessage = "SUCCESS!!!";
            htmlString = archLibBL.ViewToHtmlString(this, "_CategoryHier", categoryHierListModel);
            actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        // GET: CategoryList
        //[AjaxAuthorize]
        //[Authorize]
        [HttpGet]
        public ActionResult CategoryList(string id)
        {
            ViewData["ActionName"] = "CategoryList";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            CategoryListModel categoryListModel = retailSlnBL.CategoryList(clientId, ipAddress, execUniqueId, loggedInUserId);
            success = true;
            processMessage = "SUCCESS!!!";
            htmlString = archLibBL.ViewToHtmlString(this, "_CategoryList", categoryListModel);
            actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        // GET: ItemHierList
        //[AjaxAuthorize]
        //[Authorize]
        [HttpGet]
        public ActionResult ItemHierList(string id, string assignedPageNum, string assignedRowCount, string pageNum, string rowCount)
        {
            ViewData["ActionName"] = "ItemHierList";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            CategoryItemHierListModel itemHierListModel = retailSlnBL.ItemHierList(long.Parse(id), int.Parse(assignedPageNum), int.Parse(assignedRowCount), int.Parse(pageNum), int.Parse(rowCount), clientId, ipAddress, execUniqueId, loggedInUserId);
            success = true;
            processMessage = "SUCCESS!!!";
            htmlString = archLibBL.ViewToHtmlString(this, "_ItemHier", itemHierListModel);
            actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        // GET: SearchKeywordList
        //[AjaxAuthorize]
        //[Authorize]
        [HttpGet]
        public ActionResult SearchKeywordList(string pageNum, string rowCount)
        {
            ViewData["ActionName"] = "SearchKeywordList";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            if (!int.TryParse(pageNum, out int pageNumInt))
            {
                pageNumInt = 1;
            }
            if (!int.TryParse(rowCount, out int rowCountInt))
            {
                rowCountInt = 50;
            }
            SearchKeywordListModel searchKeywordListModel = retailSlnBL.SearchKeywordList(pageNumInt, rowCountInt, clientId, ipAddress, execUniqueId, loggedInUserId);
            success = true;
            processMessage = "SUCCESS!!!";
            htmlString = archLibBL.ViewToHtmlString(this, "_SearchKeyword", searchKeywordListModel);
            actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }
    }
}
