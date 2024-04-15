using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace RetailSlnEnumerations
{
    public enum CorpAcctTypeEnum : int
    {
        [Description("Individual (Retail)")]
        Individual = 100,
        [Description("Wholesale")]
        Wholesale = 200,
        [Description("Bulk Orders")]
        BulkOrders = 300,
        [Description("Priest")]
        Priest = 400,
    }
}
