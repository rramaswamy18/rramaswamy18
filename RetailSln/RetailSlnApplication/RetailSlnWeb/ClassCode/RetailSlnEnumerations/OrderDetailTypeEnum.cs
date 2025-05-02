using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace RetailSlnEnumerations
{
    public enum OrderDetailTypeEnum :int
    {
        [Description("Item")]
        DoNotShow = 0,
        [Description("Item")]
        Item = 100,
        [Description("Total Order Amount")]
        TotalOrderAmount = 200,
        [Description("Discount Amount")]
        DiscountAmount = 300,
        [Description("Total Order Amount after Discount")]
        TotalOrderAmountAfterDiscount = 400,
        [Description("Sales Tax Amount")]
        SalesTaxAmount = 500,
        [Description("Shipping & Handling Charges")]
        ShippingHandlingCharges = 600,
        [Description("Additional Charges")]
        AdditionalCharges = 700,
        [Description("Discount")]
        Discount = 800,
        [Description("Total Invoice Amount")]
        TotalInvoiceAmount = 900,
        [Description("Advance Amount Paid")]
        AdvanceAmountPaid = 1000,
        [Description("Amount Paid by Gift Cert")]
        AmountPaidByGiftCert = 1100,
        [Description("Amount Paid by Coupon")]
        AmountPaidByCoupon = 1200,
        [Description("Amount Paid")]
        AmountPaidByCreditCard = 1300,
        [Description("Total Amount Paid")]
        TotalAmountPaid = 1400,
        [Description("Balance Due")]
        BalanceDue = 1500,
        [Description("Other")]
        Other = 9900,
    }
}
