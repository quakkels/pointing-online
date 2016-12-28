using PointingPoker.DataAccess.Models;
using System.Collections.Generic;

namespace PointingPoker.DataAccess.Commands
{
    public interface ITeamCommands
    {
        int CreateTeam(Team team, IEnumerable<string> memberEmails);
    }
}
