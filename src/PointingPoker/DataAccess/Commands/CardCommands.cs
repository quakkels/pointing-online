using System;
using Dapper;
using PointingPoker.DataAccess.Models;

namespace PointingPoker.DataAccess.Commands
{
    public class CardCommands : ICardCommands
    {
        private readonly IDbConnectionProvider _connectionProvider;

        public CardCommands(IDbConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public int CreateCard(Card card)
        {
            using (var conn = _connectionProvider.GetOpenPointingPokerConnection())
            {
                var command =
                    @"insert into Cards 
                    (Description, CreatedBy, ClosedBy, TeamId)
                    values (@Description, @CreatedBy, @ClosedBy, @TeamId)
                    select scope_identity()";
                var id = conn.ExecuteScalar<int>(command, card);
                return id;
            }
        }

        public void ClosePointing(int cardId, int userId)
        {
            using (var conn = _connectionProvider.GetOpenPointingPokerConnection())
            {
                var command =
                    @"update Cards
                    set ClosedBy = @userId
                    where Id = @cardId";
                conn.Execute(command, new { cardId, userId });
            }
        }

        public void OpenPointing(int cardId)
        {
            using (var conn = _connectionProvider.GetOpenPointingPokerConnection())
            {
                conn.Execute(
                    @"update Cards
                    set ClosedBy = null
                    where Id = @cardId", 
                    new { cardId });
            }
        }
    }
}
