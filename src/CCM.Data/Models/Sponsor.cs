using System;
using System.Collections.Generic;
using System.Text;

namespace CCM.Data.Models
{
    public class Sponsor
    {
        public Sponsor()
        {
            Id = Guid.NewGuid().ToString();
        }

        public String Id { get; set; }
        public String Name { get; set; }
        public byte[] Logo { get; set; }
        public String LogoContentType { get; set; }
        public String LogoFileName { get; set; }
        public String CompanyUrl { get; set; }
        public int TypeId { get; set; }
        public int CampId { get; set; }

        public virtual SponsorType SponsorType { get; set; }
        public virtual Camp Camp { get; set; }
    }
}
