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
        Processed = 500,
        [Description("On Hold")]
        OnHold = 600,
        [Description("Canceled")]
        Canceled = 700,
    }
}
