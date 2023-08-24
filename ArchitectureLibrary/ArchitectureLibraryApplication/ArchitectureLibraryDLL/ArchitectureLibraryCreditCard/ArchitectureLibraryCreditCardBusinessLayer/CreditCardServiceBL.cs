using ArchitectureLibraryCreditCardDataLayer;
using ArchitectureLibraryCreditCardModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryCreditCardBusinessLayer
{
    public class CreditCardServiceBL
    {
        public bool ProcessCreditCard(CreditCardDataModel creditCardDataModel, out string processMessage, SqlConnection sqlConnection, long clientId = 0, string ipAddress = "", string execUniqueId = "", string loggedInuserId = "")
        {
            bool returnValue = ProcessCreditCard(creditCardDataModel.CreditCardAmount, creditCardDataModel.CurrencyCode, creditCardDataModel.CreditCardNumber, creditCardDataModel.CreditCardSecCode, creditCardDataModel.CreditCardExpMM, creditCardDataModel.CreditCardExpYear, creditCardDataModel.NameAsOnCard, creditCardDataModel.CreditCardProcessor, creditCardDataModel.CreditCardTranType, creditCardDataModel.CreditCardKVPs, out string cardNumberLast4, out long creditCardDataId, out processMessage, sqlConnection, clientId, ipAddress, execUniqueId, loggedInuserId);
            creditCardDataModel.CreditCardNumberLast4 = cardNumberLast4;
            creditCardDataModel.CreditCardDataId = creditCardDataId;
            creditCardDataModel.ProcessMessage = processMessage;
            return returnValue;
        }
        public bool ProcessCreditCard(string creditCardAmount, string currencyCode, string creditCardNumber, string creditCardSecCode, string creditCardExpMM, string creditCardExpYear, string nameAsOnCard, string creditCardProcessor, string creditCardTranType, Dictionary<string, string> creditCardKVPs, out string cardNumberLast4, out long creditCardDataId, out string processMessage, SqlConnection sqlConnection, long clientId = 0, string ipAddress = "", string execUniqueId = "", string loggedInUserId = "")
        {
            bool returnValue;
            string requestData, responseData;
            CreditCardDataModel creditCardDataModel;
            switch (creditCardProcessor.ToUpper())
            {
                case "TESTMODE":
                    CreditCardTestBL creditCardTestBL = new CreditCardTestBL();
                    returnValue = creditCardTestBL.ProcessCreditCard(creditCardAmount, currencyCode, creditCardNumber, creditCardSecCode, creditCardExpMM, creditCardExpYear, nameAsOnCard, creditCardTranType, creditCardKVPs, sqlConnection, out cardNumberLast4, out processMessage, out requestData, out responseData, clientId, ipAddress, execUniqueId, loggedInUserId);
                    creditCardDataModel = new CreditCardDataModel
                    {
                        CreditCardProcessor = creditCardProcessor,
                        ProcessMessage = processMessage,
                        RequestData = requestData,
                        ResponseData = responseData,
                        StatusNameDesc = returnValue ? "SUCCESS" : "ERROR",
                    };
                    ArchLibCreditCardDataContext.CreateCreditCardData(creditCardDataModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                    creditCardDataId = creditCardDataModel.CreditCardDataId;
                    break;
                case "NUVEI":
                    CreditCardNuveiBL creditCardNuveiBL = new CreditCardNuveiBL();
                    returnValue = creditCardNuveiBL.ProcessCreditCard(creditCardAmount, currencyCode, creditCardNumber, creditCardSecCode, creditCardExpMM, creditCardExpYear, nameAsOnCard, creditCardTranType, creditCardKVPs, out cardNumberLast4, out processMessage, out requestData, out responseData, clientId, ipAddress, execUniqueId, loggedInUserId);
                    creditCardDataModel = new CreditCardDataModel
                    {
                        CreditCardProcessor = creditCardProcessor,
                        ProcessMessage = processMessage,
                        RequestData = requestData,
                        ResponseData = responseData,
                        StatusNameDesc = returnValue ? "SUCCESS" : "ERROR",
                    };
                    ArchLibCreditCardDataContext.CreateCreditCardData(creditCardDataModel, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                    creditCardDataId = creditCardDataModel.CreditCardDataId;
                    break;
                default:
                    creditCardDataId = 0;
                    cardNumberLast4 = "";
                    returnValue = false;
                    processMessage = "Invalid Credit Card Processor";
                    break;
            }
            return returnValue;
        }
    }
}
