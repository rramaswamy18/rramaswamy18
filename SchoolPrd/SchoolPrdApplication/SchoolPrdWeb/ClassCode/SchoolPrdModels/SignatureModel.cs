using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolPrdModels
{
    public class SignatureModel : AuditInfoModel
    {
        public long ClientId { set; get; }

        public string ClientDateTimeBase { set; get; }

        public long? ClassEnrollId { set; get; }

        public string PostMethod { set; get; }

        public ClassEnrollModel ClassEnrollModel { set; get; }

        public List<ClassEnrollModel> ClassEnrollModels { set; get; }

        public string ServerDateTimeBase { set; get; }

        public List<SignatureDataModel> SignatureDataModels { set; get; }

        public int? TotalInitialsSignatureCount { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
