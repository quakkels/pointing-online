using System;
using PointingPoker.DataAccess.Models;
using System.Collections.Generic;

namespace PointingPoker.Domain
{
    public interface IPointService
    {
        bool PointCard(Point point);
        int GetCardPoint(int cardId, int userId);
        IEnumerable<CardScore> GetCardScore(int cardId);
    }
}
