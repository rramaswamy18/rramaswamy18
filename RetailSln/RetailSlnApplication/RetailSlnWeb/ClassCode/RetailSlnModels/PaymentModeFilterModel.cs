using ArchitectureLibraryEnumerations;
using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class PaymentModeFilterModel : AuditInfoModel
    {
        public long PaymentModeFilterId { set; get; }
        public long ClientId { set; get; }
        public YesNoEnum CreditSale { set; get; }
        public long PaymentModeId { set; get; }
    }
}
