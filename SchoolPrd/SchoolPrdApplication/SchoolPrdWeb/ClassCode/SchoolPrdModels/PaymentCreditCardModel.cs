using SchoolPrdEnumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPrdModels
{
    public class PaymentCreditCardModel
    {
        public long? PaymentId { set; get; }
        public long? JobBoardId { get; set; }
        public long? JobBoardPaymentId { set; get; }
        public PaymentMethodEnum? PaymentMethodId { set; get; }
        [Display(Name = "CC Amount")]
        [Required(ErrorMessage = "Enter valid Amount")]
        public decimal? CreditCardAmount { get; set; }
        [Display(Name = "Credit Card#")]
        [Required(ErrorMessage = "Enter valid CreditCardNumber")]
        [StringLength(16, ErrorMessage = "Enter valid CreditCardNumber(16 digits)")]
        [MaxLength(20)]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$")]
        public string CreditCardNumber { get; set; }
        [Display(Name = "CVV")]
        [Required(ErrorMessage = "Enter valid CVV")]
        [StringLength(3, ErrorMessage = "Enter valid CVV(3 digits)")]
        [MaxLength(5)]
        // [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$")]
        public string CreditCardCVV { set; get; }
        [Display(Name = "Expiry")]
        [Required(ErrorMessage = "Enter valid ExpiryMonth")]
        public string CreditCardExpiryMM { get; set; }
        [Display(Name = "Expiry MM/YYYY")]
        [Required(ErrorMessage = "Enter valid ExpiryYear")]
        public string CreditCardExpiryYYYY { get; set; }
        [Display(Name = "Name as on card")]
        [Required(ErrorMessage = "Enter valid Name as on card")]
        public string CreditCardHolderFullName { get; set; }
    }
}
