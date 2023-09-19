using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolPrdModels
{
    public class HolidayListModel
    {
        public List<HolidayModel> HolidayModels { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}