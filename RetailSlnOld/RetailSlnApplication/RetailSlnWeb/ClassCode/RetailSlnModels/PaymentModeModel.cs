using ArchitectureLibraryModels;
using RetailSlnEnumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class PaymentModeModel
    {
        public string PaymentModeDesc { set; get; }
        public string PaymentModeDesc1 { set; get; }
        [Display(Name = "Payment...")]
        [Required(ErrorMessage = "Select Payment")]
        public PaymentModeEnum? PaymentModeId { set; get; }
        public string PaymentModeName { set; get; }
        public List<CodeDataModel> PaymentModes { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
