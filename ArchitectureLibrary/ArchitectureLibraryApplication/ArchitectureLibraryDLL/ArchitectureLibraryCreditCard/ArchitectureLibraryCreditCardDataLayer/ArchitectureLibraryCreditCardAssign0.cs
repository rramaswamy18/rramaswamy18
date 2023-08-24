using ArchitectureLibraryCreditCardModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryCreditCardDataLayer
{
    public static partial class ArchLibCreditCardDataContext
    {
        private static void AssignCreditCardData(CreditCardDataModel creditCardDataModel, SqlCommand sqlCommand, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            sqlCommand.Parameters["@ProcessMessage"].Value = creditCardDataModel.ProcessMessage;
            sqlCommand.Parameters["@ProcessorName"].Value = creditCardDataModel.CreditCardProcessor;
            sqlCommand.Parameters["@RequestData"].Value = creditCardDataModel.RequestData;
            sqlCommand.Parameters["@ResponseData"].Value = creditCardDataModel.ResponseData;
            sqlCommand.Parameters["@StatusNameDesc"].Value = creditCardDataModel.StatusNameDesc;
        }
    }
}
