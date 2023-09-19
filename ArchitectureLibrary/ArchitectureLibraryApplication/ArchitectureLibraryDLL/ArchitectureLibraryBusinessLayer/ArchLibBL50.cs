using ArchitectureLibraryCacheData;
using ArchitectureLibraryEmailService;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.IO;

namespace ArchitectureLibraryBusinessLayer
{
    public partial class ArchLibBL
    {
        public List<Dictionary<string, string>> SearchData(SearchDataModel searchDataModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<Dictionary<string, string>> sqlQueryResults;
            switch (searchDataModel.SearchType.ToUpper())
            {
                case "ZIPCODE":
                    sqlQueryResults = SearchZipCode(long.Parse(searchDataModel.SearchKeyValuePairs["DemogInfoCountryId"]), searchDataModel.SearchKeyValuePairs["ZipCode"], clientId, ipAddress, execUniqueId, loggedInUserId);
                    break;
                case "CITYNAME":
                    sqlQueryResults = SearchCityName(long.Parse(searchDataModel.SearchKeyValuePairs["DemogInfoCountryId"]), searchDataModel.SearchKeyValuePairs["CityName"], clientId, ipAddress, execUniqueId, loggedInUserId);
                    break;
                default:
                    sqlQueryResults = null;
                    break;
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return sqlQueryResults;
        }
        public List<Dictionary<string, string>> SearchZipCode(long demogInfoCountryId, string zipCode, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            string sqlStmt = "";
            sqlStmt += "        SELECT TOP 25" + Environment.NewLine;
            sqlStmt += "               DemogInfoCountry.DemogInfoCountryId" + Environment.NewLine;
            sqlStmt += "              ,DemogInfoCountry.CountryAbbrev" + Environment.NewLine;
            sqlStmt += "              ,DemogInfoSubDivision.DemogInfoSubDivisionId" + Environment.NewLine;
            sqlStmt += "              ,DemogInfoSubDivision.StateAbbrev" + Environment.NewLine;
            sqlStmt += "              ,DemogInfoCounty.DemogInfoCountyId" + Environment.NewLine;
            sqlStmt += "              ,DemogInfoCounty.CountyName" + Environment.NewLine;
            sqlStmt += "              ,DemogInfoCity.DemogInfoCityId" + Environment.NewLine;
            sqlStmt += "              ,DemogInfoCity.CityName" + Environment.NewLine;
            sqlStmt += "              ,DemogInfoZip.DemogInfoZipId" + Environment.NewLine;
            sqlStmt += "              ,DemogInfoZip.ZipCode" + Environment.NewLine;
            sqlStmt += "              ,DemogInfoZipPlus.DemogInfoZipPlusId" + Environment.NewLine;
            sqlStmt += "          FROM ArchLib.DemogInfoCountry" + Environment.NewLine;
            sqlStmt += "    INNER JOIN ArchLib.DemogInfoSubDivision" + Environment.NewLine;
            sqlStmt += "            ON DemogInfoCountry.DemogInfoCountryId = DemogInfoSubDivision.DemogInfoCountryId" + Environment.NewLine;
            sqlStmt += "    INNER JOIN ArchLib.DemogInfoCounty" + Environment.NewLine;
            sqlStmt += "            ON DemogInfoSubDivision.DemogInfoSubDivisionId = DemogInfoCounty.DemogInfoSubDivisionId" + Environment.NewLine;
            sqlStmt += "    INNER JOIN ArchLib.DemogInfoCity" + Environment.NewLine;
            sqlStmt += "            ON DemogInfoCounty.DemogInfoCountyId = DemogInfoCity.DemogInfoCountyId" + Environment.NewLine;
            sqlStmt += "    INNER JOIN ArchLib.DemogInfoZip" + Environment.NewLine;
            sqlStmt += "            ON DemogInfoCity.DemogInfoCityId = DemogInfoZip.DemogInfoCityId" + Environment.NewLine;
            sqlStmt += "    INNER JOIN ArchLib.DemogInfoZipPlus" + Environment.NewLine;
            sqlStmt += "            ON DemogInfoZip.DemogInfoZipId = DemogInfoZipPlus.DemogInfoZipId" + Environment.NewLine;
            sqlStmt += "         WHERE DemogInfoZip.ZipCode LIKE '" + zipCode + "%'" + Environment.NewLine;
            sqlStmt += "           AND DemogInfoZipPlus.ZipPlus4 = ''" + Environment.NewLine;
            sqlStmt += "           AND DemogInfoCountry.DemogInfoCountryId = " + demogInfoCountryId + Environment.NewLine;
            sqlStmt += "      ORDER BY DemogInfoZip.ZipCode" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = sqlStmt;
            sqlCommand.CommandType = CommandType.Text;
            List<Dictionary<string, string>> searchResultRows;
            try
            {
                searchResultRows = null;//ApplicationDataContext.GetSqlQueryResult(sqlCommand);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }

            return searchResultRows;
        }
        public List<Dictionary<string, string>> SearchCityName(long demogInfoCountryId, string cityName, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            string sqlStmt = "";
            sqlStmt += "" + Environment.NewLine;
            sqlStmt += "        SELECT TOP 25" + Environment.NewLine;
            sqlStmt += "               DemogInfoCountry.DemogInfoCountryId" + Environment.NewLine;
            sqlStmt += "              ,DemogInfoCountry.CountryAbbrev" + Environment.NewLine;
            sqlStmt += "              ,DemogInfoSubDivision.DemogInfoSubDivisionId" + Environment.NewLine;
            sqlStmt += "              ,DemogInfoSubDivision.StateAbbrev" + Environment.NewLine;
            sqlStmt += "              ,DemogInfoCounty.DemogInfoCountyId" + Environment.NewLine;
            sqlStmt += "              ,DemogInfoCounty.CountyName" + Environment.NewLine;
            sqlStmt += "              ,DemogInfoCity.DemogInfoCityId" + Environment.NewLine;
            sqlStmt += "              ,DemogInfoCity.CityName" + Environment.NewLine;
            sqlStmt += "              ,DemogInfoZip.DemogInfoZipId" + Environment.NewLine;
            sqlStmt += "              ,DemogInfoZip.ZipCode" + Environment.NewLine;
            sqlStmt += "              ,DemogInfoZipPlus.DemogInfoZipPlusId" + Environment.NewLine;
            sqlStmt += "          FROM ArchLib.DemogInfoCountry" + Environment.NewLine;
            sqlStmt += "    INNER JOIN ArchLib.DemogInfoSubDivision" + Environment.NewLine;
            sqlStmt += "            ON DemogInfoCountry.DemogInfoCountryId = DemogInfoSubDivision.DemogInfoCountryId" + Environment.NewLine;
            sqlStmt += "    INNER JOIN ArchLib.DemogInfoCounty" + Environment.NewLine;
            sqlStmt += "            ON DemogInfoSubDivision.DemogInfoSubDivisionId = DemogInfoCounty.DemogInfoSubDivisionId" + Environment.NewLine;
            sqlStmt += "    INNER JOIN ArchLib.DemogInfoCity" + Environment.NewLine;
            sqlStmt += "            ON DemogInfoCounty.DemogInfoCountyId = DemogInfoCity.DemogInfoCountyId" + Environment.NewLine;
            sqlStmt += "    INNER JOIN ArchLib.DemogInfoZip" + Environment.NewLine;
            sqlStmt += "            ON DemogInfoCity.DemogInfoCityId = DemogInfoZip.DemogInfoCityId" + Environment.NewLine;
            sqlStmt += "    INNER JOIN ArchLib.DemogInfoZipPlus" + Environment.NewLine;
            sqlStmt += "            ON DemogInfoZip.DemogInfoZipId = DemogInfoZipPlus.DemogInfoZipId" + Environment.NewLine;
            sqlStmt += "         WHERE DemogInfoCity.CityName LIKE '" + cityName + "%'" + Environment.NewLine;
            sqlStmt += "           AND DemogInfoZipPlus.ZipPlus4 = ''" + Environment.NewLine;
            sqlStmt += "           AND DemogInfoCountry.DemogInfoCountryId = " + demogInfoCountryId + Environment.NewLine;
            //sqlStmt += "           AND DemogInfoZip.ZipCode <> ''" + Environment.NewLine;
            sqlStmt += "      ORDER BY DemogInfoCity.CityName" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = sqlStmt;
            sqlCommand.CommandType = CommandType.Text;
            //Add Parameters and restrict the query to the country passed
            List<Dictionary<string, string>> searchResultRows = new List<Dictionary<string, string>>();
            searchResultRows = null; //ApplicationDataContext.GetSqlQueryResult(sqlCommand);
            return searchResultRows;
        }
        public List<Dictionary<string, string>> SearchDataZipCodeCityName(long demogInfoCountryId, string zipCode, string cityName, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                List<Dictionary<string, string>> sqlQueryResults;
                if (string.IsNullOrWhiteSpace(zipCode) && string.IsNullOrWhiteSpace(cityName))
                {
                    sqlQueryResults = new List<Dictionary<string, string>>();
                }
                if (!string.IsNullOrWhiteSpace(zipCode))
                {
                    sqlQueryResults = SearchZipCode(demogInfoCountryId, zipCode, clientId, ipAddress, execUniqueId, loggedInUserId);
                    if (sqlQueryResults.Count == 1)
                    {
                        ;
                    }
                    else
                    {
                        sqlQueryResults = SearchCityName(demogInfoCountryId, cityName, clientId, ipAddress, execUniqueId, loggedInUserId);
                        if (sqlQueryResults.Count == 0)
                        {
                            sqlQueryResults = new List<Dictionary<string, string>>();
                        }
                        else
                        {
                            sqlQueryResults.RemoveRange(1, sqlQueryResults.Count - 1);
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(cityName))
                    {
                        sqlQueryResults = SearchCityName(demogInfoCountryId, cityName, clientId, ipAddress, execUniqueId, loggedInUserId);
                        if (sqlQueryResults.Count == 0)
                        {
                            sqlQueryResults = new List<Dictionary<string, string>>();
                        }
                        else
                        {
                            sqlQueryResults.RemoveRange(1, sqlQueryResults.Count - 1);
                        }
                    }
                    else
                    {
                        sqlQueryResults = new List<Dictionary<string, string>>();
                    }
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return sqlQueryResults;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Error", exception);
                throw;
            }
        }
        public void SendEmail(string toEmailAddress, string emailSubjectText, string emailBodyHtml, List<string> emailAttachmentFileNames, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                EmailService emailService = new EmailService();
                string privateKey = ArchLibCache.GetPrivateKey(clientId);
                string emailDirectoryName = Utilities.GetApplicationValue("EmailDirectoryName");
                //string documentsImagesDirectoryName = Utilities.GetApplicationValue("DocumentsImagesDirectoryName");
                var fromEmailAddress = new KeyValuePair<string, string>(ArchLibCache.GetApplicationDefault(clientId, "FromEmailAddress", ""), ArchLibCache.GetApplicationDefault(clientId, "FromEmailAddressDisplayName", ""));
                var toEmailAddresses = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>(toEmailAddress, ""),
                };
                List<KeyValuePair<string, string>> ccEmailAddresses = new List<KeyValuePair<string, string>>
                {
                    fromEmailAddress,
                };
                List<KeyValuePair<string, string>> bccEmailAddresses = new List<KeyValuePair<string, string>>();
                try
                {
                    bccEmailAddresses.Add(new KeyValuePair<string, string>(ArchLibCache.GetApplicationDefault(clientId, "BccEmailAddress", ""), ""));
                }
                catch
                {
                    ;
                }
                string emailServiceType;
                try
                {
                    emailServiceType = emailServiceType = Utilities.GetApplicationValue("EmailServiceType");//ConfigurationManager.AppSettings["EmailServiceType"];
                }
                catch
                {
                    emailServiceType = "";
                }
                GetSmtpValues(emailServiceType, out bool pickupDirectory, out string smtpClientHost, out bool? smtpClientEnableSsl, out int? smtpPort, out string networkUsername, out string networkPassword, clientId, ipAddress, execUniqueId, loggedInUserId);
                emailService.SendEmail(emailServiceType, emailDirectoryName, "", fromEmailAddress, emailSubjectText, emailBodyHtml, toEmailAddresses, ipAddress, execUniqueId, loggedInUserId, privateKey, null, ccEmailAddresses, bccEmailAddresses, emailAttachmentFileNames, pickupDirectory, smtpClientHost, smtpPort, smtpClientEnableSsl, networkUsername, networkPassword);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        private void GetSmtpValues(string emailServiceType, out bool pickupDirectory, out string smtpClientHost, out bool? smtpClientEnableSsl, out int? smtpPort , out string networkUsername, out string networkPassword, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            if (emailServiceType == "SMTP")
            {
                try
                {
                    pickupDirectory = bool.Parse(ArchLibCache.GetApplicationDefault(clientId, "SMTP", "PickupDirectory"));
                }
                catch
                {
                    pickupDirectory = false;
                }
                if (pickupDirectory)
                {
                    smtpClientHost = null;
                    smtpClientEnableSsl = null;
                    smtpPort = null;
                    networkUsername = null;
                    networkPassword = null;
                }
                else
                {
                    smtpClientHost = ArchLibCache.GetApplicationDefault(clientId, "SMTP", "SmtpClientHost");
                    smtpClientEnableSsl = bool.Parse(ArchLibCache.GetApplicationDefault(clientId, "SMTP", "SmtpClientEnableSsl"));
                    smtpPort = int.Parse(ArchLibCache.GetApplicationDefault(clientId, "SMTP", "SmtpPort"));
                    networkUsername = ArchLibCache.GetApplicationDefault(clientId, "SMTP", "NetworkUsername");
                    networkPassword = ArchLibCache.GetApplicationDefault(clientId, "SMTP", "NetworkPassword");
                }
            }
            else
            {
                pickupDirectory = false;
                smtpClientHost = null;
                smtpClientEnableSsl = null;
                smtpPort = null;
                networkUsername = null;
                networkPassword = null;
            }
        }
        public string ViewToHtmlString(Controller controller, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = controller.ControllerContext.RouteData.GetRequiredString("action");
            }
            controller.ViewData.Model = model;

            using (var stringWriter = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, stringWriter);
                viewResult.View.Render(viewContext, stringWriter);
                return stringWriter.GetStringBuilder().ToString();
            }
        }
        //public string GenerateHtmlStringFromView(Controller controller, string viewName, object model)
        //{
        //    if (string.IsNullOrEmpty(viewName))
        //    {
        //        viewName = controller.ControllerContext.RouteData.GetRequiredString("action");
        //    }
        //    controller.ViewData.Model = model;

        //    using (var stringWriter = new StringWriter())
        //    {
        //        var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
        //        var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, stringWriter);
        //        viewResult.View.Render(viewContext, stringWriter);
        //        return stringWriter.GetStringBuilder().ToString();
        //    }
        //}
        //public T CreateController<T>(RouteData routeData = null) where T : Controller, new()
        //{
        //    //Create a disconnected controller instance
        //    T controller = new T();
        //    //Get context wrapper from HttpContext if available
        //    HttpContextBase wrapper;
        //    if (HttpContext.Current != null)
        //    {
        //        wrapper = new HttpContextWrapper(HttpContext.Current);
        //    }
        //    else
        //    {
        //        throw new InvalidOperationException("Can't create Controller Context if no active HttpContext instance is available.");
        //    }
        //    if (routeData == null)
        //    {
        //        routeData = new RouteData();
        //    }
        //    // add the controller routing if not existing
        //    if (!routeData.Values.ContainsKey("controller") && !routeData.Values.ContainsKey("Controller"))
        //    {
        //        routeData.Values.Add("controller", controller.GetType().Name.ToLower().Replace("controller", ""));
        //    }
        //    controller.ControllerContext = new ControllerContext(wrapper, routeData, controller);
        //    return controller;
        //}
    }
}
//https://www.codemag.com/article/1312081/Rendering-ASP.NET-MVC-Razor-Views-to-String
