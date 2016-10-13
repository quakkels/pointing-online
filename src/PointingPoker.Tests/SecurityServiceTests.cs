using Microsoft.AspNetCore.Http;
using Moq;
using PointingPoker.DataAccess.Queries;
using PointingPoker.Domain;
using Xunit;

namespace PointingPoker.Tests
{
    public class SecurityServiceTests
    {
        private Mock<IUserQueries> _userQueryiesMock;
        private Mock<IHttpContextAccessor> _httpContextAccessor;

        private SecurityService _service;

        private void SetUp()
        {
            _httpContextAccessor = new Mock<IHttpContextAccessor>();

            _userQueryiesMock = new Mock<IUserQueries>();
            _userQueryiesMock
                .Setup(x => x.GetPasswordHashByUserName(It.Is<string>(y => y != "user")))
                .Returns((string)null);
            _userQueryiesMock
                .Setup(x => x.GetPasswordHashByUserName(It.Is<string>(y => y == "user")))
                .Returns("match");

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
    }
}
