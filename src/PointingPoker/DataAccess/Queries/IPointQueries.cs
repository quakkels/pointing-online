using PointingPoker.DataAccess.Models;
using System;
using System.Collections.Generic;

namespace PointingPoker.DataAccess.Queries
{
    public interface IPointQueries
    {
        int GetCardPoints(int cardId, int userId);
        IEnumerable<CardScore> GetCardPoints(int cardId);
    }
}