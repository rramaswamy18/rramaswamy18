using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class AddToCartModel
    {
        public long? ParentCategoryId { set; get; }
        public int? PageNum { set; get; }
        public int? PageSize { set; get; }
        public int? TotalRowCount { set; get; }
        public List<ShoppingCartItemModel> ShoppingCartItemModels { set; get; }
    }
}
