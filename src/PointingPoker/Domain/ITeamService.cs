using PointingPoker.DataAccess.Models;
using System.Collections.Generic;

namespace PointingPoker.Domain
{
    public interface ITeamService
    {
        bool CreateTeam(Team team, IEnumerable<string> memberEmails);
    }
}
