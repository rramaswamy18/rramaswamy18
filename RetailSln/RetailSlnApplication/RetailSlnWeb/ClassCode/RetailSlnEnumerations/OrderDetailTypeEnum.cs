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
        [Description("Sales Tax Amount")]
        SalesTaxAmount = 300,
        [Description("Shipping & Handling Charges")]
        ShippingHandlingCharges = 400,
        [Description("Additional Charges")]
        AdditionalCharges = 500,
        [Description("Discount")]
        Discount = 600,
        [Description("Total Invoice Amount")]
        TotalInvoiceAmount = 700,
        [Description("Amount Paid by Gift Cert ")]
        AmountPaidByGiftCert = 800,
        [Description("Amount Paid by Credit Card")]
        AmountPaidByCreditCard = 900,
        [Description("Total Amount Paid")]
        TotalAmountPaid = 1000,
        [Description("Balance Due")]
        BalanceDue = 1100,
        [Description("Other")]
        Other = 9900,
    }
}
