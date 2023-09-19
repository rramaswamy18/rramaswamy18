using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolPrdModels
{
    public class InitialSignatureModel : AuditInfoModel
    {
        public long? InitialSignatureId { set; get; }
        public long ClientId { get; set; }
        public string DocumentTypeNameDesc { set; get; }
        public string TabName { set; get; }
        public List<InitialSignatureDetailModel> InitialSignatureDetailModels { set; get; }
    }
}
