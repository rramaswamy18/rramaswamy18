using ArchitectureLibraryEnumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArchitectureLibraryModels
{
    public class DemogInfoAddressModel : AuditInfoModel
    {
        public long DemogInfoAddressId { set; get; }

        public long ClientId { set; get; }

        [Display(Name = "Address line# 1")]
        [Required(ErrorMessage = "Enter address line 1")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Enter valid address line 1")]
        public string AddressLine1 { set; get; }

        [Display(Name = "Address line# 2")]
        [StringLength(100, ErrorMessage = "Enter valid address line 2")]
        public string AddressLine2 { set; get; }

        public AddressTypeEnum? AddressTypeId { set; get; }

        [Display(Name = "Type")]
        [Required(ErrorMessage = "Type")]
        public BuildingTypeEnum? BuildingTypeId { set; get; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City Name")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "City Name")]
        public string CityName { set; get; }

        public string CountryAbbrev { set; get; }

        public string CountryDesc { set; get; }

        public string CountyName { set; get; }

        public long? DemogInfoCityId { set; get; }

        public long? DemogInfoCountyId { set; get; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country")]
        public long? DemogInfoCountryId { set; get; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "State")]
        public long? DemogInfoSubDivisionId { set; get; }

        public long? DemogInfoZipId { set; get; }

        public long? DemogInfoZipPlusId { set; get; }

        [Display(Name = "House#")]
        [MaxLength(50, ErrorMessage = "50 chars")]
        public string HouseNumber { set; get; }

        public string StateAbbrev { set; get; }

        [Display(Name = "Postal Code")]
        //[RegularExpression(@"^\d{5}$", ErrorMessage = "Zip Code")]
        [Required(ErrorMessage = "Postal Code")]
        //[StringLength(5, MinimumLength = 5, ErrorMessage = "Zip Code")]
        public string ZipCode { set; get; }

        public string ZipPlus4 { set; get; }
        public string Comments { set; get; }
        public string FromDate { set; get; }
        public string ToDate { set; get; }
    }
}
