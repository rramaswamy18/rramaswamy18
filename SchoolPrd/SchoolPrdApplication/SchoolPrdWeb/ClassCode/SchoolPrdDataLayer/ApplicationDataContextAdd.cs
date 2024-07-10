using SchoolPrdModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SchoolPrdDataLayer
{
    public static partial class ApplicationDataContext
    {
        public static void AddPersonExtn1(PersonExtn1Model personExtn1Model, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            SqlCommand sqlCommand = new SqlCommand("INSERT RetailSlnSch.PersonExtn1(ClientId, PersonId, CorpAcctId, AddUserId, UpdUserId) SELECT @ClientId, @PersonId, @CorpAcctId, @LoggedInUserId, @LoggedInUserId", sqlConnection);
            sqlCommand.Parameters.Add("@ClientId", System.Data.SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@PersonId", System.Data.SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@CertificateDocumentId", System.Data.SqlDbType.BigInt);
            sqlCommand.Parameters.Add("@LoggedInUserId", System.Data.SqlDbType.NVarChar, 512);
            sqlCommand.Parameters["@ClientId"].Value = clientId;
            sqlCommand.Parameters["@PersonId"].Value = personExtn1Model.PersonId;
            sqlCommand.Parameters["@CertificateDocumentId"].Value = personExtn1Model.CertificateDocumentId;
            sqlCommand.Parameters["@LoggedInUserId"].Value = loggedInUserId;
            sqlCommand.ExecuteNonQuery();
        }
    }
}
