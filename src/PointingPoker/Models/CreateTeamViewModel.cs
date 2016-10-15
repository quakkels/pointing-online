using System;
using System.ComponentModel.DataAnnotations;

namespace PointingPoker.Models
{
    public class CreateTeamViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string MemberEmails { get; set; }
    }
}
