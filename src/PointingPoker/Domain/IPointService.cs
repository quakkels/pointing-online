using System;
using PointingPoker.DataAccess.Models;

namespace PointingPoker.Domain
{
    public interface IPointService
    {
        bool PointCard(Point point);
        int GetCardPoint(Guid cardId, Guid userId);
    }
}
