using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArchitectureLibraryModels
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "Please enter reset password catcha answer")]
        [Display(Name = "Reset password captcha answer")]
        public string CaptchaAnswerResetPassword { set; get; }

        public string CaptchaNumberResetPassword0 { set; get; }

        public string CaptchaNumberResetPassword1 { set; get; }

        [Compare(nameof(ResetPasswordEmailAddress), ErrorMessage = "Reset password email address & confirm do not match")]
        [Display(Name = "Confirm reset password email address")]
        //[EmailAddress(ErrorMessage = "Please enter valid confirm register email address")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter valid email addres")]
        [Required(ErrorMessage = "Please enter confirm email address")]
        public string ConfirmResetPasswordEmailAddress { get; set; }

        [Display(Name = "Reset password email address")]
        //[EmailAddress(ErrorMessage = "Please enter valid register email address")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter valid email addres")]
        [Required(ErrorMessage = "Please enter reset password email address")]
        public string ResetPasswordEmailAddress { get; set; }

        public string FirstName { set; get; }

        public string LastName { set; get; }

        public string ResetPasswordKey { set; get; }

        public string ResetPasswordQueryString { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
