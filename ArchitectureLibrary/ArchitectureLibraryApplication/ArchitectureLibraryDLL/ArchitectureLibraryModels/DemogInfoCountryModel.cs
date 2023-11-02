using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryModels
{
    public class DemogInfoCountryModel : AuditInfoModel
    {
        public long DemogInfoCountryId { set; get; }
        public string CountryAbbrev { set; get; }
        public string CountryDesc { set; get; }
        public string Alpha2Code { set; get; }
        public string Alpha3Code { set; get; }
        public string NumericCode { set; get; }
        public string SubDivisionCodeHyperLink { set; get; }
        public short? TelephoneCode { set; get; }
        public string PostalCodeLabel { set; get; }
        public string PostalCodeRegEx { set; get; }
        public List<DemogInfoSubDivisionModel> DemogInfoSubDivisionModels { set; get; }
    }
}
