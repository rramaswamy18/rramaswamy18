using ArchitectureLibraryException;
using ArchitectureLibraryUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml;

namespace ArchitectureLibraryCreditCardBusinessLayer
{
    public class CreditCardNuveiBL
    {
        public bool ProcessCreditCard(string creditCardAmount, string currencyCode, string creditCardNumber, string creditCardSecCode, string creditCardExpMM, string creditCardExpYear, string nameAsOnCard, string creditCardTranType, Dictionary<string, string> creditCardKVPs, out string cardNumberLast4, out string processMessage, out string requestData, out string responseData, long clientId = 0, string ipAddress = "", string execUniqueId = "", string loggedInUserId = "")
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string terminalType = "2", transactionType = "7";
                string creditCardType;
                string cardExpiry = creditCardExpMM + creditCardExpYear.Substring(2);
                switch (creditCardNumber.Substring(0, 1))
                {
                    case "37":
                        creditCardType = "AMEX";
                        break;
                    case "38":
                        creditCardType = "DINERS";
                        break;
                    case "4":
                        creditCardType = "VISA";
                        break;
                    case "5":
                        creditCardType = "MASTERCARD";
                        break;
                    case "6":
                        creditCardType = "DISCOVER";
                        break;
                    default:
                        creditCardType = "UNKNOWN";
                        break;
                }
                bool returnValue = ProcessCreditCard(creditCardAmount, currencyCode, creditCardNumber, creditCardType, cardExpiry, nameAsOnCard, creditCardSecCode, terminalType, transactionType, creditCardKVPs, out cardNumberLast4, out processMessage, out requestData, out responseData, clientId, ipAddress, execUniqueId, loggedInUserId);
                return returnValue;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        private bool ProcessCreditCard(string amount, string currency, string cardNumber, string cardType, string cardExpiry, string cardHolderName, string cVV, string terminalType, string transactionType, Dictionary<string, string> creditCardKVPs, out string cardNumberLast4, out string processMessage, out string requestData, out string responseData, long clientId = 0, string ipAddress = "", string execUniqueId = "", string loggedInUserId = "")
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                string restAPIRootUri, requestUri, terminalId, sharedSecret, privateKey;
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Before getting values from Config");
                //privateKey
                try
                {
                    privateKey = creditCardKVPs["PrivateKey"];
                }
                catch
                {
                    privateKey = Utilities.GetApplicationValue("PrivateKey");
                }
                //"https://testpayments.nuvei.com/";
                try
                {
                    restAPIRootUri = creditCardKVPs["NuveiRestAPIRootUri"];
                }
                catch
                {
                    restAPIRootUri = Utilities.GetApplicationValue("NuveiRestAPIRootUri");
                }
                //"merchant/xmlpayment";
                try
                {
                    requestUri = creditCardKVPs["NuveiRequestUri"];
                }
                catch
                {
                    requestUri = Utilities.GetApplicationValue("NuveiRequestUri");
                }
                //"1064144";
                try
                {
                    terminalId = creditCardKVPs["NuveiTerminalId"];
                }
                catch
                {
                    terminalId = Utilities.GetApplicationValue("NuveiTerminalId");
                }
                //"123456789G1";
                try
                {
                    sharedSecret = creditCardKVPs["NuveiSharedSecret"];
                }
                catch
                {
                    sharedSecret = Utilities.GetApplicationValue("NuveiSharedSecret");
                }
                terminalId = EncryptDecrypt.DecryptDataMd5(terminalId, privateKey);
                sharedSecret = EncryptDecrypt.DecryptDataMd5(sharedSecret, privateKey);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: After getting values from Config");
                cardNumberLast4 = cardNumber.Substring(cardNumber.Length - 4);
                var returnValue = ProcessCreditCard(restAPIRootUri, requestUri, terminalId, sharedSecret, terminalType, transactionType, currency, amount, cardHolderName, cardNumber, cardNumberLast4, cVV, cardType, cardExpiry, out processMessage, out string requestXML, out string requestXML1, out string responseXML, clientId, ipAddress, execUniqueId, loggedInUserId);
                requestData = requestXML1;
                responseData = responseXML;
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return returnValue;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        public bool ProcessCreditCard(string restAPIRootUri, string requestUri, string terminalId, string sharedSecret, string terminalType, string transactionType, string currency, string amount, string cardHolderName, string cardNumber, string cardNumberLast4, string cVV, string cardType, string cardExpiry, out string processMessage, out string requestXML, out string requestXML1, out string responseXML, long clientId = 0, string ipAddress = "", string execUniqueId = "", string loggedInUserId = "")
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                var returnValue = true;
                string reasonPhrase;
                string orderId = GenerateOrderId();
                string dateTime = DateTime.Now.ToString("dd-MM-yyyy:HH:mm:ss:fff");
                string hash = GenerateHash(terminalId, orderId, amount, dateTime, sharedSecret);
                requestXML = GenerateXML(orderId, terminalId, amount, dateTime, cardNumber, cardType, cardExpiry, cardHolderName, hash, currency, terminalType, transactionType, cVV);
                requestXML1 = GenerateXML(orderId, terminalId, amount, dateTime, cardNumberLast4, cardType, cardExpiry, cardHolderName, hash, currency, terminalType, transactionType, cVV);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: Before Web API", "amount", amount, "cardHolderName", cardHolderName, "cardNumberLast4", cardNumberLast4);
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                HttpResponseMessage httpResponseMessage = CallRESTServicePost(restAPIRootUri, requestUri, "", null, null, "text/xml; charset=utf-8", requestXML);
                reasonPhrase = httpResponseMessage.ReasonPhrase;
                responseXML = httpResponseMessage.Content.ReadAsStringAsync().Result;
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: After Web API", "amount", amount, "cardHolderName", cardHolderName, "cardNumberLast4", cardNumberLast4, "StatusCode", httpResponseMessage.StatusCode.ToString(), "reasonPhrase", reasonPhrase, "responseXML", responseXML);

