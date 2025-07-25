﻿using ArchitectureLibraryUtility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace RetailSlnDataLayer
{
    public static partial class ApplicationDataContext
    {
        private static SqlConnection sqlConnection_;
        public static void OpenSqlConnection()
        {
            string databaseConnectionString = Utilities.GetDatabaseConnectionString("DatabaseConnectionString");
            sqlConnection_ = new SqlConnection(databaseConnectionString);
            sqlConnection_.Open();
        }
        public static SqlConnection OpenSqlConnection(bool dummy1)
        {
            string databaseConnectionString = Utilities.GetDatabaseConnectionString("DatabaseConnectionString");
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            sqlConnection.Open();
            return sqlConnection;
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
    }
}
