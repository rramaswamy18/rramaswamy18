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
        public string CouponNumber { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
