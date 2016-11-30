﻿using PointingPoker.DataAccess.Models;
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
    }
}
