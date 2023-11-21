using ArchitectureLibraryCacheBusinessLayer;
using ArchitectureLibraryMenuModels;
using ArchitectureLibraryModels;
using ArchitectureLibraryUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryCacheData
{
    public static class ArchLibCache
    {
        public static long ClientId { set; get; }
        public static string ClientName { set; get; }
        public static string IpInfoClientAccessToken { set; get; }
        public static string IpInfoMode { set; get; }
        public static bool RedirectToHttps { set; get; }
        public static string ValidationSummaryMessageFixErrors { get { return "PLEASE FIX ERRORS TO CONTINUE???"; } }
        public static string ValidationSummaryMessageSuccess { get { return "Process completed successfully!!!"; } }
        public static List<ApplicationDefaultModel> ApplicationDefaultModels { set; get; }
        public static List<ClientModel> ClientModels { get; set; }
        public static List<AspNetRoleModel> AspNetRoleModels { set; get; }
        public static List<AspNetRoleParentMenu> AspNetRoleParentMenus { get; set; }
        public static List<UserProfileMetaDataModel> UserProfileMetaDatas { get; set; }
        public static string GetApplicationDefault(long clientId, string kvpKey, string kvpSubKey)
        {
            return ClientModels.FirstOrDefault(x => x.ClientId == clientId).ApplicationDefaultModels.FirstOrDefault(x => x.ClientId == clientId && x.KVPKey == kvpKey && x.KVPSubKey == kvpSubKey).KVPValue;
        }
        public static string GetPrivateKey(long clientId)
        {
            string privateKey;
            string encryptedData = GetApplicationDefault(0, "PrivateKeyMaster", "");
            string decryptedData = EncryptDecrypt.DecryptDataMd5(encryptedData, "MANA9DNAV");
            encryptedData = GetApplicationDefault(clientId, "PrivateKey", "");
            privateKey = EncryptDecrypt.DecryptDataMd5(encryptedData, decryptedData);
            return privateKey;
        }
        public static void Initialize(long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            ArchLibCacheBL archLibCacheBL = new ArchLibCacheBL();
            archLibCacheBL.Initialize(out List<ApplicationDefaultModel> applicationDefaultModels, out List<ClientModel> clientModels, out List<AspNetRoleModel> aspNetRoleModels, out List<AspNetRoleParentMenu> aspNetRoleParentMenus, out List<UserProfileMetaDataModel> userProfileMetaDataModels, clientId, ipAddress, execUniqueId, loggedInUserId);
            ApplicationDefaultModels = applicationDefaultModels;
            ClientModels = clientModels;
            AspNetRoleParentMenus = aspNetRoleParentMenus;
            AspNetRoleModels = aspNetRoleModels;
            UserProfileMetaDatas = userProfileMetaDataModels;
            BuildCacheModels(applicationDefaultModels, clientModels, clientId, ipAddress, execUniqueId, loggedInUserId);
            ClientId = clientId;
            ClientName = Utilities.GetApplicationValue("ClientName");
            IpInfoClientAccessToken = GetApplicationDefault(0, "IpInfo", "ClientAccessToken");
            IpInfoMode = Utilities.GetApplicationValue("IpInfoMode");
            RedirectToHttps = bool.Parse(Utilities.GetApplicationValue("RedirectToHttps"));
        }
        private static void BuildCacheModels(List<ApplicationDefaultModel> applicationDefaultModels, List<ClientModel> clientModels, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            //Add children ApplicationDefaults to Client
            foreach (var clientModel in clientModels)
            {
                clientModel.ApplicationDefaultModels = applicationDefaultModels.Where(x => x.ClientId == clientModel.ClientId).OrderBy(x => x.SeqNum).ToList();
            }
            //Assign parent Client to ApplicationDefault
            foreach (var applicationDefaultModel in applicationDefaultModels)
            {
                applicationDefaultModel.ClientModel = clientModels.Find(x => x.ClientId == applicationDefaultModel.ClientId);
            }
        }
    }
}
