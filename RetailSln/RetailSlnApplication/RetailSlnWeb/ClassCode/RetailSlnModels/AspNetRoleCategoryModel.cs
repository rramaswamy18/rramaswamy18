using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class AspNetRoleCategoryModel : AuditInfoModel
    {
        public long AspNetRoleCategoryId { set; get; }
        public long ClientId { set; get; }
        public string AspNetRoleName { set; get; }
        public long CategoryId { set; get; }
        public CategoryModel CategoryModel { set; get; }
    }
}
