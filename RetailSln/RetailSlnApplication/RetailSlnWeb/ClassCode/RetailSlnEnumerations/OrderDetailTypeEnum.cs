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
        Discount = 600,
        [Description("Total Invoice Amount")]
        TotalInvoiceAmount = 800,
        [Description("Amount Paid by Gift Cert ")]
        AmountPaidByGiftCert = 900,
        [Description("Amount Paid by Credit Card")]
        AmountPaidByCreditCard = 1000,
        [Description("Total Amount Paid")]
        TotalAmountPaid = 1100,
        [Description("Balance Due")]
        BalanceDue = 1200,
        [Description("Other")]
        Other = 9900,
    }
}
