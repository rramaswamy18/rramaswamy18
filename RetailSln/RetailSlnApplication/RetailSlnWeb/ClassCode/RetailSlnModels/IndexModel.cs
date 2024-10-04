using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class IndexModel
    {
        public string ActionName { set; get; }
        public string AspNetRoleName { set; get; }
        public string ControllerName { set; get; }
        public string HtmlString { set; get; }
        public int PageNum { set; get; }
        public long ParentCategoryId { set; get; }
        public int RowCount { set; get; }
    }
}
