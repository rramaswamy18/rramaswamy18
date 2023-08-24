using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryModels
{
    public class AspNetRoleParentMenu : AuditInfoModel
    {
        public string AspNetRoleParentMenuId { get; set; }
        public long ClientId { get; set; }
        public string AspNetRoleId { get; set; }
        public string ParentMenuNameDesc { get; set; }
    }
}
