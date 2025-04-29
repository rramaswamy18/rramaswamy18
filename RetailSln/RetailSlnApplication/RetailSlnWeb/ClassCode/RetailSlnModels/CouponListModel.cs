using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class CouponListModel : AuditInfoModel
    {
        public long? CouponListId { set; get; }
        public string CouponNum { set; get; }
        public string BegEffDate { set; get; }
        public float DiscountPercent { set; get; }
        public string EndEffDate { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
