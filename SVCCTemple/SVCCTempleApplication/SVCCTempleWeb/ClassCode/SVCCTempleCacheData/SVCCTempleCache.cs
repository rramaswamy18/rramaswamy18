using ArchitectureLibraryException;
using ArchitectureLibraryUtility;
using SVCCTempleBusinessLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;

namespace SVCCTempleCacheData
{
    public static class SVCCTempleCache
    {
        public static string PrivateKey = "ELPME9TCCVS";
        public static string TodaysDate;
        public static Dictionary<string, Dictionary<string, string>> TodaysInfoLocations;
        public static void Initialize(long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            TodaysDate = "";
            BuildTodaysInfo(clientId, ipAddress, execUniqueId, loggedInUserId);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return;
        }
        public static void BuildTodaysInfo(long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            var todaysDate = DateTime.Now.ToString("yyyy-MM-dd");
            if (TodaysDate != todaysDate)
            {
                TodaysDate = todaysDate;
                TodaysInfoLocations = BuildTodaysInfo(todaysDate, clientId, ipAddress, execUniqueId, loggedInUserId);
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return;
        }
        private static Dictionary<string, Dictionary<string, string>> BuildTodaysInfo(string todaysDate, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            Dictionary<string, Dictionary<string, string>> todaysInfoLocations = new Dictionary<string, Dictionary<string, string>>();
            Dictionary<string, string> todaysInfos;
            todaysInfos = GetTodaysInfo("SACRAMENTO", todaysDate, clientId, ipAddress, execUniqueId, loggedInUserId);
            todaysInfoLocations["SACRAMENTO"] = todaysInfos;
            todaysInfos = GetTodaysInfo("FREMONT", todaysDate, clientId, ipAddress, execUniqueId, loggedInUserId);
            todaysInfoLocations["FREMONT"] = todaysInfos;
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            DeleteExceptionLog(clientId, ipAddress, execUniqueId, loggedInUserId);
            return todaysInfoLocations;
        }
        private static Dictionary<string, string> GetTodaysInfo(string locationNameDesc, string gregorianDate, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            Dictionary<string, string> todaysInfos;
            todaysInfos = new Dictionary<string, string>();
            string databaseConnectionString = Utilities.GetDatabaseConnectionString("DatabaseConnectionString");
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM RiseSet WHERE LocationNameDesc = '" + locationNameDesc + "' AND GregorianDate = '" + gregorianDate + "'", sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            sqlDataReader.Read();
            todaysInfos["SunRise"] = sqlDataReader["SunRise"].ToString();
            todaysInfos["SunSet"] = sqlDataReader["SunSet"].ToString();
            todaysInfos["RKStart"] = sqlDataReader["RKStart"].ToString();
            todaysInfos["RKFinish"] = sqlDataReader["RKFinish"].ToString();
            todaysInfos["YGStart"] = sqlDataReader["YGStart"].ToString();
            todaysInfos["YGFinish"] = sqlDataReader["YGFinish"].ToString();
            sqlDataReader.Close();
            sqlConnection.Close();
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return todaysInfos;
        }
        private static void DeleteExceptionLog(long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SqlConnection sqlConnection = new SqlConnection(Utilities.GetDatabaseConnectionString("DatabaseConnectionString"));
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("TRUNCATE TABLE ArchLib.ExceptionLog", sqlConnection);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        }
    }
}
