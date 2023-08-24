using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArchitectureLibraryModels
{
    public class RegisterUserLoginUserModel
    {
        public LoginUserProfModel LoginUserProfModel { set; get; }
        public string QueryString { set; get; }
        public RegisterUserProfModel RegisterUserProfModel { set; get; }
        public ResetPasswordModel ResetPasswordModel { set; get; }
        public ContactUsModel ContactUsModel { set; get; }
    }
}
