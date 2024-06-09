using ArchitectureLibraryEnumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ApiDemogInfoAddressModel
    {
        public long DemogInfoAddressId { get; set; }
        public long ClientId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public BuildingTypeEnum? BuildingTypeId { get; set; }
        public string CityName { get; set; }
        public string CountryAbbrev { get; set; }
        public string CountryDesc { get; set; }
        public string CountyName { get; set; }
        public long? DemogInfoCityId { get; set; }
        public long? DemogInfoCountyId { get; set; }
        public long? DemogInfoCountryId { get; set; }
        public long? DemogInfoSubDivisionId { get; set; }
        public long? DemogInfoZipId { get; set; }
        public long? DemogInfoZipPlusId { get; set; }
        public string FromDate { get; set; }
        public string HouseNumber { get; set; }
        public string StateAbbrev { get; set; }
        public string ToDate { get; set; }
        public string ZipCode { get; set; }
        public string ZipPlus4 { get; set; }
    }
}
