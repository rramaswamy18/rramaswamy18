using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryTemplate
{
    public class TemplateModel
    {
        public long? TemplateId { set; get; }
        public string TemplateName { set; get; }
        public TemplateTypeEnum? TemplateTypeId { set; get; }
        public List<TemplateKeywordModel> TemplateKeywordModelsForTemplate { set; get; }
        public List<TemplateKeywordModel> TemplateKeywordModelsForKeyword { set; get; }
    }
}
