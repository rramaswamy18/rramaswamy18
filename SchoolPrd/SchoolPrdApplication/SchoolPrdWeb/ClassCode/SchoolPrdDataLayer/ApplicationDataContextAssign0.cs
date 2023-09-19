using ArchitectureLibraryEnumerations;
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
        private static ClassListModel AssignClassList(SqlDataReader sqlDataReader, long clientId, string ipAddress, string exceUniqueId, string loggedInUserId)
        {
            ClassListModel classListModel = new ClassListModel
            {
                ClassListId = long.Parse(sqlDataReader["ClassListId"].ToString()),
                ClassListDesc = sqlDataReader["ClassListDesc"].ToString(),
            };
            return classListModel;
        }
        private static void AssignClassList(ClassListModel classListModel, SqlCommand sqlCommand, long clientId, string ipAddress, string exceUniqueId, string loggedInUserId)
        {
            sqlCommand.Parameters["@ClassListDesc"].Value = classListModel.ClassListDesc;
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
        }

        //Review below
        private static ClassDetailModel AssignClassDetail(SqlDataReader sqlDataReader, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            ClassDetailModel classDetailModel = new ClassDetailModel
            {
                ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                ClassDetailId = long.Parse(sqlDataReader["ClassDetailId"].ToString()),
                ClassDetailDesc = sqlDataReader["ClassDetailDesc"].ToString(),
                ClassSessionId = long.Parse(sqlDataReader["ClassSessionId"].ToString()),
                FinishTime = sqlDataReader["FinishTime"].ToString(),
                SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                StartTime = sqlDataReader["StartTime"].ToString(),
            };
            return classDetailModel;
        }
        private static ClassFeesModel AssignClassFees(SqlDataReader sqlDataReader, long clientId, string ipAddress, string execUniqueId, string loggedInUserId, string prefixString = "")
        {
            ClassFeesModel classFeesModel = new ClassFeesModel
            {
                ClientId = long.Parse(sqlDataReader[prefixString + "ClientId"].ToString()),
                ClassFeesId = long.Parse(sqlDataReader[prefixString + "ClassFeesId"].ToString()),
                ClassFeesDesc = sqlDataReader[prefixString + "ClassFeesDesc"].ToString(),
                ClassFeesTypeId = (ClassFeesTypeEnum)int.Parse(sqlDataReader[prefixString + "ClassFeesTypeId"].ToString()),
                ClassListId = long.Parse(sqlDataReader[prefixString + "ClassListId"].ToString()),
                ClassFeesAmount = float.Parse(sqlDataReader[prefixString + "ClassFeesAmount"].ToString()),
                //DiscountAmount = sqlDataReader[prefixString + "DiscountAmount"].ToString() == "" ? (short?)null : short.Parse(sqlDataReader[prefixString + "DiscountAmount"].ToString()),
                //FeesAmount = sqlDataReader[prefixString + "FeesAmount"].ToString() == "" ? (short?)null : short.Parse(sqlDataReader[prefixString + "FeesAmount"].ToString()),
                SeqNum = float.Parse(sqlDataReader[prefixString + "SeqNum"].ToString()),
            };
            return classFeesModel;
        }
        private static ClassListModel AssignClassList(SqlDataReader sqlDataReader, long clientId, string ipAddress, string execUniqueId, string loggedInUserId, string prefixString = "")
        {
            ClassListModel classListModel = new ClassListModel
            {
                ClientId = long.Parse(sqlDataReader[prefixString + "ClientId"].ToString()),
                ClassListId = long.Parse(sqlDataReader[prefixString + "ClassListId"].ToString()),
                ClassListDesc = sqlDataReader[prefixString + "ClassListDesc"].ToString(),
            };
            return classListModel;
        }
        private static ClassScheduleModel AssignClassSchedule(SqlDataReader sqlDataReader, long clientId, string ipAddress, string execUniqueId, string loggedInUserId, string prefixString = "")
        {
            ClassScheduleModel classScheduleModel = new ClassScheduleModel
            {
                ClientId = long.Parse(sqlDataReader[prefixString + "ClientId"].ToString()),
                ClassScheduleId = long.Parse(sqlDataReader[prefixString + "ClassScheduleId"].ToString()),
                ClassScheduleDesc = sqlDataReader[prefixString + "ClassScheduleDesc"].ToString(),
                ClassSessionId = long.Parse(sqlDataReader[prefixString + "ClassSessionId"].ToString()),
                GraduationDate = DateTime.Parse(sqlDataReader[prefixString + "GraduationDate"].ToString()).ToString("MM/dd/yyyy"),
                RegisterDate = DateTime.Parse(sqlDataReader[prefixString + "RegisterDate"].ToString()).ToString("MM/dd/yyyy"),
                StartDate = DateTime.Parse(sqlDataReader[prefixString + "StartDate"].ToString()).ToString("MM/dd/yyyy"),
            };
            return classScheduleModel;
        }
        private static ClassSessionModel AssignClassSession(SqlDataReader sqlDataReader, long clientId, string ipAddress, string execUniqueId, string loggedInUserId, string prefixString = "")
        {
            ClassSessionModel classSessionModel = new ClassSessionModel
            {
                ClientId = long.Parse(sqlDataReader[prefixString + "ClientId"].ToString()),
                ClassSessionId = long.Parse(sqlDataReader[prefixString + "ClassSessionId"].ToString()),
                ClassSessionDesc = sqlDataReader[prefixString + "ClassSessionDesc"].ToString(),
                ClassListId = long.Parse(sqlDataReader[prefixString + "ClassListId"].ToString()),
                SeqNum = float.Parse(sqlDataReader[prefixString + "SeqNum"].ToString()),
            };
            return classSessionModel;
        }
        private static void AssignClassEnroll(ClassEnrollModel classEnrollModel, SqlCommand sqlCommand, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            sqlCommand.Parameters["@ClientId"].Value = clientId;
            sqlCommand.Parameters["@CatalogInitialsCount"].Value = classEnrollModel.CatalogInitialsCount;
            sqlCommand.Parameters["@CatalogSignaturesCount"].Value = classEnrollModel.CatalogSignaturesCount;
            sqlCommand.Parameters["@ClassEnrollStatusId"].Value = (int)classEnrollModel.ClassEnrollStatusId;
            sqlCommand.Parameters["@ClassScheduleId"].Value = classEnrollModel.ClassScheduleId;
            sqlCommand.Parameters["@EnrollmentAgreementInitialsCount"].Value = classEnrollModel.EnrollmentAgreementInitialsCount;
            sqlCommand.Parameters["@EnrollmentAgreementSignaturesCount"].Value = classEnrollModel.EnrollmentAgreementSignaturesCount;
            sqlCommand.Parameters["@ClassEnrollStatusId"].Value = classEnrollModel.ClassEnrollStatusId;
            sqlCommand.Parameters["@FundingRequired"].Value = (int)classEnrollModel.FundingRequired;
            sqlCommand.Parameters["@FundingSoureName"].Value = string.IsNullOrEmpty(classEnrollModel.FundingSoureName) ? (object)DBNull.Value : classEnrollModel.FundingSoureName;
            sqlCommand.Parameters["@PerformanceFactSheetInitialsCount"].Value = classEnrollModel.PerformanceFactSheetInitialsCount;
            sqlCommand.Parameters["@PerformanceFactSheetSignaturesCount"].Value = classEnrollModel.PerformanceFactSheetSignaturesCount;
            sqlCommand.Parameters["@PersonId"].Value = classEnrollModel.PersonId;
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
        }
        private static void AssignEnrollment(EnrollmentModel enrollmentModel, SqlCommand sqlCommand, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            sqlCommand.Parameters["@FirstName"].Value = enrollmentModel.FirstName;
            sqlCommand.Parameters["@MiddleName"].Value = enrollmentModel.MiddleName ?? (object)DBNull.Value;// : enrollmentModel.MiddleName;
            sqlCommand.Parameters["@LastName"].Value = enrollmentModel.LastName;
            sqlCommand.Parameters["@EmailAddress"].Value = enrollmentModel.EmailAddress;
            sqlCommand.Parameters["@TelephoneNumber"].Value = enrollmentModel.TelephoneNumber;
            sqlCommand.Parameters["@ContactMessage"].Value = enrollmentModel.ContactMessage ?? (object)DBNull.Value;// : enrollmentModel.ContactMessage;
            sqlCommand.Parameters["@DemogInfoAddressId"].Value = enrollmentModel.DemogInfoAddressId == null ? 0 : enrollmentModel.DemogInfoAddressId;
            sqlCommand.Parameters["@DrugScreenId"].Value = enrollmentModel.DrugScreenId;
            sqlCommand.Parameters["@EnrollmentTypeIds"].Value = enrollmentModel.EnrollmentTypeIds;
            sqlCommand.Parameters["@EnrollmentTypeDescs"].Value = enrollmentModel.EnrollmentTypeDescs;
            sqlCommand.Parameters["@AddressLine1"].Value = enrollmentModel.AddressLine1;
            sqlCommand.Parameters["@AddressLine2"].Value = enrollmentModel.AddressLine2 ?? (object)DBNull.Value;// : enrollmentModel.AddressLine2;
            sqlCommand.Parameters["@CityName"].Value = enrollmentModel.CityName;
            sqlCommand.Parameters["@DemogInfoSubDivisionId"].Value = enrollmentModel.DemogInfoSubDivisionId;
            sqlCommand.Parameters["@DrivingLicenseDemogInfoSubDivisionId"].Value = enrollmentModel.DrivingLicenseDemogInfoSubDivisionId;
            sqlCommand.Parameters["@ZipCode"].Value = enrollmentModel.ZipCode;
        }
        private static HolidayModel AssignHoliday(SqlDataReader sqlDataReader, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            HolidayModel HolidayModel = new HolidayModel
            {
                ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                HolidayId = long.Parse(sqlDataReader["HolidayId"].ToString()),
                HolidayName = sqlDataReader["HolidayName"].ToString(),
                HolidayDate = sqlDataReader["HolidayDate"].ToString(),
                HolidayDescription = sqlDataReader["HolidayDescription"].ToString(),
            };
            return HolidayModel;
        }
        private static InitialSignatureModel AssignInitialSignature(SqlDataReader sqlDataReader, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            InitialSignatureModel InitialSignatureModel = new InitialSignatureModel
            {
                ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                InitialSignatureId = long.Parse(sqlDataReader["InitialSignatureId"].ToString()),
                //ApplyClearDocumentElementPrefix = sqlDataReader["ApplyClearDocumentElementPrefix"].ToString(),
                DocumentTypeNameDesc = sqlDataReader["DocumentTypeNameDesc"].ToString(),
                TabName = sqlDataReader["TabName"].ToString(),
                //InitialSignatureDocumentElementPrefix = sqlDataReader["InitialSignatureDocumentElementPrefix"].ToString(),
            };
            return InitialSignatureModel;
        }
        private static InitialSignatureDetailModel AssignInitialSignatureDetail(SqlDataReader sqlDataReader, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            InitialSignatureDetailModel initialSignatureDetailModel = new InitialSignatureDetailModel
            {
                ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                Height = sqlDataReader["Height"].ToString(),
                InitialSignatureId = long.Parse(sqlDataReader["InitialSignatureId"].ToString()),
                InitialSignatureDetailId = long.Parse(sqlDataReader["InitialSignatureDetailId"].ToString()),
                InitialSignatureTypeNameDesc = sqlDataReader["InitialSignatureTypeNameDesc"].ToString(),
                SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                Width = sqlDataReader["Width"].ToString(),
            };
            return initialSignatureDetailModel;
        }
        private static ClassSessionModel AssignClassSession(SqlDataReader sqlDataReader, long clientId, string ipAddress, string exceUniqueId, string loggedInUserId)
        {
            ClassSessionModel classSessionModel = new ClassSessionModel
            {
                ClassSessionId = long.Parse(sqlDataReader["ClassSessionId"].ToString()),
                ClassListId = long.Parse(sqlDataReader["ClassListId"].ToString()),
                ClassSessionDesc = sqlDataReader["ClassSessionDesc"].ToString(),
                SeqNum = sqlDataReader["SeqNum"].ToString() == "" ? (float?)null : float.Parse(sqlDataReader["SeqNum"].ToString()),
            };
            return classSessionModel;
        }
        private static void AssignClassSession(ClassSessionModel classSessionModel, SqlCommand sqlCommand, string ipAddress, string exceUniqueId, string loggedInUserId)
        {
            sqlCommand.Parameters["@ClassListId"].Value = classSessionModel.ClassListId;
            sqlCommand.Parameters["@ClassSessionDesc"].Value = classSessionModel.ClassSessionDesc;
            sqlCommand.Parameters["@SeqNum"].Value = classSessionModel.SeqNum;
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
        }
        private static ClassScheduleModel AssignClassSchedule(SqlDataReader sqlDataReader, long clientId, string ipAddress, string exceUniqueId, string loggedInUserId)
        {
            ClassScheduleModel classScheduleModel = new ClassScheduleModel
            {
                ClassScheduleId = long.Parse(sqlDataReader["ClassScheduleId"].ToString()),
                ClassSessionId = long.Parse(sqlDataReader["ClassSessionId"].ToString()),
                ClassScheduleDesc = sqlDataReader["ClassScheduleDesc"].ToString(),
                GraduationDate = DateTime.Parse(sqlDataReader["GraduationDate"].ToString()).ToString("yyyy-MM-dd"),
                RegisterDate = DateTime.Parse(sqlDataReader["RegisterDate"].ToString()).ToString("yyyy-MM-dd"),
                StartDate = DateTime.Parse(sqlDataReader["StartDate"].ToString()).ToString("yyyy-MM-dd"),
                StatusId = (StatusEnum)int.Parse(sqlDataReader["StatusId"].ToString()),
            };
            return classScheduleModel;
        }
        private static void AssignClassSchedule(ClassScheduleModel classScheduleModel, SqlCommand sqlCommand, string ipAddress, string exceUniqueId, string loggedInUserId)
        {
            sqlCommand.Parameters["@ClassSessionId"].Value = classScheduleModel.ClassSessionId;
            sqlCommand.Parameters["@GraduationDate"].Value = classScheduleModel.GraduationDate;
            sqlCommand.Parameters["@StartDate"].Value = classScheduleModel.StartDate;
            sqlCommand.Parameters["@RegisterDate"].Value = classScheduleModel.RegisterDate;
            sqlCommand.Parameters["@StatusId"].Value = (long)classScheduleModel.StatusId;
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
        }
        private static ClassFeesModel AssignClassFees(SqlDataReader sqlDataReader, long clientId, string ipAddress, string exceUniqueId, string loggedInUserId)
        {
            ClassFeesModel classFeesModel = new ClassFeesModel
            {
                ClassFeesId = long.Parse(sqlDataReader["ClassFeesId"].ToString()),
                ClassFeesDesc = sqlDataReader["ClassFeesDesc"].ToString(),
                ClassFeesTypeId = (ClassFeesTypeEnum)int.Parse(sqlDataReader["ClassFeesTypeId"].ToString()),
                ClassFeesAmount = float.Parse(sqlDataReader["ClassFeesAmount"].ToString()),
                ClassListId = long.Parse(sqlDataReader["ClassListId"].ToString()),
                //DiscountAmount = sqlDataReader["DiscountAmount"].ToString() == "" ? (short?)null : short.Parse(sqlDataReader["DiscountAmount"].ToString()),
                //FeesAmount = sqlDataReader["FeesAmount"].ToString() == "" ? (short?)null : short.Parse(sqlDataReader["FeesAmount"].ToString()),
                SeqNum = sqlDataReader["SeqNum"].ToString() == "" ? (float?)null : float.Parse(sqlDataReader["SeqNum"].ToString()),
            };
            return classFeesModel;
        }
        private static void AssignClassFees(ClassFeesModel classFeesModel, SqlCommand sqlCommand, string ipAddress, string exceUniqueId, string loggedInUserId)
        {
            sqlCommand.Parameters["@ClassFeesDesc"].Value = classFeesModel.ClassFeesDesc;
            sqlCommand.Parameters["@ClassListId"].Value = classFeesModel.ClassListId;
            //sqlCommand.Parameters["@DiscountAmount"].Value = classFeesModel.DiscountAmount;
            //sqlCommand.Parameters["@FeesAmount"].Value = classFeesModel.FeesAmount;
            sqlCommand.Parameters["@SeqNum"].Value = classFeesModel.SeqNum;
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
        }
        private static void AssignHoliday(HolidayModel holidayModel, SqlCommand sqlCommand, string ipAddress, string exceUniqueId, string loggedInUserId)
        {
            sqlCommand.Parameters["@HolidayName"].Value = holidayModel.HolidayName;
            sqlCommand.Parameters["@HolidayDate"].Value = holidayModel.HolidayDate;
            sqlCommand.Parameters["@HolidayDescription"].Value = holidayModel.HolidayDescription;
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
        }
        private static ClassEnrollModel AssignClassEnroll(SqlDataReader sqlDataReader, long clientId, string ipAddress, string exceUniqueId, string loggedInUserId)
        {
            ClassEnrollModel classEnrollModel = new ClassEnrollModel
            {
                ClassEnrollId = long.Parse(sqlDataReader["ClassEnrollId"].ToString()),
                FundingSoureName = sqlDataReader["FundingSoureName"].ToString(),
                ClassScheduleId = long.Parse(sqlDataReader["ClassScheduleId"].ToString()),
                CatalogInitialsCount = int.Parse(sqlDataReader["CatalogInitialsCount"].ToString()),
                CatalogSignaturesCount = int.Parse(sqlDataReader["CatalogSignaturesCount"].ToString()),
                CancelDate = sqlDataReader["CancelDate"].ToString() == "" ? null : DateTime.Parse(sqlDataReader["CancelDate"].ToString()).ToString("yyyy-MM-dd"),
                CourseCompletionDate = sqlDataReader["CourseCompletionDate"].ToString() == "" ? null : DateTime.Parse(sqlDataReader["CourseCompletionDate"].ToString()).ToString("yyyy-MM-dd"),
                DMVTestDate = sqlDataReader["DMVTestDate"].ToString() == "" ? null : DateTime.Parse(sqlDataReader["DMVTestDate"].ToString()).ToString("yyyy-MM-dd"),
                EnrollmentAgreementInitialsCount = int.Parse(sqlDataReader["EnrollmentAgreementInitialsCount"].ToString()),
                EnrollmentAgreementSignaturesCount = int.Parse(sqlDataReader["EnrollmentAgreementSignaturesCount"].ToString()),
                ClassEnrollStatusId = (ClassEnrollStatusEnum)int.Parse(sqlDataReader["ClassEnrollStatusId"].ToString()),
                FundingRequired = (YesNoEnum)long.Parse(sqlDataReader["FundingRequired"].ToString()),
                PerformanceFactSheetInitialsCount = int.Parse(sqlDataReader["PerformanceFactSheetInitialsCount"].ToString()),
                PerformanceFactSheetSignaturesCount = int.Parse(sqlDataReader["PerformanceFactSheetSignaturesCount"].ToString()),
                PersonId = long.Parse(sqlDataReader["PersonId"].ToString()),
            };
            return classEnrollModel;
        }
        private static void AssignClassEnroll(ClassEnrollModel classEnrollModel, SqlCommand sqlCommand, string ipAddress, string exceUniqueId, string loggedInUserId)
        {
            sqlCommand.Parameters["@ClassScheduleId"].Value = classEnrollModel.ClassScheduleId;
            sqlCommand.Parameters["@CatalogInitialsCount"].Value = classEnrollModel.CatalogInitialsCount;
            sqlCommand.Parameters["@CatalogSignaturesCount"].Value = classEnrollModel.CatalogSignaturesCount;
            sqlCommand.Parameters["@Canceldate"].Value = string.IsNullOrEmpty(classEnrollModel.CancelDate) ? (object)DBNull.Value : classEnrollModel.CancelDate;
            sqlCommand.Parameters["@CourseCompletionDate"].Value = string.IsNullOrEmpty(classEnrollModel.CourseCompletionDate) ? (object)DBNull.Value : classEnrollModel.CourseCompletionDate;
            sqlCommand.Parameters["@DMVTestDate"].Value = string.IsNullOrEmpty(classEnrollModel.DMVTestDate) ? (object)DBNull.Value : classEnrollModel.DMVTestDate;
            sqlCommand.Parameters["@EnrollmentAgreementInitialsSignaturesCount"].Value = classEnrollModel.EnrollmentAgreementInitialsCount;
            sqlCommand.Parameters["@ClassEnrollStatusId"].Value = classEnrollModel.ClassEnrollStatusId;
            sqlCommand.Parameters["@FundingRequired"].Value = (YesNoEnum)classEnrollModel.FundingRequired;
            sqlCommand.Parameters["@FundingSoureName"].Value = string.IsNullOrEmpty(classEnrollModel.FundingSoureName) ? (object)DBNull.Value : classEnrollModel.FundingSoureName;
            sqlCommand.Parameters["@PerformanceFactSheetInitialsSignaturesCount"].Value = classEnrollModel.PerformanceFactSheetInitialsCount;
            sqlCommand.Parameters["@PersonId"].Value = classEnrollModel.PersonId;
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
        }
        private static void AssignClassEnrollFees(ClassEnrollFeesModel classEnrollFeesModel, SqlCommand sqlCommand, string ipAddress, string exceUniqueId, string loggedInUserId)
        {
            sqlCommand.Parameters["@AmountPaid"].Value = classEnrollFeesModel.AmountPaid == null ? (object)DBNull.Value : classEnrollFeesModel.AmountPaid;
            sqlCommand.Parameters["@ClassEnrollFeesAmount"].Value = classEnrollFeesModel.ClassEnrollFeesAmount;
            sqlCommand.Parameters["@ClassEnrollFeesDesc"].Value = string.IsNullOrEmpty(classEnrollFeesModel.ClassEnrollFeesDesc) ? (object)DBNull.Value : classEnrollFeesModel.ClassEnrollFeesDesc;
            sqlCommand.Parameters["@ClassEnrollId"].Value = classEnrollFeesModel.ClassEnrollId;
            sqlCommand.Parameters["@ClassFeesId"].Value = classEnrollFeesModel.ClassFeesId;
            sqlCommand.Parameters["@ClassFeesTypeId"].Value = (int)classEnrollFeesModel.ClassFeesTypeId;
            sqlCommand.Parameters["@DatePaid"].Value = classEnrollFeesModel.DatePaid == null ? (object)DBNull.Value : classEnrollFeesModel.DatePaid;
            sqlCommand.Parameters["@DueDate"].Value = classEnrollFeesModel.DueDate == null ? (object)DBNull.Value : classEnrollFeesModel.DueDate;
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
        }
    }
}
