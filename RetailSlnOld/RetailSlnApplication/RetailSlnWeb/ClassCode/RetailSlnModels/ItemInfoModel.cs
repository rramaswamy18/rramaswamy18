using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RetailSlnModels
{
    public class ItemInfoModel : AuditInfoModel
    {
        public long? ItemInfoId { set; get; }

        public long ClientId { set; get; }

        public long ItemId { set; get; }

        [Display(Name = "Spec Label")]
        [MaxLength(50, ErrorMessage = "Item spec label cannot exceed 50 characters")]
        [Required(ErrorMessage = "Enter spec label")]
        public string ItemInfoLabelText { set; get; }

        [AllowHtml]
        [Display(Name = "Spec(s)")]
        [MaxLength(12288, ErrorMessage = "Item spec text cannot exceed 12288 (12K) characters inclusive of tags")]
        [Required(ErrorMessage = "Enter spec(s)")]
        public string ItemInfoText { set; get; }

        [Display(Name = "Seq#")]
        [Range(1, 999.999)]
        [RegularExpression(@"^\d+(\.\d{1,3})?$", ErrorMessage = "Seq# to be between 1 & 999.999 with 3 digits after decimal (period)")]
        [Required(ErrorMessage = "Enter seq#")]
        public float SeqNum { set; get; }

        public float? SeqNumItem { set; get; }

        public float? SeqNumItemMaster { set; get; }

        public ItemModel ItemModel { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
