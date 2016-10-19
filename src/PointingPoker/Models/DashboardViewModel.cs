using PointingPoker.DataAccess.Models;
using System.Collections.Generic;

namespace PointingPoker.Models
{
    public class DashboardViewModel
    {
        public IEnumerable<Card> NeedsPoints { get; set; }
        public IEnumerable<Card> Cards { get; set; }
        public IEnumerable<Team> Teams { get; set; }
    }
}
