using ArchitectureLibraryModels;
using RetailSlnEnumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RetailSlnModels
{
    public class CategoryModel
    {
        public long? CategoryId { set; get; }

        public long? ClientId { set; get; }

        public bool AssignSubCategory { set; get; }

        public bool AssignItem { set; get; }

        public string CategoryName { set; get; }

        public string CategoryNameDesc { set; get; }

        [Display(Name = "Description")]
        [MaxLength(512)]
        [Required(ErrorMessage = "Please enter description for the category")]
        public string CategoryDesc { set; get; }

        [Display(Name = "Category Status")]
        public CategoryStatusEnum? CategoryStatusId { set; get; }

        [Display(Name = "Category Type")]
        public CategoryTypeEnum? CategoryTypeId { set; get; }

        [Display(Name = "Category Image")]

        public bool DefaultCategory { set; get; }

        public HttpPostedFileBase HttpPostedFileBase { get; set; }

        public string ImageExtension { set; get; }

        [Display(Name = "Category Image")]
        public string ImageName { set; get; }

        public short MaxPerPage { set; get; }

        public string UploadImageFileName { set; get; }

        public string ViewName { set; get; }

        public List<CategoryModel> CategoryModels { set; get; }

        public List<ItemModel> ItemModels { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
