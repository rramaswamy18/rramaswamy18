using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace RetailSlnEnumerations
{
    public enum InvoiceTypeEnum : int
    {
        [Description("Tax Invoice")]
        TaxInvoice = 100,
        [Description("Order Form")]
        OrderForm = 200,
    }
}
