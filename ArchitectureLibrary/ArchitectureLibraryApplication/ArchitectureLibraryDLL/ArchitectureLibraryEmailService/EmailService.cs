using ArchitectureLibraryCacheData;
using ArchitectureLibraryEmailLibrary;
using ArchitectureLibraryException;
using ArchitectureLibraryUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ArchitectureLibraryEmailService
{
    public class EmailService
    {
        public void SendEmail(string emailServiceType, string emailDirectory, string aspNetUserId, KeyValuePair<string, string> fromEmailAddress, string emailSubject, string emailBody, List<KeyValuePair<string, string>> toEmailAddresses, string ipAddress, string execUniqueId, string loggedInUserId, string privateKey = "", List<KeyValuePair<string, string>> replyToEmailAddresses = null, List<KeyValuePair<string, string>> ccEmailAddresses = null, List<KeyValuePair<string, string>> bccEmailAddresses = null, List<string> emailAttachmentFileNames = null, bool pickupDirectory = true, string smtpClientHost = null, int? smtpPort = null, bool? smtpClientEnableSsl = null, string networkUsername = null, string networkPassword = null)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter", toEmailAddresses, "aspNetUserId", aspNetUserId);
            try
            {
                if (string.IsNullOrWhiteSpace(fromEmailAddress.Key))
                {
                    exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00001000 :: From Email Address Override", "Key", fromEmailAddress.Key, "Value", fromEmailAddress.Value);
                    foreach (var ccEmailAddress in ccEmailAddresses)
                    {
                        if (string.IsNullOrWhiteSpace(ccEmailAddress.Key))
                        {
                            ccEmailAddresses.Remove(ccEmailAddress);
                            break;
                        }
                    }
                    ccEmailAddresses.Add(new KeyValuePair<string, string>(fromEmailAddress.Key, fromEmailAddress.Value));
                }
                if (replyToEmailAddresses == null)
                {
                    replyToEmailAddresses = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>(fromEmailAddress.Key, fromEmailAddress.Value)
                    };
                }
                switch (emailServiceType)
                {
                    case "SENDGRID":
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00002000 Before SendGridEmailService.SendEmail", toEmailAddresses, "aspNetUserId", aspNetUserId);
                        SendGridEmailService sendGridEmailService = new SendGridEmailService();
                        sendGridEmailService.SendEmail(emailDirectory, aspNetUserId, fromEmailAddress, replyToEmailAddresses, emailSubject, emailBody, toEmailAddresses, ipAddress, execUniqueId, loggedInUserId, privateKey, ccEmailAddresses, bccEmailAddresses, emailAttachmentFileNames);
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00003000 After SendGridEmailService.SendEmail", toEmailAddresses, "aspNetUserId", aspNetUserId);
                        break;
                    case "GMAIL":
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00004000 Before GmailService.SendEmail", toEmailAddresses, "aspNetUserId", aspNetUserId);
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00005000 After GmailService.SendEmail", toEmailAddresses, "aspNetUserId", aspNetUserId);
                        break;
                    case "SMTP":
                    default:
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00006000 Before SmtpEmailService.SendEmail", toEmailAddresses, "aspNetUserId", aspNetUserId);
                        SmtpEmailService smtpEmailService = new SmtpEmailService();
                        smtpEmailService.SendEmail(emailDirectory, aspNetUserId, fromEmailAddress, emailSubject, emailBody, replyToEmailAddresses, toEmailAddresses, ipAddress, execUniqueId, loggedInUserId, ccEmailAddresses, bccEmailAddresses, emailAttachmentFileNames, pickupDirectory, smtpClientHost, smtpPort, smtpClientEnableSsl, networkUsername, networkPassword);
                        exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00007000 After SmtpEmailService.SendEmail", toEmailAddresses, "aspNetUserId", aspNetUserId);
                        break;
                }
                exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 End SendEmail", toEmailAddresses, "aspNetUserId", aspNetUserId, "fromEmailAddress", fromEmailAddress.Key);
            }
            catch (Exception exception)
            {
                exceptionLogger.LogError(methodName, Utilities.GetCallerLineNumber(), "00099000 :: Exception", exception);
            }
        }
    }
}
