using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class SearchForUserDataModel
    {
        public string SearchText { set; get; }
        public string CorpAcctName { set; get; }
        public long CorpAcctLocationId { set; get; }
        public string EmailAddress { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string LocationName { set; get; }
        public long PersonId { set; get; }
        public CorpAcctLocationModel CorpAcctLocationModel { set; get; }
        public List<CorpAcctLocationModel> CorpAcctLocationModels { set; get; }
        public List<PersonModel> PersonModels { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
