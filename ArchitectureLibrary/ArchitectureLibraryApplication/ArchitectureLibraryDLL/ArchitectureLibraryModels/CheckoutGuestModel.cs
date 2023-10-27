using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArchitectureLibraryModels
{
    public class CheckoutGuestModel
    {
        [Required(ErrorMessage = "Please enter login catcha answer")]
        [Display(Name = "Login captcha answer")]
        public string CaptchaAnswerCheckoutGuest { set; get; }

        public string CaptchaNumberCheckoutGuest0 { set; get; }

        public string CaptchaNumberCheckoutGuest1 { set; get; }

        [Compare(nameof(CheckoutGuestEmailAddress), ErrorMessage = "Checkout guest email address & confirm do not match")]
        [Display(Name = "Confirm email address")]
        //[EmailAddress(ErrorMessage = "Please enter valid confirm register email address")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter valid confirm email addres")]
        [Required(ErrorMessage = "Please enter confirm email address")]
        public string ConfirmCheckoutGuestEmailAddress { get; set; }

        [Display(Name = "Checkout guest email address")]
        //[EmailAddress(ErrorMessage = "Please enter valid register email address")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter valid checkout guest email addres")]
        [Required(ErrorMessage = "Please enter checkout guest email address")]
        public string CheckoutGuestEmailAddress { get; set; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
