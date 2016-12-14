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
            
        public bool DoesCardCreatorExist(int creatorId)
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

                var exists = conn.QueryFirst<bool>(query, new { creatorId });
                return exists;

            }
        }

        public IEnumerable<Card> GetCardsToPointByUser(int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Card> GetCardsToPointForTeam(int userId, int teamId)
        {
            using (var conn = _connectionProvider.GetOpenPointingPokerConnection())
            {
                var cards = conn.Query<Card>(
                    @"select [Id], [Description], [CreatedBy], [ClosedBy], [TeamId], [DateCreated]
                    from Cards
                    where 
	                    not exists (select 1 from Points where Points.PointedBy = @userId and Points.CardId = Cards.Id)
	                    and Cards.TeamId = @teamId
                    order by DateCreated",
                    new { userId, teamId });

                return cards;
            }
        }

        public IEnumerable<Card> GetOpenCardsForTeam(int teamId)
        {
            using (var conn = _connectionProvider.GetOpenPointingPokerConnection())
            {
                var cards = conn.Query<Card>(
                    @"select [Id], [Description], [CreatedBy], [ClosedBy], [TeamId], [DateCreated]
                    from Cards
                    where
	                    ClosedBy is null
	                    and Cards.TeamId = @teamId
                    order by DateCreated",
                    new { teamId });

                return cards;
            }
        }

        public Card GetCard(int cardId)
        {
            using (var conn = _connectionProvider.GetOpenPointingPokerConnection())
            {
                var card = conn.QueryFirstOrDefault<Card>(
                    @"select top 1 [Id], [Description], [CreatedBy], [ClosedBy], [TeamId], [DateCreated]
                    from Cards
                    where [Id] = @cardId",
                    new { cardId });

                return card;
            }
        }

        public bool IsCardClosedForPointing(int cardId)
        {
            using ( var conn = _connectionProvider.GetOpenPointingPokerConnection())
            {
                var result = conn.QueryFirstOrDefault<bool>(
                    @"select top 1 
	                    case when ClosedBy is null 
	                    then 'False'
	                    else 'True '
	                    end
                    from Cards
                    where Id = @cardId",
                    new { cardId });

                return result;
            }
        }
    }
}
