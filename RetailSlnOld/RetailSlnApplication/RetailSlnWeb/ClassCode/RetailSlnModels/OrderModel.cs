using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class OrderModel
    {
        public OrderHeaderModel OrderHeaderModel { set; get; }
        public DeliveryInfoModel DeliveryInfoModel { set; get; }
    }
}
