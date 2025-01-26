using ArchitectureLibraryModels;
using RetailSlnEnumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class CorpAcctModel : AuditInfoModel
    {
        public long CorpAcctId { set; get; }

        public long ClientId { set; get; }

        public string CorpAcctKey { set; get; }

        public string CorpAcctName { set; get; }

        public CorpAcctTypeEnum? CorpAcctTypeId { set; get; }

        public short CreditDays { set; get; }

        public float CreditLimit { set; get; }

        public bool CreditSale { set; get; }

        public float MinOrderAmount { set; get; }

        public bool ShippingAndHandlingCharges { set; get; }

        public string TaxIdentNum { set; get; }

        public List<CorpAcctLocationModel> CorpAcctLocationModels { set; get; }

        public List<DiscountDtlModel> DiscountDtlModels { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
