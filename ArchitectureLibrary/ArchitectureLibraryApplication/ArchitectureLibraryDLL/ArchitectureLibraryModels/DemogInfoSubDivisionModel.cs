using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryModels
{
    public class DemogInfoSubDivisionModel : AuditInfoModel
    {
        public long DemogInfoSubDivisionId { get; set; }
        public long DemogInfoCountryId { get; set; }
        public string StateAbbrev { get; set; }
        public string SubDivisionCode { get; set; }
        public string SubDivisionDesc { get; set; }
        public string SubDivisionCategoryNameDesc { get; set; }
        public string ParentSubDivisionCode { get; set; }
        public DemogInfoCountryModel DemogInfoCountryModel { set; get; }
    }
}
