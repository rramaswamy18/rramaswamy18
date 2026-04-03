using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace RetailSlnEnumerations
{
    public enum ItemStockStatusEnum : int
    {
        [Description("In Stock")]
        InStock = 100,
        [Description("Out of Stock")]
        OutOfStock = 200,
    }
}
