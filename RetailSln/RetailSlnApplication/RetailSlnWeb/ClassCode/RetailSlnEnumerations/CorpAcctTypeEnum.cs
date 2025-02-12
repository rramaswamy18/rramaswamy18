using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace RetailSlnEnumerations
{
    public enum CorpAcctTypeEnum
    {
        [Description("Individual")]
        Individual = 100,
        [Description("Retail / Wholesale")]
        Wholesale = 500,
        [Description("Temple / Bulk Orders")]
        BulkOrder = 600,
        [Description("Priest")]
        Priest = 700,
    }
}
