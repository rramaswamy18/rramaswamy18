using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ApiShoppingCartModel
    {
        public string JwtToken { set; get; }
        public string CreditCardProcessor { set; get; }
        public ApiDeliveryInfoModel DeliveryInfoModel { set; get; }
        public List<ApiShoppingCartItemModel> ShoppingCartItemModels { set; get; }
        public List<ApiShoppingCartItemModel> ShoppingCartItemSummaryModels { set; get; }
        public ApiShoppingCartSummaryModel ShoppingCartSummaryModel { set; get; }
        public ResponseObjectModel ResponseObjectModel { get; set; }
    }
}
