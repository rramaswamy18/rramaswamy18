using ArchitectureLibraryMenuModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryMenuDataLayer
{
    public static partial class ArchLibMenuDataContext
    {
        private static MenuKVPModel AssignMenuKVP(SqlDataReader sqlDataReader, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            MenuKVPModel menuKVPModel = new MenuKVPModel
            {
                MenuKVPId = long.Parse(sqlDataReader["MenuKVPId"].ToString()),
                ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                MenuListId = long.Parse(sqlDataReader["MenuListId"].ToString()),
                SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
                MenuKVPKeyData = sqlDataReader["MenuKVPKeyData"].ToString(),
                MenuKVPValueData = sqlDataReader["MenuKVPValueData"].ToString(),
            };
            return menuKVPModel;
        }
        private static MenuLayoutModel AssignMenuLayout(SqlDataReader sqlDataReader, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            MenuLayoutModel menuLayoutModel = new MenuLayoutModel
            {
                MenuLayoutId = long.Parse(sqlDataReader["MenuLayoutId"].ToString()),
                ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                MenuListDesc = sqlDataReader["MenuListDesc"].ToString(),
                MenuListId = long.Parse(sqlDataReader["MenuListId"].ToString()),
                ParentMenuListId = long.Parse(sqlDataReader["ParentMenuListId"].ToString()),
                SeqNum = float.Parse(sqlDataReader["SeqNum"].ToString()),
            };
            return menuLayoutModel;
        }
        private static MenuListModel AssignMenuList(SqlDataReader sqlDataReader, string ipAddress, string execUniqueId, string loggedInUserId)
        {
            MenuListModel menuListModel = new MenuListModel
            {
                MenuListId = long.Parse(sqlDataReader["MenuListId"].ToString()),
                ClientId = long.Parse(sqlDataReader["ClientId"].ToString()),
                MenuListDesc = sqlDataReader["MenuListDesc"].ToString(),
                MenuListNameDesc = sqlDataReader["MenuListNameDesc"].ToString(),
            };
            return menuListModel;
        }
    }
}
