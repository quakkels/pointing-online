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

        public void CreateCard(Card card)
        {
            using (var conn = _connectionProvider.GetOpenPointingPokerConnection())
            {
                var command = 
                    @"insert into Cards 
                    (Id, Description, CreatedBy, IsPointingClosed, TeamId)
                    values (@Id, @Description, @CreatedBy, @IsPointingClosed, @TeamId)";
                conn.Execute(command, card);
            }
        }
    }
}