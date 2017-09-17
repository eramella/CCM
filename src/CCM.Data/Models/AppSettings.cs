using System;
using System.Collections.Generic;
using System.Text;

namespace CCM.Data.Models
{
    public class AppSettings
    {        
        public bool Id { get; set; }
        public String CampName { get; set; }
        public String TagLine { get; set; }
        public int? NextCampId { get; set; }
        
        public byte[] Pic1 { get; set; }
        public String Pic1ContentType { get; set; }
        public String Pic1FileName { get; set; }

        public byte[] Pic2 { get; set; }
        public String Pic2ContentType { get; set; }
        public String Pic2FileName { get; set; }

        public byte[] Pic3 { get; set; }
        public String Pic3ContentType { get; set; }
        public String Pic3FileName { get; set; }

        public byte[] Pic4 { get; set; }
        public String Pic4ContentType { get; set; }
        public String Pic4FileName { get; set; }

        public byte[] Pic5 { get; set; }
        public String Pic5ContentType { get; set; }
        public String Pic5FileName { get; set; }
    }
}
