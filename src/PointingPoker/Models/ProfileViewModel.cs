using PointingPoker.DataAccess.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace PointingPoker.Models
{
    public class ProfileViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Username { get; set; }

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
            Username = user.Username;
            Email = user.Email;
        }
    }
}
