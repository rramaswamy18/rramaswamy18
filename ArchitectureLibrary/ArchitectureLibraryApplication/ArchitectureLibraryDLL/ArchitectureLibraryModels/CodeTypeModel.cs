using ArchitectureLibraryEnumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryModels
{
    public class CodeTypeModel : AuditInfoModel
    {
        public long CodeTypeId { set; get; }
        public CodeTypeEnum CodeTypeNameId { set; get; }
        public string CodeTypeNameDesc { set; get; }
        public string CodeTypeDesc { set; get; }
        public List<CodeDataModel> CodeDataModelsCodeDataId { set; get; }
        public List<CodeDataModel> CodeDataModelsCodeDataNameId { set; get; }
        public List<CodeDataModel> CodeDataModelsCodeDataNameDesc { set; get; }
    }
}
