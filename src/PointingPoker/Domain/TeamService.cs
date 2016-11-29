using System;
using PointingPoker.DataAccess.Models;
using PointingPoker.DataAccess.Commands;
using System.Collections.Generic;
using System.Linq;
using PointingPoker.DataAccess.Queries;

namespace PointingPoker.Domain
{
    public class TeamService : ITeamService
    {
        private ITeamCommands _teamCommands;
        private ITeamQueries _teamQueries;

        public TeamService(ITeamCommands teamCommands, ITeamQueries teamQueries)
        {
            _teamCommands = teamCommands;
            _teamQueries = teamQueries;
        }

        public bool CreateTeam(Team team, IEnumerable<string> memberEmails)
        {
            if (
                string.IsNullOrEmpty(team.Name)
                || team.CreatedBy == Guid.Empty)
            {
                return false;
            }

            memberEmails = memberEmails?.Distinct();

            if (memberEmails == null)
            {
                memberEmails = new List<string>();
            }

            var teamId = _teamCommands.CreateTeam(
                team, 
                memberEmails);

            team.Id = teamId;

            return true;
        }

        public IEnumerable<Team> GetTeamsByUser(Guid memberUserId)
        {
            var teams = _teamQueries.GetTeamsByUser(memberUserId);
            return teams;
        }

        public Team GetTeam(int teamId)
        {
            return _teamQueries.GetTeam(teamId);
        }
    }
}
