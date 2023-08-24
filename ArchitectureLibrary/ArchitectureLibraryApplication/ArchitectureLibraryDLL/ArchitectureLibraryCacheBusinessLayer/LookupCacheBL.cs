using ArchitectureLibraryUtility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchitectureLibraryException;
using System.Reflection;
using System.Web.Mvc;
using ArchitectureLibraryModels;
using ArchitectureLibraryEnumerations;

namespace ArchitectureLibraryCacheBusinessLayer
{
    public class LookupCacheBL
    {
        public void Initialize(out List<CodeTypeModel> codeTypeModels, out List<CodeDataModel> codeDataModels, out Dictionary<string, Dictionary<string, List<SelectListItem>>> codeTypeSelectListItems, out Dictionary<string, Dictionary<string, string>> codeDataOptionTags, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            string databaseConnectionString = Utilities.GetDatabaseConnectionString("DatabaseConnectionString");
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            sqlConnection.Open();
            codeTypeModels = LoadCodeTypeModels(sqlConnection, ipAddress, execUniqueId, loggedInUserId);
            codeDataModels = LoadCodeDataModels(codeTypeModels, sqlConnection, ipAddress, execUniqueId, loggedInUserId);
            sqlConnection.Close();
            BuildCodeDataModels(codeTypeModels, codeDataModels, ipAddress, execUniqueId, loggedInUserId);
            codeTypeSelectListItems = BuildCodeTypeSelectListItems(codeTypeModels, ipAddress, execUniqueId, loggedInUserId);
            codeDataOptionTags = BuildCodeDataOptionTags(codeTypeModels, ipAddress, execUniqueId, loggedInUserId);
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return;
        }
        private List<CodeDataModel> LoadCodeDataModels(List<CodeTypeModel> codeTypeModels, SqlConnection sqlConnection, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<CodeDataModel> codeDataModels = new List<CodeDataModel>();
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Lookup.CodeData ORDER BY CodeDataId", sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                codeDataModels.Add
                (
                    new CodeDataModel
                    {
                        AddDateTime = sqlDataReader["AddDateTime"].ToString(),
                        AddUserId = sqlDataReader["AddUserId"].ToString(),
                        AddUserName = sqlDataReader["AddUserName"].ToString(),
                        CodeDataDesc0 = sqlDataReader["CodeDataDesc0"].ToString(),
                        CodeDataDesc1 = sqlDataReader["CodeDataDesc1"].ToString(),
                        CodeDataDesc2 = sqlDataReader["CodeDataDesc2"].ToString(),
                        CodeDataId = long.Parse(sqlDataReader["CodeDataId"].ToString()),
                        CodeDataNameDesc = sqlDataReader["CodeDataNameDesc"].ToString(),
                        CodeDataNameId = long.Parse(sqlDataReader["CodeDataNameId"].ToString()),
                        CodeTypeModel = codeTypeModels.Find(x => x.CodeTypeId == long.Parse(sqlDataReader["CodeTypeId"].ToString())),
                        CodeTypeId = long.Parse(sqlDataReader["CodeTypeId"].ToString()),
                        CodeTypeNameId = (CodeTypeEnum)long.Parse(sqlDataReader["CodeTypeNameId"].ToString()),
                        UpdDateTime = sqlDataReader["UpdDateTime"].ToString(),
                        UpdUserId = sqlDataReader["UpdUserId"].ToString(),
                        UpdUserName = sqlDataReader["UpdUserName"].ToString(),
                    }
                );
            }
            sqlDataReader.Close();
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return codeDataModels;
        }
        private List<CodeTypeModel> LoadCodeTypeModels(SqlConnection sqlConnection, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            List<CodeTypeModel> codeTypeModels = new List<CodeTypeModel>();
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Lookup.CodeType ORDER BY CodeTypeId", sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                codeTypeModels.Add
                (
                    new CodeTypeModel
                    {
                        AddDateTime = sqlDataReader["AddDateTime"].ToString(),
                        AddUserId = sqlDataReader["AddUserId"].ToString(),
                        AddUserName = sqlDataReader["AddUserName"].ToString(),
                        CodeDataModelsCodeDataId = null,
                        CodeDataModelsCodeDataNameDesc = null,
                        CodeDataModelsCodeDataNameId = null,
                        CodeTypeDesc = sqlDataReader["CodeTypeDesc"].ToString(),
                        CodeTypeNameDesc = sqlDataReader["CodeTypeNameDesc"].ToString(),
                        CodeTypeId = long.Parse(sqlDataReader["CodeTypeId"].ToString()),
                        CodeTypeNameId = (CodeTypeEnum)long.Parse(sqlDataReader["CodeTypeNameId"].ToString()),
                        UpdDateTime = sqlDataReader["UpdDateTime"].ToString(),
                        UpdUserId = sqlDataReader["UpdUserId"].ToString(),
                        UpdUserName = sqlDataReader["UpdUserName"].ToString(),
                    }
                );
            }
            sqlDataReader.Close();
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            return codeTypeModels;
        }
        private void BuildCodeDataModels(List<CodeTypeModel> codeTypeModels, List<CodeDataModel> codeDataModels, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            foreach (var codeTypeModel in codeTypeModels)
            {
                codeTypeModel.CodeDataModelsCodeDataId = codeDataModels.FindAll(x => x.CodeTypeId == codeTypeModel.CodeTypeId).OrderBy(x => x.CodeDataId).ToList();
                codeTypeModel.CodeDataModelsCodeDataNameId = codeDataModels.FindAll(x => x.CodeTypeId == codeTypeModel.CodeTypeId).OrderBy(x => x.CodeDataNameId).ToList();
                codeTypeModel.CodeDataModelsCodeDataNameDesc = codeDataModels.FindAll(x => x.CodeTypeId == codeTypeModel.CodeTypeId).OrderBy(x => x.CodeDataNameDesc).ToList();
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        }
        private Dictionary<string, Dictionary<string, List<SelectListItem>>> BuildCodeTypeSelectListItems(List<CodeTypeModel> codeTypeModels, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            Dictionary<string, Dictionary<string, List<SelectListItem>>> codeTypeSelectListItems = new Dictionary<string, Dictionary<string, List<SelectListItem>>>();
            Dictionary<string, List<SelectListItem>> x;
            List<SelectListItem> y;
            foreach (var codeTypeModel in codeTypeModels)
            {
                x = new Dictionary<string, List<SelectListItem>>();
                codeTypeSelectListItems[codeTypeModel.CodeTypeNameDesc] = x;
                y = new List<SelectListItem>();
                x["CodeDataId"] = y;
                foreach (var z in codeTypeModel.CodeDataModelsCodeDataId)
                {
                    y.Add(new SelectListItem { Text = z.CodeDataDesc0, Value = z.CodeDataNameId.ToString() });
                }
                y = new List<SelectListItem>();
                x["CodeDataNameId"] = y;
                foreach (var z in codeTypeModel.CodeDataModelsCodeDataNameId)
                {
                    y.Add(new SelectListItem { Text = z.CodeDataDesc0, Value = z.CodeDataNameId.ToString() });
                }
                y = new List<SelectListItem>();
                x["CodeDataNameDesc"] = y;
                foreach (var z in codeTypeModel.CodeDataModelsCodeDataNameDesc)
                {
                    y.Add(new SelectListItem { Text = z.CodeDataDesc0, Value = z.CodeDataNameId.ToString() });
                }
            }
            x = new Dictionary<string, List<SelectListItem>>();
            codeTypeSelectListItems["GregorianMonths"] = x;
            y = new List<SelectListItem>();
            x["Type1"] = y;
            DateTime dateTime;
            int year = int.Parse(DateTime.Now.ToString("yyyy"));
            for (int i = 1; i <= 12; i++)
            {
                dateTime = DateTime.Parse(year + "-" + i + "-1");
                y.Add(new SelectListItem { Text = dateTime.ToString("MMM"), Value = dateTime.ToString("MM") });
            }
            return codeTypeSelectListItems;
        }
        private Dictionary<string, Dictionary<string, string>> BuildCodeDataOptionTags(List<CodeTypeModel> codeTypeModels, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string optionTags;
            Dictionary<string, Dictionary<string, string>> codeTypeOptionTags = new Dictionary<string, Dictionary<string, string>>();
            Dictionary<string, string> codeDataOptionTags;
            foreach (var codeTypeModel in codeTypeModels)
            {
                codeDataOptionTags = new Dictionary<string, string>();
                codeTypeOptionTags[codeTypeModel.CodeTypeNameDesc] = codeDataOptionTags;

                optionTags = "";
                optionTags += "<option value=\"\">---</option>" + Environment.NewLine;
                foreach (var codeDataModel in codeTypeModel.CodeDataModelsCodeDataId)
                {
                    optionTags += "<option value=\"" + codeDataModel.CodeDataNameId + "\">" + codeDataModel.CodeDataDesc0 + "</option>" + Environment.NewLine;
                }
                codeDataOptionTags["CodeDataId"] = optionTags;

                optionTags = "";
                optionTags += "<option value=\"\">---</option>" + Environment.NewLine;
                foreach (var codeDataModel in codeTypeModel.CodeDataModelsCodeDataNameId)
                {
                    optionTags += "<option value=\"" + codeDataModel.CodeDataNameId + "\">" + codeDataModel.CodeDataDesc0 + "</option>" + Environment.NewLine;
                }
                codeDataOptionTags["CodeDataNameId"] = optionTags;

                optionTags = "";
                optionTags += "<option value=\"\">---</option>" + Environment.NewLine;
                foreach (var codeDataModel in codeTypeModel.CodeDataModelsCodeDataNameId)
                {
                    optionTags += "<option value=\"" + codeDataModel.CodeDataNameId + "\">" + codeDataModel.CodeDataDesc0 + "</option>" + Environment.NewLine;
                }
                codeDataOptionTags["CodeDataNameDesc"] = optionTags;
            }
            return codeTypeOptionTags;
        }
    }
}
