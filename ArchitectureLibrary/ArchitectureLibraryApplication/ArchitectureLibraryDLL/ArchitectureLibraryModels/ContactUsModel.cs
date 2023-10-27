using ArchitectureLibraryEnumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArchitectureLibraryModels
{
    public class ContactUsModel
    {
        public long? ContactUsId { get; set; }

        [Display(Name = "Answer captcha question")]
        [Required(ErrorMessage = "Please enter captcha answer")]
        public string CaptchaAnswerContactUs { set; get; }

        public string CaptchaNumberContactUs0 { set; get; }

        public string CaptchaNumberContactUs1 { set; get; }

        [Display(Name = "Comments / Message")]
        [Required(ErrorMessage = "Please enter comments")]
        [StringLength(2048, MinimumLength = 1, ErrorMessage = "Please enter valid comments / message (max 2048 characters)")]
        public string CommentsRequests { get; set; }

        [Display(Name = "Contact Us Type")]
        [Required(ErrorMessage = "Select Contact Us Type")]
        public ContactUsTypeEnum? ContactUsTypeId { set; get; }

        [Display(Name = "Email Address")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter valid email addres")]
        [Required(ErrorMessage = "Please enter email address")]
        public string EmailAddress { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please enter first name")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Please enter valid first name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please enter last name")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Please enter valid last name")]
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please enter valid 10 digit phone#")]
        [Required(ErrorMessage = "Please enter phone#")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Please enter 10 digit phone#")]
        public string ContactUsTelephoneNumber { get; set; }

        public long? ContactUsTelephoneCountryId { set; get; }

        public string LoginPassword { set; get; }

        public bool UserProfRegistered { set; get; }

        public string RegisterInfo { set; get; }

        public string LoginDetails { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
