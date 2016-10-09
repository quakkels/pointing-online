using Dapper;
using PointingPoker.DataAccess.Models;
using System;

namespace PointingPoker.DataAccess.Commands
{
    public class UserCommands : IUserCommands
    {
        private readonly IDbConnectionProvider _connectionProvider;
        public UserCommands(IDbConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public void CreateUser(User user)
        {
            using (var conn = _connectionProvider.GetOpenPointingPokerConnection())
            {
                var command = "insert into dbo.Users values(@Id, @Username, @Email, @PasswordHash)";
                conn.Execute(command, user);
            }
        }

        public void UpdateUserInfo(Guid id, string username, string email)
        {
            using (var conn = _connectionProvider.GetOpenPointingPokerConnection())
            {
                var command = @"
                    update 
                        Users 
                    set 
                        Username = @username, 
                        Email = @email
                    where
                        Id = @id";
                conn.Execute(command, new { id, username, email });
            }
        }

        public void UpdatePassword(Guid id, string passwordHash)
        {
            using (var conn = _connectionProvider.GetOpenPointingPokerConnection())
            {
                var command = @"
                    update
                        Users
                    set
                        PasswordHash = @passwordHash
                    where
                        Id = @id";
                conn.Execute(command, new { id, passwordHash });
            }
        }
    }
}
