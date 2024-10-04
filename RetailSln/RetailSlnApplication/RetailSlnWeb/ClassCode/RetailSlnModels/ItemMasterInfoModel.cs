using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RetailSlnModels
{
    public class ItemMasterInfoModel : AuditInfoModel
    {
        public long? ItemMasterInfoId { set; get; }

        public long ClientId { set; get; }

        public long ItemMasterId { set; get; }

        [Display(Name = "Label")]
        [MaxLength(50, ErrorMessage = "Label cannot exceed 50 characters")]
        //[Required(ErrorMessage = "Enter label")]
        public string ItemMasterInfoLabelText { set; get; }

        [AllowHtml]
        [Display(Name = "Text")]
        [MaxLength(12288, ErrorMessage = "Text cannot exceed 12288 (12K) characters inclusive of tags")]
        //[Required(ErrorMessage = "Enter text")]
        public string ItemMasterInfoText { set; get; }

        [Display(Name = "Seq#")]
        [Range(1, 999.999)]
        //[RegularExpression(@"^\d+(\.\d{1,3})?$", ErrorMessage = "Seq# to be can be decimal")]
        [Required(ErrorMessage = "Enter seq#")]
        public float? SeqNum { set; get; }

        public ItemMasterModel ItemMasterModel { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
