using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class OrderListModel
    {
        public long OrderHeaderId { set; get; }
        public OrderHeader OrderHeader { set; get; }
        public OrderHeaderSummary OrderHeaderSummary { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
