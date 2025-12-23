using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class OrderInvoiceModel
    {
        public string InvoiceFileNamePdf { set; get; }
        public string InvoiceFullFileNamePdf { set; get; }
        public string InvoiceHtmlString { set; get; }
        public long OrderHeaderId { set; get; }
    }
}
