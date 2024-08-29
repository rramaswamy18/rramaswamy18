using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class OrderSummaryModel
    {
        public string AspNetUserId { set; get; }
        //public long? DeliveryAddressId { set; get; }
        //public float BalanceDue { set; get; }
        //public string BalanceDueFormatted { set; get; }

        public CorpAcctModel CorpAcctModel { set; get; }

        public string EmailAddress { set; get; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "First Name")]
        public string FirstName { set; get; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Last Name")]
        public string LastName { set; get; }

        public long? OrderHeaderId { set; get; }

        public long? PersonId { set; get; }

        public string TelephoneCode { set; get; }

        public string TelephoneCountryId { set; get; }

        public string TelephoneNumber { set; get; }

        public string UserFullName { set; get; }
    }
}
