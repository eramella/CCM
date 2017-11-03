using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCM.ViewModels
{
    public class TeamMemberVm
    {
        public String Id { get; set; }
        public String Username { get; set; }

        public String FirstName { get; set; }
        public String LastName { get; set; }
        public bool isTeamMember { get; set; }
    }
}
