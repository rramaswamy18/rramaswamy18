using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetailSlnEnumerations
{
    public enum DeliveryModeEnum : int
    {
        [Display(Name = "Surface")]
        Surface = 100,
        [Display(Name = "Air")]
        Air = 200,
    }
}
