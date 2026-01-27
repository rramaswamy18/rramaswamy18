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

        public string CatalogMessage { set; get; }

        public string CatalogMessageForeColor { set; get; }

        public List<CategoryItemMasterHierModel> CategoryCategoryItemMasterHierModels { set; get; }

        public List<CategoryItemMasterHierModel> CategoryItemMasterHierModels { set; get; }

        public string CurrencySymbol { set; get; }

        public long ItemCount { set; get; }

        public Dictionary<long, ItemDiscountModel> ItemDiscountModels { set; get; }

        public long ItemMasterCount { set; get; }

        public long ItemMasterCountFrom { set; get; }

        public long ItemMasterCountTo { set; get; }

        public long ItemMasterCountTotal { set; get; }

        public int PageNum { set; get; }

        public string ParentCategoryDesc { set; get; }

        public long? ParentCategoryId { set; get; }

        public bool PdfFlag { set; get; }

        public bool SearchFlag { set; get; }

        public long TotalPageCount { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
