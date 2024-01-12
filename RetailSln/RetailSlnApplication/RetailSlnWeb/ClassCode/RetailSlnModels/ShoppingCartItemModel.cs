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
        public long? ItemId { set; get; }

        public DimensionUnitEnum? DimensionUnitId { set; get; }

        public float? HeightValue { set; get; }

        public string ItemDesc { set; get; }

        public float? ItemDiscountAmount { set; get; }

        public float? ItemDiscountPercent { set; get; }

        public float? ItemRate { set; get; }

        public float? ItemRateBeforeDiscount { set; get; }

        public string ItemShortDesc { set; get; }

        public float? LengthValue { set; get; }

        public float? VolumeValue { set; get; }

        public WeightUnitEnum? WeightUnitId { set; get; }

        public float? WeightValue { set; get; }

        public float? WidthValue { set; get; }

        public long? OrderQty { set; get; }

        public float? OrderAmount { set; get; }

        public float? OrderAmountBeforeDiscount { set; get; }

        public string OrderComments { set; get; }

        public OrderDetailTypeEnum OrderDetailTypeId { set; get; }

        public List<ShoppingCartItemModel> ShoppingCartItemSummarys { set; get; }
    }
}
