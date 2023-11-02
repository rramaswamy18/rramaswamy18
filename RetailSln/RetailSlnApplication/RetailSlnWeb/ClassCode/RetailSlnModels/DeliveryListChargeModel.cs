using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class DeliveryListChargeModel
    {
        public long DeliveryListChargeId { set; get; }

        public long ClientId { set; get; }

        public long DeliveryListId { set; get; }

        public string ChargeTypeDesc {  set; get; }

        public string ChargeTypeNameDesc { set; get; }

        public string ChargeValueType {  set; get; }

        public float ChargeValue { set; get; }
    }
}