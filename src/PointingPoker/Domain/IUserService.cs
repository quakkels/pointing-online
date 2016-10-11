using PointingPoker.DataAccess.Models;
using System;
using System.Collections.Generic;

namespace PointingPoker.Domain
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();
        bool CreateUser(User user);
        User GetUserByUsername(string username);
        bool UpdateUserInfo(Guid id, string username, string email);
        bool UpdatePassword(Guid id, string newPassword);
    }
}
