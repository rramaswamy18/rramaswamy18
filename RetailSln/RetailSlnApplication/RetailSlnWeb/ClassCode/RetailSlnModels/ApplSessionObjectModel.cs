using RetailSlnEnumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ApplSessionObjectModel
    {
        public long CorpAcctLocationId { set; get; }
        public CorpAcctModel CorpAcctModel { set; get; }
        public float TotalBalanceDue { set; get; }
    }
}
