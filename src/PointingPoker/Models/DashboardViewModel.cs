using PointingPoker.DataAccess.Models;
using System.Collections.Generic;

namespace PointingPoker.Models
{
    public class DashboardViewModel
    {
        public IEnumerable<Team> Teams { get; set; }
    }
}
