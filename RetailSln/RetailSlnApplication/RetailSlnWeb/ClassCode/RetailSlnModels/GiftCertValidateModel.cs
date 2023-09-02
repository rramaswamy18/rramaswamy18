using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class GiftCertValidateModel
    {
        [Display(Name = "Gift Cert#")]
        [MinLength(16, ErrorMessage = "Enter 16 digit Gift Cert number")]
        [MaxLength(16, ErrorMessage = "Enter 16 digit Gift Cert number")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter valid 16 digit Gift Cert number")]
        [Required(ErrorMessage = "Enter Gift Cert number")]
        public string GiftCertNumber { set; get; }

        [Display(Name = "Gift Cert Key")]
        [MinLength(8, ErrorMessage = "Enter 8 digit Gift Cert key")]
        [MaxLength(8, ErrorMessage = "Enter 8 digit Gift Cert key")]
        [Required(ErrorMessage = "Enter Gift Cert key")]
        public string GiftCertKey { set; get; }
    }
}
