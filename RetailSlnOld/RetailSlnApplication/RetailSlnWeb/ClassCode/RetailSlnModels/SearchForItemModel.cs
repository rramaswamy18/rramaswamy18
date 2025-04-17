using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class SearchForItemModel
    {
        public long ParentCategoryId { set; get; }
        public CategoryModel ParentCategoryModel { set; get; }
        public List<ItemMasterModel> ItemMasterModels { set; get; }
        public string SearchText { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
