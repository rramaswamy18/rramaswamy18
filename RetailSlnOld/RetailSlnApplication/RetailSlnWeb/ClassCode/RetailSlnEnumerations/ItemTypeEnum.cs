using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace RetailSlnEnumerations
{
    public enum ItemTypeEnum : int
    {
        [Description("Product")]
        RegularItem = 100,
        [Description("Book")]
        Book = 200,
        [Description("Bundle")]
        ItemBundle = 300,
    }
}
