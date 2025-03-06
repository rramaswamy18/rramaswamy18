using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace RetailSlnEnumerations
{
    public enum InvoiceTypeEnum : int
    {
        [Description("Order Form")]
        OrderForm = 100,
        [Description("Tax Invoice")]
        TaxInvoice = 200,
    }
}
