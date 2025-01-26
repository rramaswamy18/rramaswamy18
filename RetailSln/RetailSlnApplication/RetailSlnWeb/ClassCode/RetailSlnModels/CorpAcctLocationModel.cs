using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class CorpAcctLocationModel : AuditInfoModel
    {
        public long? CorpAcctLocationId { set; get; }

        public long ClientId { set; get; }

        public long? AlternateTelephoneCountryId { set; get; }

        public long? AlternateTelephoneNumber { set; get; }

        public long CorpAcctId { set; get; }

        public long DemogInfoAddressId { set; get; }

        public string LocationName { set; get; }

        public long? PrimaryTelephoneCountryId { set; get; }

        public long? PrimaryTelephoneNumber { set; get; }

        public float? SeqNum { set; get; }

        public DemogInfoAddressModel DemogInfoAddressModel { set; get; }

        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
