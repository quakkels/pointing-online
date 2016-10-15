using System;
using PointingPoker.DataAccess.Models;
using PointingPoker.DataAccess.Commands;

namespace PointingPoker.Domain
{
    public class TeamService : ITeamService
    {
        private ITeamCommands _teamCommands;

        public TeamService(ITeamCommands teamCommands)
        {
            _teamCommands = teamCommands;
        }

        public bool CreateTeam(Team team)
        {
            if (team.Id == Guid.Empty
                || string.IsNullOrEmpty(team.Name)
                || team.CreatedBy == Guid.Empty)
            {
                return false;
            }

            _teamCommands.CreateTeam(team);
            return true;
        }
    }
}
