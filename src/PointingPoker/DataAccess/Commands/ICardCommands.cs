﻿using PointingPoker.DataAccess.Models;

namespace PointingPoker.DataAccess.Commands
{
    public interface ICardCommands
    {
        int CreateCard(Card card);
    }
}
