using ArchitectureLibraryCacheData;
using ArchitectureLibraryEnumerations;
using ArchitectureLibraryModels;
using ArchitectureLibraryTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ArchitectureLibraryBusinessLayer
{
    public partial class ArchLibBL
    {
        public void CopyReponseObjectToModelErrors(ModelStateDictionary modelStateDictionary, List<KeyValuePair<string, List<string>>> propertyErrorsKVP, List<string> responseMessages)
        {
            if (propertyErrorsKVP != null)
            {
                foreach (var propertyErrorKVP in propertyErrorsKVP)
                {
                    foreach (var propertyErrorKVPValue in propertyErrorKVP.Value)
                    {
                        modelStateDictionary.AddModelError(propertyErrorKVP.Key, propertyErrorKVPValue);
                        modelStateDictionary.AddModelError("", propertyErrorKVPValue);
                    }
                }
            }
            if (responseMessages != null)
            {
                foreach (var responseMessage in responseMessages)
                {
                    modelStateDictionary.AddModelError("", responseMessage);
                }
            }
            return;
        }
        public ResponseObjectModel CreateSystemError(long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            ResponseObjectModel responseObjectModel = new ResponseObjectModel
            {
                ColumnCount = 1,
                ListStyleType = "decimal",
                ResponseMessages = new List<string>
                {
                    "System error occurred while processing",
                    "Please contact support personnel",
                },
                ResponseTypeId = ResponseTypeEnum.Error,
                TextAlign = "left",
                TextColor = "#ff0000",
            };
            return responseObjectModel;
        }
        public void CreateSystemError(ModelStateDictionary modelStateDictionary, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            modelStateDictionary.AddModelError("", "System error occurred while processing");
            modelStateDictionary.AddModelError("", "Please contact support personnel");
            return;
        }
        public string GenerateRandomKey(int keyLength)
        {
            string randomKey = "";
            string randomChar;
            string[] upperCaseChars = new string[26];
            string[] lowerCaseChars = new string[26];
            int[] numbers = new int[10];
            string[] specialChars = new string[] { "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "+" };
            int i;
            int randomNumber;
            Random randomNumberObject = new Random();
            Random randomValueObject = new Random();
            for (i = 0; i < 26; i++)
            {
                upperCaseChars[i] = Convert.ToChar(i + 65).ToString();
                lowerCaseChars[i] = Convert.ToChar(i + 97).ToString();
            }
            for (i = 0; i < 10; i++)
            {
                numbers[i] = i;
            }
            for (i = 0; i < keyLength; i++)
            {
                randomNumber = randomNumberObject.Next(0, 3);
                switch (randomNumber)
                {
                    case 0: //Upper Case
                        randomNumber = randomValueObject.Next(0, 25);
                        randomChar = upperCaseChars[randomNumber];
                        break;
                    case 1: //Lower Case
                        randomNumber = randomValueObject.Next(0, 25);
                        randomChar = lowerCaseChars[randomNumber];
                        break;
                    case 2: //Number
                        randomNumber = randomValueObject.Next(0, 9);
                        randomChar = numbers[randomNumber].ToString();
                        break;
                    case 3: //Special Char
                        randomNumber = randomValueObject.Next(0, specialChars.Length - 1);
                        randomChar = specialChars[randomNumber];
                        break;
                    default:
                        randomChar = "";
                        break;
                }
                randomKey = randomKey + randomChar;
            }
            return randomKey;
        }
        public void GenerateCaptchaQuesion(HttpSessionStateBase sessionObject, string number0Session, string number1Session)
        {
            int captchaQuestion0, captchaQuestion1;
            Random random = new Random();
            captchaQuestion0 = random.Next(1, 11);
            captchaQuestion1 = random.Next(1, 11);
            sessionObject[number0Session] = captchaQuestion0;
            sessionObject[number1Session] = captchaQuestion1;
        }
        public void GenerateCaptchaQuesion(HttpSessionStateBase sessionObject, List<string> numberSessions)
        {
            Random random = new Random();
            int captchaQuestion;
            foreach (var numberSession in numberSessions)
            {
                captchaQuestion = random.Next(1, 11);
                sessionObject[numberSession] = captchaQuestion;
            }
        }
        public void MergeModelStateErrorMessages(ModelStateDictionary modelStateDictionary)
        {
            int i;
            string errorMessage;
            Dictionary<string, string> dictionaryErrorMessages = new Dictionary<string, string>();
            foreach (var modelState in modelStateDictionary)
            {
                if (modelState.Key != "")
                {
                    if (modelState.Value.Errors != null && modelState.Value.Errors.Count > 0)
                    {
                        errorMessage = modelState.Value.Errors[0].ErrorMessage;
                        if (modelState.Value.Errors.Count > 1)
                        {
                            for (i = 1; i < modelState.Value.Errors.Count; i++)
                            {
                                errorMessage += "<br />" + modelState.Value.Errors[i].ErrorMessage;
                            }
                            dictionaryErrorMessages[modelState.Key] = errorMessage;
                        }
                    }
                }
            }
            foreach (var dictionaryErrorMessage in dictionaryErrorMessages)
            {
                modelStateDictionary.Remove(dictionaryErrorMessage.Key);
                modelStateDictionary.AddModelError(dictionaryErrorMessage.Key, dictionaryErrorMessage.Value);
            }
        }
        public string SignatureTemplate(long clientId)
        {
            string templateString, templateData;
            Dictionary<string, string> keywordValues = new Dictionary<string, string>
            {
                { "@@##AdminRepresentativeName##@@", ArchLibCache.GetApplicationDefault(clientId, "AdminRepresentativeName", "") },
                { "@@##AdminRepresentativeTitle##@@", ArchLibCache.GetApplicationDefault(clientId, "AdminRepresentativeTitle", "") },
                { "@@##OrganizationName##@@", ArchLibCache.GetApplicationDefault(clientId, "BusinessName", "") },
                { "@@##AddressLine1##@@", ArchLibCache.GetApplicationDefault(clientId, "AddressLine1", "") },
                { "@@##AddressCityName##@@", ArchLibCache.GetApplicationDefault(clientId, "AddressCityName", "") },
                { "@@##AddressStateAbbrev##@@", ArchLibCache.GetApplicationDefault(clientId, "AddressStateAbbrev", "") },
                { "@@##AddressZipCode##@@", ArchLibCache.GetApplicationDefault(clientId, "AddressZipCode", "") },
                { "@@##BaseUrl##@@", ArchLibCache.GetApplicationDefault(clientId, "BaseUrl", "") },
                //{ "@@##UpdatePasswordUrl##@@", SchoolPrdCacheData.ArchLibCache.GetApplicationDefault(0, "UpdatePasswordUrl", "") },
                { "@@##ContactPhone##@@", ArchLibCache.GetApplicationDefault(clientId, "ContactPhone", "") },
                { "@@##ContactFax##@@", "" },
                { "@@##EmailAddress##@@", ArchLibCache.GetApplicationDefault(clientId, "BusinessEmail", "") },
                { "@@##CurrentDateTime##@@", DateTime.Now.ToString("MMM-dd-yyyy hh:mm tt") },
            };
            TemplateBL templateBL = new TemplateBL();
            templateString = templateBL.GetTemplateString("SignatureTemplate");
            templateData = templateBL.PopulateStringTemplate(templateString, keywordValues);
            return templateData;
        }
        public bool ValidateCaptcha(HttpSessionStateBase sessionObject, string number0Session, string number1Session, string answerInput)
        {
            var returnValue = true;
            try
            {
                int number1 = int.Parse(sessionObject[number0Session].ToString());
                int number2 = int.Parse(sessionObject[number1Session].ToString());
                int answer = int.Parse(answerInput);
                if (number1 + number2 == answer)
                {
                    ;
                }
                else
                {
                    returnValue = false;
                }
            }
            catch
            {
                returnValue = false;
            }
            return returnValue;
        }
        //public bool ValidateEmailAddress(string emailAddress)
        //{
        //    bool returnValue = false;
        //    if (!string.IsNullOrWhiteSpace(emailAddress))
        //    {
        //        var emailAddressTrimmed = emailAddress.Trim();
        //        if (emailAddressTrimmed.EndsWith("."))
        //        {
        //        }
        //        else
        //        {
        //            try
        //            {
        //                var mailAddress = new System.Net.Mail.MailAddress(emailAddressTrimmed);
        //                returnValue = true;
        //            }
        //            catch
        //            {
        //            }
        //        }
        //    }
        //    else
        //    {
        //        returnValue = true; //On purpose - this is handled by the caller
        //    }
        //    return returnValue;
        //}
        //public string ValidateEmailAddress(string inputData, bool mandatory, string mandatoryErrorMessage, string invalidErrorMessage, List<string> propertyErrors, List<string> propertiesErrors, out bool returnValue)
        //{
        //    if (string.IsNullOrWhiteSpace(inputData))
        //    {
        //        returnValue = mandatory;
        //        if (mandatory)
        //        {
        //            propertyErrors.Add(mandatoryErrorMessage);
        //            propertiesErrors.Add(mandatoryErrorMessage);
        //            returnValue = false;
        //        }
        //    }
        //    else
        //    {
        //        var trimmedEmail = inputData.Trim();
        //        if (inputData.EndsWith("."))
        //        {
        //            propertyErrors.Add(invalidErrorMessage);
        //            propertiesErrors.Add(invalidErrorMessage);
        //            returnValue = false;
        //            //suggested by @TK-421
        //        }
        //        try
        //        {
        //            var emailAddress = new System.Net.Mail.MailAddress(inputData);
        //            if (emailAddress.Address == trimmedEmail)
        //            {
        //                returnValue = true;
        //            }
        //            else
        //            {
        //                propertyErrors.Add(invalidErrorMessage);
        //                propertiesErrors.Add(invalidErrorMessage);
        //                returnValue = false;
        //            }
        //            //return addr.Address == trimmedEmail;
        //        }
        //        catch
        //        {
        //            propertyErrors.Add(invalidErrorMessage);
        //            propertiesErrors.Add(invalidErrorMessage);
        //            returnValue = false;
        //        }
        //    }
        //    return inputData;
        //}
        //public string ValidateExists(string inputData, bool mandatory, string mandatoryErrorMessage, List<string> propertyErrors, List<string> propertiesErrors, out bool returnValue)
        //{
        //    if (string.IsNullOrWhiteSpace(inputData))
        //    {
        //        returnValue = mandatory;
        //        if (mandatory)
        //        {
        //            propertyErrors.Add(mandatoryErrorMessage);
        //            propertiesErrors.Add(mandatoryErrorMessage);
        //            returnValue = false;
        //        }
        //    }
        //    else
        //    {
        //        returnValue = true;
        //    }
        //    return inputData;
        //}
        //public string ValidateNumericData(string inputData, bool mandatory, int minLength, int maxLength, string mandatoryErrorMessage, string lengthErrorMessage, string numericErrorMessage, List<string> propertyErrors, List<string> propertiesErrors, out bool returnValue)
        //{
        //    if (string.IsNullOrWhiteSpace(inputData))
        //    {
        //        returnValue = mandatory;
        //        if (mandatory)
        //        {
        //            propertyErrors.Add(mandatoryErrorMessage);
        //            propertiesErrors.Add(mandatoryErrorMessage);
        //            returnValue = false;
        //        }
        //    }
        //    else
        //    {
        //        inputData = inputData.Trim();
        //        if (inputData.Length >= minLength && inputData.Length <= maxLength)
        //        {
        //            try
        //            {
        //                var temp = long.Parse(inputData);
        //                returnValue = true;
        //            }
        //            catch
        //            {
        //                propertyErrors.Add(numericErrorMessage);
        //                propertiesErrors.Add(numericErrorMessage);
        //                returnValue = false;
        //            }
        //        }
        //        else
        //        {
        //            propertyErrors.Add(lengthErrorMessage);
        //            propertiesErrors.Add(lengthErrorMessage);
        //            returnValue = false;
        //        }
        //    }
        //    return inputData;
        //}
        //public string ValidateStringData(string inputData, bool mandatory, int minLength, int maxLength, string mandatoryErrorMessage, string lengthErrorMessage, List<string> propertyErrors, List<string> propertiesErrors, out bool returnValue)
        //{
        //    if (string.IsNullOrWhiteSpace(inputData))
        //    {
        //        returnValue = mandatory;
        //        if (mandatory)
        //        {
        //            propertyErrors.Add(mandatoryErrorMessage);
        //            propertiesErrors.Add(mandatoryErrorMessage);
        //            returnValue = false;
        //        }
        //    }
        //    else
        //    {
        //        inputData = inputData.Trim();
        //        if (inputData.Length >= minLength && inputData.Length <= maxLength)
        //        {
        //            returnValue = true;
        //        }
        //        else
        //        {
        //            propertyErrors.Add(lengthErrorMessage);
        //            propertiesErrors.Add(lengthErrorMessage);
        //            returnValue = false;
        //        }
        //    }
        //    return inputData;
        //}
    }
}
