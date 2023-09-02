using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ShoppingCartModel
    {
        public bool Checkout { set; get; }
        public long? DeliveryAddressId { set; get; }
        public long? OrderHeaderId { set; get; }
        public float? ShoppingCartTotalAmount { set; get; }
        public List<ShoppingCartItemModel> ShoppingCartItems { set; get; }
        public List<ShoppingCartItemModel> ShoppingCartSummaryItems { set; get; }
        public ResponseObjectModel ResponseObjectModel { get; set; }
    }
}
