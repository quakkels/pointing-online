using System;
using System.ComponentModel.DataAnnotations;

namespace PointingPoker.Models
{
    public class CreateTeamViewModel
    {
        [Required]
        public string Name { get; set; }

        public string MemberEmails { get; set; }
    }
}
