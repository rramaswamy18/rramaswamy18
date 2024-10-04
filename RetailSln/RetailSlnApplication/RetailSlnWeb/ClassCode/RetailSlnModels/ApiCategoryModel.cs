using ArchitectureLibraryModels;
using RetailSlnEnumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ApiCategoryModel
    {
        public long? CategoryId { set; get; }

        public long? ClientId { set; get; }

        public bool AssignSubCategory { set; get; }

        public bool AssignItem { set; get; }

        public string CategoryName { set; get; }

        public string CategoryNameDesc { set; get; }

        public string CategoryDesc { set; get; }

        public CategoryStatusEnum? CategoryStatusId { set; get; }

        public bool DefaultCategory { set; get; }

        public string ImageExtension { set; get; }

        public string ImageName { set; get; }

        public short MaxPerPage { set; get; }

        public string UploadImageFileName { set; get; }

        public string ViewName { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
