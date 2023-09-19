using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolPrdModels
{
    public class InitialSignatureDetailModel : AuditInfoModel
    {
        public long? InitialSignatureDetailId { set; get; }

        public long ClientId { get; set; }

        public string Height { set; get; }

        public long? InitialSignatureId { set; get; }

        public string InitialSignatureTypeNameDesc { set; get; }

        public float? SeqNum { get; set; }

        public string Width { set; get; }

        public InitialSignatureModel InitialSignatureModel { set; get; }
    }
}
