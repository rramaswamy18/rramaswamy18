using ArchitectureLibraryMenuCacheBusinessLayer;
using ArchitectureLibraryMenuModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryMenuCacheData
{
    public static class ArchLibMenuCache
    {
        public static List<MenuKVPModel> MenuKVPModels { set; get; }
        public static List<MenuLayoutModel> MenuLayoutModels { set; get; }
        public static List<MenuListModel> MenuListModels { set; get; }
        public static Dictionary<string, List<MenuLayoutModel>> ParentMenuLayoutModels { set; get; }
        public static void Initialize(long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            ArchLibMenuCacheBL archLibCacheMenuBL = new ArchLibMenuCacheBL();
            archLibCacheMenuBL.Initialize(out List<MenuKVPModel> menuKVPModels, out List<MenuLayoutModel> menuLayoutModels, out List<MenuListModel> menuListModels, clientId, ipAddress, execUniqueId, loggedInUserId);
            MenuKVPModels = menuKVPModels;
            MenuLayoutModels = menuLayoutModels;
            MenuListModels = menuListModels;
            BuildCacheModels(menuKVPModels, menuLayoutModels, menuListModels, ipAddress, execUniqueId, loggedInUserId);
            ParentMenuLayoutModels = BuildParentMenuLayoutModels(menuLayoutModels, menuListModels, clientId, ipAddress, execUniqueId, loggedInUserId);
        }
        private static void BuildCacheModels(List<MenuKVPModel> menuKVPModels, List<MenuLayoutModel> menuLayoutModels, List<MenuListModel> menuListModels, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            //Add children MenuKVP to MenuList
            foreach (var menuListModel in menuListModels)
            {
                menuListModel.MenuKVPModels = menuKVPModels.FindAll(x => x.MenuListId == menuListModel.MenuListId);
            }
            //Assign Parent MenuList to MenuKVP
            foreach (var menuKVPModel in menuKVPModels)
            {
                menuKVPModel.MenuListModel = menuListModels.Find(x => x.MenuListId == menuKVPModel.MenuListId);
            }
            //Assign Parent  MenuList to MenuListId and Parent MenuList to menuListId
            foreach (var menuLayoutModel in menuLayoutModels)
            {
                menuLayoutModel.MenuListModel = menuListModels.Find(x => x.MenuListId == menuLayoutModel.MenuListId);
                menuLayoutModel.ParentMenuListModel = menuListModels.Find(x => x.MenuListId == menuLayoutModel.ParentMenuListId);
            }
        }
        private static Dictionary<string, List<MenuLayoutModel>> BuildParentMenuLayoutModels(List<MenuLayoutModel> menuLayoutModels, List<MenuListModel> menuListModels, long clientId, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            Dictionary<string, List<MenuLayoutModel>> parentMenuLayoutModels = new Dictionary<string, List<MenuLayoutModel>>();
            List<MenuLayoutModel> menuLayoutModelsTemp;
            MenuListModel menuListModel;
            foreach (var menuListNameDesc in menuLayoutModels.Select(x => x.ParentMenuListModel.MenuListNameDesc).Distinct().ToList())
            {
                menuLayoutModelsTemp = menuLayoutModels.FindAll(x => x.ParentMenuListModel.MenuListNameDesc == menuListNameDesc).OrderBy(y => y.SeqNum).ToList();
                foreach (var menuLayoutModelTemp in menuLayoutModelsTemp)
                {
                    menuListModel = menuListModels.First(x => x.MenuListId == menuLayoutModelTemp.MenuListId);
                    menuLayoutModelTemp.MenuUrlAction = new MenuUrlAction
                    {
                        AccessType = menuListModel.MenuKVPModels.First(x => x.MenuKVPKeyData == "AccessType").MenuKVPValueData,
                        ActionName = menuListModel.MenuKVPModels.First(x => x.MenuKVPKeyData == "ActionName").MenuKVPValueData,
                        AjaxUpdateTargetId = menuListModel.MenuKVPModels.First(x => x.MenuKVPKeyData == "AjaxUpdateTargetId").MenuKVPValueData,
                        ControllerName = menuListModel.MenuKVPModels.First(x => x.MenuKVPKeyData == "ControllerName").MenuKVPValueData,
                        //HrefTarget = menuListModel.MenuKVPModels.First(x => x.MenuKVPKeyData == "HrefTarget").MenuKVPValueData,
                        HrefWidth = "100%",//menuListModel.MenuKVPModels.First(x => x.MenuKVPKeyData == "HrefWidth").MenuKVPValueData,
                        LinkText = menuLayoutModelTemp.MenuListDesc,
                        QueryString = menuListModel.MenuKVPModels.First(x => x.MenuKVPKeyData == "QueryString").MenuKVPValueData,
                        RedirectActionName = menuListModel.MenuKVPModels.First(x => x.MenuKVPKeyData == "RedirectActionName").MenuKVPValueData,
                        RedirectControllerName = menuListModel.MenuKVPModels.First(x => x.MenuKVPKeyData == "RedirectControllerName").MenuKVPValueData,
                        RedirectQueryString = menuListModel.MenuKVPModels.First(x => x.MenuKVPKeyData == "RedirectQueryString").MenuKVPValueData,
                        RedirectMessage = menuListModel.MenuKVPModels.First(x => x.MenuKVPKeyData == "RedirectMessage").MenuKVPValueData,
                        RedirectMessageId = menuListModel.MenuKVPModels.First(x => x.MenuKVPKeyData == "RedirectMessage").MenuKVPId,
                    };
                }
                parentMenuLayoutModels[menuListNameDesc] = menuLayoutModelsTemp;
            }
            return parentMenuLayoutModels;
        }
    }
}
