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

        public int CreateTeam(Team team, IEnumerable<string> memberEmails)
        {

            using (var conn = _connectionProvider.GetOpenPointingPokerConnection())
            {
                var teamId = conn.ExecuteScalar<int>(
                    @"
                        insert into Teams values (@Name, @CreatedBy)
                        declare @teamId int = scope_identity();
                        insert into TeamMembers values (@teamId, @CreatedBy)
                        select @teamId", 
                    team);

                var teamMembers = memberEmails
                    .Select(email => new
                    {
                        TeamId = teamId,
                        UserId = team.CreatedBy,
                        Email = email
                    });

                conn.Execute(
                    @"
                        insert into TeamMembers(TeamId, UserId)
                        select @TeamId,	Id from Users 
                        where email = @email and not exists (
	                    select 1 from TeamMembers tm where tm.UserId = Id)", 
                    teamMembers);

                return teamId;
            }
        }
    }
}
