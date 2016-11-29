using Dapper;
using System;
using System.Collections.Generic;
using PointingPoker.DataAccess.Models;
using System.Linq;

namespace PointingPoker.DataAccess.Queries
{
    public class TeamQueries : ITeamQueries
    {
        private IDbConnectionProvider _connectionProvider;

        public TeamQueries(IDbConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public IEnumerable<Team> GetTeamsByUser(Guid memberUserId)
        {
            using (var conn = _connectionProvider.GetOpenPointingPokerConnection())
            {
                var teams = conn.Query<Team>(
                    @"select [Id], [Name], [CreatedBy]
                    from Teams t
                    inner join TeamMembers tm on t.Id = tm.TeamId
                    where tm.UserId = @userId", 
                    new { userId = memberUserId });
                return teams;
            }
        }

        public Team GetTeam(int teamId)
        {
            using ( var conn = _connectionProvider.GetOpenPointingPokerConnection())
            {
                var teams = conn.Query<Team>(
                    @"select top 1 [Id], [Name], [CreatedBy]
                    from Teams
                    where Id = @teamId",
                    new { teamId });

                return teams?.FirstOrDefault();
            }
        }
    }
}
