using System;
using PointingPoker.DataAccess.Models;
using PointingPoker.DataAccess.Commands;
using System.Collections.Generic;
using System.Linq;

namespace PointingPoker.Domain
{
    public class TeamService : ITeamService
    {
        private ITeamCommands _teamCommands;

        public TeamService(ITeamCommands teamCommands)
        {
            _teamCommands = teamCommands;
        }

        public bool CreateTeam(Team team, IEnumerable<string> memberEmails)
        {
            if (team.Id == Guid.Empty
                || string.IsNullOrEmpty(team.Name)
                || team.CreatedBy == Guid.Empty)
            {
                return false;
            }

            memberEmails = memberEmails?.Distinct();

            if (memberEmails == null)
            {
                memberEmails = new List<string>();
            }

            _teamCommands.CreateTeam(
                team, 
                memberEmails);

            return true;
        }
    }
}
