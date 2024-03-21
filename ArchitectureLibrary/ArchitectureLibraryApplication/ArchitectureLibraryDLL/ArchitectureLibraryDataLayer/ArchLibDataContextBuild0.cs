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
        private static SqlCommand BuildSqlCommandContactUsInsert(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            #region
            string sqlStmt = "";
            sqlStmt += "        INSERT ArchLib.ContactUs" + Environment.NewLine;
            sqlStmt += "              (" + Environment.NewLine;
            sqlStmt += "               ClientId" + Environment.NewLine;
            sqlStmt += "              ,ContactUsTypeId" + Environment.NewLine;
            sqlStmt += "              ,FirstName" + Environment.NewLine;
            sqlStmt += "              ,LastName" + Environment.NewLine;
            sqlStmt += "              ,EmailAddress" + Environment.NewLine;
            sqlStmt += "              ,TelephoneCountryId" + Environment.NewLine;
            sqlStmt += "              ,TelephoneNumber" + Environment.NewLine;
            sqlStmt += "              ,CommentsRequests" + Environment.NewLine;
            sqlStmt += "              )" + Environment.NewLine;
            sqlStmt += "        OUTPUT INSERTED.ContactUsId" + Environment.NewLine;
            sqlStmt += "        SELECT" + Environment.NewLine;
            sqlStmt += "               @ClientId" + Environment.NewLine;
            sqlStmt += "              ,@ContactUsTypeId" + Environment.NewLine;
            sqlStmt += "              ,@FirstName" + Environment.NewLine;
            sqlStmt += "              ,@LastName" + Environment.NewLine;
            sqlStmt += "              ,@EmailAddress" + Environment.NewLine;
            sqlStmt += "              ,@TelephoneCountryId" + Environment.NewLine;
            sqlStmt += "              ,@TelephoneNumber" + Environment.NewLine;
            sqlStmt += "              ,@CommentsRequests" + Environment.NewLine;
            #endregion
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@ClientId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ContactUsTypeId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@FirstName", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@LastName", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@EmailAddress", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@TelephoneCountryId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@TelephoneNumber", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@CommentsRequests", System.DBNull.Value);
            return sqlCommand;
        }
        private static SqlCommand BuildSqlCommandAspNetUserAdd(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "";
            sqlStmt += "        INSERT ArchLib.AspNetUser" + Environment.NewLine;
            sqlStmt += "              (" + Environment.NewLine;
            sqlStmt += "               AspNetUserId" + Environment.NewLine;
            sqlStmt += "              ,ClientId" + Environment.NewLine;
            sqlStmt += "              ,AccessFailedCount" + Environment.NewLine;
            sqlStmt += "              ,Email" + Environment.NewLine;
            sqlStmt += "              ,EmailConfirmed" + Environment.NewLine;
            sqlStmt += "              ,EmailConfirmationToken" + Environment.NewLine;
            sqlStmt += "              ,LockoutEnabled" + Environment.NewLine;
            sqlStmt += "              ,LockoutEndDateUtc" + Environment.NewLine;
            sqlStmt += "              ,LoginPassword" + Environment.NewLine;
            sqlStmt += "              ,LoginTypeId" + Environment.NewLine;
            sqlStmt += "              ,PasswordHash" + Environment.NewLine;
            sqlStmt += "              ,PasswordExpiry" + Environment.NewLine;
            sqlStmt += "              ,TelephoneCountryId" + Environment.NewLine;
            sqlStmt += "              ,PhoneNumber" + Environment.NewLine;            
            sqlStmt += "              ,PhoneNumberConfirmed" + Environment.NewLine;
            sqlStmt += "              ,ResetPasswordKey" + Environment.NewLine;
            sqlStmt += "              ,ResetPasswordQueryString" + Environment.NewLine;
            sqlStmt += "              ,ResetPasswordExpiryDateTime" + Environment.NewLine;
            sqlStmt += "              ,ResetPasswordCompletedDateTime" + Environment.NewLine;
            sqlStmt += "              ,SecurityStamp" + Environment.NewLine;
            sqlStmt += "              ,TwoFactorEnabled" + Environment.NewLine;
            sqlStmt += "              ,UserName" + Environment.NewLine;
            sqlStmt += "              ,UserTypeId" + Environment.NewLine;
            sqlStmt += "              ,UserStatusId" + Environment.NewLine;
            sqlStmt += "              )" + Environment.NewLine;
            sqlStmt += "        SELECT" + Environment.NewLine;
            sqlStmt += "               @AspNetUserId" + Environment.NewLine;
            sqlStmt += "              ,@ClientId" + Environment.NewLine;
            sqlStmt += "              ,@AccessFailedCount" + Environment.NewLine;
            sqlStmt += "              ,@Email" + Environment.NewLine;
            sqlStmt += "              ,@EmailConfirmed" + Environment.NewLine;
            sqlStmt += "              ,@EmailConfirmationToken" + Environment.NewLine;
            sqlStmt += "              ,@LockoutEnabled" + Environment.NewLine;
            sqlStmt += "              ,@LockoutEndDateUtc" + Environment.NewLine;
            sqlStmt += "              ,@LoginPassword" + Environment.NewLine;
            sqlStmt += "              ,@LoginTypeId" + Environment.NewLine;
            sqlStmt += "              ,@PasswordHash" + Environment.NewLine;
            sqlStmt += "              ,@PasswordExpiry" + Environment.NewLine;
            sqlStmt += "              ,@TelephoneCountryId" + Environment.NewLine;
            sqlStmt += "              ,@PhoneNumber" + Environment.NewLine;
            sqlStmt += "              ,@PhoneNumberConfirmed" + Environment.NewLine;
            sqlStmt += "              ,@ResetPasswordKey" + Environment.NewLine;
            sqlStmt += "              ,@ResetPasswordQueryString" + Environment.NewLine;
            sqlStmt += "              ,@ResetPasswordExpiryDateTime" + Environment.NewLine;
            sqlStmt += "              ,@ResetPasswordCompletedDateTime" + Environment.NewLine;
            sqlStmt += "              ,@SecurityStamp" + Environment.NewLine;
            sqlStmt += "              ,@TwoFactorEnabled" + Environment.NewLine;
            sqlStmt += "              ,@UserName" + Environment.NewLine;
            sqlStmt += "              ,@UserTypeId" + Environment.NewLine;
            sqlStmt += "              ,@UserStatusId" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@AspNetUserId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ClientId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@AccessFailedCount", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@Email", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@EmailConfirmed", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@EmailConfirmationToken", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@LockoutEnabled", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@LockoutEndDateUtc", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@LoginPassword", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@LoginTypeId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@PasswordHash", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@PasswordExpiry", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@TelephoneCountryId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@PhoneNumber", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@PhoneNumberConfirmed", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ResetPasswordKey", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ResetPasswordQueryString", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ResetPasswordExpiryDateTime", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ResetPasswordCompletedDateTime", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@SecurityStamp", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@TwoFactorEnabled", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@UserName", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@UserTypeId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@UserStatusId", System.DBNull.Value);
            return sqlCommand;
        }
        private static SqlCommand BuildSqlCommandAspNetUserRoleAdd(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "";
            sqlStmt += "        INSERT ArchLib.AspNetUserRole" + Environment.NewLine;
            sqlStmt += "              (" + Environment.NewLine;
            sqlStmt += "               ClientId" + Environment.NewLine;
            sqlStmt += "              ,AddUserId" + Environment.NewLine;
            sqlStmt += "              ,AspNetRoleId" + Environment.NewLine;
            sqlStmt += "              ,AspNetUserId" + Environment.NewLine;
            sqlStmt += "              ,AspNetUserRoleId" + Environment.NewLine;
            sqlStmt += "              )" + Environment.NewLine;
            sqlStmt += "        SELECT" + Environment.NewLine;
            sqlStmt += "               @ClientId" + Environment.NewLine;
            sqlStmt += "              ,@AddUserId" + Environment.NewLine;
            sqlStmt += "              ,@AspNetRoleId" + Environment.NewLine;
            sqlStmt += "              ,@AspNetUserId" + Environment.NewLine;
            sqlStmt += "              ,@AspNetUserRoleId" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@ClientId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@AddUserId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@AspNetRoleId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@AspNetUserId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@AspNetUserRoleId", System.DBNull.Value);
            return sqlCommand;
        }
        private static SqlCommand BuildSqlCommandAspNetUserUpd(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "";
            sqlStmt += "        UPDATE ArchLib.AspNetUser" + Environment.NewLine;
            sqlStmt += "           SET " + Environment.NewLine;
            sqlStmt += "               LoginPassword = @LoginPassword" + Environment.NewLine;
            sqlStmt += "              ,PasswordExpiry = @PasswordExpiry" + Environment.NewLine;
            sqlStmt += "              ,ResetPasswordCompletedDateTime = @ResetPasswordCompletedDateTime" + Environment.NewLine;
            sqlStmt += "              ,UserStatusId = @UserStatusId" + Environment.NewLine;
            sqlStmt += "              ,UpdUserId = @UpdUserId" + Environment.NewLine;
            sqlStmt += "              ,UpdUserName = @UpdUserName" + Environment.NewLine;
            sqlStmt += "         WHERE " + Environment.NewLine;
            sqlStmt += "               AspNetUserId = @AspNetUserId" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@AspNetUserId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@LoginPassword", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@PasswordExpiry", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ResetPasswordCompletedDateTime", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@UserStatusId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@UpdUserId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@UpdUserName", System.DBNull.Value);
            return sqlCommand;
        }
        private static SqlCommand BuildSqlCommandAspNetUserUpd2(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "";
            sqlStmt += "        UPDATE ArchLib.AspNetUser" + Environment.NewLine;
            sqlStmt += "           SET " + Environment.NewLine;
            sqlStmt += "               ResetPasswordExpiryDateTime = @ResetPasswordExpiryDateTime" + Environment.NewLine;
            sqlStmt += "              ,ResetPasswordKey = @ResetPasswordKey" + Environment.NewLine;
            sqlStmt += "              ,ResetPasswordQueryString = @ResetPasswordQueryString" + Environment.NewLine;
            sqlStmt += "              ,UserTypeId = @UserTypeId" + Environment.NewLine;
            sqlStmt += "              ,UpdUserId = @UpdUserId" + Environment.NewLine;
            sqlStmt += "              ,UpdUserName = SUSER_NAME()" + Environment.NewLine;
            sqlStmt += "              ,UpdDateTime = GETDATE()" + Environment.NewLine;
            sqlStmt += "         WHERE " + Environment.NewLine;
            sqlStmt += "               AspNetUserId = @AspNetUserId" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ResetPasswordExpiryDateTime", SqlDbType.NVarChar, 50);
            sqlCommand.Parameters.Add("@ResetPasswordKey", SqlDbType.NVarChar, 18);
            sqlCommand.Parameters.Add("@ResetPasswordQueryString", SqlDbType.NVarChar, 1024);
            sqlCommand.Parameters.Add("@UserTypeId", SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@UpdUserId", SqlDbType.NVarChar, 256);
            sqlCommand.Parameters.Add("@AspNetUserId", SqlDbType.NVarChar, 128);
            return sqlCommand;
        }
        private static SqlCommand BuildSqlCommandPersonUpd(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "";
            sqlStmt += "        UPDATE ArchLib.Person" + Environment.NewLine;
            sqlStmt += "           SET " + Environment.NewLine;
            sqlStmt += "               StatusId = @StatusId" + Environment.NewLine;
            sqlStmt += "              ,UpdUserId = @UpdUserId" + Environment.NewLine;
            sqlStmt += "              ,UpdUserName = @UpdUserName" + Environment.NewLine;
            sqlStmt += "         WHERE " + Environment.NewLine;
            sqlStmt += "               AspNetUserId = @AspNetUserId AND ClientId=@ClientId" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@ClientId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@AspNetUserId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@StatusId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@UpdUserId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@UpdUserName", System.DBNull.Value);
            return sqlCommand;
        }
        private static SqlCommand BuildSqlCommandPersonUpd1(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "";
            sqlStmt += "        UPDATE ArchLib.Person" + Environment.NewLine;
            sqlStmt += "           SET " + Environment.NewLine;
            sqlStmt += "               CitizenshipId = @CitizenshipId" + Environment.NewLine;
            sqlStmt += "              ,CertificateDocumentId = @CertificateDocumentId" + Environment.NewLine;
            sqlStmt += "              ,DateOfBirth = @DateOfBirth" + Environment.NewLine;
            sqlStmt += "              ,DriverLicenseDemogInfoSubDivisionId = @DriverLicenseDemogInfoSubDivisionId" + Environment.NewLine;
            sqlStmt += "              ,DriverLicenseExpiryDate = @DriverLicenseExpiryDate" + Environment.NewLine;
            sqlStmt += "              ,DriverLicenseNumber = @DriverLicenseNumber" + Environment.NewLine;
            sqlStmt += "              ,DriverLicenseType = @DriverLicenseType" + Environment.NewLine;
            sqlStmt += "              ,ElectronicSignatureConsent = @ElectronicSignatureConsent" + Environment.NewLine;
            sqlStmt += "              ,ElectronicSignatureConsentAccepted = @ElectronicSignatureConsentAccepted" + Environment.NewLine;
            sqlStmt += "              ,FirstName = @FirstName" + Environment.NewLine;
            sqlStmt += "              ,InitialsTextId = @InitialsTextId" + Environment.NewLine;
            sqlStmt += "              ,InitialsTextValue = @InitialsTextValue" + Environment.NewLine;
            sqlStmt += "              ,LastName = @LastName" + Environment.NewLine;
            sqlStmt += "              ,MaritalStatusId = @MaritalStatusId" + Environment.NewLine;
            sqlStmt += "              ,MiddleName = @MiddleName" + Environment.NewLine;
            //sqlStmt += "              ,MilitaryServiceId = @MilitaryServiceId" + Environment.NewLine;
            sqlStmt += "              ,NicknameFirst = @NicknameFirst" + Environment.NewLine;
            sqlStmt += "              ,NicknameLast = @NicknameLast" + Environment.NewLine;
            sqlStmt += "              ,SalutationId = @SalutationId" + Environment.NewLine;
            sqlStmt += "              ,SignatureTextId = @SignatureTextId" + Environment.NewLine;
            sqlStmt += "              ,SignatureTextValue = @SignatureTextValue" + Environment.NewLine;
            sqlStmt += "              ,SSN = @SSN" + Environment.NewLine;
            sqlStmt += "              ,SuffixId = @SuffixId" + Environment.NewLine;
            sqlStmt += "              ,UpdUserId = @LoggedInUserId" + Environment.NewLine;
            sqlStmt += "              ,UpdUserName = SUSER_NAME()" + Environment.NewLine;
            sqlStmt += "              ,UpdDateTime = GETDATE()" + Environment.NewLine;
            sqlStmt += "         WHERE " + Environment.NewLine;
            sqlStmt += "               AspNetUserId = @AspNetUserId" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@CitizenshipId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@CertificateDocumentId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@DateOfBirth", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@DriverLicenseDemogInfoSubDivisionId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@DriverLicenseExpiryDate", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@DriverLicenseNumber", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@DriverLicenseType", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ElectronicSignatureConsent", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ElectronicSignatureConsentAccepted", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@FirstName", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@InitialsTextId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@InitialsTextValue", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@LastName", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@MaritalStatusId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@MiddleName", System.DBNull.Value);
            //sqlCommand.Parameters.AddWithValue("@MilitaryServiceId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@NicknameFirst", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@NicknameLast", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@SalutationId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@SignatureTextId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@SignatureTextValue", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@SSN", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@SuffixId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@LoggedInUserId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@AspNetUserId", System.DBNull.Value);
            return sqlCommand;
        }
        private static SqlCommand BuildSqlCommandPersonUpd2(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "";
            sqlStmt += "        UPDATE ArchLib.Person" + Environment.NewLine;
            sqlStmt += "           SET " + Environment.NewLine;
            sqlStmt += "               FirstName = @FirstName" + Environment.NewLine;
            sqlStmt += "              ,LastName = @LastName" + Environment.NewLine;
            sqlStmt += "              ,MiddleName = @MiddleName" + Environment.NewLine;
            sqlStmt += "              ,SalutationId = @SalutationId" + Environment.NewLine;
            sqlStmt += "              ,SuffixId = @SuffixId" + Environment.NewLine;
            sqlStmt += "              ,UpdUserId = @LoggedInUserId" + Environment.NewLine;
            sqlStmt += "              ,UpdUserName = SUSER_NAME()" + Environment.NewLine;
            sqlStmt += "              ,UpdDateTime = GETDATE()" + Environment.NewLine;
            sqlStmt += "         WHERE " + Environment.NewLine;
            sqlStmt += "               AspNetUserId = @AspNetUserId" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@CitizenshipId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@CertificateDocumentId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@DateOfBirth", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@DriverLicenseDemogInfoSubDivisionId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@DriverLicenseExpiryDate", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@DriverLicenseNumber", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@DriverLicenseType", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ElectronicSignatureConsent", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ElectronicSignatureConsentAccepted", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@FirstName", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@InitialsTextId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@InitialsTextValue", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@LastName", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@MaritalStatusId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@MiddleName", System.DBNull.Value);
            //sqlCommand.Parameters.AddWithValue("@MilitaryServiceId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@NicknameFirst", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@NicknameLast", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@SalutationId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@SignatureTextId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@SignatureTextValue", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@SSN", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@SuffixId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@LoggedInUserId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@AspNetUserId", System.DBNull.Value);
            return sqlCommand;
        }
        private static SqlCommand BuildSqlCommandAspNetUserUpd1(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "";
            sqlStmt += "        UPDATE ArchLib.AspNetUser" + Environment.NewLine;
            sqlStmt += "           SET " + Environment.NewLine;
            sqlStmt += "               TelephoneCountryId = @TelephoneCountryId" + Environment.NewLine;
            sqlStmt += "              ,PhoneNumber = @PhoneNumber" + Environment.NewLine;
            sqlStmt += "              ,UpdUserId = @LoggedInUserId" + Environment.NewLine;
            sqlStmt += "              ,UpdUserName = SUSER_NAME()" + Environment.NewLine;
            sqlStmt += "              ,UpdDateTime = GETDATE()" + Environment.NewLine;
            sqlStmt += "         WHERE " + Environment.NewLine;
            sqlStmt += "               AspNetUserId = @AspNetUserId" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@TelephoneCountryId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@PhoneNumber", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@LoggedInUserId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@AspNetUserId", System.DBNull.Value);
            return sqlCommand;
        }
        private static SqlCommand BuildSqlCommandAspNetUserUpdResetPassword(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "";
            sqlStmt += "        UPDATE ArchLib.AspNetUser" + Environment.NewLine;
            sqlStmt += "           SET " + Environment.NewLine;
            sqlStmt += "               ResetPasswordCompletedDateTime = NULL" + Environment.NewLine;
            sqlStmt += "              ,ResetPasswordQueryString = @ResetPasswordQueryString" + Environment.NewLine;
            sqlStmt += "              ,ResetPasswordExpiryDateTime = @ResetPasswordExpiryDateTime" + Environment.NewLine;
            sqlStmt += "              ,ResetPasswordKey = @ResetPasswordKey" + Environment.NewLine;
            sqlStmt += "              ,UpdUserId = @LoggedInUserId" + Environment.NewLine;
            sqlStmt += "              ,UpdUserName = SUSER_NAME()" + Environment.NewLine;
            sqlStmt += "              ,UpdDateTime = GETDATE()" + Environment.NewLine;
            sqlStmt += "         WHERE " + Environment.NewLine;
            sqlStmt += "               AspNetUserId = @AspNetUserId" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@ResetPasswordQueryString", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ResetPasswordExpiryDateTime", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ResetPasswordKey", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@LoggedInUserId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@AspNetUserId", System.DBNull.Value);
            return sqlCommand;
        }
        private static SqlCommand BuildSqlCommandDemogInfoAddressAdd(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: Before Insert Query into AspNetUserRole Table");
                sqlStmt += "        INSERT ArchLib.DemogInfoAddress" + Environment.NewLine;
                sqlStmt += "              (" + Environment.NewLine;
                sqlStmt += "               ClientId" + Environment.NewLine;
                sqlStmt += "              ,AddressLine1" + Environment.NewLine;
                sqlStmt += "              ,AddressLine2" + Environment.NewLine;
                sqlStmt += "              ,AddressLine3" + Environment.NewLine;
                sqlStmt += "              ,AddressLine4" + Environment.NewLine;
                sqlStmt += "              ,AddressTypeId" + Environment.NewLine;
                sqlStmt += "              ,BuildingTypeId" + Environment.NewLine;
                sqlStmt += "              ,CityName" + Environment.NewLine;
                sqlStmt += "              ,Comments" + Environment.NewLine;
                sqlStmt += "              ,CountryAbbrev" + Environment.NewLine;
                sqlStmt += "              ,CountryDesc" + Environment.NewLine;
                sqlStmt += "              ,CountyName" + Environment.NewLine;
                sqlStmt += "              ,DemogInfoCityId" + Environment.NewLine;
                sqlStmt += "              ,DemogInfoCountryId" + Environment.NewLine;
                sqlStmt += "              ,DemogInfoCountyId" + Environment.NewLine;
                sqlStmt += "              ,DemogInfoSubDivisionId" + Environment.NewLine;
                sqlStmt += "              ,DemogInfoZipId" + Environment.NewLine;
                sqlStmt += "              ,DemogInfoZipPlusId" + Environment.NewLine;
                sqlStmt += "              ,FromDate" + Environment.NewLine;
                sqlStmt += "              ,HouseNumber" + Environment.NewLine;
                sqlStmt += "              ,StateAbbrev" + Environment.NewLine;
                sqlStmt += "              ,ToDate" + Environment.NewLine;
                sqlStmt += "              ,ZipCode" + Environment.NewLine;
                sqlStmt += "              ,ZipPlus4" + Environment.NewLine;
                sqlStmt += "              ,AddUserId" + Environment.NewLine;
                sqlStmt += "              ,UpdUserId" + Environment.NewLine;
                sqlStmt += "              )" + Environment.NewLine;
                sqlStmt += "        OUTPUT INSERTED.DemogInfoAddressId" + Environment.NewLine;
                sqlStmt += "        SELECT" + Environment.NewLine;
                sqlStmt += "               @ClientId" + Environment.NewLine;
                sqlStmt += "              ,@AddressLine1" + Environment.NewLine;
                sqlStmt += "              ,@AddressLine2" + Environment.NewLine;
                sqlStmt += "              ,@AddressLine3" + Environment.NewLine;
                sqlStmt += "              ,@AddressLine4" + Environment.NewLine;
                sqlStmt += "              ,@AddressTypeId" + Environment.NewLine;
                sqlStmt += "              ,@BuildingTypeId" + Environment.NewLine;
                sqlStmt += "              ,@CityName" + Environment.NewLine;
                sqlStmt += "              ,@Comments" + Environment.NewLine;
                sqlStmt += "              ,@CountryAbbrev" + Environment.NewLine;
                sqlStmt += "              ,@CountryDesc" + Environment.NewLine;
                sqlStmt += "              ,@CountyName" + Environment.NewLine;
                sqlStmt += "              ,@DemogInfoCityId" + Environment.NewLine;
                sqlStmt += "              ,@DemogInfoCountryId" + Environment.NewLine;
                sqlStmt += "              ,@DemogInfoCountyId" + Environment.NewLine;
                sqlStmt += "              ,@DemogInfoSubDivisionId" + Environment.NewLine;
                sqlStmt += "              ,@DemogInfoZipId" + Environment.NewLine;
                sqlStmt += "              ,@DemogInfoZipPlusId" + Environment.NewLine;
                sqlStmt += "              ,@FromDate" + Environment.NewLine;
                sqlStmt += "              ,@HouseNumber" + Environment.NewLine;
                sqlStmt += "              ,@StateAbbrev" + Environment.NewLine;
                sqlStmt += "              ,@ToDate" + Environment.NewLine;
                sqlStmt += "              ,@ZipCode" + Environment.NewLine;
                sqlStmt += "              ,@ZipPlus4" + Environment.NewLine;
                sqlStmt += "              ,@LoggedInUserId" + Environment.NewLine;
                sqlStmt += "              ,@LoggedInUserId" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@ClientId", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@AddressLine1", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@AddressLine2", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@AddressLine3", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@AddressLine4", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@AddressTypeId", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@BuildingTypeId", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@CityName", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@Comments", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@CountryAbbrev", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@CountryDesc", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@CountyName", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@DemogInfoCityId", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@DemogInfoCountryId", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@DemogInfoCountyId", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@DemogInfoSubDivisionId", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@DemogInfoZipId", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@DemogInfoZipPlusId", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@FromDate", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@HouseNumber", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@StateAbbrev", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@ToDate", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@ZipCode", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@ZipPlus4", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@LoggedInUserId", DBNull.Value);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00009000 :: Exit");
                return sqlCommand;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        private static SqlCommand BuildSqlCommandUpdDemogInfoAddress(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Before Update Query Person");
                sqlStmt += "        UPDATE ArchLib.DemogInfoAddress" + Environment.NewLine;
                sqlStmt += "           SET " + Environment.NewLine;
                sqlStmt += "              AddressLine1 = @AddressLine1" + Environment.NewLine;
                sqlStmt += "              ,AddressLine2 = @AddressLine2" + Environment.NewLine;
                sqlStmt += "              ,AddressTypeId = @AddressTypeId" + Environment.NewLine;
                sqlStmt += "              ,BuildingTypeId = @BuildingTypeId" + Environment.NewLine;
                sqlStmt += "              ,CityName = @CityName" + Environment.NewLine;
                sqlStmt += "              ,CountryAbbrev = @CountryAbbrev" + Environment.NewLine;
                sqlStmt += "              ,CountyName = @CountyName" + Environment.NewLine;
                sqlStmt += "              ,DemogInfoCityId = @DemogInfoCityId" + Environment.NewLine;
                sqlStmt += "              ,DemogInfoCountryId = @DemogInfoCountryId" + Environment.NewLine;
                sqlStmt += "              ,DemogInfoSubDivisionId = @DemogInfoSubDivisionId" + Environment.NewLine;
                sqlStmt += "              ,DemogInfoZipId = @DemogInfoZipId" + Environment.NewLine;
                sqlStmt += "              ,DemogInfoZipPlusId = @DemogInfoZipPlusId" + Environment.NewLine;
                sqlStmt += "              ,HouseNumber = @HouseNumber" + Environment.NewLine;
                sqlStmt += "              ,StateAbbrev = @StateAbbrev" + Environment.NewLine;
                sqlStmt += "              ,ZipCode = @ZipCode" + Environment.NewLine;
                sqlStmt += "              ,ZipPlus4 = @ZipPlus4" + Environment.NewLine;
                sqlStmt += "              ,UpdUserId = @UpdUserId" + Environment.NewLine;
                sqlStmt += "              ,UpdUserName = SUSER_NAME()" + Environment.NewLine;
                sqlStmt += "              ,UpdDateTime = SYSDATETIME()" + Environment.NewLine;
                sqlStmt += "         WHERE " + Environment.NewLine;
                sqlStmt += "               DemogInfoAddressId = @DemogInfoAddressId" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@DemogInfoAddressId", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@AddressLine1", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@AddressLine2", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@AddressTypeId", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@BuildingTypeId", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@CityName", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@CountryAbbrev", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@CountyName", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@DemogInfoCityId", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@DemogInfoCountryId", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@DemogInfoSubDivisionId", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@DemogInfoZipId", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@DemogInfoZipPlusId", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@HouseNumber", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@StateAbbrev", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@ZipCode", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@ZipPlus4", DBNull.Value);
                sqlCommand.Parameters.AddWithValue("@UpdUserId", DBNull.Value);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: After Update Query of Person");
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00009000 :: Exit");
                return sqlCommand;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        private static AspNetUserRoleModel BuildAspNetUserRole(string aspNetUserId, string aspNetRoleId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            AspNetUserRoleModel aspNetUserRoleModel = new AspNetUserRoleModel
            {
                AddUserId = aspNetUserId,
                AspNetRoleId = aspNetRoleId,
                AspNetUserId = aspNetUserId,
                AspNetUserRoleId = Guid.NewGuid().ToString(),
                UpdUserId = aspNetUserId,
            };
            return aspNetUserRoleModel;
        }
        private static AspNetUserModel BuildAspNetUser(string aspNetUserId, string loginNameId1, string loginPassword, DateTime? loginPasswordExpiryDateTime, long? telephoneCountryId, string phoneNumber, string resetPasswordQueryString, string resetPasswordDateTime, string resetPasswordKey, LoginTypeEnum? loginTypeId, UserTypeEnum? userTypeId, UserStatusEnum? userStatusId, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            AspNetUserModel aspNetUserModel = new AspNetUserModel
            {
                AccessFailedCount = 0,
                AddUserId = aspNetUserId,
                AspNetUserId = aspNetUserId,
                Email = loginNameId1,
                EmailConfirmed = false,
                LockoutEnabled = false,
                LoginPassword = loginPassword,
                LoginTypeId = loginTypeId,
                PasswordExpiry = DateTime.Now.AddDays(180).ToString("yyyy-MM-dd HH:mm:ss"),
                TelephoneCountryId = telephoneCountryId,
                PhoneNumber = phoneNumber,
                PhoneNumberConfirmed = false,
                ResetPasswordExpiryDateTime = resetPasswordDateTime,
                ResetPasswordQueryString = resetPasswordQueryString,
                ResetPasswordKey = resetPasswordKey,
                TwoFactorEnabled = false,
                UpdUserId = aspNetUserId,
                UserName = loginNameId1,
                UserStatusId = userStatusId,
                UserTypeId = userTypeId,
            };
            return aspNetUserModel;
        }
        private static DemogInfoAddressModel BuildDemogInfoAddress(string addressLine1, string addressLine2, string addressLine3, string addressLine4, AddressTypeEnum addressTypeId, BuildingTypeEnum buildingTypeId, string cityName, string countryAbbrev, long? demogInfoCountryId, string aspNetUserId, long clientId, string execUniqueId, string ipAddress, string loggedInUserId)
        {
            DemogInfoAddressModel demogInfoAddressModel = new DemogInfoAddressModel
            {
                AddUserId = aspNetUserId,
                AddressLine1 = addressLine1,
                AddressLine2 = addressLine2,
                AddressLine3 = addressLine3,
                AddressLine4 = addressLine4,
                AddressTypeId = addressTypeId,
                BuildingTypeId = buildingTypeId,
                CityName = cityName,
                CountryAbbrev = countryAbbrev,
                CountyName = "",
                DemogInfoCityId = 0,
                DemogInfoCountryId = demogInfoCountryId,
                DemogInfoZipPlusId = 0,
                HouseNumber = "",
                DemogInfoSubDivisionId = 0,
                UpdUserId = aspNetUserId,
                ZipCode = "",
                ZipPlus4 = "",
            };
            return demogInfoAddressModel;
        }
        private static SqlCommand BuildSqlCommandPersonAdd(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "";
            sqlStmt += "        INSERT ArchLib.Person" + Environment.NewLine;
            sqlStmt += "              (" + Environment.NewLine;
            sqlStmt += "               AddUserId" + Environment.NewLine;
            sqlStmt += "              ,AspNetUserId" + Environment.NewLine;
            sqlStmt += "              ,ClientId" + Environment.NewLine;
            sqlStmt += "              ,CertificateDocumentId" + Environment.NewLine;
            //sqlStmt += "              ,CertificateDocumentNoImage" + Environment.NewLine;
            sqlStmt += "              ,ElectronicSignatureConsentAccepted" + Environment.NewLine;
            sqlStmt += "              ,FirstName" + Environment.NewLine;
            sqlStmt += "              ,HomeDemogInfoAddressId" + Environment.NewLine;
            sqlStmt += "              ,InitialsTextId" + Environment.NewLine;
            sqlStmt += "              ,InitialsTextValue" + Environment.NewLine;
            sqlStmt += "              ,LastName" + Environment.NewLine;
            sqlStmt += "              ,NicknameFirst" + Environment.NewLine;
            sqlStmt += "              ,NicknameLast" + Environment.NewLine;
            sqlStmt += "              ,SalutationId" + Environment.NewLine;
            sqlStmt += "              ,SignatureTextId" + Environment.NewLine;
            sqlStmt += "              ,SignatureTextValue" + Environment.NewLine;
            sqlStmt += "              ,StatusId" + Environment.NewLine;
            sqlStmt += "              ,SuffixId" + Environment.NewLine;
            sqlStmt += "              ,UpdUserId" + Environment.NewLine;
            sqlStmt += "              )" + Environment.NewLine;
            sqlStmt += "        OUTPUT INSERTED.PersonId" + Environment.NewLine;
            sqlStmt += "        SELECT" + Environment.NewLine;
            sqlStmt += "               @AddUserId" + Environment.NewLine;
            sqlStmt += "              ,@AspNetUserId" + Environment.NewLine;
            sqlStmt += "              ,@ClientId" + Environment.NewLine;
            sqlStmt += "              ,@CertificateDocumentId" + Environment.NewLine;
            //sqlStmt += "              ,@CertificateDocumentNoImage" + Environment.NewLine;
            sqlStmt += "              ,@ElectronicSignatureConsentAccepted" + Environment.NewLine;
            sqlStmt += "              ,@FirstName" + Environment.NewLine;
            sqlStmt += "              ,@HomeDemogInfoAddressId" + Environment.NewLine;
            sqlStmt += "              ,@InitialsTextId" + Environment.NewLine;
            sqlStmt += "              ,@InitialsTextValue" + Environment.NewLine;
            sqlStmt += "              ,@LastName" + Environment.NewLine;
            sqlStmt += "              ,@NicknameFirst" + Environment.NewLine;
            sqlStmt += "              ,@NicknameLast" + Environment.NewLine;
            sqlStmt += "              ,@SalutationId" + Environment.NewLine;
            sqlStmt += "              ,@SignatureTextId" + Environment.NewLine;
            sqlStmt += "              ,@SignatureTextValue" + Environment.NewLine;
            sqlStmt += "              ,@StatusId" + Environment.NewLine;
            sqlStmt += "              ,@SuffixId" + Environment.NewLine;
            sqlStmt += "              ,@UpdUserId" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@AddUserId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@AspNetUserId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ClientId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@CertificateDocumentId", System.DBNull.Value);
            //sqlCommand.Parameters.AddWithValue("@CertificateDocumentNoImage", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@ElectronicSignatureConsentAccepted", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@FirstName", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@HomeDemogInfoAddressId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@InitialsTextId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@InitialsTextValue", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@LastName", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@NicknameFirst", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@NicknameLast", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@SalutationId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@SignatureTextId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@SignatureTextValue", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@StatusId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@SuffixId", System.DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@UpdUserId", System.DBNull.Value);
            return sqlCommand;
        }
        private static PersonModel BuildPerson(string aspNetUserId, string firstName, string lastName, long certificateDocumentId, long homeDemogInfoAddressId, bool electronicSignatureConsentAccepted, long initialsTextId, string initialsTextValue, long signatureTextId, string signatureTextValue, long clientId, string execUniqueId, string ipAddress, string loggedInUserId)
        {
            PersonModel personModel = new PersonModel
            {
                AddUserId = aspNetUserId,
                AspNetUserId = aspNetUserId,
                CertificateDocumentId = certificateDocumentId,
                ElectronicSignatureConsentAccepted = electronicSignatureConsentAccepted,
                FirstName = firstName ?? null,
                HomeDemogInfoAddressId = homeDemogInfoAddressId,
                InitialsTextId = initialsTextId,
                InitialsTextValue = initialsTextValue,
                LastName = lastName ?? "",
                NicknameFirst = "",
                NicknameLast = "",
                SalutationId = (int)SalutationEnum._,
                SignatureTextId = signatureTextId,
                SignatureTextValue = signatureTextValue,
                StatusId = GenericStatusEnum.Active,
                SuffixId = (int)SuffixEnum._,
                UpdUserId = aspNetUserId,
            };
            return personModel;
        }
    }
}
