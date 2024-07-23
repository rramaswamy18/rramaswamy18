using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class CategoryItemHierListModel
    {
        public long ParentCategoryId { set; get; }
        public List<CategoryItemHierModel> CategoryItemHierModels { set; get; }
        public List<CategoryModel> CategoryModels { set; get; }
        public List<ItemModel> ItemModels { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
