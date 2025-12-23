using ArchitectureLibraryEnumerations;
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
        public float ShoppingCartWIPSeqNum { set; get; }
        public List<ShoppingCartItemModel> ShoppingCartItemModels { set; get; }
        public List<ShoppingCartItemModel> ShoppingCartItemModelsSummary { set; get; }
        public ShoppingCartSummaryModel ShoppingCartSummaryModel { set; get; }
        public bool ShowDetail { set; get; }
        public ResponseObjectModel ResponseObjectModel { get; set; }
    }
}
