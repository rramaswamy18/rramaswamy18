using ArchitectureLibraryBusinessLayer;
using ArchitectureLibraryCacheData;
using ArchitectureLibraryDataLayer;
using ArchitectureLibraryEmailService;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryTemplate;
using ArchitectureLibraryUtility;
using RetailSlnModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace RetailSlnBusinessLayer
{
    public partial class RetailSlnBL
    {
        private bool RegisterUserProf(RegisterUserProfModel registerUserProfModel, string loginPassword, string phoneNumber, string firstName, string lastName, SqlConnection sqlConnection, long clientId, string ipAddress, string execUniqueId, string loggedInUserId, out AspNetUserModel aspNetUserModel)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            ArchLibBL archLibBL = new ArchLibBL();
            try
            {
                aspNetUserModel = ArchLibDataContext.SelectAspNetUserFromUserName(registerUserProfModel.RegisterEmailAddress, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                if (string.IsNullOrWhiteSpace(aspNetUserModel.AspNetUserId))
                {//User is not registered - create user with password - Do not need reset password - Set the expiry to 30 days
                    string aspNetUserId = Guid.NewGuid().ToString();
                    string privateKey = ArchLibCache.GetPrivateKey(clientId);
                    string loginPasswordEncrypted = EncryptDecrypt.EncryptDataMd5(loginPassword, privateKey);
                    DateTime loginPasswordExpiryDateTime = DateTime.Now.AddDays(30);
                    long certificateDocumentId = 0;
                    ArchLibDataContext.CreateRegisterUser(aspNetUserId, registerUserProfModel.RegisterEmailAddress, loginPasswordEncrypted, loginPasswordExpiryDateTime, phoneNumber, null, null, null, firstName, lastName, certificateDocumentId, 0, "", 0, "", ArchitectureLibraryEnumerations.UserStatusEnum.Active, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                    aspNetUserModel = ArchLibDataContext.SelectAspNetUserFromUserName(registerUserProfModel.RegisterEmailAddress, sqlConnection, clientId, ipAddress, execUniqueId, loggedInUserId);
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
                    return true;
                }
                else
                {//User is already registered
                    return false;
                }
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
                throw;
            }
            finally
            {
            }
        }
        //private void SendGiftCertReceiptEmail(GiftCertModel giftCertModel, bool userProfRegistered, string loginPassword, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        //{
        //    ArchLibBL archLibBL = new ArchLibBL();
        //    TemplateBL templateBL = new TemplateBL();
        //    Dictionary<string, string> keywordValues = new Dictionary<string, string>
        //    {
        //        { "@@##GiftCertNumber##@@", giftCertModel.GiftCertNumber.ToString() },
        //        { "@@##GiftCertKey##@@", giftCertModel.GiftCertKey.ToString() },
        //        { "@@##SenderFullName##@@", giftCertModel.SenderFullName },
        //        { "@@##SenderEmailAddress##@@", giftCertModel.SenderEmailAddress },
        //        { "@@##RecipientFullName##@@", giftCertModel.RecipientFullName },
        //        { "@@##RecipientEmailAddress##@@", giftCertModel.RecipientEmailAddress },
        //        { "@@##GiftCertAmount##@@", giftCertModel.GiftCertAmount.ToString() },
        //        { "@@##GiftCertMessage##@@", giftCertModel.GiftCertMessage },
        //        { "@@##LoginPassword##@@", loginPassword },
        //        { "@@##CreditCardProcessStatus##@@", giftCertModel.CreditCardProcessStatus ? "Success" : "Failure" },
        //        { "@@##CreditCardLast4##@@", giftCertModel.CreditCardLast4 },
        //        { "@@##CreditCardProcessMessage##@@", giftCertModel.CreditCardProcessMessage },

        //        { "@@##BaseUrl##@@", ArchLibCache.GetApplicationDefault(clientId, "BaseUrl", "") },
        //        { "@@##ContactUsUrl##@@", ArchLibCache.GetApplicationDefault(0, "ContactUsUrl", "") },
        //        { "@@##ContactPhone##@@", ArchLibCache.GetApplicationDefault(clientId, "ContactPhone", "") },
        //        { "@@##SignatureTemplate##@@", archLibBL.SignatureTemplate(clientId) },
        //    };
        //    if (userProfRegistered)
        //    {
        //        keywordValues["@@##RecipientEmailAddressRegistered##@@"] = " registered successfully with password " + loginPassword + " . Feel free to reset your password at anytime";
        //    }
        //    else
        //    {
        //        keywordValues["@@##RecipientEmailAddressRegistered##@@"] = " is already registered";
        //    }
        //    Dictionary<string, string> templateWithData = templateBL.PopulateKeyWords("GiftCertReceiptEmailTemplate", keywordValues);
        //    EmailService emailService = new EmailService();
        //    string privateKey = ArchLibCache.GetPrivateKey(clientId);
        //    string emailDirectoryName = Utilities.GetApplicationValue("EmailDirectoryName");
        //    var fromEmailAddress = new KeyValuePair<string, string>(ArchLibCache.GetApplicationDefault(clientId, "FromEmailAddress", ""), ArchLibCache.GetApplicationDefault(clientId, "FromEmailAddressDisplayName", ""));
        //    var toEmailAddresses = new List<KeyValuePair<string, string>>
        //    {
        //        new KeyValuePair<string, string>(giftCertModel.SenderEmailAddress, ""),
        //        new KeyValuePair<string, string>(giftCertModel.RecipientEmailAddress, ""),
        //    };
        //    List<KeyValuePair<string, string>> ccEmailAddresses = new List<KeyValuePair<string, string>>
        //    {
        //        fromEmailAddress,
        //    };
        //    List<KeyValuePair<string, string>> bccEmailAddresses = new List<KeyValuePair<string, string>>();
        //    try
        //    {
        //        bccEmailAddresses.Add(new KeyValuePair<string, string>(ArchLibCache.GetApplicationDefault(clientId, "BccEmailAddress", ""), ""));
        //    }
        //    catch
        //    {
        //        ;
        //    }
        //    List<string> emailAttachmentFileNames = new List<string>
        //    {
        //        giftCertModel.GiftCertImageFileName
        //    };
        //    emailService.SendEmail(emailDirectoryName, "", fromEmailAddress, templateWithData["EMAIL_SUBJECT"], templateWithData["EMAIL_BODY"], toEmailAddresses, execUniqueId, privateKey, null, ccEmailAddresses, bccEmailAddresses, emailAttachmentFileNames);
        //}
    }
}
//System.Web.Mvc.Html.PartialExtensions.Partial(html, "~/Views/Orders/OrdersPartialView.cshtml", orderModel).ToString();
