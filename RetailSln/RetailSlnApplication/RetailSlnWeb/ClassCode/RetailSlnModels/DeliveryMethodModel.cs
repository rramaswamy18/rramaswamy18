using ArchitectureLibraryModels;
using RetailSlnEnumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RetailSlnModels
{
    public class DeliveryMethodModel
    {
        public string DeliveryMethodDesc { set; get; }
        public string DeliveryMethodDesc1 { set; get; }
        //[Display(Name = "Delivery Method")]
        //[Required(ErrorMessage = "Select Delivery Method")]
        public DeliveryMethodEnum? DeliveryMethodId { set; get; }
        [Display(Name = "Delivery...")]
        [Required(ErrorMessage = "Select delivery")]
        public string DeliveryMethodIdPickupLocationId { set; get; }
        public string DeliveryMethodName { set; get; }
        public List<CodeDataModel> DeliveryMethods { set; get; }
        public List<SelectListItem> DeliveryMethodPickupLocationSelectListItems { set; get; }
        public List<DemogInfoAddressModel> PickupLocationDemogInfoAddressModels { set; get; }
        //[Display(Name = "Pickup Location")]
        //[Required(ErrorMessage = "Select Pickup Location")]
        public long? PickupLocationId { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
