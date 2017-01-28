using Dapper;
using PointingPoker.DataAccess.Models;
using System.Collections.Generic;

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

        public IEnumerable<CardScore> GetCardPoints(int cardId)
        {
            using (var conn = _connectionProvider.GetOpenPointingPokerConnection())
            {
                // RANK():
                // https://msdn.microsoft.com/en-us/library/ms176102.aspx
                var result = conn.Query<CardScore>(
                    @"SELECT Id as CardId, PointedBy, Points FROM (
                    SELECT 
	                    Cards.Id, 
	                    Points.PointedBy, 
	                    Points.Points, 
	                    RANK() OVER (
		                    PARTITION BY 
			                    Cards.Id, 
			                    Points.PointedBy 
			                    ORDER BY Points.DateCreated ASC
	                    ) AS R FROM Points
                    INNER JOIN Cards on Points.CardId = Cards.Id
                    ) a 
                    WHERE 
	                    R = 1
	                    and Id = @cardId",
                    new { cardId });

                return result;
            }
        }
    }
}
