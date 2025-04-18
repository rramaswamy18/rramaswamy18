using ArchitectureLibraryEnumerations;
using ArchitectureLibraryModels;
using RetailSlnEnumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class CorpAcctModel : AuditInfoModel
    {
        public long? CorpAcctId { set; get; }

        public long ClientId { set; get; }

        public string CorpAcctKey { set; get; }

        [Display(Name = "Corp Acct Name")]
        [Required(ErrorMessage = "Enter corp acct name")]
        public string CorpAcctName { set; get; }

        [Display(Name = "Corp Acct Type")]
        [Required(ErrorMessage = "Select corp account type")]
        public CorpAcctTypeEnum? CorpAcctTypeId { set; get; }

        [Display(Name = "Credit Days")]
        [Required(ErrorMessage = "Enter credit days")]
        public short? CreditDays { set; get; }

        [Display(Name = "Credit Limit")]
        [Required(ErrorMessage = "Enter credit limit")]
        public float? CreditLimit { set; get; }

        [Display(Name = "Credit Sale")]
        [Required(ErrorMessage = "Select credit sale")]
        public YesNoEnum? CreditSale { set; get; }

        [Display(Name = "Default Discount %")]
        [Required(ErrorMessage = "Enter default discount")]
        public float? DefaultDiscountPercent { set; get; }

        [Display(Name = "Min Order Amount")]
        [Required(ErrorMessage = "Enter min order amount")]
        public float? MinOrderAmount { set; get; }

        [Display(Name = "Order Approval")]
        [Required(ErrorMessage = "Select order approval")]
        public YesNoEnum? OrderApprovalRequired { set; get; }

        [Display(Name = "S&H Charges")]
        [Required(ErrorMessage = "Select S&H Charges")]
        public YesNoEnum? ShippingAndHandlingCharges { set; get; }

        [Display(Name = "Tax Ident#")]
        [Required(ErrorMessage = "Enter Tax Ident#")]
        public string TaxIdentNum { set; get; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Select status")]
        public YesNoEnum? StatusId { set; get; }

        public List<CodeDataModel> CorpAcctTypes { set; get; }

        public List<CorpAcctLocationModel> CorpAcctLocationModels { set; get; }

        public List<DiscountDtlModel> DiscountDtlModels { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
