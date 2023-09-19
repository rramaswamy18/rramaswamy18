using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SVCCTempleModels
{
    public class ContactUsModel
    {
        public long? ContactUsId { get; set; }

        [Display(Name = "Request Type")]
        [Required(ErrorMessage = "Please select type")]
        public string ContactUsTypeId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Enter first name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Enter last name")]
        public string LastName { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Enter email address")]
        [EmailAddress(ErrorMessage = "Enter valid email address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Telephone#")]
        [Required(ErrorMessage = "Enter phone#")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Enter valid phone#")]
        public string TelephoneNumber { get; set; }

        [Required(ErrorMessage = "Enter Comments")]
        [Display(Name = "Comments")]
        public string Comments { get; set; }

        public ResponseObjectModel0 ResponseObjectModel { set; get; }
    }
}
