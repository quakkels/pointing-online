using System;
using System.ComponentModel.DataAnnotations;

namespace PointingPoker.Models
{
    public class RegisterViewModel
    {

        public Guid Id { get; set; }

        [Required]
        public string Username { get; set; }

        [EmailAddress, Required] 
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Compare("Password")]
        public string VerifyPassword { get; set; }

        public RegisterViewModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
