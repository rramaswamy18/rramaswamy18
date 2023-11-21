using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArchitectureLibraryModels
{
    public class AspNetRoleModel : AuditInfoModel
    {
        public string AspNetRoleId { get; set; }
        public long ClientId { get; set; }
        public string ActionName { get; set; }
        public string AspNetRoleName { get; set; }
        public string ControllerName { get; set; }
        public string Name { get; set; }
        public long UserTypeId { get; set; }
    }
}
