using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace RetailSlnModels
{
    public class OrderItemModel
    {
        public string AspNetRoleName { set; get; }
        public List<CategoryItemMasterHierModel> CategoryItemMasterHierModels { set; get; }
        public long PageCount { set; get; }
        public long PageNum { set; get; }
        public long PageSize { set; get; }
        public long ParentCategoryId { set; get; }
        //public ShoppingCartModel ShoppingCartModel { set; get; }
        public long TotalRowCount { set; get; }
        public string ViewName { set; get; }
        public CategoryModel ParentCategoryModel { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
