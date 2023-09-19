using ArchitectureLibraryUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryTemplate
{
    public class TemplateBL
    {
        public string GetTemplateString(string templateName)
        {
            string templatesDirectoryName = Utilities.GetApplicationValue("TemplatesDirectoryName");
            StreamReader streamReader = new StreamReader(templatesDirectoryName + templateName + ".html");
            string templateWithData = streamReader.ReadToEnd();
            streamReader.Close();
            return templateWithData;
        }
        public string PopulateStringTemplate(string templateString, Dictionary<string,string> keywordValues)
        {
            string templateWithData = templateString;
            foreach (var keywordValue in keywordValues)
            {
                templateWithData = templateWithData.Replace(keywordValue.Key, keywordValue.Value);
            }
            return templateWithData;
        }
        public string PopulateFileTemplate(string templateName, string dataFullFileName, Dictionary<string, string> keywordValues)
        {
            StreamReader streamReader = new StreamReader(dataFullFileName);
            string templateWithData = streamReader.ReadToEnd();
            streamReader.Close();
            foreach (var keywordValue in keywordValues)
            {
                templateWithData = templateWithData.Replace(keywordValue.Key, keywordValue.Value);
            }
            return templateWithData;
        }
        public Dictionary<string, string> PopulateKeyWords(string templateName, Dictionary<string, string> keywordValues)
        {
            /* This code works and WIP
            //SqlConnection sqlConnection = new SqlConnection(Utilities.GetDatabaseConnectionString("DatabaseConnectionString"));
            SqlConnection sqlConnection = new SqlConnection("DATA SOURCE=.; INTEGRATED SECURITY=SSPI; INITIAL CATALOG=TestDB");
            sqlConnection.Open();
            TemplateModel templateModel = LoadTemplateInfo(templateName, sqlConnection);
            Dictionary<string, string> templateWithData1 = new Dictionary<string, string>();
            switch (templateModel.TemplateTypeId)
            {
                case TemplateTypeEnum.EmailTemplate:
                    templateWithData1["@@##EmailSubjectFile##@@"] = templateModel.TemplateKeywordModelsForTemplate.Find(x => x.KeywordModel.KeywordName == "@@##EmailSubjectFile##@@").TemplateDataValue;
                    templateWithData1["@@##EmailBodyFile##@@"] = templateModel.TemplateKeywordModelsForTemplate.Find(x => x.KeywordModel.KeywordName == "@@##EmailBodyFile##@@").TemplateDataValue;
                    templateWithData1["@@##FromEmailAddress##@@"] = templateModel.TemplateKeywordModelsForTemplate.Find(x => x.KeywordModel.KeywordName == "@@##FromEmailAddress##@@").TemplateDataValue;
                    templateWithData1["@@##FromEmailAddressDisplayName##@@"] = templateModel.TemplateKeywordModelsForTemplate.Find(x => x.KeywordModel.KeywordName == "@@##FromEmailAddressDisplayName##@@").TemplateDataValue;
                    foreach (var x in templateModel.TemplateKeywordModelsForTemplate)
                    {
                    }
                    break;
                default:
                    break;
            }
            sqlConnection.Close();
            */
            //string templatesDirectoryName;
            //SqlConnection sqlConnection;

            //templatesDirectoryName = Utilities.GetApplicationValue("TemplatesDirectoryName");
            //sqlConnection = new SqlConnection(Utilities.GetDatabaseConnectionString("DatabaseConnectionString"));
            //sqlConnection.Open();
            //TemplateModel templateModel = PopulateKeyWords(templateName, keywordValues, templatesDirectoryName, sqlConnection);
            //Dictionary<string, string> templateWithData = new Dictionary<string, string>();
            //foreach (var x in templateModel.TemplateKVPModels)
            //{
            //    templateWithData[x.KVPKey] = x.KVPValue;
            //    switch (x.KVPKey)
            //    {
            //        case "EMAIL_SUBJECT_FILE":
            //            break;
            //        default:
            //            break;
            //    }
            //}
            //sqlConnection.Close();
            //return templateWithData;
            string templatesDirectoryName = Utilities.GetApplicationValue("TemplatesDirectoryName");
            Dictionary<string, string> templateWithData = new Dictionary<string, string>();
            StreamReader streamReader;
            streamReader = new StreamReader(templatesDirectoryName + templateName + "Subject.txt");
            templateWithData["EMAIL_SUBJECT"] = streamReader.ReadToEnd() + "(" + DateTime.Now.ToString("MMM dd yyyy hh:mm:ss tt - ddd") + ")";
            streamReader.Close();
            streamReader = new StreamReader(templatesDirectoryName + templateName + "Body.html");
            templateWithData["EMAIL_BODY"] = streamReader.ReadToEnd();
            streamReader.Close();
            foreach (var keywordValue in keywordValues)
            {
                templateWithData["EMAIL_BODY"] = templateWithData["EMAIL_BODY"].Replace(keywordValue.Key, keywordValue.Value);
            }
           // templateWithData["FROM_EMAIL_ADDRESS_DISPLAY_NAME"] = clientModel.ApplicationDefaultModels.FirstOrDefault(x => x.KVPKey == "FromEmailAddressDisplayName").KVPValue;//Utilities.GetApplicationValue("FromEmailAddressDisplayName");
           // templateWithData["FROM_EMAIL_ADDRESS"] = clientModel.ApplicationDefaultModels.FirstOrDefault(x => x.KVPKey == "FromEmailAddress").KVPValue;//Utilities.GetApplicationValue("FromEmailAddress");
            return templateWithData;
        }
        public TemplateModel PopulateKeyWords(string templateName, Dictionary<string, string> keywordValues, string templatesDirectoryName, SqlConnection sqlConnection)
        {
            //1. Get the template and keywords from the database
            //2. Load the HTML from folder
            //3. Replace keywords with the passed values
            TemplateModel templateModel = null;
            SqlCommand sqlCommand = BuildSqlCommandTemplateInfo(sqlConnection);
            sqlCommand.Parameters["@TemplateName"].Value = templateName;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            bool sqlDataReaderRead = sqlDataReader.Read();
            while (sqlDataReaderRead)
            {
                templateModel = new TemplateModel
                {
                    TemplateId = long.Parse(sqlDataReader["TemplateId"].ToString()),
                    TemplateName = sqlDataReader["TemplateName"].ToString(),
                    TemplateTypeId = (TemplateTypeEnum)int.Parse(sqlDataReader["TemplateTypeId"].ToString()),
                    TemplateKeywordModelsForTemplate = new List<TemplateKeywordModel>(),
                };
                while (sqlDataReaderRead && templateModel.TemplateId == long.Parse(sqlDataReader["TemplateId"].ToString()))
                {
                    templateModel.TemplateKeywordModelsForTemplate.Add
                    (
                        new TemplateKeywordModel
                        {
                            TemplateKeywordId = long.Parse(sqlDataReader["TemplateKeywordId"].ToString()),
                            KeywordId = long.Parse(sqlDataReader["KeywordId"].ToString()),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                            KeywordTypeId = (KeywordTypeEnum)int.Parse(sqlDataReader["KeywordTypeId"].ToString()),
                            TemplateId = long.Parse(sqlDataReader["TemplateId"].ToString()),
                        }
                    );
                    sqlDataReaderRead = sqlDataReader.Read();
                }
            }
            sqlDataReader.Close();
            return templateModel;
        }
        private TemplateModel LoadTemplateInfo(string templateName, SqlConnection sqlConnection)
        {
            TemplateModel templateModel = null;
            SqlCommand sqlCommand = BuildSqlCommandTemplateInfo(sqlConnection);
            sqlCommand.Parameters["@TemplateName"].Value = templateName;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            bool sqlDataReaderRead = sqlDataReader.Read();
            while (sqlDataReaderRead)
            {
                templateModel = new TemplateModel
                {
                    TemplateId = long.Parse(sqlDataReader["TemplateId"].ToString()),
                    TemplateName = sqlDataReader["TemplateName"].ToString(),
                    TemplateTypeId = (TemplateTypeEnum)int.Parse(sqlDataReader["TemplateTypeId"].ToString()),
                    TemplateKeywordModelsForTemplate = new List<TemplateKeywordModel>(),
                    TemplateKeywordModelsForKeyword = new List<TemplateKeywordModel>(),
                };
                while (sqlDataReaderRead && templateModel.TemplateId == long.Parse(sqlDataReader["TemplateId"].ToString()))
                {
                    switch ((KeywordTypeEnum)int.Parse(sqlDataReader["KeywordTypeId"].ToString()))
                    {
                        case KeywordTypeEnum.TemplateType:
                            templateModel.TemplateKeywordModelsForTemplate.Add
                            (
                                new TemplateKeywordModel
                                {
                                    TemplateKeywordId = long.Parse(sqlDataReader["TemplateKeywordId"].ToString()),
                                    KeywordId = long.Parse(sqlDataReader["KeywordId"].ToString()),
                                    KeywordTypeId = (KeywordTypeEnum)int.Parse(sqlDataReader["KeywordTypeId"].ToString()),
                                    SeqNum = float.Parse(sqlDataReader["TemplateKeywordSeqNum"].ToString()),
                                    TemplateId = long.Parse(sqlDataReader["TemplateId"].ToString()),
                                    KeywordModel = new KeywordModel
                                    {
                                        KeywordId = long.Parse(sqlDataReader["KeywordId"].ToString()),
                                        KeywordName = sqlDataReader["KeywordName"].ToString(),
                                    },
                                }
                            );
                            break;
                        case KeywordTypeEnum.KeywordType:
                            templateModel.TemplateKeywordModelsForKeyword.Add
                            (
                                new TemplateKeywordModel
                                {
                                    TemplateKeywordId = long.Parse(sqlDataReader["TemplateKeywordId"].ToString()),
                                    KeywordId = long.Parse(sqlDataReader["KeywordId"].ToString()),
                                    SeqNum = float.Parse(sqlDataReader["TemplateKeywordSeqNum"].ToString()),
                                    KeywordTypeId = (KeywordTypeEnum)int.Parse(sqlDataReader["KeywordTypeId"].ToString()),
                                    TemplateDataValue = sqlDataReader["TemplateDataValue"].ToString(),
                                    TemplateId = long.Parse(sqlDataReader["TemplateId"].ToString()),
                                    KeywordModel = new KeywordModel
                                    {
                                        KeywordId = long.Parse(sqlDataReader["KeywordId"].ToString()),
                                        KeywordName = sqlDataReader["KeywordName"].ToString(),
                                    },
                                }
                            );
                            break;
                        default:
                            break;
                    }
                    sqlDataReaderRead = sqlDataReader.Read();
                }
            }
            sqlDataReader.Close();
            return templateModel;
        }
        private SqlCommand BuildSqlCommandTemplateInfo(SqlConnection sqlConnection)
        {
            string sqlStmt = "";
            sqlStmt += "" + Environment.NewLine;
            sqlStmt += "        SELECT " + Environment.NewLine;
            sqlStmt += "               Template.TemplateId" + Environment.NewLine;
            sqlStmt += "              ,Template.TemplateName" + Environment.NewLine;
            sqlStmt += "              ,Template.TemplateTypeId" + Environment.NewLine;
            sqlStmt += "              ,TemplateKeyword.TemplateKeywordId" + Environment.NewLine;
            sqlStmt += "              ,TemplateKeyword.KeywordTypeId" + Environment.NewLine;
            sqlStmt += "              ,TemplateKeyword.SeqNum AS TemplateKeywordSeqNum" + Environment.NewLine;
            sqlStmt += "              ,TemplateKeyword.TemplateDataValue" + Environment.NewLine;
            sqlStmt += "              ,Keyword.KeywordId" + Environment.NewLine;
            sqlStmt += "              ,Keyword.KeywordName" + Environment.NewLine;
            sqlStmt += "          FROM ArchLib.Template" + Environment.NewLine;
            sqlStmt += "    INNER JOIN ArchLib.TemplateKeyword" + Environment.NewLine;
            sqlStmt += "            ON Template.TemplateId = TemplateKeyword.TemplateId" + Environment.NewLine;
            sqlStmt += "    INNER JOIN ArchLib.Keyword" + Environment.NewLine;
            sqlStmt += "            ON TemplateKeyword.KeywordId = Keyword.KeywordId" + Environment.NewLine;
            sqlStmt += "         WHERE Template.TemplateName = @TemplateName" + Environment.NewLine;
            sqlStmt += "      ORDER BY TemplateKeyword.SeqNum" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@TemplateName", SqlDbType.NVarChar, 100);
            return sqlCommand;
        }
    }
}
