using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryUtility
{
    public static class RestAPI
    {
        #region REST Methods
        public static HttpResponseMessage GetAccessToken(string restAPIRootUri, string grantType, string userName, string password)
        {
            HttpResponseMessage httpResponseMessage;
            var parms = new Dictionary<string, string>();
            parms.Add("grant_type", grantType);
            parms.Add("username", userName);
            parms.Add("password", password);
            using (HttpClient client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Add("key", "value"); //For syntax and if needed for other purposes

                httpResponseMessage = client.PostAsync(restAPIRootUri + "token", new FormUrlEncodedContent(parms)).Result;
                //securityTokenJson = response.Content.ReadAsStringAsync().Result;
            }
            return httpResponseMessage;
        }
        public static HttpResponseMessage CallRESTServiceGet(string restAPIRootUri, string requestUri, string queryString, string authorizationKey = null, string authorizationValue = null)
        {
            string webAPIURL = restAPIRootUri + requestUri + queryString;
            HttpResponseMessage httpResponseMessage;
            using (HttpClient client = new HttpClient())
            {
                if (authorizationKey != null && authorizationValue != null)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authorizationKey, authorizationValue);
                }
                httpResponseMessage = client.GetAsync(webAPIURL).Result;
            }
            return httpResponseMessage;
        }
        public static HttpResponseMessage CallRESTServicePost(string restAPIRootUri, string requestUri, string queryString, string authorizationKey = null, string authorizationValue = null, string contentType = null, string contentData = null)
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

            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri(restApiRoot);
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //HttpResponseMessage httpResponseMessage = client.PostAsJsonAsync(webAPIURL, inputObject).Result;
            //responseData = httpResponseMessage.Content.ReadAsStringAsync().Result;

            //return responseData;
        }
        #endregion

        #region REST Data function
        //public static string DownloadRestFulDataGet(string restApiRoot, string webAPIURL, string queryString, string methodTypeNameDesc, string contentType)
        //{
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(restApiRoot + webAPIURL + queryString);
        //    request.Method = methodTypeNameDesc;
        //    request.ContentType = contentType;
        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //    StreamReader reader = new StreamReader(response.GetResponseStream());
        //    string str = reader.ReadToEnd();
        //    reader.Close();
        //    return str;
        //}

        //public static string DownloadRestFulDataPutPost(string _requestApiUri, string _requestApiController, string _methodTypeNameDesc, string _contentType, string _jsonInputData)
        //{
        //    string str;
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_requestApiUri + _requestApiController);
        //    request.Method = _methodTypeNameDesc;
        //    request.ContentType = _contentType;
        //    StreamWriter writer = new StreamWriter(request.GetRequestStream());
        //    writer.Write(_jsonInputData);
        //    writer.Close();
        //    try
        //    {
        //        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //        StreamReader reader = new StreamReader(response.GetResponseStream());
        //        str = reader.ReadToEnd();
        //        reader.Close();
        //    }
        //    catch (Exception exception)
        //    {
        //        Console.WriteLine(exception.Message);
        //        throw;
        //    }
        //    return str;
        //}

        //public static string DownloadRestFulDataPutPost<T>(string _requestApiUri, string _requestApiController, string _methodTypeNameDesc, string _contentType, T _inputObjectInstance)
        //{
        //    string str = JsonConvert.SerializeObject(_inputObjectInstance);
        //    return DownloadRestFulDataPutPost(_requestApiUri, _requestApiController, _methodTypeNameDesc, _contentType, str);
        //}
        #endregion

        #region
        //public static string DownloadRestFulDataPutPost(string restAPIRootUri, string grantType, string userName, string password, bool validatePassword)
        //{
        //    string jsonInputData, responseData;
        //    System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(restAPIRootUri + "token?grant_type=password&username=test1@hotmail.com&password=password1");
        //    request.Method = "POST";
        //    request.ContentType = "application/json";
        //    request.ContentType = "application/x-www-form-urlencoded";
        //    StreamWriter writer = new StreamWriter(request.GetRequestStream());
        //    //jsonInputData = "";
        //    //jsonInputData += "{";
        //    //jsonInputData += "\"grant_type\": \"" + grantType + "\", \"userName\": \"" + userName + "\", \"password\": " + password + "\", \"validatePassword\": \"" + validatePassword + "\"";
        //    //jsonInputData += "}";
        //    //writer.Write(jsonInputData);
        //    writer.Close();
        //    try
        //    {
        //        System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
        //        StreamReader reader = new StreamReader(response.GetResponseStream());
        //        responseData = reader.ReadToEnd();
        //        reader.Close();
        //    }
        //    catch (Exception exception)
        //    {
        //        Console.WriteLine(exception.Message);
        //        throw;
        //    }
        //    return responseData;
        //}
        //public static T CallRESTServiceGet<T>(string restApiRoot, string webAPIURL, string queryString, string mediaType)
        //{
        //    //var handler = new HttpClientHandler()
        //    //{
        //    //    Proxy = HttpWebRequest.GetSystemWebProxy()
        //    //};
        //    //HttpClient client = new HttpClient(handler);
        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri(restApiRoot);
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
        //    //client.DefaultRequestHeaders.Add("Accept", mediaType);

        //    HttpResponseMessage httpResponse = client.GetAsync(webAPIURL + queryString).Result;

        //    dynamic responseData = httpResponse.Content.ReadAsAsync<dynamic>().Result;

        //    return responseData;
        //}
        //public static T CallRESTServicePost<T>(string restApiRoot, string webAPIURL, object inputObject)
        //{
        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri(restApiRoot);
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //    HttpResponseMessage httpResponse = client.PostAsJsonAsync(webAPIURL, inputObject).Result;

        //    dynamic responseData = httpResponse.Content.ReadAsAsync<dynamic>().Result;

        //    return responseData;
        //}
        //public static string CallRESTServicePost(string restApiRoot, string webAPIURL, object inputObject)
        #endregion
    }
}
