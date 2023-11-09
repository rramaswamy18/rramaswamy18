using ArchitectureLibraryDocumentDataLayer;
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
        public static AspNetUserModel GetAspNetUserFromResetPasswordQueryString(string resetPasswordQueryString, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                sqlStmt += "        SELECT *" + Environment.NewLine;
                sqlStmt += "          FROM ArchLib.AspNetUser WHERE ResetPasswordQueryString = '" + resetPasswordQueryString + "'" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                AspNetUserModel aspNetUserModel;
                if (sqlDataReader.Read())
                {
                    aspNetUserModel = new AspNetUserModel
                    {
                        AspNetUserId = sqlDataReader["AspNetUserId"].ToString(),
                        ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                        Email = sqlDataReader["Email"].ToString(),
                        UserName = sqlDataReader["UserName"].ToString(),
                        LoginPassword = sqlDataReader["LoginPassword"].ToString(),
                        ResetPasswordCompletedDateTime = sqlDataReader["ResetPasswordCompletedDateTime"].ToString(),
                        ResetPasswordQueryString = sqlDataReader["ResetPasswordQueryString"].ToString(),
                        ResetPasswordExpiryDateTime = sqlDataReader["ResetPasswordExpiryDateTime"].ToString(),
                        ResetPasswordKey = sqlDataReader["ResetPasswordKey"].ToString(),
                    };
                }
                else
                {
                    aspNetUserModel = null;
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return aspNetUserModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static PersonModel GetPersonFromEmailAddress(string loginEmailAddress, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SqlDataReader sqlDataReader = null;
            try
            {
                #region
                string sqlStmt = "";
                sqlStmt += "        SELECT " + Environment.NewLine;
                sqlStmt += "               Person.*" + Environment.NewLine;
                sqlStmt += "              ,AspNetUser.*" + Environment.NewLine;
                sqlStmt += "              ,AspNetRole.*" + Environment.NewLine;
                sqlStmt += "              ,AspNetUserRole.*" + Environment.NewLine;
                sqlStmt += "              ,DemogInfoAddress.*" + Environment.NewLine;
                sqlStmt += "          FROM ArchLib.Person" + Environment.NewLine;
                sqlStmt += "    INNER JOIN ArchLib.DemogInfoAddress" + Environment.NewLine;
                sqlStmt += "            ON Person.HomeDemogInfoAddressId = DemogInfoAddress.DemogInfoAddressId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN ArchLib.AspNetUser" + Environment.NewLine;
                sqlStmt += "            ON Person.AspNetUserId = AspNetUser.AspNetUserId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN ArchLib.AspNetUserRole" + Environment.NewLine;
                sqlStmt += "            ON AspNetUser.AspNetUserId = AspNetUserRole.AspNetUserId" + Environment.NewLine;
                sqlStmt += "    INNER JOIN ArchLib.AspNetRole" + Environment.NewLine;
                sqlStmt += "            ON AspNetUserRole.AspNetRoleId = AspNetRole.AspNetRoleId" + Environment.NewLine;
                sqlStmt += "         WHERE AspNetUser.UserName = '" + loginEmailAddress + "'" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                #endregion
                sqlDataReader = sqlCommand.ExecuteReader();
                PersonModel personModel;
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Before Reading the data  SqlReader", "Username", loginEmailAddress);
                if (sqlDataReader.Read())
                {
                    //personModel = PopulatePersonModel(sqlDataReader, execUniqueId);
                    personModel = new PersonModel
                    {
                        AspNetUserModel = new AspNetUserModel
                        {
                            AspNetUserId = sqlDataReader["AspNetUserId"].ToString(),
                            Email = sqlDataReader["Email"].ToString(),
                            EmailConfirmationToken = sqlDataReader["EmailConfirmationToken"].ToString(),
                            EmailConfirmed = bool.Parse(sqlDataReader["EmailConfirmed"].ToString()),
                            LockoutEnabled = bool.Parse(sqlDataReader["LockoutEnabled"].ToString()),
                            LockoutEndDateUtc = sqlDataReader["LockoutEndDateUtc"].ToString(),
                            LoginPassword = sqlDataReader["LoginPassword"].ToString(),
                            LoginTypeId = sqlDataReader["LoginTypeId"].ToString() == "" ? (LoginTypeEnum?)null : (LoginTypeEnum)int.Parse(sqlDataReader["LoginTypeId"].ToString()),
                            PasswordExpiry = sqlDataReader["PasswordExpiry"].ToString(),
                            PasswordHash = sqlDataReader["PasswordHash"].ToString(),
                            PhoneNumber = sqlDataReader["PhoneNumber"].ToString(),
                            PhoneNumberConfirmed = bool.Parse(sqlDataReader["PhoneNumberConfirmed"].ToString()),
                            ResetPasswordExpiryDateTime = sqlDataReader["ResetPasswordExpiryDateTime"].ToString(),
                            ResetPasswordQueryString = sqlDataReader["ResetPasswordQueryString"].ToString(),
                            UserName = sqlDataReader["UserName"].ToString(),
                            UserStatusId = (UserStatusEnum)int.Parse(sqlDataReader["UserStatusId"].ToString()),
                            UserTypeId = sqlDataReader["UserTypeId"].ToString() == "" ? (UserTypeEnum?)null : (UserTypeEnum)int.Parse(sqlDataReader["UserTypeId"].ToString()),
                            AspNetUserRoleModel = new AspNetUserRoleModel
                            {
                                AspNetRoleModel = new AspNetRoleModel
                                {
                                    AspNetRoleId = sqlDataReader["AspNetRoleId"].ToString(),
                                    Name = sqlDataReader["Name"].ToString(),
                                    AspNetRoleName = sqlDataReader["AspNetRoleName"].ToString(),
                                    ControllerName = sqlDataReader["ControllerName"].ToString(),
                                    ActionName = sqlDataReader["ActionName"].ToString(),
                                }
                            },
                        },
                        AspNetUserId = sqlDataReader["AspNetUserId"].ToString(),
                        CorpAcctId = long.Parse(sqlDataReader["CorpAcctId"].ToString()),
                        DateOfBirth = sqlDataReader["DateOfBirth"].ToString() == "" ? (DateTime?)null : DateTime.Parse(sqlDataReader["DateOfBirth"].ToString()),
                        DriverLicenseDemogInfoSubDivisionId = sqlDataReader["DriverLicenseDemogInfoSubDivisionId"].ToString() == "" ? (long?)null : long.Parse(sqlDataReader["DriverLicenseDemogInfoSubDivisionId"].ToString()),
                        DriverLicenseExpiryDate = sqlDataReader["DriverLicenseExpiryDate"].ToString() == "" ? (DateTime?)null : DateTime.Parse(sqlDataReader["DriverLicenseExpiryDate"].ToString()),
                        DriverLicenseNumber = sqlDataReader["DriverLicenseNumber"].ToString(),
                        DriverLicenseType = sqlDataReader["DriverLicenseType"].ToString(),
                        FirstName = sqlDataReader["FirstName"].ToString(),
                        InitialsTextId = long.Parse(sqlDataReader["InitialsTextId"].ToString()),
                        InitialsTextValue = sqlDataReader["InitialsTextValue"].ToString(),
                        //HomeDemogInfoAddressModel = new DemogInfoAddressModel
                        //{
                        //    DemogInfoAddressId = long.Parse(sqlDataReader["DemogInfoAddressId"].ToString()),
                        //},
                        LastName = sqlDataReader["LastName"].ToString(),
                        MiddleName = sqlDataReader["MiddleName"].ToString(),
                        NicknameFirst = sqlDataReader["NicknameFirst"].ToString(),
                        NicknameLast = sqlDataReader["NicknameLast"].ToString(),
                        PersonId = long.Parse(sqlDataReader["PersonId"].ToString()),
                        SignatureTextId = long.Parse(sqlDataReader["SignatureTextId"].ToString()),
                        SignatureTextValue = sqlDataReader["SignatureTextValue"].ToString(),
                        SSN = sqlDataReader["SSN"].ToString(),
                        StatusId = sqlDataReader["StatusId"].ToString() == "" ? (GenericStatusEnum?)null : (GenericStatusEnum)int.Parse(sqlDataReader["StatusId"].ToString()),
                    };
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001100 :: After reading the data", "Username", loginEmailAddress);
                }
                else
                {
                    personModel = null;
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001200 :: Unable to read the data", "Username", loginEmailAddress);
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return personModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            finally
            {
                try
                {
                    sqlDataReader.Close();
                }
                catch
                {
                    ;
                }
            }
        }
        public static PersonModel GetPersonAspNetUserFromAspNetUserId(string aspNetUserId, string documentColumnPrefix, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                //Join to Document should not be here
                //This is a remote system - This is done for performance
                string sqlStmt = "";
                sqlStmt += "        SELECT Person.*" + Environment.NewLine;
                sqlStmt += "              ,AspNetUser.*" + Environment.NewLine;
                sqlStmt += "              ,DemogInfoAddress.*" + Environment.NewLine;
                sqlStmt += "              ,Document.DocumentId AS CertificateDocument_DocumentId" + Environment.NewLine;
                sqlStmt += "              ,Document.ClientId AS CertificateDocument_ClientId" + Environment.NewLine;
                sqlStmt += "              ,Document.ClientFileName AS CertificateDocument_ClientFileName" + Environment.NewLine;
                sqlStmt += "              ,Document.ClientHeight AS CertificateDocument_ClientHeight" + Environment.NewLine;
                sqlStmt += "              ,Document.ClientHeightUnit AS CertificateDocument_ClientHeightUnit" + Environment.NewLine;
                sqlStmt += "              ,Document.ClientWidth AS CertificateDocument_ClientWidth" + Environment.NewLine;
                sqlStmt += "              ,Document.ClientWidthUnit AS CertificateDocument_ClientWidthUnit" + Environment.NewLine;
                sqlStmt += "              ,Document.ContentData AS CertificateDocument_ContentData" + Environment.NewLine;
                sqlStmt += "              ,Document.ContentLength AS CertificateDocument_ContentLength" + Environment.NewLine;
                sqlStmt += "              ,Document.ContentType AS CertificateDocument_ContentType" + Environment.NewLine;
                sqlStmt += "              ,Document.FileExtension AS CertificateDocument_FileExtension" + Environment.NewLine;
                sqlStmt += "              ,Document.Height AS CertificateDocument_Height" + Environment.NewLine;
                sqlStmt += "              ,Document.HeightUnit AS CertificateDocument_HeightUnit" + Environment.NewLine;
                sqlStmt += "              ,Document.ServerFileName AS CertificateDocument_ServerFileName" + Environment.NewLine;
                sqlStmt += "              ,Document.Width AS CertificateDocument_Width" + Environment.NewLine;
                sqlStmt += "              ,Document.WidthUnit AS CertificateDocument_WidthUnit" + Environment.NewLine;
                sqlStmt += "          FROM ArchLib.Person" + Environment.NewLine;
                sqlStmt += "   INNER  JOIN ArchLib.AspNetUser" + Environment.NewLine;
                sqlStmt += "            ON Person.AspNetUserId = AspNetUser.AspNetUserId" + Environment.NewLine;
                sqlStmt += "   INNER  JOIN ArchLib.DemogInfoAddress" + Environment.NewLine;
                sqlStmt += "            ON Person.HomeDemogInfoAddressId = DemogInfoAddress.DemogInfoAddressId" + Environment.NewLine;
                sqlStmt += "   INNER  JOIN ArchLib.Document" + Environment.NewLine;
                sqlStmt += "            ON Person.CertificateDocumentId = Document.DocumentId" + Environment.NewLine;
                sqlStmt += "         WHERE Person.AspNetUserId = '" + aspNetUserId + "'" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                PersonModel personModel;
                if (sqlDataReader.Read())
                {
                    personModel = AssignPerson(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId);
                    personModel.AspNetUserModel = AssignAspNetUser(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId);
                    personModel.HomeDemogInfoAddressModel = AssignDemogInfoAddress(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId);
                    personModel.CertificateDocumentModel = ArchLibDocumentDataContext.AssignDocumentModel(sqlDataReader, documentColumnPrefix, clientId, ipAddress, execUniqueId, loggedInUserId);
                    if (personModel.CertificateDocumentModel.DocumentId == 0)
                    {
                        personModel.CertificateDocumentModel.ServerFileName = null;
                    }
                }
                else
                {
                    personModel = null;
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return personModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static PersonModel GetPersonFromAspNetUserId(string aspNetUserId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                sqlStmt += "        SELECT *" + Environment.NewLine;
                sqlStmt += "          FROM ArchLib.Person WHERE AspNetUserId = '" + aspNetUserId + "'" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                PersonModel personModel;
                if (sqlDataReader.Read())
                {
                    personModel = new PersonModel
                    {
                        AspNetUserId = sqlDataReader["AspNetUserId"].ToString(),
                    };
                }
                else
                {
                    personModel = null;
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return personModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public static PersonModel GetPersonAspNetUserFromPersonId(long personId, string documentColumnPrefix, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string sqlStmt = "";
                sqlStmt += "        SELECT *" + Environment.NewLine;
                sqlStmt += "          FROM ArchLib.Person" + Environment.NewLine;
                sqlStmt += "   INNER  JOIN ArchLib.AspNetUser" + Environment.NewLine;
                sqlStmt += "            ON Person.AspNetUserId = AspNetUser.AspNetUserId" + Environment.NewLine;
                sqlStmt += "   INNER  JOIN ArchLib.DemogInfoAddress" + Environment.NewLine;
                sqlStmt += "            ON Person.HomeDemogInfoAddressId = DemogInfoAddress.DemogInfoAddressId" + Environment.NewLine;
                sqlStmt += "   INNER  JOIN ArchLib.Document AS CertificateDocument" + Environment.NewLine;
                sqlStmt += "            ON Person.CertificateDocumentId = CertificateDocument.DocumentId" + Environment.NewLine;
                sqlStmt += "         WHERE Person.PersonId = " + personId + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                PersonModel personModel;
                if (sqlDataReader.Read())
                {
                    personModel = AssignPerson(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId);
                    personModel.AspNetUserModel = AssignAspNetUser(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId);
                    personModel.CertificateDocumentModel = ArchLibDocumentDataContext.AssignDocumentModel(sqlDataReader, documentColumnPrefix, clientId, ipAddress, execUniqueId, loggedInUserId);
                    personModel.HomeDemogInfoAddressModel = AssignDemogInfoAddress(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId);
                }
                else
                {
                    personModel = null;
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return personModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        private static AspNetUserModel GetAspNetUserFromUserName(string userName, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SqlDataReader sqlDataReader = null;
            try
            {
                string sqlStmt = "";
                sqlStmt += "        SELECT *" + Environment.NewLine;
                sqlStmt += "          FROM ArchLib.AspNetUser" + Environment.NewLine;
                sqlStmt += "     LEFT JOIN ArchLib.Person" + Environment.NewLine;
                sqlStmt += "            ON AspNetUser.AspNetUserId = Person.AspNetUserId" + Environment.NewLine;
                sqlStmt += "         WHERE ISNULL(UserName, '') != ''" + Environment.NewLine;
                sqlStmt += "           AND UserName = '" + userName + "'" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                AspNetUserModel aspNetUserModel;
                if (sqlDataReader.Read())
                {
                    aspNetUserModel = new AspNetUserModel
                    {
                        AspNetUserId = sqlDataReader["AspNetUserId"].ToString(),
                        Email = sqlDataReader["Email"].ToString(),
                        UserName = sqlDataReader["UserName"].ToString(),
                        LoginPassword = sqlDataReader["LoginPassword"].ToString(),
                        ResetPasswordQueryString = sqlDataReader["ResetPasswordQueryString"].ToString(),
                        PersonModel = new PersonModel
                        {
                            FirstName = sqlDataReader["FirstName"].ToString(),
                            LastName = sqlDataReader["LastName"].ToString(),
                            PersonId = long.Parse(sqlDataReader["PersonId"].ToString()),
                        },
                    };
                }
                else
                {
                    aspNetUserModel = null;
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return aspNetUserModel;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            finally
            {
                try
                {
                    sqlDataReader.Close();
                }
                catch
                {

                }
            }
        }
        private static string GetAspNetRole(string Name, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {

                string sqlStmt = "";
                sqlStmt += "        SELECT *" + Environment.NewLine;
                sqlStmt += "          FROM ArchLib.AspNetRole WHERE Name = '" + Name + "'" + Environment.NewLine;
                SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                string aspNetRoleId;
                if (sqlDataReader.Read())
                {
                    aspNetRoleId = sqlDataReader["AspNetRoleId"].ToString();
                }
                else
                {
                    aspNetRoleId = null;
                }
                sqlDataReader.Close();
                return aspNetRoleId;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        //private static AspNetUserModel GetAspNetUserFromUserName(string userName, string execUniqueId)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        string sqlStmt = "";
        //        sqlStmt += "        SELECT *" + Environment.NewLine;
        //        sqlStmt += "          FROM ArchLib.AspNetUser" + Environment.NewLine;
        //        sqlStmt += "    INNER JOIN ArchLib.Person" + Environment.NewLine;
        //        sqlStmt += "            ON AspNetUser.AspNetUserId = Person.AspNetUserId" + Environment.NewLine;
        //        sqlStmt += "         WHERE UserName = '" + userName + "'" + Environment.NewLine;
        //        SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
        //        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
        //        AspNetUserModel aspNetUserModel;
        //        if (sqlDataReader.Read())
        //        {
        //            aspNetUserModel = new AspNetUserModel
        //            {
        //                AspNetUserId = sqlDataReader["AspNetUserId"].ToString(),
        //                Email = sqlDataReader["Email"].ToString(),
        //                UserName = sqlDataReader["UserName"].ToString(),
        //                LoginPassword = sqlDataReader["LoginPassword"].ToString(),
        //                ResetPasswordQueryString = sqlDataReader["ResetPasswordQueryString"].ToString(),
        //                PersonModel = new PersonModel
        //                {
        //                    FirstName = sqlDataReader["FirstName"].ToString(),
        //                    LastName = sqlDataReader["LastName"].ToString(),
        //                    PersonId = long.Parse(sqlDataReader["PersonId"].ToString()),
        //                },
        //            };
        //        }
        //        else
        //        {
        //            aspNetUserModel = null;
        //        }
        //        sqlDataReader.Close();
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        //        return aspNetUserModel;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
        //        throw;
        //    }
        //}
    }
}
