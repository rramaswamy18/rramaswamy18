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
        public bool ProcessCreditCard(CreditCardDataModel creditCardDataModel, SqlConnection sqlConnection, out string processMessage, long clientId = 0, string ipAddress = "", string execUniqueId = "", string loggedInuserId = "")
        {
            processMessage = "";
            creditCardDataModel.CreditCardNumberLast4 = "";
            creditCardDataModel.CreditCardDataId = -1;
            creditCardDataModel.ProcessMessage = processMessage;
            return true;
        }
        public bool ProcessCreditCard(CreditCardDataModel creditCardDataModel, SqlConnection sqlConnection, out string processMessage, out object creditCardResponseObject, long clientId = 0, string ipAddress = "", string execUniqueId = "", string loggedInuserId = "")
        {
            bool returnValue = ProcessCreditCard(creditCardDataModel.CreditCardAmount, creditCardDataModel.CurrencyCode, creditCardDataModel.CreditCardNumber, creditCardDataModel.CreditCardSecCode, creditCardDataModel.CreditCardExpMM, creditCardDataModel.CreditCardExpYear, creditCardDataModel.NameAsOnCard, creditCardDataModel.CreditCardProcessor, creditCardDataModel.CreditCardTranType, creditCardDataModel.CreditCardKVPs, out string cardNumberLast4, out long creditCardDataId, out processMessage, sqlConnection, out creditCardResponseObject, clientId, ipAddress, execUniqueId, loggedInuserId);
            creditCardDataModel.CreditCardNumberLast4 = cardNumberLast4;
            creditCardDataModel.CreditCardDataId = creditCardDataId;
            creditCardDataModel.ProcessMessage = processMessage;
            return returnValue;
        }
        public bool ProcessCreditCard(string creditCardAmount, string currencyCode, string creditCardNumber, string creditCardSecCode, string creditCardExpMM, string creditCardExpYear, string nameAsOnCard, string creditCardProcessor, string creditCardTranType, Dictionary<string, string> creditCardKVPs, out string cardNumberLast4, out long creditCardDataId, out string processMessage, SqlConnection sqlConnection, out object creditCardResponseObject, long clientId = 0, string ipAddress = "", string execUniqueId = "", string loggedInUserId = "")
        {
            bool returnValue;
            string requestData, responseData;
            CreditCardDataModel creditCardDataModel;
            CreditCardNuveiBL creditCardNuveiBL = new CreditCardNuveiBL();
            CreditCardRazorPayBL creditCardRazorPayBL = new CreditCardRazorPayBL();
            switch (creditCardProcessor.ToUpper())
            {
                case "TESTMODE":
                    creditCardResponseObject = null;
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
                case "NUVEITEST":
                    creditCardResponseObject = null;
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
                case "NUVEIPROD":
                    creditCardResponseObject = null;
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
                case "PHONEPETEST":
                    creditCardResponseObject = null;
                    creditCardDataId = 0;
                    cardNumberLast4 = "";
                    returnValue = false;
                    processMessage = "Invalid Credit Card Processor";
                    break;
                case "PHONEPEPROD":
                    creditCardResponseObject = null;
                    creditCardDataId = 0;
                    cardNumberLast4 = "";
                    returnValue = false;
                    processMessage = "Invalid Credit Card Processor";
                    break;
                case "RAZORPAYTEST":
                    RazorPayRequest razorPayRequest = new RazorPayRequest
                    {
                        Address = "",
                        Amount = creditCardAmount,
                        ApiKey = "rzp_test_PSawHylUJK5KZi",
                        ApiSecret = "Zz8yhAXr1BMLKHfPLSHR7TpH",
                        Name = "Fake name",
                        Email = "fake@fakemail.com",
                        PhoneNumber = "912084422881",
                        Currency = "INR",
                        Receipt = Guid.NewGuid().ToString(),
                        PaymentCapture = "1"
                    };
                    RazorPayResponse razorPayResponse = creditCardRazorPayBL.ProcessCreditCard(razorPayRequest);
                    creditCardResponseObject = razorPayResponse;
                    creditCardDataId = 0;
                    cardNumberLast4 = "";
                    returnValue = true;
                    processMessage = "Order created succssfully";
                    break;
                case "RAZORPAYPROD":
                    creditCardResponseObject = null;
                    creditCardDataId = 0;
                    cardNumberLast4 = "";
                    returnValue = false;
                    processMessage = "Invalid Credit Card Processor";
                    break;
                default:
                    creditCardResponseObject = null;
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
