using System;
using System.Collections.Generic;
using System.Text;

namespace CCM.Data.DynamicQuery
{
    public class DynamicRequest
    {
        public String Token { get; set; }
        public int Skip { get; set; }
        public int PageSize { get; set; }
    }
}
