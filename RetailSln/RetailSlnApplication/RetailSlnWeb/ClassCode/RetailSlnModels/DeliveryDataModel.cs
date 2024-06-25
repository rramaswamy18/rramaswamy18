using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class DeliveryDataModel
    {
        [Required(ErrorMessage = "Country")]
        public long? AlternateTelephoneDemogInfoCountryId { set; get; }

        [Display(Name = "Alt. Phone#")]
        [Required(ErrorMessage = "Alternate phone#")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "10 digit phone#")]
        public string AlternateTelephoneNum { set; get; }

        public long? AlternateTelephoneTelephoneCode { set; get; }

        [Display(Name = "Delivery Instructions")]
        public string DeliveryInstructions { set; get; }

        [Required(ErrorMessage = "Country")]
        public long? PrimaryTelephoneDemogInfoCountryId { set; get; }

        [Display(Name = "Prim. Phone#")]
        [Required(ErrorMessage = "Primary phone#")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "10 digit phone#")]
        public string PrimaryTelephoneNum { set; get; }

        public long? PrimaryTelephoneTelephoneCode { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
