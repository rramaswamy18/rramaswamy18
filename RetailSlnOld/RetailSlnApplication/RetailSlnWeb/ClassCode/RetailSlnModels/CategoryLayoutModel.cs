using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class CategoryLayoutModel
    {
        public CategoryModel FeaturedItemsCategoryModel { set; get; }
        public List<CategoryModel> CategoryModels { set; get; }
        public long ParentCategoryId { set; get; }
        public CategoryModel ParentCategoryModel { set; get; }
        public List<ItemMasterModel> ItemMasterModels { set; get; }
        public List<ItemModel> ItemModels { set; get; }
        public List<CategoryItemMasterHierModel> CategoryItemMasterHierModels { set; get; }
        public List<CategoryItemHierModel> CategoryItemHierModels { set; get; }
    }
}
