using ArchitectureLibraryModels;
using RetailSlnEnumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class DeliveryMethodModel
    {
        public string DeliveryMethodDesc { set; get; }
        [Display(Name = "Delivery Method")]
        [Required(ErrorMessage = "Select Delivery Method")]
        public DeliveryMethodEnum? DeliveryMethodId { set; get; }
        public string DeliveryMethodName { set; get; }
        public List<CodeDataModel> DeliveryMethods { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
