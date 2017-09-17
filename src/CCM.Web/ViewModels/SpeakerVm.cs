using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace CCM.ViewModels
{
    public class SpeakerVm
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public bool OkToContact { get; set; }
        public String Bio { get; set; }     
    }
}
