using ArchitectureLibraryBusinessLayer;
using ArchitectureLibraryCacheData;
using ArchitectureLibraryClassCode;
using ArchitectureLibraryEnumerations;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using RetailSlnBusinessLayer;
using RetailSlnCacheData;
using RetailSlnModels;
using RetailSlnWeb.ClassCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RetailSlnWeb.Controllers
{
    [ClassCode.AjaxAuthorize]
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
            //Session.Timeout = 1;
            ViewData["ActionName"] = "Index";
            return View();
        }

        // GET: CategoryHierList
        [HttpGet]
        public ActionResult CategoryHierList(string id)
        {
            ViewData["ActionName"] = "CategoryHierList";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request, lastIpAddress, ArchLibCache.IpInfoClientAccessToken), loggedInUserId = GetLoggedInUserId();
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
        [HttpGet]
        public ActionResult CategoryList(string id)
        {
            ViewData["ActionName"] = "CategoryList";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request, lastIpAddress, ArchLibCache.IpInfoClientAccessToken), loggedInUserId = GetLoggedInUserId();
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

        // GET: CorpAcct
        [HttpGet]
        public ActionResult CorpAcct(string id)
        {
            ViewData["ActionName"] = "CorpAcct";
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

        // POST: CorpAcct
        [HttpPost]
        public ActionResult CorpAcct(CorpAcctModel corpAcctModel)
        {
            //int x = 1, y = 0, z = x / y;
            ViewData["ActionName"] = "CorpAcct";
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
                ModelState.Clear();
                TryValidateModel(corpAcctModel);
                retailSlnBL.CorpAcct(ref corpAcctModel, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (ModelState.IsValid)
                {
                    success = true;
                    processMessage = "SUCCESS!!!";
                    CorpAcctListModel corpAcctListModel = retailSlnBL.CorpAcctList(null, null, clientId, ipAddress, execUniqueId, loggedInUserId);
                    htmlString = archLibBL.ViewToHtmlString(this, "_CorpAcctListData", corpAcctListModel);
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

        // GET: CorpAcctList
        [HttpGet]
        public ActionResult CorpAcctList(string pageNum, string rowCount)
        {
            ViewData["ActionName"] = "CorpAcctList";
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

        // GET: CorpAcctLocationList
        [HttpGet]
        public ActionResult CorpAcctLocationList(string id)
        {
            ViewData["ActionName"] = "CorpAcctLocation";
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

        // GET: CorpAcctLocation
        [HttpGet]
        public ActionResult CorpAcctLocation(string id, string corpAcctId)
        {
            ViewData["ActionName"] = "CorpAcctLocation";
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

        // POST: CorpAcctLocation
        [HttpPost]
        public ActionResult CorpAcctLocation(/*CorpAcctLocationModel corpAcctLocationModel*/FormCollection formCollection)
        {
            //int x = 1, y = 0, z = x / y;
            ViewData["ActionName"] = "CorpAcctLocation";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = GetLoggedInUserId();
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            CorpAcctLocationModel corpAcctLocationModel = new CorpAcctLocationModel();
            long tempLong;
            #region
            try
            {
                corpAcctLocationModel.AlternateTelephoneCountryId = long.TryParse(formCollection["AlternateTelephoneCountryId"], out tempLong) ? tempLong : (long?)null;
                corpAcctLocationModel.AlternateTelephoneNumber = long.TryParse(formCollection["AlternateTelephoneNumber"], out tempLong) ? tempLong : (long?)null;
                corpAcctLocationModel.CorpAcctId = long.Parse(formCollection["CorpAcctId"]);
                corpAcctLocationModel.DemogInfoAddressId = long.TryParse(formCollection["DemogInfoAddressId"], out tempLong) ? tempLong : (long?)null;
                corpAcctLocationModel.LocationName = formCollection["LocationName"];
                corpAcctLocationModel.PrimaryTelephoneCountryId = long.TryParse(formCollection["PrimaryTelephoneCountryId"], out tempLong) ? tempLong : (long?)null;
                corpAcctLocationModel.PrimaryTelephoneNumber = long.TryParse(formCollection["PrimaryTelephoneNumber"], out tempLong) ? tempLong : (long?)null;
                corpAcctLocationModel.StatusId = long.TryParse(formCollection["StatusId"], out tempLong) ? (YesNoEnum)tempLong : (YesNoEnum?)null;
                corpAcctLocationModel.DemogInfoAddressModel = new DemogInfoAddressModel
                {
                    AddressLine1 = formCollection["DemogInfoAddressModel.AddressLine1"],
                    AddressLine2 = formCollection["DemogInfoAddressModel.AddressLine2"],
                    AddressLine3 = formCollection["DemogInfoAddressModel.AddressLine3"],
                    BuildingTypeId = string.IsNullOrWhiteSpace(formCollection["DemogInfoAddressModel.BuildingTypeId"]) ? (BuildingTypeEnum?)null : (BuildingTypeEnum)long.Parse(formCollection["DemogInfoAddressModel.BuildingTypeId"]),
                    CityName = formCollection["DemogInfoAddressModel.CityName"],
                    ZipCode = formCollection["DemogInfoAddressModel.ZipCode"],
                    DemogInfoCountryId = string.IsNullOrWhiteSpace(formCollection["DemogInfoAddressModel.DemogInfoCountryId"]) ? (long?)null : long.Parse(formCollection["DemogInfoAddressModel.DemogInfoCountryId"]),
                    DemogInfoSubDivisionId = string.IsNullOrWhiteSpace(formCollection["DemogInfoAddressModel.DemogInfoSubDivisionId"]) ? (long?)null : long.Parse(formCollection["DemogInfoAddressModel.DemogInfoSubDivisionId"]),
                };
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
            }
            #endregion
            try
            {
                //int x = 1, y = 0, z = x / y;
                ModelState.Clear();
                #region
                if (string.IsNullOrWhiteSpace(corpAcctLocationModel.LocationName))
                {
                    ModelState.AddModelError("LocationName", "Enter location name");
                }
                if (corpAcctLocationModel.PrimaryTelephoneCountryId == null)
                {
                    ModelState.AddModelError("PrimaryTelephoneCountryId", "Select country");
                }
                if (corpAcctLocationModel.PrimaryTelephoneNumber == null)
                {
                    ModelState.AddModelError("PrimaryTelephoneNumber", "Enter 10 digit telephone number");
                }
                else
                {
                    if (corpAcctLocationModel.PrimaryTelephoneNumber.Value.ToString().Trim().Length != 10)
                    {
                        ModelState.AddModelError("", "Enter 10 digit telephone number");
                    }
                }
                if (corpAcctLocationModel.AlternateTelephoneCountryId == null)
                {
                    ModelState.AddModelError("AlternateTelephoneCountryId", "Select country");
                }
                if (corpAcctLocationModel.AlternateTelephoneNumber != null && corpAcctLocationModel.AlternateTelephoneNumber.Value.ToString().Trim().Length != 10)
                {
                    ModelState.AddModelError("AlternateTelephoneNumber", "Enter 10 digit telephone number");
                }
                if (corpAcctLocationModel.StatusId == null)
                {
                    ModelState.AddModelError("StatusId", "Select status");
                }
                if (corpAcctLocationModel.DemogInfoAddressModel.DemogInfoCountryId == null)
                {
                    ModelState.AddModelError("DemogInfoAddressModel.DemogInfoCountryId", "Select country");
                }
                if (corpAcctLocationModel.DemogInfoAddressModel.BuildingTypeId == null)
                {
                    ModelState.AddModelError("DemogInfoAddressModel.BuildingTypeId", "Select building type");
                }
                if (string.IsNullOrWhiteSpace(corpAcctLocationModel.DemogInfoAddressModel.AddressLine1))
                {
                    ModelState.AddModelError("DemogInfoAddressModel.AddressLine1", "Enter address line 1");
                }
                if (string.IsNullOrWhiteSpace(corpAcctLocationModel.DemogInfoAddressModel.ZipCode))
                {
                    ModelState.AddModelError("DemogInfoAddressModel.ZipCode", "Enter 6 digit PIN code");
                }
                else
                {
                    if (!(long.TryParse(corpAcctLocationModel.DemogInfoAddressModel.ZipCode, out tempLong) && corpAcctLocationModel.DemogInfoAddressModel.ZipCode.Trim().Length == 6))
                    {
                        ModelState.AddModelError("DemogInfoAddressModel.ZipCode", "Enter 6 digit PIN code");
                    }
                }
                if (string.IsNullOrWhiteSpace(corpAcctLocationModel.DemogInfoAddressModel.CityName))
                {
                    ModelState.AddModelError("DemogInfoAddressModel.CityName", "Enter city name");
                }
                if (corpAcctLocationModel.DemogInfoAddressModel.DemogInfoSubDivisionId == null)
                {
                    ModelState.AddModelError("DemogInfoAddressModel.DemogInfoSubDivisionId", "Select state");
                }
                #endregion
                //TryValidateModel(corpAcctLocationModel);
                //TryValidateModel(corpAcctLocationModel.DemogInfoAddressModel, "DemogInfoAddressModel");
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
        [HttpGet]
        public ActionResult ItemMasterList(string pageNum, string rowCount)
        {
            ViewData["ActionName"] = "ItemMasterList";
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
        [HttpGet]
        public ActionResult ItemMasterInfo(string id)
        {
            ViewData["ActionName"] = "ItemMasterInfo";
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
        [HttpPost]
        public ActionResult ItemMasterInfo(ItemMasterModel itemMasterModel)
        {
            ViewData["ActionName"] = "ItemMasterInfo";
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

        // GET: ItemHierList
        [HttpGet]
        public ActionResult ItemHierList(string id, string assignedPageNum, string assignedRowCount, string pageNum, string rowCount)
        {
            ViewData["ActionName"] = "ItemHierList";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = GetLoggedInUserId();
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

        // GET: OrderCreatedFor
        [HttpGet]
        public ActionResult OrderCreatedFor(string id, string locnId)
        {
            ViewData["ActionName"] = "OrderCreatedFor";
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
                long personId = long.Parse(id);
                long corpAcctLocationId = long.Parse(locnId);
                SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
                SessionObjectModel createForSessionObject = archLibBL.BuildSessionObject(personId, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                ApplSessionObjectModel applSessionObjectModel = retailSlnBL.LoginUserProf(createForSessionObject.PersonId, corpAcctLocationId, this, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                createForSessionObject.ApplSessionObjectModel = applSessionObjectModel;
                Session["CreateForSessionObject"] = createForSessionObject;
                PaymentInfo1Model paymentInfoModel = (PaymentInfo1Model)Session["PaymentInfo"];
                retailSlnBL.UpdateShoppingCart(ref paymentInfoModel, false, true, sessionObjectModel, createForSessionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
                int shoppingCartItemsCount = 0;
                string shoppingCartTotalAmount = 0f.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                shoppingCartTotalAmount = paymentInfoModel?.ShoppingCartModel?.ShoppingCartSummaryModel?.TotalOrderAmount.Value.ToString(RetailSlnCache.CurrencyDecimalPlaces, RetailSlnCache.CurrencyCultureInfo).Replace(" ", "");
                success = true;
                processMessage = "SUCCESS!!!";
                htmlString = "Order Created For Success";
                actionResult = Json(new { success, processMessage, htmlString, shoppingCartItemsCount, shoppingCartTotalAmount }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                processMessage = "ERROR???";
                htmlString = "Order Created For Failure";
                actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
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
                success = true;
                processMessage = "SUCCESS!!!";
                SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
                string aspNetRoleName;
                aspNetRoleName = sessionObjectModel.AspNetRoleName;
                var orderCategoryItemModel = retailSlnBL.OrderItem(aspNetRoleName, id, pageNum, rowCount, sessionObjectModel, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                htmlString = archLibBL.ViewToHtmlString(this, "_OrderItem2", orderCategoryItemModel);
                actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (ApplicationException applicationException)
            {
                actionResult = Json(new { success = false, errorCode = "RELOAD_PAGE", message = applicationException.Message }, JsonRequestBehavior.AllowGet);
                //return actionResult;
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
                actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            }
            //actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            return actionResult;
        }

        // GET: OrderItemData
        [HttpGet]
        public ActionResult OrderItemData(string id, string pageNum, string rowCount)
        {
            ViewData["ActionName"] = "OrderItemData";
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
                success = true;
                processMessage = "SUCCESS!!!";
                SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
                string aspNetRoleName;
                aspNetRoleName = sessionObjectModel.AspNetRoleName;
                var orderCategoryItemModel = retailSlnBL.OrderItem(aspNetRoleName, id, pageNum, rowCount, sessionObjectModel, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                htmlString = archLibBL.ViewToHtmlString(this, "_OrderItem3", orderCategoryItemModel);
                actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (ApplicationException applicationException)
            {
                actionResult = Json(new { success = false, errorCode = "RELOAD_PAGE", message = applicationException.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                ModelState.AddModelError("", "OrderItemData / GET");
                archLibBL.CopyReponseObjectToModelErrors(ModelState, null, responseObjectModel.ResponseMessages);
                success = false;
                processMessage = "ERROR???";
                htmlString = archLibBL.ViewToHtmlString(this, "Error", responseObjectModel);
                actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            }
            return actionResult;
        }

        // GET: OrderList
        [HttpGet]
        public ActionResult OrderList(string id, string rowCount)
        {
            ViewData["ActionName"] = "OrderList";
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
                success = true;
                processMessage = "SUCCESS!!!";
                SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
                SessionObjectModel createForSessionObjectModel = (SessionObjectModel)Session["CreateForSessionObject"];
                OrderListModel orderListModel = retailSlnBL.OrderList(id, rowCount, sessionObjectModel, createForSessionObjectModel, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                htmlString = archLibBL.ViewToHtmlString(this, "_OrderList", orderListModel);
                actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (ApplicationException applicationException)
            {
                actionResult = Json(new { success = false, errorCode = "RELOAD_PAGE", message = applicationException.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                ModelState.AddModelError("", "OrderList / GET");
                archLibBL.CopyReponseObjectToModelErrors(ModelState, null, responseObjectModel.ResponseMessages);
                success = false;
                processMessage = "ERROR???";
                htmlString = archLibBL.ViewToHtmlString(this, "Error", responseObjectModel);
                actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            }
            return actionResult;
        }

        // GET: OrderListView
        [HttpGet]
        public ActionResult OrderListView(string id, string invoiceTypeId)
        {
            ViewData["ActionName"] = "OrderListView";
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
                success = true;
                processMessage = "SUCCESS!!!";
                htmlString = archLibBL.ViewToHtmlString(this, "_OrderListViewData", (object)(id + ";" + invoiceTypeId));
                actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (ApplicationException applicationException)
            {
                actionResult = Json(new { success = false, errorCode = "RELOAD_PAGE", message = applicationException.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ResponseObjectModel responseObjectModel = archLibBL.CreateSystemError(clientId, ipAddress, execUniqueId, loggedInUserId);
                ModelState.AddModelError("", "OrderList / GET");
                archLibBL.CopyReponseObjectToModelErrors(ModelState, null, responseObjectModel.ResponseMessages);
                success = false;
                processMessage = "ERROR???";
                htmlString = archLibBL.ViewToHtmlString(this, "Error", responseObjectModel);
                actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            }
            return actionResult;
        }

        // GET: SearchKeywordList
        [HttpGet]
        public ActionResult SearchKeywordList(string pageNum, string rowCount)
        {
            ViewData["ActionName"] = "SearchKeywordList";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = GetLoggedInUserId();
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

        // GET: UserAddEdit
        [HttpGet]
        public ActionResult UserAddEdit(string id)
        {
            ViewData["ActionName"] = "UserAddEdit";
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
                success = true;
                processMessage = "SUCCESS!!!";
                UserAddEditModel userAddEditModel = retailSlnBL.UserAddEdit(id, clientId, ipAddress, execUniqueId, loggedInUserId);
                htmlString = archLibBL.ViewToHtmlString(this, "_UserAddEdit", userAddEditModel);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                success = false;
                processMessage = "ERROR???";
                htmlString = "Error while loading User for Add/Edit";
            }
            actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        // POST: UserAddEdit
        [HttpPost]
        public ActionResult UserAddEdit(UserAddEditModel userAddEditModel)
        {
            ViewData["ActionName"] = "UserAddEdit";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = "";
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            //int x = 1, y = 0, z = x / y;
            try
            {
                //int a1 = 1, a2 = 0, a3 = a1 / a2;
                ModelState.Clear();
                TryValidateModel(userAddEditModel);
                if (ModelState.IsValid)
                {
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Valid Model");
                    SessionObjectModel sessionObjectModel = (SessionObjectModel)Session["SessionObject"];
                    SessionObjectModel createForSessionObject = (SessionObjectModel)Session["CreateForSessionObject"];
                    retailSlnBL.UserAddEdit(ref userAddEditModel, this, sessionObjectModel, createForSessionObject, Session, ModelState, clientId, ipAddress, execUniqueId, loggedInUserId);
                    if (ModelState.IsValid)
                    {
                        UserListModel userListModel = retailSlnBL.UserList("1", "45", clientId, ipAddress, execUniqueId, loggedInUserId);
                        success = true;
                        processMessage = "SUCCESS!!!";
                        htmlString = archLibBL.ViewToHtmlString(this, "_UserList", userListModel);
                    }
                    else
                    {
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Error while creating User");
                        userAddEditModel.ResponseObjectModel = new ResponseObjectModel
                        {
                            ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                        };
                        success = false;
                        processMessage = "ERROR???";
                        htmlString = archLibBL.ViewToHtmlString(this, "_UserAddEditData", userAddEditModel);
                    }
                }
                else
                {
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: Model Errors");
                    userAddEditModel.ResponseObjectModel = new ResponseObjectModel
                    {
                        ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                    };
                    success = false;
                    processMessage = "ERROR???";
                    htmlString = archLibBL.ViewToHtmlString(this, "_UserAddEditData", userAddEditModel);
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                ModelState.AddModelError("", "Error occurred while saving User");
                userAddEditModel.ResponseObjectModel = new ResponseObjectModel
                {
                    ValidationSummaryMessage = ArchLibCache.ValidationSummaryMessageFixErrors,
                };
                success = false;
                processMessage = "ERROR???";
                htmlString = archLibBL.ViewToHtmlString(this, "_UserAddEditData", userAddEditModel);
            }
            actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        // GET: UserList
        [HttpGet]
        public ActionResult UserList(string pageNum, string rowCount)
        {
            ViewData["ActionName"] = "UserList";
            string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(Request), loggedInUserId = GetLoggedInUserId();
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            RetailSlnBL retailSlnBL = new RetailSlnBL();
            ActionResult actionResult;
            bool success;
            string processMessage, htmlString;
            UserListModel userListModel = retailSlnBL.UserList(pageNum, rowCount, clientId, ipAddress, execUniqueId, loggedInUserId);
            success = true;
            processMessage = "SUCCESS!!!";
            htmlString = archLibBL.ViewToHtmlString(this, "_UserList", userListModel);
            actionResult = Json(new { success, processMessage, htmlString }, JsonRequestBehavior.AllowGet);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return actionResult;
        }

        //private ActionResult AbandonSession()
        //{
        //    FormsAuthentication.SignOut();
        //    Session.Abandon();
        //    Request.GetOwinContext().Authentication.SignOut();
        //    Session["SessionObject"] = null;
        //    Session.Abandon();
        //    return RedirectToAction("LoginUserProf", "Home");
        //}

        // Private: GetLoggedInUserId
        private string GetLoggedInUserId()
        {
            return ((SessionObjectModel)Session["SessionObject"]).AspNetUserId;
        }

        //private bool UserIsAuthenticated()
        //{
        //    bool isAuthenticated;
        //    isAuthenticated = (User.Identity.IsAuthenticated && (SessionObjectModel)Session["SessionObject"] != null);
        //    return isAuthenticated;
        //}
    }
}
