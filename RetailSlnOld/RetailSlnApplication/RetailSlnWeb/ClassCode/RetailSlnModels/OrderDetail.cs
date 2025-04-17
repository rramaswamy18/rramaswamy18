using ArchitectureLibraryDocumentModels;
using ArchitectureLibraryEnumerations;
using RetailSlnEnumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class OrderDetail : AuditInfoModel
    {
        public long OrderDetailId { set; get; }
        public long ClientId { set; get; }
        public DimensionUnitEnum DimensionUnitId { set; get; }
        public float HeightValue { set; get; }
        public string HSNCode { set; get; }
        public float ItemDiscountAmount { set; get; }
        public long? ItemId { set; get; }
        public float ItemRate { set; get; }
        public float ItemRateBeforeDiscount { set; get; }
        public string ItemShortDesc { set; get; }
        public float LengthValue { set; get; }
        public float OrderAmount { set; get; }
        public float OrderAmountBeforeDiscount { set; get; }
        public string OrderComments { set; get; }
        public OrderDetailTypeEnum? OrderDetailTypeId { set; get; }
        public long OrderHeaderSummaryId { set; get; }
        public string OrderNumber { set; get; }
        public long OrderQty { set; get; }
        public string ProductCode { set; get; }
        public float ProductOrVolumetricWeight { set; get; }
        public WeightUnitEnum ProductOrVolumetricWeightUnitId { set; get; }
        public double SeqNum { set; get; }
        public float VolumeValue { set; get; }
        public WeightUnitEnum WeightCalcUnitId { set; get; }
        public float WeightCalcValue { set; get; }
        public WeightUnitEnum WeightUnitId { set; get; }
        public float WeightValue { set; get; }
        public float WidthValue { set; get; }
    }
}
