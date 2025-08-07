using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ItemCatalogFileModel
    {
        public string BannerImageUrl { set; get; }

        public List<CategoryItemMasterHierModel> CategoryCategoryItemMasterHierModels { set; get; }

        public List<CategoryItemMasterHierModel> CategoryItemMasterHierModels { set; get; }

        public string CurrencySymbol { set; get; }

        public Dictionary<long, ItemDiscountModel> ItemDiscountModels { set; get; }

        public long ItemMasterCount { set; get; }

        public long ItemCount { set; get; }

        public string ParentCategoryDesc { set; get; }

        public long? ParentCategoryId { set; get; }

        public bool PDFFlag { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
