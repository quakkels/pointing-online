using Moq;
using PointingPoker.Domain;
using PointingPoker.DataAccess.Commands;
using PointingPoker.DataAccess.Models;
using PointingPoker.DataAccess.Queries;
using System;
using System.Collections.Generic;
using Xunit;

namespace PointingPoker.Tests
{
    public class UserServiceTests
    {
        private Mock<IUserQueries> _userQueriesMock;
        private Mock<IUserCommands> _userCommandsMock;
        private User _user;

        private void Setup()
        {
            _user = new User
            {
                UserName = "un1",
                Email = "em1@example.com",
                PasswordHash = "password1"
            };

            _userCommandsMock = new Mock<IUserCommands>();
            _userCommandsMock
                .Setup(x => x.CreateUser(It.IsAny<User>()));
            
            _userQueriesMock = new Mock<IUserQueries>();
            _userQueriesMock.Setup(x => x.GetUsers())
                .Returns(new List<User>
                {
                    new User{
                        Id = 1,
                        UserName = "bob",
                        Email = "email@email"
                    }
                });
        }

        [Fact]
        public void CanGetUsers()
        {
            // arrange
            Setup();
            var userService = new UserService(_userQueriesMock.Object, _userCommandsMock.Object);

            // act
            var result = userService.GetUsers() as List<User>;

            // assert
            Assert.Equal("bob", result[0].UserName);
        }

        [Fact]
        public void CanAddUserWhenUsernameIsNew()
        {
            // arrange
            Setup();
            _userQueriesMock.Setup(x => x.DoesUsernameExist(It.IsAny<string>())).Returns(false);
            var userService = new UserService(_userQueriesMock.Object, _userCommandsMock.Object);

            // act
            var result = userService.CreateUser(new User());

            // assert
            Assert.True(result);
        }

        [Fact]
        public void CanNotAddUserWhenUsernameIsNotNew()
        {
            // arrange
            Setup();
            _userQueriesMock.Setup(x => x.DoesUsernameExist(It.IsAny<string>())).Returns(true);
            var userService = new UserService(_userQueriesMock.Object, _userCommandsMock.Object);

            // act
            var result = userService.CreateUser(new User());

            // assert
            Assert.False(result);
        }

        [Fact]
        public void CanGetUser()
        {
            // arrange
            Setup();
            _userQueriesMock
                .Setup(x => x.GetUserByUserName(It.IsAny<string>()))
                .Returns(_user);
            var service = new UserService(
                _userQueriesMock.Object,
                _userCommandsMock.Object);

            // act
            var result = service.GetUserByUsername("any");

            // assert
            Assert.Equal("un1", result.UserName);
        }

        [Fact]
        public void WillNotUpdateUserInfoWithoutId()
        {
            // arrange
            Setup();
            var service = new UserService(
                _userQueriesMock.Object,
                _userCommandsMock.Object);

            // act
            var result = service.UpdateUserInfo(0, "username", "email");

            // assert
            Assert.False(result);
        }

        [Fact]
        public void WillNotUpdateUserInfoWithoutEmail()
        {
            // arrange
            Setup();
            var service = new UserService(
                _userQueriesMock.Object,
                _userCommandsMock.Object);

            // act
            var result = service.UpdateUserInfo(1, "username", null);
            var result2 = service.UpdateUserInfo(1, "username", "");

            // assert
            Assert.False(result);
            Assert.False(result2);
        }

        [Fact]
        public void WillNotUpdateUserInfoWithoutUsername()
        {
            // arrange
            Setup();
            var service = new UserService(
                _userQueriesMock.Object,
                _userCommandsMock.Object);

            // act
            var result = service.UpdateUserInfo(1, "", "email");
            var result2 = service.UpdateUserInfo(1, null, "email");

            // assert
            Assert.False(result);
            Assert.False(result2);
        }

        [Fact]
        public void WillNotUpdateUserInfoWithDuplicateUsername()
        {
            // arrange
            Setup();
            _userQueriesMock
                .Setup(x => x.DoesUserNameExist(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(true);
            var service = new UserService(
                _userQueriesMock.Object,
                _userCommandsMock.Object);

            // act
            var result = service.UpdateUserInfo(1, "duplicate", "email");

            // assert
            Assert.False(result);
        }

        [Fact]
        public void CanUpdateUserInfo()
        {
            // arrange
            Setup();
            var service = new UserService(
                _userQueriesMock.Object,
                _userCommandsMock.Object);

            // act
            var result = service.UpdateUserInfo(1, "username", "email");

            Assert.True(result);
            _userCommandsMock.Verify(
                x => x.UpdateUserInfo(
                    It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Once);
        }

        [Fact]
        public void WillNotUpdatePasswordWithoutId()
        {
            // arrange
            Setup();
            var service = new UserService(
                _userQueriesMock.Object,
                _userCommandsMock.Object);

            // act
            var result = service.UpdatePassword(0, "password");

            // assert
            Assert.False(result);
        }

        [Fact]
        public void WillNotUpdatePasswordWithoutPassword()
        {
            // arrange
            Setup();
            var service = new UserService(
                _userQueriesMock.Object,
                _userCommandsMock.Object);

            // act
            var result = service.UpdatePassword(1, "");
            var result2 = service.UpdatePassword(1, null);

            // assert
            Assert.False(result);
            Assert.False(result2);
        }

        [Fact]
        public void CanUpdatePassword()
        {
            // arrange
            Setup();
            var service = new UserService(
                _userQueriesMock.Object,
                _userCommandsMock.Object);

            // act
            var result = service.UpdatePassword(1, "newpassword");

            // assert
            Assert.True(result);
            _userCommandsMock.Verify(
                x => x.UpdatePassword(
                    It.IsAny<int>(),
                    It.IsAny<string>()),
                Times.Once);
        }
    }
}
