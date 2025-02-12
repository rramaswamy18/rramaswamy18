using ArchitectureLibraryEnumerations;
using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class CorpAcctLocationModel : AuditInfoModel
    {
        public long? CorpAcctLocationId { set; get; }

        public long ClientId { set; get; }

        [Display(Name = "Alternate Telephone Country")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Enter valid 10 digit phone#")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Enter 10 digit phone#")]
        public long? AlternateTelephoneCountryId { set; get; }

        [Display(Name = "Alternate Telephone#")]
        public long? AlternateTelephoneNumber { set; get; }

        public long? CorpAcctId { set; get; }

        public long? DemogInfoAddressId { set; get; }

        [Display(Name = "Location Name")]
        [Required(ErrorMessage = "Enter Location Name")]
        public string LocationName { set; get; }

        [Display(Name = "Primary Telephone Country")]
        [Required(ErrorMessage = "Enter Primary Telephone Country")]
        public long? PrimaryTelephoneCountryId { set; get; }

        [Display(Name = "Primary Telephone#")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Enter valid 10 digit phone#")]
        [Required(ErrorMessage = "Enter Primary Telephone#")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Enter 10 digit phone#")]
        public long? PrimaryTelephoneNumber { set; get; }

        public float? SeqNum { set; get; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Select Status")]
        public YesNoEnum? StatusId {  set; get; }

        public CorpAcctModel CorpAcctModel { set; get; }

        public DemogInfoAddressModel DemogInfoAddressModel { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
