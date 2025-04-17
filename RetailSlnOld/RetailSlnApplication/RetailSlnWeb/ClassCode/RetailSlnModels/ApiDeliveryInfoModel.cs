using RetailSlnEnumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ApiDeliveryInfoModel
    {
        public long? AlternateTelephoneDemogInfoCountryId { set; get; }
        public string AlternateTelephoneFormatted { set; get; }
        public string AlternateTelephoneHref { set; get; }
        public string AlternateTelephoneNum { set; get; }
        public long? AlternateTelephoneTelephoneCode { set; get; }
        public bool CreateDeliveryAddress { set; get; }
        public string DeliveryInstructions { set; get; }
        public ApiDemogInfoAddressModel DeliveryAddressModel { set; get; }
        public ApiDeliveryMethodModel DeliveryMethodModel { set; get; }
        public ApiPaymentMethodModel PaymentMethodModel { set; get; }
        public long? PrimaryTelephoneDemogInfoCountryId { set; get; }
        public string PrimaryTelephoneFormatted { set; get; }
        public string PrimaryTelephoneHref { set; get; }
        public string PrimaryTelephoneNum { set; get; }
        public long? PrimaryTelephoneTelephoneCode { set; get; }
    }
}
