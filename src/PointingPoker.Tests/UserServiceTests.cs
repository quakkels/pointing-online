using Moq;
using PointingPoker.DataAccess;
using PointingPoker.DataAccess.Models;
using PointingPoker.DataAccess.Queries;
using System;
using System.Collections.Generic;
using Xunit;

namespace PointingPoker.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public void CanGetUsers()
        {
            // arrange
            var userQueriesMock = new Mock<IUserQueries>();
            userQueriesMock.Setup(x => x.GetUsers())
                .Returns(new List<User>
                {
                    new User{
                        Id = Guid.NewGuid(),
                        Username = "bob",
                        Email = "email@email"
                    }
                });
            var userService = new UserService(userQueriesMock.Object);

            // act
            var result = userService.GetUsers() as List<User>;

            // assert
            Assert.Equal("bob", result[0].Username);
        }
    }
}
