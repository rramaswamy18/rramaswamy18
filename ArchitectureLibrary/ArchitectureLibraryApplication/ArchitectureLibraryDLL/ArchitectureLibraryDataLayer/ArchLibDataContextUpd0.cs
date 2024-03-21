using ArchitectureLibraryEnumerations;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryDataLayer
{
    public static partial class ArchLibDataContext
    {
        public static void UpdAspNetUserRole2(string aspNetUserId, string aspNetRoleId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInuserId)
        {
            string sqlStmt = "";
            sqlStmt += "        UPDATE ArchLib.AspNetUserRole" + Environment.NewLine;
            sqlStmt += "           SET AspNetRoleId = @AspNetRoleId" + Environment.NewLine;
            sqlStmt += "              ,UpdUserId = @AspNetUserId" + Environment.NewLine;
            sqlStmt += "              ,UpdUserName = SUSER_NAME()" + Environment.NewLine;
            sqlStmt += "              ,UpdDateTime = GETDATE()" + Environment.NewLine;
            sqlStmt += "         WHERE AspNetUserId = @AspNetUserId" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@AspNetRoleId", SqlDbType.NVarChar, 256);
            sqlCommand.Parameters.Add("@AspNetUserId", SqlDbType.NVarChar, 256);
            sqlCommand.Parameters["@AspNetRoleId"].Value = aspNetRoleId;
            sqlCommand.Parameters["@AspNetUserId"].Value = aspNetUserId;
            sqlCommand.ExecuteNonQuery();
        }
        public static void UpdAspNetUser(AspNetUserModel aspNetUserModel, string aspNetUserId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInuserId)
        {
            SqlCommand sqlCommand = BuildSqlCommandAspNetUserUpd(sqlConnection, clientId, ipAddress, execUniqueId, loggedInuserId);
            sqlCommand.Parameters["@AspNetUserId"].Value = aspNetUserModel.AspNetUserId;
            sqlCommand.Parameters["@LoginPassword"].Value = aspNetUserModel.LoginPassword ?? (object)DBNull.Value;// : aspNetUserModel.LoginPassword;
            sqlCommand.Parameters["@PasswordExpiry"].Value = aspNetUserModel.PasswordExpiry;
            sqlCommand.Parameters["@ResetPasswordCompletedDateTime"].Value = aspNetUserModel.ResetPasswordCompletedDateTime;
            sqlCommand.Parameters["@UserStatusId"].Value = aspNetUserModel.UserStatusId;
            sqlCommand.Parameters["@UpdUserId"].Value = aspNetUserModel.UpdUserId;
            sqlCommand.Parameters["@UpdUserName"].Value = aspNetUserModel.UpdUserName ?? "";// : aspNetUserModel.UpdUserName;
            sqlCommand.ExecuteNonQuery();
        }
        public static void UpdAspNetUser2(string aspNetUserId, string resetPasswordExpiryDateTime, string resetPasswordKey, string resetPasswordQueryString, long userTypeId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInuserId)
        {
            SqlCommand sqlCommand = BuildSqlCommandAspNetUserUpd2(sqlConnection, clientId, ipAddress, execUniqueId, loggedInuserId);
            sqlCommand.Parameters["@ResetPasswordExpiryDateTime"].Value = resetPasswordExpiryDateTime;
            sqlCommand.Parameters["@ResetPasswordKey"].Value = resetPasswordKey;
            sqlCommand.Parameters["@ResetPasswordQueryString"].Value = resetPasswordQueryString;
            sqlCommand.Parameters["@UserTypeId"].Value = userTypeId;
            sqlCommand.Parameters["@UpdUserId"].Value = aspNetUserId;
            sqlCommand.Parameters["@AspNetUserId"].Value = aspNetUserId;
            sqlCommand.ExecuteNonQuery();
        }
        public static void UpdPerson(PersonModel personModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInuserId)
        {
            SqlCommand sqlCommand = BuildSqlCommandPersonUpd(sqlConnection, clientId, ipAddress, execUniqueId, loggedInuserId);
            sqlCommand.Parameters["@ClientId"].Value = clientId;
            sqlCommand.Parameters["@AspNetUserId"].Value = personModel.AspNetUserId;
            sqlCommand.Parameters["@StatusId"].Value = StatusEnum.Active;
            sqlCommand.Parameters["@UpdUserId"].Value = personModel.AspNetUserId;
            sqlCommand.Parameters["@UpdUserName"].Value = personModel.UpdUserName ?? "";// : personModel.UpdUserName;
            sqlCommand.ExecuteNonQuery();
        }
        public static void UpdPerson1(PersonModel personModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInuserId)
        {
            SqlCommand sqlCommand = BuildSqlCommandPersonUpd1(sqlConnection, clientId, ipAddress, execUniqueId, loggedInuserId);
            sqlCommand.Parameters["@CitizenshipId"].Value = personModel.CitizenshipId ?? (object)DBNull.Value;
            sqlCommand.Parameters["@CertificateDocumentId"].Value = personModel.CertificateDocumentId ?? 0;
            sqlCommand.Parameters["@DateOfBirth"].Value = personModel.DateOfBirth ?? (object)DBNull.Value;
            sqlCommand.Parameters["@DriverLicenseDemogInfoSubDivisionId"].Value = personModel.DriverLicenseDemogInfoSubDivisionId ?? (object)DBNull.Value;
            sqlCommand.Parameters["@DriverLicenseExpiryDate"].Value = personModel.DriverLicenseExpiryDate ?? (object)DBNull.Value;
            sqlCommand.Parameters["@DriverLicenseNumber"].Value = personModel.DriverLicenseNumber ?? (object)DBNull.Value;
            sqlCommand.Parameters["@DriverLicenseType"].Value = personModel.DriverLicenseType ?? (object)DBNull.Value;
            sqlCommand.Parameters["@ElectronicSignatureConsent"].Value = (bool)personModel.ElectronicSignatureConsentAccepted ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : (object)DBNull.Value;
            sqlCommand.Parameters["@ElectronicSignatureConsentAccepted"].Value = personModel.ElectronicSignatureConsentAccepted;
            sqlCommand.Parameters["@FirstName"].Value = personModel.FirstName;
            sqlCommand.Parameters["@InitialsTextId"].Value = personModel.InitialsTextId ?? 0;
            sqlCommand.Parameters["@InitialsTextValue"].Value = personModel.InitialsTextValue ?? "";
            sqlCommand.Parameters["@LastName"].Value = personModel.LastName;
            sqlCommand.Parameters["@MaritalStatusId"].Value = personModel.MaritalStatusId ?? (object)DBNull.Value;
            sqlCommand.Parameters["@MiddleName"].Value = personModel.MiddleName ?? (object)DBNull.Value;
            //sqlCommand.Parameters["@MilitaryServiceId"].Value = personModel.MilitaryServiceId;
            sqlCommand.Parameters["@NicknameFirst"].Value = personModel.NicknameFirst;
            sqlCommand.Parameters["@NicknameLast"].Value = personModel.NicknameLast;
            sqlCommand.Parameters["@SalutationId"].Value = personModel.SalutationId;
            sqlCommand.Parameters["@SignatureTextId"].Value = personModel.SignatureTextId ?? 0;
            sqlCommand.Parameters["@SignatureTextValue"].Value = personModel.SignatureTextValue ?? "";
            sqlCommand.Parameters["@SSN"].Value = personModel.SSN ?? (object)DBNull.Value;
            sqlCommand.Parameters["@SuffixId"].Value = personModel.SuffixId;
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInuserId;
            sqlCommand.Parameters["@AspNetUserId"].Value = personModel.AspNetUserId;
            sqlCommand.ExecuteNonQuery();
        }
        public static void UpdPerson2(PersonModel personModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInuserId)
        {
            SqlCommand sqlCommand = BuildSqlCommandPersonUpd2(sqlConnection, clientId, ipAddress, execUniqueId, loggedInuserId);
            sqlCommand.Parameters["@FirstName"].Value = personModel.FirstName;
            sqlCommand.Parameters["@LastName"].Value = personModel.LastName;
            sqlCommand.Parameters["@MaritalStatusId"].Value = personModel.MaritalStatusId ?? (object)DBNull.Value;
            sqlCommand.Parameters["@MiddleName"].Value = personModel.MiddleName ?? (object)DBNull.Value;
            sqlCommand.Parameters["@SalutationId"].Value = personModel.SalutationId;
            sqlCommand.Parameters["@SuffixId"].Value = personModel.SuffixId;
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInuserId;
            sqlCommand.Parameters["@AspNetUserId"].Value = personModel.AspNetUserId;
            sqlCommand.ExecuteNonQuery();
        }
        public static void UpdAspNetUser1(AspNetUserModel aspNetUserModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInuserId)
        {
            SqlCommand sqlCommand = BuildSqlCommandAspNetUserUpd1(sqlConnection, clientId, ipAddress, execUniqueId, loggedInuserId);
            sqlCommand.Parameters["@TelephoneCountryId"].Value = aspNetUserModel.TelephoneCountryId;
            sqlCommand.Parameters["@PhoneNumber"].Value = aspNetUserModel.PhoneNumber;
            sqlCommand.Parameters["@LoggedInuserId"].Value = loggedInuserId;
            sqlCommand.Parameters["@AspNetUserId"].Value = aspNetUserModel.AspNetUserId;
            sqlCommand.ExecuteNonQuery();
        }
        public static void UpdAspNetUserResetPassword(AspNetUserModel aspNetUserModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInuserId)
        {
            SqlCommand sqlCommand = BuildSqlCommandAspNetUserUpdResetPassword(sqlConnection, clientId, ipAddress, execUniqueId, loggedInuserId);
            sqlCommand.Parameters["@ResetPasswordQueryString"].Value = aspNetUserModel.ResetPasswordQueryString;
            sqlCommand.Parameters["@ResetPasswordExpiryDateTime"].Value = aspNetUserModel.ResetPasswordExpiryDateTime;
            sqlCommand.Parameters["@ResetPasswordKey"].Value = aspNetUserModel.ResetPasswordKey;
            sqlCommand.Parameters["@LoggedInuserId"].Value = loggedInuserId;
            sqlCommand.Parameters["@AspNetUserId"].Value = aspNetUserModel.AspNetUserId;
            sqlCommand.ExecuteNonQuery();
        }
        public static void UpdDemogInfoAddress(DemogInfoAddressModel demogInfoAddressModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Before calling the BuildSqlCommandUpdPerson()", "AspNetUserId", loggedInUserId);
                SqlCommand sqlCommand = BuildSqlCommandUpdDemogInfoAddress(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002100 After calling the BuildSqlCommandUpdPersonForStatus()", "AspNetUserId", loggedInUserId);
                sqlCommand.Parameters["@DemogInfoAddressId"].Value = demogInfoAddressModel.DemogInfoAddressId;
                sqlCommand.Parameters["@AddressLine1"].Value = demogInfoAddressModel.AddressLine1;
                sqlCommand.Parameters["@AddressLine2"].Value = demogInfoAddressModel.AddressLine2 == null ? (object)DBNull.Value : demogInfoAddressModel.AddressLine2;
                sqlCommand.Parameters["@AddressLine3"].Value = demogInfoAddressModel.AddressLine3 == null ? (object)DBNull.Value : demogInfoAddressModel.AddressLine3;
                sqlCommand.Parameters["@AddressLine4"].Value = demogInfoAddressModel.AddressLine4 == null ? (object)DBNull.Value : demogInfoAddressModel.AddressLine4;
                sqlCommand.Parameters["@AddressTypeId"].Value = AddressTypeEnum.Home;
                sqlCommand.Parameters["@BuildingTypeId"].Value = demogInfoAddressModel.BuildingTypeId == null ? 0 : demogInfoAddressModel.BuildingTypeId;
                sqlCommand.Parameters["@CityName"].Value = demogInfoAddressModel.CityName == null ? (object)DBNull.Value : demogInfoAddressModel.CityName;
                sqlCommand.Parameters["@CountryAbbrev"].Value = demogInfoAddressModel.CountryAbbrev == null ? (object)DBNull.Value : demogInfoAddressModel.CountryAbbrev;
                sqlCommand.Parameters["@CountyName"].Value = demogInfoAddressModel.CountyName == null ? (object)DBNull.Value : demogInfoAddressModel.CountyName;
                sqlCommand.Parameters["@DemogInfoCityId"].Value = demogInfoAddressModel.DemogInfoCityId == null ? (object)DBNull.Value : demogInfoAddressModel.DemogInfoCityId;
                sqlCommand.Parameters["@DemogInfoCountryId"].Value = demogInfoAddressModel.DemogInfoCountryId == null ? (object)DBNull.Value : demogInfoAddressModel.DemogInfoCountryId;
                sqlCommand.Parameters["@DemogInfoSubDivisionId"].Value = demogInfoAddressModel.DemogInfoSubDivisionId == null ? (object)DBNull.Value : demogInfoAddressModel.DemogInfoSubDivisionId;
                sqlCommand.Parameters["@DemogInfoZipId"].Value = demogInfoAddressModel.DemogInfoZipId == null ? (object)DBNull.Value : demogInfoAddressModel.DemogInfoZipId;
                sqlCommand.Parameters["@DemogInfoZipPlusId"].Value = demogInfoAddressModel.DemogInfoZipPlusId == null ? 0 : demogInfoAddressModel.DemogInfoZipPlusId;
                sqlCommand.Parameters["@HouseNumber"].Value = demogInfoAddressModel.HouseNumber == null ? (object)DBNull.Value : demogInfoAddressModel.HouseNumber;
                sqlCommand.Parameters["@StateAbbrev"].Value = demogInfoAddressModel.StateAbbrev == null ? (object)DBNull.Value : demogInfoAddressModel.StateAbbrev;
                sqlCommand.Parameters["@ZipCode"].Value = demogInfoAddressModel.ZipCode == null ? (object)DBNull.Value : demogInfoAddressModel.ZipCode;
                sqlCommand.Parameters["@ZipPlus4"].Value = demogInfoAddressModel.ZipPlus4 == null ? (object)DBNull.Value : demogInfoAddressModel.ZipPlus4;
                sqlCommand.Parameters["@UpdUserId"].Value = loggedInUserId;
                sqlCommand.ExecuteNonQuery();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
    }
}
