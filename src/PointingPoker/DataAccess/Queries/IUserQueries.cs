using PointingPoker.DataAccess.Models;
using System.Collections.Generic;

namespace PointingPoker.DataAccess.Queries
{
    public interface IUserQueries
    {
        IEnumerable<User> GetUsers();
    }
}
