using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class DeliveryListModel : AuditInfoModel
    {
        public long DeliveryListId { set; get; }

        public long ClientId { set; get; }

        public string DeliveryListName { set; get; }

        public List<DeliveryChargeModel> DeliveryChargeModels { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
