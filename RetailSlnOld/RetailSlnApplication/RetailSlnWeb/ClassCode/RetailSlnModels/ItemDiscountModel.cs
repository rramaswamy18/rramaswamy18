using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ItemDiscountModel : AuditInfoModel
    {
        public long ItemDiscountId { set; get; }

        public long ClientId { set; get; }

        public long CorpAcctId { set; get;}

        public long ItemId { set; get; }

        public float DiscountPercent { set; get; }

        public DateTime BegEffDate { set; get; }

        public DateTime EndEffDate {  set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
