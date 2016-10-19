using System;
using PointingPoker.DataAccess.Models;
using PointingPoker.DataAccess.Commands;

namespace PointingPoker.Domain
{
    public class PointService : IPointService
    {
        private readonly IPointCommands _pointCommands;

        public PointService(IPointCommands pointCommands)
        {
            _pointCommands = pointCommands;
        }

        public bool PointCard(Point point)
        {
            if (point.Id == Guid.Empty
                || point.CardId == Guid.Empty
                || point.PointedBy == Guid.Empty)
            {
                return false;
            }

            _pointCommands.CreatePoint(point);
            return true;
        }
    }
}
