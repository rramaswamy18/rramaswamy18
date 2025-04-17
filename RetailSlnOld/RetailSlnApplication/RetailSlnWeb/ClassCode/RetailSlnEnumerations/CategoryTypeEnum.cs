using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace RetailSlnEnumerations
{
    public enum CategoryTypeEnum : int
    {
        [Description("Parent Category")]
        ParentCategory = 0,
        [Description("Regular Category")]
        RegularCategory = 100,
        [Description("Featured Item")]
        FeaturedItem = 200,
        [Description("New Arrivals")]
        NewArrivals = 300,
        [Description("Item Bundle")]
        ItemBundle = 400,
    }
}
