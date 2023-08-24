using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryMenuModels
{
    public class MenuListModel : AuditInfoModel
    {
        public long MenuListId { set; get; }
        public long ClientId { set; get; }
        public string MenuListDesc { set; get; }
        public string MenuListNameDesc { set; get; }
        public List<MenuKVPModel> MenuKVPModels { set; get; }
    }
}
