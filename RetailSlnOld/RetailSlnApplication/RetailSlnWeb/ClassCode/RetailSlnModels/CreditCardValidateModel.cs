using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class CreditCardValidateModel
    {
        [Display(Name = "Exp MM")]
        [Required(ErrorMessage = "Select month")]
        public string CardExpiryMM { get; set; }

        [Display(Name = "Exp YY")]
        [Required(ErrorMessage = "Select year")]
        [StringLength(4, ErrorMessage = "Enter valid 4 digit expiry year")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter valid expiry year")]
        public string CardExpiryYYYY { get; set; }

        [Display(Name = "Name as on card")]
        [MinLength(1, ErrorMessage = "Enter valid name (as on card)")]
        [MaxLength(250, ErrorMessage = "Enter valid name (as on card)")]
        [Required(ErrorMessage = "Enter name as on card")]
        public string CardHolderName { get; set; }

        [Display(Name = "Credit Card#")]
        [MinLength(13, ErrorMessage = "Card# to be between 13 & 19 digits")]
        [MaxLength(19, ErrorMessage = "Card# to be between 13 & 19 digits")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter valid card# to be between 13 & 19 digits")]
        [Required(ErrorMessage = "Enter credit card#")]
        public string CreditCardNumber { get; set; }
        //regex = "^[0-9]{13, 19}$";

        [Display(Name = "CVV")]
        [MinLength(3, ErrorMessage = "CVV to be 3 or 4 digits")]
        [MaxLength(4, ErrorMessage = "CVV to be 3 or 4 digits")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter valid CVV between 3 & 9 digits")]
        [Required(ErrorMessage = "Enter CVV")]
        public string CVV { set; get; }
    }
}
