using ArchitectureLibraryBusinessLayer;
using ArchitectureLibraryCacheData;
using ArchitectureLibraryClassCode;
using ArchitectureLibraryEnumerations;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using RetailSlnBusinessLayer;
using RetailSlnModels;
using RetailSlnWeb.ClassCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace RetailSlnWeb.Controllers
{
    [ClassCode.AjaxAuthorize]
    [Authorize]
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
            ViewData["ActionName"] = "Index";
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

        [HttpGet]
        public ActionResult CorpAcct(string id)
        {
            ViewData["ActionName"] = "CorpAcct";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            try
            {
                success = true;
                processMessage = "SUCCESS!!!";
                CorpAcctModel corpAcctModel = retailSlnBL.CorpAcct(id, clientId, ipAddress, execUniqueId, loggedInUserId);
                htmlString = archLibBL.ViewToHtmlString(this, "_CorpAcct", corpAcctModel);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                processMessage = "ERROR???";
                htmlString = "Error while loading Corp Acct";
            }
            actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        [HttpPost]
        public ActionResult CorpAcct(CorpAcctModel corpAcctModel)
        {
            //int x = 1, y = 0, z = x / y;
            ViewData["ActionName"] = "CorpAcct";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
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
                ModelState.Clear();
                TryValidateModel(corpAcctModel);
                retailSlnBL.CorpAcct(ref corpAcctModel, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (ModelState.IsValid)
                {
                    success = true;
                    processMessage = "SUCCESS!!!";
                    CorpAcctListModel corpAcctListModel = retailSlnBL.CorpAcctList(null, null, clientId, ipAddress, execUniqueId, loggedInUserId);
                    htmlString = archLibBL.ViewToHtmlString(this, "_CorpAcctList", corpAcctListModel);
                }
                else
                {
                    success = false;
                    processMessage = "ERROR???";
                    htmlString = archLibBL.ViewToHtmlString(this, "_CorpAcctData", corpAcctModel);
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                processMessage = "ERROR???";
                ModelState.AddModelError("", "Error while saving Corp Acct");
                htmlString = archLibBL.ViewToHtmlString(this, "_CorpAcctData", corpAcctModel);
            }
            actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        [HttpGet]
        public ActionResult CorpAcctList(string pageNum, string rowCount)
        {
            ViewData["ActionName"] = "CorpAcctList";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            try
            {
                success = true;
                processMessage = "SUCCESS!!!";
                CorpAcctListModel corpAcctListModel = retailSlnBL.CorpAcctList(pageNum, rowCount, clientId, ipAddress, execUniqueId, loggedInUserId);
                htmlString = archLibBL.ViewToHtmlString(this, "_CorpAcctList", corpAcctListModel);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                processMessage = "ERROR???";
                htmlString = "Error while loading Corp Acct List";
            }
            actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        [HttpGet]
        public ActionResult CorpAcctLocationList(string id)
        {
            ViewData["ActionName"] = "CorpAcctLocation";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            try
            {
                success = true;
                processMessage = "SUCCESS!!!";
                CorpAcctModel corpAcctModel = retailSlnBL.CorpAcct(id, clientId, ipAddress, execUniqueId, loggedInUserId);
                htmlString = archLibBL.ViewToHtmlString(this, "_CorpAcctLocationList", corpAcctModel);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                processMessage = "ERROR???";
                htmlString = "Error while loading Corp Acct Location List";
            }
            actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        [HttpGet]
        public ActionResult CorpAcctLocation(string id, string corpAcctId)
        {
            ViewData["ActionName"] = "CorpAcctLocation";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            try
            {
                success = true;
                processMessage = "SUCCESS!!!";
                CorpAcctLocationModel corpAcctLocationModel = retailSlnBL.CorpAcctLocation(id, corpAcctId, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                htmlString = archLibBL.ViewToHtmlString(this, "_CorpAcctLocation", corpAcctLocationModel);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                processMessage = "ERROR???";
                htmlString = "Error while loading Corp Acct Location";
            }
            actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        [HttpPost]
        public ActionResult CorpAcctLocation(/*CorpAcctLocationModel corpAcctLocationModel*/FormCollection formCollection)
        {
            //int x = 1, y = 0, z = x / y;
            ViewData["ActionName"] = "CorpAcctLocation";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            CorpAcctLocationModel corpAcctLocationModel = new CorpAcctLocationModel();
            try
            {
                long tempLong;
                corpAcctLocationModel.AlternateTelephoneCountryId = long.TryParse(formCollection["AlternateTelephoneCountryId"], out tempLong) ? (long?)null : long.Parse(formCollection["AlternateTelephoneCountryId"]);
                corpAcctLocationModel.AlternateTelephoneNumber = long.TryParse(formCollection["AlternateTelephoneNumber"], out tempLong) ? tempLong : (long?)null;
                corpAcctLocationModel.CorpAcctId = long.Parse(formCollection["CorpAcctId"]);
                corpAcctLocationModel.DemogInfoAddressId = long.TryParse(formCollection["DemogInfoAddressId"], out tempLong) ? tempLong : (long?)null;
                corpAcctLocationModel.LocationName = formCollection["LocationName"];
                corpAcctLocationModel.PrimaryTelephoneCountryId = long.TryParse(formCollection["PrimaryTelephoneCountryId"], out tempLong) ? tempLong : (long?)null;
                corpAcctLocationModel.PrimaryTelephoneNumber = long.TryParse(formCollection["PrimaryTelephoneNumber"], out tempLong) ? tempLong : (long?)null;
                corpAcctLocationModel.StatusId = long.TryParse(formCollection["StatusId"], out tempLong) ? (YesNoEnum)tempLong : (YesNoEnum?)null;
            }
            catch
            {
                ;
            }
            try
            {
                //int x = 1, y = 0, z = x / y;
                ModelState.Clear();
                TryValidateModel(corpAcctLocationModel);
                retailSlnBL.CorpAcctLocation(ref corpAcctLocationModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (ModelState.IsValid)
                {
                    success = true;
                    processMessage = "SUCCESS!!!";
                    CorpAcctModel corpAcctModel = retailSlnBL.CorpAcct(corpAcctLocationModel.CorpAcctId.ToString(), clientId, ipAddress, execUniqueId, loggedInUserId);
                    htmlString = archLibBL.ViewToHtmlString(this, "_CorpAcctLocationListData", corpAcctModel);
                }
                else
                {
                    success = false;
                    processMessage = "ERROR???";
                    htmlString = archLibBL.ViewToHtmlString(this, "_CorpAcctLocationData", corpAcctLocationModel);
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                processMessage = "ERROR???";
                ModelState.AddModelError("", "Error while saving Corp Acct");
                htmlString = archLibBL.ViewToHtmlString(this, "_CorpAcctLocationData", corpAcctLocationModel);
            }
            actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        // GET: ItemMasterList
        //[AjaxAuthorize]
        //[Authorize]
        [HttpGet]
        public ActionResult ItemMasterList(string pageNum, string rowCount)
        {
            ViewData["ActionName"] = "ItemMasterList";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            try
            {
                success = true;
                processMessage = "SUCCESS!!!";
                ItemMasterListModel itemMasterListModel = retailSlnBL.ItemMasterList(pageNum, rowCount, clientId, ipAddress, execUniqueId, loggedInUserId);
                htmlString = archLibBL.ViewToHtmlString(this, "_ItemMasterList", itemMasterListModel);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                processMessage = "ERROR???";
                htmlString = "Error while loading Item Master";
            }
            actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        // GET: ItemMasterInfo
        //[AjaxAuthorize]
        //[Authorize]
        [HttpGet]
        public ActionResult ItemMasterInfo(string id)
        {
            ViewData["ActionName"] = "ItemMasterInfo";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            try
            {
                success = true;
                processMessage = "SUCCESS!!!";
                ItemMasterModel itemMasterModel = retailSlnBL.ItemMasterInfo(id, clientId, ipAddress, execUniqueId, loggedInUserId);
                htmlString = archLibBL.ViewToHtmlString(this, "_ItemMasterInfo", itemMasterModel);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                processMessage = "ERROR???";
                htmlString = "Error while loading Item Master";
            }
            actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        // POST: ItemMasterInfo
        //[AjaxAuthorize]
        //[Authorize]
        [HttpPost]
        public ActionResult ItemMasterInfo(ItemMasterModel itemMasterModel)
        {
            ViewData["ActionName"] = "ItemMasterInfo";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            try
            {
                success = true;
                processMessage = "SUCCESS!!!";
                if (ModelState.IsValid)
                {
                    success = true;
                    processMessage = "SUCCESS!!!";
                    retailSlnBL.ItemMasterInfo(ref itemMasterModel, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    htmlString = archLibBL.ViewToHtmlString(this, "_ItemMasterInfo", itemMasterModel);
                }
                else
                {
                    success = false;
                    processMessage = "ERROR???";
                    itemMasterModel.ResponseObjectModel = new ResponseObjectModel
                    {
                        ResponseTypeId = ResponseTypeEnum.Error,
                        ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                    };
                    htmlString = archLibBL.ViewToHtmlString(this, "_ItemMasterInfo", itemMasterModel);
                }
                //retailSlnBL.ItemMasterInfo(ref itemMasterModel, clientId, ipAddress, execUniqueId, loggedInUserId);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                processMessage = "ERROR???";
                htmlString = "Error while loading Item Master";
            }
            actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        [HttpGet]
        public ActionResult OrderItem(string id, string pageNum, string rowCount)
        {
            ViewData["ActionName"] = "OrderItem";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
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
                success = true;
                processMessage = "SUCCESS!!!";
                SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
                string aspNetRoleName;
                if (sessionObjectModel == null)
                {
                    aspNetRoleName = "DEFAULTROLE";
                }
                else
                {
                    aspNetRoleName = sessionObjectModel.AspNetRoleName;
                }
                var orderCategoryItemModel = retailSlnBL.OrderCategoryItem(aspNetRoleName, id, pageNum, rowCount, sessionObjectModel, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                htmlString = archLibBL.ViewToHtmlString(this, orderCategoryItemModel.ViewName, orderCategoryItemModel);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (ApplicationException ex)
            {
                actionResult = Json(new { success = false, errorCode = "RELOAD_PAGE", message = ex.Message }, JsonRequestBehavior.AllowGet);
                return actionResult;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                ModelState.AddModelError("", "OrderCategoryItemData / GET");
                archLibBL.CopyReponseObjectToModelErrors(ModelState, null, responseObjectModel.ResponseMessages);
                success = false;
                processMessage = "ERROR???";
                htmlString = archLibBL.ViewToHtmlString(this, "Error", responseObjectModel);
            }
            actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
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
