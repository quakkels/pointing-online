using PointingPoker.DataAccess.Models;
using System;
using System.Collections.Generic;

namespace PointingPoker.DataAccess.Queries
{
    public interface ITeamQueries
    {
        IEnumerable<Team> GetTeamsByUser(Guid memberUserId);
        Team GetTeam(Guid teamId);
    }
}
