using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryTools
{
    public class CodeGeneration
    {
        public void GenerateLookup(string databaseConnectionString, string outputDirectoryName)
        {
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            sqlConnection.Open();
            GenerateLookup(sqlConnection, outputDirectoryName);
            sqlConnection.Close();
        }
        public void GenerateLookup(SqlConnection sqlConnection, string outputDirectoryName)
        {
            StreamWriter streamWriterDropTables = new StreamWriter(outputDirectoryName + @"\LookupDropTables.sql");
            StreamWriter streamWriterCreateTables = new StreamWriter(outputDirectoryName + @"\LookupCreateTables.sql");
            StreamWriter streamWriterPopulateTables = new StreamWriter(outputDirectoryName + @"\LookupPopulateTables.sql");
            StreamWriter streamWriterValidateTables = new StreamWriter(outputDirectoryName + @"\LookupRowCount.sql");
            streamWriterDropTables.Write("{0}{1}", "-- Drop Lookup Tables", Environment.NewLine);
            streamWriterCreateTables.Write("{0}{1}", "-- Create Lookup Tables", Environment.NewLine);
            streamWriterPopulateTables.Write("{0}{1}", "-- Populate Lookup Tables", Environment.NewLine);
            streamWriterValidateTables.Write("{0}{1}", "-- Count Feilds Lookup Tables", Environment.NewLine);
            string UnionSelect = "";
            string[] columnNames =
            {
                 " @@##TABLE_NAME##@@Id BIGINT NOT NULL"// IDENTITY(0, 100)"
                ,",ClientId BIGINT NOT NULL"
                ,",@@##TABLE_NAME##@@NameDesc NVARCHAR(100) NOT NULL"
                ,",@@##TABLE_NAME##@@Desc0 NVARCHAR(1024) NOT NULL"
                ,",@@##TABLE_NAME##@@Desc1 NVARCHAR(1024) NULL"
                ,",@@##TABLE_NAME##@@Desc2 NVARCHAR(1024) NULL"
                ,",AddUserId NVARCHAR(256) NOT NULL CONSTRAINT @@##TABLE_NAME##@@_DF_AddUserId DEFAULT ''"
                ,",AddUserName NVARCHAR(512) NOT NULL CONSTRAINT @@##TABLE_NAME##@@_DF_AddUserName DEFAULT SUSER_NAME()"
                ,",AddDateTime DATETIME2 NOT NULL CONSTRAINT @@##TABLE_NAME##@@_DF_AddDateTime DEFAULT SYSDATETIME()"
                ,",UpdUserId NVARCHAR(256) NOT NULL CONSTRAINT @@##TABLE_NAME##@@_DF_UpdUserId DEFAULT ''"
                ,",UpdUserName NVARCHAR(512) NOT NULL CONSTRAINT @@##TABLE_NAME##@@_DF_UpdUserName DEFAULT SUSER_NAME()"
                ,",UpdDateTime DATETIME2 NOT NULL CONSTRAINT @@##TABLE_NAME##@@_DF_UpdDateTime DEFAULT SYSDATETIME()"
            };
            string sqlStmt = "";
            sqlStmt += "        SELECT CodeType.CodeTypeNameId, CodeType.CodeTypeNameDesc, CodeType.CodeTypeDesc" + Environment.NewLine;
            sqlStmt += "          FROM Lookup.CodeType" + Environment.NewLine;
            sqlStmt += "      ORDER BY" + Environment.NewLine;
            sqlStmt += "               CodeType.CodeTypeNameDesc" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            string codeTypeNameDesc;
            while (sqlDataReader.Read())
            {
                codeTypeNameDesc = sqlDataReader["CodeTypeNameDesc"].ToString();
                streamWriterDropTables.Write("{0}{1}{2}", "DROP TABLE Lookup.", codeTypeNameDesc, Environment.NewLine);
                streamWriterDropTables.Write("{0}{1}", "GO", Environment.NewLine);
                streamWriterCreateTables.Write("{0}{1}{2}", "CREATE TABLE Lookup.", codeTypeNameDesc, Environment.NewLine);
                streamWriterCreateTables.Write("{0}{1}", "(", Environment.NewLine);
                foreach (string columnName in columnNames)
                {
                    streamWriterCreateTables.Write("{0}{1}{2}", "    ", columnName.Replace("@@##TABLE_NAME##@@", codeTypeNameDesc), Environment.NewLine);
                }
                streamWriterCreateTables.Write("{0}{1}{2}", "    ", ",CONSTRAINT " + codeTypeNameDesc + "_PK PRIMARY KEY CLUSTERED", Environment.NewLine);
                streamWriterCreateTables.Write("{0}{1}{2}", "    ", "(", Environment.NewLine);
                streamWriterCreateTables.Write("{0}{1}{2}", "    ", "     " + codeTypeNameDesc + "Id ASC", Environment.NewLine);
                streamWriterCreateTables.Write("{0}{1}{2}", "    ", ")", Environment.NewLine);
                streamWriterCreateTables.Write("{0}{1}{2}", "    ", ",CONSTRAINT " + codeTypeNameDesc + "_IX0 UNIQUE NONCLUSTERED", Environment.NewLine);
                streamWriterCreateTables.Write("{0}{1}{2}", "    ", "(", Environment.NewLine);
                streamWriterCreateTables.Write("{0}{1}{2}", "    ", "     " + codeTypeNameDesc + "NameDesc ASC", Environment.NewLine);
                streamWriterCreateTables.Write("{0}{1}{2}", "    ", ")", Environment.NewLine);
                streamWriterCreateTables.Write("{0}{1}", ")", Environment.NewLine);
                streamWriterCreateTables.Write("{0}{1}", "GO", Environment.NewLine);

                //streamWriterPopulateTables.Write("{0}{1}", "SET IDENTITY_INSERT Lookup." + codeTypeNameDesc + " ON", Environment.NewLine);
                streamWriterPopulateTables.Write("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}", "    ", "INSERT Lookup.", codeTypeNameDesc, "(", codeTypeNameDesc + "Id, ClientId, ", codeTypeNameDesc + "NameDesc, ", codeTypeNameDesc + "Desc0, ", codeTypeNameDesc + "Desc1, ", codeTypeNameDesc + "Desc2", ")", Environment.NewLine);
                streamWriterPopulateTables.Write("{0}{1}{2}", "    ", "SELECT CodeDataNameId, 0 AS ClientId, CodeDataNameDesc, CodeDataDesc0, CodeDataDesc1, CodeDataDesc2", Environment.NewLine);
                streamWriterPopulateTables.Write("{0}{1}{2}", "    ", "  FROM Lookup.CodeData", Environment.NewLine);
                streamWriterPopulateTables.Write("{0}{1}{2}", "", "INNER JOIN Lookup.CodeType", Environment.NewLine);
                streamWriterPopulateTables.Write("{0}{1}{2}", "    ", "    ON CodeData.CodeTypeId = CodeType.CodeTypeId", Environment.NewLine);
                streamWriterPopulateTables.Write("{0}{1}{2}", "    ", " WHERE CodeType.CodeTypeNameDesc = '" + codeTypeNameDesc + "'", Environment.NewLine);
                //Create Insert for each table
                //streamWriterPopulateTables.Write("{0}{1}", "SET IDENTITY_INSERT Lookup." + codeTypeNameDesc + " OFF", Environment.NewLine);
                streamWriterPopulateTables.Write("{0}{1}", "GO", Environment.NewLine);
                //Counting Rows             
                streamWriterValidateTables.Write("{0}{1}{2}", UnionSelect, "SELECT 'Lookup." + codeTypeNameDesc + "' AS TableName, COUNT(*) FROM Lookup." + codeTypeNameDesc, Environment.NewLine);
                UnionSelect = "UNION ";
                //streamWriterValidateTables.Write("{0}{1}", "SELECT 'Lookup." + codeTypeNameDesc + "' AS TableName, COUNT(*) FROM Lookup." + codeTypeNameDesc + "", Environment.NewLine);
            }
            streamWriterValidateTables.Write("{0}{1}", "GO", Environment.NewLine);
            sqlDataReader.Close();
            streamWriterPopulateTables.Close();
            streamWriterCreateTables.Close();
            streamWriterDropTables.Close();
            streamWriterValidateTables.Close();
        }
    }
}
