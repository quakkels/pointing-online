﻿using Dapper;
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
    }
}
