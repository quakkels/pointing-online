using Dapper;
using System;
using System.Linq;

namespace PointingPoker.DataAccess.Queries
{
    public class CardQueries : ICardQueries
    {
        private readonly IDbConnectionProvider _connectionProvider;

        public CardQueries(IDbConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }
            
        public bool DoesCardCreatorExist(Guid creatorId)
        {
            using (var conn = _connectionProvider.GetOpenPointingPokerConnection())
            {
                var query = @"
                select 
	                case when
                        exists(
                            select 1
                            from Users
                            where Id = @creatorId)
                    then cast(1 as bit)
	                else cast(0 as bit)
                    end as [exists]";

                var exists = conn.Query<bool>(query, new { creatorId });
                return exists.FirstOrDefault();

            }
        }
    }
}
