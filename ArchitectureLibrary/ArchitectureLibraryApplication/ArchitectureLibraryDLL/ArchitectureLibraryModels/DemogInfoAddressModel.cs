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
        [Required(ErrorMessage = "Please enter address line# 1")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Please enter valid address line# 1")]
        public string AddressLine1 { set; get; }

        [Display(Name = "Address line# 2")]
        [StringLength(100, ErrorMessage = "Please enter valid address line# 2")]
        public string AddressLine2 { set; get; }

        public AddressTypeEnum? AddressTypeId { set; get; }

        [Display(Name = "Type")]
        [Required(ErrorMessage = "Select Building Type")]
        public BuildingTypeEnum? BuildingTypeId { set; get; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "Please enter city")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Please enter valid city name")]
        public string CityName { set; get; }

        public string CountryAbbrev { set; get; }

        public string CountyName { set; get; }

        public long? DemogInfoCityId { set; get; }

        public long? DemogInfoCountyId { set; get; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "Select Country")]
        public long? DemogInfoCountryId { set; get; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "Select State")]
        public long? DemogInfoSubDivisionId { set; get; }

        public long? DemogInfoZipId { set; get; }

        public long? DemogInfoZipPlusId { set; get; }

        [Display(Name = "House#")]
        public string HouseNumber { set; get; }

        public string StateAbbrev { set; get; }

        [Display(Name = "Zip Code")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Please enter 5 digit valid zip code")]
        [Required(ErrorMessage = "Please enter zip code")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Please enter 5 digit zip code")]
        public string ZipCode { set; get; }

        public string ZipPlus4 { set; get; }
        public string Comments { set; get; }
        public string FromDate { set; get; }
        public string ToDate { set; get; }
    }
}
