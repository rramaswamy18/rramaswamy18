using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class DiscountDtlModel : AuditInfoModel
    {
        public long DiscountDtlId { set; get; }

        public long CorpAcctId { set; get; }

        public float CorpAcctDiscountPercent {  set; get; }

        public List<CorpAcctModel> CorpAcctModels { set; get; }
       
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}