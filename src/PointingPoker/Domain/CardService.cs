using PointingPoker.DataAccess.Commands;
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

        public bool CreateCard(Card card)
        {
            if (
                card.Id == Guid.Empty
                || card.CreatedBy == Guid.Empty
                || string.IsNullOrEmpty(card.Description)
                || card.TeamId == Guid.Empty)
            {
                return false;
            }

            if (!_cardQueries.DoesCardCreatorExist(card.CreatedBy))
            {
                return false;
            }

            _cardCommands.CreateCard(card);
            return true;
        }

        public IEnumerable<Card> GetCardsToPointForTeam(Guid userId, Guid teamId)
        {
            return _cardQueries.GetCardsToPointForTeam(userId, teamId);
        }

        public IEnumerable<Card> GetOpenCardsForTeam(Guid teamId)
        {
            return _cardQueries.GetOpenCardsForTeam(teamId);
        }
    }
}
