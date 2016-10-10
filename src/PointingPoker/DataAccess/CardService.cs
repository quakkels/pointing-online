using PointingPoker.DataAccess.Commands;
using PointingPoker.DataAccess.Models;
using PointingPoker.DataAccess.Queries;
using System;

namespace PointingPoker.DataAccess
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
                || string.IsNullOrEmpty(card.Description))
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
    }
}
