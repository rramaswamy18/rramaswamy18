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

        [Display(Name = "Expected avail.")]
        public string ExpectedAvailability { set; get; }

        public string ExpectedAvailabilityFormatted { set; get; }

        [Display(Name = "Select image")]
        public HttpPostedFileBase HttpPostedFileBase { get; set; }

        public string ImageName { set; get; }

        public string ImageTitle { set; get; }

        public long ItemMasterId { set; get; }

        public string ItemName { set; get; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Enter price")]
        public float? ItemRate { set; get; }

        public string ItemRateFormatted { set; get; }

        [Display(Name = "MSRP")]
        [Required(ErrorMessage = "Enter MSRP")]
        public float? ItemRateMSRP { set; get; }

        [Display(Name = "Stock status")]
        [Required(ErrorMessage = "Select stock status")]
        public ItemStockStatusEnum? ItemStockStatusId { set; get; }

        [Display(Name = "Star#")]
        //[Required(ErrorMessage = "Please enter star#")]
        public int? ItemStarCount { set; get; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Select status")]
        public ItemStatusEnum? ItemStatusId { set; get; }

        public long ProductItemId { set; get; }

        public float? QuantityOnHand { set; get; }

        public string UploadImageFileName { set; get; }

        public string ItemItemSpecsForDisplay { set; get; }

        public string ItemItemSpecsForDisplayAll { set; get; }

        public ItemMasterModel ItemMasterModel { set; get; }

        public List<ItemInfoModel> ItemInfoModels { set; get; }

        public Dictionary<string, ItemItemSpecModel> ItemItemSpecModels { set; get; }

        public List<ItemItemSpecModel> ItemItemSpecModelsList { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
