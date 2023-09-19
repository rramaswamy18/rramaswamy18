using SchoolPrdEnumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPrdModels
{
    public class PaymentCheckModel
    {
        [Display(Name = "Check Amount")]
        [Required(ErrorMessage = "Enter valid Check Amount")]
        public decimal? CheckAmount { set; get; }
        [Display(Name = "Check Date")]
        [Required(ErrorMessage = "Enter valid Date")]
        public DateTime? CheckDate { set; get; }
        [Display(Name = "Memo")]
        [Required(ErrorMessage = "Enter CheckMemo")]
        public string CheckMemo {set; get; }
        [Display(Name = "Check#")]
        [Required(ErrorMessage = "Enter valid CheckNumber")]
        public string CheckNumber { set; get; }
        [Display(Name = "Payer Name")]
        [Required(ErrorMessage = "Enter valid PayerName")]
        public string PayerName { set; get; }
        public long? PaymentId { set; get; }
        public long? JobBoardPaymentId { set; get; }
        public PaymentMethodEnum? PaymentMethodId { set; get; }
    }
}
