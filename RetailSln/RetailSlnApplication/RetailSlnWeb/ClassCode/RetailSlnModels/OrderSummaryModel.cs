using RetailSlnEnumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class OrderSummaryModel
    {
        //public string AspNetUserId { set; get; }

        //public string AuthorizedSignatureFontFamily { set; get; }

        //public string AuthorizedSignatureFontSize { set; get; }

        //public long AuthorizedSignatureTextId { set; get; }

        //public string AuthorizedSignatureTextValue { set; get; }

        public CorpAcctModel CorpAcctModel { set; get; }

        //public string CreatedByEmailAddress { set; get; }

        //public string CreatedByFirstName { set; get; }

        //public string CreatedByLastName { set; get; }

        public string OrderDateTime { set; get; }

        [Display(Name = "Addtl Charges")]
        //[Range(1, 999999, ErrorMessage = "Enter valid addtl charges")]
        //[Required(ErrorMessage = "Enter addtl charges")]
        public float? AdditionalCharges { set; get; }

        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Enter valid email address")]
        [Required(ErrorMessage = "Enter email address")]
        public string EmailAddress { set; get; }

        public bool EmailExists { set; get; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "First Name")]
        public string FirstName { set; get; }

        public string InvoiceType { set; get; }

        public InvoiceTypeEnum? InvoiceTypeId { set; get; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Last Name")]
        public string LastName { set; get; }

        public long? OrderHeaderId { set; get; }
    }
}
