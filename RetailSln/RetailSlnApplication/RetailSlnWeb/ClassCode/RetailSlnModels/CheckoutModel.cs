using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class CheckoutModel
    {
        public ContactUsModel ContactUsModel { set; get; }
        public CheckoutGuestModel CheckoutGuestModel { set; get; }
        public LoginUserProfModel LoginUserProfModel { set; get; }
        public RegisterUserProfModel RegisterUserProfModel { set; get; }
        public ResetPasswordModel ResetPasswordModel { set; get; }
        public ShoppingCartModel ShoppingCartModel { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
