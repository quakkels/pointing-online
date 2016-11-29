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

        public int CreatePoint(Point point)
        {
            using(var conn = _connectionProvider.GetOpenPointingPokerConnection())
            {
                var id = conn.ExecuteScalar<int>(
                    @"insert into Points (PointedBy, CardId, Points) values (@PointedBy, @CardId, @Points)
                      select scope_identity()",
                    point);

                return id;
            }
        }
    }
}
