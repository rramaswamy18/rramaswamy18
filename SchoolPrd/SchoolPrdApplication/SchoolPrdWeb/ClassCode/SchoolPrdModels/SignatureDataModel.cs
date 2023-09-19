using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolPrdModels
{
    public class SignatureDataModel
    {
        public List<string> ClientDateTimes { set; get; }
        public string DocumentTypeNameDesc { set; get; }
        public string HtmlContent { set; get; }
        public List<string> InitialSignatureDetailIds { set; get; }
        public long InitialSignatureTotalCount { set; get; }
        public long InitialSignatureSignedCount { set; get; }
        //public long InitialsTextId { set; get; }
        //public string InitialsTextValue { set; get; }
        //public long SignatureTextId { set; get; }
        //public string SignatureTextValue { set; get; }
        //public long AdminSignatureTextId { set; get; }
        //public string AdminSignatureTextValue { set; get; }
        public string TabName { set; get; }
    }
}
