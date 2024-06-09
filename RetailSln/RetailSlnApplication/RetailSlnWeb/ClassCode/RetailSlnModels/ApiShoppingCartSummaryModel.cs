using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ApiShoppingCartSummaryModel
    {
        public CorpAcctModel CorpAcctModel { set; get; }
        public long? PersonId { set; get; }
        public long? TotalItemsCount { set; get; }
        public float? TotalProductOrVolumetricWeight { set; get; }
        public long TotalProductOrVolumetricWeightRounded { set; get; }
        public float? TotalShoppingCartAmount { set; get; }
        public float? TotalVolumeValue { set; get; }
        public float? TotalWeightCalc { set; get; }
    }
}
