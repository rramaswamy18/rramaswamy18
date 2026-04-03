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

        [Display(Name = "Label")]
        [MaxLength(50, ErrorMessage = "Label max 50 char(s)")]
        //[Required(ErrorMessage = "Enter label")]
        public string ItemMasterInfoLabelText { set; get; }

        [AllowHtml]
        [Display(Name = "Text")]
        [MaxLength(12288, ErrorMessage = "Text max 12288 (12K) char(s)")]
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
