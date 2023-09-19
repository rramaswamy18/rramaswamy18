using ArchitectureLibraryModels;
using SchoolPrdEnumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolPrdModels
{
    public class ClassFeesModel : AuditInfoModel
    {
        public long ClientId { get; set; }
        public long? ClassFeesId { get; set; }

        [Required(ErrorMessage = "Please enter fees desc")]
        [Display(Name = "Class Fees Desc ")]
        public string ClassFeesDesc { get; set; }

        [Required(ErrorMessage = "Please enter fees amount")]
        [Display(Name = "Class Fees Amount ")]
        public float ClassFeesAmount { get; set; }

        [Required(ErrorMessage = "Select fees type")]
        [Display(Name = "Class Fees Type ")]
        public ClassFeesTypeEnum ClassFeesTypeId { get; set; }

        [Required(ErrorMessage = "Please enter Class list id")]
        [Display(Name = "Class List Id ")]
        public long? ClassListId { get; set; }

        [Required(ErrorMessage = "Please enter seq num")]
        [Display(Name = " Seq Num ")]
        public float? SeqNum { get; set; }

        public ClassListModel ClassListModel { set; get; }
        public List<ClassListModel> ClassListModels { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
