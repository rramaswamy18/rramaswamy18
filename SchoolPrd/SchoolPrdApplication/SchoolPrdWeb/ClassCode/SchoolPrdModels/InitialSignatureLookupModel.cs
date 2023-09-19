using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolPrdModels
{
    public class InitialSignatureLookupModel
    {
        public string InitialSignatureCategory { set; get; }//This could be Enum - Initials, Signature
        public string InitialSignatureFontName { set; get; }
        public string InitialSignatureFontSize { set; get; }
        public string InitialSignatureTextValue { set; get; }
        public string InitialSignatureType { set; get; }//This could be Enum - User, Representative
    }
}
