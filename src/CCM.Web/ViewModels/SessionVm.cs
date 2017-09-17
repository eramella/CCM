using CCM.Business.Utilities;
using CCM.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CCM.ViewModels
{
    public class SessionVm
    {
        public string Id { get; set; }
        [Required]
        public String Title { get; set; }
        public int Level { get; set; }
        public String Description { get; set; }
        [Required]
        public int CampId { get; set; }
        public IEnumerable<String> Tags { get; set; }
        public String UserId { get; set; }
        public String UserFirstName { get; set; }
        public String UserLastName { get; set; }

        public String LevelName
        {
            get
            {
                try
                {
                    return EnumUtilities.GetEnumDescription<SessionLevel>(Level);
                }
                catch (Exception x)
                {
                    return "";
                }

            }
        }
    }
}
