using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SVCCTempleModels
{
    public class LoginUserModel
    {
        public long LoginUserId { set; get; }
        public string LoginNameId1 { set; get; }
        public string LoginPassword { set; get; }
        public string PasswordExpiry { set; get; }
        public string ResetPasswordCompletedDateTime { set; get; }
        public string AspNetUserId { set; get; }
        public string ResetPasswordExpiryDateTime { set; get; }
        public string ResetPasswordQueryString { set; get; }
        public string ResetPasswordKey { set; get; }
        public string UpdUserId { set; get; }
    }
}
