using Microsoft.AspNetCore.Http;
using System;

namespace CCM.ViewModels
{
    public class AppSettingsVm
    {
        public String CampName { get; set; }
        public String TagLine { get; set; }
        public int? NextCamp { get; set; }

        public bool? Image1Deleted { get; set; }
        public bool? Image2Deleted { get; set; }
        public bool? Image3Deleted { get; set; }
        public bool? Image4Deleted { get; set; }
        public bool? Image5Deleted { get; set; }

        public String Image1Url { get; set; }
        public String Image2Url { get; set; }
        public String Image3Url { get; set; }
        public String Image4Url { get; set; }
        public String Image5Url { get; set; }

        public IFormFile pic1Upload { get; set; }
        public IFormFile pic2Upload { get; set; }
        public IFormFile pic3Upload { get; set; }
        public IFormFile pic4Upload { get; set; }
        public IFormFile pic5Upload { get; set; }
    }
}
