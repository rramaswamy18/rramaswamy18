using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RetailSlnModels
{
    public class ItemSpecModel : AuditInfoModel
    {
        public long? ItemSpecId { set; get; }

        public long ClientId { set; get; }

        public long ItemId { set; get; }

        [Display(Name = "Spec Label")]
        [MaxLength(50, ErrorMessage = "Item spec label cannot exceed 50 characters")]
        [Required(ErrorMessage = "Enter spec label")]
        public string ItemSpecLabelText { set; get; }

        [AllowHtml]
        [Display(Name = "Spec(s)")]
        [MaxLength(10240, ErrorMessage = "Item spec text cannot exceed 10240 characters inclusinve of tags")]
        [Required(ErrorMessage = "Enter spec(s)")]
        public string ItemSpecText { set; get; }

        [Display(Name = "Seq#")]
        [Range(1, 999.999)]
        [RegularExpression(@"^\d+(\.\d{1,3})?$", ErrorMessage = "Seq# to be between 1 & 999.999 with 3 digits after decimal (period)")]
        [Required(ErrorMessage = "Enter seq#")]
        public float SeqNum { set; get; }

        public ItemModel ItemModel { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
