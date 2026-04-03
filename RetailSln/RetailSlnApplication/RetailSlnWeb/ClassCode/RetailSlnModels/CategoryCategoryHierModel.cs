using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class CategoryCategoryHierModel : AuditInfoModel
    {
        public long CategoryCategoryHierId { set; get; }
        public long ClientId { set; get; }
        public long CategoryId { set; get; }
        public long ParentCategoryId { set; get; }
        public float SeqNum { set; get; }
        public CategoryModel CategoryModel { set; get; }
        public CategoryModel ParentCategoryModel { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
