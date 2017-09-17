using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCM.Data.Models
{
    public class Tag
    {
        public Tag()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public String Name { get; set; }

        public virtual List<TagSession> TagSessions { get; set; }
    }
}
