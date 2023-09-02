using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class GiftCertModel : AuditInfoModel
    {
        public long? GiftCertId { set; get; }

        public long ClientId { set; get; }

        //[Display(Name = "First Name")]
        //[Required(ErrorMessage = "Please enter first name")]
        //[StringLength(100, MinimumLength = 1, ErrorMessage = "Please enter valid first name")]
        public string FirstName { set; get; }

        //[Display(Name = "Last Name")]
        //[Required(ErrorMessage = "Please enter last name")]
        //[StringLength(100, MinimumLength = 1, ErrorMessage = "Please enter valid last name")]
        public string LastName { set; get; }

        //[Display(Name = "Phone Number")]
        //[RegularExpression(@"^\d{10}$", ErrorMessage = "Please enter valid 10 digit phone#")]
        //[Required(ErrorMessage = "Please enter phone#")]
        //[StringLength(10, MinimumLength = 10, ErrorMessage = "Please enter 10 digit phone#")]
        public string TelephoneNumber { set; get; }

        [Display(Name = "Gift Cert Amount")]
        [Required(ErrorMessage = "Please enter amount")]
        public float? GiftCertAmount { set; get; }

        public float GiftCertBalanceAmount { set; get; }

        public string GiftCertImageFileName { set; get; }

        [Display(Name = "Gift Cert Message")]
        [MaxLength(250, ErrorMessage = "Message not to exceed 100 characters")]
        [Required(ErrorMessage = "Please enter message")]
        public string GiftCertMessage { set; get; }

        public long GiftCertNumber { set; get; }

        public string GiftCertKey { set; get; }

        public float GiftCertUsedAmount { set; get; }

        public long PersonId { set; get; }

        public long RecipientPersonId { set; get; }

        [Required(ErrorMessage = "Please enter catcha answer")]
        [Display(Name = "Captcha answer")]
        public string CaptchaAnswer { set; get; }

        public string CaptchaNumber0 { set; get; }

        public string CaptchaNumber1 { set; get; }

        public bool CreditCardProcessStatus { set; get; }

        public string CreditCardLast4 { set; get; }

        public string CreditCardProcessMessage { set; get; }

        [Display(Name = "Sender email address")]
        //[EmailAddress(ErrorMessage = "Please enter valid register email address")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter valid sender email addres")]
        [Required(ErrorMessage = "Please enter sender email address")]
        public string SenderEmailAddress { get; set; }

        [Display(Name = "Sender password")]
        [Required(ErrorMessage = "Please enter sender password")]
        public string SenderPassword { get; set; }

        [Compare(nameof(RecipientEmailAddress), ErrorMessage = "Register email address & confirm do not match")]
        [Display(Name = "Confirm recipient email address")]
        //[EmailAddress(ErrorMessage = "Please enter valid confirm register email address")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter valid confirm email addres")]
        [Required(ErrorMessage = "Please enter confirm email address")]
        public string ConfirmRecipientEmailAddress { get; set; }

        [Display(Name = "Recipient email address")]
        //[EmailAddress(ErrorMessage = "Please enter valid register email address")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter valid recipient email addres")]
        [Required(ErrorMessage = "Please enter recipient email address")]
        public string RecipientEmailAddress { get; set; }

        public string RecipientEmailAddressRegistered { get; set; }

        public long CreditCardDataId { set; get; }

        [Display(Name = "Expiry")]
        //[Required(ErrorMessage = "Select expiry")]
        public string CardExpiry { get; set; }

        [Display(Name = "Exp MM")]
        [Required(ErrorMessage = "Select month")]
        public string CardExpiryMM { get; set; }

        [Display(Name = "Exp YY")]
        [Required(ErrorMessage = "Select year")]
        public string CardExpiryYYYY { get; set; }

        [Display(Name = "Name as on card")]
        [MaxLength(250, ErrorMessage = "Enter valid name (as on card)")]
        [Required(ErrorMessage = "Enter name as on card")]
        public string CardHolderName { get; set; }

        [Display(Name = "Credit Card#")]
        [MinLength(13, ErrorMessage = "Card# to be between 13 & 19 digits")]
        [MaxLength(19, ErrorMessage = "Card# to be between 13 & 19 digits")]
        [Required(ErrorMessage = "Enter credit card#")]
        public string CreditCardNumber { get; set; }
        //regex = "^[0-9]{13, 19}$";

        public string Currency { set; get; }

        [Display(Name = "CVV")]
        [MinLength(3, ErrorMessage = "CVV to be 3 or 4 digits")]
        [MaxLength(4, ErrorMessage = "CVV to be 3 or 4 digits")]
        [Required(ErrorMessage = "Enter CVV")]
        public string CVV { set; get; }
        //regex = "^[0-9]{3, 4}$";

        public string SelectedTemplateImageId { set; get; }

        [Required(ErrorMessage = "Select template")]
        public string SelectedTemplateName { set; get; }

        [Display(Name = "Sender full name")]
        [MaxLength(250, ErrorMessage = "Enter valid sender full name")]
        [Required(ErrorMessage = "Enter Sender full name")]
        public string SenderFullName { get; set; }

        [Display(Name = "Recipient full name")]
        [MaxLength(250, ErrorMessage = "Enter valid  recipient full name")]
        [Required(ErrorMessage = "Enter Recipient full name")]
        public string RecipientFullName { get; set; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
