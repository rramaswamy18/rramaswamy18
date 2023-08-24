using ArchitectureLibraryCacheData;
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
        public static PersonModel CreateRegisterUser(string aspNetUserId, string loginNameId1, string loginPassword, DateTime? loginPasswordExpiryDateTime, string phoneNumber, string resetPasswordQueryString, string resetPasswordDateTime, string resetPasswordKey, string firstName, string lastName, long certificateDocumentId, long initialsTextId, string initialsTextValue, long signatureTextId, string signatureTextValue, UserStatusEnum? userStatusId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Before AspNetRole", "loginNameId1", loginNameId1);
                string aspNetRoleId = GetAspNetRole("Default Role", sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: Before AspNetUser", "loginNameId1", loginNameId1);
                AspNetUserModel aspNetUserModel = BuildAspNetUser(aspNetUserId, loginNameId1, loginPassword, loginPasswordExpiryDateTime, phoneNumber, resetPasswordQueryString, resetPasswordDateTime, resetPasswordKey, LoginTypeEnum.EmailAddress, UserTypeEnum.RegularUser, userStatusId, clientId, ipAddress, execUniqueId, loggedInUserId);
                AddAspNetUser(aspNetUserModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);

                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00003000 :: Before AspNetUserRole", "loginNameId1", loginNameId1);
                AspNetUserRoleModel aspNetUserRoleModel = BuildAspNetUserRole(aspNetUserId, aspNetRoleId, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                AddAspNetUserRole(aspNetUserRoleModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);

                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00004000 :: Before DemogInfoAddress", "loginNameId1", loginNameId1);
                DemogInfoAddressModel demogInfoAddressModel = BuildDemogInfoAddress("", "", AddressTypeEnum._, BuildingTypeEnum._, "", ArchLibCache.GetApplicationDefault(clientId, "Currency", "CountryAbbrev"), long.Parse(ArchLibCache.GetApplicationDefault(clientId, "Currency", "DemogInfoCountryId")), aspNetUserId, clientId, ipAddress, execUniqueId, loggedInUserId);
                AddDemogInfoAddress(demogInfoAddressModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);

                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00007000 :: Before Person", "loginNameId1", loginNameId1);
                PersonModel personModel = BuildPerson(aspNetUserId, firstName, lastName, certificateDocumentId, demogInfoAddressModel.DemogInfoAddressId, false, initialsTextId, initialsTextValue, signatureTextId, signatureTextValue, clientId, execUniqueId, ipAddress, loggedInUserId);
                AddPerson(personModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);

                //sqlTransaction.Commit();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return personModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static void CreateContactUs(ContactUsModel contactUsModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                AddContactUs(contactUsModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static void CreateDemogInfoAddress(DemogInfoAddressModel demogInfoAddressModel, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                AddDemogInfoAddress(demogInfoAddressModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                return;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }

        }
        //public static PersonModel CreateResetPassword(string aspNetUserId, string loginNameId1, string loginPassword, DateTime? loginPasswordExpiryDateTime, string phoneNumber, string resetPasswordQueryString, string resetPasswordDateTime, string resetPasswordKey, string firstName, string lastName, string initialsTextId, string initialsTextValue, string signatureTextId, string signatureTextValue, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name;
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Before AspNetRole", "loginNameId1", loginNameId1);
        //        string aspNetRoleId = GetAspNetRole("Default Role", execUniqueId);
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: Before AspNetUser", "loginNameId1", loginNameId1);
        //        AspNetUserModel aspNetUserModel = BuildAspNetUser(aspNetUserId, loginNameId1, loginPassword, loginPasswordExpiryDateTime, phoneNumber, resetPasswordQueryString, resetPasswordDateTime, resetPasswordKey, LoginTypeEnum.EmailAddress, UserTypeEnum.RegularUser, UserStatusEnum.Inactive, clientId, ipAddress, execUniqueId, loggedInUserId);
        //        AddAspNetUser(aspNetUserModel, execUniqueId);

        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00003000 :: Before AspNetUserRole", "loginNameId1", loginNameId1);
        //        AspNetUserRoleModel aspNetUserRoleModel = BuildAspNetUserRole(aspNetUserId, aspNetRoleId, execUniqueId);
        //        AddAspNetUserRole(aspNetUserRoleModel, execUniqueId);

        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00004000 :: Before DemogInfoAddress", "loginNameId1", loginNameId1);
        //        DemogInfoAddressModel demogInfoAddressModel = BuildDemogInfoAddress("", "", AddressTypeEnum._, BuildingTypeEnum._, "", "USA", 236, aspNetUserId, execUniqueId);
        //        AddDemogInfoAddress(demogInfoAddressModel, execUniqueId);

        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00007000 :: Before Person", "loginNameId1", loginNameId1);
        //        PersonModel personModel = BuildPerson(aspNetUserId, firstName, lastName, demogInfoAddressModel.DemogInfoAddressId, false, initialsTextId, initialsTextValue, signatureTextId, signatureTextValue, clientId, execUniqueId, loggedInUserId);
        //        AddPerson(personModel, execUniqueId);

        //        //sqlTransaction.Commit();
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //        return personModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
    }
}
