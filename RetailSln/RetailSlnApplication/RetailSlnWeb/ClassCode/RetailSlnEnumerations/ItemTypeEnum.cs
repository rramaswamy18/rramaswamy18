using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace RetailSlnEnumerations
{
    public enum ItemTypeEnum : int
    {
        [Description("Regular Item")]
        RegularItem = 100,
        [Description("Item Bundle")]
        ItemBundle = 200,
    }
}
