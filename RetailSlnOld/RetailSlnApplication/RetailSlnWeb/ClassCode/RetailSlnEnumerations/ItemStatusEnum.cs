using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace RetailSlnEnumerations
{
    public enum ItemStatusEnum : int
    {
        [Description("In Stock")]
        InStock = 100,
        [Description("Out of Stock")]
        OutOfStock = 200,
        [Description("Mark to be deleted")]
        MarkedForDeletion = 800,
        [Description("Inactive")]
        Inactive = 900,
    }
}
