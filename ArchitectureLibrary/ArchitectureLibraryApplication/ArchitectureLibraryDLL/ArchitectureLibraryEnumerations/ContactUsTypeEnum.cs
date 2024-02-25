using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryEnumerations
{
    public enum ContactUsTypeEnum : int
    {
        [Description("")]
        _ = 0,
        [Description("Comments")]
        Request = 100,
        [Description("Question")]
        Question = 200,
        [Description("Request new item")]
        RequestNewItem = 300,
        [Description("Request to be wholesale dealer")]
        RequestWholesaleDealer = 400,
    }
}
