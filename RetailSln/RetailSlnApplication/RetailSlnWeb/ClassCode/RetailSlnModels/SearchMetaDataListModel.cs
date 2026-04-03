using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class SearchMetaDataListModel
    {
        public List<SearchMetaDataModel> SearchMetaDataModels { set; get; }
        public PaginationModel PaginationModel { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
