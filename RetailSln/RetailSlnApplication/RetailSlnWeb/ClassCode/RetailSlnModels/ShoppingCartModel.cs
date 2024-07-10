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
        public bool BackToTop { set; get; }
        public bool Checkout { set; get; }
        public ShoppingCartSummaryModel ShoppingCartSummaryModel { set; get; }
        public List<ShoppingCartItemModel> ShoppingCartItems { set; get; }
        public List<ShoppingCartItemModel> ShoppingCartSummaryItems { set; get; }
        public ResponseObjectModel ResponseObjectModel { get; set; }
    }
}
