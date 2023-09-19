using ArchitectureLibraryUtility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using ArchitectureLibraryException;

namespace SVCCTempleDataLayer
{
    public static partial class SVCCTempleDataContext
    {
        public static long? AddPerson(string locationNameDesc, string emailAddress, string salutationId, string firstName, string middleName, string lastName, string suffixId, string dateOfBirth, string primaryTelephoneNum, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                long? personId = null;
                SqlCommand sqlCommand = BuildSqlCommadPersonInsert(sqlConnection);

                sqlCommand.Parameters["@LocationNameDesc"].Value = locationNameDesc;
                sqlCommand.Parameters["@SalutationId"].Value = salutationId;
                sqlCommand.Parameters["@FirstName"].Value = firstName;
                sqlCommand.Parameters["@MiddleName"].Value = middleName;
                sqlCommand.Parameters["@LastName"].Value = lastName;
                sqlCommand.Parameters["@SuffixId"].Value = suffixId;
                sqlCommand.Parameters["@DateOfBirth"].Value = dateOfBirth;
                sqlCommand.Parameters["@PrimaryAddressId"].Value = "0";
                sqlCommand.Parameters["@PrimaryTelephoneNum"].Value = primaryTelephoneNum;
                sqlCommand.Parameters["@PrimaryTelephoneExtn"].Value = "";
                sqlCommand.Parameters["@PrimaryEmailAddress"].Value = emailAddress;
                sqlCommand.Parameters["@AlternateAddressId"].Value = "0";
                sqlCommand.Parameters["@AlternateTelephoneNum"].Value = "0";
                sqlCommand.Parameters["@AlternateTelephoneExtn"].Value = "";
                sqlCommand.Parameters["@AlternateEmailAddress"].Value = "";

                personId = (long)sqlCommand.ExecuteScalar();

                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return personId;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Error", exception);
                throw;
            }
        }
        public static long? AddLoginUser(string locationNameDesc, long personId, string registerEmailAddress, long userTypeId, long userStatusId, string resetPasswordQueryString, string resetPasswordExpiryDateTime, string resetPasswordKey, string aspNetUserId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = new ExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());//, currentMethodDeclaringType_);// MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                long? loginUserId = null;

                SqlCommand sqlCommand = BuildSqlCommadLoginUserInsert(sqlConnection);

                sqlCommand.Parameters["@LoginNameId1"].Value = registerEmailAddress;
                sqlCommand.Parameters["@LoginNameId2"].Value = "";
                sqlCommand.Parameters["@LoginNameId3"].Value = "";
                sqlCommand.Parameters["@LocationNameDesc"].Value = locationNameDesc;
                sqlCommand.Parameters["@LoginTypeNameDesc"].Value = "EMAIL_ADDRESS";
                sqlCommand.Parameters["@zzz_LoginPassword"].Value = "";
                sqlCommand.Parameters["@LoginPassword"].Value = "";
                sqlCommand.Parameters["@PasswordExpiryDate"].Value = "1900-01-01";
                sqlCommand.Parameters["@UserTypeId"].Value = userTypeId;//"8";
                sqlCommand.Parameters["@UserStatusId"].Value = userStatusId;// "9";
                sqlCommand.Parameters["@PersonId"].Value = personId;
                sqlCommand.Parameters["@ResetPasswordQueryString"].Value = resetPasswordQueryString;
                sqlCommand.Parameters["@ResetPasswordExpiryDateTime"].Value = resetPasswordExpiryDateTime;
                sqlCommand.Parameters["@ResetPasswordKey"].Value = resetPasswordKey;
                sqlCommand.Parameters["@AspNetUserId"].Value = aspNetUserId;

                loginUserId = (long)sqlCommand.ExecuteScalar();

                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return loginUserId;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Error", exception);
                throw;
            }
        }
        private static SqlCommand BuildSqlCommadPersonInsert(SqlConnection sqlConnection)
        {
            string sqlStmt = "";

            sqlStmt += "INSERT Person(LocationNameDesc, SalutationId, FirstName, MiddleName, LastName, SuffixId, DateOfBirth, PrimaryAddressId, PrimaryTelephoneNum, PrimaryTelephoneExtn, PrimaryEmailAddress, AlternateAddressId, AlternateTelephoneNum, AlternateTelephoneExtn, AlternateEmailAddress) OUTPUT INSERTED.PersonId VALUES(@LocationNameDesc, @SalutationId, @FirstName, @MiddleName, @LastName, @SuffixId, @DateOfBirth, @PrimaryAddressId, @PrimaryTelephoneNum, @PrimaryTelephoneExtn, @PrimaryEmailAddress, @AlternateAddressId, @AlternateTelephoneNum, @AlternateTelephoneExtn, @AlternateEmailAddress)";
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);

            sqlCommand.Parameters.Add("@LocationNameDesc", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@SalutationId", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@FirstName", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@MiddleName", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@LastName", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@SuffixId", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@DateOfBirth", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@PrimaryAddressId", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@PrimaryTelephoneNum", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@PrimaryTelephoneExtn", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@PrimaryEmailAddress", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@AlternateAddressId", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@AlternateTelephoneNum", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@AlternateTelephoneExtn", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@AlternateEmailAddress", SqlDbType.VarChar);

            return sqlCommand;
        }
        private static SqlCommand BuildSqlCommadLoginUserInsert(SqlConnection sqlConnection)
        {
            string sqlStmt = "";

            sqlStmt += "INSERT LoginUser(LoginNameId1, LoginNameId2, LoginNameId3, LocationNameDesc, LoginTypeNameDesc, zzz_LoginPassword, LoginPassword, PasswordExpiryDate, UserTypeId, UserStatusId, PersonId, ResetPasswordQueryString, ResetPasswordExpiryDateTime, ResetPasswordKey, AspNetUserId) OUTPUT INSERTED.LoginUserId VALUES(@LoginNameId1, @LoginNameId2, @LoginNameId3, @LocationNameDesc, @LoginTypeNameDesc, @zzz_LoginPassword, @LoginPassword, @PasswordExpiryDate, @UserTypeId, @UserStatusId, @PersonId, @ResetPasswordQueryString, @ResetPasswordExpiryDateTime, @ResetPasswordKey, @AspNetUserId)";
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);

            sqlCommand.Parameters.Add("@LoginNameId1", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@LoginNameId2", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@LoginNameId3", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@LocationNameDesc", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@LoginTypeNameDesc", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@zzz_LoginPassword", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@LoginPassword", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@PasswordExpiryDate", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@UserTypeId", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@UserStatusId", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@PersonId", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@ResetPasswordQueryString", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@ResetPasswordExpiryDateTime", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@ResetPasswordKey", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@AspNetUserId", SqlDbType.VarChar);

            return sqlCommand;
        }
    }
}
