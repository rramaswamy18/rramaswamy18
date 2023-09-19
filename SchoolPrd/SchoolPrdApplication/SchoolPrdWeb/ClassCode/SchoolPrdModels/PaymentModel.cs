using ArchitectureLibraryModels;
using SchoolPrdEnumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPrdModels
{
    public class PaymentModel
    {
        public long? PaymentId { set; get; }
        public decimal? AmountPaid { set; get; }
        public decimal? BalanceAmount { set; get; }
        public long? ClassEnrollId { set; get; }
        public PaymentStatusEnum? PaymentStatusId { set; get; }
        [Required(ErrorMessage = "Please select one of the payment methods")]
        public decimal? TotalAmount { set; get; }
        public ClassEnrollModel ClassEnrollModel { set; get; }
        public PaymentCheckModel PaymentCheckModel { set; get; }
        public List<PaymentCheckModel> PaymentCheckModels { set; get; }
        public PaymentCreditCardModel PaymentCreditCardModel { set; get; }
        public List<PaymentCreditCardModel> PaymentCreditCardModels { set; get; }
        public string PaymentMethodSelected { set; get; }
        public PaymentCheckModel ThirdPartyPaymentCheckModel { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
