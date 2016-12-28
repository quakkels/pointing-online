using PointingPoker.DataAccess.Models;
using System;
using System.Collections.Generic;

namespace PointingPoker.Models
{
    public class TeamSummaryViewModel
    {
        public Team Team { get; set; }
        public IEnumerable<string> TeamUserNames { get; set; }
        public IEnumerable<Team> Teams { get; set; }
        public IEnumerable<Card> CardsToPoint { get; set; }
        public IEnumerable<Card> PointedCards { get; set; }
        public IEnumerable<Card> ClosedCards { get; set; }
    }
}
