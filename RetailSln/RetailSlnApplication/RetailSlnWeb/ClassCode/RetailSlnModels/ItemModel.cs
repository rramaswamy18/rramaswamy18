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

        public long ClientId { set; get; }

        [Display(Name = "Expected Availability")]
        public string ExpectedAvailability { set; get; }

        public string ExpectedAvailabilityFormatted { set; get; }

        [Display(Name = "Select Image")]
        public HttpPostedFileBase HttpPostedFileBase { get; set; }

        public string ImageName { set; get; }

        public string ImageTitle { set; get; }

        public YesNoEnum? ItemForSaleId { set; get; }

        public long ItemMasterId { set; get; }

        public ItemMasterModel ItemMasterModel { set; get; }

        public string ItemName { set; get; }

        [Display(Name = "Item Rate")]
        [Required(ErrorMessage = "Please enter rate")]
        public float? ItemRate { set; get; }

        public string ItemRateFormatted { set; get; }

        [Display(Name = "MSRP")]
        [Required(ErrorMessage = "Please enter MSRP")]
        public float? ItemRateMSRP { set; get; }

        public string ItemShortDesc { set; get; }

        [Display(Name = "Short Desc.")]
        [MaxLength(512, ErrorMessage = "Short desc not to exceed 512 characters")]
        [Required(ErrorMessage = "Please enter short description")]
        public string ItemShortDesc0 { set; get; }

        [Display(Name = "Short Desc.")]
        [MaxLength(512, ErrorMessage = "Short desc not to exceed 512 characters")]
        [Required(ErrorMessage = "Please enter short description")]
        public string ItemShortDesc1 { set; get; }

        [Display(Name = "Short Desc.")]
        [MaxLength(512, ErrorMessage = "Short desc not to exceed 512 characters")]
        [Required(ErrorMessage = "Please enter short description")]
        public string ItemShortDesc2 { set; get; }

        [Display(Name = "Short Desc.")]
        [MaxLength(512, ErrorMessage = "Short desc not to exceed 512 characters")]
        [Required(ErrorMessage = "Please enter short description")]
        public string ItemShortDesc3 { set; get; }

        [Display(Name = "Item Star#")]
        [Required(ErrorMessage = "Please enter star#")]
        public int? ItemStarCount { set; get; }

        [Display(Name = "Item Status")]
        [Required(ErrorMessage = "Please select a value")]
        public ItemStatusEnum? ItemStatusId { set; get; }

        [Display(Name = "Item Type")]
        [Required(ErrorMessage = "Please select a value")]
        public ItemTypeEnum? ItemTypeId { set; get; }

        public long ProductItemId { set; get; }

        public string UploadImageFileName { set; get; }

        public List<CategoryModel> CategoryModels { set; get; }

        public List<ItemInfoModel> ItemInfoModels { set; get; }

        public List<ItemImageModel> ItemImageModels { set; get; }

        public List<ItemDiscountModel> ItemDicountModels { set; get; }

        public List<ItemSpecModel> ItemItemSpecModels { set; get; }

        public List<ItemSpecModel> ItemSpecModels { set; get; }

        public Dictionary<string, ItemSpecModel> ItemItemSpecModelsForDisplay { set; get; }

        public Dictionary<string, ItemSpecModel> ItemSpecModelsForDisplay { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
