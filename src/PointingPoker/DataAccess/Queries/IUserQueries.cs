using PointingPoker.DataAccess.Models;
using System;
using System.Collections.Generic;

namespace PointingPoker.DataAccess.Queries
{
    public interface IUserQueries
    {
        IEnumerable<User> GetUsers();
        bool DoesUsernameExist(string username);
        bool DoesUsernameExist(Guid currentId, string username);
        User GetUserByUsername(string username);
    }
}
