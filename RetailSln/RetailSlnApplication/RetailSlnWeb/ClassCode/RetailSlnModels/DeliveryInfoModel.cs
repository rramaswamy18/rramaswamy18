using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class DeliveryInfoModel
    {
        public CompleteOrderModel CompleteOrderModel { set; get; }
        public CouponPaymentModel CouponPaymentModel { set; get; }
        public DemogInfoAddressModel DeliveryAddressModel { set; get; }
        public DeliveryDataModel DeliveryDataModel { set; get; }
        public DeliveryMethodModel DeliveryMethodModel { set; get; }
        public GiftCertPaymentModel GiftCertPaymentModel { set; get; }
        public OrderSummaryModel OrderSummaryModel { set; get; }
        public PaymentModeModel PaymentModeModel { set; get; }
        public ResponseObjectModel ResponseObjectModel { get; set; }
    }
}
