using ArchitectureLibraryMenuDataLayer;
using ArchitectureLibraryMenuModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryMenuCacheBusinessLayer
{
    public class ArchLibMenuCacheBL
    {
        public void Initialize(out List<MenuKVPModel> menuKVPModels, out List<MenuLayoutModel> menuLayoutModels, out List<MenuListModel> menuListModels, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            ArchLibMenuDataContext.OpenSqlConnection();
            menuKVPModels = ArchLibMenuDataContext.GetMenuKVPs(ArchLibMenuDataContext.SqlConnectionObject, ipAddress, execUniqueId, loggedInUserId);
            menuLayoutModels = ArchLibMenuDataContext.GetMenuLayouts(ArchLibMenuDataContext.SqlConnectionObject, ipAddress, execUniqueId, loggedInUserId);
            menuListModels = ArchLibMenuDataContext.GetMenuLists(ArchLibMenuDataContext.SqlConnectionObject, ipAddress, execUniqueId, loggedInUserId);
            ArchLibMenuDataContext.CloseSqlConnection();
        }
    }
}
