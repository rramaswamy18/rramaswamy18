using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetailSlnEnumerations
{
    public enum DeliveryTypeEnum : int
    {
        [Display(Name = "Normal")]
        Normal = 100,
        [Display(Name = "Express")]
        Express = 200,
    }
}
