using Dapper;
using PointingPoker.DataAccess.Models;

namespace PointingPoker.DataAccess.Commands
{
    public class PointCommands : IPointCommands
    {
        private readonly IDbConnectionProvider _connectionProvider;

        public PointCommands(IDbConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public void CreatePoint(Point point)
        {
            using(var conn = _connectionProvider.GetOpenPointingPokerConnection())
            {
                conn.Execute(
                    @"insert into Points (Id, PointedBy, CardId, Points) set (@Id, @PointedBy, @CardId, @Points)",
                    point);
            }
        }
    }
}
