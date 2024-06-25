using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class PaymentInfo1Model
    {
        //Additional models --> PaymentDataModel, PersonModel, OrderSumamryModel and...
        public CouponPaymentModel CouponPaymentModel { set; get; }
        public DeliveryAddressModel DeliveryAddressModel { set; get; }
        public DeliveryDataModel DeliveryDataModel { set; get; }
        public DeliveryMethodModel DeliveryMethodModel { set; get; }
        public GiftCertPaymentModel GiftCertPaymentModel { set; get; }
        public PaymentModeModel PaymentModeModel { set; get; }
        public ShoppingCartModel ShoppingCartModel { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
