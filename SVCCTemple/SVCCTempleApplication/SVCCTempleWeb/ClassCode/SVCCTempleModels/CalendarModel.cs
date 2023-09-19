using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SVCCTempleModels
{
    public class CalendarModel
    {
        public string LocationNameDesc { set; get; }
        public DateTime CalendarFromDate { set; get; }
        public DateTime CalendarToDate { set; get; }
        public DateTime CalendarStartDate { set; get; }
        public DateTime CalendarFinishDate { set; get; }
        public int CalendarYearSelected { set; get; }
        public int CalendarMonthSelected { set; get; }
        public string[] CalendarMonthName { set; get; }
        public int[] CalendarYear { set; get; }
        public List<ImportantDatesModel> ImportantDatesModelsList { set; get; }
        public SortedList<string, List<CalendarData>> CalendarDataListList { set; get; }
        public SortedList<DateTime, List<CalendarData>> CalendarDateListList { set; get; }
        public SortedList<DateTime, List<CalendarEvent>> CalendarEventListList { set; get; }
    }
}
