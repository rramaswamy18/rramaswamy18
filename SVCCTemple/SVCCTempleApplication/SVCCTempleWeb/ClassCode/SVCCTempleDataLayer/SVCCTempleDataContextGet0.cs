using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using Microsoft.Owin.BuilderProperties;
using SVCCTempleModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SVCCTempleDataLayer
{
    public static partial class SVCCTempleDataContext
    {
        public static bool GetLoginUser(string locationNameDesc, string emailAddress, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            SqlCommand sqlCommand = BuildSqlCommandLoginUserSelect(sqlConnection);
            sqlCommand.Parameters["@LocationNameDesc"].Value = locationNameDesc;
            sqlCommand.Parameters["@LoginNameId1"].Value = emailAddress;
            sqlCommand.Parameters["@LoginNameId2"].Value = "";
            sqlCommand.Parameters["@LoginNameId3"].Value = "";
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            var returnValue = sqlDataReader.Read();
            sqlDataReader.Close();
            return returnValue;
        }
        public static LoginUserModel GettLoginUserFromResetPasswordQueryString(string locationNameDesc, string resetPasswordQueryString, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                LoginUserModel loginUserModel = null;
                SqlCommand sqlCommand = BuildSqlCommandLoginUserFromResetPasswordSelect(sqlConnection);
                sqlCommand.Parameters["@LocationNameDesc"].Value = locationNameDesc;
                sqlCommand.Parameters["@ResetPasswordQueryString"].Value = resetPasswordQueryString;
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    loginUserModel = new LoginUserModel
                    {
                        LoginUserId = long.Parse(sqlDataReader["LoginUserId"].ToString()),
                        AspNetUserId = sqlDataReader["AspNetUserId"].ToString(),
                        ResetPasswordExpiryDateTime = sqlDataReader["ResetPasswordExpiryDateTime"].ToString(),
                        ResetPasswordQueryString = sqlDataReader["ResetPasswordQueryString"].ToString(),
                    };
                }
                sqlDataReader.Close();
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return loginUserModel;
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
                }
                catch
                {
                    ;
                }
            }
        }
        private static SqlCommand BuildSqlCommandLoginUserSelect(SqlConnection sqlConnection)
        {
            string sqlStmt = "";

            sqlStmt += "        SELECT LoginUser.LoginUserId";
            sqlStmt += "              ,LoginUser.PersonId";
            sqlStmt += "              ,LoginUser.UserTypeId";
            sqlStmt += "              ,LoginUser.UserStatusId";
            sqlStmt += "              ,LoginUser.LoginPassword";
            sqlStmt += "              ,LoginUser.LoginNameId1";
            sqlStmt += "              ,LoginUser.PasswordExpiryDate";
            sqlStmt += "              ,UserType.CodeDataNameDesc AS UserTypeNameDesc";
            sqlStmt += "              ,UserType.CodeDataNameId AS UserTypeNameId";
            sqlStmt += "              ,UserStatus.CodeDataNameDesc AS UserStatusNameDesc";
            sqlStmt += "              ,Person.FirstName";
            sqlStmt += "              ,Person.LastName";
            sqlStmt += "          FROM LoginUser";
            sqlStmt += "    INNER JOIN Person";
            sqlStmt += "            ON LoginUser.PersonId = Person.PersonId";
            sqlStmt += "    INNER JOIN CodeData AS UserType";
            sqlStmt += "            ON LoginUser.UserTypeId = UserType.CodeDataId";
            sqlStmt += "    INNER JOIN CodeData AS UserStatus";
            sqlStmt += "            ON LoginUser.UserStatusId = UserStatus.CodeDataId";
            sqlStmt += "         WHERE LoginUser.LocationNameDesc = @LocationNameDesc";
            sqlStmt += "           AND LoginUser.LoginNameId1 = @LoginNameId1";
            sqlStmt += "           AND ISNULL(LoginUser.LoginNameId2, '') = @LoginNameId2";
            sqlStmt += "           AND ISNULL(LoginUser.LoginNameId3, '') = @LoginNameId3";

            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@LocationNameDesc", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@LoginNameId1", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@LoginNameId2", SqlDbType.VarChar);
            sqlCommand.Parameters.Add("@LoginNameId3", SqlDbType.VarChar);

            return sqlCommand;
        }
        private static SqlCommand BuildSqlCommandLoginUserFromResetPasswordSelect(SqlConnection sqlConnection)
        {
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM LoginUser WHERE LocationNameDesc = @LocationNameDesc AND ResetPasswordQueryString = @ResetPasswordQueryString", sqlConnection);
            sqlCommand.Parameters.Add("@LocationNameDesc", SqlDbType.VarChar, 50);
            sqlCommand.Parameters.Add("@ResetPasswordQueryString", SqlDbType.VarChar, 512);

            return sqlCommand;
        }
    }
}
