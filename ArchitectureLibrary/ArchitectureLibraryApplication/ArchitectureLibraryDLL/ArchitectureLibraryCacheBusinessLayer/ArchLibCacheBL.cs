using ArchitectureLibraryDataLayer;
using ArchitectureLibraryMenuDataLayer;
using ArchitectureLibraryMenuModels;
using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryCacheBusinessLayer
{
    public class ArchLibCacheBL
    {
        public void Initialize(out List<ApplicationDefaultModel> applicationDefaultModels, out List<ClientModel> clientModels, out List<AspNetRoleModel> aspNetRoleModels, out List<AspNetRoleParentMenu> aspNetRoleParentMenus, out List<UserProfileMetaDataModel> userProfileMetaDataModels, out List<SalesTaxListModel> salesTaxListModels, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            ArchLibDataContext.OpenSqlConnection();
            applicationDefaultModels = ArchLibDataContext.GetApplicationDefaults(ArchLibDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
            aspNetRoleModels = ArchLibDataContext.GetAspNetRoles(ArchLibDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
            clientModels = ArchLibDataContext.GetClients(ArchLibDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
            aspNetRoleParentMenus = ArchLibDataContext.GetAspNetRoleParentMenus(ArchLibDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
            userProfileMetaDataModels = ArchLibDataContext.GetUserProfileMetaDatas(ArchLibDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
            salesTaxListModels = ArchLibDataContext.GetSalesTaxLists(ArchLibDataContext.SqlConnectionObject, clientId, ipAddress, execUniqueId, loggedInUserId);
            ArchLibDataContext.CloseSqlConnection();
        }
    }
}
