using ArchitectureLibraryCreditCardModels;
using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class PaymentInfoModel
    {
        public CreditCardDataModel CreditCardDataModel { set; get; }
        public DeliveryInfoModel DeliveryInfoModel { set; get; }
        //public OrderHeaderWIPModel OrderHeaderWIPModel { set; get; }
        public PaymentDataModel PaymentDataModel { set; get; }
        //public bool HideShoppingCart { set; get; }
        //public OrderHeaderWIPModel OrderHeaderWIPModel { set; get; }
        //public OrderSummaryModel OrderSummaryModel { set; get; }
        //public PaymentDataModel PaymentDataModel { set; get; }
        //public CompleteOrderModel CompleteOrderModel { set; get; }
        //public CouponPaymentModel CouponPaymentModel { set; get; }
        //public DemogInfoAddressModel DeliveryAddressModel { set; get; }
        //public DeliveryDataModel DeliveryDataModel { set; get; }
        //public DeliveryMethodModel DeliveryMethodModel { set; get; }
        //public GiftCertPaymentModel GiftCertPaymentModel { set; get; }
        //public OrderApprovalModel OrderApprovalModel { set; get; }
        //public OrderHeaderWIPModel OrderHeaderWIPModel { set; get; }
        //public OrderSummaryModel OrderSummaryModel { set; get; }
        //public PaymentDataModel PaymentDataModel { set; get; }
        //public PaymentModeModel PaymentModeModel { set; get; }
        public ShoppingCartModel ShoppingCartModel { set; get; }
        public ResponseObjectModel ResponseObjectModel { get; set; }
    }
}
