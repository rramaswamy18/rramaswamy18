using ArchitectureLibraryCacheData;
using ArchitectureLibraryException;
using ArchitectureLibraryUtility;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryEmailLibrary
{
    public class SendGridEmailService
    {
        public void SendEmail(string emailDirectory, string aspNetUserId, KeyValuePair<string, string> fromEmailAddress, List<KeyValuePair<string, string>> replyToEmailAddresses, string emailSubject, string emailBody, List<KeyValuePair<string, string>> toEmailAddresses, string ipAddress, string execUniqueId, string loggedInUserId, string privateKey = "", List<KeyValuePair<string, string>> ccEmailAddresses = null, List<KeyValuePair<string, string>> bccEmailAddresses = null, List<string> emailAttachmentFileNames = null)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            try
            {
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: API Key");
                var emailAPIKey = ArchLibCache.GetApplicationDefault(0, "SendGridAPIKey", "");
                //var emailAPIKey = Utilities.GetApplicationValue("EmailAPIKey");
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001500 :: API Key & Private Key", "emailAPIKey", emailAPIKey, "privateKey", privateKey);
                //emailAPIKey = EncryptDecrypt.DecryptDataMd5(emailAPIKey, privateKey);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: Email Client, From & To Email", "FromEmailAddress", fromEmailAddress.Key, "FromEmailDisplay", fromEmailAddress.Value, "ToEmailAddress", toEmailAddresses[0].Key, "emailAPIKey", emailAPIKey);
                var emailClient = new SendGridClient(emailAPIKey);
                var fromEmail = new EmailAddress(fromEmailAddress.Key, fromEmailAddress.Value);
                var toEmail = new EmailAddress(toEmailAddresses[0].Key);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00003000 :: Email Message");
                var emailMessage = MailHelper.CreateSingleEmail(fromEmail, toEmail, emailSubject, emailBody, emailBody);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00004000 :: Reply To");
                emailMessage.ReplyTo = new EmailAddress(replyToEmailAddresses[0].Key);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00005000 :: Cc Email Addresses");
                if (ccEmailAddresses != null)
                {
                    foreach (var ccEmailAddress in ccEmailAddresses)
                    {
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00006000 :: Cc Email Address", "CcEmailAddress", ccEmailAddress.Key);
                        emailMessage.AddCc(ccEmailAddress.Key);
                    }
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00007000 :: Bcc Email Addresses");
                if (bccEmailAddresses != null)
                {
                    foreach (var bccEmailAddress in bccEmailAddresses)
                    {
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00008000 :: Bcc Email Address", "BccEmailAddress", bccEmailAddress.Key);
                        emailMessage.AddBcc(bccEmailAddress.Key);
                    }
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00009000 :: Email Attachments");
                if (emailAttachmentFileNames != null)
                {
                    SendGrid.Helpers.Mail.Attachment emailAttachment;
                    foreach (var emailAttachmentFileName in emailAttachmentFileNames)
                    {
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00010000 Attachment to SenGrid Email", "aspNetUserId", aspNetUserId, "emailAttachmentFileName", emailAttachmentFileName);
                        emailAttachment = new SendGrid.Helpers.Mail.Attachment();
                        emailAttachment.Filename = emailAttachmentFileName;
                        emailAttachment.Content = MediaTypeNames.Application.Octet;
                        emailMessage.AddAttachment(emailAttachment);
                    }
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00011000 :: Email Repositry");
                EmailRepository emailRepository = new EmailRepository();
                MailMessage mailMessage = new MailMessage
                {
                    Body = emailBody,
                    From = new MailAddress(fromEmailAddress.Key, fromEmailAddress.Value),
                    IsBodyHtml = true,
                    Subject = emailSubject.Replace("\r\n", ""),
                };
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00012000 :: AddToMailAddressCollection - replyToEmailAddresses");
                AddToMailAddressCollection(mailMessage.ReplyToList, replyToEmailAddresses, ipAddress, execUniqueId, loggedInUserId);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00013000 :: AddToMailAddressCollection - toEmailAddresses");
                AddToMailAddressCollection(mailMessage.To, toEmailAddresses, ipAddress, execUniqueId, loggedInUserId);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00014000 :: AddToMailAddressCollection - ccEmailAddresses");
                AddToMailAddressCollection(mailMessage.CC, ccEmailAddresses, ipAddress, execUniqueId, loggedInUserId);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00015000 :: AddToMailAddressCollection - bccEmailAddresses");
                AddToMailAddressCollection(mailMessage.Bcc, bccEmailAddresses, ipAddress, execUniqueId, loggedInUserId);
                if (emailAttachmentFileNames != null)
                {
                    foreach (var emailAtatchmentFileName in emailAttachmentFileNames)
                    {
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00016000 :: EmailRepository Attachment", "emailAtatchmentFileName", emailAtatchmentFileName);
                        System.Net.Mail.Attachment emailAttachment = new System.Net.Mail.Attachment(emailAtatchmentFileName, MediaTypeNames.Application.Octet);
                        ContentDisposition disposition = emailAttachment.ContentDisposition;
                        disposition.CreationDate = File.GetCreationTime(emailAtatchmentFileName);
                        disposition.ModificationDate = File.GetLastWriteTime(emailAtatchmentFileName);
                        disposition.ReadDate = File.GetLastAccessTime(emailAtatchmentFileName);
                        disposition.FileName = Path.GetFileName(emailAtatchmentFileName);
                        disposition.Size = new FileInfo(emailAtatchmentFileName).Length;
                        disposition.DispositionType = DispositionTypeNames.Attachment;

                        mailMessage.Attachments.Add(emailAttachment);
                    }
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00017000 :: EmailRepository - SaveEmail");
                emailRepository.SaveEmail(emailDirectory, aspNetUserId, fromEmailAddress, emailSubject, emailBody, replyToEmailAddresses, toEmailAddresses, execUniqueId, mailMessage, ccEmailAddresses, bccEmailAddresses, emailAttachmentFileNames);
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00018000 :: SendGrid - Before SendEmailAsync");
                var response = emailClient.SendEmailAsync(emailMessage);
                mailMessage.Dispose();
                mailMessage = null;
                emailClient = null;
                System.Threading.Thread.Sleep(9000); //Sleep for 9 seconds
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00019000 :: SendGrid - After SendEmailAsync", "response", JsonConvert.SerializeObject(response));
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 Error while sending email", exception);
                throw;
            }
        }
        private void AddToMailAddressCollection(MailAddressCollection mailAddressCollection, List<KeyValuePair<string, string>> emailAddresses, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            if (emailAddresses != null)
            {
                MailAddress mailAddress;
                foreach (var emailaddress in emailAddresses)
                {
                    mailAddress = new MailAddress(emailaddress.Key, emailaddress.Value);
                    mailAddressCollection.Add(mailAddress);
                }
            }
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 Exit");
        }
        //public void SendEmail_WithPassword(string emailDirectory, string aspNetUserId, KeyValuePair<string, string> fromEmailAddress, List<KeyValuePair<string, string>> replyToEmailAddresses, string emailSubject, string emailBody, List<KeyValuePair<string, string>> toEmailAddresses, string execUniqueId, string privateKey = "", List<KeyValuePair<string, string>> ccEmailAddresses = null, List<KeyValuePair<string, string>> bccEmailAddresses = null, List<string> emailAttachmentFileNames = null)
        //{
        //    string methodName = MethodBase.GetCurrentMethod().Name, ipAddress = Utilities.GetIPAddress(), loggedInUserId = "";
        //    ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
        //    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
        //    try
        //    {
        //        //Handle attachments
        //        var userName = Utilities.GetApplicationValue("EmailServiceUserName");
        //        var password = Utilities.GetApplicationValue("EmailServicePassword");
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 :: Before Decrypt the username and password ", "AspNetUserId", aspNetUserId);
        //        userName = EncryptDecrypt.DecryptDataMd5(userName, privateKey);
        //        password = EncryptDecrypt.DecryptDataMd5(password, privateKey);
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002100 :: After Decrypt the username and password", "AspNetUserId", aspNetUserId);
        //        SendGrid sendGridMail;
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00003000 :: Before Passing the Network Credentials", "Username", userName, "Password", password);
        //        var credentials = new NetworkCredential(userName, password);
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00003100 :: After Passing the Network Credentials", "Username", userName, "Password", password);
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00004000 :: Before Calling Transweb", "Username", credentials.UserName, "Password", credentials.Password);
        //        var transportWeb = Web.GetInstance(credentials);
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00004100 :: After Calling Transweb", "Username", credentials.UserName, "Password", credentials.Password);
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00005000 :: Before Calling GetInstance()", "Username", credentials.UserName);
        //        sendGridMail = SendGrid.GetInstance();
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00005100 :: After Calling GetInstance()", "Username", credentials.UserName);
        //        sendGridMail.From = new MailAddress(fromEmailAddress.Key, fromEmailAddress.Value);
        //        if (replyToEmailAddresses != null)
        //        {
        //            foreach (var emailAddress in replyToEmailAddresses)
        //            {
        //                sendGridMail.Headers.Add("Reply-To", emailAddress.Key);
        //            }
        //        }
        //        if (toEmailAddresses != null)
        //        {
        //            foreach (var emailAddress in toEmailAddresses)
        //            {
        //                sendGridMail.AddTo(emailAddress.Key);
        //            }
        //        }
        //        if (ccEmailAddresses != null)
        //        {
        //            foreach (var emailAddress in ccEmailAddresses)
        //            {
        //                sendGridMail.AddCc(emailAddress.Key);
        //            }
        //        }
        //        if (bccEmailAddresses != null)
        //        {
        //            foreach (var emailAddress in bccEmailAddresses)
        //            {
        //                sendGridMail.AddBcc(emailAddress.Key);
        //            }
        //        }

        //        if (emailAttachmentFileNames != null)
        //        {
        //            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00006000 Add attachments to MailMessage", "aspNetUserId", aspNetUserId);
        //            foreach (var emailAtatchmentFileName in emailAttachmentFileNames)
        //            {
        //                // Attachment emailAttachment = new Attachment(emailAtatchmentFileName, MediaTypeNames.Application.Octet);
        //                // ContentDisposition disposition = emailAttachment.ContentDisposition;
        //                // disposition.CreationDate = File.GetCreationTime(emailAtatchmentFileName);
        //                // disposition.ModificationDate = File.GetLastWriteTime(emailAtatchmentFileName);
        //                // disposition.ReadDate = File.GetLastAccessTime(emailAtatchmentFileName);
        //                // disposition.FileName = Path.GetFileName(emailAtatchmentFileName);
        //                // disposition.Size = new FileInfo(emailAtatchmentFileName).Length;
        //                // disposition.DispositionType = DispositionTypeNames.Attachment;
        //                //// StreamReader streamReader = new StreamReader(disposition.FileName);
        //                sendGridMail.AddAttachment(emailAtatchmentFileName);
        //            }
        //        }
        //        sendGridMail.Subject = emailSubject;
        //        sendGridMail.Text = emailBody;
        //        sendGridMail.Html = emailBody;
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00007000 Before EmailRepository.SaveEmail", "aspNetUserId", aspNetUserId);
        //        EmailRepository emailRepository = new EmailRepository();
        //        MailMessage mailMessage = new MailMessage
        //        {
        //            Body = emailBody,
        //            From = new MailAddress(fromEmailAddress.Key, fromEmailAddress.Value),
        //            IsBodyHtml = true,
        //            Subject = emailSubject.Replace("\r\n", ""),
        //        };
        //        AddToMailAddressCollection(mailMessage.ReplyToList, replyToEmailAddresses, execUniqueId);
        //        AddToMailAddressCollection(mailMessage.To, toEmailAddresses, execUniqueId);
        //        AddToMailAddressCollection(mailMessage.CC, ccEmailAddresses, execUniqueId);
        //        AddToMailAddressCollection(mailMessage.Bcc, bccEmailAddresses, execUniqueId);
        //        if (emailAttachmentFileNames != null)
        //        {
        //            foreach (var emailAtatchmentFileName in emailAttachmentFileNames)
        //            {
        //                System.Net.Mail.Attachment emailAttachment = new System.Net.Mail.Attachment(emailAtatchmentFileName, MediaTypeNames.Application.Octet);
        //                ContentDisposition disposition = emailAttachment.ContentDisposition;
        //                disposition.CreationDate = File.GetCreationTime(emailAtatchmentFileName);
        //                disposition.ModificationDate = File.GetLastWriteTime(emailAtatchmentFileName);
        //                disposition.ReadDate = File.GetLastAccessTime(emailAtatchmentFileName);
        //                disposition.FileName = Path.GetFileName(emailAtatchmentFileName);
        //                disposition.Size = new FileInfo(emailAtatchmentFileName).Length;
        //                disposition.DispositionType = DispositionTypeNames.Attachment;

        //                mailMessage.Attachments.Add(emailAttachment);
        //            }
        //        }

        //        emailRepository.SaveEmail(emailDirectory, aspNetUserId, fromEmailAddress, emailSubject, emailBody, replyToEmailAddresses, toEmailAddresses, execUniqueId, mailMessage, ccEmailAddresses, bccEmailAddresses, emailAttachmentFileNames);
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00008000 :: Before Sending Mail", "EmailSubject", sendGridMail.Subject);
        //        transportWeb.Deliver(sendGridMail);
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00009000 :: After Delivering the Mail", "EmailSubject", sendGridMail.Subject);
        //        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00098000 :: Exit");
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 Error while sending email", exception);
        //        throw;
        //    }
        //}
    }
}
