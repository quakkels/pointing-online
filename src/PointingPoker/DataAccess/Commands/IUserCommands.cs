using PointingPoker.DataAccess.Models;
using System;

namespace PointingPoker.DataAccess.Commands
{
    public interface IUserCommands
    {
        void CreateUser(User user);
        void UpdateUserInfo(int id, string username, string email);
        void UpdatePassword(int id, string passwordHash);
    }
}
