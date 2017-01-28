using PointingPoker.DataAccess.Commands;
using PointingPoker.DataAccess.Models;
using PointingPoker.DataAccess.Queries;
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
            return _userQueries.GetUserByUserName(username);
        }

        public User GetUserById(int id)
        {
            return _userQueries.GetUserById(id);
        }

        public IEnumerable<User> GetUsers()
        {
            var users = _userQueries.GetUsers();
            return users;
        }

        public bool CreateUser(User user)
        {
            if (_userQueries.DoesUsernameExist(user.UserName))
            {
                return false;
            }

            if (_userQueries.DoesEmailExist(user.Email))
            {
                return false;
            }

            _userCommands.CreateUser(user);
            return true;
        }

        public bool UpdateUserInfo(int id, string username, string email)
        {
            if (
                id == 0 
                || string.IsNullOrEmpty(username)
                || string.IsNullOrEmpty(email))
            {
                return false;
            }

            if (_userQueries.DoesUserNameExist(id, username))
            {
                return false;
            }

            if (_userQueries.DoesEmailExist(id, email))
            {
                return false;
            }

            _userCommands.UpdateUserInfo(id, username, email);
            return true;
        }

        public bool UpdatePassword(int id, string newPassword)
        {
            if (id == 0 || string.IsNullOrEmpty(newPassword))
            {
                return false;
            }

            _userCommands.UpdatePassword(id, newPassword);
            return true;
        }

        public IEnumerable<string> GetUserNamesByTeam(int teamId)
        {
            return _userQueries.GetUserNamesByTeam(teamId);
        }

        public bool DoesUsernameExist(string username)
        {
            return _userQueries.DoesUsernameExist(username);
        }

        public bool DoesEmailExist(string email)
        {
            return _userQueries.DoesEmailExist(email);
        }
    }
}
