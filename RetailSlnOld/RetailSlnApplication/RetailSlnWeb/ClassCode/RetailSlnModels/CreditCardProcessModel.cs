using ArchitectureLibraryModels;
using RetailSlnEnumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class CreditCardProcessModel
    {
        public string AspNetRoleName { set; get; }

        public long CreditCardDataId { set; get; }

        public long OrderHeaderId { set; get; }

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
        [MinLength(1, ErrorMessage = "Enter valid name (as on card)")]
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
        [MinLength(3, ErrorMessage = "CVV to be 3 & 5 digits")]
        [MaxLength(5, ErrorMessage = "CVV to be 3 & 5 digits")]
        [Required(ErrorMessage = "Enter CVV")]
        public string CVV { set; get; }
        //regex = "^[0-9]{3, 4}$";

        [Display(Name = "Amount")]
        public float? CreditCardAmount { set; get; }

        [Display(Name = "Amount")]
        public string CreditCardAmountFormatted { set; get; }

        public bool CreditCardProcessStatus { set; get; }

        public string CreditCardProcessMessage { set; get; }

        public string CreditCardNumberLast4 { set; get; }

        public long CorpAcctId { set; get; }

        [Display(Name = "Email Address")]
        public string EmailAddress { set; get; }

        [Display(Name = "User Name")]
        public string UserFullName { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
