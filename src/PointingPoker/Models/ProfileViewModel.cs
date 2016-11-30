using PointingPoker.DataAccess.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace PointingPoker.Models
{
    public class ProfileViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }
        
        public string Password { get; set; }

        [Compare("Password")]
        public string VerifyPassword { get; set; }

        public ProfileViewModel() { }
        public ProfileViewModel(User user)
        {
            Id = user.Id;
            UserName = user.UserName;
            Email = user.Email;
        }
    }
}
