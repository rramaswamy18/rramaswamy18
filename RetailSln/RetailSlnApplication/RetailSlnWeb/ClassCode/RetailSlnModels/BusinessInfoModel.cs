using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class BusinessInfoModel
    {
        public long ClientId { set; get; }
        public string BaseUrl { set; get; }
        public string BusinessName1 { set; get; }
        public string BusinessName2 { set; get; }
        public string BusinessType { set; get; }
        public string ContactPhoneFormatted { set; get; }
        public string ContactPhoneHref { set; get; }
        public string ContactTextPhoneFormatted { set; get; }
        public string ContactTextPhoneHref { set; get; }
        public string ContactWhatsAppPhone {  set; get; }
        public string ContactWhatsAppPhoneFormatted { set; get; }
        public List<DemogInfoAddressModel> DemogInfoAddressModels { set; get; }
        public string LogoImageFullUrl { set; get; }
        public string LogoImageName { set; get; }
        public string LogoRelativeUrl { set; get; }
        public string PhoneImageFullUrl { get; set; }
        public string SMSImageFullUrl { set; get; }
        public string WhatsAppImageFullUrl { get; set; }
        public string WhatsAppUrl { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
