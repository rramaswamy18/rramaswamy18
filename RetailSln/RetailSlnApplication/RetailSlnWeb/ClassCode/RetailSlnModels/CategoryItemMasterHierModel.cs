using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class CategoryItemMasterHierModel : AuditInfoModel
    {
        public long? CategoryItemMasterHierId { set; get; }
        public long ClientId { set; get; }
        public string AspNetRoleName { set; get; }
        public long ItemMasterId { set; get; }
        public long ParentCategoryId { set; get; }
        public float SeqNum { set; get; }
        public ItemMasterModel ItemMasterModel { set; get; }
        public CategoryModel ParentCategoryModel { set; get; }
        public List<CategoryItemHierModel> CategoryItemHierModels { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
