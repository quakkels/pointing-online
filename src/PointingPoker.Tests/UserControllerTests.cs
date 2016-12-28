using Moq;
using PointingPoker.Controllers;
using PointingPoker.DataAccess.Models;
using PointingPoker.Domain;
using PointingPoker.Models;
using System;
using Xunit;
namespace PointingPoker.Tests
{
    public class UserControllerTests
    {
        private Mock<ISecurityService> _securityServiceMock;
        private Mock<IUserService> _userServiceMock;
        private ProfileViewModel _profileViewModel;
        private User _user;

        private void Setup()
        {
            _profileViewModel = new ProfileViewModel
            {
                Id = 1,
                UserName = "username",
                Email = "email",
                Password = "password",
                VerifyPassword = "password"                        
            };

            _user = new User
            {
                UserName = "user",
                PasswordHash = "match",
                Email = "email"
            };

            _securityServiceMock = new Mock<ISecurityService>();
            _securityServiceMock
                .Setup(x => x.VerifyUserPassword(It.Is<string>(y => y == _user.UserName), It.Is<string>(y => y == _user.PasswordHash)))
                .Returns(true);

            _userServiceMock = new Mock<IUserService>();
            _userServiceMock
                .Setup(x => x.UpdatePassword(
                    _profileViewModel.Id,
                    _profileViewModel.Password))
                .Returns(true);
            _userServiceMock
                .Setup(x => x.UpdateUserInfo(
                    _profileViewModel.Id,
                    _profileViewModel.UserName,
                    _profileViewModel.Email))
                .Returns(true);
            _userServiceMock
                .Setup(x => x.GetUserByUsername(It.Is<string>(y => y == _user.UserName)))
                .Returns(_user);
        }

        [Fact]
        public void CanPostNewProfileInfoWithoutUpdatingPassword()
        {
            // arrange
            Setup();
            _profileViewModel.Password = null;
            var controller = new UserController(
                _securityServiceMock.Object,
                _userServiceMock.Object);

            // act
            var result = controller.Profile(_profileViewModel);

            // assert
            Assert.True(controller.ModelState.IsValid);
            _userServiceMock.Verify(
                x => x.UpdateUserInfo(
                    _profileViewModel.Id,
                    _profileViewModel.UserName,
                    _profileViewModel.Email),
                Times.Once);
            _userServiceMock.Verify(
                x => x.UpdatePassword(
                    It.IsAny<int>(),
                    It.IsAny<string>()),
                Times.Never);
        }

        [Fact]
        public void CanUpdatePasswordAndProfileInfo()
        {
            // arrange
            Setup();
            var controller = new UserController(
                _securityServiceMock.Object,
                _userServiceMock.Object);

            // act
            var result = controller.Profile(_profileViewModel);

            // assert
            Assert.True(controller.ModelState.IsValid);
            _userServiceMock.Verify(
                x => x.UpdateUserInfo(
                    _profileViewModel.Id,
                    _profileViewModel.UserName,
                    _profileViewModel.Email),
                Times.Once);
            _userServiceMock.Verify(
                x => x.UpdatePassword(
                    _profileViewModel.Id,
                    _profileViewModel.Password),
                Times.Once);
        }

        [Fact]
        public void WillNotSignInWithBadCredentials()
        {
            // arrange
            Setup();
            var controller = new UserController(
                _securityServiceMock.Object,
                _userServiceMock.Object);

            // act
            var result = controller.SignIn(new SignInViewModel { UserName = "user", Password = "bad" });

            // assert
            _securityServiceMock.Verify(x => x.SignIn(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void WillSignInWithGoodCredentials()
        {
            // arrange
            Setup();
            var controller = new UserController(
                _securityServiceMock.Object,
                _userServiceMock.Object);

            // act
            var result = controller.SignIn(new SignInViewModel { UserName = "user", Password = "match" });

            // assert
            _securityServiceMock.Verify(x => x.SignIn(It.IsAny<int>()), Times.Once);
        }
    }
}
