using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class OrderDelivery
    {
        public long OrderDeliveryId { get; set; }
        public long ClientId { set; get; }
        public string TrackingRefNumber { set; get; }
    }
}
