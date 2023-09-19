using ArchitectureLibraryException;
using ArchitectureLibraryUtility;
using SVCCTempleCacheData;
using SVCCTempleModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SVCCTempleBusinessLayer
{
    public partial class SVCCTempleBL
    {
        public IndexModel BuildIndexModel(string locationNameDesc, string locationNameDesc1, string importantIdsList, int monthCount, string startDate, string finishDate, string execUniqueId)
        {
            string databaseConnectionString = Utilities.GetDatabaseConnectionString("DatabaseConnectionString");
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            sqlConnection.Open();
            IndexModel indexModel = new IndexModel
            {
                ImportantDatesModels = GetImportantDates2(locationNameDesc, monthCount, importantIdsList, sqlConnection, execUniqueId),
                TempleInfoDatesModel = new TempleInfoDatesModel
                {
                    StartDate = startDate,
                    FinishDate = finishDate,
                    TempleInfoDateModels = new List<TempleInfoDateModel>(),
                }
            };
            SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM TempleInfo WHERE StartDate BETWEEN '{startDate}' AND '{finishDate}' AND LocationNameDesc = '{locationNameDesc}' ORDER BY StartDate, StartTime, SeqNum", sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            TempleInfoDateModel templeInfoDateModel;
            bool sqlDataReaderRead = sqlDataReader.Read();
            while (sqlDataReaderRead)
            {
                indexModel.TempleInfoDatesModel.TempleInfoDateModels.Add
                (
                    templeInfoDateModel = new TempleInfoDateModel
                    {
                        TempleInfoDate = sqlDataReader["StartDate"].ToString(),
                        TempleInfoModels = new List<TempleInfoModel>(),
                    }
                );
                while (sqlDataReaderRead && templeInfoDateModel.TempleInfoDate == sqlDataReader["StartDate"].ToString())
                {
                    templeInfoDateModel.TempleInfoModels.Add
                    (
                        new TempleInfoModel
                        {
                            TempleInfoId = long.Parse(sqlDataReader["TempleInfoId"].ToString()),
                            LocationNameDesc = sqlDataReader["LocationNameDesc"].ToString(),
                            ImportantDatesId = long.Parse(sqlDataReader["ImportantDatesId"].ToString()),
                            InfoType = sqlDataReader["InfoType"].ToString(),
                            SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                            StartDate = sqlDataReader["StartDate"].ToString(),
                            StartTime = sqlDataReader["StartTime"].ToString(),
                            FinishDate = sqlDataReader["FinishDate"].ToString(),
                            FinishTime = sqlDataReader["FinishTime"].ToString(),
                            InfoText1 = sqlDataReader["InfoText1"].ToString(),
                            InfoText2 = sqlDataReader["InfoText2"].ToString(),
                            InfoText3 = sqlDataReader["InfoText3"].ToString(),
                            HtmlFileName1 = sqlDataReader["HtmlFileName1"].ToString(),
                            ImageName1 = sqlDataReader["ImageName1"].ToString(),
                            SponsorshipGroupId = sqlDataReader["SponsorshipGroupId"].ToString(),
                        }
                    );
                    sqlDataReaderRead = sqlDataReader.Read();
                }
            }
            sqlDataReader.Close();
            sqlCommand = new SqlCommand($"SELECT * FROM RiseSet WHERE GregorianDate BETWEEN '{startDate}' AND '{finishDate}' AND LocationNameDesc = '{locationNameDesc}' ORDER BY GregorianDate", sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                templeInfoDateModel = indexModel.TempleInfoDatesModel.TempleInfoDateModels.First(x => x.TempleInfoDate == sqlDataReader["GregorianDate"].ToString());
                templeInfoDateModel.TempleInfoModels.Add
                (
                    new TempleInfoModel
                    {
                        TempleInfoId = -1,
                        LocationNameDesc = sqlDataReader["LocationNameDesc"].ToString(),
                        ImportantDatesId = 99000,
                        InfoType = "HOMEPAGE",
                        SeqNum = 99000,
                        StartDate = null,
                        StartTime = sqlDataReader["SunRise"].ToString(),
                        FinishDate = null,
                        FinishTime = sqlDataReader["SunSet"].ToString(),
                        InfoText1 = "Sunrise / Set",
                        InfoText2 = null,
                        InfoText3 = null,
                        HtmlFileName1 = null,
                        ImageName1 = null,
                        SponsorshipGroupId = null,
                    }
                );
                templeInfoDateModel.TempleInfoModels.Add
                (
                    new TempleInfoModel
                    {
                        TempleInfoId = -1,
                        LocationNameDesc = sqlDataReader["LocationNameDesc"].ToString(),
                        ImportantDatesId = 99001,
                        InfoType = "HOMEPAGE",
                        SeqNum = 99001,
                        StartDate = null,
                        StartTime = sqlDataReader["RKStart"].ToString(),
                        FinishDate = null,
                        FinishTime = sqlDataReader["RKFinish"].ToString(),
                        InfoText1 = "Rahu",
                        InfoText2 = null,
                        InfoText3 = null,
                        HtmlFileName1 = null,
                        ImageName1 = null,
                        SponsorshipGroupId = null,
                    }
                );
                templeInfoDateModel.TempleInfoModels.Add
                (
                    new TempleInfoModel
                    {
                        TempleInfoId = -1,
                        LocationNameDesc = sqlDataReader["LocationNameDesc"].ToString(),
                        ImportantDatesId = 99002,
                        InfoType = "HOMEPAGE",
                        SeqNum = 99002,
                        StartDate = null,
                        StartTime = sqlDataReader["YGStart"].ToString(),
                        FinishDate = null,
                        FinishTime = sqlDataReader["YGFinish"].ToString(),
                        InfoText1 = "Yama",
                        InfoText2 = null,
                        InfoText3 = null,
                        HtmlFileName1 = null,
                        ImageName1 = null,
                        SponsorshipGroupId = null,
                    }
                );
            }
            sqlDataReader.Close();
            sqlConnection.Close();
            return indexModel;
        }
        public List<ImportantDatesModel> GetImportantDates2(string locationNameDesc, int monthCount, string importantIdsList, SqlConnection sqlConnection, string execUniqueId)
        {
            string fromDate, toDate, sqlStmt;
            fromDate = DateTime.Now.ToString("yyyy-MM-01");
            toDate = DateTime.Parse(fromDate).AddMonths(monthCount).AddDays(-1).ToString("yyyy-MM-dd");
            sqlStmt = "";
            sqlStmt += "        SELECT MonthList_ImportantDates.*" + Environment.NewLine;
            sqlStmt += "              ,ImportantDatesDate.EventDate" + Environment.NewLine;
            sqlStmt += "              ,ImportantDatesDate.EventTime" + Environment.NewLine;
            sqlStmt += "              ,ImportantDatesDate.EventText1" + Environment.NewLine;
            sqlStmt += "              ,ImportantDatesDate.EventText2" + Environment.NewLine;
            sqlStmt += "              ,ImportantDatesDate.EventText3" + Environment.NewLine;
            sqlStmt += "          FROM " + Environment.NewLine;
            sqlStmt += "              (" + Environment.NewLine;
            sqlStmt += "        SELECT" + Environment.NewLine;
            sqlStmt += "               *" + Environment.NewLine;
            sqlStmt += "          FROM MonthList" + Environment.NewLine;
            sqlStmt += "    CROSS JOIN ImportantDates" + Environment.NewLine;
            sqlStmt += "         WHERE MonthList.BeginDate BETWEEN '" + fromDate + "' AND '" + toDate + "'" + Environment.NewLine;
            sqlStmt += "           AND ImportantDates.LocationNameDesc = '" + locationNameDesc + "'" + Environment.NewLine;
            sqlStmt += "           AND ImportantDates.ImportantDatesId IN(" + importantIdsList + ")" + Environment.NewLine;
            sqlStmt += "              ) MonthList_ImportantDates" + Environment.NewLine;
            sqlStmt += "     LEFT JOIN ImportantDatesDate" + Environment.NewLine;
            sqlStmt += "            ON MonthList_ImportantDates.ImportantDatesId = ImportantDatesDate.ImportantDatesId" + Environment.NewLine;
            sqlStmt += "           AND ImportantDatesDate.EventDate BETWEEN MonthList_ImportantDates.BeginDate AND MonthList_ImportantDates.EndDate" + Environment.NewLine;
            sqlStmt += "         WHERE MonthList_ImportantDates.LocationNameDesc = '" + locationNameDesc + "'" + Environment.NewLine;
            sqlStmt += "      ORDER BY MonthList_ImportantDates.BeginDate" + Environment.NewLine;
            sqlStmt += "              ,MonthList_ImportantDates.SeqNum" + Environment.NewLine;
            bool sqlConnectionClose = false;
            if (sqlConnection == null)
            {
                string databaseConnectionString = Utilities.GetDatabaseConnectionString("DatabaseConnectionString");
                sqlConnection = new SqlConnection(databaseConnectionString);
                sqlConnection.Open();
                sqlConnectionClose = true;
            }
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            ImportantDatesModel importantDatesModels;
            List<ImportantDatesModel> importantDatesModelsList = new List<ImportantDatesModel>();
            importantDatesModels = new ImportantDatesModel
            {
                ImportantDatesId = "",
                BeginDate = fromDate,
                EndDate = toDate,
            };
            importantDatesModelsList.Add(importantDatesModels);
            while (sqlDataReader.Read())
            {
                importantDatesModels = new ImportantDatesModel
                {
                    MonthListId = sqlDataReader["MonthListId"].ToString(),
                    BeginDate = sqlDataReader["BeginDate"].ToString(),
                    EndDate = sqlDataReader["EndDate"].ToString(),
                    ImportantDatesId = sqlDataReader["ImportantDatesId"].ToString(),
                    LocationNameDesc = sqlDataReader["LocationNameDesc"].ToString(),
                    SeqNum = sqlDataReader["SeqNum"].ToString(),
                    EventName1 = sqlDataReader["EventName1"].ToString(),
                    EventName2 = sqlDataReader["EventName2"].ToString(),
                    EventDesc1 = sqlDataReader["EventDesc1"].ToString(),
                    EventDesc2 = sqlDataReader["EventDesc2"].ToString(),
                    StartTime = sqlDataReader["StartTime"].ToString(),
                    FinishTime = sqlDataReader["FinishTime"].ToString(),
                    ImageName1 = sqlDataReader["ImageName1"].ToString(),
                    ImageName2 = sqlDataReader["ImageName2"].ToString(),
                    EventTypeNameDesc = sqlDataReader["EventTypeNameDesc"].ToString(),
                    EventDate = sqlDataReader["EventDate"].ToString(),
                    EventTime = sqlDataReader["EventTime"].ToString().Trim() == "" ? "" : DateTime.Parse("1900-01-01 " + sqlDataReader["EventTime"].ToString()).ToString("hh:mm tt"),
                    EventText1 = sqlDataReader["EventText1"].ToString(),
                    EventText2 = sqlDataReader["EventText2"].ToString(),
                    EventText3 = sqlDataReader["EventText3"].ToString(),
                    SponsorshipGroupId = sqlDataReader["SponsorshipGroupId"].ToString(),
                };
                importantDatesModelsList.Add(importantDatesModels);
            }
            sqlDataReader.Close();
            if (sqlConnectionClose)
            {
                sqlConnection.Close();
            }
            return importantDatesModelsList;
        }
    }
}
