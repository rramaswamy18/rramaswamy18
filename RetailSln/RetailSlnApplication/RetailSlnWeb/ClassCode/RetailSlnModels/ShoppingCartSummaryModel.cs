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
        public string BalanceDueFormatted { set; get; }
        public string ShoppingCartTotalAmountDisplay { set; get; }
        public bool ShowDiscountsAdditionalCharges { set; get; }
        public float? TotalAmountPaid { set; get; }
        public float? TotalDiscountAmount { set; get; }
        public float? TotalInvoiceAmount { set; get; }
        public string TotalInvoiceAmountFormatted { set; get; }
        public string TotalInvoiceAmountInWords { set; get; }
        public long? TotalItemsCount { set; get; }
        public float? TotalOrderAmount { set; get; }
        public string TotalOrderAmountFormatted { set; get; }
        public float? TotalOrderAmountBeforeDiscount { set; get; }
        public long? TotalOrderQtyCount { set; get; }
        public float? TotalProductOrVolumetricWeight { set; get; }
        public long TotalProductOrVolumetricWeightRounded { set; get; }
        public WeightUnitEnum? TotalProductOrVolumetricWeightRoundedUnit { set; get; }
        public float TotalShippingAndHandlingChargesAmount { set; get; }
        public float TotalTaxAmount { set; get; }
        public float? TotalVolumeValue { set; get; }
        public float? TotalWeightCalc { set; get; }
        public WeightUnitEnum? TotalWeightCalcUnit { set; get; }
    }
}
