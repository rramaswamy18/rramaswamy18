using SVCCTempleModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SVCCTempleWeb.ClassCode
{
    public class WebUtilities
    {
        public ResponseObjectModel0 InitializeResponseObjectModel(ResponseObjectModel0 responseObjectModel = null)
        {
            if (responseObjectModel == null)
            {
                responseObjectModel = new ResponseObjectModel0
                {
                    //ResponseDescription = "",
                    ResponseMessages = new List<KeyValuePair<string, string>>(),
                    StatusCode = HttpStatusCode.OK,
                };
            }
            else
            {
                if (responseObjectModel.ResponseMessages == null)
                {
                    responseObjectModel = new ResponseObjectModel0
                    {
                        //ResponseDescription = "",
                        ResponseMessages = new List<KeyValuePair<string, string>>(),
                        StatusCode = HttpStatusCode.OK,
                    };
                }
            }
            return responseObjectModel;
        }
        public void CopyReponseMessageToModelErrors(ModelStateDictionary modelStateDictionary, List<KeyValuePair<string, string>> responseMessages)
        {
            foreach (var responseMessage in responseMessages)
            {
                modelStateDictionary.AddModelError(responseMessage.Key, responseMessage.Value);
            }
            return;
        }
        public List<KeyValuePair<string, string>> CopyModelErrorsToReponseMessage(System.Web.Http.ModelBinding.ModelStateDictionary modelStateDictionary, List<KeyValuePair<string, string>> responseMessages)
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
        public List<KeyValuePair<string, string>> CopyModelErrorsToReponseMessage2(ModelStateDictionary modelStateDictionary, List<KeyValuePair<string, string>> responseMessages)
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
        public List<KeyValuePair<string, string>> CopyModelErrorsToReponseMessage(IEnumerable<string> errors, List<KeyValuePair<string, string>> responseMessages)
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
        public List<KeyValuePair<string, List<string>>> CopyModelErrorsToReponseMessagesError(ModelStateDictionary modelStateDictionary, List<KeyValuePair<string, List<string>>> responseMessagesError = null)
        {
            if (responseMessagesError == null)
            {
                responseMessagesError = new List<KeyValuePair<string, List<string>>>();
            }
            List<string> errorMessages;
            foreach (var keyValuePair in modelStateDictionary)
            {
                var key = keyValuePair.Key;
                responseMessagesError.Add(new KeyValuePair<string, List<string>>(keyValuePair.Key, errorMessages = new List<string>()));
                var valueErrors = keyValuePair.Value.Errors;
                foreach (var valueError in valueErrors)
                {
                    errorMessages.Add(valueError.ErrorMessage);
                }
            }
            return responseMessagesError;
        }
    }
}
