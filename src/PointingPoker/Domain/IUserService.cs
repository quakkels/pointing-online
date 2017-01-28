using PointingPoker.DataAccess.Models;
using System.Collections.Generic;

namespace PointingPoker.Domain
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();
        bool CreateUser(User user);
        User GetUserByUsername(string username);
        User GetUserById(int id);
        bool UpdateUserInfo(int id, string username, string email);
        bool UpdatePassword(int id, string newPassword);
        IEnumerable<string> GetUserNamesByTeam(int teamId);
        bool DoesUsernameExist(string username);
        bool DoesEmailExist(string email);
    }
}
