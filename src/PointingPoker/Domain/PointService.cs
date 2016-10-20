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
        private readonly IPointQueries _pointQueries;

        public PointService(
            IPointCommands pointCommands,
            IPointQueries pointQueries,
            ICardQueries cardQueries)
        {
            _pointCommands = pointCommands;
            _pointQueries = pointQueries;
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

        public int GetCardPoint(Guid cardId, Guid userId)
        {
            return _pointQueries.GetCardPoints(cardId, userId);
        }
    }
}
