using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ItemMasterAttributesModel
    {
        public long ItemMasterId { set; get; }
        //public ItemMasterModel ItemMasterModel { set; get; }
        public OrderItem1Model OrderItem1Model { set; get; }
        public long TabId { set; get; }
    }
}
