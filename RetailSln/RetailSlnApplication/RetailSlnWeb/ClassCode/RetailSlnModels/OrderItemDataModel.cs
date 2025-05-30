using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class OrderItemDataModel
    {
        public string OrderItemDataHtmlFileName { set; get; }

        public string OrderItemDataHtmlString { set; get; }

        public string OrderItemDataPdfFileName { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
