using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RetailSlnModels
{
    public class DeliveryAddressModel
    {
        public List<KeyValuePair<long, string>> DeliveryDemogInfoCountrys {  set; get; }
        public List<KeyValuePair<long, List<KeyValuePair<long, string>>>> DeliveryDemogInfoCountrySubDivisions { set; get; }
        public List<SelectListItem> DeliveryDemogInfoCountrySelectListItems { set; get; }
        public Dictionary<long, List<SelectListItem>> DeliveryDemogInfoSubDivisionSelectListItems { set; get; }
        public DemogInfoAddressModel DeliveryAddressDataModel { set; get; }
    }
}
