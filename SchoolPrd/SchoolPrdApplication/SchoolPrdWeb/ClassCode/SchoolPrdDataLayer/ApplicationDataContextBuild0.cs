using ArchitectureLibraryException;
using ArchitectureLibraryUtility;
using SchoolPrdEnumerations;
using SchoolPrdModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SchoolPrdDataLayer
{
    public static partial class ApplicationDataContext
    {
        private static SqlCommand BuildClassListSelect(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "SELECT * FROM SchoolPrdSch.ClassList WHERE ClassListId = @ClassListId";
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ClassListId", SqlDbType.BigInt);
            return sqlCommand;
        }
        private static SqlCommand BuildClassListInsert(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "INSERT SchoolPrdSch.ClassList(ClassListDesc, AddUserId, UpdUserId)";
            sqlStmt += "OUTPUT INSERTED.ClassListId SELECT @ClassListDesc, @LoggedInUserId, @LoggedInUserId" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ClassListDesc", SqlDbType.NVarChar, 250);
            sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 250);
            return sqlCommand;
        }
        private static SqlCommand BuildClassListUpdate(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "UPDATE SchoolPrdSch.ClassList SET ClassListDesc = @ClassListDesc,  UpdUserId = @LoggedInUserId, UpdUserName = SUSER_NAME(), UpdDateTime = GETDATE() WHERE ClassListId = @ClassListId";
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ClassListId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@ClassListDesc", SqlDbType.NVarChar, 250);
            sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 250);
            return sqlCommand;
        }
        private static SqlCommand BuildClassEnrollSelect(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "SELECT * FROM SchoolPrdSch.ClassEnroll WHERE ClassEnrollId = @ClassEnrollId";
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ClassEnrollId", SqlDbType.BigInt);
            return sqlCommand;
        }
        private static SqlCommand BuildSqlCommandClassEnrollInsert(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            #region
            string sqlStmt = "";
            sqlStmt += "        INSERT SchoolPrdSch.ClassEnroll" + Environment.NewLine;
            sqlStmt += "              (" + Environment.NewLine;
            sqlStmt += "               ClientId" + Environment.NewLine;
            sqlStmt += "              ,CatalogInitialsCount" + Environment.NewLine;
            sqlStmt += "              ,CatalogSignaturesCount" + Environment.NewLine;
            sqlStmt += "              ,ClassEnrollStatusId" + Environment.NewLine;
            sqlStmt += "              ,ClassScheduleId" + Environment.NewLine;
            sqlStmt += "              ,EnrollmentAgreementInitialsCount" + Environment.NewLine;
            sqlStmt += "              ,EnrollmentAgreementSignaturesCount" + Environment.NewLine;
            sqlStmt += "              ,FundingRequired" + Environment.NewLine;
            sqlStmt += "              ,FundingSoureName" + Environment.NewLine;
            sqlStmt += "              ,PerformanceFactSheetInitialsCount" + Environment.NewLine;
            sqlStmt += "              ,PerformanceFactSheetSignaturesCount" + Environment.NewLine;
            sqlStmt += "              ,PersonId" + Environment.NewLine;
            sqlStmt += "              ,AddUserId" + Environment.NewLine;
            sqlStmt += "              ,UpdUserId" + Environment.NewLine;
            sqlStmt += "              )" + Environment.NewLine;
            sqlStmt += "        OUTPUT INSERTED.ClassEnrollId" + Environment.NewLine;
            sqlStmt += "        SELECT" + Environment.NewLine;
            sqlStmt += "               @ClientId" + Environment.NewLine;
            sqlStmt += "              ,@CatalogInitialsCount" + Environment.NewLine;
            sqlStmt += "              ,@CatalogSignaturesCount" + Environment.NewLine;
            sqlStmt += "              ,@ClassEnrollStatusId" + Environment.NewLine;
            sqlStmt += "              ,@ClassScheduleId" + Environment.NewLine;
            sqlStmt += "              ,@EnrollmentAgreementInitialsCount" + Environment.NewLine;
            sqlStmt += "              ,@EnrollmentAgreementSignaturesCount" + Environment.NewLine;
            sqlStmt += "              ,@FundingRequired" + Environment.NewLine;
            sqlStmt += "              ,@FundingSoureName" + Environment.NewLine;
            sqlStmt += "              ,@PerformanceFactSheetInitialsCount" + Environment.NewLine;
            sqlStmt += "              ,@PerformanceFactSheetSignaturesCount" + Environment.NewLine;
            sqlStmt += "              ,@PersonId" + Environment.NewLine;
            sqlStmt += "              ,@LoggedInUserId" + Environment.NewLine;
            sqlStmt += "              ,@LoggedInUserId" + Environment.NewLine;
            #endregion
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ClientId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@CatalogInitialsCount", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@CatalogSignaturesCount", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@ClassEnrollStatusId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@ClassScheduleId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@EnrollmentAgreementInitialsCount", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@EnrollmentAgreementSignaturesCount", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@FundingRequired", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@FundingSoureName", SqlDbType.NVarChar, 100);
            sqlCommand.Parameters.Add("@PerformanceFactSheetInitialsCount", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@PerformanceFactSheetSignaturesCount", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@PersonId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 512);
            return sqlCommand;
        }
        private static SqlCommand BuildSqlCommandClassEnrollFeesAddFromClassFees(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "";
            sqlStmt += "        INSERT SchoolPrdSch.ClassEnrollFees" + Environment.NewLine;
            sqlStmt += "              (" + Environment.NewLine;
            sqlStmt += "               ClientId" + Environment.NewLine;
            sqlStmt += "              ,AmountPaid" + Environment.NewLine;
            sqlStmt += "              ,ClassEnrollFeesAmount" + Environment.NewLine;
            sqlStmt += "              ,ClassEnrollFeesDesc" + Environment.NewLine;
            sqlStmt += "              ,ClassEnrollId" + Environment.NewLine;
            sqlStmt += "              ,ClassFeesId" + Environment.NewLine;
            sqlStmt += "              ,ClassFeesTypeId" + Environment.NewLine;
            sqlStmt += "              ,DatePaid" + Environment.NewLine;
            sqlStmt += "              ,DueDate" + Environment.NewLine;
            sqlStmt += "              ,SeqNum" + Environment.NewLine;
            sqlStmt += "              ,AddUserId" + Environment.NewLine;
            sqlStmt += "              ,UpdUserId" + Environment.NewLine;
            sqlStmt += "              )" + Environment.NewLine;
            sqlStmt += "        SELECT" + Environment.NewLine;
            sqlStmt += "               @ClientId" + Environment.NewLine;
            sqlStmt += "              ,NULL" + Environment.NewLine;
            sqlStmt += "              ,ClassFees.ClassFeesAmount" + Environment.NewLine;
            sqlStmt += "              ,ClassFees.ClassFeesDesc" + Environment.NewLine;
            sqlStmt += "              ,@ClassEnrollId" + Environment.NewLine;
            sqlStmt += "              ,ClassFees.ClassFeesId" + Environment.NewLine;
            sqlStmt += "              ,ClassFees.ClassFeesTypeId" + Environment.NewLine;
            sqlStmt += "              ,NULL" + Environment.NewLine;
            sqlStmt += "              ,@DueDate" + Environment.NewLine;
            sqlStmt += "              ,ClassFees.SeqNum" + Environment.NewLine;
            sqlStmt += "              ,@LoggedInUserId" + Environment.NewLine;
            sqlStmt += "              ,@LoggedInUserId" + Environment.NewLine;
            sqlStmt += "          FROM SchoolPrdSch.ClassFees" + Environment.NewLine;
            sqlStmt += "         WHERE ClassFees.ClassFeesId > 0" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ClientId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@ClassEnrollId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@DueDate", SqlDbType.Date);
            sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 512);
            return sqlCommand;
        }

        //Review Later
        private static SqlCommand BuildSqlCommandEnrollmentInsert(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            #region
            string sqlStmt = "";
            sqlStmt += "        INSERT SchoolPrdSch.Enrollment" + Environment.NewLine;
            sqlStmt += "              (" + Environment.NewLine;
            sqlStmt += "               FirstName" + Environment.NewLine;
            sqlStmt += "              ,MiddleName" + Environment.NewLine;
            sqlStmt += "              ,LastName" + Environment.NewLine;
            sqlStmt += "              ,EmailAddress" + Environment.NewLine;
            sqlStmt += "              ,TelephoneNumber" + Environment.NewLine;
            sqlStmt += "              ,ContactMessage" + Environment.NewLine;
            sqlStmt += "              ,DemogInfoAddressId" + Environment.NewLine;
            sqlStmt += "              ,DrugScreenId" + Environment.NewLine;
            sqlStmt += "              ,EnrollmentTypeIds" + Environment.NewLine;
            sqlStmt += "              ,EnrollmentTypeDescs" + Environment.NewLine;
            sqlStmt += "              ,AddressLine1" + Environment.NewLine;
            sqlStmt += "              ,AddressLine2" + Environment.NewLine;
            sqlStmt += "              ,CityName" + Environment.NewLine;
            sqlStmt += "              ,DemogInfoSubDivisionId" + Environment.NewLine;
            sqlStmt += "              ,DrivingLicenseDemogInfoSubDivisionId" + Environment.NewLine;
            sqlStmt += "              ,ZipCode" + Environment.NewLine;
            sqlStmt += "              )" + Environment.NewLine;
            sqlStmt += "        OUTPUT INSERTED.EnrollmentId" + Environment.NewLine;
            sqlStmt += "        SELECT" + Environment.NewLine;
            sqlStmt += "               @FirstName" + Environment.NewLine;
            sqlStmt += "              ,@MiddleName" + Environment.NewLine;
            sqlStmt += "              ,@LastName" + Environment.NewLine;
            sqlStmt += "              ,@EmailAddress" + Environment.NewLine;
            sqlStmt += "              ,@TelephoneNumber" + Environment.NewLine;
            sqlStmt += "              ,@ContactMessage" + Environment.NewLine;
            sqlStmt += "              ,@DemogInfoAddressId" + Environment.NewLine;
            sqlStmt += "              ,@DrugScreenId" + Environment.NewLine;
            sqlStmt += "              ,@EnrollmentTypeIds" + Environment.NewLine;
            sqlStmt += "              ,@EnrollmentTypeDescs" + Environment.NewLine;
            sqlStmt += "              ,@AddressLine1" + Environment.NewLine;
            sqlStmt += "              ,@AddressLine2" + Environment.NewLine;
            sqlStmt += "              ,@CityName" + Environment.NewLine;
            sqlStmt += "              ,@DemogInfoSubDivisionId" + Environment.NewLine;
            sqlStmt += "              ,@DrivingLicenseDemogInfoSubDivisionId" + Environment.NewLine;
            sqlStmt += "              ,@ZipCode" + Environment.NewLine;
            #endregion
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@FirstName", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@MiddleName", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@LastName", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@EmailAddress", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@TelephoneNumber", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ContactMessage", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@DemogInfoAddressId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@DrugScreenId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@EnrollmentTypeIds", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@EnrollmentTypeDescs", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@AddressLine1", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@AddressLine2", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@CityName", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@DemogInfoSubDivisionId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@DrivingLicenseDemogInfoSubDivisionId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ZipCode", System.DBNull.Value);
            return sqlCommand;
        }
        public static SqlCommand BuildClassSessionSelect(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "";
            sqlStmt += "        SELECT " + Environment.NewLine;
            sqlStmt += "               ClassSession.ClassSessionId AS ClassSession_ClassSessionId" + Environment.NewLine;
            sqlStmt += "              ,ClassSession.ClientId AS ClassSession_ClientId" + Environment.NewLine;
            sqlStmt += "              ,ClassSession.ClassSessionDesc AS ClassSession_ClassSessionDesc" + Environment.NewLine;
            sqlStmt += "              ,ClassSession.SeqNum AS ClassSession_SeqNum" + Environment.NewLine;
            sqlStmt += "              ,ClassSession.ClassListId AS ClassSession_ClassListId" + Environment.NewLine;
            sqlStmt += "              ,ClassList.ClassListId AS ClassList_ClassListId" + Environment.NewLine;
            sqlStmt += "              ,ClassList.ClientId AS ClassList_ClientId" + Environment.NewLine;
            sqlStmt += "              ,ClassList.ClassListDesc AS ClassList_ClassListDesc" + Environment.NewLine;
            sqlStmt += "          FROM SchoolPrdSch.ClassSession" + Environment.NewLine;
            sqlStmt += "    INNER JOIN SchoolPrdSch.ClassList" + Environment.NewLine;
            sqlStmt += "            ON ClassSession.ClassListId = ClassList.ClassListId" + Environment.NewLine;
            sqlStmt += "    ORDER BY ClassList.ClassListId" + Environment.NewLine;
            sqlStmt += "            ,ClassSession.SeqNum" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            return sqlCommand;
        }
        public static SqlCommand BuildClassScheduleSelect(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "";
            sqlStmt += "        SELECT " + Environment.NewLine;
            sqlStmt += "               ClassSchedule.ClassScheduleId AS ClassSchedule_ClassScheduleId" + Environment.NewLine;
            sqlStmt += "               ,ClassSchedule.ClientId AS ClassSchedule_ClientId" + Environment.NewLine;
            sqlStmt += "               ,ClassSchedule.ClassScheduleDesc AS ClassSchedule_ClassScheduleDesc" + Environment.NewLine;
            sqlStmt += "              ,ClassSchedule.RegisterDate AS ClassSchedule_RegisterDate" + Environment.NewLine;
            sqlStmt += "              ,ClassSchedule.StartDate AS ClassSchedule_StartDate" + Environment.NewLine;
            sqlStmt += "              ,ClassSchedule.GraduationDate AS ClassSchedule_GraduationDate" + Environment.NewLine;
            sqlStmt += "              ,ClassSchedule.ClassSessionId AS ClassSchedule_ClassSessionId" + Environment.NewLine;
            sqlStmt += "              ,ClassSession.ClassSessionId AS ClassSession_ClassSessionId" + Environment.NewLine;
            sqlStmt += "              ,ClassSession.ClientId AS ClassSession_ClientId" + Environment.NewLine;
            sqlStmt += "              ,ClassSession.ClassSessionDesc AS ClassSession_ClassSessionDesc" + Environment.NewLine;
            sqlStmt += "              ,ClassSession.SeqNum AS ClassSession_SeqNum" + Environment.NewLine;
            sqlStmt += "              ,ClassSession.ClassListId AS ClassSession_ClassListId" + Environment.NewLine;
            sqlStmt += "              ,ClassList.ClassListId AS ClassList_ClassListId" + Environment.NewLine;
            sqlStmt += "              ,ClassList.ClientId AS ClassList_ClientId" + Environment.NewLine;
            sqlStmt += "              ,ClassList.ClassListDesc AS ClassList_ClassListDesc" + Environment.NewLine;
            sqlStmt += "          FROM SchoolPrdSch.ClassSchedule" + Environment.NewLine;
            sqlStmt += "    INNER JOIN SchoolPrdSch.ClassSession" + Environment.NewLine;
            sqlStmt += "            ON ClassSchedule.ClassSessionId = ClassSession.ClassSessionId" + Environment.NewLine;
            sqlStmt += "    INNER JOIN SchoolPrdSch.ClassList" + Environment.NewLine;
            sqlStmt += "            ON ClassSession.ClassListId = ClassList.ClassListId" + Environment.NewLine;
            sqlStmt += "       ORDER BY ClassSession.SeqNum" + Environment.NewLine;
            sqlStmt += "            ,ClassSchedule.StartDate" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            return sqlCommand;
        }
        public static SqlCommand BuildClassFeesSelect(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "";
            sqlStmt += "        SELECT " + Environment.NewLine;
            sqlStmt += "               ClassFees.ClassFeesId AS ClassFees_ClassFeesId" + Environment.NewLine;
            sqlStmt += "              ,ClassFees.ClientId AS ClassFees_ClientId" + Environment.NewLine;
            sqlStmt += "              ,ClassFees.ClassFeesDesc AS ClassFees_ClassFeesDesc" + Environment.NewLine;
            sqlStmt += "              ,ClassFees.DiscountAmount AS ClassFees_DiscountAmount" + Environment.NewLine;
            sqlStmt += "              ,ClassFees.FeesAmount AS ClassFees_FeesAmount" + Environment.NewLine;
            sqlStmt += "              ,ClassFees.SeqNum AS ClassFees_SeqNum" + Environment.NewLine;
            sqlStmt += "              ,ClassFees.ClassListId AS ClassFees_ClassListId" + Environment.NewLine;
            sqlStmt += "              ,ClassList.ClassListId AS ClassList_ClassListId" + Environment.NewLine;
            sqlStmt += "              ,ClassList.ClientId AS ClassList_ClientId" + Environment.NewLine;
            sqlStmt += "              ,ClassList.ClassListDesc AS ClassList_ClassListDesc" + Environment.NewLine;
            sqlStmt += "          FROM SchoolPrdSch.ClassFees" + Environment.NewLine;
            sqlStmt += "    INNER JOIN SchoolPrdSch.ClassList" + Environment.NewLine;
            sqlStmt += "            ON ClassFees.ClassListId = ClassList.ClassListId" + Environment.NewLine;
            sqlStmt += "    ORDER BY ClassList.ClassListId" + Environment.NewLine;
            sqlStmt += "            ,ClassFees.SeqNum" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            return sqlCommand;
        }
        private static SqlCommand BuildSelectClassSession(SqlConnection sqlConnection)
        {
            string sqlStmt = "SELECT * FROM SchoolPrdSch.ClassSession WHERE ClassSessionId = @ClassSessionId";
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ClassSessionId", SqlDbType.BigInt);
            return sqlCommand;
        }
        private static SqlCommand BuildClassSessionInsert(SqlConnection sqlConnection)
        {
            string sqlStmt = "INSERT SchoolPrdSch.ClassSession(ClassSessionDesc, ClassListId, SeqNum, AddUserId, UpdUserId)";
            sqlStmt += "OUTPUT INSERTED.ClassSessionId SELECT @ClassSessionDesc, @ClassListId, @SeqNum, @LoggedInUserId, @LoggedInUserId" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ClassSessionDesc", SqlDbType.NVarChar, 250);
            sqlCommand.Parameters.Add("@ClassListId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@SeqNum", SqlDbType.Float);
            sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 250);
            return sqlCommand;
        }
        private static SqlCommand BuildClassSessionUpdate(SqlConnection sqlConnection)
        {
            string sqlStmt = "UPDATE SchoolPrdSch.ClassSession SET ClassSessionDesc = @ClassSessionDesc, ClassListId = @ClassListId, UpdUserId = @LoggedInUserId, UpdUserName = SUSER_NAME(), UpdDateTime = GETDATE() WHERE ClassSessionId = @ClassSessionId";
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ClassSessionId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@ClassListId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@ClassSessionDesc", SqlDbType.NVarChar, 250);
            sqlCommand.Parameters.Add("@SeqNum", SqlDbType.Float);
            sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 250);
            return sqlCommand;
        }
        private static SqlCommand BuildSelectClassSchedule(SqlConnection sqlConnection)
        {
            string sqlStmt = "SELECT * FROM SchoolPrdSch.ClassSchedule WHERE ClassScheduleId = @ClassScheduleId";
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ClassScheduleId", SqlDbType.BigInt);
            return sqlCommand;
        }
        private static SqlCommand BuildClassScheduleInsert(SqlConnection sqlConnection)
        {
            string sqlStmt = "INSERT SchoolPrdSch.ClassSchedule(ClassSessionId, GraduationDate, RegisterDate, StartDate, StatusId, AddUserId, UpdUserId)";
            sqlStmt += "OUTPUT INSERTED.ClassScheduleId SELECT @ClassSessionId, @GraduationDate, @RegisterDate, @StartDate, @StatusId, @LoggedInUserId, @LoggedInUserId" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ClassSessionId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@GraduationDate", SqlDbType.Date); 
            sqlCommand.Parameters.Add("@RegisterDate", SqlDbType.Date);
            sqlCommand.Parameters.Add("@StartDate", SqlDbType.Date);
            sqlCommand.Parameters.Add("@StatusId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 250);
            return sqlCommand;
        }
        private static SqlCommand BuildClassScheduleUpdate(SqlConnection sqlConnection)
        {
            string sqlStmt = "UPDATE SchoolPrdSch.ClassSchedule SET ClassSessionId = @ClassSessionId, GraduationDate = @GraduationDate, RegisterDate = @RegisterDate, StartDate = @StartDate, UpdUserId = @LoggedInUserId, UpdUserName = SUSER_NAME(), UpdDateTime = GETDATE() WHERE ClassScheduleId = @ClassScheduleId";
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ClassScheduleId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@ClassSessionId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@GraduationDate", SqlDbType.NVarChar, 10);
            sqlCommand.Parameters.Add("@RegisterDate", SqlDbType.NVarChar, 10);
            sqlCommand.Parameters.Add("@StartDate", SqlDbType.NVarChar, 10);
            sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 250);
            return sqlCommand;
        }
        private static SqlCommand BuildSelectClassFees(SqlConnection sqlConnection)
        {
            string sqlStmt = "SELECT * FROM SchoolPrdSch.ClassFees WHERE ClassFeesId = @ClassFeesId";
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ClassFeesId", SqlDbType.BigInt);
            return sqlCommand;
        }
        private static SqlCommand BuildClassFeesInsert(SqlConnection sqlConnection)
        {
            string sqlStmt = "INSERT SchoolPrdSch.ClassFees(ClassFeesDesc, ClassListId, DiscountAmount, FeesAmount, SeqNum, AddUserId, UpdUserId)";
            sqlStmt += "OUTPUT INSERTED.ClassFeesId SELECT @ClassFeesDesc, @ClassListId, @DiscountAmount, @FeesAmount, @SeqNum, @LoggedInUserId, @LoggedInUserId" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ClassFeesDesc", SqlDbType.NVarChar, 250);
            sqlCommand.Parameters.Add("@ClassListId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@DiscountAmount", SqlDbType.NVarChar, 250);
            sqlCommand.Parameters.Add("@FeesAmount", SqlDbType.NVarChar, 250);
            sqlCommand.Parameters.Add("@SeqNum", SqlDbType.Float);
            sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 250);
            return sqlCommand;
        }
        private static SqlCommand BuildClassFeesUpdate(SqlConnection sqlConnection)
        {
            string sqlStmt = "UPDATE SchoolPrdSch.ClassFees SET ClassFeesDesc = @ClassFeesDesc, ClassListId = @ClassListId, DiscountAmount = @DiscountAmount, FeesAmount = @FeesAmount, SeqNum = @SeqNum, UpdUserId = @LoggedInUserId, UpdUserName = SUSER_NAME(), UpdDateTime = GETDATE() WHERE ClassFeesId = @ClassFeesId";
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ClassFeesId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@ClassListId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@ClassFeesDesc", SqlDbType.NVarChar, 250);
            sqlCommand.Parameters.Add("@DiscountAmount", SqlDbType.NVarChar, 250);
            sqlCommand.Parameters.Add("@FeesAmount", SqlDbType.NVarChar, 250);
            sqlCommand.Parameters.Add("@SeqNum", SqlDbType.NVarChar, 250);
            sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 250);
            return sqlCommand;
        }
        private static SqlCommand BuildSelectHoliday(SqlConnection sqlConnection)
        {
            string sqlStmt = "SELECT * FROM SchoolPrdSch.Holiday WHERE HolidayId = @HolidayId";
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@HolidayId", SqlDbType.BigInt);
            return sqlCommand;
        }
        private static SqlCommand BuildHolidayInsert(SqlConnection sqlConnection)
        {
            string sqlStmt = "INSERT SchoolPrdSch.Holiday(HolidayName, HolidayDate, HolidayDescription, AddUserId, UpdUserId)";
            sqlStmt += "OUTPUT INSERTED.HolidayId SELECT @HolidayName, @HolidayDate, @HolidayDescription, @LoggedInUserId, @LoggedInUserId" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@HolidayName", SqlDbType.NVarChar, 250);
            sqlCommand.Parameters.Add("@HolidayDate", SqlDbType.NVarChar, 250);
            sqlCommand.Parameters.Add("@HolidayDescription", SqlDbType.NVarChar, 250);
            sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 250);
            return sqlCommand;
        }
        private static SqlCommand BuildHolidayUpdate(SqlConnection sqlConnection)
        {
            string sqlStmt = "UPDATE SchoolPrdSch.Holiday SET HolidayName = @HolidayName, HolidayDate = @HolidayDate, HolidayDescription = @HolidayDescription, UpdUserId = @LoggedInUserId, UpdUserName = SUSER_NAME(), UpdDateTime = GETDATE() WHERE HolidayId = @HolidayId";
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@HolidayId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@HolidayName", SqlDbType.NVarChar, 250);
            sqlCommand.Parameters.Add("@HolidayDate", SqlDbType.NVarChar, 250);
            sqlCommand.Parameters.Add("@HolidayDescription", SqlDbType.NVarChar, 250);
            sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 250);
            return sqlCommand;
        }
        private static SqlCommand BuildClassSessionMaxSeqNumGet(SqlConnection sqlConnection)
        {
            string sqlStmt = "SELECT MAX(ClassSession.SeqNum) FROM SchoolPrdSch.ClassSession WHERE ClassSession.ClassListId = @ClassListId";
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ClassListId", SqlDbType.BigInt);
            return sqlCommand;
        }
        private static SqlCommand BuildClassFeesMaxSeqNumGet(SqlConnection sqlConnection)
        {
            string sqlStmt = "SELECT MAX(ClassFees.SeqNum) FROM SchoolPrdSch.ClassFees WHERE ClassFees.ClassListId = @ClassListId";
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ClassListId", SqlDbType.BigInt);
            return sqlCommand;
        }
        private static SqlCommand BuildSelectClassEnrollFromPersonId(SqlConnection sqlConnection)
        {
            string sqlStmt = "";
            sqlStmt += "        SELECT ClassEnroll.*" + Environment.NewLine;
            sqlStmt += "          FROM SchoolPrdSch.ClassEnroll" + Environment.NewLine;
            sqlStmt += "         WHERE ClassEnrollId IN" + Environment.NewLine;
            sqlStmt += "               (" + Environment.NewLine;
            sqlStmt += "                 SELECT MAX(ClassEnrollId) FROM SchoolPrdSch.ClassEnroll WHERE PersonId = @PersonId" + Environment.NewLine;
            sqlStmt += "               )" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@PersonId", SqlDbType.BigInt);
            return sqlCommand;
        }
        private static SqlCommand BuildClassEnrollInsert(SqlConnection sqlConnection)
        {
            string sqlStmt = "INSERT SchoolPrdSch.ClassEnroll(FundingSoureName, ClassScheduleId, CatalogInitialsSignaturesCount, CancelDate, CourseCompletionDate, DMVTestDate, EnrollmentAgreementInitialsSignaturesCount, ClassEnrollStatusId, FundingRequired, PerformanceFactSheetInitialsSignaturesCount, PersonId, AddUserId, UpdUserId)";
            sqlStmt += "OUTPUT INSERTED.ClassEnrollId SELECT @FundingSoureName, @ClassScheduleId, @CatalogInitialsSignaturesCount, @CancelDate, @CourseCompletionDate, @DMVTestDate, @EnrollmentAgreementInitialsSignaturesCount, @ClassEnrollStatusId, @FundingRequired, @PerformanceFactSheetInitialsSignaturesCount, @PersonId, @LoggedInUserId, @LoggedInUserId" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@FundingSoureName", SqlDbType.NVarChar, 250);
            sqlCommand.Parameters.Add("@ClassScheduleId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@CatalogInitialsSignaturesCount", SqlDbType.Int);
            sqlCommand.Parameters.Add("@CancelDate", SqlDbType.NVarChar, 250);
            sqlCommand.Parameters.Add("@CourseCompletionDate", SqlDbType.NVarChar, 250);
            sqlCommand.Parameters.Add("@DMVTestDate", SqlDbType.NVarChar, 250);
            sqlCommand.Parameters.Add("@EnrollmentAgreementInitialsSignaturesCount", SqlDbType.Int);
            sqlCommand.Parameters.Add("@ClassEnrollStatusId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@FundingRequired", SqlDbType.Bit);
            sqlCommand.Parameters.Add("@PerformanceFactSheetInitialsSignaturesCount", SqlDbType.Int);
            sqlCommand.Parameters.Add("@PersonId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 250);
            return sqlCommand;
        }
        private static SqlCommand BuildClassEnrollUpdate(SqlConnection sqlConnection)
        {
            string sqlStmt = "UPDATE SchoolPrdSch.ClassEnroll SET FundingSoureName = @FundingSoureName, ClassScheduleId = @ClassScheduleId, CatalogInitialsSignaturesCount = @CatalogInitialsSignaturesCount, CancelDate = @CancelDate, CourseCompletionDate = @CourseCompletionDate,  DMVTestDate = @DMVTestDate, EnrollmentAgreementInitialsSignaturesCount = @EnrollmentAgreementInitialsSignaturesCount, ClassEnrollStatusId = @ClassEnrollStatusId, FundingRequired = @FundingRequired, PerformanceFactSheetInitialsSignaturesCount = @PerformanceFactSheetInitialsSignaturesCount, PersonId = @PersonId, UpdUserId = @LoggedInUserId, UpdUserName = SUSER_NAME(), UpdDateTime = GETDATE() WHERE ClassEnrollId = @ClassEnrollId";
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ClassEnrollId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@ClassScheduleId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@FundingSoureName", SqlDbType.NVarChar, 250);
            sqlCommand.Parameters.Add("@CatalogInitialsSignaturesCount", SqlDbType.Int);
            sqlCommand.Parameters.Add("@CancelDate", SqlDbType.NVarChar, 250);
            sqlCommand.Parameters.Add("@CourseCompletionDate", SqlDbType.NVarChar, 250);
            sqlCommand.Parameters.Add("@DMVTestDate", SqlDbType.NVarChar, 250);
            sqlCommand.Parameters.Add("@EnrollmentAgreementInitialsSignaturesCount", SqlDbType.Int);
            sqlCommand.Parameters.Add("@ClassEnrollStatusId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@FundingRequired", SqlDbType.Bit);
            sqlCommand.Parameters.Add("@PerformanceFactSheetInitialsSignaturesCount", SqlDbType.Int);
            sqlCommand.Parameters.Add("@PersonId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 250);
            return sqlCommand;
        }
        private static SqlCommand BuildClassEnrollFeesInsert(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "";
            sqlStmt += "        INSERT SchoolPrdSch.ClassEnrollFees" + Environment.NewLine;
            sqlStmt += "              (" + Environment.NewLine;
            sqlStmt += "               AmountPaid" + Environment.NewLine;
            sqlStmt += "              ,ClassEnrollFeesAmount" + Environment.NewLine;
            sqlStmt += "              ,ClassEnrollFeesDesc" + Environment.NewLine;
            sqlStmt += "              ,ClassEnrollId" + Environment.NewLine;
            sqlStmt += "              ,ClassFeesId" + Environment.NewLine;
            sqlStmt += "              ,ClassFeesTypeId" + Environment.NewLine;
            sqlStmt += "              ,DatePaid" + Environment.NewLine;
            sqlStmt += "              ,DueDate" + Environment.NewLine;
            sqlStmt += "              ,AddUserId" + Environment.NewLine;
            sqlStmt += "              ,UpdUserId" + Environment.NewLine;
            sqlStmt += "              )" + Environment.NewLine;
            sqlStmt += "        OUTPUT INSERTED.ClassEnrollFeesId" + Environment.NewLine;
            sqlStmt += "        SELECT" + Environment.NewLine;
            sqlStmt += "               @AmountPaid" + Environment.NewLine;
            sqlStmt += "              ,@ClassEnrollFeesAmount" + Environment.NewLine;
            sqlStmt += "              ,@ClassEnrollFeesDesc" + Environment.NewLine;
            sqlStmt += "              ,@ClassEnrollId" + Environment.NewLine;
            sqlStmt += "              ,@ClassFeesId" + Environment.NewLine;
            sqlStmt += "              ,@ClassFeesTypeId" + Environment.NewLine;
            sqlStmt += "              ,@DueDate" + Environment.NewLine;
            sqlStmt += "              ,@DatePaid" + Environment.NewLine;
            sqlStmt += "              ,@LoggedInUserId" + Environment.NewLine;
            sqlStmt += "              ,@LoggedInUserId" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@AmountPaid", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ClassEnrollFeesAmount", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ClassEnrollFeesDesc", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ClassEnrollId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ClassFeesId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ClassFeesTypeId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@DatePaid", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@DueDate", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@LoggedInUserId", System.DBNull.Value);
            return sqlCommand;
        }
        private static SqlCommand BuildSignatureInsert(SqlConnection sqlConnection)
        {
            string sqlStmt = "INSERT SchoolPrdSch.Signature(HtmlContent, InitialSignatureTotalCount, InitialSignatureSignedCount, AddUserId, UpdUserId)";
            sqlStmt += "OUTPUT INSERTED.SignatureId SELECT  @HtmlContent, @InitialSignatureTotalCount, @InitialSignatureSignedCount, @LoggedInUserId, @LoggedInUserId" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@HtmlContent", SqlDbType.NVarChar, 250);
            sqlCommand.Parameters.Add("@InitialSignatureTotalCount", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@InitialSignatureSignedCount", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 250);
            return sqlCommand;
        }
        private static SqlCommand BuildClassEnrollInitialSignatureInsert(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "INSERT SchoolPrdSch.ClassEnrollInitialSignature(ClientId, ClassEnrollId, ClientDateTime, DocumentTypeNameDesc, InitialsSignatureId, SeqNum, ServerDateTime, AddUserId, UpdUserId)";
            sqlStmt += "OUTPUT INSERTED.ClassEnrollInitialSignatureId SELECT @ClientId, @ClassEnrollId, @ClientDateTime, @DocumentTypeNameDesc, @InitialsSignatureId, @SeqNum, @ServerDateTime,  @LoggedInUserId, @LoggedInUserId" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ClientId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@ClassEnrollId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@ClientDateTime", SqlDbType.DateTime);
            sqlCommand.Parameters.Add("@DocumentTypeNameDesc", SqlDbType.NVarChar, 50);
            sqlCommand.Parameters.Add("@InitialsSignatureId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@SeqNum", SqlDbType.Float);
            sqlCommand.Parameters.Add("@ServerDateTime", SqlDbType.DateTime);
            sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 256);
            return sqlCommand;
        }
        private static SqlCommand BuildClassEnrollInitialSignatureDelete(SqlConnection sqlConnection)
        {
            string sqlStmt = "DELETE SchoolPrdSch.ClassEnrollInitialSignature WHERE ClassEnrollId = @ClassEnrollId";
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ClassEnrollId", SqlDbType.BigInt);
            return sqlCommand;
        }
        private static SqlCommand BuildClassEnrollInitialAgreementDelete(SqlConnection sqlConnection)
        {
            string sqlStmt = "DELETE SchoolPrdSch.ClassEnrollInitialSignature WHERE ClassEnrollId = @ClassEnrollId AND DocumentTypeNameDesc = 'Agreement'";
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ClassEnrollId", SqlDbType.BigInt);
            return sqlCommand;
        }
        private static SqlCommand BuildClassEnrollInitialSignatureUpdate(SqlConnection sqlConnection)
        {
            string sqlStmt = "UPDATE SchoolPrdSch.ClassEnroll SET CatalogInitialsSignaturesCount = @CatalogInitialsSignaturesCount, PerformanceFactSheetInitialsSignaturesCount = @PerformanceFactSheetInitialsSignaturesCount, UpdUserId = @LoggedInUserId, UpdUserName = SUSER_NAME(), UpdDateTime = GETDATE() WHERE ClassEnrollId = @ClassEnrollId";
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ClassEnrollId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@CatalogInitialsSignaturesCount", SqlDbType.Int);
            sqlCommand.Parameters.Add("@PerformanceFactSheetInitialsSignaturesCount", SqlDbType.Int);
            sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 250);
            return sqlCommand;
        }
        private static SqlCommand BuildClassEnrollInitialAgreementUpdate(SqlConnection sqlConnection)
        {
            string sqlStmt = "UPDATE SchoolPrdSch.ClassEnroll SET EnrollmentAgreementInitialsSignaturesCount = @EnrollmentAgreementInitialsSignaturesCount, UpdUserId = @LoggedInUserId, UpdUserName = SUSER_NAME(), UpdDateTime = GETDATE() WHERE ClassEnrollId = @ClassEnrollId";
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ClassEnrollId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@EnrollmentAgreementInitialsSignaturesCount", SqlDbType.Int);
            sqlCommand.Parameters.Add("@LoggedInUserId", SqlDbType.NVarChar, 250);
            return sqlCommand;
        }
    }
}
