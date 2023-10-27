using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryModels
{
    public class UserProfileMetaDataModel : AuditInfoModel
    {
        public long UserProfileMetaDataId { set; get; }
        public long ClientId { set; get; }
        public string MetaDataName { set; get; }
        public float SeqNum { set; get; }
        public bool IsMapped { set; get; }
    }
}
