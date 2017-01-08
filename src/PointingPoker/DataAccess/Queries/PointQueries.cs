using Dapper;
using System;

namespace PointingPoker.DataAccess.Queries
{
    public class PointQueries : IPointQueries
    {
        private readonly IDbConnectionProvider _connectionProvider;

        public PointQueries(IDbConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public int GetCardPoints(int cardId, int userId)
        {
            using(var conn = _connectionProvider.GetOpenPointingPokerConnection())
            {
                var points = conn.QueryFirstOrDefault<int>(
                    @"select top 1 Points from Points where CardId = @cardId and PointedBy = @userId
                    order by DateCreated desc",
                    new { cardId, userId });

                return points;
            }
        }
    }
}
