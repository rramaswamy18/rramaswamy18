using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetailSlnEnumerations
{
    public enum DeliveryMethodEnum : int
    {
        [Display(Name = "Ship to delivery address")]
        ShipToDeliveryAddress = 100,
        [Display(Name = "Pickup from store")]
        PickupFromStore = 200,
    }
}
