using ArchitectureLibraryException;
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
        public static void AddClassEnroll(ClassEnrollModel classEnrollModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            SqlCommand sqlCommand = BuildSqlCommandClassEnrollInsert(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
            AssignClassEnroll(classEnrollModel, sqlCommand, clientId, ipAddress, execUniqueId, loggedInUserId);
            classEnrollModel.ClassEnrollId = (long)sqlCommand.ExecuteScalar();
        }
        public static void AddClassList(ClassListModel classListModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                SqlCommand sqlCommand = BuildClassListInsert(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                AssignClassList(classListModel, sqlCommand, clientId, ipAddress, execUniqueId, loggedInUserId);
                classListModel.ClassListId = (long)sqlCommand.ExecuteScalar();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static void AddClassEnrollFeesFromClassFees(long classEnrollId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Before calling the BuildSqlCommandDemogInfoaddressAdd()", "AspNetUserId", "");
                SqlCommand sqlCommand = BuildSqlCommandClassEnrollFeesAddFromClassFees(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                sqlCommand.Parameters["@ClientId"].Value = clientId;
                sqlCommand.Parameters["@ClassEnrollId"].Value = classEnrollId;
                sqlCommand.Parameters["@DueDate"].Value = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd 23:59:59");
                sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
                sqlCommand.ExecuteNonQuery();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00009000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static void AddEnrollment(EnrollmentModel enrollmentModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            SqlCommand sqlCommand = BuildSqlCommandEnrollmentInsert(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
            AssignEnrollment(enrollmentModel, sqlCommand, clientId, ipAddress, execUniqueId, loggedInUserId);
            enrollmentModel.EnrollmentId = long.Parse(sqlCommand.ExecuteScalar().ToString());
        }
    }
}
/*
BEGIN TRAN
DELETE ArchLib.AspNetUserRole WHERE AspNetUserId IN(SELECT AspNetUserId FROM ArchLib.AspNetUser WHERE Email NOT IN('', 'test1@email.com'))
DELETE ArchLib.Person WHERE AspNetUserId IN(SELECT AspNetUserId FROM ArchLib.AspNetUser WHERE Email NOT IN('', 'test1@email.com'))
DELETE ArchLib.AspNetUser WHERE Email NOT IN('', 'test1@email.com')
SELECT * FROM ArchLib.AspNetUser
SELECT * FROM ArchLib.Person
SELECT * FROM ArchLib.AspNetUserRole
COMMIT
*/
