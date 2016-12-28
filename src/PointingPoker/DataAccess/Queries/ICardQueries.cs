using PointingPoker.DataAccess.Models;
using System;
using System.Collections.Generic;

namespace PointingPoker.DataAccess.Queries
{
    public interface ICardQueries
    {
        bool DoesCardCreatorExist(int creatorId);
        IEnumerable<Card> GetCardsToPointByUser(int userId);
        IEnumerable<Card> GetCardsToPointForTeam(int userId, int teamId);
        IEnumerable<Card> GetOpenCardsForTeam(int teamId);
        Card GetCard(int cardId);
        bool IsCardClosedForPointing(int cardId);
        IEnumerable<Card> GetClosedCardsForTeam(int teamId);
    }
}
