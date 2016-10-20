using PointingPoker.DataAccess.Models;
using System;
using System.Collections.Generic;

namespace PointingPoker.Domain
{
    public interface ITeamService
    {
        bool CreateTeam(Team team, IEnumerable<string> memberEmails);
        IEnumerable<Team> GetTeamsByUser(Guid memberUserId);
        Team GetTeam(Guid teamId);
    }
}
