using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CCM.Data.Enums
{
    public enum SessionLevel
    {
        [Description("100")]
        L100 = 1,

        [Description("200")]
        L200,

        [Description("300")]
        L300,

        [Description("400")]
        L400,

        [Description("500")]
        L500
    }
}
