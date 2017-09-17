using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCM.Data.Models
{
    public class TagSession
    {
        public String TagId { get; set; }
        public String SessionId { get; set; }

        public virtual Tag Tag { get; set; }
        public virtual Session Session { get; set; }
    }
}
