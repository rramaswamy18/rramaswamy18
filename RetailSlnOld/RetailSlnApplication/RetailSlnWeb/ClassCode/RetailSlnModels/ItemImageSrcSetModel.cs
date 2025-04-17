using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ItemImageSrcSetModel : AuditInfoModel
    {
        public long? ItemImageSrcSetId { set; get; }

        public int ImageHeight { set; get; }

        public string ImageHeightUnit { set; get; }

        public string ImageName { set; get; }

        public int ImageWidth { set; get; }

        public string ImageWidthUnit { set; get; }

        public long ItemImageId { set; get; }

        public float SeqNum { set; get; }
    }
}
