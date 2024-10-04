using ArchitectureLibraryEnumerations;
using ArchitectureLibraryModels;
using RetailSlnEnumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ApiItemModel
    {
        public long? ItemId { set; get; }

        public long ClientId { set; get; }

        public string ExpectedAvailability { set; get; }

        public string ExpectedAvailabilityFormatted { set; get; }

        public string ImageName { set; get; }

        public string ImageTitle { set; get; }

        public YesNoEnum? ItemForSaleId { set; get; }

        public long ItemMasterId { set; get; }

        public string ItemName { set; get; }

        public float? ItemRate { set; get; }

        public string ItemRateFormatted { set; get; }

        public float? ItemRateMSRP { set; get; }

        public string ItemShortDesc { set; get; }

        public string ItemShortDesc0 { set; get; }

        public string ItemShortDesc1 { set; get; }

        public string ItemShortDesc2 { set; get; }

        public string ItemShortDesc3 { set; get; }

        public int? ItemStarCount { set; get; }

        public ItemStatusEnum? ItemStatusId { set; get; }

        public ItemTypeEnum? ItemTypeId { set; get; }

        public long ProductItemId { set; get; }

        public string UploadImageFileName { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
