using PointingPoker.DataAccess.Models;
using System.Collections.Generic;

namespace PointingPoker.DataAccess
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();
    }
}
