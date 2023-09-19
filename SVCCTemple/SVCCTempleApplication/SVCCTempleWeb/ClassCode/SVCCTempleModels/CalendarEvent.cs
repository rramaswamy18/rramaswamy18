using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleModels
{
    public class CalendarEvent
    {
        public long CalendarEventId { set; get; }
        public string LocationNameDesc { set; get; }
        public string EventDate { set; get; }
        public float SeqNum { set; get; }
        public string EventText { set; get; }
    }
}