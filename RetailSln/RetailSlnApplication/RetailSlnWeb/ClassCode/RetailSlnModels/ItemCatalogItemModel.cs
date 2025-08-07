using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ItemCatalogItemModel
    {
        public Dictionary<long, ItemDiscountModel> ItemDiscountModels { set; get; }

        public string ItemCatalogHtmlFileName { set; get; }

        public string ItemCatalogHtmlString { set; get; }

        public string ItemCatalogPdfFileName { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }


    }
}
