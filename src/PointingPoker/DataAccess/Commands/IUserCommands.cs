using PointingPoker.DataAccess.Models;
using System;

namespace PointingPoker.DataAccess.Commands
{
    public interface IUserCommands
    {
        void CreateUser(User user);
        void UpdateUserInfo(Guid id, string username, string email);
        void UpdatePassword(Guid id, string passwordHash);
    }
}
