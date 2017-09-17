using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace CCM.ViewModels
{
    public class UserVm
    {
        public String Username { get; set; }

        [Display(Name = "First Name")]
        public String FirstName { get; set; }
        [Display(Name = "Last Name")]
        public String LastName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public String Email { get; set; }
        [Phone]
        [Display(Name ="Mobile")]
        [RegularExpression(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}", ErrorMessage = "Invalid Phone Number!")]
        public String MobilePhone { get; set; }
        public bool PublicProfile { get; set; }
        [Display(Name ="Enable Emailing")]
        public bool OkToContact { get; set; }
        public String Bio { get; set; }
        public IFormFile UploadedImage { get; set; }
        public byte[] Avatar { get; set; }
        public String AvatarContentType { get; set; }
        public String AvatarFileName { get; set; }
        [Url]
        [Display(Name = "LinekdIn Url")]
        public String LinkedinUrl { get; set; }
        [Url]
        [Display(Name ="Twitter Url")]
        public String TwitterUrl { get; set; }
    }
}
