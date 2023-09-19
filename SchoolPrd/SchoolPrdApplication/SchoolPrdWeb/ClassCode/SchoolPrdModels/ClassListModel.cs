using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolPrdModels
{
    public class ClassListModel : AuditInfoModel
    {
        public long? ClassListId { get; set; }

        public long ClientId { get; set; }

        [Required(ErrorMessage = "Please enter class list desc")]
        [Display(Name = "Class List Desc ")]
        public string ClassListDesc { get; set; }

        public List<ClassFeesModel> ClassFeesModels { get; set; }

        public List<ClassSessionModel> ClassSessionModels { get; set; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
