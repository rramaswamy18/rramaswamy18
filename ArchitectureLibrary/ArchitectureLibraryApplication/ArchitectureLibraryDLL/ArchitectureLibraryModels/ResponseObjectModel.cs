using ArchitectureLibraryEnumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArchitectureLibraryModels
{
    public class ResponseObjectModel
    {
        public int ColumnCount { get; set; }
        public string ListStyleType { set; get; }
        public List<KeyValuePair<string, List<string>>> PropertyErrorsKVP { set; get; }
        public List<string> ResponseMessages { set; get; }
        public ResponseTypeEnum? ResponseTypeId { set; get; }
        public string TextAlign { set; get; }
        public string TextColor { set; get; }
        public string ValidationSummaryMessage { set; get; }
    }
}
