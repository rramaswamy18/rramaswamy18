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
        //[StringLength(18, MinimumLength = 18, ErrorMessage = "Enter 18 digit Gift Cert#")]
        //[Required(ErrorMessage = "Enter gift cert#")]
        public string GiftCertNumber { set; get; }

        public string GiftCertNumberLast4 { set; get; }

        [Display(Name = "Gift Cert Key")]
        //[StringLength(9, MinimumLength = 9, ErrorMessage = "Enter 9 digit Gift Cert key")]
        //[Required(ErrorMessage = "Enter gift cert key")]
        public string GiftCertKey { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
