using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryModels
{
    public class DemogInfoCountryModel : AuditInfoModel
    {
        public long DemogInfoCountryId { get; set; }
        public string CountryAbbrev { get; set; }
        public string CountryDesc { get; set; }
        public string Alpha2Code { get; set; }
        public string Alpha3Code { get; set; }
        public string NumericCode { get; set; }
        public string SubDivisionCodeHyperLink { get; set; }
        public short? TelephoneCode { get; set; }
        public List<DemogInfoSubDivisionModel> DemogInfoSubDivisionModels { set; get; }
    }
}
