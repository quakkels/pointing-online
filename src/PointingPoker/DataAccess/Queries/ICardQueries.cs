using PointingPoker.DataAccess.Models;
using System;
using System.Collections.Generic;

namespace PointingPoker.DataAccess.Queries
{
    public interface ICardQueries
    {
        bool DoesCardCreatorExist(Guid creatorId);
        IEnumerable<Card> GetCardsToPointByUser(Guid userId);
        IEnumerable<Card> GetCardsToPointForTeam(Guid userId, Guid teamId);
        IEnumerable<Card> GetOpenCardsForTeam(Guid teamId);
        Card GetCard(int cardId);
        bool IsCardClosedForPointing(int cardId);
    }
}
