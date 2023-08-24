using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace ArchitectureLibraryUtility
{
    public static class Utilities
    {
        #region Properties
        private static bool invalidEmailAddress_;
        private static SortedDictionary<string, string> ConfigDataAppSettings;
        private static SortedDictionary<string, string> ConfigDatabaseConnectionStrings;

        #endregion

        #region Config Data
        public static void Initialize()
        {
            int i;
            string configKey, configValue;
            ConfigDataAppSettings = new SortedDictionary<string, string>();
            for (i = 0; i < ConfigurationManager.AppSettings.Keys.Count; i++)
            {
                configKey = ConfigurationManager.AppSettings.Keys[i];
                configValue = ConfigurationManager.AppSettings[configKey];
                ConfigDataAppSettings[configKey] = configValue;
            }
            ConfigDatabaseConnectionStrings = new SortedDictionary<string, string>();
            for (i = 0; i < ConfigurationManager.ConnectionStrings.Count; i++)
            {
                configKey = ConfigurationManager.ConnectionStrings[i].Name;
                configValue = ConfigurationManager.ConnectionStrings[i].ConnectionString;
                ConfigDatabaseConnectionStrings[configKey] = configValue;
            }
        }
        public static void Initialize(HttpContext httpContextCurrent)
        {
            int i;
            string configKey, configValue;
            ConfigDataAppSettings = new SortedDictionary<string, string>();
            for (i = 0; i < ConfigurationManager.AppSettings.Keys.Count; i++)
            {
                configKey = ConfigurationManager.AppSettings.Keys[i];
                configValue = ConfigurationManager.AppSettings[configKey];
                ConfigDataAppSettings[configKey] = configValue;
            }
            ConfigDatabaseConnectionStrings = new SortedDictionary<string, string>();
            for (i = 0; i < ConfigurationManager.ConnectionStrings.Count; i++)
            {
                configKey = ConfigurationManager.ConnectionStrings[i].Name;
                configValue = ConfigurationManager.ConnectionStrings[i].ConnectionString;
                ConfigDatabaseConnectionStrings[configKey] = configValue;
            }
            HttpApplicationState httpApplicationState = httpContextCurrent.Application;
            for (i = 0; i < ConfigurationManager.AppSettings.Keys.Count; i++)
            {
                httpApplicationState[ConfigurationManager.AppSettings.Keys[i]] = ConfigurationManager.AppSettings[i];
            }
        }
        public static string GetApplicationValue(string applicationKey)
        {
            return ConfigDataAppSettings[applicationKey];
        }
        public static string GetDatabaseConnectionString(string connectionStringName)
        {
            return ConfigDatabaseConnectionStrings[connectionStringName];
        }
        public static void SetApplicationValue(string applicationKey, string applicationValue)
        {
            ConfigDataAppSettings[applicationKey] = applicationValue;
        }
        #endregion

        #region Public Methods
        public static List<string> GetCallerMemberNameLineNumber([CallerMemberName] string callerMemberName = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            List<string> callerMemberLineInfos = new List<string>
            {
                callerMemberName,
                callerLineNumber.ToString(),
            };
            return callerMemberLineInfos;
        }
        public static string GetCallerMemberName([CallerMemberName] string callerMemberName = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            return callerMemberName;
        }
        public static int GetCallerLineNumber([CallerMemberName] string callerMemberName = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            return callerLineNumber;
        }
        public static string GetPrivateKey()
        {
            return GetApplicationValue("PrivateKey");
        }
        public static string GetServerMapPath(string virtualPath)
        {
            return HttpContext.Current.Server.MapPath(virtualPath);
        }
        public static bool IsValidEmail(string emailAddress)
        {
            invalidEmailAddress_ = false;
            if (string.IsNullOrEmpty(emailAddress))
            {
                return false;
            }
            try
            {
                emailAddress = Regex.Replace(emailAddress, "(@)(.+)$", new MatchEvaluator(DomainMapper), RegexOptions.None, TimeSpan.FromMilliseconds(200.0));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            if (invalidEmailAddress_)
            {
                return false;
            }
            try
            {
                return Regex.IsMatch(emailAddress, "^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250.0));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        public static string EncodeUrl(string _decodedUrlData)
        {
            string encodedUrlData;

            encodedUrlData = HttpUtility.UrlEncode(_decodedUrlData, HttpContext.Current.Response.ContentEncoding);

            return encodedUrlData;
        }
        public static string DecodeUrl(string encodedUrlData)
        {
            string decodedUrlData;

            decodedUrlData = HttpUtility.UrlEncode(encodedUrlData, HttpContext.Current.Response.ContentEncoding);

            return decodedUrlData;
        }
        public static ExceptionLogger CreateExceptionLogger(string applicationName, string ipAddress, string execUniqueId, string loggedInUserId, string callingAssembly, string executingAssembly, string currentMethodDeclaringType)
        {
            ExceptionLogger exceptionLogger = new ExceptionLogger(applicationName, ipAddress, execUniqueId, loggedInUserId, callingAssembly, executingAssembly, currentMethodDeclaringType);
            return exceptionLogger;
        }
        public static string CreateExecUniqueId()
        {
            string execUniqueId;
            var execUniqeIdObject = HttpContext.Current.Session["ExecUniqueId"];
            if (execUniqeIdObject == null || execUniqeIdObject.ToString() == "")
            {
                execUniqeIdObject = Guid.NewGuid().ToString();
                HttpContext.Current.Session["ExecUniqueId"] = execUniqeIdObject;
            }
            execUniqueId = execUniqeIdObject.ToString();
            return execUniqueId;
        }
        public static string GetIPAddress(HttpRequestBase request)
        {
            // handle standardized 'Forwarded' header
            string forwarded = request.Headers["Forwarded"];
            if (!String.IsNullOrEmpty(forwarded))
            {
                foreach (string segment in forwarded.Split(',')[0].Split(';'))
                {
                    string[] pair = segment.Trim().Split('=');
                    if (pair.Length == 2 && pair[0].Equals("for", StringComparison.OrdinalIgnoreCase))
                    {
                        string ip = pair[1].Trim('"');

                        // IPv6 addresses are always enclosed in square brackets
                        int left = ip.IndexOf('['), right = ip.IndexOf(']');
                        if (left == 0 && right > 0)
                        {
                            return ip.Substring(1, right - 1);
                        }

                        // strip port of IPv4 addresses
                        int colon = ip.IndexOf(':');
                        if (colon != -1)
                        {
                            return ip.Substring(0, colon);
                        }

                        // this will return IPv4, "unknown", and obfuscated addresses
                        return ip;
                    }
                }
            }

            // handle non-standardized 'X-Forwarded-For' header
            string xForwardedFor = request.Headers["X-Forwarded-For"];
            if (!String.IsNullOrEmpty(xForwardedFor))
            {
                return xForwardedFor.Split(',')[0];
            }

            return request.UserHostAddress;
        }
        public static string GetLoggedInUserId(HttpSessionStateBase httpSessionStateBase)
        {
            string loggedInuserId;
            try
            {
                var sessionObjectModel = (SessionObjectModel)httpSessionStateBase["SessionObject"];
                loggedInuserId = sessionObjectModel.AspNetUserId;
            }
            catch
            {
                loggedInuserId = "";
            }
            return loggedInuserId;
        }
        #endregion

        #region Private Methods
        private static string DomainMapper(Match match)
        {
            IdnMapping mapping = new IdnMapping();
            string unicode = match.Groups[2].Value;
            try
            {
                unicode = mapping.GetAscii(unicode);
            }
            catch (ArgumentException)
            {
                invalidEmailAddress_ = true;
            }
            return (match.Groups[1].Value + unicode);
        }
        #endregion

        //public static string GetIPAddress()
        //{
        //    return "";
        //    //string ipAddress = "", ipAddressTemp = "";
        //    //int indexOf1, indexOf2;
        //    //try
        //    //{
        //    //    WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
        //    //    using (WebResponse response = request.GetResponse())
        //    //    using (StreamReader stream = new StreamReader(response.GetResponseStream()))
        //    //    {
        //    //        ipAddressTemp = stream.ReadToEnd();
        //    //    }
        //    //    indexOf1 = ipAddressTemp.IndexOf("Address: ") + 9; //9 is the length of "Address: " without quotes
        //    //    if (indexOf1 > -1)
        //    //    {
        //    //        indexOf2 = ipAddressTemp.IndexOf("</body>");
        //    //        ipAddress = ipAddressTemp.Substring(indexOf1, indexOf2 - indexOf1);
        //    //    }
        //    //}
        //    //catch
        //    //{
        //    //    ;
        //    //}
        //    //return ipAddress;
        //}
        //private static ExceptionLogger CreateExceptionLogger(string callingAssembly, string currentMethodDeclaringType, string executingAssembly)
        //{
        //    string execUniqueId = CreateExecUniqueId();
        //    ExceptionLogger exceptionLogger = CreateExceptionLogger(execUniqueId, callingAssembly, executingAssembly, currentMethodDeclaringType);
        //    return exceptionLogger;
        //}
        //private static ExceptionLogger CreateExceptionLogger(string execUniqueId, string callingAssembly, string currentMethodDeclaringType, string executingAssembly)
        //{
        //    string applicationName = Utilities.GetApplicationValue("ApplicationName");
        //    ExceptionLogger exceptionLogger = CreateExceptionLogger(applicationName, execUniqueId, callingAssembly, executingAssembly, currentMethodDeclaringType);
        //    return exceptionLogger;
        //}
        //public static ExceptionLogger CreateExceptionLogger(string applicationName, string execUniqueId, string callingAssembly, string executingAssembly, string currentMethodDeclaringType)
        //{
        //    ExceptionLogger exceptionLogger = new ExceptionLogger(applicationName, execUniqueId, callingAssembly, executingAssembly, currentMethodDeclaringType);
        //    return exceptionLogger;
        //}
        //public static string SerializeJSONObject<T>(T jsonObject)
        //{
        //    return JsonConvert.SerializeObject(jsonObject);
        //}
        //The below method needs System.Web.Mvc - I believe so - I forgot how got it earlier
        //public static string ViewToHTMLString(string _viewName, object _modelInstance, ControllerContext _controllerContextInstance, ViewDataDictionary _viewDataDictionaryInstance, TempDataDictionary _tempDataDictionaryInstance)
        //{
        //    _viewDataDictionaryInstance.Model = _modelInstance;
        //    using (StringWriter writer = new StringWriter())
        //    {
        //        ViewEngineResult result = ViewEngines.Engines.FindPartialView(_controllerContextInstance, _viewName);
        //        ViewContext viewContext = new ViewContext(_controllerContextInstance, result.View, _viewDataDictionaryInstance, _tempDataDictionaryInstance, writer);
        //        result.View.Render(viewContext, writer);
        //        result.ViewEngine.ReleaseView(_controllerContextInstance, result.View);
        //        return writer.GetStringBuilder().ToString();
        //    }
        //}
        //public static string RenderPartialViewToString(Controller controller, string viewName, object model)
        //{
        //    //if (string.IsNullOrEmpty(viewName))
        //    //{
        //    //    viewName = controller.ControllerContext.RouteData.GetRequiredString("action");

        //    //}
        //    controller.ViewData.Model = model;

        //    using (var sw = new StringWriter())
        //    {
        //        var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
        //        var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
        //        viewResult.View.Render(viewContext, sw);
        //        return sw.GetStringBuilder().ToString();
        //    }
        //}
        //public static void GetHostIPAddress(out string hostName, out string ipAddress, out IPAddress[] ipAddressList)
        //{
        //    hostName = Dns.GetHostName();
        //    //IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
        //    IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        //    IPAddress ipAddressTemp = ipHostInfo.AddressList[0];

        //    ipAddress = ipAddressTemp.ToString();
        //    ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        //    ipAddressList = ipHostInfo.AddressList;
        //}
        //public static void GetIPAddressOld(out string _hostName, out string _ipAddress, out string _siteName, out string _userHostAddress, out string _localAddress)
        //{
        //    string hostName, ipAddress, siteName, userHostAddress, localAddress;
        //    IPAddress[] ipAddressList;
        //    GetHostIPAddress(out hostName, out ipAddress, out ipAddressList);
        //    siteName = System.Web.Hosting.HostingEnvironment.ApplicationHost.GetSiteName();
        //    for (int i = 0; i < ipAddressList.Length; i++)
        //    {
        //        //Response.Write(TabChar + i + " IP Address List =" + ipAddressList[i] + "<br />");
        //    }
        //    userHostAddress = HttpContext.Current.Request.UserHostAddress;
        //    localAddress = HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"];
        //    _hostName = hostName;
        //    _ipAddress = ipAddress;
        //    _siteName = siteName;
        //    _userHostAddress = userHostAddress;
        //    _localAddress = localAddress;
        //}
        //public static string GetUserIPAddress()
        //{
        //    string VisitorsIPAddr = string.Empty;
        //    if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
        //    {
        //        VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
        //    }
        //    else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
        //    {
        //        VisitorsIPAddr = HttpContext.Current.Request.UserHostAddress;
        //    }
        //    //uip.Text = "Your IP is: " + VisitorsIPAddr;
        //    return VisitorsIPAddr;
        //}
    }
}
