using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class CategoryItemListModel
    {
        public CategoryModel ParentCategoryModel { set; get; }
        public List<long> Assigned { set; get; }
        public List<long> Unassigned { set; get; }
        public List<ItemModel> ItemModelsAssigned { set; get; }
        public List<ItemModel> ItemModelsUnassigned { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
