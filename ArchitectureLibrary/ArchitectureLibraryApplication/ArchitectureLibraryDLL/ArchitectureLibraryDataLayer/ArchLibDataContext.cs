using ArchitectureLibraryUtility;
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
        private static SqlConnection sqlConnection_;
        public static void OpenSqlConnection()
        {
            string databaseConnectionString = Utilities.GetDatabaseConnectionString("DatabaseConnectionString");
            sqlConnection_ = new SqlConnection(databaseConnectionString);
            sqlConnection_.Open();
        }
        public static void CloseSqlConnection()
        {
            sqlConnection_.Close();
        }
        public static SqlConnection SqlConnectionObject
        {
            get
            {
                return sqlConnection_;
            }
        }

        public static List<Dictionary<string, string>> GetSqlQueryResult(SqlCommand sqlCommand, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            int i;
            sqlCommand.Connection = sqlConnection;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            List<Dictionary<string, string>> searchResultRows = new List<Dictionary<string, string>>();
            Dictionary<string, string> searchResultRow;
            while (sqlDataReader.Read())
            {
                searchResultRow = new Dictionary<string, string>();
                for (i = 0; i < sqlDataReader.FieldCount; i++)
                {
                    searchResultRow[sqlDataReader.GetName(i)] = sqlDataReader[i].ToString();
                }
                searchResultRows.Add(searchResultRow);
            }
            sqlDataReader.Close();
            return searchResultRows;
        }
    }
}
