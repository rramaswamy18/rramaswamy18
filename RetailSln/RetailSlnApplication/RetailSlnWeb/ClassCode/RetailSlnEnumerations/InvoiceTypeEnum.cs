using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace RetailSlnEnumerations
{
    public enum InvoiceTypeEnum : int
    {
        [Description("Quotation")]
        Quotation = 100,
        [Description("Order Form")]
        OrderForm = 200,
        [Description("Tax Invoice")]
        FinalInvoice = 900,
    }
}
