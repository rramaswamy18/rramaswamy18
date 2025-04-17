using ArchitectureLibraryEnumerations;
using RetailSlnEnumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class OrderDetailModel
    {
        public long OrderDetailId { set; get; }

        public DimensionUnitEnum DimensionUnitId { set; get; }

        public float HeightValue { set; get; }

        public long? ItemId { set; get; }

        public float ItemRate { set; get; }

        public string ItemShortDesc { set; get; }

        public float LengthValue { set; get; }

        public float OrderAmount { set; get; }

        public string OrderComments { set; get; }

        public OrderDetailTypeEnum? OrderDetailTypeId { set; get; }

        public long OrderHeaderSummaryId { set; get; }

        public string OrderNumber { set; get; }

        public long OrderQty { set; get; }

        public double SeqNum { set; get; }

        public float VolumeValue { set; get; }

        public WeightUnitEnum WeightUnitId { set; get; }

        public float WeightValue { set; get; }

        public float WidthValue { set; get; }
    }
}
