using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryMenuModels
{
    public abstract class AuditInfoModel
    {
        public virtual string AddDateTime { set; get; }
        public virtual string AddUserId { set; get; }
        public virtual string AddUserName { set; get; }
        public virtual string UpdDateTime { set; get; }
        public virtual string UpdUserId { set; get; }
        public virtual string UpdUserName { set; get; }
    }
}
