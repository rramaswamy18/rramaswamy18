using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class OrderListModel
    {
        public List<OrderHeader> OrderHeaders { set; get; }
        public List<OrderHeaderSummary> OrderHeaderSummarys { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
