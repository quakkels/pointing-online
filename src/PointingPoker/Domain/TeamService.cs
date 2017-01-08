using PointingPoker.DataAccess.Models;
using PointingPoker.DataAccess.Commands;
using System.Collections.Generic;
using System.Linq;
using PointingPoker.DataAccess.Queries;
using System;

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
                || team.CreatedBy == 0)
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

        public IEnumerable<Team> GetTeamsByUser(int memberUserId)
        {
            var teams = _teamQueries.GetTeamsByUser(memberUserId);
            return teams;
        }

        public bool IsUserInTeam(int userId, int teamid)
        {
            var teams = _teamQueries.GetTeamsByUser(userId);
            if (teams == null)
            {
                return false;
            }

            var isMember = teams.Any(x => x.Id == teamid);
            return isMember;
        }

        public Team GetTeam(int teamId)
        {
            return _teamQueries.GetTeam(teamId);
        }

        public void AddMembersByEmail(int addedByUserId, int teamId, IEnumerable<string> invitedEmails)
        {
            throw new NotImplementedException();
        }
    }
}
