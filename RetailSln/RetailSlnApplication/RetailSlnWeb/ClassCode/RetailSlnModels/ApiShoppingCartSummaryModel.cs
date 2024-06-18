using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ApiShoppingCartSummaryModel
    {
        public float? AmountPaidByCreditCard { set; get; }
        public float? AmountPaidByGiftCert { set; get; }
        public float? BalanceDue { set; get; }
        public CorpAcctModel CorpAcctModel { set; get; }
        public string EmailAddress { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public long? PersonId { set; get; }
        public string TelephoneCode { set; get; }
        public string TelephoneCountryId { set; get; }
        public string TelephoneNumber { set; get; }
        public float? TotalAmountPaid { set; get; }
        public float? TotalDiscountAmount { set; get; }
        public float? TotalInvoiceAmount { set; get; }
        public long? TotalItemsCount { set; get; }
        public float? TotalOrderAmount { set; get; }
        public float? TotalOrderAmountBeforeDiscount { set; get; }
        public float? TotalProductOrVolumetricWeight { set; get; }
        public long TotalProductOrVolumetricWeightRounded { set; get; }
        public float? TotalVolumeValue { set; get; }
        public float? TotalWeightCalc { set; get; }
    }
}
