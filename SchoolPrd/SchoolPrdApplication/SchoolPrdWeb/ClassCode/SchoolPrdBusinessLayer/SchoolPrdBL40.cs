using ArchitectureLibraryBusinessLayer;
using ArchitectureLibraryCacheData;
using ArchitectureLibraryEnumerations;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryPDFLibrary;
using ArchitectureLibraryTemplate;
using ArchitectureLibraryUtility;
using SchoolPrdCacheData;
using SchoolPrdModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SchoolPrdBusinessLayer
{
    public partial class SchoolPrdBL
    {
        private string BuildClassFeesDetailRows(List<ClassFeesModel> classFeesModels, out short totalFeesAmount, out short totalDiscountAmount)
        {
            //short feesAmount, discountAmount;
            string classFeesDetailRow, classFeesDetailRowWithData = "";
            string templatesDirectoryName = Utilities.GetApplicationValue("TemplatesDirectoryName");
            StreamReader streamReader;
            streamReader = new StreamReader(templatesDirectoryName + "ClassFeesDetailRow.html");
            string classFeesDetailRowTemplate = streamReader.ReadToEnd();
            streamReader.Close();
            totalFeesAmount = 0;
            totalDiscountAmount = 0;
            foreach (var classFeesModel in classFeesModels)
            {
                //feesAmount = classFeesModel.FeesAmount == null ? (short)0 : classFeesModel.FeesAmount.Value;
                //discountAmount = classFeesModel.DiscountAmount == null ? (short)0 : classFeesModel.DiscountAmount.Value;
                classFeesDetailRow = classFeesDetailRowTemplate;
                classFeesDetailRow = classFeesDetailRow.Replace("@@##ClassFeesDesc##@@", classFeesModel.ClassFeesDesc);
                //classFeesDetailRow = classFeesDetailRow.Replace("@@##FeesAmount##@@", feesAmount.ToString("c0"));
                //classFeesDetailRow = classFeesDetailRow.Replace("@@##DiscountAmount##@@", discountAmount.ToString("c0"));
                classFeesDetailRowWithData += classFeesDetailRow;
                //totalFeesAmount += feesAmount;
                //totalDiscountAmount += discountAmount;
            }
            return classFeesDetailRowWithData;
        }
        private Dictionary<string, string> BuildAgreementEmailTemplateKeywordValues(ClassEnrollModel classEnrollModel, SessionObjectModel sessionObjectModel, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            ArchLibBL archLibBL = new ArchLibBL();
            Dictionary<string, string> keywordValues = new Dictionary<string, string>
            {
                { "@@##FirstName##@@", sessionObjectModel.FirstName },
            };
            keywordValues["@@##LastName##@@"] = sessionObjectModel.LastName;
            keywordValues["@@##StudentEmailAddress##@@"] = sessionObjectModel.EmailAddress;
            keywordValues["@@##StudentPhoneNumber##@@"] = long.Parse(sessionObjectModel.PhoneNumber).ToString("(###) ###-####");
            keywordValues["@@##ClassListDesc##@@"] = classEnrollModel.ClassScheduleModel.ClassSessionModel.ClassListModel.ClassListDesc;
            keywordValues["@@##StartDate##@@"] = classEnrollModel.ClassScheduleModel.StartDate;
            keywordValues["@@##GraduationDate##@@"] = classEnrollModel.ClassScheduleModel.GraduationDate;
            keywordValues["@@##ClassScheduleDesc##@@"] = classEnrollModel.ClassScheduleModel.ClassScheduleDesc;
            keywordValues["@@##ClassSessionDesc##@@"] = classEnrollModel.ClassScheduleModel.ClassSessionModel.ClassSessionDesc;
            keywordValues["@@##ReportingTime##@@"] = classEnrollModel.ClassScheduleModel.ClassSessionModel.ClassDetailModels[0].StartTime;
            keywordValues["@@##StartDateAndDay##@@"] = classEnrollModel.ClassScheduleModel.StartDate;
            keywordValues["@@##ClassFees##@@"] = BuildClassFeesDetailRows(classEnrollModel.ClassScheduleModel.ClassSessionModel.ClassListModel.ClassFeesModels, out short totalFeesAmount, out short totalDiscountAmount);
            keywordValues["@@##TotalAmount##@@"] = (totalFeesAmount - totalDiscountAmount).ToString("c0");
            keywordValues["@@##TotalFeesAmount##@@"] = totalFeesAmount.ToString("c0");
            keywordValues["@@##TotalDiscountAmount##@@"] = totalDiscountAmount.ToString("c0");
            if (classEnrollModel.PersonModel.CertificateDocumentId > 0)
            {
                keywordValues["@@##CertificateDocument##@@"] = "We will use the attached certificate in your agreement.";
            }
            else
            {
                keywordValues["@@##CertificateDocument##@@"] = "<b>According to our records, you have not uploaded the high school diploma. Please upload the high school diploma or contact us for alternatives</b>";
            }
            keywordValues["@@##ContactPhone##@@"] = ArchLibCache.GetApplicationDefault(clientId, "ContactPhone", "");
            keywordValues["@@##BaseUrl##@@"] = ArchLibCache.GetApplicationDefault(clientId, "BaseUrl", "");
            keywordValues["@@##ContactUsUrl##@@"] = ArchLibCache.GetApplicationDefault(0, "ContactUsUrl", "");
            keywordValues["@@##SignatureTemplate##@@"] = archLibBL.SignatureTemplate(clientId);
            return keywordValues;
        }
    }
}
