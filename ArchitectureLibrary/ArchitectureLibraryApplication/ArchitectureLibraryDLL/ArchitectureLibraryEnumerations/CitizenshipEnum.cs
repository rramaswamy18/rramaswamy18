using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureLibraryEnumerations
{
    public enum CitizenshipEnum : int
    {
        [Description("U.S. Citizen")]
        USCitizen = 100,
        [Description("Permanent Resident Alien")]
        PermanentResidentAlien = 200,
        [Description("Non-Permanent Resident Alien")]
        NonPermanentResidentAlien = 300,
    }
}
