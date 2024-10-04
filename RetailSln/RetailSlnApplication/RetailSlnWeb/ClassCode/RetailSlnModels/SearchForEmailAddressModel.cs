using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class SearchForEmailAddressModel
    {
        public string SearchText { set; get; }
        public List<SearchForEmailAddressDataModel> SearchForEmailAddressDataModels { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
