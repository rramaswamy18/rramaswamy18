using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
	public class CouponPaymentModel
	{
        [Display(Name = "Coupon#")]
        //[Required(ErrorMessage = "Enter coupon#")]
        public string CouponNumber { set; get; }
        public float? CouponPaymentAmount { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
