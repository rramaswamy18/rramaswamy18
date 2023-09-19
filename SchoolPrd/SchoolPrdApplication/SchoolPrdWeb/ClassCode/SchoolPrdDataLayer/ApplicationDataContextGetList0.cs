using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using SchoolPrdModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SchoolPrdDataLayer
{
    public static partial class ApplicationDataContext
    {
        public static List<ClassEnrollModel> GetClassEnrolls(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<ClassEnrollModel> classEnrollModels = new List<ClassEnrollModel>();
            try
            {
                string sqlStmt = "";
                sqlStmt += "        SELECT *" + Environment.NewLine;
                sqlStmt += "          FROM SchoolPrdSch.ClassEnroll" + Environment.NewLine;
                sqlStmt += "    INNER JOIN SchoolPrdSch.ClassSchedule" + Environment.NewLine;
                sqlStmt += "            ON ClassEnroll.ClassScheduleId = ClassSchedule.ClassScheduleId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN SchoolPrdSch.ClassSession" + Environment.NewLine;
                sqlStmt += "            ON ClassSchedule.ClassSessionId = ClassSession.ClassSessionId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN ArchLib.Person" + Environment.NewLine;
                sqlStmt += "            ON ClassEnroll.PersonId = Person.PersonId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN ArchLib.AspNetUser" + Environment.NewLine;
                sqlStmt += "            ON Person.AspNetUserId = AspNetUser.AspNetUserId" + Environment.NewLine;
                sqlStmt += "      ORDER BY ClassEnroll.ClassEnrollId" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                ClassEnrollModel classEnrollModel;
                while (sqlDataReader.Read())
                {
                    classEnrollModel = AssignClassEnroll(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId);
                    classEnrollModel.ClassScheduleModel = new ClassScheduleModel
                    {
                        ClassScheduleId = long.Parse(sqlDataReader["ClassScheduleId"].ToString()),
                        ClassScheduleDesc = sqlDataReader["ClassScheduleDesc"].ToString(),
                        GraduationDate = DateTime.Parse(sqlDataReader["GraduationDate"].ToString()).ToString("yyyy-MM-dd"),
                        RegisterDate = DateTime.Parse(sqlDataReader["RegisterDate"].ToString()).ToString("yyyy-MM-dd"),
                        StartDate = DateTime.Parse(sqlDataReader["StartDate"].ToString()).ToString("yyyy-MM-dd"),
                        ClassSessionModel = new ClassSessionModel
                        {
                            ClassSessionId = long.Parse(sqlDataReader["ClassSessionId"].ToString()),
                            ClassSessionDesc = sqlDataReader["ClassSessionDesc"].ToString(),
                        },
                    };
                    classEnrollModel.PersonModel = new PersonModel
                    {
                        PersonId = long.Parse(sqlDataReader["PersonId"].ToString()),
                        FirstName = sqlDataReader["FirstName"].ToString(),
                        LastName = sqlDataReader["LastName"].ToString(),
                        AspNetUserModel = new AspNetUserModel
                        {
                            Email = sqlDataReader["Email"].ToString(),
                        },
                    };
                    classEnrollModels.Add(classEnrollModel);
                }
                sqlDataReader.Close();
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            return classEnrollModels;
        }
        public static List<ClassListModel> GetClassLists(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<ClassListModel> ClassListModels = new List<ClassListModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM SchoolPrdSch.ClassList ORDER BY ClassListId", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    ClassListModels.Add(AssignClassList(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId));
                }
                sqlDataReader.Close();
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            return ClassListModels;
        }
        public static List<ClassSessionModel> GetClassSessions(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<ClassSessionModel> ClassSessionModels = new List<ClassSessionModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM SchoolPrdSch.ClassSession INNER JOIN SchoolPrdSch.ClassList ON ClassSession.ClassListId = ClassList.ClassListId ORDER BY ClassSessionId", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                ClassSessionModel classSessionModel;
                while (sqlDataReader.Read())
                {
                    ClassSessionModels.Add(classSessionModel = AssignClassSession(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId));
                    classSessionModel.ClassListModel = new ClassListModel
                    {
                        ClassListId = long.Parse(sqlDataReader["ClassListId"].ToString()),
                        ClassListDesc = sqlDataReader["ClassListDesc"].ToString(),
                    };
                }
                sqlDataReader.Close();
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            return ClassSessionModels;
        }

        //Review Later
        public static List<ClassDetailModel> GetClassDetails(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<ClassDetailModel> classDetailModels = new List<ClassDetailModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM SchoolPrdSch.ClassDetail ORDER BY ClassDetailId", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    classDetailModels.Add(AssignClassDetail(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId));
                }
                sqlDataReader.Close();
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            return classDetailModels;
        }
        public static List<ClassFeesModel> GetClassFeess(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<ClassFeesModel> ClassFeesModels = new List<ClassFeesModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM SchoolPrdSch.ClassFees ORDER BY ClassFeesId", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    ClassFeesModels.Add(AssignClassFees(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId));
                }
                sqlDataReader.Close();
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            return ClassFeesModels;
        }
        public static List<ClassScheduleModel> GetClassSchedules(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<ClassScheduleModel> ClassScheduleModels = new List<ClassScheduleModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM SchoolPrdSch.ClassSchedule ORDER BY StartDate, ClassSessionId", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    ClassScheduleModels.Add(AssignClassSchedule(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId));
                }
                sqlDataReader.Close();
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            return ClassScheduleModels;
        }
        public static List<InitialSignatureModel> GetInitialSignatures(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<InitialSignatureModel> InitialSignatureModels = new List<InitialSignatureModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM SchoolPrdSch.InitialSignature ORDER BY InitialSignatureId", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    InitialSignatureModels.Add(AssignInitialSignature(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId));
                }
                sqlDataReader.Close();
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            return InitialSignatureModels;
        }
        public static List<InitialSignatureDetailModel> GetInitialSignatureDetails(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<InitialSignatureDetailModel> initialSignatureDetailModels = new List<InitialSignatureDetailModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM SchoolPrdSch.InitialSignatureDetail ORDER BY InitialSignatureDetailId", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    initialSignatureDetailModels.Add(AssignInitialSignatureDetail(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId));
                }
                sqlDataReader.Close();
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            return initialSignatureDetailModels;
        }
        public static List<ClassSessionModel> GetClassSessions(SqlConnection sqlConnection, int pageNumber, int rowCount, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<ClassSessionModel> classSessionModels = new List<ClassSessionModel>();
            try
            {
                SqlCommand sqlCommand = BuildClassSessionSelect(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                ClassSessionModel classSessionModel;
                while (sqlDataReader.Read())
                {
                    classSessionModel = AssignClassSession(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId, "ClassSession_");
                    classSessionModel.ClassListModel = AssignClassList(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId, "ClassList_");                        
                    classSessionModels.Add(classSessionModel);
                }
                sqlDataReader.Close();
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            return classSessionModels;
        }
        public static List<ClassScheduleModel> GetClassSchedules(SqlConnection sqlConnection, int pageNumber, int rowCount, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                List<ClassScheduleModel> classScheduleModels = new List<ClassScheduleModel>();
                SqlCommand sqlCommand = BuildClassScheduleSelect(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                ClassScheduleModel classScheduleModel;
                while (sqlDataReader.Read())
                {
                    classScheduleModel = AssignClassSchedule(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId, "ClassSchedule_");
                    classScheduleModel.ClassSessionModel = AssignClassSession(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId, "ClassSession_");
                    classScheduleModel.ClassSessionModel.ClassListModel = AssignClassList(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId, "ClassList_");
                    classScheduleModels.Add(classScheduleModel);
                }
                sqlDataReader.Close();
                return classScheduleModels;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static List<ClassFeesModel> GetClassFeess(SqlConnection sqlConnection, int pageNumber, int rowCount, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<ClassFeesModel> classFeesModels = new List<ClassFeesModel>();
            try
            {
                SqlCommand sqlCommand = BuildClassFeesSelect(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                ClassFeesModel classFeesModel;
                while (sqlDataReader.Read())
                {
                    classFeesModel = AssignClassFees(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId, "ClassFees_");
                    classFeesModel.ClassListModel = AssignClassList(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId, "ClassList_");
                    classFeesModels.Add(classFeesModel);
                }
                sqlDataReader.Close();
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            return classFeesModels;
        }
        public static ClassSessionModel GetClassSession(long classSessionId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            SqlCommand sqlCommand = BuildSelectClassSession(sqlConnection);
            sqlCommand.Parameters["@ClassSessionId"].Value = classSessionId;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            ClassSessionModel classSessionModel;
            if (sqlDataReader.Read())
            {
                classSessionModel = AssignClassSession(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId);
            }
            else
            {
                classSessionModel = new ClassSessionModel();
            }
            sqlDataReader.Close();
            return classSessionModel;
        }
        public static ClassScheduleModel GetClassSchedule(long classScheduleId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            SqlCommand sqlCommand = BuildSelectClassSchedule(sqlConnection);
            sqlCommand.Parameters["@ClassScheduleId"].Value = classScheduleId;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            ClassScheduleModel classScheduleModel;
            if (sqlDataReader.Read())
            {
                classScheduleModel = AssignClassSchedule(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId);
            }
            else
            {
                classScheduleModel = new ClassScheduleModel();
            }
            sqlDataReader.Close();
            return classScheduleModel;
        }
        public static ClassFeesModel GetClassFees(long classFeesId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            SqlCommand sqlCommand = BuildSelectClassFees(sqlConnection);
            sqlCommand.Parameters["@ClassFeesId"].Value = classFeesId;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            ClassFeesModel classFeesModel;
            if (sqlDataReader.Read())
            {
                classFeesModel = AssignClassFees(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId);
            }
            else
            {
                classFeesModel = new ClassFeesModel();
            }
            sqlDataReader.Close();
            return classFeesModel;
        }
        public static List<HolidayModel> GetHolidays(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<HolidayModel> HolidayModels = new List<HolidayModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM SchoolPrdSch.Holiday ORDER BY HolidayId", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    HolidayModels.Add(AssignHoliday(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId));
                }
                sqlDataReader.Close();
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            return HolidayModels;
        }
        public static List<ClassEnrollModel> GetClassEnrolls(SqlConnection sqlConnection, int pageNumber, int rowCount, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<ClassEnrollModel> classEnrollModels = new List<ClassEnrollModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM SchoolPrdSch.ClassEnroll ORDER BY ClassEnrollId", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    classEnrollModels.Add(AssignClassEnroll(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId));                    
                }
                sqlDataReader.Close();
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            return classEnrollModels;
        }
        public static List<ClassEnrollModel> GetClassEnrolls(long personId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<ClassEnrollModel> classEnrollModels = new List<ClassEnrollModel>();
            try
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM SchoolPrdSch.ClassEnroll WHERE PersonId = " + personId + " ORDER BY ClassEnrollId", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                ClassEnrollModel classEnrollModel;
                while (sqlDataReader.Read())
                {
                    classEnrollModels.Add(classEnrollModel = AssignClassEnroll(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId));
                    classEnrollModel.ClassScheduleModel = SchoolPrdCacheData.SchoolPrdCache.ClassScheduleModels.First(x => x.ClassScheduleId == classEnrollModel.ClassScheduleId);
                }
                sqlDataReader.Close();
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            return classEnrollModels;
        }
    }
}
