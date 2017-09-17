using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace CCM.Data.Models
{
    public class CCMUser : IdentityUser
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public bool PublicProfile { get; set; }
        public String Bio { get; set; }
        public byte[] Avatar { get; set; }
        public String AvatarContentType { get; set; }
        public String AvatarFileName { get; set; }
        public bool OkToContact { get; set; }
        public String LinkedinUrl { get; set; }
        public String TwitterUrl { get; set; }

        public virtual ICollection<AuditLog> AuditLogs { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
