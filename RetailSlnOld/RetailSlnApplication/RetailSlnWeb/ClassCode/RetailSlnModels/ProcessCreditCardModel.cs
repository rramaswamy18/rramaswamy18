using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class ProcessCreditCardModel
    {
        public CreditCardProcessModel CreditCardProcessModel { set; get; }
        public GiftCertPaymentModel GiftCertPaymentModel { set; get; }
        public PaymentSummaryDataModel PaymentSummaryDataModel { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
