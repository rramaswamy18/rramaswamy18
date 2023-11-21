using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArchitectureLibraryCacheBusinessLayer
{
    public class DemogInfoCacheBL
    {
        public void Initialize(out List<DemogInfoCountryModel> demogInfoCountryModels, out List<DemogInfoSubDivisionModel> demogInfoSubDivisionModels, out List<SelectListItem> demogInfoCountrySelectListItems, out List<SelectListItem> demogInfoCountrySelectListItemsAbbrev, out List<SelectListItem> demogInfoCountrySelectListItemsName, out Dictionary<long, List<SelectListItem>> demogInfoSubDivisionSelectListItems, out string demogInfoCountryOptionTags, out Dictionary<long, string> demogInfoSubDivisionOptionTags, string execUniqueId)
        {
            string databaseConnectionString = Utilities.GetDatabaseConnectionString("DatabaseConnectionString");
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            sqlConnection.Open();
            demogInfoCountryModels = LoadDemogInfoCountryModels(sqlConnection, execUniqueId);
            demogInfoSubDivisionModels = LoadDemogInfoSubDivisionModels(demogInfoCountryModels, sqlConnection, execUniqueId);
            sqlConnection.Close();
            BuildDemogInfoSubDivisionModel(demogInfoCountryModels, demogInfoSubDivisionModels, execUniqueId);
            BuildDemogInfoSelectListItems(demogInfoCountryModels, out demogInfoCountrySelectListItems, out demogInfoCountrySelectListItemsAbbrev, out demogInfoCountrySelectListItemsName, out demogInfoSubDivisionSelectListItems, execUniqueId);
            BuildDemogInfoOptionTags(demogInfoCountryModels, out demogInfoCountryOptionTags, out demogInfoSubDivisionOptionTags, execUniqueId);
            return;
        }
        #region
        private List<DemogInfoCountryModel> LoadDemogInfoCountryModels(SqlConnection sqlConnection, string execUniqueId)
        {
            List<DemogInfoCountryModel> demogInfoCountryModels = new List<DemogInfoCountryModel>();
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM ArchLib.DemogInfoCountry ORDER BY DemogInfoCountryId", sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                demogInfoCountryModels.Add
                (
                    new DemogInfoCountryModel
                    {
                        AddDateTime = sqlDataReader["AddDateTime"].ToString(),
                        AddUserId = sqlDataReader["AddUserId"].ToString(),
                        AddUserName = sqlDataReader["AddUserName"].ToString(),
                        Alpha2Code = sqlDataReader["Alpha2Code"].ToString(),
                        Alpha3Code = sqlDataReader["Alpha3Code"].ToString(),
                        CountryAbbrev = sqlDataReader["CountryAbbrev"].ToString(),
                        CountryDesc = sqlDataReader["CountryDesc"].ToString(),
                        DemogInfoCountryId = long.Parse(sqlDataReader["DemogInfoCountryId"].ToString()),
                        DemogInfoSubDivisionModels = null,
                        NumericCode = sqlDataReader["NumericCode"].ToString(),
                        SubDivisionCodeHyperLink = sqlDataReader["SubDivisionCodeHyperLink"].ToString(),
                        TelephoneCode = sqlDataReader["TelephoneCode"].ToString() == "" ? (short?)null : short.Parse(sqlDataReader["TelephoneCode"].ToString()),
                        PostalCodeLabel = sqlDataReader["PostalCodeLabel"].ToString(),
                        PostalCodeRegEx = sqlDataReader["PostalCodeRegEx"].ToString(),
                        UpdDateTime = sqlDataReader["UpdDateTime"].ToString(),
                        UpdUserId = sqlDataReader["UpdUserId"].ToString(),
                        UpdUserName = sqlDataReader["UpdUserName"].ToString(),
                    }
                );
            }
            sqlDataReader.Close();
            return demogInfoCountryModels;
        }
        private List<DemogInfoSubDivisionModel> LoadDemogInfoSubDivisionModels(List<DemogInfoCountryModel> demogInfoCountryModels, SqlConnection sqlConnection, string execUniqueId)
        {
            List<DemogInfoSubDivisionModel> demogInfoSubDivisionModels = new List<DemogInfoSubDivisionModel>();
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM ArchLib.DemogInfoSubDivision ORDER BY DemogInfoCountryId", sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                demogInfoSubDivisionModels.Add
                (
                    new DemogInfoSubDivisionModel
                    {
                        AddDateTime = sqlDataReader["AddDateTime"].ToString(),
                        AddUserId = sqlDataReader["AddUserId"].ToString(),
                        AddUserName = sqlDataReader["AddUserName"].ToString(),
                        DemogInfoCountryId = long.Parse(sqlDataReader["DemogInfoCountryId"].ToString()),
                        DemogInfoCountryModel = demogInfoCountryModels.Find(x => x.DemogInfoCountryId == long.Parse(sqlDataReader["DemogInfoCountryId"].ToString())),
                        DemogInfoSubDivisionId = long.Parse(sqlDataReader["DemogInfoSubDivisionId"].ToString()),
                        ParentSubDivisionCode = sqlDataReader["ParentSubDivisionCode"].ToString(),
                        StateAbbrev = sqlDataReader["StateAbbrev"].ToString(),
                        SubDivisionCategoryNameDesc = sqlDataReader["SubDivisionCategoryNameDesc"].ToString(),
                        SubDivisionCode = sqlDataReader["SubDivisionCode"].ToString(),
                        SubDivisionDesc = sqlDataReader["SubDivisionDesc"].ToString(),
                        UpdDateTime = sqlDataReader["UpdDateTime"].ToString(),
                        UpdUserId = sqlDataReader["UpdUserId"].ToString(),
                        UpdUserName = sqlDataReader["UpdUserName"].ToString(),
                    }
                );
            }
            sqlDataReader.Close();
            return demogInfoSubDivisionModels;
        }
        private void BuildDemogInfoSubDivisionModel(List<DemogInfoCountryModel> demogInfoCountryModels, List<DemogInfoSubDivisionModel> demogInfoSubDivisionModels, string execUniqueId)
        {
            foreach (var demogInfoCountryModel in demogInfoCountryModels)
            {
                demogInfoCountryModel.DemogInfoSubDivisionModels = demogInfoSubDivisionModels.FindAll(x => x.DemogInfoCountryId == demogInfoCountryModel.DemogInfoCountryId).OrderBy(x => x.StateAbbrev).ToList();
            }
        }
        private void BuildDemogInfoSelectListItems(List<DemogInfoCountryModel> demogInfoCountryModels, out List<SelectListItem> demogInfoCountrySelectListItems, out List<SelectListItem> demogInfoCountrySelectListItemsAbbrev, out List<SelectListItem> demogInfoCountrySelectListItemsName, out Dictionary<long, List<SelectListItem>> demogInfoCountrySubDivisionSelectListItems, string execUniqueId)
        {
            demogInfoCountrySelectListItems = new List<SelectListItem>();
            demogInfoCountrySubDivisionSelectListItems = new Dictionary<long, List<SelectListItem>>();
            List<SelectListItem> demogInfoSubDivisionSelectListItems;
            foreach (var demogInfoCountryModel in demogInfoCountryModels)
            {
                demogInfoCountrySelectListItems.Add
                (
                    new SelectListItem
                    {
                        Text = demogInfoCountryModel.CountryDesc,
                        Value = demogInfoCountryModel.DemogInfoCountryId.ToString(),
                    }
                );
                demogInfoSubDivisionSelectListItems = new List<SelectListItem>();
                demogInfoCountrySubDivisionSelectListItems[demogInfoCountryModel.DemogInfoCountryId] = demogInfoSubDivisionSelectListItems;
                foreach (var demogInfoSubDivisionModel in demogInfoCountryModel.DemogInfoSubDivisionModels)
                {
                    demogInfoSubDivisionSelectListItems.Add
                    (
                        new SelectListItem
                        {
                            Text = demogInfoSubDivisionModel.SubDivisionDesc,
                            Value = demogInfoSubDivisionModel.DemogInfoSubDivisionId.ToString(),
                        }
                    );
                }
            }
            demogInfoCountrySelectListItemsAbbrev = new List<SelectListItem>();
            foreach (var demogInfoCountryModel in demogInfoCountryModels.OrderBy(x => x.CountryAbbrev))
            {
                if (demogInfoCountryModel.CountryAbbrev != "")
                {
                    demogInfoCountrySelectListItemsAbbrev.Add
                    (
                        new SelectListItem
                        {
                            Text = demogInfoCountryModel.CountryAbbrev + " " + demogInfoCountryModel.CountryDesc,
                            Value = demogInfoCountryModel.DemogInfoCountryId.ToString(),
                        }
                    );
                }
            }
            demogInfoCountrySelectListItemsName = new List<SelectListItem>();
            foreach (var demogInfoCountryModel in demogInfoCountryModels.OrderBy(x => x.CountryDesc))
            {
                if (demogInfoCountryModel.CountryAbbrev != "")
                {
                    demogInfoCountrySelectListItemsName.Add
                    (
                        new SelectListItem
                        {
                            Text = demogInfoCountryModel.CountryDesc + " " + demogInfoCountryModel.CountryAbbrev,
                            Value = demogInfoCountryModel.DemogInfoCountryId.ToString(),
                        }
                    );
                }
            }
            return;
        }
        private void BuildDemogInfoOptionTags(List<DemogInfoCountryModel> demogInfoCountryModels, out string demogInfoCountryOptionTags, out Dictionary<long, string> demogInfoCountrySubDivisionOptionTags, string execUniqueId)
        {
            string demogInfoSubDivisionOptionTags;
            demogInfoCountryOptionTags = "";
            demogInfoCountrySubDivisionOptionTags = new Dictionary<long, string>();
            demogInfoCountryOptionTags += "<option value=\"\">---</option>" + Environment.NewLine;
            foreach (var demogInfoCountryModel in demogInfoCountryModels)
            {
                demogInfoCountryOptionTags += "<option value=\"" + demogInfoCountryModel.DemogInfoCountryId + "\">" + demogInfoCountryModel.CountryAbbrev + "</option>" + Environment.NewLine;
                demogInfoSubDivisionOptionTags = "";
                demogInfoSubDivisionOptionTags += "<option value=\"\">---</option>" + Environment.NewLine;
                foreach (var demogInfoSubDivisionModel in demogInfoCountryModel.DemogInfoSubDivisionModels)
                {
                    demogInfoSubDivisionOptionTags += "<option value=\"" + demogInfoSubDivisionModel.DemogInfoSubDivisionId + "\">" + demogInfoSubDivisionModel.StateAbbrev + "</option>" + Environment.NewLine;
                }
                demogInfoCountrySubDivisionOptionTags[demogInfoCountryModel.DemogInfoCountryId] = demogInfoSubDivisionOptionTags;
            }
            return;
        }
        #endregion
    }
}
