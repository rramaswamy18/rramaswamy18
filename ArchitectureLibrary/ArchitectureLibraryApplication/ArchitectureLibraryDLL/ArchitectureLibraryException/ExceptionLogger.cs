/*
1.	Exception Logger supports both Application Logging and Error Handling
2.	Idea is to provide key information of the flow and when an error occurs
3.	Error can be system exception or user exception
4.	User row not found is an example of user exception
5.	To make things easier for debugging the below information and more is captured
a.	Application Name
b.	Calling Assembly Name
c.	Executing Assembly Name
d.	Caller Namespace
e.	Caller Member Name
f.	Caller Full File Name
6.	The above information is passed when the Exception Logger is instantiated
7.	It is recommended to create a new instance in each method
8.	At various levels Key Value Pairs are passed and maintained in Exception Logger
9.	The keyword Username and the value Email Address is an example of KVP
10.	When the Log method is called – the KVP are output as JSON object
11.	You can continue and build on top the KVP or erase and start new
12.	The base information as described above like Application Name till Caller Full File Name is maintained for the life of this instance
13. Useful link https://stackify.com/log4net-guide-dotnet-logging/
14. https://www.codeproject.com/Articles/20526/Have-Fun-Again-With-Custom-Attributes-Part-1
    Refer to the above to make the parameters being passed generic using PostSharp - Don't know what this is
*/

