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
        [MinLength(18, ErrorMessage = "Enter 18 digit Gift Cert number")]
        [MaxLength(18, ErrorMessage = "Enter 18 digit Gift Cert number")]
        //[Required(ErrorMessage = "Please enter gift cert number")]
        public string GiftCertNumber { set; get; }

        public string GiftCertNumberLast4 { set; get; }

        [Display(Name = "Gift Cert Key")]
        [MinLength(9, ErrorMessage = "Enter 9 digit Gift Cert key")]
        [MaxLength(9, ErrorMessage = "Enter 9 digit Gift Cert key")]
        //[Required(ErrorMessage = "Please enter gift cert key")]
        public string GiftCertKey { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
