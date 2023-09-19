using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolPrdModels
{
    public class ClassScheduleListModel
    {
        public List<ClassScheduleModel> ClassScheduleModels { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}