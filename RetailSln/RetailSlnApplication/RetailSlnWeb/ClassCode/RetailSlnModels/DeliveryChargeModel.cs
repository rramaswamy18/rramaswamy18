using ArchitectureLibraryModels;
using RetailSlnEnumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class DeliveryChargeModel : AuditInfoModel
    {
        public long DeliveryChargeId { set; get; }

        public long ClientId { set; get; }

        public float DeliveryChargeAmount { set; get; }

        public float DeliveryChargeAmountAdditional { set; get; }

        public long DeliveryListId { set; get; }

        public DeliveryModeEnum? DeliveryModeId { set; get; }

        public string DeliveryTime { set; get; }

        public DeliveryTypeEnum? DeliveryTypeId { set; get; }

        public long? DestDemogInfoCityId { set; get; }

        public long? DestDemogInfoCountryId { set; get; }

        public long? DestDemogInfoCountyId { set; get; }

        public long? DestDemogInfoSubDivisionId { set; get; }

        public long? DestDemogInfoZipIdFrom { set; get; }

        public long? DestDemogInfoZipIdTo { set; get; }

        public float FuelChargePercent { set; get; }

        public string GSTCaption { set; get; }

        public float GSTPercent { set; get; }

        public long? SrceDemogInfoCountryId { set; get; }

        public long UnitId { set; get; }

        public string UnitMeasure { set; get; }

        public long ValueFrom { set; get; }

        public long ValueTo { set; get; }

        public string ZipCodeFrom { set; get; }

        public string ZipCodeTo { set; get; }

        public DeliveryListModel DeliveryListModel { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
