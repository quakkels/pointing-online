using PointingPoker.DataAccess.Commands;
using PointingPoker.DataAccess.Models;
using PointingPoker.DataAccess.Queries;
using System;
using System.Collections.Generic;

namespace PointingPoker.Domain
{
    public class UserService : IUserService
    {
        private readonly IUserQueries _userQueries;
        private readonly IUserCommands _userCommands;

        public UserService(IUserQueries userQueries, IUserCommands userCommands)
        {
            _userQueries = userQueries;
            _userCommands = userCommands;
        }
        public User GetUserByUsername(string username)
        {
            return _userQueries.GetUserByUsername(username);
        }

        public IEnumerable<User> GetUsers()
        {
            var users = _userQueries.GetUsers();
            return users;
        }

        public bool CreateUser(User user)
        {
            if (_userQueries.DoesUsernameExist(user.Username))
            {
                return false;
            }

            _userCommands.CreateUser(user);
            return true;
        }

        public bool UpdateUserInfo(Guid id, string username, string email)
        {
            if (
                id == Guid.Empty 
                || string.IsNullOrEmpty(username)
                || string.IsNullOrEmpty(email))
            {
                return false;
            }

            if (_userQueries.DoesUsernameExist(id, username))
            {
                return false;
            }

            _userCommands.UpdateUserInfo(id, username, email);
            return true;
        }

        public bool UpdatePassword(Guid id, string newPassword)
        {
            if (id == Guid.Empty || string.IsNullOrEmpty(newPassword))
            {
                return false;
            }

            _userCommands.UpdatePassword(id, newPassword);
            return true;
        }
    }
}
