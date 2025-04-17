using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace RetailSlnEnumerations
{
    public enum CategoryStatusEnum : int
    {
        [Description("Active")]
        Active = 100,
        [Description("Mark to be deleted")]
        MarkedForDeletion = 800,
        [Description("Inactive")]
        Inactive = 900,
    }
}
