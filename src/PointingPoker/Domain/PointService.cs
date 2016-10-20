using System;
using PointingPoker.DataAccess.Models;
using PointingPoker.DataAccess.Commands;
using PointingPoker.DataAccess.Queries;

namespace PointingPoker.Domain
{
    public class PointService : IPointService
    {
        private readonly IPointCommands _pointCommands;
        private readonly ICardQueries _cardQueries;

        public PointService(
            IPointCommands pointCommands,
            ICardQueries cardQueries)
        {
            _pointCommands = pointCommands;
            _cardQueries = cardQueries;
        }

        public bool PointCard(Point point)
        {
            if (point.Id == Guid.Empty
                || point.CardId == Guid.Empty
                || point.PointedBy == Guid.Empty)
            {
                return false;
            }

            var isCardClosed = _cardQueries
                .IsCardClosedForPointing(point.CardId);

            if (isCardClosed)
            {
                return false;
            }

            _pointCommands.CreatePoint(point);
            return true;
        }
    }
}
