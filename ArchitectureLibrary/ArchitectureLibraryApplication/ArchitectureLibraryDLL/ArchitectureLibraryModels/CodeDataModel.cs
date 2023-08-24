using ArchitectureLibraryEnumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryModels
{
    public class CodeDataModel : AuditInfoModel
    {
        public long CodeDataId { set; get; }
        public long CodeTypeId { set; get; }
        public CodeTypeEnum CodeTypeNameId { set; get; }
        public long CodeDataNameId { set; get; }
        public string CodeDataNameDesc { set; get; }
        public string CodeDataDesc0 { set; get; }
        public string CodeDataDesc1 { set; get; }
        public string CodeDataDesc2 { set; get; }
        public CodeTypeModel CodeTypeModel { set; get; }
    }
}
