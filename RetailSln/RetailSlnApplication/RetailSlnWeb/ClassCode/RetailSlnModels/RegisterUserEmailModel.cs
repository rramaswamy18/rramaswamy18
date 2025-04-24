using ArchitectureLibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RetailSlnModels
{
    public class RegisterUserEmailModel
    {
        public RegisterUserModel RegisterUserModel { set; get; }
        public CouponListModel CouponListModel { set; get; }
        public PriestListModel PriestListModel { set; get; }
    }
}
