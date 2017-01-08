using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PointingPoker.Models
{
    public class InviteTeamMembersViewModel
    {
        [Required]
        public int TeamId { get; set; }

        [Required(ErrorMessage = "To invite site users to the team, give us their account's emails")]
        public string InvitedEmails { get; set; }

        public IList<string> GetParsedInvitedEmails => string.IsNullOrEmpty(InvitedEmails) ? new string[0] : InvitedEmails?.Split(' ');
    }
}
