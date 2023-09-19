using ArchitectureLibraryModels;
using SchoolPrdEnumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolPrdModels
{
    public class ClassEnrollFeesModel : AuditInfoModel
    {
        public long? ClassEnrollFeesId { set; get; }
        public long ClientId { get; set; }
        public float? AmountPaid { set; get; }
        public float? ClassEnrollFeesAmount { set; get; }
        public string ClassEnrollFeesDesc { set; get; }
        public long ClassEnrollId { set; get; }
        public long ClassFeesId { set; get; }
        public ClassFeesTypeEnum ClassFeesTypeId { set; get; }
        public string DatePaid { set; get; }
        public string DueDate { set; get; }
        public float SeqNum { set; get; }
    }
}
