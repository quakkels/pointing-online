using Dapper;
using PointingPoker.DataAccess.Models;
using System.Collections.Generic;
using System.Linq;

namespace PointingPoker.DataAccess.Commands
{
    public class TeamCommands : ITeamCommands
    {
        private readonly IDbConnectionProvider _connectionProvider;

        public TeamCommands(IDbConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public void CreateTeam(Team team, IEnumerable<string> memberEmails)
        {
            var teamCommand = @"
                insert into Teams values (@id, @Name, @CreatedBy)
                insert into TeamMembers values (@id, @CreatedBy)";

            var teamMemberCommand = @"
                insert into TeamMembers(TeamId, UserId)
                select @TeamId,	Id from Users 
                where email = @email and not exists (
	                select 1 from TeamMembers tm where tm.UserId = Id)";

            var teamMembers = memberEmails.Select(
                email => new
                {
                    TeamId = team.Id,
                    UserId = team.CreatedBy,
                    Email = email
                });

            using (var conn = _connectionProvider.GetOpenPointingPokerConnection())
            {
                conn.Execute(teamCommand, team);
                conn.Execute(teamMemberCommand, teamMembers);
            }
        }
    }
}
