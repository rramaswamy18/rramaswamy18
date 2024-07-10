using ArchitectureLibraryEnumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ShoppingCartSummaryModel
    {
        public float? AmountPaidByCreditCard { set; get; }
        public float? AmountPaidByGiftCert { set; get; }
        public float? BalanceDue { set; get; }
        public bool ShowDiscountsAdditionalCharges { set; get; }
        public float? TotalAmountPaid { set; get; }
        public float? TotalDiscountAmount { set; get; }
        public float? TotalInvoiceAmount { set; get; }
        public long? TotalItemsCount { set; get; }
        public float? TotalOrderAmount { set; get; }
        public float? TotalOrderAmountBeforeDiscount { set; get; }
        public float? TotalProductOrVolumetricWeight { set; get; }
        public long TotalProductOrVolumetricWeightRounded { set; get; }
        public WeightUnitEnum? TotalProductOrVolumetricWeightRoundedUnit { set; get; }
        public float? TotalVolumeValue { set; get; }
        public float? TotalWeightCalc { set; get; }
        public WeightUnitEnum? TotalWeightCalcUnit { set; get; }
    }
}
