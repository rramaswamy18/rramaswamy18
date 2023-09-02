using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class DeliveryChargeModel : AuditInfoModel
    {
        public long DeliveryChargeId { set; get; }

        public string ChargeUnitMeasure { set; get; }

        public float DeliveryChargeAmount { set; get; }

        public long UnitId { set; get; }

        public long ValueFrom { set; get; }

        public long ValueTo { set; get; }

        public string ZipCodeFrom { set; get; }

        public string ZipCodeTo { set; get; }

    }
}
