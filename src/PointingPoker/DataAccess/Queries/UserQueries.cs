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

        public bool DoesUsernameExist(string userName)
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
                var exists = conn.Query<bool>(query, new { userName });
                return exists.FirstOrDefault();
            }
        }

        public bool DoesUserNameExist(Guid currentId, string userName)
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
                var exists = conn.Query<bool>(query, new { userName, id = currentId });
                return exists.FirstOrDefault();
            }
        }

        public User GetUserByUserName(string userName)
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
                var user = conn.Query<User>(query, new { userName });
                return user.FirstOrDefault();
            }
        }

        public User GetUserById(Guid id)
        {
            var query = @"
                select top 1
                    Id, Username, Email, PasswordHash 
                from 
                    Users
                where 
                    Id = @id";

            using (var conn = _connectionProvider.GetOpenPointingPokerConnection())
            {
                var user = conn.Query<User>(query, new { id });
                return user.FirstOrDefault();
            }
        }

        public string GetPasswordHashByUserName(string userName)
        {
            var query = @"
                select top 1
                    PasswordHash
                from
                    Users
                where
                    UserName = @userName";
            using (var conn = _connectionProvider.GetOpenPointingPokerConnection())
            {
                var passwordHash = conn.Query<string>(query, new { userName });
                return passwordHash.FirstOrDefault();
            }
        }
        public IEnumerable<string> GetUserNamesByTeam(int teamId)
        {
            using (var conn = _connectionProvider.GetOpenPointingPokerConnection())
            {
                var userNames = conn.Query<string>(
                    @"select u.userName 
                    from Users u
                    inner join TeamMembers tm on tm.UserId = u.Id
                    where tm.TeamId = @teamId",
                    new { teamId });

                return userNames;
            }
        }
    }
}