                XmlDocument responseDataXmlDoc = new XmlDocument();
                responseDataXmlDoc.LoadXml(responseXML);

                XmlNode nodeToFind;
                XmlElement root = responseDataXmlDoc.DocumentElement;

                // Selects all the title elements that have an attribute named lang
                nodeToFind = root.SelectSingleNode("//PAYMENTRESPONSE");
                string prefixString = "";
                if (nodeToFind != null)
                {
                    returnValue = true;
                    processMessage = "";
                    nodeToFind = root.SelectSingleNode("//PAYMENTRESPONSE/UNIQUEREF");
                    if (nodeToFind != null)
                    {
                        processMessage = nodeToFind.InnerXml;
                    }
                    nodeToFind = root.SelectSingleNode("//PAYMENTRESPONSE/BANKRESPONSECODE");
                    if (nodeToFind != null)
                    {
                        if (nodeToFind.InnerXml == "00")
                        {
                            ;
                        }
                        else
                        {
                            returnValue = false;
                            nodeToFind = root.SelectSingleNode("//PAYMENTRESPONSE/RESPONSETEXT");
                            processMessage += Environment.NewLine + nodeToFind.InnerXml;
                        }
                    }
                }
                else
                {
                    returnValue = false;
                    nodeToFind = root.SelectSingleNode("//ERROR");
                    processMessage = "";
                    foreach (XmlNode xmlNode in nodeToFind)
                    {
                        processMessage += prefixString + xmlNode.InnerXml;
                        prefixString = Environment.NewLine;
                    }
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: ProcessResult", "returnValue", returnValue.ToString(), "amount", amount, "cardHolderName", cardHolderName, "cardNumberLast4", cardNumberLast4, "StatusCode", httpResponseMessage.StatusCode.ToString(), "reasonPhrase", reasonPhrase, "responseXML", responseXML);

                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                return returnValue;
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
        }
        private string GenerateOrderId()
        {
            string orderId;
            orderId = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            return orderId;
        }
        private string GenerateHash(string terminalId, string orderId, string amount, string dateTime, string sharedSecret)
        {
            string plainString;
            plainString = terminalId + orderId + amount + dateTime + sharedSecret;
            byte[] toBeHashedByteArray = Encoding.UTF8.GetBytes(plainString);
            MD5CryptoServiceProvider cryptHandler = new MD5CryptoServiceProvider();
            byte[] hashedByteArray = cryptHandler.ComputeHash(toBeHashedByteArray);
            string hashedString = "";
            foreach (byte hashedByte in hashedByteArray)
            {
                if (hashedByte < 16)
                {
                    hashedString += "0" + hashedByte.ToString("x");
                }
                else
                {
                    hashedString += hashedByte.ToString("x");
                }
            }
            return hashedString;
        }
        private string GenerateXML(string orderId, string terminalId, string amount, string dateTime, string cardNumber, string cardType, string cardExpiry, string cardHolderName, string hash, string currency, string terminalType, string transactionType, string cVV)
        {
            string xmlData = "";

            xmlData += "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine;
            xmlData += "<PAYMENT>" + Environment.NewLine;
            xmlData += "    <ORDERID>" + orderId + "</ORDERID>" + Environment.NewLine;
            xmlData += "    <TERMINALID>" + terminalId + "</TERMINALID>" + Environment.NewLine;
            xmlData += "    <AMOUNT>" + amount + "</AMOUNT>" + Environment.NewLine;
            xmlData += "    <DATETIME>" + dateTime + "</DATETIME>" + Environment.NewLine;
            xmlData += "    <CARDNUMBER>" + cardNumber + "</CARDNUMBER>" + Environment.NewLine;
            xmlData += "    <CARDTYPE>" + cardType + "</CARDTYPE>" + Environment.NewLine;
            xmlData += "    <CARDEXPIRY>" + cardExpiry + "</CARDEXPIRY>" + Environment.NewLine;
            xmlData += "    <CARDHOLDERNAME>" + cardHolderName + "</CARDHOLDERNAME>" + Environment.NewLine;
            xmlData += "    <HASH>" + hash + "</HASH>" + Environment.NewLine;
            xmlData += "    <CURRENCY>" + currency + "</CURRENCY>" + Environment.NewLine;
            xmlData += "    <TERMINALTYPE>" + terminalType + "</TERMINALTYPE>" + Environment.NewLine;
            xmlData += "    <TRANSACTIONTYPE>" + transactionType + "</TRANSACTIONTYPE>" + Environment.NewLine;
            xmlData += "    <CVV>" + cVV + "</CVV>" + Environment.NewLine;
            xmlData += "</PAYMENT>" + Environment.NewLine;

            return xmlData;
        }
        private HttpResponseMessage CallRESTServicePost(string restAPIRootUri, string requestUri, string queryString, string authorizationKey = null, string authorizationValue = null, string contentType = null, string contentData = null)
        {
            string webAPIURL = restAPIRootUri + requestUri + queryString;
            HttpContent content = null;
            HttpResponseMessage httpResponseMessage;
            if (contentData != null && contentData != "")
            {
                content = new StringContent(contentData);
                content.Headers.Clear();
                content.Headers.Add("Content-Type", contentType);
            }
            using (HttpClient client = new HttpClient())
            {
                if (authorizationKey != null && authorizationValue != null)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authorizationKey, authorizationValue);
                }
                httpResponseMessage = client.PostAsync(webAPIURL, content).Result;
            }
            return httpResponseMessage;
        }
    }
}
