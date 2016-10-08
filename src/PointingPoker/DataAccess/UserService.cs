using PointingPoker.DataAccess.Models;
using PointingPoker.DataAccess.Queries;
using System.Collections.Generic;

namespace PointingPoker.DataAccess
{
    public class UserService : IUserService
    {
        private readonly IUserQueries _userQueries;

        public UserService(IUserQueries userQueries)
        {
            _userQueries = userQueries;
        }
        public IEnumerable<User> GetUsers()
        {
            var users = _userQueries.GetUsers();
            return users;
        }
    }
}
