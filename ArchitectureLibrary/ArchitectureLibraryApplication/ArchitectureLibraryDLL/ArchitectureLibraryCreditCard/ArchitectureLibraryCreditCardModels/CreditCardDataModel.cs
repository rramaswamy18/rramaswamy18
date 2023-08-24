using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryCreditCardModels
{
    public class CreditCardDataModel
    {
        public long CreditCardDataId { set; get; }
        public string CreditCardAmount { set; get; }
        public string CreditCardExpMM { set; get; }
        public string CreditCardExpYear { set; get; }
        public Dictionary<string, string> CreditCardKVPs { set; get; }
        public string CreditCardNumber { set; get; }
        public string CreditCardNumberLast4 { set; get; }
        public string CreditCardProcessor { set; get; }
        public string CreditCardSecCode { set; get; }
        public string CreditCardTranType { set; get; }
        public string CurrencyCode { set; get; }
        public string NameAsOnCard { set; get; }
        public string ProcessMessage { set; get; }
        public string RequestData { set; get; }
        public string ResponseData { set; get; }
        public string StatusNameDesc { set; get; }
    }
}
