using ArchitectureLibraryBusinessLayer;
using ArchitectureLibraryEnumerations;
using ArchitectureLibraryException;
using ArchitectureLibraryUtility;
using SVCCTempleCacheData;
using SVCCTempleModels;
using SVCCTempleWeb.ClassCode;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SVCCTempleBusinessLayer
{
    public partial class SVCCTempleBL
    {
        //GET Calendar
        public CalendarModel GenerateCalendarData(string locationNameDesc, string yearMonth, string importantDatesIds, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            CalendarModel calendarModel = new CalendarModel();
            if (string.IsNullOrWhiteSpace(yearMonth))
            {
                yearMonth = DateTime.Today.ToString("yyyy-MM");
                calendarModel.CalendarFromDate = DateTime.Parse(DateTime.Today.ToString("yyyy-MM-01"));
            }
            else
            {
                calendarModel.CalendarFromDate = DateTime.Parse(yearMonth + "-01");
            }
            if (calendarModel.CalendarFromDate.DayOfWeek == DayOfWeek.Sunday)
            {
                calendarModel.CalendarStartDate = calendarModel.CalendarFromDate.AddDays(-7);
            }
            else
            {
                calendarModel.CalendarStartDate = calendarModel.CalendarFromDate.AddDays(0 - calendarModel.CalendarFromDate.DayOfWeek);
            }
            calendarModel.CalendarFinishDate = calendarModel.CalendarStartDate.AddDays(42);
            calendarModel.CalendarToDate = calendarModel.CalendarFromDate.AddMonths(1).AddDays(-1);
            calendarModel.CalendarMonthName = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            calendarModel.CalendarYear = new int[] { 2013, 2014, 2015, 2016, 2017, 2018, 2019, 2020, 2021, 2022, 2023, 2024, 2025, 2026, 2027, 2028, 2029, 2030, 2031, 2032, 2033, 2034, 2035 };
            calendarModel.CalendarYearSelected = calendarModel.CalendarFromDate.Year;
            calendarModel.CalendarMonthSelected = calendarModel.CalendarFromDate.Month;
            calendarModel.CalendarDataListList = new SortedList<string, List<CalendarData>>();
            calendarModel.CalendarDateListList = new SortedList<DateTime, List<CalendarData>>();
            calendarModel.CalendarEventListList = new SortedList<DateTime, List<CalendarEvent>>();
            GetCalendarData(locationNameDesc, yearMonth, calendarModel.CalendarStartDate, calendarModel.CalendarFinishDate, calendarModel.CalendarFromDate, calendarModel.CalendarToDate, calendarModel.CalendarDataListList, calendarModel.CalendarDateListList, calendarModel.CalendarEventListList, clientId, ipAddress, execUniqueId, loggedInUserId);
            GetImportantDates3(locationNameDesc, calendarModel, 1, importantDatesIds, clientId, ipAddress, execUniqueId, ipAddress);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return calendarModel;
        }
        //GET ContactUs
        public ContactUsModel ContactUs(string locationNameDesc, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            ContactUsModel contactUsModel = new ContactUsModel
            {
                ResponseObjectModel = new ResponseObjectModel0
                {
                    ResponseMessages = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("", "Please enter the below information & click Submit"),
                        new KeyValuePair<string, string>("", "Please check your email for a copy"),
                        new KeyValuePair<string, string>("", "We will get back to you, once we review your email"),
                    },
                    ResponseTypeId = SVCCTempleEnumerations.ResponseTypeEnum.Info,
                    TextAlign = "left",
                    ListStyleType = "decimal",
                    StatusCode = HttpStatusCode.OK,
                },
            };
            return contactUsModel;
        }
        //POST ContactUs
        public void ContactUs(string locationNameDesc, string locationNameDesc1, ContactUsModel contactUsModel, Controller controller, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
           string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SqlConnection sqlConnection = null;
            SqlDataReader sqlDataReader = null;
            WebUtilities webUtilities = new WebUtilities();
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
                sqlConnection = new SqlConnection(databaseConnectionString);
                sqlConnection.Open();
                InsertContactUs(locationNameDesc, contactUsModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                sqlConnection.Close();
                string privateKey = SVCCTempleCache.PrivateKey;
                string emailSubjectHtml = archLibBL.ViewToHtmlString(controller, "_ContactUsEmailSubject", contactUsModel);
                string emailBodyHtml = archLibBL.ViewToHtmlString(controller, "_ContactUsEmailBody", contactUsModel);
                string signatureHtml = archLibBL.ViewToHtmlString(controller, "_SignatureTemplate", contactUsModel);
                emailBodyHtml += signatureHtml;
                archLibBL.SendEmail(contactUsModel.EmailAddress, emailSubjectHtml, emailBodyHtml, null, clientId, ipAddress, execUniqueId, loggedInUserId);
                contactUsModel.ResponseObjectModel = webUtilities.InitializeResponseObjectModel(contactUsModel.ResponseObjectModel);
                contactUsModel.ResponseObjectModel.ResponseTypeId = SVCCTempleEnumerations.ResponseTypeEnum.Success;
                contactUsModel.ResponseObjectModel.TextAlign = "left";
                contactUsModel.ResponseObjectModel.ListStyleType = "decimal";
                contactUsModel.ResponseObjectModel.StatusCode = HttpStatusCode.OK;
                contactUsModel.ResponseObjectModel.ResponseMessages = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("", "Your request successfully saved"),
                    new KeyValuePair<string, string>("", "Check your Inbox for a copy"),
                    new KeyValuePair<string, string>("", "Check your Junk / Spam folder"),
                    new KeyValuePair<string, string>("", "Still need to contact, please call us"),
                };
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Error", exception);
                throw;
            }
            finally
            {
                try
                {
                    sqlDataReader.Close();
                }
                catch
                {
                    ;
                }
                try
                {
                    sqlConnection.Close();
                }
                catch
                {
                    ;
                }
            }
        }
        public void GetCalendarData(string locationNameDesc, string yearMonth, DateTime calendarStartDate, DateTime calendarFinishDate, DateTime calendarFromDate, DateTime calendarToDate, SortedList<string, List<CalendarData>> calendarDataListList, SortedList<DateTime, List<CalendarData>> calendarDateListList, SortedList<DateTime, List<CalendarEvent>> calendarEventListList, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            DateTime fromDate, toDate;
            string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(BuildSqlStmtCalendarData(clientId, ipAddress, execUniqueId, loggedInUserId), sqlConnection);
            sqlCommand.Parameters.Add("@LocationNameDesc", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@StartDate", SqlDbType.VarChar, 10);
            sqlCommand.Parameters.Add("@FinishDate", SqlDbType.VarChar, 10);
            sqlCommand.Parameters["@LocationNameDesc"].Value = locationNameDesc;
            sqlCommand.Parameters["@StartDate"].Value = calendarStartDate.ToString("yyyy-MM-dd");
            sqlCommand.Parameters["@FinishDate"].Value = calendarFinishDate.ToString("yyyy-MM-dd");
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            bool sqlDataReaderRead = sqlDataReader.Read();
            string codeTypeNameDesc;
            List<CalendarData> calendarDataList;
            List<CalendarData> calendarDateList;
            CalendarData calendarDataInstance;
            bool addCalendarData;
            int yearMonthNum, yearMonthStartDate, yearMonthFinishDate;
            while (sqlDataReaderRead)
            {
                codeTypeNameDesc = sqlDataReader["CodeTypeNameDesc"].ToString();
                calendarDataList = new List<CalendarData>();
                calendarDataListList.Add(codeTypeNameDesc, calendarDataList);
                while (sqlDataReaderRead && sqlDataReader["CodeTypeNameDesc"].ToString() == codeTypeNameDesc)
                {
                    if (sqlDataReader["CodeTypeNameDesc"].ToString() == "PANCHANGAM_YEAR" || sqlDataReader["CodeTypeNameDesc"].ToString() == "PANCHANGAM_YEAR_PART" || sqlDataReader["CodeTypeNameDesc"].ToString() == "PANCHANGAM_SEASON" || sqlDataReader["CodeTypeNameDesc"].ToString() == "PANCHANGAM_MONTH" || sqlDataReader["CodeTypeNameDesc"].ToString() == "PANCHANGAM_RAASI")
                    {
                        yearMonthNum = int.Parse(yearMonth.Replace("-", ""));
                        yearMonthStartDate = int.Parse(DateTime.Parse(sqlDataReader["StartDate"].ToString()).ToString("yyyyMM"));
                        yearMonthFinishDate = int.Parse(DateTime.Parse(sqlDataReader["FinishDate"].ToString()).ToString("yyyyMM"));
                        if (yearMonthNum >= yearMonthStartDate && yearMonthNum <= yearMonthFinishDate)
                        {
                            addCalendarData = true;
                        }
                        else
                        {
                            addCalendarData = false;
                        }
                    }
                    else
                    {
                        addCalendarData = true;
                    }
                    if (addCalendarData)
                    {
                        calendarDataInstance = new CalendarData
                        {
                            StartDate = DateTime.Parse(sqlDataReader["StartDate"].ToString()),
                            StartTime = sqlDataReader["StartTime"].ToString(),
                            FinishDate = DateTime.Parse(sqlDataReader["FinishDate"].ToString()),
                            FinishTime = sqlDataReader["FinishTime"].ToString(),
                            CodeTypeNameId = long.Parse(sqlDataReader["CodeTypeNameId"].ToString()),
                            CodeTypeNameDesc = sqlDataReader["CodeTypeNameDesc"].ToString(),
                            CalendarCodeId = long.Parse(sqlDataReader["CalendarCodeId"].ToString()),
                            CodeDataNameId = long.Parse(sqlDataReader["CodeDataNameId"].ToString()),
                            CodeDataDesc0 = sqlDataReader["CodeDataDesc0"].ToString(),
                            CodeDataDesc9 = sqlDataReader["CodeDataDesc9"].ToString(),
                        };
                        calendarDataList.Add(calendarDataInstance);
                        if (calendarDataInstance.CodeTypeNameDesc == "PANCHANGAM_THITHI" || calendarDataInstance.CodeTypeNameDesc == "PANCHANGAM_NAKSHATRA" || calendarDataInstance.CodeTypeNameDesc == "RISE_SET")
                        {
                            fromDate = calendarDataInstance.StartDate;
                            toDate = calendarDataInstance.FinishDate;
                            while (fromDate <= toDate)
                            {
                                if (fromDate < calendarStartDate)
                                {

                                }
                                else
                                {
                                    if (!calendarDateListList.TryGetValue(fromDate, out calendarDateList))
                                    {
                                        calendarDateList = new List<CalendarData>();
                                        calendarDateListList.Add(fromDate, calendarDateList);
                                    }
                                    if (fromDate < calendarFromDate || toDate > calendarToDate)
                                    {
                                        calendarDataInstance.Color = "#aaaaaa";
                                    }
                                    else
                                    {
                                        switch (calendarDataInstance.CodeTypeNameDesc)
                                        {
                                            case "PANCHANGAM_NAKSHATRA":
                                                calendarDataInstance.Color = "#0000ff";
                                                break;
                                            case "RISE_SET":
                                                calendarDataInstance.Color = "#a54000";
                                                break;
                                            default:
                                                calendarDataInstance.Color = "#000000";
                                                break;
                                        }
                                    }
                                    calendarDateList.Add(calendarDataInstance);
                                }
                                fromDate = fromDate.AddDays(1);
                            }
                        }
                    }
                    sqlDataReaderRead = sqlDataReader.Read();
                }
            }
            sqlDataReader.Close();
            sqlCommand = new SqlCommand(BuildSqlStmtCalendarEventData(clientId, ipAddress, execUniqueId, loggedInUserId), sqlConnection);
            sqlCommand.Parameters.Add("@LocationNameDesc", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@StartDate", SqlDbType.VarChar, 10);
            sqlCommand.Parameters.Add("@FinishDate", SqlDbType.VarChar, 10);
            sqlCommand.Parameters["@LocationNameDesc"].Value = locationNameDesc;
            sqlCommand.Parameters["@StartDate"].Value = calendarStartDate.ToString("yyyy-MM-dd");
            sqlCommand.Parameters["@FinishDate"].Value = calendarFinishDate.ToString("yyyy-MM-dd");
            sqlDataReader = sqlCommand.ExecuteReader();
            sqlDataReaderRead = sqlDataReader.Read();
            string eventDate;
            List<CalendarEvent> calendarEventList;
            while (sqlDataReaderRead)
            {
                eventDate = sqlDataReader["EventDate"].ToString();
                calendarEventList = new List<CalendarEvent>();
                calendarEventListList.Add(DateTime.Parse(eventDate), calendarEventList);
                while (sqlDataReaderRead && sqlDataReader["EventDate"].ToString() == eventDate)
                {
                    calendarEventList.Add
                    (
                        new CalendarEvent
                        {
                            CalendarEventId = long.Parse(sqlDataReader["CalendarEventId"].ToString()),
                            LocationNameDesc = locationNameDesc,
                            EventDate = sqlDataReader["EventDate"].ToString(),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                            EventText = sqlDataReader["EventText"].ToString(),
                        }
                    );
                    sqlDataReaderRead = sqlDataReader.Read();
                }
            }
            sqlDataReader.Close();
            sqlConnection.Close();
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return;
        }
        public void GetImportantDates3(string locationNameDesc, CalendarModel templeEventsModels, int monthCount, string importantIdsList, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            //string sqlStmt;
            //sqlStmt = "";
            //sqlStmt += "        SELECT ImportantDates.*" + Environment.NewLine;
            //sqlStmt += "          FROM ImportantDates" + Environment.NewLine;
            //sqlStmt += "         WHERE ImportantDates.LocationNameDesc = '" + locationNameDesc + "'" + Environment.NewLine;
            //sqlStmt += "           AND ImportantDates.ImportantDatesId IN(" + importantIdsList + ")" + Environment.NewLine;
            //sqlStmt += "      ORDER BY ImportantDates.SeqNum" + Environment.NewLine;
            //string databaseConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
            //SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            //sqlConnection.Open();
            //SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            //SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            templeEventsModels.ImportantDatesModelsList = new List<ImportantDatesModel>();
            //while (sqlDataReader.Read())
            //{
            //    templeEventsModels.ImportantDatesModelsList.Add
            //    (
            //        new ImportantDatesModel
            //        {
            //            ImportantDatesId = sqlDataReader["ImportantDatesId"].ToString(),
            //            LocationNameDesc = sqlDataReader["LocationNameDesc"].ToString(),
            //            SeqNum = sqlDataReader["SeqNum"].ToString(),
            //            EventName1 = sqlDataReader["EventName1"].ToString(),
            //            EventName2 = sqlDataReader["EventName2"].ToString(),
            //            EventDesc1 = sqlDataReader["EventDesc1"].ToString(),
            //            EventDesc2 = sqlDataReader["EventDesc2"].ToString(),
            //            StartTime = sqlDataReader["StartTime"].ToString(),
            //            FinishTime = sqlDataReader["FinishTime"].ToString(),
            //            ImageName1 = sqlDataReader["ImageName1"].ToString(),
            //            ImageName2 = sqlDataReader["ImageName2"].ToString(),
            //            EventTypeNameDesc = sqlDataReader["EventTypeNameDesc"].ToString(),
            //        }
            //    );
            //}
            //sqlDataReader.Close();
            //sqlConnection.Close();
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        }
        public void InsertContactUs(string locationNameDesc, ContactUsModel contactUsModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                SqlCommand sqlCommand = BuildSqlCommadContactUsInsert(sqlConnection);

                sqlCommand.Parameters["@LocationNameDesc"].Value = locationNameDesc;
                sqlCommand.Parameters["@ContactUsTypeId"].Value = contactUsModel.ContactUsTypeId;
                sqlCommand.Parameters["@FirstName"].Value = contactUsModel.FirstName;
                sqlCommand.Parameters["@LastName"].Value = contactUsModel.LastName;
                sqlCommand.Parameters["@EmailAddress"].Value = contactUsModel.EmailAddress;
                sqlCommand.Parameters["@TelephoneNum"].Value = contactUsModel.TelephoneNumber;
                sqlCommand.Parameters["@Comments"].Value = contactUsModel.Comments;

                sqlCommand.ExecuteNonQuery();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Error", exception);
                throw;
            }
        }
        private SqlCommand BuildSqlCommadContactUsInsert(SqlConnection sqlConnection)
        {
            string sqlStmt = "";

            sqlStmt += "INSERT ContactUs(LocationNameDesc, ContactUsTypeId, FirstName, LastName, EmailAddress, TelephoneNum, Comments) SELECT @LocationNameDesc, @ContactUsTypeId, @FirstName, @LastName, @EmailAddress, @TelephoneNum, @Comments";
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);

            sqlCommand.Parameters.Add("@LocationNameDesc", SqlDbType.NVarChar, 50);
            sqlCommand.Parameters.Add("@ContactUsTypeId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar, 100);
            sqlCommand.Parameters.Add("@LastName", SqlDbType.NVarChar, 100);
            sqlCommand.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 512);
            sqlCommand.Parameters.Add("@TelephoneNum", SqlDbType.NVarChar, 50);
            sqlCommand.Parameters.Add("@Comments", SqlDbType.NVarChar, 2048);

            return sqlCommand;

        }
        private string BuildSqlStmtCalendarData(long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            string sqlStmt = "";

            sqlStmt += "        SELECT Calendar.StartDate" + Environment.NewLine;
            sqlStmt += "              ,Calendar.StartTime" + Environment.NewLine;
            sqlStmt += "              ,Calendar.FinishDate" + Environment.NewLine;
            sqlStmt += "              ,Calendar.FinishTime" + Environment.NewLine;
            sqlStmt += "              ,CodeType.CodeTypeNameId" + Environment.NewLine;
            sqlStmt += "              ,CodeType.CodeTypeNameDesc" + Environment.NewLine;
            sqlStmt += "              ,CodeType.CodeTypeDesc" + Environment.NewLine;
            sqlStmt += "              ,CodeType.CodeTypeDesc1" + Environment.NewLine;
            sqlStmt += "              ,Calendar.CalendarCodeId" + Environment.NewLine;
            sqlStmt += "              ,CodeData.CodeDataNameId" + Environment.NewLine;
            sqlStmt += "              ,CodeData.CodeDataNameDesc" + Environment.NewLine;
            sqlStmt += "              ,CodeData.CodeDataDesc0" + Environment.NewLine;
            sqlStmt += "              ,CodeData.CodeDataDesc9" + Environment.NewLine;
            sqlStmt += "          FROM Calendar" + Environment.NewLine;
            sqlStmt += "    INNER JOIN CodeData" + Environment.NewLine;
            sqlStmt += "            ON Calendar.CalendarCodeId = CodeData.CodeDataId" + Environment.NewLine;
            sqlStmt += "    INNER JOIN CodeType" + Environment.NewLine;
            sqlStmt += "            ON CodeData.CodeTypeId = CodeType.CodeTypeId" + Environment.NewLine;
            sqlStmt += "         WHERE Calendar.LocationNameDesc = @LocationNameDesc" + Environment.NewLine;
            sqlStmt += "           AND CodeType.CodeTypeNameDesc NOT IN('PANCHANGAM_KARANA')" + Environment.NewLine;
            sqlStmt += "           AND" + Environment.NewLine;
            sqlStmt += "              (" + Environment.NewLine;
            sqlStmt += "               @StartDate BETWEEN Calendar.StartDate AND Calendar.FinishDate" + Environment.NewLine;
            sqlStmt += "            OR @FinishDate BETWEEN Calendar.StartDate AND Calendar.FinishDate" + Environment.NewLine;
            sqlStmt += "              )" + Environment.NewLine;
            sqlStmt += "         UNION" + Environment.NewLine;
            sqlStmt += "        SELECT Calendar.StartDate" + Environment.NewLine;
            sqlStmt += "              ,Calendar.StartTime" + Environment.NewLine;
            sqlStmt += "              ,Calendar.FinishDate" + Environment.NewLine;
            sqlStmt += "              ,Calendar.FinishTime" + Environment.NewLine;
            sqlStmt += "              ,CodeType.CodeTypeNameId" + Environment.NewLine;
            sqlStmt += "              ,CodeType.CodeTypeNameDesc" + Environment.NewLine;
            sqlStmt += "              ,CodeType.CodeTypeDesc" + Environment.NewLine;
            sqlStmt += "              ,CodeType.CodeTypeDesc1" + Environment.NewLine;
            sqlStmt += "              ,Calendar.CalendarCodeId" + Environment.NewLine;
            sqlStmt += "              ,CodeData.CodeDataNameId" + Environment.NewLine;
            sqlStmt += "              ,CodeData.CodeDataNameDesc" + Environment.NewLine;
            sqlStmt += "              ,CodeData.CodeDataDesc0" + Environment.NewLine;
            sqlStmt += "              ,CodeData.CodeDataDesc9" + Environment.NewLine;
            sqlStmt += "          FROM Calendar" + Environment.NewLine;
            sqlStmt += "    INNER JOIN CodeData" + Environment.NewLine;
            sqlStmt += "            ON Calendar.CalendarCodeId = CodeData.CodeDataId" + Environment.NewLine;
            sqlStmt += "    INNER JOIN CodeType" + Environment.NewLine;
            sqlStmt += "            ON CodeData.CodeTypeId = CodeType.CodeTypeId" + Environment.NewLine;
            sqlStmt += "         WHERE Calendar.LocationNameDesc = @LocationNameDesc" + Environment.NewLine;
            sqlStmt += "           AND CodeType.CodeTypeNameDesc NOT IN('PANCHANGAM_KARANA')" + Environment.NewLine;
            sqlStmt += "           AND" + Environment.NewLine;
            sqlStmt += "              (" + Environment.NewLine;
            sqlStmt += "               Calendar.StartDate BETWEEN @StartDate AND @FinishDate" + Environment.NewLine;
            sqlStmt += "            OR Calendar.FinishDate BETWEEN @StartDate AND @FinishDate" + Environment.NewLine;
            sqlStmt += "              )" + Environment.NewLine;
            sqlStmt += "         UNION" + Environment.NewLine;
            sqlStmt += "        SELECT RiseSet.GregorianDate" + Environment.NewLine;
            sqlStmt += "              ,RiseSet.SunRise" + Environment.NewLine;
            sqlStmt += "              ,RiseSet.GregorianDate" + Environment.NewLine;
            sqlStmt += "              ,RiseSet.SunSet" + Environment.NewLine;
            sqlStmt += "              ,9100 CodeTypeNameId" + Environment.NewLine;
            sqlStmt += "              ,'RISE_SET' AS CodeTypeNameDesc" + Environment.NewLine;
            sqlStmt += "              ,'' AS CodeTypeDesc" + Environment.NewLine;
            sqlStmt += "              ,'' AS CodeTypeDesc1" + Environment.NewLine;
            sqlStmt += "              ,RiseSet.RiseSetId" + Environment.NewLine;
            sqlStmt += "              ,9100 AS CodeDataNameId" + Environment.NewLine;
            sqlStmt += "              ,'' AS CodeDataNameDesc" + Environment.NewLine;
            sqlStmt += "              ,'Rise/Set' AS CodeDataDesc0" + Environment.NewLine;
            sqlStmt += "              ,'' AS CodeDataDesc9" + Environment.NewLine;
            sqlStmt += "          FROM RiseSet" + Environment.NewLine;
            sqlStmt += "         WHERE RiseSet.LocationNameDesc = @LocationNameDesc" + Environment.NewLine;
            sqlStmt += "           AND GregorianDate BETWEEN @StartDate AND @FinishDate" + Environment.NewLine;
            sqlStmt += "         UNION" + Environment.NewLine;
            sqlStmt += "        SELECT RiseSet.GregorianDate" + Environment.NewLine;
            sqlStmt += "              ,RiseSet.RKStart" + Environment.NewLine;
            sqlStmt += "              ,RiseSet.GregorianDate" + Environment.NewLine;
            sqlStmt += "              ,RiseSet.RKFinish" + Environment.NewLine;
            sqlStmt += "              ,9200 CodeTypeNameId" + Environment.NewLine;
            sqlStmt += "              ,'RISE_SET' AS CodeTypeNameDesc" + Environment.NewLine;
            sqlStmt += "              ,'' AS CodeTypeDesc" + Environment.NewLine;
            sqlStmt += "              ,'' AS CodeTypeDesc1" + Environment.NewLine;
            sqlStmt += "              ,RiseSet.RiseSetId" + Environment.NewLine;
            sqlStmt += "              ,9200 AS CodeDataNameId" + Environment.NewLine;
            sqlStmt += "              ,'' AS CodeDataNameDesc" + Environment.NewLine;
            sqlStmt += "              ,'Rahu' AS CodeDataDesc0" + Environment.NewLine;
            sqlStmt += "              ,'' AS CodeDataDesc9" + Environment.NewLine;
            sqlStmt += "          FROM RiseSet" + Environment.NewLine;
            sqlStmt += "         WHERE RiseSet.LocationNameDesc = @LocationNameDesc" + Environment.NewLine;
            sqlStmt += "           AND GregorianDate BETWEEN @StartDate AND @FinishDate" + Environment.NewLine;
            sqlStmt += "         UNION" + Environment.NewLine;
            sqlStmt += "        SELECT RiseSet.GregorianDate" + Environment.NewLine;
            sqlStmt += "              ,RiseSet.YGStart" + Environment.NewLine;
            sqlStmt += "              ,RiseSet.GregorianDate" + Environment.NewLine;
            sqlStmt += "              ,RiseSet.YGFinish" + Environment.NewLine;
            sqlStmt += "              ,9300 CodeTypeNameId" + Environment.NewLine;
            sqlStmt += "              ,'RISE_SET' AS CodeTypeNameDesc" + Environment.NewLine;
            sqlStmt += "              ,'' AS CodeTypeDesc" + Environment.NewLine;
            sqlStmt += "              ,'' AS CodeTypeDesc1" + Environment.NewLine;
            sqlStmt += "              ,RiseSet.RiseSetId" + Environment.NewLine;
            sqlStmt += "              ,9300 AS CodeDataNameId" + Environment.NewLine;
            sqlStmt += "              ,'' AS CodeDataNameDesc" + Environment.NewLine;
            sqlStmt += "              ,'Yama' AS CodeDataDesc0" + Environment.NewLine;
            sqlStmt += "              ,'' AS CodeDataDesc9" + Environment.NewLine;
            sqlStmt += "          FROM RiseSet" + Environment.NewLine;
            sqlStmt += "         WHERE RiseSet.LocationNameDesc = @LocationNameDesc" + Environment.NewLine;
            sqlStmt += "           AND GregorianDate BETWEEN @StartDate AND @FinishDate" + Environment.NewLine;
            sqlStmt += "      ORDER BY CodeType.CodeTypeNameId" + Environment.NewLine;
            sqlStmt += "              ,Calendar.StartDate" + Environment.NewLine;

            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return sqlStmt;
        }
        private string BuildSqlStmtCalendarEventData(long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            string sqlStmt = "";

            sqlStmt += "        SELECT CalendarEvent.CalendarEventId";
            sqlStmt += "              ,CalendarEvent.EventDate";
            sqlStmt += "              ,CalendarEvent.SeqNum";
            sqlStmt += "              ,CalendarEvent.EventText";
            sqlStmt += "          FROM CalendarEvent";
            sqlStmt += "         WHERE CalendarEvent.LocationNameDesc = @LocationNameDesc";
            sqlStmt += "           AND CalendarEvent.EventDate BETWEEN @StartDate AND @FinishDate";
            sqlStmt += "      ORDER BY CalendarEvent.EventDate";
            sqlStmt += "              ,CalendarEvent.SeqNum";

            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return sqlStmt;
        }
    }
}
