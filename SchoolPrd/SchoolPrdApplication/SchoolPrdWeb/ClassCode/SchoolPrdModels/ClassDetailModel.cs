using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolPrdModels
{
    public class ClassDetailModel : AuditInfoModel
    {
        public long? ClassDetailId { get; set; }
        public long ClientId { set; get; }
        public string ClassDetailDesc { get; set; }
        public long? ClassSessionId { get; set; }
        public string FinishTime { get; set; }
        public float? SeqNum { get; set; }
        public string StartTime { get; set; }
    }
}
