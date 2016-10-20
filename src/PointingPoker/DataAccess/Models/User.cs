using System;
using System.ComponentModel.DataAnnotations;

namespace PointingPoker.DataAccess.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
