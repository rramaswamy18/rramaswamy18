using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class CategoryItemLayoutModel
    {
        public string AspNetRoleName { set; get; }
        public List<CategoryModel> CategoryModels { set; get; }
        public CategoryItemHierModel CategoryItemHierModel { set; get; }
        public ItemMasterModel ItemMasterModel { set; get; }
        public List<ItemModel> ItemModels { set; get; }
        public long ParentCategoryId { set; get; }
        public CategoryModel ParentCategoryModel { set; get; }
    }
}
