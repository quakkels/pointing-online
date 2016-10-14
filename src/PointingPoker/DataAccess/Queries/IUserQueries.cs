using PointingPoker.DataAccess.Models;
using System;
using System.Collections.Generic;

namespace PointingPoker.DataAccess.Queries
{
    public interface IUserQueries
    {
        IEnumerable<User> GetUsers();
        bool DoesUsernameExist(string userName);
        bool DoesUserNameExist(Guid currentId, string userName);
        User GetUserByUserName(string userName);
        User GetUserById(Guid id);
        string GetPasswordHashByUserName(string userName);
    }
}
