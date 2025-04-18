using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace RetailSlnEnumerations
{
    public enum OrderStatusEnum :int
    {
        [Description("Order Placed")]
        Open = 100,
        [Description("Order Viewed")]
        Viewed = 200,
        [Description("In Process")]
        InProcess = 300,
        [Description("In Transit")]
        InTransit = 400,
        [Description("Delivered")]
        Delivered = 500,
        [Description("Partial Payment")]
        PartialPayment = 800,
        [Description("Paid")]
        Paid = 900,
        [Description("On Hold")]
        OnHold = 9000,
        [Description("Canceled")]
        Canceled = 9900,
    }
}
