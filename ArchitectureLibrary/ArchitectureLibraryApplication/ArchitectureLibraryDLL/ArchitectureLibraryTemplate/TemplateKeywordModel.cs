using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryTemplate
{
    public class TemplateKeywordModel
    {
        public long? TemplateKeywordId { set; get; }
        public long? KeywordId { set; get; }
        public float? SeqNum { set; get; }
        public KeywordTypeEnum KeywordTypeId { set; get; }
        public string TemplateDataValue { set; get; }
        public long TemplateId { set; get; }
        public KeywordModel KeywordModel { set; get; }
    }
}
