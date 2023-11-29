using ArchitectureLibraryDocumentModels;
using ArchitectureLibraryEnumerations;
using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryDataLayer
{
    public static partial class ArchLibDataContext
    {
        private static ApplicationDefaultModel AssignApplicationDefault(SqlDataReader sqlDataReader, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            //string methodName = MethodBase.GetCurrentMethod().Name;
            //ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            //exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ApplicationDefaultModel applicationDefaultModel;
            applicationDefaultModel = new ApplicationDefaultModel
            {
                ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                KVPKey = sqlDataReader["KVPKey"].ToString(),
                KVPSubKey = sqlDataReader["KVPSubkey"].ToString() == "" ? "" : sqlDataReader["KVPSubkey"].ToString(),
                KVPValue = sqlDataReader["KVPValue"].ToString(),
                SeqNum = double.Parse(sqlDataReader["SeqNum"].ToString()),
            };
            return applicationDefaultModel;
        }
        private static AspNetRoleModel AssignAspNetRole(SqlDataReader sqlDataReader, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            //string methodName = MethodBase.GetCurrentMethod().Name;
            //ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            //exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            AspNetRoleModel aspNetRoleModel = new AspNetRoleModel
            {
                AspNetRoleId = sqlDataReader["AspNetRoleId"].ToString(),
                ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                ActionName = sqlDataReader["ActionName"].ToString(),
                AspNetRoleName = sqlDataReader["AspNetRoleName"].ToString(),
                ControllerName = sqlDataReader["ControllerName"].ToString(),
                Name = sqlDataReader["Name"].ToString(),
                UserTypeId = long.Parse(sqlDataReader["UserTypeId"].ToString()),
            };
            return aspNetRoleModel;
        }
        private static AspNetRoleParentMenu AssignAspNetRoleParentMenu(SqlDataReader sqlDataReader, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            //string methodName = MethodBase.GetCurrentMethod().Name;
            //ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            //exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            AspNetRoleParentMenu aspNetRoleParentMenuModel;
            aspNetRoleParentMenuModel = new AspNetRoleParentMenu
            {
                AspNetRoleParentMenuId = sqlDataReader["AspNetRoleParentMenuId"].ToString(),
                ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                AspNetRoleId = sqlDataReader["AspNetRoleId"].ToString(),
                ParentMenuNameDesc = sqlDataReader["ParentMenuNameDesc"].ToString(),
            };
            return aspNetRoleParentMenuModel;
        }
        private static AspNetUserModel AssignAspNetUser(SqlDataReader sqlDataReader, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            AspNetUserModel aspNetUserModel = new AspNetUserModel
            {
                AspNetUserId = sqlDataReader["AspNetUserId"].ToString(),
                ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                Email = sqlDataReader["Email"].ToString(),
                TelephoneCountryId = long.Parse(sqlDataReader["TelephoneCountryId"].ToString()),
                PhoneNumber = sqlDataReader["PhoneNumber"].ToString(),
                UserName = sqlDataReader["UserName"].ToString(),
            };
            return aspNetUserModel;
        }
        private static ClientModel AssignClient(SqlDataReader sqlDataReader, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            ClientModel clientModel = new ClientModel
            {
                ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                ClientName = sqlDataReader["ClientName"].ToString(),
                ClientDesc = sqlDataReader["ClientDesc"].ToString(),
                ParentClientId = sqlDataReader["ClientId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["ClientId"].ToString()),
                WebSiteUrl = sqlDataReader["WebSiteUrl"].ToString(),
            };
            return clientModel;
        }
        private static void AssignContactUs(ContactUsModel contactUsModel, SqlCommand sqlCommand, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            sqlCommand.Parameters["@ClientId"].Value = clientId;
            sqlCommand.Parameters["@ContactUsTypeId"].Value = contactUsModel.ContactUsTypeId == null ? 0 : contactUsModel.ContactUsTypeId;
            sqlCommand.Parameters["@FirstName"].Value = contactUsModel.FirstName;
            sqlCommand.Parameters["@FirstName"].Value = contactUsModel.FirstName;
            sqlCommand.Parameters["@LastName"].Value = contactUsModel.LastName;
            sqlCommand.Parameters["@EmailAddress"].Value = contactUsModel.EmailAddress;
            sqlCommand.Parameters["@TelephoneNumber"].Value = contactUsModel.ContactUsTelephoneNumber;
            sqlCommand.Parameters["@CommentsRequests"].Value = contactUsModel.CommentsRequests ?? (object)DBNull.Value;// : contactUsModel.CommentsRequests;
        }
        private static DemogInfoAddressModel AssignDemogInfoAddress(SqlDataReader sqlDataReader, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            DemogInfoAddressModel demogInfoAddressModel = new DemogInfoAddressModel
            {
                DemogInfoAddressId = long.Parse(sqlDataReader["DemogInfoAddressId"].ToString()),
                ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                AddressLine1 = sqlDataReader["AddressLine1"].ToString(),
                AddressLine2 = sqlDataReader["AddressLine2"].ToString(),
                AddressTypeId = (AddressTypeEnum)long.Parse(sqlDataReader["AddressTypeId"].ToString()),
                BuildingTypeId = (BuildingTypeEnum)long.Parse(sqlDataReader["BuildingTypeId"].ToString()),
                CityName = sqlDataReader["CityName"].ToString(),
                CountryAbbrev = sqlDataReader["CountryAbbrev"].ToString(),
                CountyName = sqlDataReader["CountyName"].ToString(),
                DemogInfoCityId = sqlDataReader["DemogInfoCityId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["DemogInfoCityId"].ToString()),
                DemogInfoCountryId = sqlDataReader["DemogInfoCountryId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["DemogInfoCountryId"].ToString()),
                DemogInfoSubDivisionId = sqlDataReader["DemogInfoSubDivisionId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["DemogInfoSubDivisionId"].ToString()),
                DemogInfoZipId = sqlDataReader["DemogInfoZipId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["DemogInfoZipId"].ToString()),
                DemogInfoZipPlusId = sqlDataReader["DemogInfoZipPlusId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["DemogInfoZipPlusId"].ToString()),
                HouseNumber = sqlDataReader["HouseNumber"].ToString(),
                StateAbbrev = sqlDataReader["StateAbbrev"].ToString(),
                ZipCode = sqlDataReader["ZipCode"].ToString(),
                ZipPlus4 = sqlDataReader["ZipPlus4"].ToString(),
            };
            return demogInfoAddressModel;
        }
        private static PersonModel AssignPerson(SqlDataReader sqlDataReader, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            PersonModel personModel = new PersonModel
            {
                PersonId = long.Parse(sqlDataReader["PersonId"].ToString()),
                ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                AspNetUserId = sqlDataReader["AspNetUserId"].ToString(),
                CertificateDocumentId = long.Parse(sqlDataReader["CertificateDocumentId"].ToString()),
                CitizenshipId = sqlDataReader["CitizenshipId"].ToString() == "" ? (CitizenshipEnum?)null : (CitizenshipEnum)long.Parse(sqlDataReader["CitizenshipId"].ToString()),
                DateOfBirth = sqlDataReader["DateOfBirth"].ToString() == "" ? (DateTime?)null : DateTime.Parse(sqlDataReader["DateOfBirth"].ToString()),
                DriverLicenseDemogInfoSubDivisionId = sqlDataReader["DriverLicenseDemogInfoSubDivisionId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["DriverLicenseDemogInfoSubDivisionId"].ToString()),
                DriverLicenseExpiryDate = sqlDataReader["DriverLicenseExpiryDate"].ToString() == "" ? (DateTime?)null : DateTime.Parse(sqlDataReader["DriverLicenseExpiryDate"].ToString()),
                DriverLicenseNumber = sqlDataReader["DriverLicenseNumber"].ToString(),
                DriverLicenseType = sqlDataReader["DriverLicenseType"].ToString(),
                ElectronicSignatureConsent = sqlDataReader["ElectronicSignatureConsent"].ToString() == "" ? null : DateTime.Parse(sqlDataReader["ElectronicSignatureConsent"].ToString()).ToString("MMM-dd-yyyy hh:mm:ss tt"),
                ElectronicSignatureConsentAccepted = bool.Parse(sqlDataReader["ElectronicSignatureConsentAccepted"].ToString()),
                FirstName = sqlDataReader["FirstName"].ToString(),
                HomeDemogInfoAddressId = long.Parse(sqlDataReader["HomeDemogInfoAddressId"].ToString()),
                InitialsTextId = long.Parse(sqlDataReader["InitialsTextId"].ToString()),
                InitialsTextValue = sqlDataReader["InitialsTextValue"].ToString(),
                LastName = sqlDataReader["LastName"].ToString(),
                MaritalStatusId = sqlDataReader["MaritalStatusId"].ToString() == "" ? (MaritalStatusEnum?)null : (MaritalStatusEnum)long.Parse(sqlDataReader["MaritalStatusId"].ToString()),
                MiddleName = sqlDataReader["MiddleName"].ToString(),
                //MilitaryServiceId = sqlDataReader["MilitaryServiceId"].ToString() == "" ? (YesNoEnum?)null : (YesNoEnum)long.Parse(sqlDataReader["MilitaryServiceId"].ToString()),
                NicknameFirst = sqlDataReader["NicknameFirst"].ToString(),
                NicknameLast = sqlDataReader["NicknameLast"].ToString(),
                SalutationId = sqlDataReader["SalutationId"].ToString() == "" ? (SalutationEnum?)null : (SalutationEnum)long.Parse(sqlDataReader["SalutationId"].ToString()),
                SignatureTextId = long.Parse(sqlDataReader["SignatureTextId"].ToString()),
                SignatureTextValue = sqlDataReader["SignatureTextValue"].ToString(),
                SSN = sqlDataReader["SSN"].ToString(),
                SuffixId = sqlDataReader["SuffixId"].ToString() == "" ? (SuffixEnum?)null : (SuffixEnum)long.Parse(sqlDataReader["SuffixId"].ToString()),
            };
            return personModel;
        }
        private static UserProfileMetaDataModel AssignUserProfileMetaData(SqlDataReader sqlDataReader, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            UserProfileMetaDataModel userProfileMetaDataModeu = new UserProfileMetaDataModel
            {
                UserProfileMetaDataId = long.Parse(sqlDataReader["UserProfileMetaDataId"].ToString()),
                ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                MetaDataName = sqlDataReader["MetaDataName"].ToString(),
                SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                IsMapped = bool.Parse(sqlDataReader["IsMapped"].ToString()),
            };
            return userProfileMetaDataModeu;
        }
        private static SalesTaxListModel AssignSalesTaxList(SqlDataReader sqlDataReader, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            SalesTaxListModel salesTaxListModeu = new SalesTaxListModel
            {
                SalesTaxListId = long.Parse(sqlDataReader["SalesTaxListId"].ToString()),
                ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                BegEffDate = DateTime.Parse(sqlDataReader["BegEffDate"].ToString()),
                DestDemogInfoCountryId = sqlDataReader["DestDemogInfoCountryId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["DestDemogInfoCountryId"].ToString()),
                DestDemogInfoSubDivisionId = sqlDataReader["DestDemogInfoSubDivisionId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["DestDemogInfoSubDivisionId"].ToString()),
                DestDemogInfoCountyId = sqlDataReader["DestDemogInfoCountyId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["DestDemogInfoCountyId"].ToString()),
                DestDemogInfoCityId = sqlDataReader["DestDemogInfoCityId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["DestDemogInfoCityId"].ToString()),
                DestDemogInfoZipId = sqlDataReader["DestDemogInfoZipId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["DestDemogInfoZipId"].ToString()),
                EndEffDate = DateTime.Parse(sqlDataReader["EndEffDate"].ToString()),
                SalesTaxCaptionId = sqlDataReader["SalesTaxCaptionId"].ToString(),
                SrceDemogInfoCountryId = sqlDataReader["SrceDemogInfoCountryId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["SrceDemogInfoCountryId"].ToString()),
                SrceDemogInfoSubDivisionId = sqlDataReader["SrceDemogInfoSubDivisionId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["SrceDemogInfoSubDivisionId"].ToString()),
                SrceDemogInfoCountyId = sqlDataReader["SrceDemogInfoCountyId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["SrceDemogInfoCountyId"].ToString()),
                SrceDemogInfoCityId = sqlDataReader["SrceDemogInfoCityId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["SrceDemogInfoCityId"].ToString()),
                SrceDemogInfoZipId = sqlDataReader["SrceDemogInfoZipId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["SrceDemogInfoZipId"].ToString()),
                SalesTaxRate = float.Parse(sqlDataReader["SalesTaxRate"].ToString()),
                ShowOnInvoice = bool.Parse(sqlDataReader["ShowOnInvoice"].ToString()),
            };
            return salesTaxListModeu;
        }
    }
}
