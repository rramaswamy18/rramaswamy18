using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class SearchResultModel
    {
        public long CategoryCountTotal { set; get; }
        public string CurrencySymbol { set; get; }
        public List<CategoryItemMasterHierModel> CategoryCategoryItemMasterHierModels { set; get; }
        public List<CategoryItemMasterHierModel> CategoryItemMasterHierModels { set; get; }
        public Dictionary<long, ItemDiscountModel> ItemDiscountModels { set; get; }
        public long ItemMasterCountFrom { set; get; }
        public long ItemMasterCountTo { set; get; }
        public long ItemMasterCountTotal { set; get; }
        public int PageNum { set; get; }
        public string SearchKeywordText { set; get; }
        public long TotalPageCount { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
