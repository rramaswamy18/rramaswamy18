using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArchitectureLibraryModels
{
    public class LoginUserProfModel
    {
        [Required(ErrorMessage = "Please enter login captcha answer")]
        [Display(Name = "Login captcha answer")]
        public string CaptchaAnswerLogin { set; get; }

        public string CaptchaNumberLogin0 { set; get; }

        public string CaptchaNumberLogin1 { set; get; }

        [Display(Name = "Login email address")]
        //[EmailAddress(ErrorMessage = "Please enter valid register email address")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter valid login email addres")]
        [Required(ErrorMessage = "Please enter login email address")]
        public string LoginEmailAddress { get; set; }

        [Display(Name = "Login password")]
        [Required(ErrorMessage = "Please enter login password")]
        public string LoginPassword { get; set; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
