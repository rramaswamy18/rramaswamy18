using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class SearchResultModel
    {
        public string SearchKeywordText { set; get; }
        public List<SearchMetaDataModel> SearchMetaDataModels { set; get; }
        public CategoryListModel CategoryListModel { set; get; }
        public ItemMasterListModel ItemMasterListModel { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
