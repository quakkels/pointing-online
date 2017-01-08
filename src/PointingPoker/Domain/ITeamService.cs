using PointingPoker.DataAccess.Models;
using System.Collections.Generic;

namespace PointingPoker.Domain
{
    public interface ITeamService
    {
        bool CreateTeam(Team team, IEnumerable<string> memberEmails);
        IEnumerable<Team> GetTeamsByUser(int memberUserId);
        bool IsUserInTeam(int userId, int teamid);
        Team GetTeam(int teamId);
    }
}
