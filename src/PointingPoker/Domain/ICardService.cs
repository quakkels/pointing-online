using PointingPoker.DataAccess.Models;
using System;
using System.Collections.Generic;

namespace PointingPoker.Domain
{
    public interface ICardService
    {
        int CreateCard(Card card);
        IEnumerable<Card> GetCardsToPointForTeam(Guid userId, Guid teamId);
        IEnumerable<Card> GetOpenCardsForTeam(Guid teamId);
        Card GetCard(int cardId);
    }
}
