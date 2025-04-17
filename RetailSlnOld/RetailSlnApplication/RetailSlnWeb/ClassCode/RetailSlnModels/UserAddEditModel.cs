using ArchitectureLibraryEnumerations;
using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class UserAddEditModel
    {
        public string AspNetUserId { set; get; }

        [Display(Name = "Alt Country")]
        //[Required(ErrorMessage = "Select country")]
        public long? AlternateTelephoneCountryId { set; get; }

        [Display(Name = "Alt Telephone#")]
        //[RegularExpression(@"(.{10})", ErrorMessage = "Enter 10 digit telephone number")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Enter valid 10 digit phone#")]
        //[Required(ErrorMessage = "Please enter alt phone#")]
        //[StringLength(10, MinimumLength = 10, ErrorMessage = "Enter 10 digit phone#")]
        public string AlternateTelephoneNumber { set; get; }

        [Display(Name = "User type")]
        [Required(ErrorMessage = "Select user type")]
        public long? AspNetRoleUserTypeId { set; get; }

        [Display(Name = "Corp Acct")]
        [Required(ErrorMessage = "Select corp acct")]
        public long? CorpAcctId { set; get; }

        [Display(Name = "Email address")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Enter valid email")]
        [Required(ErrorMessage = "Enter email address")]
        public string EmailAddress { set; get; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = "Enter first name")]
        public string FirstName { set; get; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Enter last name")]
        public string LastName { set; get; }

        public string LoginId { set; get; }

        [Display(Name = "Login type")]
        [Required(ErrorMessage = "Select login type")]
        public LoginTypeEnum? LoginTypeId { set; get; }

        public long? PersonId { set; get; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "Select country")]
        public long? TelephoneCountryId { set; get; }

        [Display(Name = "Telephone#")]
        //[RegularExpression(@"(.{10})", ErrorMessage = "Enter 10 digit telephone number")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Enter valid 10 digit phone#")]
        [Required(ErrorMessage = "Please enter phone#")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Enter 10 digit phone#")]
        public string TelephoneNumber { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
