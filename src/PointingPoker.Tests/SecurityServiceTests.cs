using Microsoft.AspNetCore.Http;
using Moq;
using PointingPoker.DataAccess.Models;
using PointingPoker.DataAccess.Queries;
using PointingPoker.Domain;
using PointingPoker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Xunit;

namespace PointingPoker.Tests
{
    public class SecurityServiceTests
    {
        private Mock<IUserQueries> _userQueryiesMock;
        private Mock<IHttpContextAccessor> _httpContextAccessor;
        private List<Claim> _claims;
        private User _user;
        private SecurityService _service;

        private void SetUp()
        {
            _claims = new List<Claim>();
            _claims.Add(new Claim("id", Guid.NewGuid().ToString()));

            _user = new User {
                Id = Guid.NewGuid(),
                UserName = "username",
                Email = "email",
                PasswordHash = "password"
            };

            _httpContextAccessor = new Mock<IHttpContextAccessor>();
            _httpContextAccessor
                .Setup(x => x.HttpContext.User.Claims)
                .Returns(_claims);

            _userQueryiesMock = new Mock<IUserQueries>();
            _userQueryiesMock
                .Setup(x => x.GetPasswordHashByUserName(It.Is<string>(y => y != "user")))
                .Returns((string)null);
            _userQueryiesMock
                .Setup(x => x.GetPasswordHashByUserName(It.Is<string>(y => y == "user")))
                .Returns("match");
            _userQueryiesMock
                .Setup(x => x.GetUserById(It.IsAny<Guid>()))
                .Returns(_user);

            _service = new SecurityService(
                _httpContextAccessor.Object,
                _userQueryiesMock.Object);
        }

        [Fact]
        public void ValidateMissingUserName()
        {
            // arrange
            SetUp();

            // act
            var result = _service.VerifyUserPassword("", "password");
            var result2 = _service.VerifyUserPassword(null, "password");

            // assert
            Assert.False(result);
            Assert.False(result2);
        }

        [Fact]
        public void ValidateMissingPassword()
        {
            // arrange
            SetUp();

            // act
            var result = _service.VerifyUserPassword("user", "");
            var result2 = _service.VerifyUserPassword("user", null);

            // assert
            Assert.False(result);
            Assert.False(result2);
        }

        [Fact]
        public void ValidatePasswordThatDoesNotMatch()
        {
            // arrange
            SetUp();

            // act
            var result = _service.VerifyUserPassword("user", "notAMatch");

            // assert
            Assert.False(result);
        }

        [Fact]
        public void ValidatePasswordThatMatches()
        {
            // arrange
            SetUp();

            // act
            var result = _service.VerifyUserPassword("user", "match");

            // assert
            Assert.True(result);
        }

        [Fact]
        public void ValidateUserNameThatDoesNotMatch()
        {
            // arrange
            SetUp();

            // act
            var result = _service.VerifyUserPassword("gibberishUser", "match");

            // assert
            Assert.False(result);
        }

        [Fact]
        public void CanGetSignedInUserId()
        {
            // arrange
            SetUp();

            // act
            var result = _service.GetCurrentUserId();

            // assert
            Assert.Equal(Guid.Parse(_claims[0].Value), result);
        }

        [Fact]
        public void CanGetSignedInUser()
        {
            // arrange
            SetUp();

            // act
            var result = _service.GetSignedInUser();

            // assert
            Assert.Equal(_user.Id, result.Id);
            Assert.Equal(_user.UserName, result.UserName);
        }
    }
}
