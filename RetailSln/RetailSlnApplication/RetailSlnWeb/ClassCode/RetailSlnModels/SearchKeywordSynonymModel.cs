using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twilio.Types;

namespace RetailSlnModels
{
    public class SearchKeywordSynonymModel : AuditInfoModel
    {
        public long SearchKeywordSynonymId { set; get; }
        public long ClientId {  set; get; }
        public long SearchKeywordId { set; get; }
        public string SearchKeywordSynonymText { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
