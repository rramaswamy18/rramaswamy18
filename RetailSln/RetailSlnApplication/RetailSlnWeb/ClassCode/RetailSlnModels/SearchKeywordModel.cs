using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class SearchKeywordModel
    {
        public long SearchKeywordId { set; get; }
        public long ClientId { set; get; }
        public string SearchKeywordText { set; get; }
        public long CategoryCount { set; get; }
        public long ItemMasterCount { set; get; }
        public List<SearchMetaDataModel> SearchMetaDataModels { set; get; }
        public List<SearchKeywordSynonymModel> SearchKeywordSynonymModels { set; get; }
    }
}
