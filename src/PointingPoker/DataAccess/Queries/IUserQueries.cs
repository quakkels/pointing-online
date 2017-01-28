using PointingPoker.DataAccess.Models;
using System;
using System.Collections.Generic;

namespace PointingPoker.DataAccess.Queries
{
    public interface IUserQueries
    {
        IEnumerable<User> GetUsers();
        bool DoesUsernameExist(string userName);
        bool DoesUserNameExist(int currentId, string userName);
        User GetUserByUserName(string userName);
        User GetUserById(int id);
        string GetPasswordHashByUserName(string userName);
        IEnumerable<string> GetUserNamesByTeam(int teamId);
        bool DoesEmailExist(string userName);
        bool DoesEmailExist(int currentUserId, string email);
    }
}
