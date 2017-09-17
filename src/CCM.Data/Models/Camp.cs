using CCM.Data.Enums;
using System;
using System.Collections.Generic;

namespace CCM.Data.Models
{
    public class Camp
    {
        public int Id { get; set; }

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public String LocationName { get; set; }
        public String LocationAddress { get; set; }
        public String LocationCity { get; set; }
        public String LocationZip { get; set; }
        public String LocationState { get; set; }
        public String LocationInfo { get; set; }

        public CampState State { get; set; } 

        public virtual ICollection<Session> Sessions { get; set; }
        public virtual ICollection<Sponsor> Sponsors { get; set; }
    }
}
