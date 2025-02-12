using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class SearchForUserModel
    {
        public List<SearchForUserDataModel> SearchForUserDataModels { set; get; }
        public string SearchText { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
