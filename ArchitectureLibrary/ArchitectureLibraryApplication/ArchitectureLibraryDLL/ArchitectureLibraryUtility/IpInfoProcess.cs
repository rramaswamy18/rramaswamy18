using IPinfo;
using IPinfo.Models;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryUtility
{
    public static class IpInfoProcess
    {
        public static void ProcessIpInfo(string ipAddress, string ipInfoClientAccessToken)
        {
            string databaseConenctionString = Utilities.GetDatabaseConnectionString("DatabaseConnectionStringLog");
            SqlConnection sqlConnection = new SqlConnection(databaseConenctionString);
            sqlConnection.Open();
            if (!CheckIpAddressExists(sqlConnection, ipAddress))
            {
                IPinfoClient ipinfoClient = new IPinfoClient.Builder().AccessToken(ipInfoClientAccessToken).Build();
                IPResponse ipResponse = ipinfoClient.IPApi.GetDetails(ipAddress);
                IpAddressLogAdd(sqlConnection, ipAddress, ipResponse.City, ipResponse.Region, ipResponse.Postal, ipResponse.Country, ipResponse.CountryName, ipResponse.Loc, ipResponse.Continent?.Name, ipResponse.IsEU.ToString());
            }
            sqlConnection.Close();
        }
        private static bool CheckIpAddressExists(SqlConnection sqlConnection, string ipAddress)
        {
            bool returnValue;
            SqlCommand sqlCommand = new SqlCommand("SELECT 1 FROM ArchLib.IpAddressLog WHERE IpAddress = '" + ipAddress + "'", sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            returnValue = sqlDataReader.Read();
            sqlDataReader.Close();
            return returnValue;
        }
        private static void IpAddressLogAdd(SqlConnection sqlConnection, string ipAddress, string cityName, string regionName, string postalCode, string countryAbbrev, string countryName, string geoCoordinates, string continentName, string isEU)
        {
            SqlCommand sqlCommand = new SqlCommand("INSERT ArchLib.IpAddressLog(IpAddress, CityName, RegionName, PostalCode, CountryAbbrev, CountryName, GeoCoordinates, ContinentName, IsEu) SELECT @IpAddress, @CityName, @RegionName, @PostalCode, @CountryAbbrev, @CountryName, @GeoCoordinates, @ContinentName, @IsEu", sqlConnection);
            sqlCommand.Parameters.Add("@IpAddress", SqlDbType.NVarChar, 100);
            sqlCommand.Parameters.Add("@CityName", SqlDbType.NVarChar, 100);
            sqlCommand.Parameters.Add("@RegionName", SqlDbType.NVarChar, 100);
            sqlCommand.Parameters.Add("@PostalCode", SqlDbType.NVarChar, 100);
            sqlCommand.Parameters.Add("@CountryAbbrev", SqlDbType.NVarChar, 100);
            sqlCommand.Parameters.Add("@CountryName", SqlDbType.NVarChar, 100);
            sqlCommand.Parameters.Add("@GeoCoordinates", SqlDbType.NVarChar, 100);
            sqlCommand.Parameters.Add("@ContinentName", SqlDbType.NVarChar, 100);
            sqlCommand.Parameters.Add("@IsEu", SqlDbType.NVarChar, 10);

            sqlCommand.Parameters["@IpAddress"].Value = ipAddress;
            sqlCommand.Parameters["@CityName"].Value = cityName ?? (object)DBNull.Value;
            sqlCommand.Parameters["@RegionName"].Value = regionName ?? (object)DBNull.Value;
            sqlCommand.Parameters["@PostalCode"].Value = postalCode ?? (object)DBNull.Value;
            sqlCommand.Parameters["@CountryAbbrev"].Value = countryAbbrev ?? (object)DBNull.Value;
            sqlCommand.Parameters["@CountryName"].Value = countryName ?? (object)DBNull.Value;
            sqlCommand.Parameters["@GeoCoordinates"].Value = geoCoordinates ?? (object)DBNull.Value;
            sqlCommand.Parameters["@ContinentName"].Value = continentName ?? (object)DBNull.Value;
            sqlCommand.Parameters["@IsEu"].Value = isEU ?? (object)DBNull.Value;

            sqlCommand.ExecuteNonQuery();
        }
    }
}
