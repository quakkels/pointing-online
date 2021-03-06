﻿using System;
using System.ComponentModel.DataAnnotations;

namespace PointingPoker.Models
{
    public class RegisterViewModel
    {

        public int Id { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [EmailAddress, Required] 
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Compare("Password")]
        [Display(Name = "Verify Password")]
        public string VerifyPassword { get; set; }
    }
}
