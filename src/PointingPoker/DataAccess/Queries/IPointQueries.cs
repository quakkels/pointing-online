﻿using System;

namespace PointingPoker.DataAccess.Queries
{
    public interface IPointQueries
    {
        int GetCardPoints(int cardId, Guid userId);
    }
}