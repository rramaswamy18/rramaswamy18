using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryMenuModels
{
    public class MenuKVPModel : AuditInfoModel
    {
        public long MenuKVPId { set; get; }
        public long ClientId { set; get; }
        public long MenuListId { set; get; }
        public float SeqNum { set; get; }
        public string MenuKVPKeyData { set; get; }
        public string MenuKVPValueData { set; get; }
        public MenuListModel MenuListModel { set; get; }
    }
}
