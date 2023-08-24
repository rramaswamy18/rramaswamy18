using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArchitectureLibraryModels
{
    public class UpdatePasswordModel
    {
        [Required(ErrorMessage = "Please enter catcha answer")]
        [Display(Name = "Captcha answer")]
        public string CaptchaAnswer { set; get; }

        public string CaptchaNumber0 { set; get; }

        public string CaptchaNumber1 { set; get; }

        [Compare(nameof(EmailAddress), ErrorMessage = "Email address & confirm do not match")]
        [Display(Name = "Confirm email address")]
        //[EmailAddress(ErrorMessage = "Please enter valid email address")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter valid login email addres")]
        [Required(ErrorMessage = "Please enter email address")]
        public string ConfirmEmailAddress { set; get; }

        [Compare(nameof(LoginPassword), ErrorMessage = "Login password & confirm do not match")]
        [Display(Name = "Confirm login password")]
        [Required(ErrorMessage = "Please enter confirm login password")]
        public string ConfirmLoginPassword { get; set; }

        [Display(Name = "Email address")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter valid login email addres")]
        [Required(ErrorMessage = "Please enter email address")]
        public string EmailAddress { set; get; }

        [Display(Name = "Login password")]
        [Required(ErrorMessage = "Please enter login password")]
        public string LoginPassword { set; get; }

        public string LoginPasswordStrengthColor { set; get; }

        public string LoginPasswordStrengthMessage { set; get; }

        public List<string> PasswordStrengthMessages { set; get; }

        [Display(Name = "Reset password key")]
        [Required(ErrorMessage = "Please enter reset password key")]
        public string ResetPasswordKey { set; get; }

        public string ResetPasswordExpiryDateTime { set; get; }

        public string ResetPasswordQueryString { set; get; }

        public string FirstName { set; get; }

        public string LastName { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
