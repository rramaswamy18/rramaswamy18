using ArchitectureLibraryEnumerations;
using ArchitectureLibraryModels;
using RetailSlnEnumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ItemMasterModel
    {
        public long ItemMasterId { set; get; }

        public long ClientId { set; get; }

        public string ImageExtension { set; get; }

        public string ImageName { set; get; }

        [Display(Name = "Item Image")]
        [Required(ErrorMessage = "Select valid image")]
        public HttpPostedFileBase ImageNameHttpPostedFileBase { get; set; }

        public string ImageTitle { set; get; }

        public string ItemMasterDesc { set; get; }

        [Display(Name = "Description 0")]
        [Required(ErrorMessage = "Enter valid description 0")]
        [MinLength(1)]
        [MaxLength(512)]
        public string ItemMasterDesc0 { set; get; }

        [Display(Name = "Description 1")]
        [Required(ErrorMessage = "Enter valid description 1")]
        [MinLength(1)]
        [MaxLength(512)]
        public string ItemMasterDesc1 { set; get; }

        [Display(Name = "Description 2")]
        //[Required(ErrorMessage = "Enter valid description 2")]
        //[MinLength(1)]
        //[MaxLength(512)]
        public string ItemMasterDesc2 { set; get; }

        [Display(Name = "Description 3")]
        //[Required(ErrorMessage = "Enter valid description 3")]
        //[MinLength(1)]
        //[MaxLength(512)]
        public string ItemMasterDesc3 { set; get; }

        public string ItemMasterName { set; get; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Select item status")]
        public YesNoEnum? ItemMasterStatusId { set; get; }

        public string ItemRatesForDisplay { set; get; }

        public string ItemRatesForDisplayAll { set; get; }

        [Display(Name = "Item Type")]
        [Required(ErrorMessage = "Select item type")]
        public ItemTypeEnum? ItemTypeId { set; get; }

        public long? ProductItemId { set; get; }

        public string UploadImageFileName { set; get; }

        public Dictionary<string, List<CategoryModel>> AspNetRoleNameCategoryModels { set; get; }

        public string CategoryIds { set; get; }

        public string ItemMasterItemSpecsForDisplay { set; get; }

        public string ItemMasterItemSpecsForDisplayAll { set; get; }

        public Dictionary<string, ItemMasterItemSpecModel> ItemMasterItemSpecModels { set; get; }

        public List<ItemModel> ItemModels { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
