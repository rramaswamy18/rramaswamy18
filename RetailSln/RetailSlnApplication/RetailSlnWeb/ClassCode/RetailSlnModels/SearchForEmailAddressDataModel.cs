using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class SearchForEmailAddressDataModel
    {
        public long PersonId { set; get; }
        public string EmailAddress { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
