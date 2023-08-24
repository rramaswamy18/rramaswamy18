using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryCreditCardDataLayer
{
     public static partial class ArchLibCreditCardDataContext
    {
        private static SqlCommand BuildSqlCommandCreditCardData(SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string sqlStmt = "";
            sqlStmt += "        INSERT ArchLib.CreditCardData" + Environment.NewLine;
            sqlStmt += "              (" + Environment.NewLine;
            sqlStmt += "               ProcessMessage" + Environment.NewLine;
            sqlStmt += "              ,ProcessorName" + Environment.NewLine;
            sqlStmt += "              ,RequestData" + Environment.NewLine;
            sqlStmt += "              ,ResponseData" + Environment.NewLine;
            sqlStmt += "              ,StatusNameDesc" + Environment.NewLine;
            sqlStmt += "              )" + Environment.NewLine;
            sqlStmt += "        OUTPUT INSERTED.CreditCardDataId" + Environment.NewLine;
            sqlStmt += "        SELECT" + Environment.NewLine;
            sqlStmt += "               @ProcessMessage" + Environment.NewLine;
            sqlStmt += "              ,@ProcessorName" + Environment.NewLine;
            sqlStmt += "              ,@RequestData" + Environment.NewLine;
            sqlStmt += "              ,@ResponseData" + Environment.NewLine;
            sqlStmt += "              ,@StatusNameDesc" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.Add("@ProcessMessage", SqlDbType.NVarChar, 250);
            sqlCommand.Parameters.Add("@ProcessorName", SqlDbType.NVarChar, 50);
            sqlCommand.Parameters.Add("@RequestData", SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@ResponseData", SqlDbType.NVarChar);
            sqlCommand.Parameters.Add("@StatusNameDesc", SqlDbType.NVarChar, 50);
            return sqlCommand;
        }
    }
}
