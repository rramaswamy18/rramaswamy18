using ArchitectureLibraryEnumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryModels
{
    public class SalesTaxListModel : AuditInfoModel
    {
        public long SalesTaxListId { set; get; }

        public long ClientId { set; get; }

        public DateTime BegEffDate { set; get; }

        public long? DestDemogInfoCountryId { set; get; }

        public long? DestDemogInfoSubDivisionId { set; get; }

        public long? DestDemogInfoCountyId { set; get; }

        public long? DestDemogInfoCityId { set; get; }

        public long? DestDemogInfoZipIdFrom { set; get; }

        public long? DestDemogInfoZipIdTo { set; get; }

        public DateTime EndEffDate { set; get; }

        public string LineItemLevelName { set; get; }

        public SalesTaxCaptionEnum SalesTaxCaptionId { set; get; }

        public float SalesTaxRate { set; get; }

        public bool ShowOnInvoice { set; get; }

        public long? SrceDemogInfoCountryId { set; get; }

        public long? SrceDemogInfoSubDivisionId { set; get; }

        public long? SrceDemogInfoCountyId { set; get; }

        public long? SrceDemogInfoCityId { set; get; }

        public long? SrceDemogInfoZipId { set; get; }

        public string TaxRegionName { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
