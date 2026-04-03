using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class CRMListModel : ResponseObjectModel
    {
        public long CRMListId { set; get; }
        public long ClientId { set; get; }
        public string TelephoneCode { set; get; }
        public long? TelephoneCountryId { set; get; }
        public string TelephoneNumber { set; get; }
        public string TelephoneNumberFormatted { set; get; }
        public long TelephoneNumberValid { set; get; }
        public string CustomerCode { set; get; }
        public string CustomerName { set; get; }
        public string Mobile { set; get; }

        public long? TelephoneCountryIdSave { set; get; }
        public string TelephoneNumberSave { set; get; }
        public long TelephoneNumberValidSave { set; get; }

        public string TelephoneNumberError { set; get; }
    }
}
