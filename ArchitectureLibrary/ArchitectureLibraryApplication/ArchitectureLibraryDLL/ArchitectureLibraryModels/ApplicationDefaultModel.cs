using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryModels
{
    public class ApplicationDefaultModel : AuditInfoModel
    {
        public long ApplicationDefaultId { get; set; }
        public long ClientId { get; set; }
        public double SeqNum { get; set; }
        public string KVPKey { get; set; }
        public string KVPSubKey { get; set; }
        public string KVPValue { get; set; }
        public ClientModel ClientModel { set; get; }
    }
}
