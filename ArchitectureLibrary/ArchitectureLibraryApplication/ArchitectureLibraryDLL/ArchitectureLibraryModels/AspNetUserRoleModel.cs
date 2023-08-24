using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArchitectureLibraryModels
{
    public class AspNetUserRoleModel : AuditInfoModel
    {
        public string AspNetUserRoleId { get; set; }
        public long ClientId { get; set; }
        public string AspNetUserId { get; set; }
        public string AspNetRoleId { get; set; }
        public AspNetRoleModel AspNetRoleModel { get; set; }
    }
}
