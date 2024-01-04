using ArchitectureLibraryEnumerations;
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
    public class ItemModel
    {
        public long? ItemId { set; get; }

        [Display(Name = "Expected Availability")]
        public string ExpectedAvailability { set; get; }

        [Display(Name = "Select Image")]
        public HttpPostedFileBase HttpPostedFileBase { get; set; }

        public string ImageName { set; get; }

        [Display(Name = "Description")]
        [MaxLength(1024, ErrorMessage = "Desc not to exceed 1024 characters")]
        [Required(ErrorMessage = "Please enter description")]
        public string ItemDesc { set; get; }

        public string ItemDescAttrib { set; get; }

        public string ItemName { set; get; }

        [Display(Name = "Item Rate")]
        [Required(ErrorMessage = "Please enter rate")]
        public float? ItemRate { set; get; }

        [Display(Name = "MSRP")]
        [Required(ErrorMessage = "Please enter MSRP")]
        public float? ItemRateMSRP { set; get; }

        [Display(Name = "Short Desc.")]
        [MaxLength(512, ErrorMessage = "Short desc not to exceed 512 characters")]
        [Required(ErrorMessage = "Please enter short description")]
        public string ItemShortDesc { set; get; }

        [Display(Name = "Item Star#")]
        [Required(ErrorMessage = "Please enter star#")]
        public int? ItemStarCount { set; get; }

        [Display(Name = "Item Status")]
        [Required(ErrorMessage = "Please select a value")]
        public ItemStatusEnum? ItemStatusId { set; get; }

        [Display(Name = "Item Type")]
        [Required(ErrorMessage = "Please select a value")]
        public ItemTypeEnum? ItemTypeId { set; get; }

        public string UploadImageFileName { set; get; }

        public List<CategoryModel> CategoryModels { set; get; }

        public List<ItemAttribModel> ItemAttribModels { set; get; }

        public List<ItemModel> ItemModels { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
