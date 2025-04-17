using ArchitectureLibraryModels;
using RetailSlnEnumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ApiItemMasterModel
    {
        public long ItemMasterId { set; get; }

        public long ClientId { set; get; }

        public string ImageExtension { set; get; }

        public string ImageName { set; get; }

        public string ImageTitle { set; get; }

        public string ItemMasterDesc { set; get; }

        public string ItemMasterDesc0 { set; get; }

        public string ItemMasterDesc1 { set; get; }

        public string ItemMasterDesc2 { set; get; }

        public string ItemMasterDesc3 { set; get; }

        public string ItemMasterName { set; get; }

        public ItemTypeEnum? ItemTypeId { set; get; }

        public long? ProductItemId { set; get; }

        public string UploadImageFileName { set; get; }

        public List<ApiItemModel> ApiItemModels { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
