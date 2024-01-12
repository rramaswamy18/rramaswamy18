using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryCreditCardModels
{
    public class RazorPayRequest
    {
        public string Address { get; set; }
        public string Amount { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string Currency { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PaymentCapture { get; set; }
        public string PhoneNumber { get; set; }
        public string Receipt { get; set; }
    }
}
