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
        public string CategoryOrItem { set; get; }
        public long CorpAcctId { set; get; }
        //public List<CategoryItemMasterHierNewModel> CategoryItemMasterHierNewModels { set; get; }
        public List<ItemMasterModel> ItemMasterModels { set; get; }
        public int ImageCountPerRow { set; get; }
        public string ImageDivWidth1 { set; get; }
        public string ImageHeight1 { set; get; }
        public string ImageWidth1 { set; get; }
        public string ImageDivWidth2 { set; get; }
        public string ImageHeight2 { set; get; }
        public string ImageWidth2 { set; get; }
        public Dictionary<long, ItemDiscountModel> ItemDiscountModels { set; get; }
        public long PageCount { set; get; }
        public long PageNum { set; get; }
        public long PageSize { set; get; }
        public long ParentCategoryId { set; get; }
        public CategoryModel ParentCategoryModel { set; get; }
        public long TotalRowCount { set; get; }
        public string ViewName { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
