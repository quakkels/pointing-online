using PointingPoker.DataAccess.Models;
using System;
using System.Collections.Generic;

namespace PointingPoker.Domain
{
    public interface ICardService
    {
        bool CreateCard(Card card);
        IEnumerable<Card> GetCardsToPointForTeam(Guid userId, Guid teamId);
        IEnumerable<Card> GetOpenCardsForTeam(Guid teamId);
        Card GetCard(Guid cardId);
    }
}
