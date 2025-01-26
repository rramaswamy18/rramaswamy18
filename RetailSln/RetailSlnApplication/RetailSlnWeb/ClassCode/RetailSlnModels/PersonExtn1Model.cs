using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class PersonExtn1Model : AuditInfoModel
    {
        public long? PersonExtn1Id { set; get; }
        public long ClientId { set; get; }
        public long? PersonId { set; get; }
        public long? CorpAcctId { set; get; }
        public long? CorpAcctLocationId { set; get; }
        public CorpAcctModel CorpAcctModel { set; get; }
        public CorpAcctLocationModel CorpAcctLocationModel { set; get; }
        public PersonModel PersonModel { set; get; }
    }
}