using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryException
{
    public class ExceptionLogger
    {
        #region
        private static readonly ILog logger_ = LogManager.GetLogger(System.Environment.MachineName);
        private string ipAddress_, applicationName_, execUniqueId_, loggedInUserId_, callingAssemblyName_, executingAssemblyName_, currentMethodDeclaringType_;
        public List<KeyValuePair<string, string>> ExceptionParmKVPs { set; get; }
        //private string exceptionDataJson;
        public ExceptionLogger(string applicationName, string ipAddress, string execUniqueId, string loggedInUserId, string callingAssemblyName, string executingAssemblyName, string currentMethodDeclaringType)
        {
            applicationName_ = applicationName;
            ipAddress_ = ipAddress;
            execUniqueId_ = execUniqueId;
            loggedInUserId_ = loggedInUserId;
            callingAssemblyName_ = callingAssemblyName;
            executingAssemblyName_ = executingAssemblyName;
            currentMethodDeclaringType_ = currentMethodDeclaringType;
            //log4net.LogicalThreadContext.Properties["ApplicationName"] = applicationName;
            //log4net.LogicalThreadContext.Properties["ExecUniqueId"] = execUniqueId;
            //log4net.LogicalThreadContext.Properties["CallingAssemblyName"] = callingAssemblyName;
            //log4net.LogicalThreadContext.Properties["ExecutingAssemblyName"] = executingAssemblyName;
            //log4net.LogicalThreadContext.Properties["CurrentMethodDeclaringType"] = currentMethodDeclaringType;

            //if (GlobalContext.Properties["ExecUniqueId"] == null)
            //{
            //    GlobalContext.Properties["ExecUniqueId"] = execUniqueId;
            //}
            //List<KeyValuePair<string, string>> exceptionDataKVPs = new List<KeyValuePair<string, string>>()
            //{
            //    new KeyValuePair<string, string>("applicationName", applicationName),
            //    new KeyValuePair<string, string>("execUniqueId", execUniqueId),
            //    new KeyValuePair<string, string>("callingAssemblyFullName", callingAssemblyName),
            //    new KeyValuePair<string, string>("executingAssemblyFullName", executingAssemblyName),
            //    new KeyValuePair<string, string>("currentMethodDeclaringType", currentMethodDeclaringType),
            //};
            //exceptionDataJson = JsonConvert.SerializeObject(exceptionDataKVPs);
        }
        public void Initialize()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
        #endregion
        #region Exception Logging
        public void LogException(ExceptionTypeEnum exceptionType, string methodName, int callerLineNumber, string message, List<KeyValuePair<string, string>> paramsKVPs = null, Exception exception = null, params string[] paramsArray)
        {
            switch (exceptionType)
            {
                case ExceptionTypeEnum.Debug:
                    if (paramsKVPs == null)
                    {
                        LogDebug(methodName, callerLineNumber, message, paramsArray);
                    }
                    else
                    {
                        LogDebug(methodName, callerLineNumber, message, paramsKVPs, paramsArray);
                    }
                    break;
                case ExceptionTypeEnum.Info:
                    if (paramsKVPs == null)
                    {
                        LogInfo(methodName, callerLineNumber, message, paramsArray);
                    }
                    else
                    {
                        LogInfo(methodName, callerLineNumber, message, paramsKVPs, paramsArray);
                    }
                    break;
                case ExceptionTypeEnum.Warn:
                    if (paramsKVPs == null)
                    {
                        LogWarn(methodName, callerLineNumber, message, paramsArray);
                    }
                    else
                    {
                        LogWarn(methodName, callerLineNumber, message, paramsKVPs, paramsArray);
                    }
                    break;
                case ExceptionTypeEnum.Error:
                    if (paramsKVPs == null)
                    {
                        LogError(methodName, callerLineNumber, message, exception, paramsArray);
                    }
                    else
                    {
                        LogError(methodName, callerLineNumber, message, exception, paramsKVPs, paramsArray);
                    }
                    break;
                case ExceptionTypeEnum.Fatal:
                    if (paramsKVPs == null)
                    {
                        LogFatal(methodName, callerLineNumber, message, exception, paramsArray);
                    }
                    else
                    {
                        LogFatal(methodName, callerLineNumber, message, exception, paramsKVPs, paramsArray);
                    }
                    break;
            }
        }
        public void LogDebug(string methodName, int callerLineNumber, string message, params string[] paramsArray)
        {
            AssignPropertyValues(methodName, callerLineNumber, BuildJsonString(paramsArray));
            logger_.Debug(message);
        }
        public void LogDebug(string methodName, int callerLineNumber, string message, List<KeyValuePair<string, string>> paramsKVPs, params string[] paramsArray)
        {
            AssignPropertyValues(methodName, callerLineNumber, BuildJsonString(paramsKVPs, paramsArray));
            logger_.Debug(message);
        }
        public void LogInfo(string methodName, int callerLineNumber, string message, params string[] paramsArray)
        {
            AssignPropertyValues(methodName, callerLineNumber, BuildJsonString(paramsArray));
            logger_.Info(message);
        }
        public void LogInfo(string methodName, int callerLineNumber, string message, List<KeyValuePair<string, string>> paramsKVPs, params string[] paramsArray)
        {
            AssignPropertyValues(methodName, callerLineNumber, BuildJsonString(paramsKVPs, paramsArray));
            logger_.Info(message);
        }
        public void LogWarn(string methodName, int callerLineNumber, string message, params string[] paramsArray)
        {
            AssignPropertyValues(methodName, callerLineNumber, BuildJsonString(paramsArray));
            logger_.Warn(message);
        }
        public void LogWarn(string methodName, int callerLineNumber, string message, List<KeyValuePair<string, string>> paramsKVPs, params string[] paramsArray)
        {
            AssignPropertyValues(methodName, callerLineNumber, BuildJsonString(paramsKVPs, paramsArray));
            logger_.Warn(message);
        }
        public void LogError(string methodName, int callerLineNumber, string message, Exception exception, params string[] paramsArray)
        {
            AssignPropertyValues(methodName, callerLineNumber, BuildJsonString(paramsArray));
            logger_.Error(message, exception);
        }
        public void LogError(string methodName, int callerLineNumber, string message, Exception exception, List<KeyValuePair<string, string>> paramsKVPs, params string[] paramsArray)
        {
            AssignPropertyValues(methodName, callerLineNumber, BuildJsonString(paramsKVPs, paramsArray));
            logger_.Error(message, exception);
        }
        public void LogFatal(string methodName, int callerLineNumber, string message, Exception exception, params string[] paramsArray)
        {
            AssignPropertyValues(methodName, callerLineNumber, BuildJsonString(paramsArray));
            logger_.Fatal(message, exception);
        }
        public void LogFatal(string methodName, int callerLineNumber, string message, Exception exception, List<KeyValuePair<string, string>> paramsKVPs, params string[] paramsArray)
        {
            AssignPropertyValues(methodName, callerLineNumber, BuildJsonString(paramsKVPs, paramsArray));
            logger_.Fatal(message, exception);
        }
        #endregion
        #region Private Methods
        private void AssignPropertyValues(string methodName, int callerLineNumber, string debugData)
        {
            log4net.LogicalThreadContext.Properties["IPAddress"] = ipAddress_;
            log4net.LogicalThreadContext.Properties["ApplicationName"] = applicationName_;
            log4net.LogicalThreadContext.Properties["ExecUniqueId"] = execUniqueId_;
            log4net.LogicalThreadContext.Properties["LoggedInUserId"] = loggedInUserId_;
            log4net.LogicalThreadContext.Properties["CallingAssemblyName"] = callingAssemblyName_;
            log4net.LogicalThreadContext.Properties["ExecutingAssemblyName"] = executingAssemblyName_;
            log4net.LogicalThreadContext.Properties["CurrentMethodDeclaringType"] = currentMethodDeclaringType_;
            log4net.LogicalThreadContext.Properties["MethodName"] = methodName;
            log4net.LogicalThreadContext.Properties["CallerLineNumber"] = callerLineNumber;
            log4net.LogicalThreadContext.Properties["DebugData"] = debugData;
        }
        private string BuildJsonString(params string[] paramsArray)
        {
            List<KeyValuePair<string, string>> paramsKVPs = new List<KeyValuePair<string, string>>();
            //paramsKVPs.Add(new KeyValuePair<string, string>("message", message));
            for (int i = 0; i < paramsArray.Length; i = i + 2)
            {
                paramsKVPs.Add(new KeyValuePair<string, string>(paramsArray[i], paramsArray[i + 1]));
            }
            return JsonConvert.SerializeObject(paramsKVPs);
        }
        //private string BuildJsonString(string methodName, int callerLineNumber, string message, params string[] paramsArray)
        //{
        //    List<KeyValuePair<string, string>> paramsKVPs = new List<KeyValuePair<string, string>>();
        //    paramsKVPs.Add(new KeyValuePair<string, string>("message", message));
        //    paramsKVPs.Add(new KeyValuePair<string, string>("callerMemberName", callerMemberName));
        //    paramsKVPs.Add(new KeyValuePair<string, string>("callerLineNumber", callerLineNumber.ToString()));
        //    paramsKVPs.Add(new KeyValuePair<string, string>("exceptionMessage", exception.Message));
        //    paramsKVPs.Add(new KeyValuePair<string, string>("innerException", exception.InnerException != null ? exception.InnerException.Message : ""));
        //    paramsKVPs.Add(new KeyValuePair<string, string>("stackTrace", exception.StackTrace != null ? exception.StackTrace : ""));
        //    for (int i = 0; i < paramsArray.Length; i = i + 2)
        //    {
        //        paramsKVPs.Add(new KeyValuePair<string, string>(paramsArray[i], paramsArray[i + 1]));
        //    }
        //    return JsonConvert.SerializeObject(paramsKVPs);
        //}
        private string BuildJsonString(List<KeyValuePair<string, string>> keyValuePairs, params string[] paramsArray)
        {
            List<KeyValuePair<string, string>> paramsKVPs = new List<KeyValuePair<string, string>>();
            //paramsKVPs.Add(new KeyValuePair<string, string>("message", message));
            if (keyValuePairs != null)
            {
                foreach (var keyValuePair in keyValuePairs)
                {
                    paramsKVPs.Add(new KeyValuePair<string, string>(keyValuePair.Key, keyValuePair.Value));
                }
            }
            //paramsKVPs.Add(new KeyValuePair<string, string>("exceptionMessage", exception.Message));
            //paramsKVPs.Add(new KeyValuePair<string, string>("innerException", exception.InnerException != null ? exception.InnerException.Message : ""));
            //paramsKVPs.Add(new KeyValuePair<string, string>("stackTrace", exception.StackTrace != null ? exception.StackTrace : ""));
            for (int i = 0; i < paramsArray.Length; i = i + 2)
            {
                paramsKVPs.Add(new KeyValuePair<string, string>(paramsArray[i], paramsArray[i + 1]));
            }
            return JsonConvert.SerializeObject(paramsKVPs);
        }
        #endregion
        /*
        #region Append Exception Parms
        private void AppendExceptionParmKVPs(string exceptionParmKVPKey, string exceptionParmKVPValue)
        {
            ExceptionParmKVPs.Add(new KeyValuePair<string, string>(exceptionParmKVPKey, exceptionParmKVPValue));
        }
        private void AppendExceptionParmKVPs(params string[] exceptionParmParams)
        {
            int exceptionParmParamsLength = exceptionParmParams.Length;
            exceptionParmParamsLength = (exceptionParmParamsLength % 2) == 0 ? exceptionParmParamsLength : (exceptionParmParamsLength - 1);
            for (int i = 0; i < exceptionParmParamsLength; i = i + 2)
            {
                ExceptionParmKVPs.Add(new KeyValuePair<string, string>(exceptionParmParams[i].ToString(), (exceptionParmParams[i + 1] ?? "").ToString()));
            }
        }
        private void AppendExceptionParmKVPs(List<KeyValuePair<string, string>> exceptionParmListKVPs)
        {
            if (exceptionParmListKVPs != null)
            {
                foreach (var exceptionParmListKVP in exceptionParmListKVPs)
                {
                    ExceptionParmKVPs.Add(new KeyValuePair<string, string>(exceptionParmListKVP.Key, exceptionParmListKVP.Value));
                }
            }
        }
        private void AppendExceptionParmKVPs(List<KeyValuePair<string, string>> exceptionParmListKVPs, params string[] exceptionParmParams)
        {
            AppendExceptionParmKVPs(exceptionParmListKVPs);
            AppendExceptionParmKVPs(exceptionParmParams);
        }
        private void AppendExceptionParmKVPs(Exception exception)
        {
            ExceptionParmKVPs.Add(new KeyValuePair<string, string>("HResult", exception.HResult.ToString()));
            ExceptionParmKVPs.Add(new KeyValuePair<string, string>("Message", exception.Message));
            if (exception.InnerException != null && exception.InnerException.InnerException != null && exception.InnerException.InnerException.Message != null)
            {
                ExceptionParmKVPs.Add(new KeyValuePair<string, string>("InnerException", exception.InnerException.InnerException.Message));
            }
        }
        #endregion
        #region Log Info & clear ExceptionParmKVPs
        public void LogInfo(string methodName, int callerLineNumber, string message)
        {
            var messageCallerLineNumber = new List<KeyValuePair<string, string>>();
            messageCallerLineNumber.Add(new KeyValuePair<string, string>("message", message));
            messageCallerLineNumber.Add(new KeyValuePair<string, string>("callerLineNumber", callerLineNumber.ToString()));
            Logger.Info(JsonConvert.SerializeObject(messageCallerLineNumber) + JsonConvert.SerializeObject(exceptionDataKVPs_) + JsonConvert.SerializeObject(ExceptionParmKVPs));
            ExceptionParmKVPs = new List<KeyValuePair<string, string>>();
        }
        public void LogInfo(string methodName, int callerLineNumber, string message, params string[] exceptionParmParams)
        {
            AppendExceptionParmKVPs(exceptionParmParams);

            var messageCallerLineNumber = new List<KeyValuePair<string, string>>();
            messageCallerLineNumber.Add(new KeyValuePair<string, string>("message", message));
            messageCallerLineNumber.Add(new KeyValuePair<string, string>("callerLineNumber", callerLineNumber.ToString()));
            Logger.Info(JsonConvert.SerializeObject(messageCallerLineNumber) + JsonConvert.SerializeObject(exceptionDataKVPs_) + JsonConvert.SerializeObject(ExceptionParmKVPs));
            ExceptionParmKVPs = new List<KeyValuePair<string, string>>();
        }
        public void LogInfo(string methodName, int callerLineNumber, string message, List<KeyValuePair<string, string>> exceptionParmListKVPs)
        {
            AppendExceptionParmKVPs(exceptionParmListKVPs);

            var messageCallerLineNumber = new List<KeyValuePair<string, string>>();
            messageCallerLineNumber.Add(new KeyValuePair<string, string>("message", message));
            messageCallerLineNumber.Add(new KeyValuePair<string, string>("callerLineNumber", callerLineNumber.ToString()));
            Logger.Info(JsonConvert.SerializeObject(messageCallerLineNumber) + JsonConvert.SerializeObject(exceptionDataKVPs_) + JsonConvert.SerializeObject(ExceptionParmKVPs));
            ExceptionParmKVPs = new List<KeyValuePair<string, string>>();
        }
        public void LogInfo(string methodName, int callerLineNumber, string message, List<KeyValuePair<string, string>> exceptionParmListKVPs, params string[] exceptionParmParams)
        {
            AppendExceptionParmKVPs(exceptionParmListKVPs, exceptionParmParams);

            var messageCallerLineNumber = new List<KeyValuePair<string, string>>();
            messageCallerLineNumber.Add(new KeyValuePair<string, string>("message", message));
            messageCallerLineNumber.Add(new KeyValuePair<string, string>("callerLineNumber", callerLineNumber.ToString()));
            Logger.Info(JsonConvert.SerializeObject(messageCallerLineNumber) + JsonConvert.SerializeObject(exceptionDataKVPs_) + JsonConvert.SerializeObject(ExceptionParmKVPs));
            ExceptionParmKVPs = new List<KeyValuePair<string, string>>();
        }
        #endregion
        #region Log Error & clear ExceptionParmKVPs
        public void LogError(string methodName, int callerLineNumber, string message, Exception exception)
        {
            AppendExceptionParmKVPs(exception);
            var messageCallerLineNumber = new List<KeyValuePair<string, string>>();
            messageCallerLineNumber.Add(new KeyValuePair<string, string>("message", message));
            messageCallerLineNumber.Add(new KeyValuePair<string, string>("callerLineNumber", callerLineNumber.ToString()));
            Logger.Error(JsonConvert.SerializeObject(messageCallerLineNumber) + JsonConvert.SerializeObject(exceptionDataKVPs_) + JsonConvert.SerializeObject(ExceptionParmKVPs), exception);
            ExceptionParmKVPs = new List<KeyValuePair<string, string>>();
        }
        public void LogError(string methodName, int callerLineNumber, string message, Exception exception, params string[] exceptionParmParams)
        {
            AppendExceptionParmKVPs(exceptionParmParams);
            AppendExceptionParmKVPs(exception);

            var messageCallerLineNumber = new List<KeyValuePair<string, string>>();
            messageCallerLineNumber.Add(new KeyValuePair<string, string>("message", message));
            messageCallerLineNumber.Add(new KeyValuePair<string, string>("callerLineNumber", callerLineNumber.ToString()));
            Logger.Error(JsonConvert.SerializeObject(messageCallerLineNumber) + JsonConvert.SerializeObject(exceptionDataKVPs_) + JsonConvert.SerializeObject(ExceptionParmKVPs), exception);
            ExceptionParmKVPs = new List<KeyValuePair<string, string>>();
        }
        public void LogError(string methodName, int callerLineNumber, string message, Exception exception, List<KeyValuePair<string, string>> exceptionParmListKVPs)
        {
            AppendExceptionParmKVPs(exceptionParmListKVPs);
            AppendExceptionParmKVPs(exception);

            var messageCallerLineNumber = new List<KeyValuePair<string, string>>();
            messageCallerLineNumber.Add(new KeyValuePair<string, string>("message", message));
            messageCallerLineNumber.Add(new KeyValuePair<string, string>("callerLineNumber", callerLineNumber.ToString()));
            Logger.Error(JsonConvert.SerializeObject(messageCallerLineNumber) + JsonConvert.SerializeObject(exceptionDataKVPs_) + JsonConvert.SerializeObject(ExceptionParmKVPs), exception);
            ExceptionParmKVPs = new List<KeyValuePair<string, string>>();
        }
        public void LogError(string methodName, int callerLineNumber, string message, Exception exception, List<KeyValuePair<string, string>> exceptionParmListKVPs, params string[] exceptionParmParams)
        {
            AppendExceptionParmKVPs(exceptionParmListKVPs, exceptionParmParams);
            AppendExceptionParmKVPs(exception);

            var messageCallerLineNumber = new List<KeyValuePair<string, string>>();
            messageCallerLineNumber.Add(new KeyValuePair<string, string>("message", message));
            messageCallerLineNumber.Add(new KeyValuePair<string, string>("callerLineNumber", callerLineNumber.ToString()));
            Logger.Error(JsonConvert.SerializeObject(messageCallerLineNumber) + JsonConvert.SerializeObject(exceptionDataKVPs_) + JsonConvert.SerializeObject(ExceptionParmKVPs), exception);
            ExceptionParmKVPs = new List<KeyValuePair<string, string>>();
        }
        #endregion
        */
        /*
        #region Log Info & do not clear ExceptionParmKVPs
        public void LogInfo(string methodName, int callerLineNumber, string message, bool doNotClearExceptionParmKVPs)
        {
            var messageCallerLineNumber = new List<KeyValuePair<string, string>>();
            messageCallerLineNumber.Add(new KeyValuePair<string, string>("message", message));
            messageCallerLineNumber.Add(new KeyValuePair<string, string>("callerLineNumber", callerLineNumber.ToString()));
            Logger.Info(JsonConvert.SerializeObject(messageCallerLineNumber) + JsonConvert.SerializeObject(exceptionDataKVPs_) + JsonConvert.SerializeObject(ExceptionParmKVPs));
        }
        public void LogInfo(string methodName, int callerLineNumber, string message, bool doNotClearExceptionParmKVPs, params string[] exceptionParmParams)
        {
            AppendExceptionParmKVPs(exceptionParmParams);

            var messageCallerLineNumber = new List<KeyValuePair<string, string>>();
            messageCallerLineNumber.Add(new KeyValuePair<string, string>("message", message));
            messageCallerLineNumber.Add(new KeyValuePair<string, string>("callerLineNumber", callerLineNumber.ToString()));
            Logger.Info(JsonConvert.SerializeObject(messageCallerLineNumber) + JsonConvert.SerializeObject(exceptionDataKVPs_) + JsonConvert.SerializeObject(ExceptionParmKVPs));
        }
        public void LogInfo(string methodName, int callerLineNumber, string message, bool doNotClearExceptionParmKVPs, List<KeyValuePair<string, string>> exceptionParmListKVPs)
        {
            AppendExceptionParmKVPs(exceptionParmListKVPs);

            var messageCallerLineNumber = new List<KeyValuePair<string, string>>();
            messageCallerLineNumber.Add(new KeyValuePair<string, string>("message", message));
            messageCallerLineNumber.Add(new KeyValuePair<string, string>("callerLineNumber", callerLineNumber.ToString()));
            Logger.Info(JsonConvert.SerializeObject(messageCallerLineNumber) + JsonConvert.SerializeObject(exceptionDataKVPs_) + JsonConvert.SerializeObject(ExceptionParmKVPs));
        }
        public void LogInfo(string methodName, int callerLineNumber, string message, bool doNotClearExceptionParmKVPs, List<KeyValuePair<string, string>> exceptionParmListKVPs, params string[] exceptionParmParams)
        {
            AppendExceptionParmKVPs(exceptionParmListKVPs, exceptionParmParams);

            var messageCallerLineNumber = new List<KeyValuePair<string, string>>();
            messageCallerLineNumber.Add(new KeyValuePair<string, string>("message", message));
            messageCallerLineNumber.Add(new KeyValuePair<string, string>("callerLineNumber", callerLineNumber.ToString()));
            Logger.Info(JsonConvert.SerializeObject(messageCallerLineNumber) + JsonConvert.SerializeObject(exceptionDataKVPs_) + JsonConvert.SerializeObject(ExceptionParmKVPs));
        }
        #endregion
        #region Log Error & do not clear ExceptionParmKVPs
        public void LogError(string methodName, int callerLineNumber, string message, Exception exception, bool doNotClearExceptionParmKVPs)
        {
            var messageCallerLineNumber = new List<KeyValuePair<string, string>>();
            messageCallerLineNumber.Add(new KeyValuePair<string, string>("message", message));
            messageCallerLineNumber.Add(new KeyValuePair<string, string>("callerLineNumber", callerLineNumber.ToString()));
            Logger.Error(JsonConvert.SerializeObject(messageCallerLineNumber) + JsonConvert.SerializeObject(exceptionDataKVPs_) + JsonConvert.SerializeObject(ExceptionParmKVPs), exception);
        }
        public void LogError(string methodName, int callerLineNumber, string message, Exception exception, bool doNotClearExceptionParmKVPs, params string[] exceptionParmParams)
        {
            AppendExceptionParmKVPs(exceptionParmParams);

            var messageCallerLineNumber = new List<KeyValuePair<string, string>>();
            messageCallerLineNumber.Add(new KeyValuePair<string, string>("message", message));
            messageCallerLineNumber.Add(new KeyValuePair<string, string>("callerLineNumber", callerLineNumber.ToString()));
            Logger.Error(JsonConvert.SerializeObject(messageCallerLineNumber) + JsonConvert.SerializeObject(exceptionDataKVPs_) + JsonConvert.SerializeObject(ExceptionParmKVPs), exception);
        }
        public void LogError(string methodName, int callerLineNumber, string message, Exception exception, bool doNotClearExceptionParmKVPs, List<KeyValuePair<string, string>> exceptionParmListKVPs)
        {
            AppendExceptionParmKVPs(exceptionParmListKVPs);

            var messageCallerLineNumber = new List<KeyValuePair<string, string>>();
            messageCallerLineNumber.Add(new KeyValuePair<string, string>("message", message));
            messageCallerLineNumber.Add(new KeyValuePair<string, string>("callerLineNumber", callerLineNumber.ToString()));
            Logger.Error(JsonConvert.SerializeObject(messageCallerLineNumber) + JsonConvert.SerializeObject(exceptionDataKVPs_) + JsonConvert.SerializeObject(ExceptionParmKVPs), exception);
        }
        public void LogError(string methodName, int callerLineNumber, string message, Exception exception, bool doNotClearExceptionParmKVPs, List<KeyValuePair<string, string>> exceptionParmListKVPs, params string[] exceptionParmParams)
        {
            AppendExceptionParmKVPs(exceptionParmListKVPs, exceptionParmParams);

            var messageCallerLineNumber = new List<KeyValuePair<string, string>>();
            messageCallerLineNumber.Add(new KeyValuePair<string, string>("message", message));
            messageCallerLineNumber.Add(new KeyValuePair<string, string>("callerLineNumber", callerLineNumber.ToString()));
            Logger.Error(JsonConvert.SerializeObject(messageCallerLineNumber) + JsonConvert.SerializeObject(exceptionDataKVPs_) + JsonConvert.SerializeObject(ExceptionParmKVPs), exception);
        }
        #endregion
        */
    }
}
