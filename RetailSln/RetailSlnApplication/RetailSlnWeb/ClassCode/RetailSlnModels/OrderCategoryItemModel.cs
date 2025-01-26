using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class OrderCategoryItemModel
    {
        public int CategoryCount { set; get; }
        public List<CategoryItemMasterHierModel> CategoryItemMasterHierModels { set; get; }
        public List<CategoryModel> CategoryModels { set; get; }
        public string OrderCreatedForEmailAddress { set; get; }
        public long? OrderCreatedForPersonId { set; get; }
        public long ParentCategoryId { set; get; }
        public CategoryModel ParentCategoryModel { set; get; }
        public int PageCount { set; get; }
        public int PageNum { set; get; }
        public int PageSize { set; get; }
        public int TotalRowCount { set; get; }
        public string ViewName { set; get; }
        public UserAddEditModel UserAddEditModel { set; get; }
    }
}
