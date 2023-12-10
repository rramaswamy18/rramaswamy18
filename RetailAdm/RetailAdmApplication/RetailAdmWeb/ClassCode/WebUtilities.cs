using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RetailAdmWeb.ClassCode
{
    public static class WebUtilities
    {
        public static void CopyReponseMessageToModelErrors(ModelStateDictionary modelStateDictionary, List<KeyValuePair<string, string>> responseMessages)
        {
            foreach (var responseMessage in responseMessages)
            {
                modelStateDictionary.AddModelError(responseMessage.Key, responseMessage.Value);
            }
            return;
        }
        public static List<KeyValuePair<string, string>> CopyModelErrorsToReponseMessage(System.Web.Http.ModelBinding.ModelStateDictionary modelStateDictionary, List<KeyValuePair<string, string>> responseMessages)
        {
            if (responseMessages == null)
            {
                responseMessages = new List<KeyValuePair<string, string>>();
            }
            foreach (var keyValuePair in modelStateDictionary)
            {
                var key = keyValuePair.Key;
                var valueErrors = keyValuePair.Value.Errors;
                foreach (var valueError in valueErrors)
                {
                    responseMessages.Add(new KeyValuePair<string, string>(key, valueError.ErrorMessage));
                }
            }
            return responseMessages;
        }
        public static List<KeyValuePair<string, string>> CopyModelErrorsToReponseMessage2(ModelStateDictionary modelStateDictionary, List<KeyValuePair<string, string>> responseMessages)
        {
            if (responseMessages == null)
            {
                responseMessages = new List<KeyValuePair<string, string>>();
            }
            foreach (var keyValuePair in modelStateDictionary)
            {
                var key = keyValuePair.Key;
                var valueErrors = keyValuePair.Value.Errors;
                foreach (var valueError in valueErrors)
                {
                    responseMessages.Add(new KeyValuePair<string, string>(key, valueError.ErrorMessage));
                }
            }
            return responseMessages;
        }
        public static List<KeyValuePair<string, string>> CopyModelErrorsToReponseMessage(IEnumerable<string> errors, List<KeyValuePair<string, string>> responseMessages)
        {
            if (responseMessages == null)
            {
                responseMessages = new List<KeyValuePair<string, string>>();
            }
            if (errors != null)
            {
                foreach (string error in errors)
                {
                    responseMessages.Add(new KeyValuePair<string, string>("", error));
                }
            }
            return responseMessages;
        }
    }
}
