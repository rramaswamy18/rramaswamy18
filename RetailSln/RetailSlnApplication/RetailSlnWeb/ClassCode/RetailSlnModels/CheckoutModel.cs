using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class CheckoutModel
    {
        public LoginUserProfModel LoginUserProfModel { set; get; }
        public PaymentInfoModel PaymentInfoModel { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
