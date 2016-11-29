﻿using PointingPoker.DataAccess.Commands;
using PointingPoker.DataAccess.Models;
using PointingPoker.DataAccess.Queries;
using System;
using System.Collections.Generic;

namespace PointingPoker.Domain
{
    public class CardService : ICardService
    {
        private readonly ICardCommands _cardCommands;
        private readonly ICardQueries _cardQueries;

        public CardService(ICardCommands cardCommands, ICardQueries cardQueries)
        {
            _cardCommands = cardCommands;
            _cardQueries = cardQueries;
        }

        public int CreateCard(Card card)
        {
            if (
                card.CreatedBy == Guid.Empty
                || string.IsNullOrEmpty(card.Description)
                || card.TeamId == Guid.Empty)
            {
                return 0;
            }

            if (!_cardQueries.DoesCardCreatorExist(card.CreatedBy))
            {
                return 0;
            }

            var id = _cardCommands.CreateCard(card);
            card.Id = id;
            return id;
        }

        public IEnumerable<Card> GetCardsToPointForTeam(Guid userId, Guid teamId)
        {
            return _cardQueries.GetCardsToPointForTeam(userId, teamId);
        }

        public IEnumerable<Card> GetOpenCardsForTeam(Guid teamId)
        {
            return _cardQueries.GetOpenCardsForTeam(teamId);
        }

        public Card GetCard(int cardId)
        {
            return _cardQueries.GetCard(cardId);
        }
    }
}
