using ArchitectureLibraryDocumentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolPrdModels
{
    public class PersonExtn1Model : AuditInfoModel
    {
        public long? PersonExtn1Id { set; get; }
        public long ClientId { set; get; }
        public long PersonId { set; get; }
        public long CertificateDocumentId { set; get; }

    }
}
