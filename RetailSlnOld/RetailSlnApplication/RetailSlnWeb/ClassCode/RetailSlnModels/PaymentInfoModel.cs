using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class PaymentInfoModel
    {
        public DeliveryInfoDataModel DeliveryInfoDataModel { set; get; }
        public GiftCertPaymentModel GiftCertPaymentModel { set; get; }
        public PaymentSummaryDataModel PaymentSummaryDataModel { set; get; }
        public ShoppingCartModel ShoppingCartModel { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
