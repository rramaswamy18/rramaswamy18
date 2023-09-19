using ArchitectureLibraryBusinessLayer;
using ArchitectureLibraryCacheData;
using ArchitectureLibraryDataLayer;
using ArchitectureLibraryEmailService;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using ArchitectureLibraryTemplate;
using SchoolPrdModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.IO;
using System.Data.SqlClient;

namespace SchoolPrdBusinessLayer
{
    public partial class SchoolPrdBL
    {
        private string LoadSignatureHtmlContent(string documentImagesDirectoryName, string htmlFullFileName)
        {
            StreamReader streamReader = new StreamReader(documentImagesDirectoryName + htmlFullFileName);
            string templateHtmlContent = streamReader.ReadToEnd();
            streamReader.Close();
            return templateHtmlContent;
        }
        private void SendEnrollmentAgreementEmail(ClassEnrollModel classEnrollModel, SessionObjectModel sessionObjectModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            Dictionary<string, string> keywordValues = BuildAgreementEmailTemplateKeywordValues(classEnrollModel, sessionObjectModel, clientId, ipAddress, execUniqueId, loggedInUserId);
            TemplateBL templateBL = new TemplateBL();
            Dictionary<string, string> templateWithData = templateBL.PopulateKeyWords("EnrollmentAgreementEmailTemplate", keywordValues);
            EmailService emailService = new EmailService();
            string privateKey = ArchLibCache.GetPrivateKey(clientId);
            string emailDirectoryName = Utilities.GetApplicationValue("EmailDirectoryName");
            var fromEmailAddress = new KeyValuePair<string, string>(ArchLibCache.GetApplicationDefault(clientId, "FromEmailAddress", ""), ArchLibCache.GetApplicationDefault(clientId, "FromEmailAddressDisplayName", ""));
            var toEmailAddresses = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(classEnrollModel.PersonModel.AspNetUserModel.Email, ""),
            };
            List<KeyValuePair<string, string>> replyToEmailAddresses = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(ArchLibCache.GetApplicationDefault(clientId, "FromEmailAddress", ""), ArchLibCache.GetApplicationDefault(clientId, "FromEmailAddressDisplayName", "")),
            };
            List<KeyValuePair<string, string>> ccEmailAddresses = new List<KeyValuePair<string, string>>
            {
                fromEmailAddress,
            };
            List<KeyValuePair<string, string>> bccEmailAddresses = new List<KeyValuePair<string, string>>();
            try
            {
                bccEmailAddresses.Add(new KeyValuePair<string, string>(ArchLibCache.GetApplicationDefault(clientId, "BccEmailAddress", ""), ""));
            }
            catch
            {
                ;
            }
            string documentsImagesDirectoryName = Utilities.GetApplicationValue("DocumentsImagesDirectoryName");
            List<string> emailAttachmentFileNames = new List<string>();
            emailAttachmentFileNames.Add(documentsImagesDirectoryName + @"\InitialsSignature_" + classEnrollModel.PersonId + @"\EnrollmentAgreement_" + classEnrollModel.PersonId + ".pdf");
            //emailService.SendEmail(emailDirectoryName, "", fromEmailAddress, templateWithData["EMAIL_SUBJECT"], templateWithData["EMAIL_BODY"], toEmailAddresses, ipAddress, execUniqueId, loggedInUserId, privateKey, replyToEmailAddresses, ccEmailAddresses, bccEmailAddresses, emailAttachmentFileNames);
        }
    }
}
