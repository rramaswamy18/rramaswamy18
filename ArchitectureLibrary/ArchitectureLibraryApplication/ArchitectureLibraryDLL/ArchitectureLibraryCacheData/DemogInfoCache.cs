using ArchitectureLibraryCacheBusinessLayer;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArchitectureLibraryCacheData
{
    public static class DemogInfoCache
    {
        public static List<DemogInfoCountryModel> DemogInfoCountryModels { set; get; }
        public static List<DemogInfoSubDivisionModel> DemogInfoSubDivisionModels { set; get; }
        public static List<SelectListItem> DemogInfoCountrySelectListItems { set; get; }
        public static Dictionary<long, List<SelectListItem>> DemogInfoSubDivisionSelectListItems { set; get; }
        public static string DemogInfoCountryOptionTags { set; get; }
        public static Dictionary<long, string> DemogInfoSubDivisionOptionTags { set; get; }
        public static void Initialize(long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            DemogInfoCacheBL demogInfoCacheBL = new DemogInfoCacheBL();
            demogInfoCacheBL.Initialize(out List<DemogInfoCountryModel> demogInfoCountryModels, out List<DemogInfoSubDivisionModel> demogInfoSubDivisionModels, out List<SelectListItem> demogInfoCountrySelectListItems, out Dictionary<long, List<SelectListItem>> demogInfoSubDivisionSelectListItems, out string demogInfoCountryOptionTags, out Dictionary<long, string> demogInfoSubDivisionOptionTags, execUniqueId);
            DemogInfoCountryModels = demogInfoCountryModels;
            DemogInfoSubDivisionModels = demogInfoSubDivisionModels;
            //BuildDemogInfoSelectListItems(demogInfoCountryModels, out List<SelectListItem> demogInfoCountrySelectListItems, out Dictionary<long, List<SelectListItem>> demogInfoSubDivisionSelectListItems, execUniqueId);
            DemogInfoCountrySelectListItems = demogInfoCountrySelectListItems;
            DemogInfoSubDivisionSelectListItems = demogInfoSubDivisionSelectListItems;
            DemogInfoCountryOptionTags = demogInfoCountryOptionTags;
            DemogInfoSubDivisionOptionTags = demogInfoSubDivisionOptionTags;
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        }
        //private static void BuildDemogInfoSelectListItems(List<DemogInfoCountryModel> demogInfoCountryModels, out List<SelectListItem> demogInfoCountrySelectListItems, out Dictionary<long, List<SelectListItem>> demogInfoCountrySubDivisionSelectListItems, string execUniqueId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    demogInfoCountrySelectListItems = new List<SelectListItem>();
        //    demogInfoCountrySubDivisionSelectListItems = new Dictionary<long, List<SelectListItem>>();
        //    List<SelectListItem> demogInfoSubDivisionSelectListItems;
        //    foreach (var demogInfoCountryModel in demogInfoCountryModels)
        //    {
        //        demogInfoCountrySelectListItems.Add
        //        (
        //            new SelectListItem
        //            {
        //                Text = demogInfoCountryModel.CountryAbbrev,
        //                Value = demogInfoCountryModel.DemogInfoCountryId.ToString(),
        //            }
        //        );
        //        demogInfoSubDivisionSelectListItems = new List<SelectListItem>();
        //        demogInfoCountrySubDivisionSelectListItems[demogInfoCountryModel.DemogInfoCountryId] = demogInfoSubDivisionSelectListItems;
        //        foreach (var demogInfoSubDivisionModel in demogInfoCountryModel.DemogInfoSubDivisionModels)
        //        {
        //            demogInfoSubDivisionSelectListItems.Add
        //            (
        //                new SelectListItem
        //                {
        //                    Text = demogInfoSubDivisionModel.StateAbbrev,
        //                    Value = demogInfoSubDivisionModel.DemogInfoSubDivisionId.ToString(),
        //                }
        //            );
        //        }
        //    }
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //    return;
        //}
        //private static Dictionary<string, List<SelectListItem>> BuildDemogInfoSubDivisionSelectListItems(List<DemogInfoCountryModel> demogInfoCountryModels, string execUniqueId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    Dictionary<string, List<SelectListItem>> demogInfoCountrySelectListItems = new Dictionary<string, List<SelectListItem>>();
        //    List<SelectListItem> demogInfoSubDivisionSelectListItems;
        //    foreach (var demogInfoCountryModel in demogInfoCountryModels)
        //    {
        //        demogInfoSubDivisionSelectListItems = new List<SelectListItem>();
        //        demogInfoCountrySelectListItems[demogInfoCountryModel.CountryAbbrev] = demogInfoSubDivisionSelectListItems;
        //        foreach (var demogInfoSubDivisionModel in demogInfoCountryModel.DemogInfoSubDivisionModels)
        //        {
        //            demogInfoSubDivisionSelectListItems.Add
        //            (
        //                new SelectListItem
        //                {
        //                    Text = demogInfoSubDivisionModel.StateAbbrev,
        //                    Value = demogInfoSubDivisionModel.DemogInfoSubDivisionId.ToString(),
        //                }
        //            );
        //        }
        //    }
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //    return demogInfoCountrySelectListItems;
        //}
    }
}
