using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleModels
{
    public class CalendarData
    {
        public DateTime StartDate { set; get; }
        public string StartTime { set; get; }
        public DateTime FinishDate { set; get; }
        public string FinishTime { set; get; }
        public long CodeTypeNameId { set; get; }
        public string CodeTypeNameDesc { set; get; }
        public string CodeTypeDesc { set; get; }
        public string CodeTypeDesc1 { set; get; }
        public long CalendarCodeId { set; get; }
        public long CodeDataNameId { set; get; }
        public string CodeDataNameDesc { set; get; }
        public string CodeDataDesc0 { set; get; }
        public string CodeDataDesc9 { set; get; }
        public string Color { set; get; }
        public string LocationNameDesc { set; get; }
    }
}
