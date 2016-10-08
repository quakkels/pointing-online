using Dapper;
using PointingPoker.DataAccess.Models;
using System.Collections.Generic;

namespace PointingPoker.DataAccess.Queries
{
    public class UserQueries : IUserQueries
    {
        private readonly IDbConnectionProvider _connectionProvider;
        public UserQueries(IDbConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public IEnumerable<User> GetUsers()
        {
            using(var connection = _connectionProvider.GetOpenPointingPokerConnection())
            {
                var users = connection.Query<User>("select * from users");
                return users;
            }
        }
    }
}
