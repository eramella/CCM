using CCM.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCM.Data.Models
{
    public class Session
    {
        public Session()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public String Title { get; set; }
        public SessionLevel Level { get; set; }
        public String Description { get; set; }
        public int CampId { get; set; }
        public String UserId { get; set; }


        public virtual Camp Camp { get; set; }
        public virtual List<TagSession> TagSessions { get; set; }
        public virtual CCMUser User { get; set; }
    }
}
