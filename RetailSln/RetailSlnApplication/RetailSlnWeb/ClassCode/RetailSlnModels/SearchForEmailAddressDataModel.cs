using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class SearchForEmailAddressDataModel
    {
        public string CorpAcctName { set; get; }
        public long CorpAcctLocationId { set; get; }
        public string EmailAddress { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string LocationName { set; get; }
        public long PersonId { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
