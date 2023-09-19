using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolPrdModels
{
    public class ClassSessionListModel
    {
        public List<ClassSessionModel> ClassSessionModels { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}