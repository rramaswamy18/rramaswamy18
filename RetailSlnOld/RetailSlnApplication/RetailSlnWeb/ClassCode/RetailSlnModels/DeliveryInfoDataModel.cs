﻿using ArchitectureLibraryModels;
using RetailSlnEnumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class DeliveryInfoDataModel
    {
        [Required(ErrorMessage = "Country")]
        public long? AlternateTelephoneDemogInfoCountryId { set; get; }

        [Display(Name = "Alt. Phone#")]
        [Required(ErrorMessage = "Alternate phone#")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "10 digit phone#")]
        public string AlternateTelephoneNum { set; get; }

        public long? AlternateTelephoneTelephoneCode { set; get; }

        public bool CreateDeliveryAddress { set; get; }

        [Display(Name = "Delivery Instructions")]
        public string DeliveryInstructions { set; get; }

        public DemogInfoAddressModel DeliveryAddressModel { set; get; }

        [Display(Name = "Delivery Method")]
        [Required(ErrorMessage = "Please select a delivery method")]
        public DeliveryMethodEnum? DeliveryMethodId { set; get; }

        public string DeliveryMethodName { set; get; }

        public DeliveryTypeEnum? DeliveryTypeId { set; get; }

        public long OrderHeaderId { set; get; }

        [Display(Name = "Payment Method")]
        [Required(ErrorMessage = "Please select payment method")]
        public PaymentModeEnum? PaymentModeId { set; get; }

        public string PaymentModeDesc { set; get; }

        public string PaymentModeName { set; get; }

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
