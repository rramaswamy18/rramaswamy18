using ArchitectureLibraryBusinessLayer;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using RetailSlnBusinessLayer;
using RetailSlnWeb.ClassCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace RetailSlnWeb.Controllers
{
    [AjaxAuthorize]
    [Authorize]
    [OutputCache(Duration = 0, NoStore = true, VaryByParam = "*")]
    public class DashboardController : Controller
    {
        private readonly long clientId = long.Parse(Utilities.GetApplicationValue("ClientId"));
        private readonly string execUniqueId = Utilities.CreateExecUniqueId();
        private readonly string lastIpAddress = Utilities.GetLastIPAddress();

        // GET: Dashboard
        [HttpGet]
        public ActionResult Index()
        {
            ActionResult actionResult;
            ViewData["ActionName"] = "Index";
            if (ValidateLoggedInAspNeRole())
            {
                actionResult = View();
            }
            else
            {
                actionResult = RedirectToAction("Index", "Home");
            }
            return actionResult;
        }

        // GET: OrderItem
        [HttpGet]
        public ActionResult OrderItem(string id, string pageNum, string rowCount)
        {
            ViewData["ActionName"] = "OrderItem";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = GetLoggedInUserId();
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            try
            {
                //int x = 1, y = 0, z = x / y;
                SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
                SessionObjectModel createForSessionObject = (SessionObjectModel)Session["CreateForSessionObject"];
                string aspNetRoleName;
                aspNetRoleName = sessionObjectModel.AspNetRoleName;
                var orderCategoryItemModel = retailSlnBL.OrderItem(aspNetRoleName, id, pageNum, "45", sessionObjectModel, createForSessionObject, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                success = true;
                processMessage = "SUCCESS!!!";
                htmlString = archLibBL.ViewToHtmlString(this, "_OrderItem2", orderCategoryItemModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                ModelState.AddModelError("", "OrderItem / GET");
                archLibBL.CopyReponseObjectToModelErrors(ModelState, null, responseObjectModel.ResponseMessages);
                success = false;
                processMessage = "ERROR???";
                htmlString = archLibBL.ViewToHtmlString(this, "Error", responseObjectModel);
            }
            actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            return actionResult;
        }

        // PRIVATE: GetLoggedInUserId
        private string GetLoggedInUserId()
        {
            return ((SessionObjectModel)Session["SessionObject"]).AspNetUserId;
        }

        private string GetLoggedInUserAspNetRole()
        {
            return ((SessionObjectModel)Session["SessionObject"]).AspNetRoleName;
        }
        
        private bool ValidateLoggedInAspNeRole()
        {
            var aspNetRoleName = GetLoggedInUserAspNetRole();
            if (aspNetRoleName == "DEFAULTROLE")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
