using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace RetailSlnEnumerations
{
    public enum ItemStatusEnum : int
    {
        [Description("Active")]
        Active = 100,
        [Description("Inactive")]
        Inactive = 200,
    }
}
