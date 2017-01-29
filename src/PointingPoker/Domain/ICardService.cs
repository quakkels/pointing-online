using PointingPoker.DataAccess.Models;
using System;
using System.Collections.Generic;

namespace PointingPoker.Domain
{
    public interface ICardService
    {
        int CreateCard(Card card);
        IEnumerable<Card> GetCardsToPointForTeam(int userId, int teamId);
        IEnumerable<Card> GetOpenCardsForTeam(int teamId);
        Card GetCard(int cardId);
        bool ClosePointing(int cardId, int userId);
        IEnumerable<Card> GetClosedCardsForTeam(int teamId, int userId);
        void OpenPointing(int openId, int cardId);
    }
}
