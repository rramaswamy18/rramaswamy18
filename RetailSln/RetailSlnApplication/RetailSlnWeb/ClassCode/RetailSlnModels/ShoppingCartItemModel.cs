using ArchitectureLibraryEnumerations;
using RetailSlnEnumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ShoppingCartItemModel
    {
        public long ClientId { set; get; }

        public DimensionUnitEnum? DimensionUnitId { set; get; }

        public bool DoNotBreakBundle { set; get; }

        public float? HeightValue { set; get; }

        public string HSNCode { set; get; }

        public string ImageName { set; get; }

        public float? ItemDiscountAmount { set; get; }

        public float? ItemDiscountPercent { set; get; }

        public string ItemDiscountPercentFormatted { set; get; }

        public long? ItemId { set; get; }

        public string ItemItemSpecsForDisplay { set; get; }

        public string ItemMasterDesc { set; get; }

        public string ItemMasterDesc0 { set; get; }

        public string ItemMasterDesc1 { set; get; }

        public string ItemMasterDesc2 { set; get; }

        public string ItemMasterDesc3 { set; get; }

        public long ItemMasterId { set; get; }

        public string ItemIdParm { set; get; }

        public float? ItemRate { set; get; }

        public float? ItemRateBeforeDiscount { set; get; }

        public string ItemRateFormatted { set; get; }

        public string ItemRateBeforeDiscountFormatted { set; get; }

        public string ItemShortDesc { set; get; }

        public ItemTypeEnum ItemTypeId { set; get; }

        public float? LengthValue { set; get; }

        public float? OrderAmount { set; get; }

        public float? OrderAmountBeforeDiscount { set; get; }

        public string OrderAmountFormatted { set; get; }

        public long? OrderAmountRounded { set; get; }

        public string OrderComments { set; get; }

        public OrderDetailTypeEnum OrderDetailTypeId { set; get; }

        public long? OrderQty { set; get; }

        public string OrderQtyParm { set; get; }

        public long? ParentItemId { set; get; }

        public string ProductCode { set; get; }

        public float? ProductOrVolumetricWeight { set; get; }

        public WeightUnitEnum? ProductOrVolumetricWeightUnitId { set; get; }

        public float? VolumeValue { set; get; }

        public WeightUnitEnum? WeightCalcUnitId { set; get; }

        public float? WeightCalcValue { set; get; }

        public WeightUnitEnum? WeightUnitId { set; get; }

        public float? WeightValue { set; get; }

        public float? WidthValue { set; get; }

        public long ShoppingCartWIPId { set; get; }

        public ItemModel ItemModel { set; get; }

        public List<ShoppingCartItemModel> ShoppingCartItemSummarys { set; get; }

        public List<ShoppingCartItemModel> ShoppingCartItemBundleModels { set; get; }
    }
}
