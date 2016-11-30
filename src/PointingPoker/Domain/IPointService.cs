using System;
using PointingPoker.DataAccess.Models;

namespace PointingPoker.Domain
{
    public interface IPointService
    {
        bool PointCard(Point point);
        int GetCardPoint(int cardId, int userId);
    }
}
