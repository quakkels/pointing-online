using Dapper;
using PointingPoker.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
            using(var conn= _connectionProvider.GetOpenPointingPokerConnection())
            {
                var users = conn.Query<User>("select * from users");
                return users;
            }
        }

        public bool DoesUsernameExist(string username)
        {
            var query = @"
                select 
	                case when
                        exists(
                            select 1
                            from Users
                            where username = @username)
                    then cast(1 as bit)
	                else cast(0 as bit)
                    end as [exists]";
            using (var conn = _connectionProvider.GetOpenPointingPokerConnection())
            {
                var exists = conn.Query<bool>(query, new { username });
                return exists.FirstOrDefault();
            }
        }

        public bool DoesUsernameExist(Guid currentId, string username)
        {
            var query = @"
                select 
	                case when
                        exists(
                            select 1
                            from Users
                            where 
                                username = @username
                                and Id <> @id)
                    then cast(1 as bit)
	                else cast(0 as bit)
                    end as [exists]";
            using (var conn = _connectionProvider.GetOpenPointingPokerConnection())
            {
                var exists = conn.Query<bool>(query, new { username, id = currentId });
                return exists.FirstOrDefault();
            }
        }

        public User GetUserByUsername(string username)
        {
            var query = @"
                select top 1
                    Id, Username, Email, PasswordHash 
                from 
                    Users
                where 
                    Username = @username";

            using (var conn = _connectionProvider.GetOpenPointingPokerConnection())
            {
                var user = conn.Query<User>(query, new { username });
                return user.FirstOrDefault();
            }
        }
    }
}
