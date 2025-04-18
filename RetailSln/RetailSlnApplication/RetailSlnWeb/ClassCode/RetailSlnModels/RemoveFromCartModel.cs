using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class RemoveFromCartModel
    {
        public int? PageNum { set; get; }
        public int? PageSize { set; get; }
        public long? ParentCategoryId { set; get; }
        public int? RemoveFromCartIndex { set; get; }
        public int? TotalRowCount { set; get; }
    }
}
