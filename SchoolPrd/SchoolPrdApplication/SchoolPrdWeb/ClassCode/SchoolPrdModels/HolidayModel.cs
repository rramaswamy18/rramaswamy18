using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolPrdModels
{
    public class HolidayModel : AuditInfoModel
    {
        public long ClientId { get; set; }
        public long? HolidayId { get; set; }

        [Required(ErrorMessage = "Please enter Holiday Name")]
        [Display(Name = "Holiday Name ")]
        public string HolidayName { get; set; }
        [Required(ErrorMessage = "Please enter Holiday Date")]
        [Display(Name = "Holiday Date ")]
        public string HolidayDate { get; set; }
        [Required(ErrorMessage = "Please enter Holiday Description")]
        [Display(Name = "Holiday Description ")]
        public string HolidayDescription { get; set; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
