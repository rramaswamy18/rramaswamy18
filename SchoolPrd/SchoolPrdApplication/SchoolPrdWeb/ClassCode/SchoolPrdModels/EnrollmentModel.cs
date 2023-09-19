using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPrdModels
{
   public class EnrollmentModel
    {
        public long EnrollmentId { set; get; }

        [Display(Name = "Answer captcha question")]
        [Required(ErrorMessage = "Please enter captcha answer")]
        public string CaptchaAnswerEnrollment { set; get; }

        public string CaptchaNumberEnrollment0 { set; get; }

        public string CaptchaNumberEnrollment1 { set; get; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please enter first name")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "First name cannot exceed 100 characters")]
        public string FirstName { set; get; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please enter last name")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Last name cannot exceed 100 characters")]
        public string LastName { set; get; }

        [Display(Name = "Middle Name")]
        [StringLength(100, MinimumLength = 0, ErrorMessage = "Middle name cannot exceed 100 characters")]
        public string MiddleName { set; get; }

        [Display(Name = "Email Address")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter valid email addres")]
        [Required(ErrorMessage = "Please enter email address")]
        public string EmailAddress { set; get; }

        [Display(Name = "Phone Number")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please enter valid 10 digit phone#")]
        [Required(ErrorMessage = "Please enter phone#")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Please enter 10 digit phone#")]
        public string TelephoneNumber { set; get; }

        [Display(Name = "Comments / Message")]
        [StringLength(2048, ErrorMessage = "Please enter valid comments / message (max 2048 characters)")]
        public string ContactMessage { set; get; }

        [Display(Name = "Can you pass a drug screen?")]
        [Required(ErrorMessage = "Select Yes or No for Drug Screen")]
        public string DrugScreenId { set; get; }

        [Display(Name = "Type of program training")]
        [Required(ErrorMessage = "Select one or more programs")]
        public List<string> EnrollmentTypeIdList { set; get; }

        public string EnrollmentTypeIds { set; get; }

        public long? DemogInfoAddressId { get; set; }

        [Display(Name = "Address Line# 1")]
        [Required(ErrorMessage = "Please enter address line# 1")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Please enter valid address line# 1")]
        public string AddressLine1 { set; get; }

        [Display(Name = "Address Line# 2")]
        [StringLength(100, ErrorMessage = "Please enter valid address line# 2")]
        public string AddressLine2 { set; get; }

        [Display(Name = "Zip Code")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Please enter 5 digit valid zip code")]
        [Required(ErrorMessage = "Please enter zip code")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Please enter 5 digit zip code")]
        public string ZipCode { set; get; }

        [Display(Name = "City Name")]
        [Required(ErrorMessage = "Please enter city name")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Please enter valid city name")]
        public string CityName { set; get; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "Select state from the list")]
        public string DemogInfoSubDivisionId { set; get; }

        [Display(Name = "Driving License Issuing State")]
        [Required(ErrorMessage = "Select the state issuing the driving licencse")]
        public string DrivingLicenseDemogInfoSubDivisionId { set; get; }

        public string EnrollmentTypeDescs { get; set; }

        public string LoginPassword { set; get; }

        public bool UserProfRegistered { set; get; }

        public DemogInfoAddressModel DemogInfoAddressModel { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
