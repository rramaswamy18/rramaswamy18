using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ItemImageModel : AuditInfoModel
    {
        public long? ItemImageId { set; get; }

        [Display(Name = "Select Image")]
        public HttpPostedFileBase HttpPostedFileBase { get; set; }

        public string ImageDesc { set; get; }

        public long ItemId { set; get; }

        public float SeqNum { set; get; }

        public ItemModel ItemModel { set; get; }

        public List<ItemImageSrcSetModel> ItemImageSrcSetModels { set; get; }
    }
}
