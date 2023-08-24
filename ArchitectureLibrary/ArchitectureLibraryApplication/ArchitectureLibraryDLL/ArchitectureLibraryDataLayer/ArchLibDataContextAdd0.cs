using ArchitectureLibraryEnumerations;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryDataLayer
{
    public static partial class ArchLibDataContext
    {
        private static void AddAspNetUser(AspNetUserModel aspNetUserModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {

                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Before calling the BuildSqlCommandAspNetUsers()", "AspNetUserId", "");
                SqlCommand sqlCommand = BuildSqlCommandAspNetUserAdd(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                sqlCommand.Parameters["@AspNetUserId"].Value = aspNetUserModel.AspNetUserId;
                sqlCommand.Parameters["@AccessFailedCount"].Value = aspNetUserModel.AccessFailedCount;
                sqlCommand.Parameters["@Email"].Value = aspNetUserModel.Email;
                sqlCommand.Parameters["@EmailConfirmed"].Value = aspNetUserModel.EmailConfirmed;
                sqlCommand.Parameters["@EmailConfirmationToken"].Value = string.IsNullOrEmpty(aspNetUserModel.EmailConfirmationToken) ? (object)DBNull.Value : aspNetUserModel.EmailConfirmationToken;
                sqlCommand.Parameters["@LockoutEnabled"].Value = aspNetUserModel.LockoutEnabled;
                sqlCommand.Parameters["@LockoutEndDateUtc"].Value = string.IsNullOrEmpty(aspNetUserModel.LockoutEndDateUtc) ? (object)DBNull.Value : aspNetUserModel.LockoutEndDateUtc;
                sqlCommand.Parameters["@LoginPassword"].Value = string.IsNullOrWhiteSpace(aspNetUserModel.LoginPassword) ? (object)DBNull.Value : aspNetUserModel.LoginPassword;
                sqlCommand.Parameters["@LoginTypeId"].Value = aspNetUserModel.LoginTypeId == null ? (object)DBNull.Value : (int)aspNetUserModel.LoginTypeId;
                sqlCommand.Parameters["@PasswordHash"].Value = string.IsNullOrEmpty(aspNetUserModel.PasswordHash) ? (object)DBNull.Value : aspNetUserModel.PasswordHash;
                sqlCommand.Parameters["@PasswordExpiry"].Value = aspNetUserModel?.PasswordExpiry ?? (object)DBNull.Value;// : aspNetUserModel.PasswordExpiry;
                sqlCommand.Parameters["@PhoneNumber"].Value = string.IsNullOrEmpty(aspNetUserModel.PhoneNumber) ? (object)DBNull.Value : aspNetUserModel.PhoneNumber;
                sqlCommand.Parameters["@PhoneNumberConfirmed"].Value = aspNetUserModel.PhoneNumberConfirmed;
                sqlCommand.Parameters["@ResetPasswordKey"].Value = string.IsNullOrWhiteSpace(aspNetUserModel.ResetPasswordKey) ? (object)DBNull.Value : aspNetUserModel.ResetPasswordKey;
                sqlCommand.Parameters["@ResetPasswordQueryString"].Value = string.IsNullOrWhiteSpace(aspNetUserModel.ResetPasswordQueryString) ? (object)DBNull.Value : aspNetUserModel.ResetPasswordQueryString;
                sqlCommand.Parameters["@ResetPasswordExpiryDateTime"].Value = aspNetUserModel?.ResetPasswordExpiryDateTime ?? (object)DBNull.Value;// : aspNetUserModel.ResetPasswordExpiryDateTime;
                sqlCommand.Parameters["@ResetPasswordCompletedDateTime"].Value = aspNetUserModel?.ResetPasswordCompletedDateTime ?? (object)DBNull.Value;// : aspNetUserModel.ResetPasswordExpiryDateTime;
                sqlCommand.Parameters["@SecurityStamp"].Value = string.IsNullOrEmpty(aspNetUserModel.SecurityStamp) ? (object)DBNull.Value : aspNetUserModel.SecurityStamp;
                sqlCommand.Parameters["@TwoFactorEnabled"].Value = aspNetUserModel.TwoFactorEnabled;
                sqlCommand.Parameters["@UserName"].Value = aspNetUserModel.UserName;
                sqlCommand.Parameters["@UserTypeId"].Value = aspNetUserModel.UserTypeId == null ? (object)DBNull.Value : (int)aspNetUserModel.UserTypeId;
                sqlCommand.Parameters["@UserStatusId"].Value = aspNetUserModel.UserStatusId == null ? (object)DBNull.Value : (int)aspNetUserModel.UserStatusId;
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
        private static void AddAspNetUserRole(AspNetUserRoleModel aspNetUserRoleModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Before calling the BuildSqlCommandAspNetUserRoles()", "AspNetUserId", "");
                SqlCommand sqlCommand = BuildSqlCommandAspNetUserRoleAdd(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                sqlCommand.Parameters["@AddUserId"].Value = aspNetUserRoleModel.AddUserId;
                sqlCommand.Parameters["@AspNetRoleId"].Value = aspNetUserRoleModel.AspNetRoleId;
                sqlCommand.Parameters["@AspNetUserId"].Value = aspNetUserRoleModel.AspNetUserId;
                sqlCommand.Parameters["@AspNetUserRoleId"].Value = aspNetUserRoleModel.AspNetUserRoleId;
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
        public static void AddContactUs(ContactUsModel contactUsModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            SqlCommand sqlCommand = BuildSqlCommandContactUsInsert(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
            AssignContactUs(contactUsModel, sqlCommand, clientId, ipAddress, execUniqueId, loggedInUserId);
            contactUsModel.ContactUsId = long.Parse(sqlCommand.ExecuteScalar().ToString());
        }
        private static void AddDemogInfoAddress(DemogInfoAddressModel demogInfoAddressModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Before calling the BuildSqlCommandAspNetUsers()", "AspNetUserId", "");
                SqlCommand sqlCommand = BuildSqlCommandDemogInfoAddressAdd(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                sqlCommand.Parameters["@AddressLine1"].Value = demogInfoAddressModel.AddressLine1;
                sqlCommand.Parameters["@AddressLine2"].Value = string.IsNullOrWhiteSpace(demogInfoAddressModel.AddressLine2) ? (object)DBNull.Value : demogInfoAddressModel.AddressLine2;
                sqlCommand.Parameters["@AddressTypeId"].Value = demogInfoAddressModel.AddressTypeId == null ? (long)AddressTypeEnum._ : (long)demogInfoAddressModel.AddressTypeId;
                sqlCommand.Parameters["@BuildingTypeId"].Value = demogInfoAddressModel.BuildingTypeId == null ? (long)BuildingTypeEnum._ : (long)demogInfoAddressModel.BuildingTypeId;
                sqlCommand.Parameters["@CityName"].Value = string.IsNullOrWhiteSpace(demogInfoAddressModel.CityName) ? (object)DBNull.Value : demogInfoAddressModel.CityName;
                sqlCommand.Parameters["@Comments"].Value = string.IsNullOrWhiteSpace(demogInfoAddressModel.Comments) ? (object)DBNull.Value : demogInfoAddressModel.Comments;
                sqlCommand.Parameters["@CountryAbbrev"].Value = string.IsNullOrWhiteSpace(demogInfoAddressModel.CountryAbbrev) ? (object)DBNull.Value : demogInfoAddressModel.CountryAbbrev;
                sqlCommand.Parameters["@CountyName"].Value = string.IsNullOrWhiteSpace(demogInfoAddressModel.CountyName) ? (object)DBNull.Value : demogInfoAddressModel.CountyName;
                sqlCommand.Parameters["@DemogInfoCityId"].Value = demogInfoAddressModel.DemogInfoCityId == null ? (object)DBNull.Value : (long)demogInfoAddressModel.DemogInfoCityId;
                sqlCommand.Parameters["@DemogInfoCountryId"].Value = demogInfoAddressModel.DemogInfoCountryId == null ? (object)DBNull.Value : (long)demogInfoAddressModel.DemogInfoCountryId;
                sqlCommand.Parameters["@DemogInfoCountyId"].Value = demogInfoAddressModel.DemogInfoCountyId == null ? (object)DBNull.Value : (long)demogInfoAddressModel.DemogInfoCountyId;
                sqlCommand.Parameters["@DemogInfoSubDivisionId"].Value = demogInfoAddressModel.DemogInfoSubDivisionId == null ? (object)DBNull.Value : (long)demogInfoAddressModel.DemogInfoSubDivisionId;
                sqlCommand.Parameters["@DemogInfoZipId"].Value = demogInfoAddressModel.DemogInfoZipId == null ? (object)DBNull.Value : demogInfoAddressModel.DemogInfoZipId;
                sqlCommand.Parameters["@DemogInfoZipPlusId"].Value = demogInfoAddressModel.DemogInfoZipPlusId == null ? (object)DBNull.Value : demogInfoAddressModel.DemogInfoZipPlusId;
                sqlCommand.Parameters["@FromDate"].Value = string.IsNullOrWhiteSpace (demogInfoAddressModel.FromDate) ? (object)DBNull.Value : DateTime.Parse(demogInfoAddressModel.FromDate).ToString("yyyy-MM-dd");
                sqlCommand.Parameters["@HouseNumber"].Value = string.IsNullOrWhiteSpace(demogInfoAddressModel.HouseNumber) ? (object)DBNull.Value : demogInfoAddressModel.HouseNumber;
                sqlCommand.Parameters["@StateAbbrev"].Value = string.IsNullOrWhiteSpace(demogInfoAddressModel.StateAbbrev) ? (object)DBNull.Value : demogInfoAddressModel.StateAbbrev;
                sqlCommand.Parameters["@ToDate"].Value = string.IsNullOrWhiteSpace(demogInfoAddressModel.ToDate) ? (object)DBNull.Value : DateTime.Parse(demogInfoAddressModel.ToDate).ToString("yyyy-MM-dd");
                sqlCommand.Parameters["@ZipCode"].Value = string.IsNullOrWhiteSpace(demogInfoAddressModel.ZipCode) ? (object)DBNull.Value : demogInfoAddressModel.ZipCode; //== null? (object)DBNull.Value : (long)demogInfoAddressModel.ZipCode;
                sqlCommand.Parameters["@ZipPlus4"].Value = string.IsNullOrWhiteSpace(demogInfoAddressModel.ZipPlus4) ? (object)DBNull.Value : demogInfoAddressModel.ZipPlus4;
                sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
                demogInfoAddressModel.DemogInfoAddressId = long.Parse(sqlCommand.ExecuteScalar().ToString());
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00009000 :: Exit");
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        private static void AddPerson(PersonModel personModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Before calling the BuildSqlCommandPersonAdd()", "AspNetUserId", "");
                SqlCommand sqlCommand = BuildSqlCommandPersonAdd(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                sqlCommand.Parameters["@AddUserId"].Value = personModel.AddUserId;
                sqlCommand.Parameters["@AspNetUserId"].Value = personModel.AspNetUserId;
                sqlCommand.Parameters["@CertificateDocumentId"].Value = personModel.CertificateDocumentId;
                sqlCommand.Parameters["@ElectronicSignatureConsentAccepted"].Value = personModel.ElectronicSignatureConsentAccepted ? 1 : 0;
                sqlCommand.Parameters["@FirstName"].Value = personModel.FirstName;
                sqlCommand.Parameters["@HomeDemogInfoAddressId"].Value = personModel.HomeDemogInfoAddressId;
                sqlCommand.Parameters["@InitialsTextId"].Value = personModel.InitialsTextId;
                sqlCommand.Parameters["@InitialsTextValue"].Value = personModel.InitialsTextValue;
                sqlCommand.Parameters["@LastName"].Value = personModel.LastName;
                sqlCommand.Parameters["@NicknameFirst"].Value = personModel.NicknameFirst;
                sqlCommand.Parameters["@NicknameLast"].Value = personModel.NicknameLast;
                sqlCommand.Parameters["@SalutationId"].Value = (long)personModel.SalutationId;
                sqlCommand.Parameters["@SignatureTextId"].Value = personModel.SignatureTextId;
                sqlCommand.Parameters["@SignatureTextValue"].Value = personModel.SignatureTextValue;
                sqlCommand.Parameters["@StatusId"].Value = (long)personModel.StatusId;
                sqlCommand.Parameters["@SuffixId"].Value = (long)personModel.SuffixId;
                sqlCommand.Parameters["@UpdUserId"].Value = personModel.UpdUserId;
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
    }
}
