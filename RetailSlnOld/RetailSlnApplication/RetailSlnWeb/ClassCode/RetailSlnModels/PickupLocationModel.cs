using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class PickupLocationModel : AuditInfoModel
    {
        public long? PickupLocationId { set; get; }
        public long ClientId { set; get; }
        public string LocationNameDesc { set; get; }
        public string LocationDesc { set; get; }
        public long LocationDemogInfoAddressId { set; get; }
        public DemogInfoAddressModel DemogInfoAddressModel { set; get; }
    }
}
