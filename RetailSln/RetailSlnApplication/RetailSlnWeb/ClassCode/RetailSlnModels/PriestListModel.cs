using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class PriestListModel : AuditInfoModel
    {
        public long? PriestListId { set; get; }
        public float? CommissionPercent { set; get; }
        public long CouponListId { set; get; }
        public float? DiscountPercent { set; get; }
        public long PersonId { set; get; }
        public CouponListModel CouponListModel { set; get; }
        public PersonModel PersonModel { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
