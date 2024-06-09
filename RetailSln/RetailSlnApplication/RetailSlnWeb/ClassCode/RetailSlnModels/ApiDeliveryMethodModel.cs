using RetailSlnEnumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ApiDeliveryMethodModel
    {
        public string DeliveryMethodDesc { set; get; }
        public DeliveryMethodEnum? DeliveryMethodId { set; get; }
        public string DeliveryMethodName { set; get; }
    }
}
