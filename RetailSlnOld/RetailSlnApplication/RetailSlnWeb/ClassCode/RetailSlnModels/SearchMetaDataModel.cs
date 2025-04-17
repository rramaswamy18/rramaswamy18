using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class SearchMetaDataModel
    {
        public long SearchMetaDataId { set; get; }
        public long ClientId { set; get; }
        public long SearchKeywordId { set; get; }
        public string EntityTypeNameDesc { set; get; }
        public long EntityId { set; get; }
        public float SeqNum { set; get; }
        public SearchKeywordModel SearchKeywordModel { set; get; }
        public CategoryModel CategoryModel { set; get; }
        public ItemModel ItemModel { set; get; }
    }
}
