using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolPrdModels
{
    public class ClassEnrollListModel
    {
        public List<ClassEnrollModel> ClassEnrollModels { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}