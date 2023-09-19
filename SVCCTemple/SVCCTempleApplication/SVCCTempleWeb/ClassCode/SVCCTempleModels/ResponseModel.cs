using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleModels
{
    public class ResponseModel
    {
        public string ResponseMessagesHtml { set; get; }
        public List<KeyValuePair<string, string>> ResponseMessagesData { set; get; }
        public List<KeyValuePair<string, List<string>>> ResponseMessagesError { set; get; }
    }
}
