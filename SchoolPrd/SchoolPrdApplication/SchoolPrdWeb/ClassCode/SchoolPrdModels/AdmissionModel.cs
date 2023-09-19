using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolPrdModels
{
    public class AdmissionModel
    {
        public List<ClassEnrollModel> ClassEnrollModels { set; get; }
        public ClassEnrollModel ClassEnrollModel { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}