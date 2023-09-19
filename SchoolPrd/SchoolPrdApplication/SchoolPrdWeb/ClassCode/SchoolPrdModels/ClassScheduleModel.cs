using ArchitectureLibraryEnumerations;
using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolPrdModels
{
    public class ClassScheduleModel : AuditInfoModel
    {
        public long ClientId { get; set; }
        public long? ClassScheduleId { get; set; }
        //[Required(ErrorMessage = "Please enter schedule desc")]
        [Display(Name = "Class Schedule Desc ")]
        public string ClassScheduleDesc { get; set; }
        //[Required(ErrorMessage = "Please enter session id")]
        [Display(Name = "Class Session Id")]
        public long? ClassSessionId { get; set; }
        //[Required(ErrorMessage = "Please enter graduation date")]
        [Display(Name = "Graduation Date ")]
        public string GraduationDate { get; set; }
        //[Required(ErrorMessage = "Please enter start date")]
        [Display(Name = "Register Date ")]
        public string RegisterDate { get; set; }
        [Display(Name = "Start Date ")]
        public string StartDate { get; set; }
        [Display(Name = "Status")]
        public StatusEnum? StatusId { set; get; }
        public List<ClassEnrollModel> ClassEnrollModels { set; get; }
        public List<ClassListModel> ClassListModels { set; get; }
        public ClassListModel ClassListModel { get; set; }
        public List<ClassSessionModel> ClassSessionModels { set; get; }
        public ClassSessionModel ClassSessionModel { get; set; }
        public ResponseObjectModel ResponseObjectModel { set; get; }

    }
}
