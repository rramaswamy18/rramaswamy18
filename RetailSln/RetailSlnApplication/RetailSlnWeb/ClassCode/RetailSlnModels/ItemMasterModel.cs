﻿using ArchitectureLibraryModels;
using RetailSlnEnumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ItemMasterModel
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

        //public string ItemMasterSpecsForDisplay { set; get; }

        public string ItemRatesForDisplay { set; get; }

        public string ItemRatesForDisplayAll { set; get; }

        //public List<ItemImageModel> ItemMasterImageModels { set; get; }

        //public List<ItemMasterInfoModel> ItemMasterInfoModels { set; get; }

        //public List<ItemSpecModel> ItemMasterItemSpecModels { set; get; }

        //public Dictionary<string, ItemSpecModel> ItemMasterItemSpecModelsForDisplay { set; get; }

        [Display(Name = "Item Type")]
        [Required(ErrorMessage = "Please select a value")]
        public ItemTypeEnum? ItemTypeId { set; get; }

        public long? ProductItemId { set; get; }

        public string UploadImageFileName { set; get; }

        public Dictionary<string, List<CategoryModel>> AspNetRoleNameCategoryModels { set; get; }

        public List<ItemModel> ItemModels { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
