using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryModels
{
    public class RegisterLoginContactResetPasswordModel
    {
        public ContactUsModel ContactUsModel { set; get; }
        public LoginUserProfModel LoginUserProfModel { set; get; }
        public RegisterUserProfModel RegisterUserProfModel { set; get; }
        public ResetPasswordModel ResetPasswordModel { set; get; }
        public string QueryString { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
