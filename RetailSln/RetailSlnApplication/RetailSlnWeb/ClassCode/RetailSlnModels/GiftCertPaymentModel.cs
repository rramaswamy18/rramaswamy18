using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class GiftCertPaymentModel
    {
        public float? GiftCertPaymentAmount { set; get; }

        [Display(Name = "Gift Cert#")]
        [MinLength(16, ErrorMessage = "Enter 16 digit Gift Cert number")]
        [MaxLength(16, ErrorMessage = "Enter 16 digit Gift Cert number")]
        [Required(ErrorMessage = "Please enter gift cert number")]
        public string GiftCertNumber { set; get; }

        public string GiftCertNumberLast4 { set; get; }

        [Display(Name = "Gift Cert Key")]
        [MinLength(8, ErrorMessage = "Enter 8 digit Gift Cert key")]
        [MaxLength(8, ErrorMessage = "Enter 8 digit Gift Cert key")]
        [Required(ErrorMessage = "Please enter gift cert key")]
        public string GiftCertKey { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
