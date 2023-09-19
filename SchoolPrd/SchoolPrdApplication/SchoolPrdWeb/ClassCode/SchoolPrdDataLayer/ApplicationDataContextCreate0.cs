using ArchitectureLibraryException;
using ArchitectureLibraryUtility;
using SchoolPrdEnumerations;
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
        public static void CreateAdmission(ClassEnrollModel classEnrollModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                AddClassEnroll(classEnrollModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                AddClassEnrollFeesFromClassFees((long)classEnrollModel.ClassEnrollId, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static void CreateAgreement(SignatureModel signatureModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                int i, j;
                float seqNum = 0;
                SqlCommand sqlCommand = BuildClassEnrollInitialAgreementDelete(sqlConnection);
                sqlCommand.Parameters["@ClassEnrollId"].Value = signatureModel.ClassEnrollId;
                sqlCommand.ExecuteNonQuery();
                sqlCommand = BuildClassEnrollInitialSignatureInsert(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                sqlCommand.Parameters["@ClientId"].Value = clientId;
                sqlCommand.Parameters["@ClassEnrollId"].Value = signatureModel.ClassEnrollId;
                sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
                sqlCommand.Parameters["@ServerDateTime"].Value = signatureModel.ServerDateTimeBase;
                for (i = 0; i < signatureModel.SignatureDataModels.Count; i++)
                {
                    sqlCommand.Parameters["@DocumentTypeNameDesc"].Value = signatureModel.SignatureDataModels[i].DocumentTypeNameDesc;
                    for (j = 0; j < signatureModel.SignatureDataModels[i].ClientDateTimes.Count; j++)
                    {
                        seqNum++;
                        sqlCommand.Parameters["@ClientDateTime"].Value = signatureModel.SignatureDataModels[i].ClientDateTimes[j];
                        sqlCommand.Parameters["@InitialsSignatureId"].Value = signatureModel.SignatureDataModels[i].InitialSignatureDetailIds[j];
                        sqlCommand.Parameters["@SeqNum"].Value = seqNum;
                        sqlCommand.ExecuteNonQuery();
                    }
                }
                sqlCommand = BuildClassEnrollInitialAgreementUpdate(sqlConnection);
                sqlCommand.Parameters["@EnrollmentAgreementInitialsSignaturesCount"].Value = signatureModel.SignatureDataModels[0].ClientDateTimes.Count;
                sqlCommand.Parameters["@ClassEnrollId"].Value = signatureModel.ClassEnrollId;
                sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
                sqlCommand.ExecuteNonQuery();
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static void CreateClassSchedule(ClassScheduleModel classScheduleModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                SqlCommand sqlCommand = BuildClassScheduleInsert(sqlConnection);
                AssignClassSchedule(classScheduleModel, sqlCommand, ipAddress, execUniqueId, loggedInUserId);
                classScheduleModel.ClassScheduleId = (long)sqlCommand.ExecuteScalar();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        //public static void CreateClassEnroll(ClassEnrollModel classEnrollModel,SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        SqlCommand sqlCommand = BuildClassEnrollInsert(sqlConnection);
        //        AssignClassEnroll(classEnrollModel, sqlCommand, ipAddress, execUniqueId, loggedInUserId);
        //        classEnrollModel.ClassEnrollId = (long)sqlCommand.ExecuteScalar();
        //        sqlCommand = BuildClassEnrollFeesInsert(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        foreach (var classEnrollFeesModel in classEnrollModel.ClassEnrollFeesModels)
        //        {
        //            if (!string.IsNullOrWhiteSpace(classEnrollFeesModel.ClassEnrollFeesDesc))
        //            {
        //                classEnrollFeesModel.ClassEnrollId = (long)classEnrollModel.ClassEnrollId;
        //                AssignClassEnrollFees(classEnrollFeesModel, sqlCommand, ipAddress, execUniqueId, loggedInUserId);
        //                classEnrollFeesModel.ClassEnrollFeesId = (long)sqlCommand.ExecuteScalar();
        //            }
        //        }
        //        //AddClassEnrollFeesFromClassFees((long)classEnrollModel.ClassEnrollId, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");                
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        //public static void CreateClassList(ClassListModel classListModel, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        SqlCommand sqlCommand = BuildClassListInsert(sqlConnection);
        //        AssignClassList(classListModel, sqlCommand, ipAddress, execUniqueId, loggedInUserId);
        //        classListModel.ClassListId = (long)sqlCommand.ExecuteScalar();
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        //public static void CreateClassSession(ClassSessionModel classSessionModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        SqlCommand sqlCommand = BuildClassSessionInsert(sqlConnection);
        //        classSessionModel.SeqNum = GetClassSessionMaxSeqNum((long)classSessionModel.ClassListId, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        classSessionModel.SeqNum++;
        //        AssignClassSession(classSessionModel, sqlCommand, ipAddress, execUniqueId, loggedInUserId);
        //        classSessionModel.ClassSessionId = (long)sqlCommand.ExecuteScalar();
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        //public static void CreateClassFees(ClassFeesModel classFeesModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        SqlCommand sqlCommand = BuildClassFeesInsert(sqlConnection);
        //        classFeesModel.SeqNum = GetClassFeesMaxSeqNum((long)classFeesModel.ClassListId, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        classFeesModel.SeqNum++;
        //        AssignClassFees(classFeesModel, sqlCommand, ipAddress, execUniqueId, loggedInUserId);
        //        classFeesModel.ClassFeesId = (long)sqlCommand.ExecuteScalar();
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        //public static void CreateEnrollment(EnrollmentModel enrollmentModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        AddEnrollment(enrollmentModel, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        return;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        //public static void CreateHoliday(HolidayModel holidayModel, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        SqlCommand sqlCommand = BuildHolidayInsert(sqlConnection);
        //        AssignHoliday(holidayModel, sqlCommand, ipAddress, execUniqueId, loggedInUserId);
        //        holidayModel.HolidayId = (long)sqlCommand.ExecuteScalar();
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
        //public static void CreateSignature(SignatureModel signatureModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        int i, j;
        //        float seqNum = 0;
        //        SqlCommand sqlCommand = BuildClassEnrollInitialSignatureDelete(sqlConnection);
        //        sqlCommand.Parameters["@ClassEnrollId"].Value = signatureModel.ClassEnrollId;
        //        sqlCommand.ExecuteNonQuery();
        //        sqlCommand = BuildClassEnrollInitialSignatureInsert(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        sqlCommand.Parameters["@ClientId"].Value = clientId;
        //        sqlCommand.Parameters["@ClassEnrollId"].Value = signatureModel.ClassEnrollId;
        //        sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
        //        sqlCommand.Parameters["@ServerDateTime"].Value = signatureModel.ServerDateTimeBase;
        //        for (i = 0; i < signatureModel.SignatureDataModels.Count; i++)
        //        {
        //            sqlCommand.Parameters["@DocumentTypeNameDesc"].Value = signatureModel.SignatureDataModels[i].DocumentTypeNameDesc;
        //            for (j = 0; j < signatureModel.SignatureDataModels[i].ClientDateTimes.Count; j++)
        //            {
        //                seqNum++;
        //                sqlCommand.Parameters["@ClientDateTime"].Value = signatureModel.SignatureDataModels[i].ClientDateTimes[j];
        //                sqlCommand.Parameters["@InitialsSignatureId"].Value = signatureModel.SignatureDataModels[i].InitialSignatureDetailIds[j];
        //                sqlCommand.Parameters["@SeqNum"].Value = seqNum;
        //                sqlCommand.ExecuteNonQuery();
        //            }
        //        }
        //        sqlCommand = BuildClassEnrollInitialSignatureUpdate(sqlConnection);
        //        sqlCommand.Parameters["@CatalogInitialsSignaturesCount"].Value = signatureModel.SignatureDataModels[0].ClientDateTimes.Count;
        //        sqlCommand.Parameters["@PerformanceFactSheetInitialsSignaturesCount"].Value = signatureModel.SignatureDataModels[1].ClientDateTimes.Count;
        //        sqlCommand.Parameters["@ClassEnrollId"].Value = signatureModel.ClassEnrollId;
        //        sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
        //        sqlCommand.ExecuteNonQuery();
        //        return;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
    }
}
