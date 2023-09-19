using ArchitectureLibraryDocumentDataLayer;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
//using SchoolPrdEnumerations;
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
        public static ClassEnrollModel GetClassEnroll(long classEnrollId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            SqlCommand sqlCommand = BuildClassEnrollSelect(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
            sqlCommand.Parameters["@ClassEnrollId"].Value = classEnrollId;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            ClassEnrollModel classEnrollModel;
            if (sqlDataReader.Read())
            {
                classEnrollModel = AssignClassEnroll(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId);
            }
            else
            {
                classEnrollModel = new ClassEnrollModel();
            }
            sqlDataReader.Close();
            return classEnrollModel;
        }
        public static ClassListModel GetClassList(long classListId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            SqlCommand sqlCommand = BuildClassListSelect(sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
            sqlCommand.Parameters["@ClassListId"].Value = classListId;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            ClassListModel classListModel;
            if (sqlDataReader.Read())
            {
                classListModel = AssignClassList(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId);
            }
            else
            {
                classListModel = new ClassListModel();
            }
            sqlDataReader.Close();
            return classListModel;
        }

        //Review Later
        public static float GetClassSessionMaxSeqNum(long classListId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            SqlCommand sqlCommand = BuildClassSessionMaxSeqNumGet(sqlConnection);
            sqlCommand.Parameters["@ClassListId"].Value = classListId;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            float seqNum;
            if (sqlDataReader.Read())
            {
                if (sqlDataReader[0].ToString() == "")
                {
                    seqNum = 0;
                }
                else
                {
                    seqNum = float.Parse(sqlDataReader[0].ToString());
                }
            }
            else
            {
                seqNum = 0;
            }
            sqlDataReader.Close();
            return seqNum;
        }
        public static float GetClassFeesMaxSeqNum(long classListId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            SqlCommand sqlCommand = BuildClassFeesMaxSeqNumGet(sqlConnection);
            sqlCommand.Parameters["@ClassListId"].Value = classListId;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            float seqNum;
            if (sqlDataReader.Read())
            {
                if (sqlDataReader[0].ToString() == "")
                {
                    seqNum = 0;
                }
                else
                {
                    seqNum = float.Parse(sqlDataReader[0].ToString());
                }
            }
            else
            {
                seqNum = 0;
            }
            sqlDataReader.Close();
            return seqNum;
        }
        public static HolidayModel GetHoliday(long holidayId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            SqlCommand sqlCommand = BuildSelectHoliday(sqlConnection);
            sqlCommand.Parameters["@holidayId"].Value = holidayId;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            HolidayModel holidayModel;
            if (sqlDataReader.Read())
            {
                holidayModel = AssignHoliday(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId);
            }
            else
            {
                holidayModel = new HolidayModel();
            }
            return holidayModel;
        }
        public static ClassEnrollModel GetClassEnrollFromPersonId(long personId, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            SqlCommand sqlCommand = BuildSelectClassEnrollFromPersonId(sqlConnection);
            sqlCommand.Parameters["@PersonId"].Value = personId;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            ClassEnrollModel classEnrollModel;
            if (sqlDataReader.Read())
            {
                classEnrollModel = AssignClassEnroll(sqlDataReader, clientId, ipAddress, execUniqueId, loggedInUserId);
            }
            else
            {
                classEnrollModel = new ClassEnrollModel();
            }
            sqlDataReader.Close();
            return classEnrollModel;
        }
        public static ClassEnrollModel GetClassEnrollPersonClassEnrollFees(long personId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            SqlDataReader sqlDataReader = null;
            try
            {
                ClassEnrollModel classEnrollModel = null;
                SqlCommand sqlCommand = null;// BuildSqlCommandClassEnrollPersonClassEnrollFees(sqlConnection, execUniqueId);
                sqlCommand.Parameters["@PersonId"].Value = personId;
                sqlDataReader = sqlCommand.ExecuteReader();
                bool sqlDataReaderRead = sqlDataReader.Read();
                while (sqlDataReaderRead)
                {
                    classEnrollModel = new ClassEnrollModel
                    {
                        ClassEnrollId = long.Parse(sqlDataReader["ClassEnrollId"].ToString()),
                        ClassScheduleId = long.Parse(sqlDataReader["ClassScheduleId"].ToString()),
                        ClassScheduleModel = new ClassScheduleModel
                        {
                            ClassScheduleDesc = sqlDataReader["ClassScheduleDesc"].ToString(),
                            ClassScheduleId = long.Parse(sqlDataReader["ClassScheduleId"].ToString()),
                            ClassSessionId = long.Parse(sqlDataReader["ClassSessionId"].ToString()),
                            GraduationDate = DateTime.Parse(sqlDataReader["GraduationDate"].ToString()).ToString("yyyy-MM-dd"),
                            RegisterDate = DateTime.Parse(sqlDataReader["RegisterDate"].ToString()).ToString("yyyy-MM-dd"),
                            StartDate = DateTime.Parse(sqlDataReader["StartDate"].ToString()).ToString("yyyy-MM-dd"),
                        },
                        PersonId = personId,
                        PersonModel = new PersonModel
                        {
                            FirstName = sqlDataReader["FirstName"].ToString(),
                            LastName = sqlDataReader["LastName"].ToString(),
                        },
                    };
                    classEnrollModel.ClassEnrollFeesModels = new List<ClassEnrollFeesModel>();
                    while (sqlDataReaderRead && classEnrollModel.ClassEnrollId == long.Parse(sqlDataReader["ClassEnrollId"].ToString()))
                    {
                        classEnrollModel.ClassEnrollFeesModels.Add
                        (
                            new ClassEnrollFeesModel
                            {
                                ClassEnrollId = long.Parse(sqlDataReader["ClassEnrollId"].ToString()),
                                ClassEnrollFeesDesc = sqlDataReader["ClassEnrollFeesDesc"].ToString(),
                                //DiscountAmount = sqlDataReader["DiscountAmount"].ToString() == "" ? 0 : decimal.Parse(sqlDataReader["DiscountAmount"].ToString()),
                                DueDate = sqlDataReader["DueDate"].ToString(),
                                //FeesAmount = sqlDataReader["FeesAmount"].ToString() == "" ? 0 : decimal.Parse(sqlDataReader["FeesAmount"].ToString()),
                            }
                        );
                        sqlDataReaderRead = sqlDataReader.Read();
                    }
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00098000 :: Exit");
                return classEnrollModel;
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
    }
}
