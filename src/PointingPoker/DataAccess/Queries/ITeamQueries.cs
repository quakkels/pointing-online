using PointingPoker.DataAccess.Models;
using System.Collections.Generic;

namespace PointingPoker.DataAccess.Queries
{
    public interface ITeamQueries
    {
        IEnumerable<Team> GetTeamsByUser(int memberUserId);
        Team GetTeam(int teamId);
    }
}
