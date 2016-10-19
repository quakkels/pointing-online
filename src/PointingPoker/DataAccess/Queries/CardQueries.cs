using Dapper;
using PointingPoker.DataAccess.Models;
using System;
using System.Collections.Generic;
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

        public IEnumerable<Card> GetCardsToPointByUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Card> GetCardsToPointForTeam(Guid userId, Guid teamId)
        {
            using (var conn = _connectionProvider.GetOpenPointingPokerConnection())
            {
                var cards = conn.Query<Card>(
                    @"select [Id], [Description], [CreatedBy], [IsPointingClosed], [TeamId], [DateCreated]
                    from Cards
                    where 
	                    not exists (select 1 from Points where Points.PointedBy = @userId and Points.CardId = Cards.Id)
	                    and Cards.TeamId = @teamId
                    order by DateCreated",
                    new { userId, teamId });

                return cards;
            }
        }

        public IEnumerable<Card> GetOpenCardsForTeam(Guid teamId)
        {
            using (var conn = _connectionProvider.GetOpenPointingPokerConnection())
            {
                var cards = conn.Query<Card>(
                    @"select [Id], [Description], [CreatedBy], [IsPointingClosed], [TeamId], [DateCreated]
                    from Cards
                    where
	                    IsPointingClosed = 0
	                    and Cards.TeamId = @teamId
                    order by DateCreated",
                    new { teamId });

                return cards;
            }
        }
    }
}
