using ArchitectureLibraryEnumerations;
using RetailSlnEnumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ApiShoppingCartItemModel
    {
        public long? ItemId { set; get; }
        public DimensionUnitEnum? DimensionUnitId { set; get; }
        public float? HeightValue { set; get; }
        public string HSNCode { set; get; }
        public string ItemDesc { set; get; }
        public float? ItemDiscountAmount { set; get; }
        public float? ItemDiscountPercent { set; get; }
        public float? ItemRate { set; get; }
        public float? ItemRateBeforeDiscount { set; get; }
        public string ItemShortDesc { set; get; }
        public float? LengthValue { set; get; }
        public float? OrderAmount { set; get; }
        public float? OrderAmountBeforeDiscount { set; get; }
        public string OrderComments { set; get; }
        public OrderDetailTypeEnum OrderDetailTypeId { set; get; }
        public long? OrderQty { set; get; }
        public string ProductCode { set; get; }
        public float? ProductOrVolumetricWeight { set; get; }
        public WeightUnitEnum? ProductOrVolumetricWeightUnitId { set; get; }
        public float? ProductWeight { set; get; }
        public WeightUnitEnum? ProductWeightUnitId { set; get; }
        public float? VolumetricWeight { set; get; }
        public WeightUnitEnum? VolumetricWeightUnitId { set; get; }
        public float? VolumeValue { set; get; }
        public float? WeightCalc { set; get; }
        public WeightUnitEnum? WeightCalcUnitId { set; get; }
        public float? WidthValue { set; get; }
        public List<ApiShoppingCartItemModel> ShoppingCartItemSummaryModels { set; get; }
    }
}
