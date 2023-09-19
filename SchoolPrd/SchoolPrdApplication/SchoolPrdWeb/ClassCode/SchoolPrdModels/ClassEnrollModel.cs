using ArchitectureLibraryEnumerations;
using ArchitectureLibraryModels;
using SchoolPrdEnumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolPrdModels
{
    public class ClassEnrollModel : AuditInfoModel
    {
        public long? ClassEnrollId { set; get; }

        public long ClientId { get; set; }

        [Display(Name = "Catalog Initials Count")]
        [Required(ErrorMessage = "Enter catalog initials count")]
        public int CatalogInitialsCount { set; get; }

        [Display(Name = "Catalog Signatures Count")]
        [Required(ErrorMessage = "Enter catalog signatures count")]
        public int CatalogSignaturesCount { set; get; }

        [Display(Name ="Cancel Date")]
        [Required(ErrorMessage = "Enter cancel date")]
        public string CancelDate { set; get; }

        [Display(Name = "Completion Date")]
        [Required(ErrorMessage = "Enter course completion date")]
        public string CourseCompletionDate { set; get; }

        [Display(Name = "DMV Test Date")]
        [Required(ErrorMessage = "Enter DMV test date")]
        public string DMVTestDate { set; get; }

        [Display(Name = "Enrollment Initials Count")]
        [Required(ErrorMessage = "Enter enrollment signatures count")]
        public int EnrollmentAgreementInitialsCount { set; get; }

        [Display(Name = "Enrollment Signatures Count")]
        [Required(ErrorMessage = "Enter enrollment initials count")]
        public int EnrollmentAgreementSignaturesCount { set; get; }

        [Display(Name = "Class Status")]
        [Required(ErrorMessage = "Select status")]
        public ClassEnrollStatusEnum? ClassEnrollStatusId { set; get; }

        [Display(Name = "Class Schedule")]
        [Required(ErrorMessage = "Select class schedule")]
        public long? ClassScheduleId { get; set; }

        [Display(Name = "Funding required")]
        [Required(ErrorMessage = "Select funding requried")]
        public YesNoEnum? FundingRequired { set; get; }

        [Display(Name = "Funding source")]
        [Required(ErrorMessage = "Enter funding soure")]
        public string FundingSoureName { get; set; }

        [Display(Name = "PFS Initials Count")]
        [Required(ErrorMessage = "Enter PFS initials count")]
        public int PerformanceFactSheetInitialsCount { set; get; }

        [Display(Name = "PFS Signature Count")]
        [Required(ErrorMessage = "Enter PFS signatures count")]
        public int PerformanceFactSheetSignaturesCount { set; get; }

        [Display(Name = "Person")]
        [Required(ErrorMessage = "Select person")]
        public long? PersonId { set; get; }

        [Display(Name = "Person")]
        [Required(ErrorMessage = "Select person")]
        public string PersonNameSearch { set; get; }

        [Display(Name = "Register Date")]
        [Required(ErrorMessage = "Enter register date")]
        public string RegisterDate { set; get; }

        public List<ClassEnrollFeesModel> ClassEnrollFeesModels { set; get; }

        public List<PersonModel> PersonModels { set; get; }

        public ClassScheduleModel ClassScheduleModel { get; set; }

        public PersonModel PersonModel { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
