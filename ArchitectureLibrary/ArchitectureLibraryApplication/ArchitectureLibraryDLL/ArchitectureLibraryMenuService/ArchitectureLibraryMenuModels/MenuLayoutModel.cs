using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryMenuModels
{
    public class MenuLayoutModel : AuditInfoModel
    {
        public long MenuLayoutId { set; get; }
        public long ClientId { set; get; }
        public string MenuListDesc { set; get; }
        public long MenuListId { set; get; }
        public long ParentMenuListId { set; get; }
        public float SeqNum { set; get; }
        public MenuListModel MenuListModel { set; get; }
        public MenuUrlAction MenuUrlAction { set; get; }
        public MenuListModel ParentMenuListModel { set; get; }
        public List<MenuLayoutModel> ChildMenuLayoutModels { set; get; }
    }
}
