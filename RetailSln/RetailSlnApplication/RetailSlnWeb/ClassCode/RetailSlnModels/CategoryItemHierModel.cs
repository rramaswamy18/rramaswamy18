using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class CategoryItemHierModel : AuditInfoModel
    {
        public long? CategoryItemHierId { set; get; }
        public long? ClientId { set; get; }
        public long? ParentCategoryId { set; get; }
        public float SeqNum { set; get; }
        public long? CategoryId { set; get; }
        public long? ItemId { set; get; }
        public string ProcessType { set; get; }
        public string CategoryOrItem { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
        public CategoryModel ParentCategoryModel { set; get; }
        public CategoryModel CategoryModel { set; get; }
        public ItemModel ItemModel { set; get; }
    }
}
