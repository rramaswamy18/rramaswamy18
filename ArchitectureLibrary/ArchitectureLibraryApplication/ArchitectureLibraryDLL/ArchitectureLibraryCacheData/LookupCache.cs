using ArchitectureLibraryCacheBusinessLayer;
using ArchitectureLibraryEnumerations;
using ArchitectureLibraryException;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArchitectureLibraryCacheData
{
    public static class LookupCache
    {
        public static List<CodeDataModel> CodeDataModels { set; get; }
        public static List<CodeTypeModel> CodeTypeModels { set; get; }
        public static List<PersonModel> PersonModels { set; get; }
        public static Dictionary<string, Dictionary<string, List<SelectListItem>>> CodeTypeSelectListItems { set; get; }
        public static Dictionary<string, Dictionary<string, string>> CodeDataOptionTags { set; get; }
        public static void Initialize(long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            ExceptionLogger exceptionLogger = Utilities.CreateExceptionLogger(Utilities.GetApplicationValue("ApplicationName"), ipAddress, execUniqueId, loggedInUserId, Assembly.GetCallingAssembly().FullName, Assembly.GetExecutingAssembly().FullName, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00000000 :: Enter");
            LookupCacheBL lookupBL = new LookupCacheBL();
            lookupBL.Initialize(out List<CodeTypeModel> codeTypeModels, out List<CodeDataModel> codeDataModels, out Dictionary<string, Dictionary<string, List<SelectListItem>>> codeTypeSelectListItems, out Dictionary<string, Dictionary<string, string>> codeDataOptionTags, ipAddress, execUniqueId, loggedInUserId);
            CodeDataModels = codeDataModels;
            CodeTypeModels = codeTypeModels;
            CodeTypeSelectListItems = codeTypeSelectListItems;
            CodeDataOptionTags = codeDataOptionTags;
            exceptionLogger.LogInfo(methodName, Utilities.GetCallerLineNumber(), "00090000 :: Exit");
        }

        /*
        1. There are 3 unique keys in Code Type
        1.1. CodeTypeId
        1.2. CodeTypeNameId
        1.3. CodeTypeNameDesc
        2. There are 3 unique keys in Code Data
        2.1. CodeDataId
        2.2. CodeDataNameId
        2.3. CodeDataNameDesc
        3. Purpose is to get in cross product of the above combos - which 3 *3 = 9
        3.1 Given CodeTypeId get all CodeDatas in order of CodeDataId
        3.2 Given CodeTypeId get all CodeDatas in order of CodeDataNameId
        3.3 Given CodeTypeId get all CodeDatas in order of CodeDataNameDesc
        3.4 Given CodeTypeNameId get all CodeDatas in order of CodeDataId
        3.5 Given CodeTypeNameId get all CodeDatas in order of CodeDataNameId
        3.6 Given CodeTypeNameId get all CodeDatas in order of CodeDataNameDesc
        3.7 Given CodeTypeNameDesc get all CodeDatas in order of CodeDataId
        3.8 Given CodeTypeNameDesc get all CodeDatas in order of CodeDataNameId
        3.9 Given CodeTypeNameDesc get all CodeDatas in order of CodeDataNameDesc
        Data is already available as part of load
        */

        #region Get CodeDatas For CodeTypeId
        /// <summary>
        /// Get CodeDatas for CodeTypeId in CodeDataId sequence
        /// </summary>
        /// <param name="codeTypeId"></param>
        /// <returns></returns>
        public static List<CodeDataModel> GetCodeDatasForCodeTypeIdByCodeDataId(long codeTypeId, string execUniqueId)
        {
            return CodeTypeModels.Find(x => x.CodeTypeId == codeTypeId).CodeDataModelsCodeDataId;
        }

        /// <summary>
        /// Get CodeDatas for CodeTypeId in CodeDataNameId sequence
        /// </summary>
        /// <param name="codeTypeId"></param>
        /// <returns></returns>
        public static List<CodeDataModel> GetCodeDatasForCodeTypeIdByCodeDataNameId(long codeTypeId, string execUniqueId)
        {
            return CodeTypeModels.Find(x => x.CodeTypeId == codeTypeId).CodeDataModelsCodeDataNameId;
        }

        /// <summary>
        /// Get CodeDatas for CodeTypeId in CodeDataNameDesc sequence
        /// </summary>
        /// <param name="codeTypeId"></param>
        /// <returns></returns>
        public static List<CodeDataModel> GetCodeDatasForCodeTypeIdByCodeDataNameDesc(long codeTypeId, string execUniqueId)
        {
            return CodeTypeModels.Find(x => x.CodeTypeId == codeTypeId).CodeDataModelsCodeDataNameDesc;
        }
        #endregion
        
        #region Get CodeDatas For CodeTypeNameId
        /// <summary>
        /// Get CodeDatas for CodeTypeNameId in CodeDataId sequence
        /// </summary>
        /// <param name="codeTypeNameId"></param>
        /// <returns></returns>
        public static List<CodeDataModel> GetCodeDatasForCodeTypeNameIdByCodeDataId(CodeTypeEnum codeTypeNameId, string execUniqueId)
        {
            return CodeTypeModels.Find(x => x.CodeTypeNameId == codeTypeNameId).CodeDataModelsCodeDataId;
        }

        /// <summary>
        /// Get CodeDatas for CodeTypeNameId in CodeDataNameId sequence
        /// </summary>
        /// <param name="codeTypeNameId"></param>
        /// <returns></returns>
        public static List<CodeDataModel> GetCodeDatasForCodeTypeNameIdByCodeDataNameId(CodeTypeEnum codeTypeNameId, string execUniqueId)
        {
            return CodeTypeModels.Find(x => x.CodeTypeNameId == codeTypeNameId).CodeDataModelsCodeDataNameId;
        }

        /// <summary>
        /// Get CodeDatas for CodeTypeNameId in CodeDataNameDesc sequence
        /// </summary>
        /// <param name="codeTypeNameId"></param>
        /// <returns></returns>
        public static List<CodeDataModel> GetCodeDatasForCodeTypeNameIdByCodeDataNameDesc(CodeTypeEnum codeTypeNameId, string execUniqueId)
        {
            return CodeTypeModels.Find(x => x.CodeTypeNameId == codeTypeNameId).CodeDataModelsCodeDataNameDesc;
        }
        #endregion

        #region Get CodeDatas For CodeTypeNameDesc
        /// <summary>
        /// Get CodeDatas for CodeTypeNameDesc in CodeDataId sequence
        /// </summary>
        /// <param name="codeTypeNameDesc"></param>
        /// <returns></returns>
        public static List<CodeDataModel> GetCodeDatasForCodeTypeNameDescByCodeDataId(string codeTypeNameDesc, string execUniqueId)
        {
            return CodeTypeModels.Find(x => x.CodeTypeNameDesc == codeTypeNameDesc).CodeDataModelsCodeDataId;
        }

        /// <summary>
        /// Get CodeDatas for CodeTypeNameDesc in CodeDataNameId sequence
        /// </summary>
        /// <param name="codeTypeNameDesc"></param>
        /// <returns></returns>
        public static List<CodeDataModel> GetCodeDatasForCodeTypeNameDescByCodeDataNameId(string codeTypeNameDesc, string execUniqueId)
        {
            return CodeTypeModels.Find(x => x.CodeTypeNameDesc == codeTypeNameDesc).CodeDataModelsCodeDataNameId;
        }

        /// <summary>
        /// Get CodeDatas for CodeTypeNameDesc in CodeDataNameDesc sequence
        /// </summary>
        /// <param name="codeTypeNameDesc"></param>
        /// <returns></returns>
        public static List<CodeDataModel> GetCodeDatasForCodeTypeNameDescByCodeDataNameDesc(string codeTypeNameDesc, string execUniqueId)
        {
            return CodeTypeModels.Find(x => x.CodeTypeNameDesc == codeTypeNameDesc).CodeDataModelsCodeDataNameDesc;
        }
        #endregion

        public static List<SelectListItem> GetCodeTypeSelectListItem(string codeTypeNameDesc, string execUniqueId)
        {
            Dictionary<string, List<SelectListItem>> x = CodeTypeSelectListItems[codeTypeNameDesc];
            List<SelectListItem> y = x["CodeDataNameId"];
            return y;
        }
    }
}
