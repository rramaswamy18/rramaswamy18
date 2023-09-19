using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleModels
{
    public class ValidationSummaryModel
    {
        public List<KeyValuePair<string, List<string>>> ResponseMessagesError { set; get; }
        public string ValidationSummaryPropertiesHtml { set; get; }
        public string ValidationSummaryMessage { set; get; }
    }
}
