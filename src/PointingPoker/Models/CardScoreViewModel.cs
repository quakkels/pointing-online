using PointingPoker.DataAccess.Models;
using System.Collections.Generic;

namespace PointingPoker.Models
{
    public class CardScoreViewModel
    {
        public Card Card { get; set; }
        public IEnumerable<CardScore> CardScores { get; set; }
    }
}
