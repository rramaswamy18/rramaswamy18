using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolPrdModels
{
    public class ClassFeesListModel
    {
        public List<ClassFeesModel> ClassFeesModels { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}