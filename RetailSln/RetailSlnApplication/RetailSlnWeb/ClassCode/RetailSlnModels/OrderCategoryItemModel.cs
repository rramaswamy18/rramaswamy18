using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class OrderCategoryItemModel
    {
        public long ParentCategoryId { set; get; }
        public int PageNum { set; get; }
        public int PageSize { set; get; }
        public long TotalRowCount { set; get; }
    }
}
