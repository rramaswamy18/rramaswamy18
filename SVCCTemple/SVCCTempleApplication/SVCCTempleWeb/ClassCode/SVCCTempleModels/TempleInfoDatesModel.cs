using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleModels
{
    public class TempleInfoDatesModel
    {
        public string StartDate { set; get; }
        public string FinishDate { set; get; }
        public List<TempleInfoDateModel> TempleInfoDateModels { set; get; }
    }
}
