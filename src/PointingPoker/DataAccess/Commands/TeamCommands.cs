using Dapper;
using PointingPoker.DataAccess.Models;

namespace PointingPoker.DataAccess.Commands
{
    public class TeamCommands : ITeamCommands
    {
        private readonly IDbConnectionProvider _connectionProvider;

        public TeamCommands(IDbConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public void CreateTeam(Team team)
        {
            var command = @"
            insert into Teams values (@id, @Name, @CreatedBy)";

            using (var conn = _connectionProvider.GetOpenPointingPokerConnection())
            {
                conn.Execute(command, team);
            }
        }
    }
}
