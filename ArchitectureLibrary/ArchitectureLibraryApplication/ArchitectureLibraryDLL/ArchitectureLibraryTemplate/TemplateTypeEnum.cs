using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryTemplate
{
    public enum TemplateTypeEnum
    {
        [Description("Template Type")]
        EmailTemplate = 100,
        [Description("Keyword Type")]
        WordTemplate = 200,
    }
}
