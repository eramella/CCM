using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CCM.ViewModels
{
    public class CampVm
    {
        public int Id { get; set; }

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        [MaxLength(128)]
        public string LocationName { get; set; }
        [MaxLength(256)]
        public string LocationAddress { get; set; }
        [MaxLength(64)]
        public string LocationCity { get; set; }
        [MaxLength(5)]
        [DataType(DataType.PostalCode)]
        public string LocationZip { get; set; }
        [MaxLength(2)]
        public string LocationState { get; set; }

        public string LocationInfo { get; set; }

        public String State { get; set; }
    }
}