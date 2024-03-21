using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class BusinessInfoModel
    {
        public string BaseUrl { set; get; }
        public string BusinessName1 { set; get; }
        public string BusinessName2 { set; get; }
        public List<DemogInfoAddressModel> DemogInfoAddressModels { set; get; }
        public string LogoImageName { set; get; }
        public string LogoRelativeUrl { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
