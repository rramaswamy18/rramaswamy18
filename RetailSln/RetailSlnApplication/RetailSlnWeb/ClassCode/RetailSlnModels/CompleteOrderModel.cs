using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class CompleteOrderModel
    {
        public bool ApprovalRequired { set; get; }
        [Display(Name = "Signature")]
        [Required(ErrorMessage = "Select signature")]
        public long? ApproverSignatureTextId { set; get; }
        [Display(Name = "Sign as")]
        [Required(ErrorMessage = "Enter your signature")]
        public string ApproverSignatureTextValue { set; get; }
        public long? PaymentAmount { set; get; }
    }
}
