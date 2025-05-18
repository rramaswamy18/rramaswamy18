using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class OrderListModel
    {
        public List<OrderListDataModel> OrderListDataModels { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
