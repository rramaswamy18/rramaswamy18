using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryModels
{
    public class ClientModel : AuditInfoModel
    {
        public long ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientDesc { get; set; }
        public long? ParentClientId { get; set; }
        public string WebSiteUrl { get; set; }
        public List<ApplicationDefaultModel> ApplicationDefaultModels { set; get; }
    }
}
