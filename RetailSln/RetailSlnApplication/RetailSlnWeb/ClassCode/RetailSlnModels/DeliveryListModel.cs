using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class DeliveryListModel
    {
        public List<DeliveryListChargeModel> DeliveryListChargeModel { set; get; }

        public List<DeliveryChargeModel> DeliveryChargeModel { set; get; }

        public long DeliveryListId { set; get; }

        public long ClientId { set; get; }

        public string DeliveryListIName { set; get; }
        
    }
}