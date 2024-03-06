using ArchitectureLibraryCreditCardModels;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryCreditCardBusinessLayer
{
    public class CreditCardRazorPayBL
    {
        public RazorPayResponse ProcessCreditCard(RazorPayRequest razorPayRequest)
        {
            try
            {
                var amount = (float.Parse(razorPayRequest.Amount) * 100).ToString();
                var options = new Dictionary<string, object>
                {
                    { "amount", amount },
                    { "currency", razorPayRequest.Currency },
                    { "receipt", razorPayRequest.Receipt },
                    { "payment_capture", razorPayRequest.PaymentCapture }
                };
                RazorpayClient razorpayClient = new RazorpayClient(razorPayRequest.ApiKey, razorPayRequest.ApiSecret);
                var order = razorpayClient.Order.Create(options);
                RazorPayResponse razorPayResponse = new RazorPayResponse
                {
                    OrderId = order["id"],
                    RazorpayKey = razorPayRequest.ApiKey,
                    Amount = amount,
                    Currency = razorPayRequest.Currency,
                    Name = razorPayRequest.Name,
                    Email = razorPayRequest.Email,
                    PhoneNumber = razorPayRequest.PhoneNumber,
                    Address = razorPayRequest.Address,
                    Description = "Order by Merchant"
                };
                return razorPayResponse;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
        public bool CheckPaymentSuccess(string razorpay_payment_id, string razorpay_order_id, string razorpay_signature)
        {
            try
            {
                Dictionary<string, string> attributes = new Dictionary<string, string>
                {
                    { "razorpay_payment_id", razorpay_payment_id },
                    { "razorpay_order_id", razorpay_order_id },
                    { "razorpay_signature", razorpay_signature }
                };
                Utils.verifyPaymentSignature(attributes);
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return false;
            }
        }
    }
}
