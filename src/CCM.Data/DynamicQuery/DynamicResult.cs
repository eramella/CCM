using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CCM.Data.DynamicQuery
{
    public class DynamicResult<T>
    {
        public int Total { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
