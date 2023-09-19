using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryModels
{
    public class ResetPasswordContactUsModel
    {
        public ContactUsModel ContactUsModel { set; get; }
        public ResetPasswordModel ResetPasswordModel { set; get; }
        public string QueryString { set; get; }
        public ResponseObjectModel ResponseObjectModel { set; get; }
    }
}
