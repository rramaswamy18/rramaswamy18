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

        public string CategoryName { set; get; }

        [Display(Name = "Description")]
        [MaxLength(512)]
        [Required(ErrorMessage = "Please enter description for the category")]
        public string CategoryDesc { set; get; }

        [Display(Name = "Category Status")]
        public CategoryTypeEnum? CategoryTypeId { set; get; }

        [Display(Name = "Category Type")]
        public CategoryStatusEnum? CategoryStatusId { set; get; }

        [Display(Name = "Category Image")]
        public string ImageName { set; get; }

        [Display(Name = "Category Image")]
        public HttpPostedFileBase HttpPostedFileBase { get; set; }

        public string UploadImageFileName { set; get; }

        public List<CategoryModel> CategoryModels { set; get; }

        public List<ItemModel> ItemModels { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
