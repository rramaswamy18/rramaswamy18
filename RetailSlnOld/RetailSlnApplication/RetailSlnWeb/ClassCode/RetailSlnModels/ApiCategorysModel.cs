using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ApiCategorysModel
    {
        public string AspNetRoleName { set; get; }
        public long ParentCategoryId { set; get; }
        public List<ApiCategoryModel> ApiCategoryModels { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
