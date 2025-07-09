using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class OrderItemFileModel
    {
        public List<CategoryItemMasterHierModel> CategoryCategoryItemMasterHierModels { set; get; }

        public List<CategoryItemMasterHierModel> CategoryItemMasterHierModels { set; get; }

        public long? ParentCategoryId { set; get; }

        public string ParentCategoryDesc { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
