using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CCM.ViewModels
{
    public class RegisterVm
    {
        [Required]
        public string Username { get; set; }
        [Required]        
        public string Password { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        [Compare("Password",ErrorMessage = "Make sure Confirm Password matches Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
