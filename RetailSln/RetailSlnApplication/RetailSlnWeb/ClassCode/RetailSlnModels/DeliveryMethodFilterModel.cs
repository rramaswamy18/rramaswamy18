using ArchitectureLibraryEnumerations;
using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class DeliveryMethodFilterModel : AuditInfoModel
    {
        public long DeliveryMethodFilterId { set; get; }
        public long ClientId { set; get; }
        public YesNoEnum ShippingAndHandlingCharges { set; get; }
        public long DeliveryMethodId { set; get; }
    }
}
