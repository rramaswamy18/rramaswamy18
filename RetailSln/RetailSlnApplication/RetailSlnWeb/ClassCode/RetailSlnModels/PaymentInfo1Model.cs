using ArchitectureLibraryCreditCardModels;
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
        public CompleteOrderModel CompleteOrderModel { set; get; }
        public CouponPaymentModel CouponPaymentModel { set; get; }
        public CreditCardDataModel CreditCardDataModel { set; get; }
        public CreditCardProcessModel CreditCardProcessModel { set; get; }
        public DemogInfoAddressModel DeliveryAddressModel { set; get; }
        public DeliveryDataModel DeliveryDataModel { set; get; }
        public DeliveryMethodModel DeliveryMethodModel { set; get; }
        public GiftCertPaymentModel GiftCertPaymentModel { set; get; }
        public OrderApprovalModel OrderApprovalModel { set; get; }
        public OrderHeaderWIPModel OrderHeaderWIPModel { set; get; }
        public OrderSummaryModel OrderSummaryModel { set; get; }
        public PaymentModeModel PaymentModeModel { set; get; }
        public PaymentData1Model PaymentDataModel { set; get; }
        public ShoppingCartModel ShoppingCartModel { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
