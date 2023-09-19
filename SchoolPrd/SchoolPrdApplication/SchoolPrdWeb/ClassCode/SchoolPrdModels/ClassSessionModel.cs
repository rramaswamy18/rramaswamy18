using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolPrdModels
{
    public class ClassSessionModel : AuditInfoModel
    {
        public long ClientId { get; set; }
        public long? ClassSessionId { get; set; }
        [Required(ErrorMessage = "Please enter session desc")]
        [Display(Name = "Class Session Desc ")]
        public string ClassSessionDesc { get; set; }
        public float? SeqNum { get; set; }
        [Required(ErrorMessage = "Please select class list")]
        [Display(Name = "Class List")]
        public long? ClassListId { get; set; }
        public List<ClassDetailModel> ClassDetailModels { get; set; }
        public List<ClassScheduleModel> ClassScheduleModels { get; set; }
        public ClassListModel ClassListModel { get; set; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
