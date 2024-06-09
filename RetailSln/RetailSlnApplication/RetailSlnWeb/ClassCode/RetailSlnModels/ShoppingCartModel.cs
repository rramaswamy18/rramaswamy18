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
        public long? DeliveryAddressId { set; get; }
        public long? OrderHeaderId { set; get; }
        public float? ShoppingCartTotalAmount { set; get; }
        public bool ShowDiscountsAdditionalCharges { set; get; }
        public long? TotalItemsCount { set; get; }
        public float? TotalVolumeValue { set; get; }
        public float? TotalWeightValue { set; get; }
        public long TotalVolumeValueRounded { set; get; }
        public long TotalWeightValueRounded { set; get; }
        public WeightUnitEnum? TotalWeightValueRoundedUnit { set; get; }
        public float? TotalProductOrVolumetricWeight { set; get; }
        public long TotalProductOrVolumetricWeightRounded { set; get; }
        public WeightUnitEnum? TotalProductOrVolumetricWeightRoundedUnit { set; get; }
        public List<ShoppingCartItemModel> ShoppingCartItems { set; get; }
        public List<ShoppingCartItemModel> ShoppingCartSummaryItems { set; get; }
        public ResponseObjectModel ResponseObjectModel { get; set; }
    }
}
