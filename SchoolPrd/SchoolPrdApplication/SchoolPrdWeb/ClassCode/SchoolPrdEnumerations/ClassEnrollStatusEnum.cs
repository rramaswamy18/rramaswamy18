using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SchoolPrdEnumerations
{
    public enum ClassEnrollStatusEnum : int
    {
        [Description("Submitted")]
        Submitted = 100,
        [Description("Approved")]
        Approved = 200,
        [Description("Completed")]
        Completed = 300,
        [Description("Not Completed")]
        NotCompleted = 400,
        [Description("DMV Test Passed")]
        DMVPass = 500,
        [Description("DMV Test Failed")]
        DMVFail = 600,
        [Description("Canceled")]
        Canceled = 700,
    }
}
