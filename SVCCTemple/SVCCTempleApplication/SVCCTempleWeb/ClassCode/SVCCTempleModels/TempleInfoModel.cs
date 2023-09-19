using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleModels
{
    public class TempleInfoModel
    {
        public long TempleInfoId { set; get; }
        public string LocationNameDesc { set; get; }
        public long ImportantDatesId { set; get; }
        public string InfoType { set; get; }
        public float SeqNum { set; get; }
        public string StartDate { set; get; }
        public string StartTime { set; get; }
        public string FinishDate { set; get; }
        public string FinishTime { set; get; }
        public string InfoText1 { set; get; }
        public string InfoText2 { set; get; }
        public string InfoText3 { set; get; }
        public string HtmlFileName1 { set; get; }
        public string HtmlFileName2 { set; get; }
        public string HtmlFileName3 { set; get; }
        public string ImageName1 { set; get; }
        public string ImageName2 { set; get; }
        public string ImageName3 { set; get; }
        public string SponsorshipGroupId { set; get; }
    }
}
