using ArchitectureLibraryModels;
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

        public string CorpAcctName { set; get;}

        public string TaxIdentNum { set; get; }

        public long DemogInfoAddressId { set; get; }

        public List<DiscountDtlModel> DiscountDtlModels  { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
