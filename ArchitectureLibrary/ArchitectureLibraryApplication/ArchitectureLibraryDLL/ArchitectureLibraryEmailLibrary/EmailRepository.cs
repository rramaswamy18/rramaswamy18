using ArchitectureLibraryExtensions;
using ArchitectureLibraryUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;

namespace ArchitectureLibraryEmailLibrary
{
    public class EmailRepository
    {
        public void SaveEmail(string emailDirectory, string aspNetUserId, KeyValuePair<string, string> fromEmailAddress, string emailSubject, string emailBody, List<KeyValuePair<string, string>> replyToEmailAddresses, List<KeyValuePair<string, string>> toEmailAddresses, string execUniqueId, MailMessage mailMessage, List<KeyValuePair<string, string>> ccEmailAddresses = null, List<KeyValuePair<string, string>> bccEmailAddresses = null, List<string> emailAttachmentFileNames = null)
        {
            string databaseConnectionString = Utilities.GetDatabaseConnectionString("DatabaseConnectionString");
            SqlConnection sqlConnection = new SqlConnection(databaseConnectionString);
            sqlConnection.Open();
            //For now do not save email contents in the database. Just save it on disk should the dir and content is available
            long emailDataId = SaveEmailData(aspNetUserId, fromEmailAddress.Key, fromEmailAddress.Value, emailSubject, emailBody, null, sqlConnection, execUniqueId);
            SaveEmailRecipients(emailDataId, replyToEmailAddresses, toEmailAddresses, sqlConnection, execUniqueId, ccEmailAddresses, bccEmailAddresses);
            sqlConnection.Close();
            if (!string.IsNullOrWhiteSpace(emailDirectory))
            {
                byte[] emailContent = mailMessage.ToByteArray();
                if (emailContent != null && emailContent.Length > 0)
                {
                    using (var mailMessageFileStream = new FileStream(emailDirectory + emailDataId + ".eml", FileMode.Create, FileAccess.Write))
                    {
                        mailMessageFileStream.Write(emailContent, 0, emailContent.Length);
                    }
                }
            }
        }
        //public void SaveEmailAdmission(string emailDirectory, string aspnetuserid, KeyValuePair<string, string> fromEmailAddress, string emailSubject, string emailBody, List<KeyValuePair<string, string>> replyToEmailAddresses, List<KeyValuePair<string, string>> toEmailAddresses, string execUniqueId, MailMessage mailMessage, List<KeyValuePair<string, string>> ccEmailAddresses = null, List<KeyValuePair<string, string>> bccEmailAddresses = null, List<string> emailAttachmentFileNames = null)
        //{
        //    //For now do not save email contents in the database. Just save it on disk should the dir and content is available
        //    long emailDataId = SaveEmailDataAdmission(aspnetuserid,fromEmailAddress.Key, fromEmailAddress.Value, emailSubject, emailBody, null, execUniqueId);
        //    if (!string.IsNullOrWhiteSpace(emailDirectory))
        //    {
        //        byte[] emailContent = mailMessage.ToByteArray();
        //        if (emailContent != null && emailContent.Length > 0)
        //        {
        //            using (var mailMessageFileStream = new FileStream(emailDirectory + emailDataId + ".eml", FileMode.Create, FileAccess.Write))
        //            {
        //                mailMessageFileStream.Write(emailContent, 0, emailContent.Length);
        //            }
        //        }
        //        SaveEmailRecipients(emailDataId, replyToEmailAddresses, toEmailAddresses, execUniqueId);
        //    }
        //}
        //public void SaveEmail(string emailAttachmentsDirectory, long loginUserId, string fromEmailAddress, string fromEmailAddressDisplayName, string emailSubject, string emailBody, List<string> replyToEmailAddresses, List<string> toEmailAddresses, List<string> ccEmailAddresses = null, List<string> bccEmailAddresses = null, List<string> emailAttachmentFileNames = null, byte[] emailContent = null)
        //{
        //    long emailDataId = SaveEmailData(loginUserId, fromEmailAddress, fromEmailAddressDisplayName, emailSubject, emailBody, emailContent);
        //    if (!string.IsNullOrWhiteSpace(emailAttachmentsDirectory))
        //    {
        //        if (emailContent != null && emailContent.Length > 0)
        //        {
        //            using (var mailMessageFileStream = new FileStream(emailAttachmentsDirectory + emailDataId + ".eml", FileMode.Create, FileAccess.Write))
        //            {
        //                mailMessageFileStream.Write(emailContent, 0, emailContent.Length);
        //            }
        //        }
        //        SaveEmailRecipients(emailDataId, replyToEmailAddresses, toEmailAddresses, ccEmailAddresses, bccEmailAddresses);
        //    }
        //}
        public long SaveEmailData(string aspNetUserId, string fromEmailAddress, string fromEmailAddressDisplayName, string emailSubject, string emailBody, byte[] emailContent, SqlConnection sqlConnection, string execUniqueId)
        {
            SqlCommand sqlCommand = BuildSqlCommandEmailDataAdd(sqlConnection);
            sqlCommand.Parameters["@AspNetUserId"].Value = aspNetUserId;
            sqlCommand.Parameters["@FromEmailAddress"].Value = fromEmailAddress;
            sqlCommand.Parameters["@FromEmailAddressDisplayName"].Value = fromEmailAddressDisplayName;
            sqlCommand.Parameters["@EmailSubject"].Value = emailSubject;
            sqlCommand.Parameters["@EmailBody"].Value = emailBody;
            sqlCommand.Parameters["@EmailFileName"].Value = ".eml";
            sqlCommand.Parameters["@EmailContent"].Value = emailContent == null ? (object)DBNull.Value : emailContent;
            long emailDataId = (long)sqlCommand.ExecuteScalar();
            return emailDataId;
        }
        //public long SaveEmailData(string aspNetUserId, string fromEmailAddress, string fromEmailAddressDisplayName, string emailHtml, byte[] emailContent, string execUniqueId)
        //{
        //    return -1;
        //    //EmailData emailData = new EmailData
        //    //{
        //    //    AspNetUserId = aspNetUserId,
        //    //    EmailBody = emailHtml,
        //    //    EmailContent = emailContent,
        //    //    EmailSubject = "Subject",
        //    //    FromEmailAddress = fromEmailAddress,
        //    //    FromEmailAddressDisplayName = fromEmailAddressDisplayName,
        //    //};
        //    //ArchitectureDataContext.AddEmailData(emailData, execUniqueId);
        //    //return emailData.EmailDataId;
        //}
        public void SaveEmailRecipients(long emailDataId, List<KeyValuePair<string, string>> replyToEmailAddresses, List<KeyValuePair<string, string>> toEmailAddresses, SqlConnection sqlConnection, string execUniqueId, List<KeyValuePair<string, string>> ccEmailAddresses = null, List<KeyValuePair<string, string>> bccEmailAddresses = null)
        {
            SqlCommand sqlCommand = BuildSqlCommandEmailRecipientAdd(sqlConnection);
            sqlCommand.Parameters["@EmailDataId"].Value = emailDataId;
            float seqNum = 0;
            SaveEmailRecipient(emailDataId, 100, replyToEmailAddresses, sqlCommand, execUniqueId, ref seqNum);
            SaveEmailRecipient(emailDataId, 200, toEmailAddresses, sqlCommand, execUniqueId, ref seqNum);
            SaveEmailRecipient(emailDataId, 300, ccEmailAddresses, sqlCommand, execUniqueId, ref seqNum);
            SaveEmailRecipient(emailDataId, 400, bccEmailAddresses, sqlCommand, execUniqueId, ref seqNum);
        }
        public void SaveEmailRecipient(long emailDataId, long recipientTypeId, List<KeyValuePair<string, string>> emailAddresses, SqlCommand sqlCommand, string execUniqueId, ref float seqNum)
        {
            if (emailAddresses != null && emailAddresses.Count > 0)
            {
                sqlCommand.Parameters["@RecipientTypeId"].Value = recipientTypeId;
                foreach (var emailAddress in emailAddresses)
                {
                    sqlCommand.Parameters["@EmailAddress"].Value = emailAddress.Key;
                    sqlCommand.Parameters["@EmailAddressDisplayName"].Value = emailAddress.Value;
                    sqlCommand.Parameters["@SeqNum"].Value = ++seqNum;
                    sqlCommand.ExecuteNonQuery();
                }
            }
            //if (emailAddresses != null && emailAddresses.Count > 0)
            //{
            //    EmailRecipient emailRecipient;
            //    foreach (var emailAddress in emailAddresses)
            //    {
            //        emailRecipient = new EmailRecipient
            //        {
            //            EmailAddress = emailAddress.Key,
            //            EmailAddressDisplayName = emailAddress.Value,
            //            EmailDataId = emailDataId,
            //            RecipientTypeId = recipientTypeId,
            //            SeqNum = ++seqNum,
            //        };
            //        ArchitectureDataContext.AddEmailRecipient(emailRecipient, execUniqueId);
            //    }
            //}
        }
        private static SqlCommand BuildSqlCommandEmailDataAdd(SqlConnection sqlConnection)
        {
            string sqlStmt = "";
            sqlStmt += "        INSERT ArchLib.EmailData" + Environment.NewLine;
            sqlStmt += "              (" + Environment.NewLine;
            sqlStmt += "               AspNetUserId" + Environment.NewLine;
            sqlStmt += "              ,FromEmailAddress" + Environment.NewLine;
            sqlStmt += "              ,FromEmailAddressDisplayName" + Environment.NewLine;
            sqlStmt += "              ,EmailSubject" + Environment.NewLine;
            sqlStmt += "              ,EmailBody" + Environment.NewLine;
            sqlStmt += "              ,EmailFileName" + Environment.NewLine;
            sqlStmt += "              ,EmailContent" + Environment.NewLine;
            sqlStmt += "              )" + Environment.NewLine;
            sqlStmt += "        OUTPUT INSERTED.EmailDataId" + Environment.NewLine;
            sqlStmt += "        SELECT" + Environment.NewLine;
            sqlStmt += "               @AspNetUserId" + Environment.NewLine;
            sqlStmt += "              ,@FromEmailAddress" + Environment.NewLine;
            sqlStmt += "              ,@FromEmailAddressDisplayName" + Environment.NewLine;
            sqlStmt += "              ,@EmailSubject" + Environment.NewLine;
            sqlStmt += "              ,@EmailBody" + Environment.NewLine;
            sqlStmt += "              ,@EmailFileName" + Environment.NewLine;
            sqlStmt += "              ,@EmailContent" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@AspNetUserId", DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@FromEmailAddress", DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@FromEmailAddressDisplayName", DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@EmailSubject", DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@EmailBody", DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@EmailFileName", DBNull.Value);
            sqlCommand.Parameters.Add("@EmailContent", SqlDbType.VarBinary);
            return sqlCommand;
        }
        private static SqlCommand BuildSqlCommandEmailRecipientAdd(SqlConnection sqlConnection)
        {
            string sqlStmt = "";
            sqlStmt += "        INSERT ArchLib.EmailRecipient" + Environment.NewLine;
            sqlStmt += "              (" + Environment.NewLine;
            sqlStmt += "               EmailDataId" + Environment.NewLine;
            sqlStmt += "              ,SeqNum" + Environment.NewLine;
            sqlStmt += "              ,RecipientTypeId" + Environment.NewLine;
            sqlStmt += "              ,EmailAddress" + Environment.NewLine;
            sqlStmt += "              ,EmailAddressDisplayName" + Environment.NewLine;
            sqlStmt += "              )" + Environment.NewLine;
            sqlStmt += "        OUTPUT INSERTED.EmailRecipientId" + Environment.NewLine;
            sqlStmt += "        SELECT" + Environment.NewLine;
            sqlStmt += "               @EmailDataId" + Environment.NewLine;
            sqlStmt += "              ,@SeqNum" + Environment.NewLine;
            sqlStmt += "              ,@RecipientTypeId" + Environment.NewLine;
            sqlStmt += "              ,@EmailAddress" + Environment.NewLine;
            sqlStmt += "              ,@EmailAddressDisplayName" + Environment.NewLine;
            SqlCommand sqlCommand = new SqlCommand(sqlStmt, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@EmailDataId", DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@SeqNum", DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@RecipientTypeId", DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@EmailAddress", DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@EmailAddressDisplayName", DBNull.Value);
            return sqlCommand;
        }
    }
}
